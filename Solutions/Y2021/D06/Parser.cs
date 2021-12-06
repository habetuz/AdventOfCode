using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D06
{
    internal class Parser : Parser<List<Lanternfish>>
    {
        internal override List<Lanternfish> Parse(string input)
        {
            string[] values = input.Split(',');

            s_progressTracker = new ProgressTracker(values.Length - 1, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            List<Lanternfish> fishes = new List<Lanternfish>();

            foreach (string value in values)
            {
                fishes.Add(new Lanternfish(int.Parse(value)));

                s_progressTracker.CurrentStep++;
            }

            s_logger.Log($"There are {fishes.Count} fishes!", LogType.Info);

            return fishes;
        }
    }
}
