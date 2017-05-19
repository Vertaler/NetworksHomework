using System;
using System.Collections;
using System.Linq;
using System.Text;
using NetworksHomework.Algorithms;
using NetworksHomework.Algorithms.ChecksumAlgorithms;

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
                    nameof(message)
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

            for (int i = 0; i < bytes.Length; i++)
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

        public Message AddNoise()
        {
            for (int i = 0; i < Size; i++)
            {
                _buffer[i] ^= 4;
            }
            return this;
        }

        public string AsString()
        {
            return Encoding.ASCII.GetString(Content);
        }

        public byte[] RemoveLastBytes(int bytesCount)
        {
            if (bytesCount > Size)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(bytesCount)
                );
            }
            var result = Content.Skip(Size - bytesCount).Take(bytesCount).ToArray();
            Size -= bytesCount;
            return result;
        }

        public Message ComputeChecksum(IChecksumAlgorithm algorithm)
        {
            var checksum = algorithm.ComputeChecksum(Content);
            AddBytes(checksum);
            return this;
        }

        public bool ValidateChecksum(IChecksumAlgorithm algorithm)
        {
            var recieved = RemoveLastBytes(algorithm.ChecksumSize);
            var computed = algorithm.ComputeChecksum(Content);

            return recieved.SequenceEqual(computed);
        }

        public Message Encode(ICodingAlgorithm coder)
        {
            var result = coder.Encode(Content);
            Size = 0;
            AddBytes(result);
            return this;
        }

        public Message Decode(ICodingAlgorithm coder)
        {
            var result = coder.Decode(Content);
            Size = 0;
            AddBytes(result);
            return this;
        }

        public Message Compress(ICompressionAlgorithm packer)
        {
            var result = packer.Compress(Content);
            Size = 0;
            AddBytes(result);
            return this;
        }

        public Message Decompress(ICompressionAlgorithm packer)
        {
            var result = packer.Decompress(Content);
            Size = 0;
            AddBytes(result);
            return this;
        }


        public Message()
        {
            _buffer = new byte[BUFFER_SIZE];
            Size = 0;
        }

        public Message(byte[] message) : this()
        {
            _CheckBounds(message);
            AddBytes(message);
        }

        public Message(string message) : this()
        {
            _CheckBounds(Encoding.ASCII.GetBytes(message));
            AddString(message);
        }
    }
}