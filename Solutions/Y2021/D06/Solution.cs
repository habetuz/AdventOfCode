using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D06
{
    internal class Solution : Solution<int[]>
    {
        private long[] _fishCounter = new long[256 + 10];

        internal override string Puzzle1(int[] input)
        {
            _fishCounter[0] = input.Length;
            foreach (int fish in input)
            {
                _fishCounter[fish + 1]++;
            }

            for (int day = 1; day <= 80; day++)
            {
                _fishCounter[day + 7] += _fishCounter[day];
                _fishCounter[day + 9] += _fishCounter[day];
                _fishCounter[day] += _fishCounter[day - 1];
                //// s_logger.Log($"After {day:D2} days there are {_fishCounter[day]} fish.");
            }

            s_logger.Log($"After 80 days there will be {_fishCounter[80]} laternfish!", LogType.Info);

            return _fishCounter[80].ToString();
        }

        internal override string Puzzle2(int[] input)
        {
            for (int day = 81; day <= 256; day++)
            {
                _fishCounter[day + 7] += _fishCounter[day];
                _fishCounter[day + 9] += _fishCounter[day];
                _fishCounter[day] += _fishCounter[day - 1];
            }

            s_logger.Log($"After 256 days there will be {_fishCounter[256]} laternfish!", LogType.Info);

            return _fishCounter[256].ToString();
        }
    }
}
