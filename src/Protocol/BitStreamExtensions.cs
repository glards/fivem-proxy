using System.Text;
using Sewer56.BitStream;
using Sewer56.BitStream.ByteStreams;

namespace Protocol;

public static class BitStreamExtensions
{
    public static byte[] ReadArray(this ref BitStream<ArrayByteStream> bs, int bitCount)
    {
        int size = bitCount / 8 + 1;
        byte[] result = new byte[size];

        int index = 0;
        while (bitCount > 8)
        {
            result[index++] = bs.Read8(8);
            bitCount -= 8;
        }

        if (bitCount > 0)
        {
            result[index++] = bs.Read8(bitCount);
        }

        return result;
    }
    public static bool IsEnd(this ref BitStream<ArrayByteStream> bs)
    {
        int length = bs.Stream.Array.Length;

        return bs.ByteOffset == length;
    }

    public static int GetBitsCount(this ref BitStream<ArrayByteStream> bs)
    {
        int length = bs.Stream.Array.Length;
        return length * 8;
    }

    public static int GetRemainingBitsCount(this ref BitStream<ArrayByteStream> bs)
    {
        int length = bs.Stream.Array.Length;
        return (length - bs.ByteOffset) * 8 - bs.BitOffset;
    }

    public static string GetBitString(this ref BitStream<ArrayByteStream> bs)
    {
        var index = bs.BitIndex;
        StringBuilder sb = new StringBuilder();
        var newBs = new BitStream<ArrayByteStream>(bs.Stream);
        for (int i = 0; i < newBs.GetBitsCount(); i++)
        {
            sb.Append(newBs.ReadBit() == 0 ? '0' : '1');
        }

        bs.BitIndex = index;
        return sb.ToString();
    }

    public static string RemainingToBitString(this ref BitStream<ArrayByteStream> bs)
    {
        var index = bs.BitIndex;
        StringBuilder sb = new StringBuilder();
        int remainingBits = bs.GetRemainingBitsCount();
        for (int i = 0; i < remainingBits; i++)
        {
            sb.Append(bs.ReadBit() == 0 ? '0' : '1');
        }
        bs.BitIndex = index;

        return sb.ToString();
    }

    public static int RemainingBitSum(this ref BitStream<ArrayByteStream> bs)
    {
        int sum = 0;
        int remainingBits = bs.GetRemainingBitsCount();
        for (int i = 0; i < remainingBits; i++)
        {
            sum += bs.ReadBit();
        }
        return sum;
    }


    public static string ToBitString(this byte[] data)
    {
        BitStream<ArrayByteStream> bs = new BitStream<ArrayByteStream>(new ArrayByteStream(data));
        return bs.GetBitString();
    }
}