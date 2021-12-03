using AdventOfCode.Common;
using SharpLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D03
{
    internal class Parser : Parser<int[]>
    {
        internal override int[] Parse(string input)
        {
            string[] lines = input.Split('\n');

            List<int> inputArray = new List<int>();

            s_progressTracker = new ProgressTracker(lines.Length - 1, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            // Parsing
            for (int i = 0; i < lines.Length; i++)
            {
                int number;
                int.TryParse(lines[i], out number);
                inputArray.Add(number);
                s_progressTracker.CurrentStep = i;
            }

            return inputArray.ToArray();
        }
    }
}
