using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    internal class Session
    {
        Socket _socket;
        int _disConnected = 0;

        public void Start(Socket socket)
        {
            _socket = socket;

            SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
            recvArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnRecvCompleted);

            recvArgs.SetBuffer(new byte[1024], 0, 1024);

            RegisterRecv(recvArgs);
        }

        void RegisterRecv(SocketAsyncEventArgs args)
        {
            bool pending = _socket.ReceiveAsync(args);
            if (pending == false)
            {
                OnRecvCompleted(null, args);
            }

        }
        public void Send(byte[] snedbuffer)
        {
            _socket.Send(snedbuffer);
        }
        public void Disconnected()
        {
            if (Interlocked.Exchange(ref _disConnected, 1) == 1)
                return;
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }
        void OnRecvCompleted(Object obj, SocketAsyncEventArgs args)
        {
            if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
            {
                try
                {
                    string recvData = Encoding.UTF8.GetString(args.Buffer, args.Offset, args.BytesTransferred);
                    Console.WriteLine($"[RecvData] : {recvData}");
                    RegisterRecv(args);
                }
                catch (Exception e)
                {

                }
            }
        }

    }
}
