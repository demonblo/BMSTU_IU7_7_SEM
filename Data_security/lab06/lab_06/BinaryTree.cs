using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using calculations;

namespace trees
{
    class TreeNode<T> where T : IComparable<T>
    {
        public T value;
        public byte sign;
        public TreeNode<T> left, right;

        public TreeNode(byte s, T val)
        {
            sign = s;
            value = val;
        }

        public TreeNode(T val)
        {
            value = val;
        }

        public TreeNode() { }
    }

    class BinaryTree<T> where T : IComparable<T>
    {
        public TreeNode<T> root;

        public static void Create(List<TreeNode<int>> stat, out BinaryTree<int> tree)
        {
            tree = new BinaryTree<int>();

            while (stat.Count > 1)
            {
                MathFuncs.InsertionSort(ref stat);

                TreeNode<int> parent = new TreeNode<int>(stat[0].value + stat[1].value);
                parent.left = stat[0];
                parent.right = stat[1];

                stat.RemoveAt(0);
                stat.RemoveAt(0);

                stat.Add(parent);
            }

            tree.root = stat.First();
        }

        public void ConvertToJSON(string filename)
        {
            string jsonString = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(this);
            File.WriteAllText(filename, jsonString);
        }

        public static BinaryTree<T> ConvertFromJSON(string filename)
        {
            string jsonString = File.ReadAllText(filename);
            return new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<BinaryTree<T>>(jsonString);
        }

        public void printTree()
        {
            _printNode(root);
        }

        private void _printNode(TreeNode<T> startNode, string indent = "", string side = null)
        {
            if (startNode != null)
            {
                var nodeSide = side == null ? "+" : side;

                if (startNode.left != null && startNode.right != null)
                    Console.WriteLine($"{indent} [{nodeSide}]- {startNode.value}");
                else if (startNode.left == null && startNode.right == null)
                    Console.WriteLine($"{indent} [{nodeSide}]- {startNode.value}, {Convert.ToChar(startNode.sign)}");

                indent += new string(' ', 3);

                _printNode(startNode.left, indent, "L");
                _printNode(startNode.right, indent, "R");
            }
        }

        private static void _print(TreeNode<T> node)
        {
            if (node == null) return;
            _print(node.left);

            Console.Write(node.sign + " : " + node.value + " ");

            if (node.right != null)
                _print(node.right);
        }

        public static void print(BinaryTree<T> tree)
        {
            _print(tree.root);
            Console.WriteLine();
        }
    }
}