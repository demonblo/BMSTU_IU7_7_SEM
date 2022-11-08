using System;
using RW;
using System.Security.Cryptography;

/*
 * RSA  шифрование - public key
 *      расшифровка - private key
 *      
 * Подпись - наоборот      
 */


namespace lab05
{
    class Signature
    {
        private static int _size;

        private static byte[] _encryptPblKey;
        private static byte[] _decryptPrvKey;

        private static string fileEncrypt;
        private static string fileDecrypt;

        private static RSACryptoServiceProvider RSA;

        public Signature(string fileEn, string fileDec)
        {
            _size = 1024;

            fileEncrypt = fileEn;
            fileDecrypt = fileDec;

            RSA = new RSACryptoServiceProvider(_size);

            _decryptPrvKey = RSA.ExportCspBlob(true);   /* true = close key */
            _encryptPblKey = RSA.ExportCspBlob(false);  /* false = open key */

            Writer.write(fileEncrypt, _encryptPblKey);
            Writer.write(fileDecrypt, _decryptPrvKey);
        }

        public Signature(string srcKey)
        {
            RSA = new RSACryptoServiceProvider(_size);

            _decryptPrvKey = Reader.read(srcKey);

            RSA.ImportCspBlob(_decryptPrvKey);
        }

        public void Sign(string data, string dest)
        {
            byte[] text = Reader.read(data);
            byte[] textHashed = SHA1.Create().ComputeHash(text);

            byte[] res = RSA.Encrypt(textHashed, true);

            Writer.write(dest, res);
        }

        public void CheckData(string signed, string data)
        {
            byte[] sign = Reader.read(signed);

            byte[] text = Reader.read(data);
            byte[] textHashed = SHA1.Create().ComputeHash(text);

            byte[] dsign = RSA.Decrypt(sign, true);

            if (Reader.isEqual(textHashed, dsign))
                Console.WriteLine("DATA IS CORRECT\n");
            else
                Console.WriteLine("DATA IS WRONG\n");
        }
    }
}