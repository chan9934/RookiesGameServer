using ServerCore;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace SeverCore
{
    internal class Program
    {
       static  Listener _listener = new Listener();

        static void OnAcceptHandler(Socket clientSocket)
        {

            try
            {
                Byte[] recvBuff = new byte[1024];
                int recvBytes = clientSocket.Receive(recvBuff);
                string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
                Console.WriteLine($"[From Client] {recvData}");

                Byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to MMORPG Server !");
                clientSocket.Send(sendBuff);

                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        static void Main(string[] args)
        {
                string host = Dns.GetHostName();
                IPHostEntry entry = Dns.GetHostEntry(host);
                IPAddress ipAddr = entry.AddressList[0];
                IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

            _listener.Init(endPoint, OnAcceptHandler);

            Console.WriteLine("Listening...");
            while (true)
            {
            }

        }
    }
}