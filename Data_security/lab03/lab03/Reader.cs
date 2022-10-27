using System;
using System.Collections;
using System.IO;

namespace lab03
{
    class Reader
    {
        private static FileStream _fs;
        public static int size_blk = 8;
        public Reader(string filename)
        {
            if (File.Exists(filename))
            {
                _fs = new FileStream(filename, FileMode.Open);
            }
        }

        public int GetBlock(int num_blk, out BitArray blk_bit)
        {
            byte[] blk = new byte[size_blk];
            int res, offset = num_blk * size_blk;

            _fs.Seek(offset, SeekOrigin.Begin);
            res = _fs.Read(blk, 0, size_blk);

            blk_bit = new BitArray(blk);

            return res;
        }

        public void Close()
        {
            _fs.Close();
        }

        public static void GetFromFile(string filename, out int[] data)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string text = reader.ReadToEnd();
                    string[] str_num = text.Split(' ');
                    data = new int[str_num.Length];

                    for (int i = 0; i < str_num.Length; i++)
                    {
                        data[i] = int.Parse(str_num[i]);
                    }
                }
            }
            catch (Exception exc)
            {
                data = null;

                Console.WriteLine(exc.Message);
            }
        }

        public static int[][][] GetSBlocksFromFile(string filename)
        {
            int num_blocks = 8;
            int num_lines = 4;
            int num_elem = 16;
            int[][][] data = new int[num_blocks][][];

            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string text = reader.ReadToEnd();
                    string[] str_blks = text.Split('-');

                    for (int i = 0; i < num_blocks; i++)
                    {
                        int[][] blk = new int[num_blocks][];
                        string[] str_blks_lines = str_blks[i].Trim().Split('\n');

                        for (int j = 0; j < num_lines; j++)
                        {
                            string[] str_line = str_blks_lines[j].Trim().Split(' ');
                            int[] line = new int[num_elem];

                            for (int k = 0; k < num_elem; k++)
                                line[k] = Convert.ToInt32(str_line[k]);
                            
                            blk[j] = line;
                        }

                        data[i] = blk;
                    }
                }
            }
            catch (Exception exc)
            {
                data = null;

                Console.WriteLine(exc.Message);
            }

            return data;
        }
    }
}