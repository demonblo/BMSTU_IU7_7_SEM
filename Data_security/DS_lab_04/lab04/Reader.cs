using System;
using System.IO;

namespace RW
{
    class Reader
    {
        private static FileStream _fs;
        public static int size_blk;
        public Reader(string filename, int size)
        {
            if (File.Exists(filename))
            {
                _fs = new FileStream(filename, FileMode.Open);
                size_blk = size;
            }
        }

        public int GetBlock(int num_blk, out UInt64 blk)
        {
            byte[] temp_blk = new byte[sizeof(UInt64)];
            int res, offset = num_blk * size_blk;

            _fs.Seek(offset, SeekOrigin.Begin);
            res = _fs.Read(temp_blk, 0, size_blk);

            blk = BitConverter.ToUInt64(temp_blk, 0);

            return res;
        }

        public void Close()
        {
            _fs.Close();
        }

    }
}