namespace AdventOfCode.Solutions.Y2021.D01
{
    using AdventOfCode.Common;
    using SharpLog;

    internal class Solution : Solution<int[]>
    {
        internal override string Puzzle1(int[] input)
        {
            int increaseCounter = 0;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i - 1] < input[i])
                {
                    increaseCounter++;
                }
            }

            SharpLog.Logging.LogDebug(string.Format("The ocean floor increases {0} many times!", increaseCounter));
            return increaseCounter + string.Empty;
        }

        internal override string Puzzle2(int[] input)
        {
            int increaseCounter = 0;

            for (int i = 3; i < input.Length; i++)
            {
                int a = input[i - 3] + input[i - 2] + input[i - 1];
                int b = input[i - 2] + input[i - 1] + input[i];
                if (a < b)
                {
                    increaseCounter++;
                }
            }

            SharpLog.Logging.LogDebug(string.Format("The ocean floor increases {0} many times!", increaseCounter));
            return string.Empty + increaseCounter;
        }
    }
}
