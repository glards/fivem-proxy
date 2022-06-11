// See https://aka.ms/new-console-template for more information

using System.Buffers;
using System.Buffers.Binary;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Channels;
using PacketDotNet;
using Protocol;
using Protocol.ENet;
using Protocol.FiveM;
using SharpPcap;
using SharpPcap.LibPcap;

string traceOutputPath = @"D:\Temp\trace\";

var localCapture = CaptureDeviceList.Instance.OfType<LibPcapLiveDevice>().FirstOrDefault(x => x.Loopback);

if (localCapture == null)
{
    Console.WriteLine("Unable to find Loopback capture");
    Environment.Exit(-1);
}

ushort port = 30120;

var protocolSink = new ProtocolSink();

localCapture.Open();
localCapture.Filter = $"udp port {port}";
localCapture.OnPacketArrival +=  (sender, capture) =>
{
    var udp = capture.GetPacket().GetPacket().Extract<UdpPacket>();
    if (udp != null)
    {
        bool fromServer = udp.SourcePort == port;

        if (fromServer)
        {
            protocolSink.ServerBoundChannel.Writer.TryWrite(udp.PayloadData);
        }
        else
        {
            protocolSink.ClientBoundChannel.Writer.TryWrite(udp.PayloadData);
        }
    }

    //var tcp = capture.GetPacket().GetPacket().Extract<TcpPacket>();
    //var slice = capture.Data.Slice(0, 2);
    //if (!BinaryPrimitives.TryReadUInt16LittleEndian(slice, out ushort peerId))
    //{
    //    return;
    //}

    //if (!BinaryPrimitives.TryReadUInt16LittleEndian(capture.Data.Slice(2, 2), out ushort sentTime))
    //{
    //    return;
    //}

    //Console.WriteLine($"Got packet with peerId '{peerId}' and sentTIme '{sentTime}'");
};

Channel<byte[]> dummyChannel = Channel.CreateUnbounded<byte[]>();

_ = ParseENet("S->C", protocolSink.ServerBoundChannel.Reader, dummyChannel.Writer, traceOutputPath);
_ = ParseENet("C->S", protocolSink.ClientBoundChannel.Reader, dummyChannel.Writer, traceOutputPath);

localCapture.StartCapture();

Console.WriteLine("Press enter to stop...");

Console.ReadLine();

localCapture.StopCapture();


static SyncTree? GetSyncTreeForEntity(Entity entity)
{
    return entity.ObjectType switch
    {
        0 => SyncTreeFive.CAutomobileSyncTree,
        1 => SyncTreeFive.CBikeSyncTree,
        2 => SyncTreeFive.CBoatSyncTree,
        3 => SyncTreeFive.CDoorSyncTree,
        4 => SyncTreeFive.CHeliSyncTree,
        5 => SyncTreeFive.CObjectSyncTree,
        6 => SyncTreeFive.CPedSyncTree,
        7 => SyncTreeFive.CPickupSyncTree,
        8 => SyncTreeFive.CPickupPlacementSyncTree,
        9 => SyncTreeFive.CPlaneSyncTree,
        10 => SyncTreeFive.CSubmarineSyncTree,
        11 => SyncTreeFive.CPlayerSyncTree,
        13 => SyncTreeFive.CTrainSyncTree,
        _ => null
    };
}

