using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public static class SequenceReaderExtensions
    {
        public static bool TryReadBigEndian(ref this SequenceReader<byte> reader, out uint val)
        {
            if (reader.TryReadBigEndian(out int v))
            {
                val = (uint)v;
                return true;
            }

            val = 0;
            return false;
        }

        public static bool TryReadBigEndian(ref this SequenceReader<byte> reader, out ushort val)
        {
            if (reader.TryReadBigEndian(out short v))
            {
                val = (ushort)v;
                return true;
            }

            val = 0;
            return false;
        }

        public static bool TryReadLittleEndian(ref this SequenceReader<byte> reader, out uint val)
        {
            if (reader.TryReadLittleEndian(out int v))
            {
                val = (uint)v;
                return true;
            }

            val = 0;
            return false;
        }


        public static bool TryReadLittleEndian(ref this SequenceReader<byte> reader, out ushort val)
        {
            if (reader.TryReadLittleEndian(out short v))
            {
                val = (ushort)v;
                return true;
            }

            val = 0;
            return false;
        }
    }
}
