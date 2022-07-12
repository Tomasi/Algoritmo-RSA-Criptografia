using System.Numerics;

namespace RsaDotNet
{
    internal class TextChunk
    {
        private string _stringVal;

        public TextChunk(string stringVal)
        {
            _stringVal = stringVal;
        }

        public override string ToString()
        {
            return _stringVal;
        }

        public TextChunk(BigInteger n)
        {
            BigInteger big256 = new BigInteger(256);
            BigInteger big0 = new BigInteger(0);

            if (n.CompareTo(big0) == 0)
            {
                _stringVal = "0";
            }
            else
            {
                _stringVal = "";
                while (n.CompareTo(big0) > 0)
                {
                    BigInteger quot = n / big256;
                    BigInteger rem = n % big256;
                    int charNum = (int)rem;
                    _stringVal += (char)charNum;
                    n = quot;
                }
            }
        }

        public BigInteger BigIntValue()
        {
            BigInteger big256 = new BigInteger(256);
            BigInteger result = new BigInteger(0);

            for (int i = _stringVal.Length - 1; i >= 0; i--)
            {
                result = result * big256;
                result = result + new BigInteger((int)_stringVal[i]);
            }

            return result;
        }

        public static int BlockSize(BigInteger n)
        {
            BigInteger big1 = new BigInteger(1);
            BigInteger big2 = new BigInteger(2);

            int blockSize = 0;
            BigInteger temp = n - big1;

            while (temp.CompareTo(big1) > 0)
            {
                temp = temp / big2;
                blockSize++;
            }
            return blockSize / 8;
        }
    }
}
