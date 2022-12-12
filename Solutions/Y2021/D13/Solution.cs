namespace AdventOfCode.Solutions.Y2021.D13
{
    using AdventOfCode.Common;
    using SharpLog;

    internal class Solution : Solution<((char, int)[], bool[,])>
    {
        internal override (object, string) Puzzle1(((char, int)[], bool[,]) input)
        {
            (char, int) instruction = input.Item1[0];

            ////Tools.Print2D(input.Item2);

            bool[,] paper = this.Fold(input.Item2, instruction.Item1, instruction.Item2);
            int counter = 0;

            ////Tools.Print2D(paper);

            foreach (bool item in paper)
            {
                if (item)
                {
                    counter++;
                }
            }

            return (counter.ToString(), $"After 1 fold there are {counter} dots left!");
        }

        internal override (object, string) Puzzle2(((char, int)[], bool[,]) input)
        {
            bool[,] paper = input.Item2;

            foreach ((char, int) instruction in input.Item1)
            {
                paper = this.Fold(paper, instruction.Item1, instruction.Item2);
            }

            SharpLog.Logging.LogDebug($"After the folds the following image appears:");
            Logging.LogInfo(Tools.Format(paper));

            return (string.Empty, "See console log!");
        }

        private bool[,] Fold(bool[,] paper, char axis, int index)
        {
            if (axis == 'x')
            {
                return this.FoldX(paper, index);
            }

            return this.FoldY(paper, index);
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
                    if (paper[x, y])
                    {
                        result[x, (2 * index) - y] = true;
                    }
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
                    if (paper[x, y])
                    {
                        result[(2 * index) - x, y] = true;
                    }
                }
            }

            return result;
        }
    }
}
