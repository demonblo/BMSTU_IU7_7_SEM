using System.Collections;


namespace lab03
{
    class Cipher
    {
        private static string _root = @"";
        
        private string _fileIP = _root + @"Tables/IP.txt";
        private string _fileIPRev = _root + @"Tables/IP-1.txt";
        private string _fileSi = _root + @"Tables/Si.txt";
        private string _fileB = _root + @"Tables/B.txt";
        private string _fileCP = _root + @"Tables/CP.txt";
        private string _fileE = _root + @"Tables/E.txt";
        private string _fileP = _root + @"Tables/P.txt";
        private string _fileSBlocks = _root + @"Tables/Sblocks.txt";


        private string _in, _out;

        private static BitArray _key;
        private static BitArray[] _keys_arr;

        static int[] prmIP;
        static int[] prmIPRev;
        public static int[] moveSi;
        public static int[] prmCP;
        public static int[] prmB;
        public static int[] prmE;
        public static int[] prmP;
        public static int[][][] sBlocks;

        public Cipher()
        {          
            Reader.GetFromFile(_fileIP, out prmIP);
            Reader.GetFromFile(_fileIPRev, out prmIPRev);
            Reader.GetFromFile(_fileSi, out moveSi);
            Reader.GetFromFile(_fileB, out prmB);
            Reader.GetFromFile(_fileCP, out prmCP);
            Reader.GetFromFile(_fileE, out prmE);
            Reader.GetFromFile(_fileP, out prmP);
            sBlocks = Reader.GetSBlocksFromFile(_fileSBlocks);

            KeysProcessing.GetKey(out _key);
            KeysProcessing.GetKeys(_key, out _keys_arr);
        }

        public void Encrypt(string inFile, string outFile)
        {
            _in = inFile;
            _out = outFile;

            Reader reader = new Reader(_in);
            Writer writer = new Writer(_out);

            BitArray blk;
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

        private static BitArray DoEcryption(BitArray blk)
        {
            BitArray left, right;
                    
            BitArray prm_blk = EncryptionSteps.Permutate(blk, prmIP);
            EncryptionSteps.GetLeftRightPart(prm_blk, out left, out right);

            for (int i = 0; i < _keys_arr.Length; i++)
            {
                var temp_left = right;
                right = left.Xor(EncryptionSteps.FeistelCipher(right, _keys_arr[i]));
                left = temp_left;
            }

            prm_blk = KeysProcessing.Join(left, right);
            prm_blk = EncryptionSteps.Permutate(prm_blk, prmIPRev);

            return prm_blk;
        }

        public void Decrypt(string inFile, string outFile)
        {
            _in = inFile;
            _out = outFile;

            Reader reader = new Reader(_in);
            Writer writer = new Writer(_out);

            BitArray blk;
            int num = 0;

            while (reader.GetBlock(num, out blk) == 8)
            {
                var eblk = DoDecryption(blk);
                writer.SaveInFile(eblk);
                num++;
            }

            int[] temp = new int[2];
            blk.CopyTo(temp, 0);

            if (temp[0] != 0)
                writer.CutFile(8 - temp[0]);

            reader.Close();
            writer.Close();
        }

        public static BitArray DoDecryption(BitArray blk)
        {
            BitArray left, right;

            BitArray prm_blk = EncryptionSteps.Permutate(blk, prmIP);
            EncryptionSteps.GetLeftRightPart(prm_blk, out left, out right);

            for (int i = _keys_arr.Length - 1; i >= 0; i--)
            {
                var temp_right = left;
                left = right.Xor(EncryptionSteps.FeistelCipher(left, _keys_arr[i]));
                right = temp_right;
            }

            prm_blk = KeysProcessing.Join(left, right);
            prm_blk = EncryptionSteps.Permutate(prm_blk, prmIPRev);

            return prm_blk;
        }
    
    }
}