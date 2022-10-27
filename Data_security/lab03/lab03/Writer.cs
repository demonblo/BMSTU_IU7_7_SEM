using System;
using System.IO;
using System.Collections;

namespace lab03
{
    class Writer
    {
        private static FileStream _fs;

        public Writer(string filename)
        {
            _fs = new FileStream(filename, FileMode.Create);
        }

        public void SaveInFile(BitArray blk)
        {
            byte[] temp = new byte[blk.Length / 8];
            blk.CopyTo(temp, 0);

            for (int i = 0; i < temp.Length; i++)
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

        public void ViewArray(BitArray arr)
        {
            for (int i = 0; i < arr.Count; i++)
                Console.WriteLine($"{i}, {arr[i]}");

            Console.WriteLine();
            Console.ReadKey();
        }

        public void ViewArray(int[] arr)
        {
            Console.WriteLine(String.Join(" ", arr));
            Console.WriteLine();
            Console.ReadKey();
        }
    }

}