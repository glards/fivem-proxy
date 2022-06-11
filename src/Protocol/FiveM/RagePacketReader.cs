using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using K4os.Compression.LZ4;

namespace Protocol.FiveM
{
    public class RagePacket
    {
        public static RagePacket CreateBasePacket(uint hash, ReadOnlySequence<byte> packetData)
        {
            return new RagePacket(hash, packetData);
        }

        public uint Hash { get; }

        public ReadOnlySequence<byte> RawData { get; }

        public string Name => Five.MapHashPacket.TryGetValue(Hash, out string? name)
            ? name
            : string.Concat("unk", Convert.ToHexString(BitConverter.GetBytes(Hash)).ToUpper());


        private RagePacket(uint hash, ReadOnlySequence<byte> packetData)
        {
            Hash = hash;
            RawData = packetData;
        }

        public RagePacket(RagePacket basePacket)
        {
            Hash = basePacket.Hash;
            RawData = basePacket.RawData;
        }
    }

    public class RagePacketReader
    {
        private static readonly Dictionary<uint, RagePacketHandler> s_handlerMaps;
        private static readonly RagePacketHandler[] s_handlers = new RagePacketHandler[]
        {
            new RoutePacketHandler(),
            new ServerEventPacketHandler(),
            //new PackedClonePacketHandler(),
        };

        static RagePacketReader()
        {
            s_handlerMaps = new Dictionary<uint, RagePacketHandler>();
            foreach (var handler in s_handlers)
            {
                s_handlerMaps[Five.HashRageString(handler.PacketId)] = handler;
            }
        }

        private readonly ReadOnlySequence<byte> _data;

        public RagePacketReader(ReadOnlySequence<byte> data)
        {
            _data = data;
        }

        public RagePacket ReadPacket()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_data);
            reader.TryReadLittleEndian(out int iHash);

            uint hash = (uint)iHash;

            var basePacket = RagePacket.CreateBasePacket(hash, reader.UnreadSequence);

            if (s_handlerMaps.TryGetValue(hash, out var handler))
            {
                return handler.ParseSubPacket(basePacket);
            }

