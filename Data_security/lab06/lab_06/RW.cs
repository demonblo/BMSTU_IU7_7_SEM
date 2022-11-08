using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using trees;
using System.Collections;

namespace RW
{
    class Reader
    {
        public static byte[] ReadFull(string filename)
        {
            byte[] text = null;

            if (File.Exists(filename))
                text = File.ReadAllBytes(filename);

            return text;
        }
    }

    class Writer
    {
        public static void Write(byte[] arr, string filename)
        {
            FileStream  fs = new FileStream(filename, FileMode.Create);

            for (int i = 0; i < arr.Length; i++)
                fs.WriteByte(arr[i]);

            fs.Close();
        }

        public static void Write(string str, string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            byte[] res = Encoding.ASCII.GetBytes(str);

            for (int i = 0; i < res.Length; i++)
                fs.WriteByte(res[i]);

            fs.Close();
        }

        public static void Write(BitArray text, string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            int dsize = text.Length % 8;
            byte add = (byte)((8 - dsize) % 8);

            byte[] temp = new byte[dsize == 0 ? text.Length / 8 : text.Length / 8 + 1];

            text.CopyTo(temp, 0);

            for (int i = 0; i < temp.Length; i++)
                fs.WriteByte(temp[i]);

            fs.WriteByte(add);

            fs.Close();
        }

        public static void print(List<TreeNode<int>> arr) 
        {
            foreach (TreeNode<int> node in arr)
                Console.WriteLine("{0} : {1}", node.sign, node.value);
            Console.WriteLine();
        }

        public static void printDict(Dictionary<byte, int> dict)
        {
            foreach (KeyValuePair<byte, int> elem in dict)
                Console.WriteLine("{0} : {1}", elem.Key, elem.Value);
            Console.WriteLine();
        }

        public static void print(BitArray arr)
        {
            foreach (bool bit in arr)
            {
                var temp = bit ? '1' : '0';
                Console.Write(temp);
            }

            Console.WriteLine();
        }
    }
}