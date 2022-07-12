using RsaDotNet;
using System.Numerics;
using System.Text;

namespace Program
{
    class Program
    {
        static public string DecodeFrom64(string encodedData)
        {
            while (encodedData.Length % 4 != 0)
            {
                encodedData += "=";
            }
            byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);
            string returnValue = Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnValue;
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("\nPlease enter three parameters. " +
                                            "\n1. Path of file that contain keys" +
                                            "\n2. Path of file that will be descrypted" +
                                            "\n3. Path of output file.\n");
            }
            
            string? privateKeyFilePath = args[0];
            string? sourceFilePath = args[1];
            string? destFilePath = args[2];

            string[] keys = File.ReadAllLines(privateKeyFilePath);
            string[] sourceText = File.ReadAllLines(sourceFilePath);

            string moduleStr = "";
            string keyStr = "";

            int count = 0;
            foreach (string line in keys)
            {
                if (count == 0)
                {
                    moduleStr = line;
                }
                else if (count == 2)
                {
                    keyStr = line;
                }

                count += 1;
            }

            BigInteger module = BigInteger.Parse(moduleStr);
            BigInteger key = BigInteger.Parse(keyStr);


            string codedText = "";
            using (StreamWriter fs = new StreamWriter(destFilePath))
            {
                foreach (var i in sourceText)
                {
                    BigInteger encodedChunk = BigInteger.Parse(i);
                    BigInteger originalChunk = BigInteger.ModPow(encodedChunk, key, module);

                    TextChunk text = new(originalChunk);
                    string base64encodedChunk = text.ToString();

                    codedText += base64encodedChunk;
                }
                fs.Write(DecodeFrom64(codedText));
            }
        }
    }
}