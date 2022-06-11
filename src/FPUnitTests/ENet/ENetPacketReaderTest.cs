using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.ENet;
using Xunit;
using Xunit.Abstractions;

namespace FPUnitTests.ENet
{
    public class ENetPacketReaderTest
    {
        private readonly ITestOutputHelper _output;

        public ENetPacketReaderTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void DiscoveryPacketRandom1()
        {
            byte[] packet = new byte[]
            {
                0x00, 0x00, 0x07, 0x01,
                0x07, 0x3a, 0x00, 0x6c, 0x00, 0x0e, 0xb4, 0xfd, 0x8d, 0x25, 0x00, 0xb4, 0xd6,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0xe0
            };

            var reader = new ENetPacketReader(new ReadOnlySequence<byte>(packet));

            var reads = reader.ReadPackets();
            var read = Assert.Single(reads);

            Assert.False(read.Header.HasSentTime);
            Assert.Equal(0x0000, read.Header.PeerIdAndSessionId);
            Assert.Equal(0x0701, read.Header.SentTime);
            
            Assert.Equal(ENetCommand.SendUnreliable, read.CommandHeader.Command);

            ENetProtocolSendUnreliable? sendUnreliable = read.Packet as ENetProtocolSendUnreliable;
            Assert.NotNull(sendUnreliable);

            Assert.Equal(0, reader.Left);
            Assert.Equal(read.Data.Length, sendUnreliable!.DataLength);
        }

        [Fact]
        public void DiscoveryPacketEvent()
        {
            byte[] packet = new byte[]
            {
                0x80, 0x00, 0x02, 0xb8, 0x86, 0x00, 0x10, 0x0d, 0x00, 0x26, 0x18, 0x6e, 0x77, 0xfa, 0x1d, 0x00, 0x67,
                0x6c, 0x5f, 0x63, 0x61, 0x73, 0x69, 0x6e, 0x6f, 0x3a, 0x62, 0x6a, 0x3a, 0x70, 0x6c, 0x61, 0x79, 0x65,
                0x72, 0x53, 0x6b, 0x69, 0x70, 0x52, 0x6f, 0x75, 0x6e, 0x64, 0x00, 0x92, 0x01, 0x04
            };

            var reader = new ENetPacketReader(new ReadOnlySequence<byte>(packet));

            var reads = reader.ReadPackets();
            var read = Assert.Single(reads);

            Assert.True(read.Header.HasSentTime);

            Assert.Equal(ENetCommand.SendReliable, read.CommandHeader.Command);

            ENetProtocolSendReliable? sendReliable = read.Packet as ENetProtocolSendReliable;
            Assert.NotNull(sendReliable);

            Assert.Equal(0, reader.Left);
            Assert.Equal(read.Data.Length, sendReliable!.DataLength);
        }

        [ClassData(typeof(PacketDataGenerator))]
        [Theory]
        public void FuzzyPacketReader(string packetData)
        {
            byte[] packet = Convert.FromHexString(packetData);

            var reader = new ENetPacketReader(new ReadOnlySequence<byte>(packet));
            foreach (var read in reader.ReadPackets())
            {

                _output.WriteLine($">> {read.CommandHeader.Command} ");
            }
            
            Assert.Equal(0, reader.Left);
        }

        public class PacketDataGenerator : IEnumerable<object[]>
        {

            public static readonly string[] packetsData =
            {
                "8000603C86000050000A3FFAFF530000000000000701005805C4000EB4FD8D25004D08000000000010E0",
                "00000701000027c8018e5b4438e9ffff860165dcb28dd1a001c2441800009ee91e29800679caf10080ebbbada003c603aeee02420e001ca66ff000d5ee875d4007b757ba1d041ab0003ae211509ca60639d885af92ce0d801241e02100034d325008ba480084ea9df001e5137d1b90023e0cc0b84d002480a0bcdc18a01177700110045800851415105022a0000260eeabf10332337cae4008f7a4fb776c00920e006e011831b550004cd1c00695734041dc22c98787906606b9d801200704688368808a006c1003301f30f9094084c3c00efea9f00299680ff39100239eb41c2c300240708460f4ffe0b000000134ac803580027f7400786050f005948110025500c33788400794030cdd0494dc00f4d40040b9ae427987bea2dfa001801207bc01c00b9b905208341001ecc23527f836ef201f31c23500f000bcdc724eb6800f7371c93813a1c80330a0f50b85ddd080128b23edbae2003c2c8fb6e022136008001cf1005401550b0051cf51ff5235d886c9e050004a1520107c69f00b48ec90d5838011ef89f67fe801244d000403b000a938000001cb",
                "00000701000027d9012c5b4438e9ffff240165dcb28dd0a001c2679800009f2d1b4d000b8319f20335daa3645e0047c0882700e0048004110002d33b740267e5002809cfe1064558401500dc05b103000390027600006aaa90ff03099d0402ff0330ca01f839ac31a148302c165fd7a4c1c32080e80e10c480e8f108a147c1980123ffc7431e4008f7acf97f5c00923cc042043a16f02122400f041804200e982002000604141600e002560020000e2004bcb0096eb8800f72c025b8084bec01c40d6c0042c6c5b12027fc36b1201f31843500e1b7f4684eba800f5fd1a13a1176488ba6c39259a28688200473de7e8abc56784224f00790223218e0c6211fc5223200f100c04c574662003d01315d104b98c00f71a7f100b29a5eeba800f6ca697ba09d988020d4f1c0ee3339d6d001e3b8cce70382",
                "0000070100002754004c5b4438e9ffff440065dcb28df105a001c151f800009d190f14000701a78803080d09178a1013b71a8005002448614378586391709900801205f42eca11e00a6007828402dc074c1000000007",
                "000001ff00be00be115a",
                "000007010000280500235b4438e9ffff1b0065dcb28df006a001c2c35800009fdd26850008000208790041000e",
                "00000701006a1583000eb4fd8d2500e827000000000010e00701006a1584000e35c8e181035c8e000000000010e0",
                "8000133d860000cb000a3ffaff530000000000000701006a154a000eb4fd8d2500c627000000000010e00701006a154b000e35c8e18103458e000000000010e0",
                "000007010000279f007e5b4438e9ffff760065dcb28dd1a001c1ee7800009e450dd20002e0a6f10d206027120047bc681dc06004914c0ab018c002a6c801e0af014c01d3a9864171540009c53ef000637b575c4007ad8ded5c043c50001c4048408e8f0c784fe0205f20ee42f00b6c24c024007c05c040f600143e400f045805800e98200000001d",
            };
            public IEnumerator<object[]> GetEnumerator()
            {
                foreach (var packet in packetsData)
                {
                    yield return new object[] { packet };
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
