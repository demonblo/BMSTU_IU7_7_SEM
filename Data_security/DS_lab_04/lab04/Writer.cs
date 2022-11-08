using System;
using System.IO;

namespace RW
{
    class Writer
    {
        private static FileStream _fs;
        public static int size_blk;

        public Writer(string filename, int size)
        {
            _fs = new FileStream(filename, FileMode.Create);
            size_blk = size;
        }

        public void SaveInFile(UInt64 blk)
        {
            byte[] temp = BitConverter.GetBytes(blk);

            for (int i = 0; i < size_blk; i++)
                _fs.WriteByte(temp[i]);
        }

        public void SaveSizeInFile(int size)
        {
            byte add = (byte)size;

            _fs.WriteByte(add);
        }

        public void CutFile(int num)
        {
            _fs.SetLength(_fs.Length - num);
        }

        public void Close()
        {
            _fs.Close();
        }

        public static void ViewArray(byte[] arr)
        {
            Console.WriteLine(String.Join(" ", arr));
            Console.WriteLine();
            //Console.ReadKey();
        }
    }

}