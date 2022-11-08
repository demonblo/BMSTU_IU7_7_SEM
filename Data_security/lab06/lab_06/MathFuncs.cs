using System.Collections.Generic;
using trees;

namespace calculations
{
    class MathFuncs
    {
        public static void InsertionSort(ref List<TreeNode<int>> arr)
        {
            int val, j;
            TreeNode<int> key, temp;

            for (var i = 1; i < arr.Count; i++)
            {
                key = arr[i];
                val = arr[i].value;
                j = i;
                
                while ((j > 0) && (arr[j - 1].value > val))
                {
                    temp = arr[j - 1];
                    arr[j - 1] = arr[j];
                    arr[j] = temp;
                    j--;
                }

                arr[j] = key;
            }
        }
    }
}