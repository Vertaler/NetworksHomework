using System;
using NetworksHomework.Algorithms.ChecksumAlgorithms;
using NetworksHomework.Networking;

namespace NetworksHomework.Reciever
{
    internal class Program
    {

         public static void Main(string[] args)
        {
            var reciever = new MessageReciever(8080);
            reciever.OnMessage += (message) =>
            {

                Console.WriteLine($"Recieve: \"{message.AsString()}\"");
                if (!message.ValidateChecksum(new CRC()))
                {
                    Console.WriteLine("Some error occured!");
                }

            };
            reciever.Listen();
        }

    }
}