namespace NetworksHomework.Algorithms.Utils
{
    public static class MathUtils
    {
        public static byte BitwiseDotProduct(byte a, byte b)
        {
            var and = a & b;
            int weight = 0;
            for (int i = 1; i <= 128; i *= 2)
            {
                if ((and & (i)) != 0)
                {
                    weight++;
                }
            }
            return (byte)(weight % 2);
        }
    }
}