using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Protocol.FiveM;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using Xunit;
using Xunit.Abstractions;

namespace FPUnitTests.FiveM
{
    public class SyncTreeTest
    {
        private readonly ITestOutputHelper _output;

        public SyncTreeTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(Skip = "Reverse engineering test")]
        public void FindFieldsPacket1()
        {
            var syncTree = Convert.FromHexString("00290D7114C9007000D07FC0011A000B08026800344E324C628000202000818062943FE00C00000000000001FFFF00360000A1FCFC0000019082005560000000004008637705C9F020C0D7114C90000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B8C0567300000000080080000400149800F80426C0080298922969B2DF01000000048005001000A00200140040028008005001000A0020014004006A008018801380000029B80");

            var state = new SyncTreeParserState(syncTree, true);
            var bitCount = state.Stream.GetBitsCount();

            _output.WriteLine($"Full synctree ({bitCount}bits) : {state.Stream.GetBitString()}");

            for (int i = 0; i < bitCount - 16; i++)
            {
                state.Stream.BitIndex = i;
                var length = (int)state.Stream.Read16(16);
                int remaining = state.Stream.GetRemainingBitsCount();

                if (length != 0 && length <= remaining)
                {
                    _output.WriteLine($"Found possible field at {i} with content at {state.Stream.BitIndex} of length {length} until {state.Stream.BitIndex + length}");
                }
            }
        }


        [Fact(Skip = "Reverse engineering test")]
        public void FindFieldsPacket2()
        {
            var syncTree = Convert.FromHexString("003B2347A07B9C400C9E001A19F8002240016100080C8000A000560035034B2BC00F7C8336CBE3994A0D00247002004008012000000000400780000000");

            var state = new SyncTreeParserState(syncTree, true);
            var bitCount = state.Stream.GetBitsCount();

            _output.WriteLine($"Full synctree ({bitCount}bits) : {state.Stream.GetBitString()}");

            for (int i = 0; i < bitCount - 16; i++)
            {
                state.Stream.BitIndex = i;
                var length = (int)state.Stream.Read16(16);
                int remaining = state.Stream.GetRemainingBitsCount();

                if (length != 0 && length <= remaining)
                {
                    _output.WriteLine($"Found possible field at {i} with content at {state.Stream.BitIndex} of length {length} until {state.Stream.BitIndex + length}");
                }
            }
        }

        [Fact]
        public void PlayerCreationSyncTreeParse()
        {
            var syncTree = Convert.FromHexString("00290D7114C9007000D07FC0011A000B08026800344E324C628000202000818062943FE00C00000000000001FFFF00360000A1FCFC0000019082005560000000004008637705C9F020C0D7114C90000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B8C0567300000000080080000400149800F80426C0080298922969B2DF01000000048005001000A00200140040028008005001000A0020014004006A008018801380000029B80");
            
            var state = new SyncTreeParserState(syncTree, true);
            var length = state.Stream.GetBitsCount();
            string fullBits = state.Stream.GetBitString();
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
            //Assert.True(state.Stream.IsEnd(), $"Data left after parsing {state.Stream.GetRemainingBitsCount()}bits out of {length}bits : {state.Stream.RemainingToBitString()}");
        }

