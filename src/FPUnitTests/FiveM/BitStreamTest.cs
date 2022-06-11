using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocol;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;
using Xunit;

namespace FPUnitTests.FiveM
{
    public class BitStreamTest
    {
        [Fact]
        public void DiscoveryTest()
        {
            byte[] data = new byte[]
            {
                0x12, // 0001 0010
                0x43, // 0100 0011
                0x5A  // 0101 1010 -> 010 110 10
            };

            ArrayByteStream abs = new ArrayByteStream(data);

            var bs = new BitStream<ArrayByteStream>(abs);
            Assert.False(bs.IsEnd());
            Assert.Equal(24, bs.GetRemainingBitsCount());

            byte a = bs.Read8(4);
            Assert.Equal(0b0001, a);
            Assert.False(bs.IsEnd());
            Assert.Equal(20, bs.GetRemainingBitsCount());

            byte b = bs.Read8(4);
            Assert.Equal(0b0010, b);
            Assert.False(bs.IsEnd());
            Assert.Equal(16, bs.GetRemainingBitsCount());

            byte c = bs.Read8(4);
            Assert.Equal(0b0100, c);
            Assert.False(bs.IsEnd());
            Assert.Equal(12, bs.GetRemainingBitsCount());

            byte d = bs.Read8(4);
            Assert.Equal(0b0011, d);
            Assert.False(bs.IsEnd());
            Assert.Equal(8, bs.GetRemainingBitsCount());

            byte e = bs.Read8(3);
            Assert.Equal(0b010, e);
            Assert.False(bs.IsEnd());
            Assert.Equal(5, bs.GetRemainingBitsCount());

            byte f = bs.Read8(3);
            Assert.Equal(0b110, f);
            Assert.False(bs.IsEnd());
            Assert.Equal(2, bs.GetRemainingBitsCount());

            byte g = bs.Read8(2);
            Assert.Equal(0b10, g);
            Assert.True(bs.IsEnd());
            Assert.Equal(0, bs.GetRemainingBitsCount());


            Assert.Equal((uint)0b0001, ReadRage(data, 0, 4));
            Assert.Equal((uint)0b0010, ReadRage(data, 4, 4));
            Assert.Equal((uint)0b0100, ReadRage(data, 8, 4));
            Assert.Equal((uint)0b0011, ReadRage(data, 12, 4));

            Assert.Equal((uint)0b010, ReadRage(data, 16, 3));
            Assert.Equal((uint)0b110, ReadRage(data, 19, 3));
            Assert.Equal((uint)0b10, ReadRage(data, 22, 2));
        }


        private static uint ReadRage(byte[] data, int bitOffset, int bitLength)
        {
            int length = bitLength;
            int m_curBit = bitOffset;
            int m_maxBit = 42;
            byte[] m_data = data;

            int startIdx = m_curBit / 8;
            int shift = m_curBit % 8;

            if ((m_curBit + length) > m_maxBit)
            {
                m_curBit += length;
                return 0;
            }

            uint retval = (byte)(m_data[startIdx] << shift);
            startIdx++;

            if (length > 8)
            {
                int remaining = ((length - 9) / 8) + 1;

                while (remaining > 0)
                {
                    uint thisVal = (uint)(m_data[startIdx] << shift);
                    startIdx++;

                    retval = thisVal | (retval << 8);

                    remaining--;
                }
            }

            if (shift > 0)
            {
                var leftover = (startIdx < m_data.Length) ? m_data[startIdx] : 0;
                retval |= (byte)(leftover >> (8 - shift));
            }

            retval = retval >> (((length + 7) & 0xF8) - length);

            m_curBit += length;

            return retval;
        }

    }
}
