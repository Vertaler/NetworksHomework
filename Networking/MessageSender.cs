using System.Net;
using System.Net.Sockets;

namespace NetworksHomework.Networking
{
    public class MessageSender
    {
        private TcpClient _tcpClient;
        private int _port;

        public MessageSender(int port)
        {
            _tcpClient = new TcpClient();
            _port = port;
        }

        public void Send(Message message)
        {
            _tcpClient.Client.Connect(IPAddress.Parse("127.0.0.1"), _port);
            _tcpClient.GetStream().Write(message.Content, 0, message.Content.Length);
            _tcpClient.Close();
        }
    }
}