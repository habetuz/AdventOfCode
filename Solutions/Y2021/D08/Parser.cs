﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D08
{
    internal class Parser : Parser<Display[]>
    {
        internal override Display[] Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            s_progressTracker = new ProgressTracker(lines.Length - 1, (int progress) =>
            {
                s_logger.Log(ProgressTracker.ProgressToString(progress), LogType.Info);
            });

            List<Display> displays = new List<Display>();

            foreach (string line in lines)
            {
                string[] values = line.Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);
                string[] inputs = values[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] outputs = values[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                displays.Add(new Display(inputs, outputs));

                s_progressTracker.CurrentStep++;
            }

            return displays.ToArray();
        }
    }
}
