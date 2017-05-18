namespace NetworksHomework.Algorithms.Utils
{
    public class BitReader
    {
        private int _bitIndex = 7;
        private byte[] _buffer;
        private int _byteIndex ;

        private bool _IsBitSet()
        {
            return  ((_buffer[_byteIndex]) & (1 << _bitIndex)) != 0;
        }

        public BitReader(byte[] buffer)
        {
            _buffer = buffer;
        }

        public byte? ReadBit()
        {
            if (_bitIndex == -1)
            {
                _bitIndex = 7;
                _byteIndex++;
            }
            if (_byteIndex >= _buffer.Length)
            {
                _byteIndex = 0;
                return null;
            }
            byte result = (byte)(_IsBitSet()? 1 :0);
            _bitIndex--;
            return result;
        }
    }
}