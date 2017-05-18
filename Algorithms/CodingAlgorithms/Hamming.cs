using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NetworksHomework.Algorithms.Utils;

namespace NetworksHomework.Algorithms.CodingAlgorithms
{
    public class Hamming : ICodingAlgorithm
    {
        private const int PARITY_BITS_COUNT = 3;
        private byte[] _parityCheckMatrix = {0xAA, 0x66,0x1E };
        private byte _syndrom;

        private Dictionary<byte, byte> _encodeTable = new Dictionary<byte, byte>()
        {
            {0, 0x00}, //0000
            {8, 0xE0}, //0001
            {4, 0x98}, //0010
            {12, 0x78}, //0011
            {2, 0x54}, //0100
            {10, 0xB4}, //0101
            {6, 0xCC}, //0110
            {14, 0x2C}, //0111
            {1, 0xD2}, //1000
            {9, 0x32}, //1001
            {5, 0x4A}, //1010
            {13, 0xAA}, //1011
            {3, 0x86}, //1100
            {11, 0x66}, //1101
            {7, 0x1E}, //1110
            {15, 0xFE} //1111
        };

        private void _ComputeSyndrome(byte codeword)
        {
            _syndrom = 0;
            for (int i = 0; i < PARITY_BITS_COUNT; i++)
            {
                _syndrom |= (byte) (MathUtils.BitwiseDotProduct(_parityCheckMatrix[i], codeword) << i);
            }
        }

        private void _CorrectErrors(byte[] code)
        {
            for (int i = 0; i < code.Length; i++)
            {
                _ComputeSyndrome(code[i]);
                if(_syndrom != 0)
                {
                    code[i] ^= (byte) (1 << (8 - _syndrom));
                }
            }
        }

        private byte _DecodeCodeword(byte codeword)
        {
            return _encodeTable.First(x => x.Value == codeword).Key;
        }

        public byte[] Encode(byte[] message)
        {
            var result = new byte[2 * message.Length];
            for (int i = 0; i < message.Length; i++)
            {
                result[2 * i] = _encodeTable[(byte) (message[i] >> 4)];
                result[2 * i + 1] = _encodeTable[(byte) (message[i] & 0x0F)];
            }
            return result;
        }


        public byte[] Decode(byte[] message)
        {
            if ((message.Length % 2) == 1)
            {
                throw new Exception("Invalid data length for Hamming code");
            }
            _CorrectErrors(message);

            var result = new byte[message.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] |= (byte)(_DecodeCodeword(message[2 * i] )<< 4);
                result[i] |=  (_DecodeCodeword(message[2 * i + 1] ));
            }

            return result;
        }
    }
}