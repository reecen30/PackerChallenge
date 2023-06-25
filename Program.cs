using System;

namespace PackerChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the file path as a command line argument.");
                return;
            }

            string filePath = args[0];
            string result = Packer.Pack(filePath);
            Console.WriteLine(result);
        }
    }
}
