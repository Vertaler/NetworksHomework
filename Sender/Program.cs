using System;
using NetworksHomework.Networking;

namespace NetworksHomework.Sender
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var message = Console.ReadLine();
            var sender = new MessageSender(8080);
            while (message != "stop")
            {
                sender.Send(new Message(message));
                message = Console.ReadLine();
            }
        }
    }
}