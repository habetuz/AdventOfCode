using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D06
{
    internal class Parser : Parser<int[]>
    {
        internal override int[] Parse(string input)
        {
            string[] values = input.Split(',');

            s_progressTracker = new ProgressTracker(values.Length - 1, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            List<int> startValues = new List<int>();

            foreach (string value in values)
            {
                startValues.Add(int.Parse(value));

                s_progressTracker.CurrentStep++;
            }

            s_logger.Log($"There are {startValues.Count} fishes!", LogType.Info);

            return startValues.ToArray();
        }
    }
}
