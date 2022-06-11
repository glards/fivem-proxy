using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol.FiveM;
using Xunit;
using Xunit.Abstractions;

namespace FPUnitTests.FiveM
{
    public class RageStringHashTest
    {
        private readonly ITestOutputHelper _output;

        public RageStringHashTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [InlineData("msgEnd", 0xCA569E63)]
        [Theory]
        public void TestHashString(string text, uint expectedHash)
        {
            uint hash = Five.HashRageString(text);
            Assert.Equal(expectedHash, hash);
        }

        [Fact]
        public void DisplayKnownPackets()
        {
            foreach (var kv in Five.MapPacketHash)
            {
                _output.WriteLine($"{kv.Key}: 0x{Convert.ToHexString(BitConverter.GetBytes(kv.Value))}");
            }
        }
    }
}
