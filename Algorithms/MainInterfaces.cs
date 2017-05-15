namespace NetworksHomework.Algorithms
{
    public interface IChecksumAlgorithm
    {
        byte[] ComputeChecksum(byte[] message);
    }

    public interface ICompressionAlgorithm
    {
        byte[] Compress(byte[] message);
        byte[] Decompress(byte[] message);
    }

    public interface ICodingAlgorithm
    {
        byte[] Encode(byte[] message);
        byte[] Decode(byte[] message);
    }
}