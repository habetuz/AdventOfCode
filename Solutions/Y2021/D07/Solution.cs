using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;

namespace AdventOfCode.Solutions.Y2021.D07
{
    internal class Solution : Solution<int[]>
    {
        internal override string Puzzle1(int[] input)
        { 
            Array.Sort(input);

            int fuel = 0;
            int alignPosition = input.Length %2 == 0? input[input.Length/2] : (input[input.Length / 2] + input[input.Length / 2 + 1])/2;
            s_logger.Log($"The crabs need to align at position {alignPosition}", SharpLog.LogType.Info);


            foreach (int crab in input)
            {
                fuel += Math.Abs(crab - alignPosition);
            }

            s_logger.Log($"The crabs need {fuel} fuel!", SharpLog.LogType.Info);

            return fuel.ToString();
        }

        internal override string Puzzle2(int[] input)
        {
            s_logger.LogDebug = true;

            int fuelLeft = 0;
            int fuelRight = 0;
            int fuelMiddle = 0;
            int alignPosition = 0;
            foreach (int crab in input)
            {
                alignPosition += crab;
            }
            alignPosition = (int) Math.Round((double) alignPosition / (double)input.Length);

            foreach (int crab in input)
            {
                for (int i = Math.Abs(crab - alignPosition); i > 0; i--)
                {
                    fuelMiddle += i;
                }

                for (int i = Math.Abs(crab - alignPosition -1); i > 0; i--)
                {
                    fuelLeft += i;
                }

                for (int i = Math.Abs(crab - alignPosition + 1); i > 0; i--)
                {
                    fuelRight += i;
                }
            }

            int fuel = fuelLeft < fuelMiddle? fuelLeft : fuelMiddle;
            fuel = fuelRight < fuel? fuelRight : fuel;

            s_logger.Log($"The crabs need {fuel} fuel!", SharpLog.LogType.Info);


            return fuel.ToString();
        }
    }
}
