using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.ENet;
using Protocol.FiveM;
using Xunit;
using Xunit.Abstractions;

namespace FPUnitTests.FiveM
{
    public class RagePacketReaderTest
    {
        private readonly ITestOutputHelper _output;

        public RagePacketReaderTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ParseServerEvent()
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

            var reliablePacket = Assert.IsAssignableFrom<ENetProtocolSendReliable>(read.Packet);

            Assert.Equal(reliablePacket.DataLength, read.Data.Length);
            
            RagePacketReader rageReader = new RagePacketReader(new ReadOnlySequence<byte>(read.Data));
            var ragePacket = rageReader.ReadPacket();

            _output.WriteLine($">> Packet type {ragePacket.Name} of {ragePacket.RawData.Length}bytes");
        }
    }
}