        [Fact]
        public void ParseAnimPacket1()
        {
            var syncTree = Convert.FromHexString("F1004D040689C6498C500004028070943FE010096064C00080000000001FFFF00360000A1FCFC00000190800180080000400141800F80487B00801B85A000000000008005001000A00200140040028008005001000A0020014002004E7E83B7EA4000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        [Fact]
        public void ParseAnimPacket2()
        {
            var syncTree = Convert.FromHexString("F1004D040689C6498C500004028070943FE010096064C00080000000001FFFF00360000A1FCFC0000019080018008000040014180215A99E97308090F60100370B400000000001000A00200140040028008005001000A002001400400280000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        [Fact]
        public void ParseAnimPacket3()
        {
            var syncTree = Convert.FromHexString("F1004D040689C6498C500004028070943FE010096064C00080000000001FFFF00360000A1FCFC00000190800180080080C0014980215A99E97308090F60100370B400000000001000A00200140040028008005001000A002001400400280000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        [Fact]
        public void ParseAnimPacket4()
        {
            var syncTree = Convert.FromHexString("F1004D040689C6498C500004028070943FE010096064C00080000000001FFFF00360000A1FCFC0000019080018008010140014980215A99E97308090F60100370B400000000001000A00200140040028008005001000A002001400400280000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        [Fact]
        public void ParseAnimPacket5()
        {
            var syncTree = Convert.FromHexString("F1004D040689C6498C500004028070943FE010096064C00080000000001FFFF00360000A1FCFC0000019080018008010140014980215A99E97308090F60100370B400000000001000A00200140040028008005001000A00200140040028004009CFD0771D4800000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        [Fact]
        public void ParseAnimPacket6()
        {
            var syncTree = Convert.FromHexString("B001003038002930042B533D2E610121EC02006E1680000000000200140040028008005001000A0020014004002800800500080139FA0EE3A9000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        [Fact]
        public void ParseAfterAnimPacket1()
        {
            var syncTree = Convert.FromHexString("B00100404100273F41DC7520000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        [Fact]
        public void ParseAfterAnimPacket2()
        {
            var syncTree = Convert.FromHexString("B001005050000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        [Fact]
        public void ParseAfterAnimPacket3()
        {
            var syncTree = Convert.FromHexString("B001006060000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        [Fact]
        public void ParseBeforeAnimPacket1()
        {
            var syncTree = Convert.FromHexString("B0010840C100273F41DBF521001C02285C000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        [Fact]
        public void ParseBeforeAnimPacket2()
        {
            var syncTree = Convert.FromHexString("B0010880C80029080139F9CEDFA90800E01142E0000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        // A100273DF1E37519001C026880000000
        [Fact]
        public void ParseDuringAnimPacket1()
        {
            var syncTree = Convert.FromHexString("A100273DF1E37519001C026880000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        // A100273DD1E27509001C026886000000
        [Fact]
        public void ParseDuringAnimPacket2()
        {
            var syncTree = Convert.FromHexString("A100273DD1E27509001C026886000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        // A80029080139ED8F07A70800E0130480000000
        [Fact]
        public void ParseDuringAnimPacket3()
        {
            var syncTree = Convert.FromHexString("A80029080139ED8F07A70800E0130480000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        // A0800E0159ED000000
        [Fact]
        public void ParseDuringAnimPacket4()
        {
            var syncTree = Convert.FromHexString("A0800E0159ED000000");
            var state = new SyncTreeParserState(syncTree, false);
            SyncTreeFive.CPlayerSyncTree.Parse(state, new TestSyncTreeOutputVisitor(_output));
            Assert.Equal(0, state.Stream.RemainingBitSum());
        }

        [Fact]
        public void HashAnims()
        {
            string animDict = "anim_casino_a@amb@casino@games@slots@male";
            string anim1 = "enter_left";
            string anim2 = "enter_right";


            _output.WriteLine($"{animDict} -> hash1:{Five.HashRageString(animDict)} / {Convert.ToHexString(BitConverter.GetBytes(Five.HashRageString(animDict)))} / {BitConverter.GetBytes(Five.HashRageString(animDict)).ToBitString()}");
            _output.WriteLine($"{animDict} -> hash2:{Five.HashRageString(animDict.ToUpperInvariant())} / {Convert.ToHexString(BitConverter.GetBytes(Five.HashRageString(animDict.ToUpperInvariant())))} / {BitConverter.GetBytes(Five.HashRageString(animDict.ToUpperInvariant())).ToBitString()}");
            _output.WriteLine($"{animDict} -> hash3:{Five.HashString(animDict)} / {Convert.ToHexString(BitConverter.GetBytes(Five.HashString(animDict)))} / {BitConverter.GetBytes(Five.HashString(animDict)).ToBitString()}");
            _output.WriteLine("");

            _output.WriteLine($"{anim1} -> hash1:{Five.HashRageString(anim1)} / {Convert.ToHexString(BitConverter.GetBytes(Five.HashRageString(anim1)))} / {BitConverter.GetBytes(Five.HashRageString(anim1)).ToBitString()}");
            _output.WriteLine($"{anim1} -> hash2:{Five.HashRageString(anim1.ToUpperInvariant())} / {Convert.ToHexString(BitConverter.GetBytes(Five.HashRageString(anim1.ToUpperInvariant())))} / {BitConverter.GetBytes(Five.HashRageString(anim1.ToUpperInvariant())).ToBitString()}");
            _output.WriteLine($"{anim1} -> hash3:{Five.HashString(anim1)} / {Convert.ToHexString(BitConverter.GetBytes(Five.HashString(anim1)))} / {BitConverter.GetBytes(Five.HashString(anim1)).ToBitString()}");
            _output.WriteLine("");

            _output.WriteLine($"{anim2} -> hash1:{Five.HashRageString(anim2)} / {Convert.ToHexString(BitConverter.GetBytes(Five.HashRageString(anim2)))} / {BitConverter.GetBytes(Five.HashRageString(anim2)).ToBitString()}");
            _output.WriteLine($"{anim2} -> hash2:{Five.HashRageString(anim2.ToUpperInvariant())} / {Convert.ToHexString(BitConverter.GetBytes(Five.HashRageString(anim2.ToUpperInvariant())))} / {BitConverter.GetBytes(Five.HashRageString(anim2.ToUpperInvariant())).ToBitString()}");
            _output.WriteLine($"{anim2} -> hash3:{Five.HashString(anim2)} / {Convert.ToHexString(BitConverter.GetBytes(Five.HashString(anim2)))} / {BitConverter.GetBytes(Five.HashString(anim2)).ToBitString()}");
            _output.WriteLine("");
        }


        public class TestSyncTreeOutputVisitor : ISyncTreeVisitor
        {
            private readonly ITestOutputHelper _output;
            public TestSyncTreeOutputVisitor(ITestOutputHelper output)
            {
                _output = output;
            }

            public void Visit(SyncTreeParserState state, NodeIds ids, Node node, DataNode? dataNode)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Visiting ");

                if (node is ParentNode parentNode)
                {
                    sb.Append($"parent node with Id {parentNode.Ids}");
                } else if (node is NodeWrapper nodeWrapper)
                {

                    sb.Append($"node of type {nodeWrapper.Data.GetType()} with Id {nodeWrapper.Ids}");

                    if (dataNode != null)
                    {
                        BitStream<ArrayByteStream> bs =
                            new BitStream<ArrayByteStream>(new ArrayByteStream(dataNode.Data));
                        sb.Append($" with data {Convert.ToHexString(dataNode.Data)} and bits {bs.GetBitString()}");
                    }
                }

                _output.WriteLine(sb.ToString());
            }
        }
    }
}
