using System;
using NetworksHomework.Networking;

namespace NetworksHomework.Sender
{
    public class Sender
    {
        public static void Main(string[] Args)
        {
            var message = Console.ReadLine();
            var sender = new MessageSender(8080);
            while (message != "stop")
            {

            }
        }
    }
}