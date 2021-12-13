using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D09
{
    internal class Parser : Parser<int[,]>
    {
        internal override int[,] Parse(string input)
        {
            string[] values = input.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);

            s_progressTracker = new ProgressTracker(values.Length, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            int[,] output = new int[values[0].Length, values.Length];

            for (int y = 0; y < output.GetLength(1); y++)
            {
                for (int x = 0; x < output.GetLength(0); x++)
                {
                    output[x, y] = int.Parse(values[y][x].ToString());
                }

                s_progressTracker.CurrentStep++;
            }

            return output;
        }
    }
}
