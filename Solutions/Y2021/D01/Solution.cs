using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D01
{
    internal class Solution : Solution<int[]>
    {
        internal override string Puzzle1(int[] input)
        {
            s_progressTracker = new ProgressTracker(input.Length - 1, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            int increaseCounter = 0;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i - 1] < input[i])
                {
                    increaseCounter++;
                }

                s_progressTracker.CurrentStep = i;
            }

            s_logger.Log(string.Format("The ocean floor increases {0} many times!", increaseCounter), LogType.Info);
            return increaseCounter + string.Empty;
        }

        internal override string Puzzle2(int[] input)
        {
            s_progressTracker = new ProgressTracker(input.Length - 1, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            int increaseCounter = 0;

            for (int i = 3; i < input.Length; i++)
            {
                int a = input[i - 3] + input[i - 2] + input[i - 1];
                int b = input[i - 2] + input[i - 1] + input[i];
                if (a < b)
                {
                    increaseCounter++;
                }

                s_progressTracker.CurrentStep = i;
            }

            s_logger.Log(string.Format("The ocean floor increases {0} many times!", increaseCounter), LogType.Info);
            return string.Empty + increaseCounter;
        }
    }
}
