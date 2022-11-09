using System;

namespace lab05
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileEncrypt = @"encryptKey.txt";
            string fileDecrypt = @"decryptKey.txt";

            string fileNameScr = @"tests/test.txt";                //@"test_video.gif"; @"img.jpg"; @"text.txt";
            string fileNameSigned = @"tests/signed_version.txt";

            Signature sign = new Signature(fileEncrypt, fileDecrypt);
            sign.Sign(fileNameScr, fileNameSigned);

            byte s = 69; /* E */
            //Writer.addSign(fileNameScr, s);

            Signature check = new Signature(fileDecrypt);
            check.CheckData(fileNameSigned, fileNameScr);

            Console.WriteLine("Press any button");
            Console.ReadKey();
        }
    }
}