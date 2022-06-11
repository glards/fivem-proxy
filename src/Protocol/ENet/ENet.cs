using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.ENet
{

    public record ENetProtocolHeader(
        ushort PeerIdAndSessionId,
        ushort SentTime
    )
    {
        public const int Size = 2 + 2;

        public ushort PeerId => (ushort)(PeerIdAndSessionId & ENetConstants.HeaderPeerMask);
        public ushort Flags => (ushort)(PeerIdAndSessionId & ENetConstants.HeaderFlagMask);
        public ushort SessionId => (ushort)((PeerIdAndSessionId & ENetConstants.HeaderSessionMask) >> ENetConstants.HeaderSessionShift);

        public bool HasSentTime => (Flags & ENetConstants.HeaderFlagSentTime) != 0;
        public bool IsCompressed => (Flags & ENetConstants.HeaderFlagCompressed) != 0;

        //peerID = ENET_NET_TO_HOST_16(header->peerID);
        //sessionID = (peerID & ENET_PROTOCOL_HEADER_SESSION_MASK) >> ENET_PROTOCOL_HEADER_SESSION_SHIFT;
        //flags = peerID & ENET_PROTOCOL_HEADER_FLAG_MASK;
        //peerID &= ~(ENET_PROTOCOL_HEADER_FLAG_MASK | ENET_PROTOCOL_HEADER_SESSION_MASK);
    }

    public record ENetProtocolCommandHeader(
        byte CommandRaw,
        byte ChannelId,
        ushort ReliableSequenceNumber
    )
    {
        public const int Size = 1 + 1 + 2;

        public ENetCommand Command => (CommandRaw <= (byte)ENetCommand.SendUnreliableFragment) ? (ENetCommand)CommandRaw : throw new ArgumentOutOfRangeException(nameof(CommandRaw));
    }

    public record ENetProtocolNone()
    {
        public static readonly ENetProtocolNone Instance = new ENetProtocolNone();
    }

    public record ENetProtocolAcknowledge(
        ushort ReceivedReliableSequenceNumber,
        ushort ReceivedSentTime
    )
    {
        public const int Size = 2 + 2;
    }

    public record ENetProtocolConnect(
        ushort OutgoingPeerId,
        byte IncomingSessionId,
        byte OutgoingSessionId,
        uint Mtu,
        uint WindowSize,
        uint ChannelCount,
        uint IncomingBandwidth,
        uint OutgoingBandwidth,
        uint PacketThrottleInterval,
        uint PacketThrottleAcceleration,
        uint PacketThrottleDeceleration,
        uint ConnectId,
        uint Data
    )
    {
        public const int Size = 2 + 1 + 1 + 4 + 4 + 4 + 4 + 4 + 4 + 4 + 4 + 4 + 4;
    }

    public record ENetProtocolVerifyConnect(
        ushort OutgoingPeerId,
        byte IncomingSessionId,
        byte OutgoingSessionId,
        uint Mtu,
        uint WindowSize,
        uint ChannelCount,
        uint IncomingBandwidth,
        uint OutgoingBandwidth,
        uint PacketThrottleInterval,
        uint PacketThrottleAcceleration,
        uint PacketThrottleDeceleration,
        uint ConnectId

    )
    {
        public const int Size = 2 + 1 + 1 + 4 + 4 + 4 + 4 + 4 + 4 + 4 + 4 + 4;
    }

    public record ENetProtocolDisconnect(
        uint Data
    )
    {
        public const int Size = 4;
    }

    public record ENetProtocolPing()
    {
        public const int Size = 0;
    }

    public record ENetProtocolSendReliable(
        ushort DataLength
    )
    {
        public const int Size = 2;
    }

    public record ENetProtocolSendUnreliable(
        ushort UnreliableSequenceNumber,
        ushort DataLength
    )
    {
        public const int Size = 2 + 2;
    }

    public record ENetProtocolSendUnsequenced(
        ushort UnsequencedGroup,
        ushort DataLength
    )
    {
        public const int Size = 2 + 2;
    }

    public record ENetProtocolSendFragment(
        ushort StartSequenceNumber,
        ushort DataLength,
        uint FragmentCount,
        uint FragmentNumber,
        uint TotalLength,
        uint FragmentOffset
    )
    {
        public const int Size = 2 + 2 + 4 + 4 + 4 + 4;
    }

    public record ENetProtocolBandwidthLimit(
        uint incomingBandwidth,
        uint outgoingBandwidth
    )
    {
        public const int Size = 4 + 4;
    }

    public record ENetProtocolThrottleConfigure(
        uint packetThrottleInterval,
        uint packetThrottleAcceleration,
        uint packetThrottleDeceleration
    )
    {
        public const int Size = 4 + 4 + 4;
    }

    public enum ENetCommand
    {
        None = 0,
        Acknowledge = 1,
        Connect = 2,
        VerifyConnect = 3,
        Disconnect = 4,
        Ping = 5,
        SendReliable = 6,
        SendUnreliable = 7,
        SendFragment = 8,
        SendUnsequenced = 9,
        BandwidthLimit = 10,
        ThrottleConfigure = 11,
        SendUnreliableFragment = 12,
    }

    public static class ENetConstants
    {
        public const ushort HeaderFlagMask = 0xC000;
        public const ushort HeaderSessionMask = 0x3000;
        public const int HeaderSessionShift = 12;
        public const ushort HeaderPeerMask = 0xFFFF & ~(HeaderFlagMask | HeaderSessionMask);

        public const ushort HeaderFlagCompressed = 1 << 14;
        public const ushort HeaderFlagSentTime = 1 << 15;

        public const ushort CommandMask = 0x000F;
    }

    public record ENetPacket(ENetProtocolHeader Header, ENetProtocolCommandHeader CommandHeader, object Packet, byte[] Data);

    public class ENetPacketReader
    {
        private readonly ReadOnlySequence<byte> _packet;
        private int _offset;

        public int Offset => _offset;

        public int Left => (int)(_packet.Length - Offset);

        public int Length => (int)_packet.Length;

        public ENetPacketReader(ReadOnlySequence<byte> packet)
        {
            _packet = packet;
            _offset = 0;
        }

        private void EnsureSize(int size)
        {
            if (Left < size)
            {
                throw new InvalidOperationException($"No bytes left to read. Length '{Length}' / Offset '{Offset}' / Left '{Left}'");
            }
        }
        
        private ENetProtocolHeader ReadHeader()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_packet.Slice(_offset));
            EnsureSize(ENetProtocolHeader.Size);
            reader.TryReadBigEndian(out short peerId);
            reader.TryReadBigEndian(out short sentTime);
            var header = new ENetProtocolHeader((ushort)peerId, (ushort)sentTime);
            _offset += 2;
            if (header.HasSentTime)
            {
                _offset += 2;
            }
            return header;
        }

        private ENetProtocolCommandHeader ReadCommandHeader()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_packet.Slice(_offset));
            EnsureSize(ENetProtocolCommandHeader.Size);
            reader.TryRead(out byte command);
            reader.TryRead(out byte channelId);
            reader.TryReadBigEndian(out short reliableSequenceNumber);
            _offset += ENetProtocolCommandHeader.Size;
            return new ENetProtocolCommandHeader((byte)(command & ENetConstants.CommandMask), channelId, (ushort)reliableSequenceNumber);
        }

        private ENetProtocolAcknowledge ReadAcknowledge()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_packet.Slice(_offset));
            EnsureSize(ENetProtocolAcknowledge.Size);
            reader.TryReadBigEndian(out short receivedReliableSequenceNumber);
            reader.TryReadBigEndian(out short receivedSentTime);
            _offset += ENetProtocolAcknowledge.Size;
            return new ENetProtocolAcknowledge((ushort) receivedReliableSequenceNumber, (ushort) receivedSentTime);
        }

        private ENetProtocolConnect ReadConnect()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_packet.Slice(_offset));
            EnsureSize(ENetProtocolConnect.Size);
            reader.TryReadBigEndian(out short outgoingPeerId);
            reader.TryRead(out var incomingSessionId);
            reader.TryRead(out var outgoingSessionId);
            reader.TryReadBigEndian(out int mtu);
            reader.TryReadBigEndian(out int windowSize);
            reader.TryReadBigEndian(out int channelCount);
            reader.TryReadBigEndian(out int incomingBandwidth);
            reader.TryReadBigEndian(out int outgoingBandwidth);
            reader.TryReadBigEndian(out int packetThrottleInterval);
            reader.TryReadBigEndian(out int packetThrottleAcceleration);
            reader.TryReadBigEndian(out int packetThrottleDeceleration);
            reader.TryReadBigEndian(out int connectId);
            reader.TryReadBigEndian(out int data);
            _offset += ENetProtocolConnect.Size;
            return new ENetProtocolConnect((ushort) outgoingPeerId, incomingSessionId, outgoingSessionId, (uint) mtu,
                (uint) windowSize, (uint) channelCount, (uint) incomingBandwidth, (uint) outgoingBandwidth,
                (uint) packetThrottleInterval, (uint) packetThrottleAcceleration, (uint) packetThrottleDeceleration,
                (uint) connectId, (uint) data);
        }

        private ENetProtocolVerifyConnect ReadVerifyConnect()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_packet.Slice(_offset));
            EnsureSize(ENetProtocolVerifyConnect.Size);
            reader.TryReadBigEndian(out short outgoingPeerId);
            reader.TryRead(out var incomingSessionId);
            reader.TryRead(out var outgoingSessionId);
            reader.TryReadBigEndian(out int mtu);
            reader.TryReadBigEndian(out int windowSize);
            reader.TryReadBigEndian(out int channelCount);
            reader.TryReadBigEndian(out int incomingBandwidth);
            reader.TryReadBigEndian(out int outgoingBandwidth);
            reader.TryReadBigEndian(out int packetThrottleInterval);
            reader.TryReadBigEndian(out int packetThrottleAcceleration);
            reader.TryReadBigEndian(out int packetThrottleDeceleration);
            reader.TryReadBigEndian(out int connectId);
            _offset += ENetProtocolVerifyConnect.Size;
            return new ENetProtocolVerifyConnect((ushort)outgoingPeerId, incomingSessionId, outgoingSessionId, (uint)mtu,
                (uint)windowSize, (uint)channelCount, (uint)incomingBandwidth, (uint)outgoingBandwidth,
                (uint)packetThrottleInterval, (uint)packetThrottleAcceleration, (uint)packetThrottleDeceleration,
                (uint)connectId);
        }

        private ENetProtocolDisconnect ReadDisconnect()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_packet.Slice(_offset));
            EnsureSize(ENetProtocolDisconnect.Size);
            reader.TryReadBigEndian(out int data);
            _offset += ENetProtocolDisconnect.Size;
            return new ENetProtocolDisconnect((uint) data);
        }

        private ENetProtocolPing ReadPing()
        {
            return new ENetProtocolPing();
        }

        private ENetProtocolSendReliable ReadSendReliable()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_packet.Slice(_offset));
            EnsureSize(ENetProtocolSendReliable.Size);
            reader.TryReadBigEndian(out short dataLength);
            _offset += ENetProtocolSendReliable.Size;
            return new ENetProtocolSendReliable((ushort) dataLength);
        }

        private ENetProtocolSendUnreliable ReadSendUnreliable()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_packet.Slice(_offset));
            EnsureSize(ENetProtocolSendUnreliable.Size);
            reader.TryReadBigEndian(out short unreliableSequenceNumber);
            reader.TryReadBigEndian(out short dataLength);
            _offset += ENetProtocolSendUnreliable.Size;
            return new ENetProtocolSendUnreliable((ushort) unreliableSequenceNumber, (ushort) dataLength);
        }

        private ENetProtocolSendFragment ReadSendFragment()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_packet.Slice(_offset));
            EnsureSize(ENetProtocolSendFragment.Size);
            reader.TryReadBigEndian(out short startSequenceNumber);
            reader.TryReadBigEndian(out short dataLength);
            reader.TryReadBigEndian(out int fragmentCount);
            reader.TryReadBigEndian(out int fragmentNumber);
            reader.TryReadBigEndian(out int totalLength);
            reader.TryReadBigEndian(out int fragmentOffset);
            _offset += ENetProtocolSendFragment.Size;
            return new ENetProtocolSendFragment((ushort) startSequenceNumber, (ushort) dataLength, (uint) fragmentCount,
                (uint) fragmentNumber, (uint) totalLength, (uint) fragmentOffset);
        }

        private ENetProtocolSendUnsequenced ReadSendUnsequenced()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_packet.Slice(_offset));
            EnsureSize(ENetProtocolSendUnsequenced.Size);
            reader.TryReadBigEndian(out short unsequencedGroup);
            reader.TryReadBigEndian(out short dataLength);
            _offset += ENetProtocolSendUnsequenced.Size;
            return new ENetProtocolSendUnsequenced((ushort) unsequencedGroup, (ushort) dataLength);
        }

        private ENetProtocolBandwidthLimit ReadBandwidthLimit()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_packet.Slice(_offset));
            EnsureSize(ENetProtocolBandwidthLimit.Size);
            reader.TryReadBigEndian(out int incomingBandwidth);
            reader.TryReadBigEndian(out int outgoingBandwidth);
            _offset += ENetProtocolBandwidthLimit.Size;
            return new ENetProtocolBandwidthLimit((uint)incomingBandwidth, (uint)outgoingBandwidth);
        }

        private ENetProtocolThrottleConfigure ReadThrottleConfigure()
        {
            SequenceReader<byte> reader = new SequenceReader<byte>(_packet.Slice(_offset));
            EnsureSize(ENetProtocolThrottleConfigure.Size);
            reader.TryReadBigEndian(out int packetThrottleInterval);
            reader.TryReadBigEndian(out int packetThrottleAcceleration);
            reader.TryReadBigEndian(out int packetThrottleDeceleration);
            _offset += ENetProtocolThrottleConfigure.Size;
            return new ENetProtocolThrottleConfigure((uint) packetThrottleInterval, (uint) packetThrottleAcceleration,
                (uint) packetThrottleDeceleration);
        }

        private object ReadCommand(ENetProtocolCommandHeader commandHeader)
        {
            return commandHeader.Command switch
            {
                ENetCommand.None => ENetProtocolNone.Instance,
                ENetCommand.Acknowledge => ReadAcknowledge(),
                ENetCommand.Connect => ReadConnect(),
                ENetCommand.VerifyConnect => ReadVerifyConnect(),
                ENetCommand.Disconnect => ReadDisconnect(),
                ENetCommand.Ping => ReadPing(),
                ENetCommand.SendReliable => ReadSendReliable(),
                ENetCommand.SendUnreliable => ReadSendUnreliable(),
                ENetCommand.SendFragment => ReadSendFragment(),
                ENetCommand.SendUnsequenced => ReadSendUnsequenced(),
                ENetCommand.SendUnreliableFragment => ReadSendFragment(),
                ENetCommand.BandwidthLimit => ReadBandwidthLimit(),
                ENetCommand.ThrottleConfigure => ReadThrottleConfigure(),
                _ => throw new NotImplementedException()
            };
        }

        private byte[] ReadData(ushort dataLength)
        {
            EnsureSize(dataLength);
            var data = _packet.Slice(_offset, dataLength).ToArray();
            _offset += dataLength;
            return data;
        }

        public IEnumerable<ENetPacket> ReadPackets()
        {
            var header = ReadHeader();
            while (Left > 0)
            {
                var commandHeader = ReadCommandHeader();
                var command = ReadCommand(commandHeader);
                byte[] data = command switch
                {
                    ENetProtocolSendUnreliable unreliablePacket => ReadData(unreliablePacket.DataLength),
                    ENetProtocolSendReliable reliablePacket => ReadData(reliablePacket.DataLength),
                    ENetProtocolSendUnsequenced unsequencedPacket => ReadData(unsequencedPacket.DataLength),
                    ENetProtocolSendFragment fragmentPacket => ReadData(fragmentPacket.DataLength),
                    _ => Array.Empty<byte>()
                };

                yield return new ENetPacket(header, commandHeader, command, data);
            }
        }
    }
}
