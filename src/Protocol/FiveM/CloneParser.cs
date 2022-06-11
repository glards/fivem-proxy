using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;

namespace Protocol.FiveM
{
    public class CloneParser
    {
        private const int ObjectTypeLengthBits = 4; // 4 for GTA / 5 for RDR
        public enum CloneDataType : byte
        {
            CloneCreate = 1,
            CloneSync = 2,
            CloneRemove = 3,
            CloneTakeover = 4,
            SetTimestamp = 5,
            SetIndex = 6,
            End = 7
        }


        public enum ObjectType
        {
            Automobile = 0,
            Bike = 1,
            Boat = 2,
            Door = 3,
            Heli = 4,
            Object = 5,
            Ped = 6,
            Pickup = 7,
            PickupPlacement = 8,
            Plane = 9,
            Submarine = 10,
            Player = 11,
            Trailer = 12,
            Train = 13,
        }

        private byte[] _data;

        public CloneParser(byte[] packet)
        {
            _data = new byte[packet.Length];
            for (int i = 0; i < packet.Length; i++)
            {
                _data[i] = packet[i];
            }
        }

        public IEnumerable<ICloneAction> Parse()
        {
            bool finished = false;
            ArrayByteStream abs = new ArrayByteStream(_data);
            var bs = new BitStream<ArrayByteStream>(abs);

            while (!finished && !bs.IsEnd())
            {
                var dataType = bs.Read8(3);
                if (dataType is 0 or > 7)
                {
                    string log = $"Aborting clone processing : remaining bits {bs.GetRemainingBitsCount()} | {bs.RemainingToBitString()}";
                    Debug.WriteLine(log);
                    throw new InvalidOperationException(log);
                }

                var cloneDataType = (CloneDataType)dataType;

                switch (cloneDataType)
                {

                    case CloneDataType.CloneCreate:
                        yield return ProcessClonePacket(ref bs, true);
                        break;

                    case CloneDataType.CloneSync:
                        yield return ProcessClonePacket(ref bs, false);
                        break;

                    case CloneDataType.CloneRemove:
                        yield return ProcessCloneRemove(ref bs);
                        break;

                    case CloneDataType.CloneTakeover:
                        yield return ProcessCloneTakeover(ref bs);
                        break;

                    case CloneDataType.SetTimestamp:
                        var newTs = bs.Read32(32);
                        yield return new SetTimestamp(newTs);
                        break;

                    case CloneDataType.SetIndex:
                        var newIndex = bs.Read32(32);
                        yield return new SetIndex(newIndex);
                        break;

                    case CloneDataType.End:
                        yield return new End();
                        finished = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private ClonePacket ProcessClonePacket(ref BitStream<ArrayByteStream> bs, bool isCreate)
        {
            var uniqifier = bs.Read16(16);
            var objectId = bs.Read16(16);

            uint creationToken = 0;
            byte objectType = Byte.MaxValue;
            if (isCreate)
            {
                creationToken = bs.Read32(32);
                objectType = bs.Read8(ObjectTypeLengthBits);
            }

            var length = bs.Read16(12);
            byte[] syncTree = new byte[length];

            int before = bs.GetRemainingBitsCount();
            bs.Read(new Span<byte>(syncTree));

            int after = bs.GetRemainingBitsCount();
            int read = before - after;

            return new ClonePacket(uniqifier, objectId, creationToken, objectType, syncTree);
        }

        private CloneRemove ProcessCloneRemove(ref BitStream<ArrayByteStream> bs)
        {
            var objectId = bs.Read16(16);
            var uniqifier = bs.Read16(16);
            return new CloneRemove(objectId, uniqifier);
        }

        private CloneTakeover ProcessCloneTakeover(ref BitStream<ArrayByteStream> bs)
        {
            var clientId = bs.Read16(16);
            var objectId = bs.Read16(16);
            return new CloneTakeover(clientId, objectId);
        }
    }

    public interface ICloneAction
    {
    }

    public record ClonePacket
        (ushort Uniqifier, ushort ObjectId, uint CreationToken, byte ObjectType, byte[] SyncData) : ICloneAction
    {
        public bool IsCreation => ObjectType != 0xFF;
    }

    public record CloneRemove(ushort ObjectId, ushort Uniqifier) : ICloneAction;

    public record CloneTakeover(ushort ClientId, ushort ObjectId) : ICloneAction;

    public record SetTimestamp(uint Timestamp) : ICloneAction;

    public record SetIndex(uint Index) : ICloneAction;

    public record End() : ICloneAction;

    public record StartTrace(Guid TraceId) : ICloneAction;
    public record StopTrace(Guid TraceId) : ICloneAction;
}