static async Task ParseENet(string direction, ChannelReader<byte[]> packetSink, ChannelWriter<byte[]> dataSink, string tracePath, CancellationToken ct = default)
{
    uint lastIndex = 0;
    uint lastTimestamp = 0;
    Guid? currentTraceId = null;
    Dictionary<ushort, Entity> entities = new Dictionary<ushort, Entity>();
    await foreach (var packet in packetSink.ReadAllAsync().WithCancellation(ct))
    {
        if (BitConverter.ToUInt32(packet, 0) == 0xFFFFFFFF)
        {
            string text = Encoding.UTF8.GetString(new ReadOnlySpan<byte>(packet)[4..]);
            Console.WriteLine($"[{direction}]: OOB packet : {text}");
        }
        else
        {
            ENetPacketReader reader = new ENetPacketReader(new ReadOnlySequence<byte>(packet));
            foreach (var read in reader.ReadPackets())
            {
                bool cloneParseSuccessful = true;
                bool interestingPacket = false;
                StringBuilder sb = new StringBuilder();

                sb.Append($"[{direction}:{read.Header.PeerId}]:");
                sb.Append($" ENet [CMD:{read.CommandHeader.Command}");

                RagePacketReader? rageReader = null;

                if (read.Packet is ENetProtocolSendReliable reliablePacket)
                {
                    sb.Append($" Reliable packet SEQ:{read.CommandHeader.ReliableSequenceNumber} {reliablePacket.DataLength}bytes");
                    rageReader = new RagePacketReader(new ReadOnlySequence<byte>(read.Data));
                } else if (read.Packet is ENetProtocolSendUnreliable unreliablePacket)
                {

                    sb.Append($" Unreliable packet USEQ:{unreliablePacket.UnreliableSequenceNumber} {unreliablePacket.DataLength}bytes");
                    rageReader = new RagePacketReader(new ReadOnlySequence<byte>(read.Data));
                } else if (read.Packet is ENetProtocolSendUnsequenced unsequencedPacket)
                {
                    sb.Append($" Unsequenced packet GROUP:{unsequencedPacket.UnsequencedGroup} {unsequencedPacket.DataLength}bytes");
                    rageReader = new RagePacketReader(new ReadOnlySequence<byte>(read.Data));
                }

                sb.Append("]");

                var ragePacket = rageReader?.ReadPacket();
                if (ragePacket != null)
                {
                    sb.Append($" RAGE [{ragePacket.Name} {ragePacket.RawData.Length}bytes]");
                }

                if (ragePacket is RoutePacket routePacket)
                {
                    //interestingPacket = true;
                    
                    sb.Append($" Route [Target:0x{Convert.ToHexString(BitConverter.GetBytes(routePacket.TargetId))} {routePacket.TypeString}]");

                    try
                    {
                        //sb.Append("[");
                        CloneParser parser = new CloneParser(routePacket.CloneData.ToArray());
                        foreach (var act in parser.Parse())
                        {
                            //sb.Append(" ");
                            //sb.Append(act.ToString());

                            if (act is ClonePacket clonePacket)
                            {
                                sb.Append(" [");
                                if (clonePacket.IsCreation)
                                {
                                    sb.Append("CloneCreate");
                                }
                                else
                                {
                                    sb.Append("CloneUpdate");
                                }
                                sb.Append($" ObjectId:{clonePacket.ObjectId}");
                                sb.Append($" Uniqifier:{clonePacket.Uniqifier}");

                                if (clonePacket.IsCreation)
                                {
                                    sb.Append($" ObjectType:{clonePacket.ObjectType}");
                                }
                                
                                sb.Append($" SyncTree:{Convert.ToHexString(clonePacket.SyncData)}");
                                sb.Append("]");
                                if (!entities.TryGetValue(clonePacket.Uniqifier, out var entity))
                                {
                                    entity = new Entity()
                                    {
                                        ObjectId = clonePacket.ObjectId,
                                        Uniqifier = clonePacket.Uniqifier
                                    };

                                    if (clonePacket.ObjectType != 255)
                                    {
                                        entity.ObjectType = clonePacket.ObjectType;
                                    }

                                    entities[clonePacket.Uniqifier] = entity;
                                }

                                if (currentTraceId != null && entity.ParticipatingTraceId != currentTraceId)
                                {
                                    entity.ParticipatingTraceId = currentTraceId;
                                    entity.Actions.Add(new CloneActionWrapper(lastIndex, lastTimestamp, new StartTrace(currentTraceId.Value)));
                                }

                                entity.Actions.Add(new CloneActionWrapper(lastIndex, lastTimestamp, act));
                            } else if (act is CloneRemove cloneRemove)
                            {
                                if (!entities.TryGetValue(cloneRemove.Uniqifier, out var entity))
                                {
                                    entity = new Entity()
                                    {
                                        ObjectId = cloneRemove.ObjectId,
                                        Uniqifier = cloneRemove.Uniqifier
                                    };

                                    entities[cloneRemove.Uniqifier] = entity;
                                }

                                if (currentTraceId != null && entity.ParticipatingTraceId != currentTraceId)
                                {
                                    entity.ParticipatingTraceId = currentTraceId;
                                    entity.Actions.Add(new CloneActionWrapper(lastIndex, lastTimestamp, new StartTrace(currentTraceId.Value)));
                                }

                                entity.Actions.Add(new CloneActionWrapper(lastIndex, lastTimestamp, act));

                            } else if (act is SetIndex cloneSetIndex)
                            {
                                lastIndex = cloneSetIndex.Index;
                            } else if (act is SetTimestamp cloneSetTimestamp)
                            {
                                lastTimestamp = cloneSetTimestamp.Timestamp;
                            }
                        }
                        //sb.Append("]");
                    }
                    catch
                    {
                        cloneParseSuccessful = false;
                    }
                } else if (ragePacket is ServerEventPacket sePacket)
                {

                    interestingPacket = true;

                    sb.Append($" ServerEvent [{sePacket.EventName} Payload:{Convert.ToHexString(sePacket.EventPayload)}]");

                    if (sePacket.EventName.Equals("capture:startTrace") && currentTraceId == null)
                    {
                        currentTraceId = Guid.NewGuid();
                        sb.Append($" STARTING TRACE {currentTraceId:N}");
                    }

                    if (sePacket.EventName.Equals("capture:stopTrace") && currentTraceId != null)
                    {
                        HashSet<Entity> participatingEntities = new HashSet<Entity>();
                        foreach (var entity in entities.Values)
                        {
                            if (entity.ParticipatingTraceId != null)
                            {
                                participatingEntities.Add(entity);

                                entity.ParticipatingTraceId = null;
                                entity.Actions.Add(new CloneActionWrapper(lastIndex , lastTimestamp, new StopTrace(currentTraceId.Value)));
                            }
                        }

                        using var fs = File.OpenWrite(Path.Combine(tracePath, $"{currentTraceId:N}.txt"));
                        using var sw = new StreamWriter(fs);

                        sw.WriteLine($"BEGIN TRACE {currentTraceId:N}");
                        foreach (var entity in participatingEntities)
                        {
                            bool isInsideTrace = false;
                            sw.WriteLine("----------------------------------------");
                            sw.WriteLine($"ENTITY ObjectType:{entity.ObjectType} ObjectId:{entity.ObjectId} Uniqifier:{entity.Uniqifier}");
                            sw.WriteLine($"ACTIONS:");

                            foreach (var action in entity.Actions)
                            {
                                if (action.Action is StartTrace startTrace && startTrace.TraceId == currentTraceId)
                                {
                                    isInsideTrace = true;
                                }

                                if (!isInsideTrace && OnlyLogTrace)
                                {
                                    continue;
                                }

                                sw.WriteLine($"[I:{action.FrameIndex}|TS:{action.FrameIndex}] {action.Action}");
                                if (action.Action is ClonePacket clonePacket)
                                {
                                    sw.WriteLine($"SyncTree: {Convert.ToHexString(clonePacket.SyncData)}");
                                    
                                    var syncTree = GetSyncTreeForEntity(entity);
                                    if (syncTree != null)
                                    {
                                        try
                                        {
                                            StringBuilder sbSt = new StringBuilder();
                                            var state = new SyncTreeParserState(clonePacket.SyncData, clonePacket.IsCreation);
                                            syncTree.Parse(state, new SyncTreeOutputVisitor(sbSt));
                                            sw.Write(sbSt.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            sw.WriteLine("ERROR: " + ex.ToString());
                                        }
                                    }
                                }
                            }
                        }

                        sw.WriteLine($"END TRACE {currentTraceId:N}");

                        sb.Append($" STOPPING TRACE {currentTraceId:N}");
                        currentTraceId = null;
                    }

                }

                if (interestingPacket)
                {
                    lock (OutputLock)
                    {
                        string log = sb.ToString();
                        var color = Console.ForegroundColor;
                        Console.ForegroundColor = cloneParseSuccessful ? ConsoleColor.Green : ConsoleColor.Red;
                        Console.WriteLine(log);
                        Console.ForegroundColor = color;
                    }
                }
            }
        }
    }
}


public partial class Program
{
    public static readonly bool OnlyLogTrace = true;
    public static readonly object OutputLock = new object();
}

public record CloneActionWrapper(uint FrameIndex, uint Timestamp, ICloneAction Action);

public class Entity
{
    public ushort ObjectId { get; set; }
    public ushort Uniqifier { get; set; }

    public byte ObjectType { get; set; }

    public Guid? ParticipatingTraceId { get; set; }

    public List<CloneActionWrapper> Actions { get; set; } = new List<CloneActionWrapper>();
}
public class SyncTreeOutputVisitor : ISyncTreeVisitor
{
    private readonly StringBuilder _output;
    public SyncTreeOutputVisitor(StringBuilder output)
    {
        _output = output;
    }

    public void Visit(SyncTreeParserState state, NodeIds ids, Node node, DataNode? dataNode)
    {
        _output.Append(" * Visiting ");

        if (node is ParentNode parentNode)
        {
            _output.Append($"parent node with Id {parentNode.Ids}");
        }
        else if (node is NodeWrapper nodeWrapper)
        {

            _output.Append($"node of type {nodeWrapper.Data.GetType()} with Id {nodeWrapper.Ids}");

            if (dataNode != null)
            {
                _output.Append($" with data {Convert.ToHexString(dataNode.Data)} and bits {dataNode.Data.ToBitString()}");
            }
        }

        _output.AppendLine();
    }
}