using System;
using System.Net;
using System.Net.Sockets;

namespace NetworksHomework.Networking
{
    public class MessageReciever
    {
        private TcpListener _tcpListener;
        private byte[] _buffer;

        public event Action<Message> OnMessage;

        private void _SocketHandle(Socket socket)
        {
            var bytes = socket.Receive(_buffer, _buffer.Length, 0);
            while (bytes > 0)
            {
                OnMessage?.Invoke(new Message(_buffer));
                bytes = socket.Receive(_buffer, _buffer.Length, 0);
            }
            socket.Close();
        }

        public void Listen()
        {
            while (true)
            {
                var socket = _tcpListener.AcceptSocket();
                _SocketHandle(socket);
            }
        }

        public MessageReciever(int port)
        {
            _buffer = new byte[1024];
            _tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"),port);
            _tcpListener.Start();
        }
    }
}