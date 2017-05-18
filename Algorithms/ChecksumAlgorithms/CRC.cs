using System;
using NetworksHomework.Algorithms.Utils;

namespace NetworksHomework.Algorithms.ChecksumAlgorithms
{
    public class CRC : IChecksumAlgorithm
    {
        private uint _state;
        private uint _polynomial ;

        public int ChecksumSize => 4;

        public byte[] ComputeChecksum(byte[] message)
        {
            _state = 0;
            var bitReader = new BitReader(message);
            var currentBit = bitReader.ReadBit();
            while (currentBit != null)
            {
                _state <<= 1;
                _state |= (uint)currentBit;
                if (currentBit == 1)
                {
                    _state ^= _polynomial;
                }
                currentBit = bitReader.ReadBit();
            }
            return BitConverter.GetBytes(_state);
        }

        public CRC(uint polynimial = 0xEDB88320, uint state = 0)
        {
            _polynomial = polynimial;
            _state = state;
        }
    }
}