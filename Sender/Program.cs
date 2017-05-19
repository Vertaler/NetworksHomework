using System;
using NetworksHomework.Algorithms.ChecksumAlgorithms;
using NetworksHomework.Algorithms.CodingAlgorithms;
using NetworksHomework.Algorithms.CompressionAlgorithms;
using NetworksHomework.Networking;

namespace NetworksHomework.Sender
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var userInput = Console.ReadLine();
            var sender = new MessageSender(8080);
            while (userInput != "stop")
            {
                var message = new Message(userInput);
                //message.ComputeChecksum(new CRC()).AddNoise();
                //message.Encode(new Hamming()).AddNoise();
                message.Compress(new ShannonFano());
                sender.Send(message);
                userInput = Console.ReadLine();
            }
        }
    }
}