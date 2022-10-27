using System.Collections;
using System;

namespace lab03
{
    class EncryptionSteps
    {
        public static BitArray Permutate(BitArray blk, int[] ind)
        {
            BitArray p_arr = new BitArray(ind.Length);

            for (int i = 0; i < ind.Length; i++)
                p_arr[i] = blk[ind[i] - 1];

            return p_arr;
        }

        public static void GetLeftRightPart(BitArray blk, out BitArray left, out BitArray right)
        {
            left = new BitArray(blk.Count / 2);
            right = new BitArray(blk.Count / 2);

            for (int i = 0; i < blk.Count / 2; i++)
                left[i] = blk[i];

            for (int i = blk.Count / 2, i_pos = 0; i < blk.Count; i++, i_pos++)
                right[i_pos] = blk[i];
        }

        public static BitArray FeistelCipher(BitArray data, BitArray ikey)
        {
            BitArray blk = Permutate(data, Cipher.prmE);

            blk.Xor(ikey);

            int[] res = new int[8];

            for (int i = 0; i < 8; i++)
            {
                int curBlk = i * 6;
                int x = 0, y = 0;

                if (blk[curBlk])        x += 10;
                if (blk[curBlk + 5])    x += 1;

                if (blk[curBlk + 1])    y += 1000;
                if (blk[curBlk + 2])    y += 100;
                if (blk[curBlk + 3])    y += 10;
                if (blk[curBlk + 4])    y += 1;

                x = Convert.ToInt32(x.ToString(), 2);
                y = Convert.ToInt32(y.ToString(), 2);

                res[i] = Cipher.sBlocks[i][x][y];
            }

            blk = _GetBitArray(res);

            return Permutate(blk, Cipher.prmP);
        }

        private static BitArray _GetBitArray(int[] arr)
        {
            BitArray res = new BitArray(arr.Length * 4);

            for (int i_blk = 0, i = 0; i_blk < arr.Length; i_blk++, i += 4)
            {
                if (arr[i_blk] >= 8)
                {
                    res[i + 3] = true;
                    arr[i_blk] -= 8;
                }

                if (arr[i_blk] >= 4)
                {
                    res[i + 2] = true;
                    arr[i_blk] -= 4;
                }

                if (arr[i_blk] >= 2)
                {
                    res[i + 1] = true;
                    arr[i_blk] -= 2;
                }

                if (arr[i_blk] == 1)
                    res[i] = true;
            }

            return res;
        }
        
    }
}