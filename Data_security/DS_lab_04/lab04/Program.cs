using System;

namespace lab04
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileNameScr = @"tests/test.txt";                //@"test_video.gif"; @"img.jpg"; @"text.txt";
            string fileNameCipher = @"tests/ciphered.txt";
            string fileNameResult = @"tests/uncipher.txt";

            Cipher cipher = new Cipher();

            cipher.Encrypt(fileNameScr, fileNameCipher);
            cipher.Decrypt(fileNameCipher, fileNameResult);

            Console.WriteLine("Press any button");
            Console.ReadKey();
        }
    }
}