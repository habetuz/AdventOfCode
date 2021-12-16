using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D13
{
    internal class Solution : Solution<((char, int)[], bool[,])>
    {
        internal override string Puzzle1(((char, int)[], bool[,]) input)
        {
            (char, int) instruction = input.Item1[0];

            ////Tools.Print2D(input.Item2);

            bool[,] paper = Fold(input.Item2 , instruction.Item1, instruction.Item2);
            int counter = 0;

            ////Tools.Print2D(paper);

            foreach (bool item in paper)
            {
                if (item) counter++;
            }

            s_logger.Log($"After 1 fold there are {counter} dots left!", SharpLog.LogType.Info);
            return counter.ToString();
            
        }

        internal override string Puzzle2(((char, int)[], bool[,]) input)
        {
            bool[,] paper = input.Item2;

            foreach ((char, int) instruction in input.Item1)
            {
                paper = Fold(paper, instruction.Item1, instruction.Item2);
            }
            
            s_logger.Log($"After the folds the following image appears:", SharpLog.LogType.Info);
            Tools.Print2D(paper);

            return "See the console log!";

        }

        private bool[,] Fold(bool[,] paper, char axis, int index)
        {
            if (axis == 'x') return FoldX(paper, index);
            return FoldY(paper, index);
        }

        private bool[,] FoldY(bool[,] paper, int index)
        {
            bool[,] result = new bool[paper.GetLength(0), index];

            for (int y = 0; y < result.GetLength(1); y++)
            {
                for (int x = 0; x < result.GetLength(0); x++)
                {
                    result[x, y] = paper[x, y];
                }
            }

            for (int y = index + 1; y < paper.GetLength(1); y++)
            {
                for (int x = 0; x < paper.GetLength(0); x++)
                {
                    if (paper[x, y]) result[x, 2 * index - y] = true;
                }
            }

            return result;
        }
        private bool[,] FoldX(bool[,] paper, int index)
        {
            bool[,] result = new bool[index, paper.GetLength(1)];

            for (int y = 0; y < result.GetLength(1); y++)
            {
                for (int x = 0; x < result.GetLength(0); x++)
                {
                    result[x, y] = paper[x, y];
                }
            }

            for (int y = 0 + 1; y < paper.GetLength(1); y++)
            {
                for (int x = index; x < paper.GetLength(0); x++)
                {
                    if (paper[x, y]) result[2 * index - x, y] = true;
                }
            }

            return result;
        }

    }
}
