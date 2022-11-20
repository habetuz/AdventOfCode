namespace AdventOfCode.Solutions.Y2021.D07
{
    using AdventOfCode.Common;
    using System;

    internal class Solution : Solution<int[]>
    {
        internal override (object, string) Puzzle1(int[] input)
        {
            Array.Sort(input);

            int fuel = 0;
            int alignPosition = input.Length % 2 == 0 ? input[input.Length / 2] : (input[input.Length / 2] + input[(input.Length / 2) + 1]) / 2;
            SharpLog.Logging.LogDebug($"The crabs need to align at position {alignPosition}");

            foreach (int crab in input)
            {
                fuel += Math.Abs(crab - alignPosition);
            }

            return (fuel.ToString(), $"The crabs need {fuel} fuel!");
        }

        internal override (object, string) Puzzle2(int[] input)
        {
            int fuelLeft = 0;
            int fuelRight = 0;
            int fuelMiddle = 0;
            int alignPosition = 0;
            foreach (int crab in input)
            {
                alignPosition += crab;
            }

            alignPosition = (int)Math.Round((double)alignPosition / (double)input.Length);

            foreach (int crab in input)
            {
                for (int i = Math.Abs(crab - alignPosition); i > 0; i--)
                {
                    fuelMiddle += i;
                }

                for (int i = Math.Abs(crab - alignPosition - 1); i > 0; i--)
                {
                    fuelLeft += i;
                }

                for (int i = Math.Abs(crab - alignPosition + 1); i > 0; i--)
                {
                    fuelRight += i;
                }
            }

            int fuel = fuelLeft < fuelMiddle ? fuelLeft : fuelMiddle;
            fuel = fuelRight < fuel ? fuelRight : fuel;

            return (fuel.ToString(), $"The crabs need {fuel} fuel!");
        }
    }
}
