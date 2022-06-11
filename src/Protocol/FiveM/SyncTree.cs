using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;

namespace Protocol.FiveM
{

    public class SyncTreeParserState
    {
        private BitStream<ArrayByteStream> _dataStream;

        public int SyncType;
        public int ObjType;
        public uint Timestamp;
        public ulong LastFrameIndex;
        public uint TargetSlotId;
        public bool IsFirstUpdate;

        public ref BitStream<ArrayByteStream> Stream => ref _dataStream;

        public SyncTreeParserState(byte[] data, bool isCreation)
        {
            SyncType = isCreation ? 1 : 2;
            _dataStream = new BitStream<ArrayByteStream>(new ArrayByteStream(data));
        }
    }

    public interface ISyncTreeVisitor
    {
        void Visit(SyncTreeParserState state, NodeIds ids, Node node, DataNode? dataNode);
    }

    public interface ISyncTreeParser
    {
        void Parse(SyncTreeParserState state, ISyncTreeVisitor? visitor);
    }


    public class SyncTree : ISyncTreeParser
    {
        public Node Root { get; }

        public SyncTree(Node root)
        {
            Root = root;
        }

        public void Parse(SyncTreeParserState state, ISyncTreeVisitor? visitor = null)
        {
            if (state.Stream.IsEnd())
            {
                return;
            }

            state.ObjType = 0;

            if (state.SyncType == 2 || state.SyncType == 4)
            {
                state.ObjType = state.Stream.ReadBit();
            }

            Root.Parse(state, visitor);
        }
    }

    public record NodeIds(int Id1, int Id2, int Id3);

    public abstract class Node : ISyncTreeParser
    {
        public NodeIds Ids { get; }

        public Node(NodeIds ids)
        {
            Ids = ids;
        }

        public abstract void Parse(SyncTreeParserState state, ISyncTreeVisitor? visitor);

        public bool ShouldRead(SyncTreeParserState state)
        {

            int mask1 = (Ids.Id1 & state.SyncType);
            int mask2 = (Ids.Id2 & state.SyncType);
            int mask3 = (Ids.Id3 & state.ObjType);

            if ((Ids.Id1 & state.SyncType) == 0)
            {
                return false;
            }

            if ((Ids.Id3 != 0) && (Ids.Id3 & state.ObjType) == 0)
            {
                return false;
            }

            if ((Ids.Id2 & state.SyncType) != 0)
            {
                var read = state.Stream.ReadBit();
                if (read == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class ParentNode : Node
    {
        public Node[] Children { get; }

        public ParentNode(NodeIds ids, params Node[] children) : base(ids)
        {
            Children = children;
        }

        public override void Parse(SyncTreeParserState state, ISyncTreeVisitor? visitor)
        {
            if (ShouldRead(state))
            {
                visitor?.Visit(state, Ids, this, null);
                foreach (var node in Children)
                {
                    node.Parse(state, visitor);
                }
            }
        }
    }
    
    public class NodeWrapper : Node
    {
        public DataNode Data { get; }
        
        public int Length { get; }

        public NodeWrapper(NodeIds ids, DataNode data, int length) : base(ids)
        {
            Data = data;
            Length = length;
        }

        public override void Parse(SyncTreeParserState state, ISyncTreeVisitor? visitor)
        {
            if (ShouldRead(state))
            {
                var length = state.Stream.Read16(16);
                var nodeData =  state.Stream.ReadArray(length);
                var dataNode = Data.Create(nodeData);
                visitor?.Visit(state, Ids, this, dataNode);
            }
        }
    }

    public abstract class DataNode
    {
        public byte[] Data { get; set; } = Array.Empty<byte>();

        public DataNode? Create(byte[] data)
        {
            var node = this.MemberwiseClone() as DataNode;
            if (node == null)
            {
                return null;
            }
            node.Data = data;
            return node;
        }

        public abstract bool Parse();
        public abstract bool Unparse();
    }
}
