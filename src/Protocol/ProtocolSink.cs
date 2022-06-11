using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Protocol
{
    public class ProtocolSink
    {

        public Channel<byte[]> ServerBoundChannel { get; } = Channel.CreateUnbounded<byte[]>();
        public Channel<byte[]> ClientBoundChannel { get; } = Channel.CreateUnbounded<byte[]>();

        public ProtocolSink()
        {
        }
    }
}
