using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace SeverCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
                string host = Dns.GetHostName();
                IPHostEntry entry = Dns.GetHostEntry(host);
                IPAddress ipAddr = entry.AddressList[0];
                IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

                Socket listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listenSocket.Bind(endPoint);
                listenSocket.Listen(10);

                while (true)
                {
                    Console.WriteLine("Listening...");

                    Socket clientSocket = listenSocket.Accept();

                    Byte[] recvBuff = new byte[1024];
                    int recvBytes = clientSocket.Receive(recvBuff);
                    string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
                    Console.WriteLine($"[From Client] {recvData}");

                    Byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to MMORPG Server !");
                    clientSocket.Send(sendBuff);

                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}