            return basePacket;
        }


    }

    public abstract class RagePacketHandler
    {
        public abstract string PacketId { get; }

        public abstract RagePacket ParseSubPacket(RagePacket packet);
    }

    public abstract class RagePacketHandler<T> : RagePacketHandler where T : RagePacket
    {

        public override RagePacket ParseSubPacket(RagePacket packet)
        {
            return Parse(packet) ?? packet;
        }

        public abstract T? Parse(RagePacket packet);
    }

    public class ServerEventPacket : RagePacket
    {
        public string EventName { get; set; }

        public byte[] EventPayload { get; set; }

        public ServerEventPacket(RagePacket basePacket, string eventName, byte[] eventPayload) : base(basePacket)
        {
            EventName = eventName;
            EventPayload = eventPayload;
        }
    }

    public class ServerEventPacketHandler : RagePacketHandler<ServerEventPacket>
    {

        public override string PacketId => "msgServerEvent";
        public override ServerEventPacket? Parse(RagePacket packet)
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(packet.RawData);
            reader.TryReadLittleEndian(out ushort eventNameLength);
            string eventName = Encoding.ASCII.GetString(reader.UnreadSpan.Slice(0, eventNameLength - 1));
            var eventPayload = reader.UnreadSpan.Slice(eventNameLength);

            return new ServerEventPacket(packet, eventName, eventPayload.ToArray());
        }
    }

    public class RoutePacketHandler : RagePacketHandler<RoutePacket>
    {
        private static readonly uint HashNetClone = Five.HashString("netClones");
        private static readonly uint HashNetAcks = Five.HashString("netAcks");

        public override string PacketId => "msgRoute";
        public override RoutePacket? Parse(RagePacket packet)
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(packet.RawData);
            reader.TryReadLittleEndian(out ushort targetId);
            reader.TryReadLittleEndian(out ushort length);

            //if (targetId != 0xFFFF)
            //{
            //    return null;
            //}

            reader.TryReadLittleEndian(out uint routeType);

            if (routeType != HashNetClone && routeType != HashNetAcks)
            {
                return null;
            }

            string routeTypeString = "netClones";
            if (routeType == HashNetAcks)
            {
                routeTypeString = "netAcks";
            }

            var data = reader.UnreadSequence.ToArray();

            var decodedData = new byte[data.Length*255];
            Span<byte> decodedSpan = new Span<byte>(decodedData);

            int decoded = LZ4Codec.Decode(reader.UnreadSpan, decodedSpan, new ReadOnlySpan<byte>(FiveLz4Dictionary.FiveDict));

            return new RoutePacket(packet, targetId, routeType, routeTypeString, decodedSpan.Slice(0, decoded));
        }
    }

    public class RoutePacket : RagePacket
    {
        public ushort TargetId { get; }

        public uint TypeHash { get; }

        public string TypeString { get; }

        public ReadOnlySequence<byte> CloneData { get; }

        public RoutePacket(RagePacket packet, ushort targetId, uint typeHash, string typeString, Span<byte> data) : base(packet)
        {
            TargetId = targetId;
            TypeHash = typeHash;
            TypeString = typeString;
            CloneData = new ReadOnlySequence<byte>(data.ToArray());
        }
    }

    public class PackedClonePacketHandler : RagePacketHandler<PackedClonePacket>
    {
        public override string PacketId => "msgPackedClones";

        public override PackedClonePacket? Parse(RagePacket packet)
        {
            throw new NotImplementedException();
            //uint8_t bufferData[16384] = { 0 };
            //int bufferLength = LZ4_decompress_safe(reinterpret_cast <const char*> (&buffer.GetData()[buffer.GetCurOffset()]), reinterpret_cast<char*>(bufferData), buffer.GetRemainingBytes(), sizeof(bufferData));


            //while (!msgBuf.IsAtEnd() && !end)
            //{


            //  auto dataType = msgBuf.Read<uint8_t>(3);
            //switch (dataType)
            //{
            //    case 1:
            //    case 2:
            //    {
            //        msgClone clone;
            //        clone.Read(dataType, msgBuf);

            //void msgClone::Read(int syncType, rl::MessageBuffer & buffer)
            //{
            //    m_syncType = syncType;
            //    m_objectId = buffer.Read<uint16_t>(13);
            //    m_clientId = buffer.Read<uint16_t>(16);

            //    if (syncType == 1)
            //    {
            //        m_entityType = (NetObjEntityType)buffer.Read<uint8_t>(g_netObjectTypeBitLength);
            //        m_creationToken = 0;

            //        if (icgi->NetProtoVersion >= 0x202002271209)
            //        {
            //            m_creationToken = buffer.Read<uint32_t>(32);
            //        }
            //    }

            //    if (icgi->NetProtoVersion >= 0x201912301309)
            //    {
            //        m_uniqifier = buffer.Read<uint16_t>(16);
            //    }
            //    else
            //    {
            //        m_uniqifier = 0;
            //    }

            //    if (icgi->NetProtoVersion >= 0x202010191044)
            //    {
            //        m_dependentFrameIndex = (uint64_t(buffer.Read<uint32_t>(32)) << 32) | buffer.Read<uint32_t>(32);
            //    }

            //    m_timestamp = buffer.Read<uint32_t>(32);

            //    auto length = buffer.Read<uint16_t>(12);

            //    m_cloneData.resize(length);
            //    buffer.ReadBits(m_cloneData.data(), m_cloneData.size() * 8);
            //}

            //        m_clones.push_back(std::move(clone));
            //        break;
            //    }
            //    case 3: // clone remove
            //    {
            //        auto stillAlive = false;
            //        uint16_t uniqifier = 0;

            //        if (icgi->NetProtoVersion >= 0x202007120951)
            //        {
            //            stillAlive = msgBuf.ReadBit();
            //        }

            //        auto remove = msgBuf.Read<uint16_t>(13);

            //        if (icgi->NetProtoVersion >= 0x202007151853)
            //        {
            //            uniqifier = msgBuf.Read<uint16_t>(16);
            //        }

            //        m_removes.push_back({ remove, uniqifier, stillAlive });
            //        break;
            //    }
            //    case 5:
            //    {
            //        uint32_t msecLow = msgBuf.Read<uint32_t>(32);
            //        uint32_t msecHigh = msgBuf.Read<uint32_t>(32);

            //        uint64_t serverTime = ((uint64_t(msecHigh) << 32) | msecLow);
            //        //UpdateTime(serverTime);

            //        break;
            //    }
            //    case 7:
            //        end = true;
            //        break;
            //}

            //}
        }
    }

    public class PackedClonePacket : RagePacket
    {
        public PackedClonePacket(RagePacket basePacket) : base(basePacket)
        {
        }
    }
}
