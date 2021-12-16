using AdventOfCode.Common;
using SharpLog;
using System.Collections.Generic;
using static AdventOfCode.Solutions.Y2021.D02.Parser;

namespace AdventOfCode.Solutions.Y2021.D02
{
    internal class Solution : Solution<KeyValuePair<Direction, int>[]>
    {
        internal override string Puzzle1(KeyValuePair<Direction, int>[] input)
        {
            // Solution here
            int posHorizontal = 0;
            int posVertical = 0;
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i].Key)
                {
                    case Direction.Forward: posHorizontal += input[i].Value; break;
                    case Direction.Up: posVertical -= input[i].Value; break;
                    case Direction.Down: posVertical += input[i].Value; break;
                }
            }

            s_logger.Log(string.Format(
                "The submarine has the horizontal position {0} and is at depth {1}. The solution is {2}!",
                posHorizontal,
                posVertical,
                posHorizontal * posVertical), LogType.Info);

            return string.Empty + (posHorizontal * posVertical);
        }

        internal override string Puzzle2(KeyValuePair<Direction, int>[] input)
        {
            // Solution here
            int posHorizontal = 0;
            int posVertical = 0;
            int aim = 0;
            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i].Key)
                {
                    case Direction.Forward:
                        posHorizontal += input[i].Value;
                        posVertical += input[i].Value * aim;
                        break;
                    case Direction.Up: aim -= input[i].Value; break;
                    case Direction.Down: aim += input[i].Value; break;
                }
            }

            s_logger.Log(string.Format(
                "The submarine has the horizontal position {0} and is at depth {1}. The solution is {2}!",
                posHorizontal,
                posVertical,
                posHorizontal * posVertical), LogType.Info);

            return string.Empty + (posHorizontal * posVertical);
        }
    }
}
