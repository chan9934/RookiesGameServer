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
                Session session = new Session();
                session.Start(clientSocket);

               byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to MMORPG Server !");
                session.Send(sendBuff);

                Thread.Sleep(1000);

                session.Disconnected();


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