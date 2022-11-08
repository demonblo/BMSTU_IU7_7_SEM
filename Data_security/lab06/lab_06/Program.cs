using System;
using fileCompression;
using trees;

namespace lab06
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileSrc = @"tests/test.txt";                //@"test_video.gif"; @"img.jpg"; @"text.txt";
            string fileCom = @"tests/compressed_version.txt";
            string fileRes = @"tests/decompressed_version.txt";

            string fileTree = @"tree.txt";

            BinaryTree<int> tree;

            tree = Huffman.Compress(fileSrc, fileCom);
            
            tree.printTree();
            tree.ConvertToJSON(fileTree);

            Huffman.Decompress(fileCom, fileRes, BinaryTree<int>.ConvertFromJSON(fileTree));


            Console.WriteLine("\nPress any button");
            Console.ReadKey();
        }
    }
}