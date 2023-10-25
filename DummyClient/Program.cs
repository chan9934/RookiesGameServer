using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;

namespace DummyClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAdr = ipHost.AddressList[0];
            IPEndPoint ipEnd = new IPEndPoint(ipAdr, 7777);

            while (true)
            {

                Socket socket = new Socket(ipEnd.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socket.Connect(ipEnd);
                    Console.WriteLine($"Connect To {socket.RemoteEndPoint.ToString}");

                    byte[] sendBuff = Encoding.UTF8.GetBytes("Hello World!");
                    int sendData = socket.Send(sendBuff);

                    byte[] recvBuff = new byte[1024];
                    int recvBytes = socket.Receive(recvBuff);
                    string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
                    Console.WriteLine($"[FromServer] {recvData}");

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString);
                }
                Thread.Sleep(100);
            }
        }
    }
}