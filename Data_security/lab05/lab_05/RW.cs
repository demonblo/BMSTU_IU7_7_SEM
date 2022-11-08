using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace RW
{
    class Reader
    {
        public static byte[] read(string filename)
        {
            byte[] text = null;

            if (File.Exists(filename))
                text = File.ReadAllBytes(filename);

            return text;
        }

        public static bool isEqual(byte[] arr1, byte[] arr2)
        {
            if (arr1.Length != arr2.Length)
                return false;

            for (int i = 0; i < arr1.Length; i++)
                if (arr1[i] != arr2[i])
                    return false;

            return true;
        }
    }

    class Writer
    {
        public static void write(string filename, byte[] arr)
        {
            FileStream  fs = new FileStream(filename, FileMode.Create);

            for (int i = 0; i < arr.Length; i++)
                fs.WriteByte(arr[i]);

            fs.Close();
        }

        public static void addSign(string filename, byte sign)
        {
            FileStream fs = new FileStream(filename, FileMode.Append);
            fs.WriteByte(sign);
            fs.Close();
        }


        public static void print(byte[] str)
        {
            foreach (byte b in str)
                Console.Write(b);
            Console.WriteLine();
        }
    }
}