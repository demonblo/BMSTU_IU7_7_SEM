using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab03
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileNameScr = @"tests/tiny_test.txt";                //tiny_test.txt img.jpg test.txt;
            string fileNameCipher = @"tests/ciphered_version.txt";
            string fileNameResult = @"tests/uncipher_version.txt";

            Cipher cipher = new Cipher();
            
            cipher.Encrypt(fileNameScr, fileNameCipher);
            cipher.Decrypt(fileNameCipher, fileNameResult);

            Console.WriteLine("Press any button");
            Console.ReadKey();
        }
    }
}