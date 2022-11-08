using System;
using System.Numerics;
using RW;

namespace lab04
{
    class Cipher
    {
        private string _in, _out;
        static UInt64 publicKey, privateKey, n;
        RSA rsa;

        public Cipher()
        {
            rsa = new RSA();
            publicKey = rsa.publicKey;
            privateKey = rsa.privateKey;
            n = rsa.n;
        }

        public void Encrypt(string inFile, string outFile)
        {
            int size_read = 7, size_write = 8;
            _in = inFile;
            _out = outFile;

            Reader reader = new Reader(_in, size_read);
            Writer writer = new Writer(_out, size_write);

            UInt64 blk;
            int num = 0, size, prevsize = 0;

            while ((size = reader.GetBlock(num, out blk)) > 0)
            {
                var eblk = DoEcryption(blk);
                writer.SaveInFile(eblk);
                prevsize = size;

                num++;
            }

            writer.SaveSizeInFile(prevsize);

            reader.Close();
            writer.Close();
        }

        private static UInt64 DoEcryption(UInt64 blk)
        {
            return (UInt64)BigInteger.ModPow(blk, publicKey, n);
        }

        public void Decrypt(string inFile, string outFile)
        {
            int size_read = 8, size_write = 7;
            _in = inFile;
            _out = outFile;

            Reader reader = new Reader(_in, size_read);
            Writer writer = new Writer(_out, size_write);

            UInt64 blk;
            int num = 0;

            while (reader.GetBlock(num, out blk) == 8)
            {
                var eblk = DoDecryption(blk);
                writer.SaveInFile(eblk);
                num++;
            }

            if (blk != 0)
                writer.CutFile(8 - (int)blk);

            reader.Close();
            writer.Close();
        }

        private static UInt64 DoDecryption(UInt64 blk)
        {
            return (UInt64)BigInteger.ModPow(blk, privateKey, n);
        }
    }
}