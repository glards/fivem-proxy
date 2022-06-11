using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO.Pipelines;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FPMain
{
    internal class Proxy
    {
        private const int TcpBufferSize = 4 * 1024;

        private static Counter<long> UdpPacketReceived = FiveMProxy.Meter.CreateCounter<long>("UdpPacketReceived");
        private static Counter<long> UdpPacketSent = FiveMProxy.Meter.CreateCounter<long>("UdpPacketSent");

        private static Counter<long> TcpPacketReceived = FiveMProxy.Meter.CreateCounter<long>("TcpPacketReceived");
        private static Counter<long> TcpPacketSent = FiveMProxy.Meter.CreateCounter<long>("TcpPacketSent");

        private static Counter<long> TcpStreamActive = FiveMProxy.Meter.CreateCounter<long>("TcpStreamActive");

        private readonly short _port;
        private readonly short _destPort;

        public Proxy(short proxyPort, short destPort)
        {
            _port = proxyPort;
            _destPort = destPort;
        }

        public async Task ProxyTcp(CancellationToken ct = default)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, _port);

            //listener.

            while (!ct.IsCancellationRequested)
            {
                var socket = await listener.AcceptSocketAsync(ct);
                _ = ProxyTcp(socket);
            }
        }

        private static async Task ProxyTcpOutbound(PipeReader reader, Socket dest, CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var result = await reader.ReadAsync(ct);

                    foreach (var segment in result.Buffer)
                    {
                        await dest.SendAsync(segment, SocketFlags.None, ct);
                    }

                    reader.AdvanceTo(result.Buffer.End);
                    if (result.IsCompleted)
                    {
                        break;
                    }
                }
            }
            catch (InvalidOperationException)
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERR: ProxyTcp: {ex}");
            }
            finally
            {
                await reader.CompleteAsync();
            }
        }

        private static async Task ProxyTcpInbound(Socket src, PipeWriter writer, CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var memory = writer.GetMemory(TcpBufferSize);
                    int read = await src.ReceiveAsync(memory, SocketFlags.None, ct);

                    if (read == 0)
                    {
                        return;
                    }

                    writer.Advance(read);

                    var result = await writer.FlushAsync(ct);
                    if (result.IsCompleted)
                    {
                        break;
                    }
                }
            }
            catch (InvalidOperationException)
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERR: ProxyTcp: {ex}");
            }
            finally
            {
                await writer.CompleteAsync();
            }
        }

        private static async Task ProxyTcp(Socket socket)
        {
            Pipe pipe = new Pipe();
            try
            {
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERR: ProxyTcp: {ex}");
            }

        }

        public async Task ProxyUdp(CancellationToken ct = default)
        {
            //var cts = CancellationTokenSource.CreateLinkedTokenSource(_proxyCts.Token, ct);

            UdpClient source = new UdpClient(_port);

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, _destPort);
            UdpClient dest = new UdpClient(endpoint);
            
            await Task.WhenAll(
                ProxyUdp(source, dest, ct, UdpPacketReceived),
                ProxyUdp(dest, source, ct, UdpPacketSent)
            );
        }

        private static async Task ProxyUdp(UdpClient from, UdpClient to, CancellationToken ct, Counter<long> counter)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var result = await from.ReceiveAsync(ct);
                    counter.Add(1);
                    await to.SendAsync(result.Buffer, ct);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERR: ProxyUdp: {ex}");
            }
        }
    }
}
