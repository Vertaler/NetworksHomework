using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace NetworksHomework.Networking
{
    public class Message
    {
        private byte[] _buffer;

        public const int BUFFER_SIZE = 1024;
        public byte[] Content => _buffer.Take(Size).ToArray();
        public int Size { get; private set; }

        private void _CheckBounds(byte[] message)
        {
            if (message.Length > BUFFER_SIZE)
            {
                throw new ArgumentOutOfRangeException(
                    $"Message length must be less than {BUFFER_SIZE}. Real length: {message.Length}"
                );
            }
        }

        public void AddBytes(byte[] bytes)
        {
            var newSize = bytes.Length + Size;

            if (newSize > _buffer.Length)
            {
                throw new Exception("Buffer overflow");
            }

            for (int i = 0 ; i < bytes.Length; i++)
            {
                _buffer[i + Size] = bytes[i];
            }
            Size = newSize;

        }

        public void AddString(string str)
        {
            AddBytes(Encoding.ASCII.GetBytes(str));
        }

        public void AddString(string str, Encoding encoding)
        {
            AddBytes(encoding.GetBytes(str));
        }

        public string AsString()
        {
            return Encoding.ASCII.GetString(Content);
        }

        public Message()
        {
            _buffer = new byte[BUFFER_SIZE];
            Size = 0;
        }

        public Message(byte[] message):this()
        {
            _CheckBounds(message);
            AddBytes(message);
        }

        public Message(string message):this()
        {
            _CheckBounds(Encoding.ASCII.GetBytes(message));
            AddString(message);
        }
    }
}