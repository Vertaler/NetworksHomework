using System;
using System.Linq;

namespace NetworksHomework.Networking
{
    public class Message
    {
        private byte[] _buffer;

        public const int BUFFER_SIZE = 1024;
        public byte[] Content => _buffer.Take(Size).ToArray();
        public int Size { get; }

        public Message()
        {
            _buffer = new byte[BUFFER_SIZE];
            Size = 0;
        }

        public void AddBytes(byte[] bytes)
        {
            var limit = bytes.Length + Size;

            if (limit > _buffer.Length)
            {
                throw new Exception("Buffer overflow");
            }

            for (int i = 0 ; i < bytes.Length; i++)
            {
                _buffer[i + Size] = bytes[i];
            }

        }

        public Message(byte[] message):this()
        {
            if (message.Length > BUFFER_SIZE)
            {
                throw new ArgumentOutOfRangeException(
                    $"Message length must be less than {BUFFER_SIZE}. Real length: {message.Length}"
                );
            }
            for (int i = 0; i < message.Length; i++)
            {
                _buffer[i] = message[i];
            }
        }

        public Message(string message):this()
        {
            if (message.Length > BUFFER_SIZE)
            {
                throw new ArgumentOutOfRangeException(
                    $"Message length must be less than {BUFFER_SIZE}. Real length: {message.Length}"
                );
            }
        }
    }
}