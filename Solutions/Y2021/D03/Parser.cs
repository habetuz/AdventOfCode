using AdventOfCode.Common;
using SharpLog;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Y2021.D03
{
    internal class Parser : Parser<int[][]>
    {
        internal override int[][] Parse(string input)
        {
            // Split file into lines
            string[] lines = input.Split('\n');

            s_progressTracker = new ProgressTracker(lines.Length, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            // Parsing to integer
            List<int[]> inputArray = new List<int[]>();
            for (int i = 0; i < lines.Length - 1; i++)
            {
                int[] bits = new int[lines[0].Length];

                for (int bit = 0; bit < lines[0].Length; bit++)
                {
                    bits[bit] = lines[i][bit] - '0';
                    s_logger.Log(string.Format("{2}:{3} | From {0} to {1}", lines[i][bit], bits[bit], i, bit));
                }

                inputArray.Add(bits);

                s_progressTracker.CurrentStep = i;
            }

            return inputArray.ToArray();
        }
    }
}
