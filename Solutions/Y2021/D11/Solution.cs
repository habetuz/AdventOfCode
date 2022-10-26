namespace AdventOfCode.Solutions.Y2021.D11
{
    using System;
    using AdventOfCode.Common;
    using SharpLog;

    internal class Solution : Solution<int[,]>
    {
        internal override string Puzzle1(int[,] input)
        {
            int flashes = 0;

            for (int i = 1; i <= 100; i++)
            {
                for (int x = 0; x < input.GetLength(0); x++)
                {
                    for (int y = 0; y < input.GetLength(1); y++)
                    {
                        this.IncreaseEnergy(input, x, y, ref flashes);
                    }
                }

                for (int x = 0; x < input.GetLength(0); x++)
                {
                    for (int y = 0; y < input.GetLength(1); y++)
                    {
                        if (input[x, y] > 9)
                        {
                            input[x, y] = 0;
                        }
                    }
                }
                ////SharpLog.Logging.LogDebug($"After step {i}");
                ////Tools.Print2D(input);
            }

            SharpLog.Logging.LogDebug($"There were {flashes} flashes after 100 steps!");
            return flashes.ToString();
        }

        internal override string Puzzle2(int[,] input)
        {
            int syncStep = 0;

            for (int i = 101; true; i++)
            {
                for (int x = 0; x < input.GetLength(0); x++)
                {
                    for (int y = 0; y < input.GetLength(1); y++)
                    {
                        this.IncreaseEnergy(input, x, y);
                    }
                }

                for (int x = 0; x < input.GetLength(0); x++)
                {
                    for (int y = 0; y < input.GetLength(1); y++)
                    {
                        if (input[x, y] > 9)
                        {
                            input[x, y] = 0;
                        }
                    }
                }

                int sum = 0;
                foreach (int value in input)
                {
                    sum += value;
                }

                ////SharpLog.Logging.LogDebug($"After step {i}");
                ////Tools.Print2D(input);

                if (sum == 0)
                {
                    syncStep = i;
                    break;
                }
            }

            SharpLog.Logging.LogDebug($"The octopuses were in sync on step {syncStep}!");
            return syncStep.ToString();
        }

        private void IncreaseEnergy(int[,] input, int x, int y, ref int flashes)
        {
            if (x < 0 || x >= input.GetLength(0) || y < 0 || y >= input.GetLength(1))
            {
                return;
            }

            input[x, y]++;
            if (input[x, y] == 10)
            {
                flashes++;
                this.IncreaseEnergy(input, x - 1, y + 0, ref flashes);
                this.IncreaseEnergy(input, x - 1, y - 1, ref flashes);
                this.IncreaseEnergy(input, x + 0, y - 1, ref flashes);
                this.IncreaseEnergy(input, x + 1, y - 1, ref flashes);
                this.IncreaseEnergy(input, x + 1, y + 0, ref flashes);
                this.IncreaseEnergy(input, x + 1, y + 1, ref flashes);
                this.IncreaseEnergy(input, x + 0, y + 1, ref flashes);
                this.IncreaseEnergy(input, x - 1, y + 1, ref flashes);
            }
        }

        private void IncreaseEnergy(int[,] input, int x, int y)
        {
            if (x < 0 || x >= input.GetLength(0) || y < 0 || y >= input.GetLength(1))
            {
                return;
            }

            input[x, y]++;
            if (input[x, y] == 10)
            {
                this.IncreaseEnergy(input, x - 1, y + 0);
                this.IncreaseEnergy(input, x - 1, y - 1);
                this.IncreaseEnergy(input, x + 0, y - 1);
                this.IncreaseEnergy(input, x + 1, y - 1);
                this.IncreaseEnergy(input, x + 1, y + 0);
                this.IncreaseEnergy(input, x + 1, y + 1);
                this.IncreaseEnergy(input, x + 0, y + 1);
                this.IncreaseEnergy(input, x - 1, y + 1);
            }
        }
    }
}
