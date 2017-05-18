using System.IO;
using System.Net;
using System.Net.Sockets;

namespace NetworksHomework.Networking
{
    public class MessageSender
    {
        private int _port;
        private TcpClient _tcpClient;

        public MessageSender(int port)
        {
            _port = port;
           _tcpClient = new TcpClient();
            _tcpClient.Client.Connect(IPAddress.Parse("127.0.0.1"), _port);
        }

        public void Send(Message message)
        {

            //var writer = new BinaryWriter();
            _tcpClient.GetStream().Write(message.Content, 0, message.Size);
        }
    }
}