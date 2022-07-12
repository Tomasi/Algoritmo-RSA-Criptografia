using RsaDotNet;
using System.Numerics;
using System.Text;

namespace Program
{
    class Program
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static List<string> SplitChunk(string chunk, int size)
        {
            List<string> retorno = new List<string>();

            int index = 0;
            string temp = string.Empty;
            foreach (char c in chunk)
            {
                temp += c;
                index += 1;

                if (index == size)
                {
                    retorno.Add(temp);
                    temp = string.Empty;
                    index = 0;
                }
            }

            return retorno;
        }
        public static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                throw new ArgumentException("\nPlease enter three parameters. " +
                                            "\n1. Path of file that contain keys" +
                                            "\n2. Path of file that will be encrypted" +
                                            "\n3. Path of output file.\n");
            }

            string? kyeFilePath = args[0];
            string? sourceFilePath = args[1];
            string? destFilePath = args[2];

            string[] keys = File.ReadAllLines(kyeFilePath);
            string sourceText = File.ReadAllText(sourceFilePath);

            string moduleStr = "";
            string keyStr = "";

            int count = 0;
            foreach (string line in keys)
            {
                if (count == 0)
                {
                    moduleStr = line;
                }
                else if (count == 1)
                {
                    keyStr = line;
                }

                count += 1;
            }

            BigInteger module = BigInteger.Parse(moduleStr);
            BigInteger key = BigInteger.Parse(keyStr);

            int chunkSize = TextChunk.BlockSize(module);

            string codedText = Base64Encode(sourceText);

            using (StreamWriter fs = new StreamWriter(destFilePath))
            {
                foreach (var i in SplitChunk(codedText, chunkSize))
                {
                    BigInteger originalChunk = new BigInteger(Encoding.UTF8.GetBytes(i));
                    BigInteger encodedChunk = BigInteger.ModPow(originalChunk, key, module);

                    fs.WriteLine(encodedChunk);
                }
            }
        }
    }

}