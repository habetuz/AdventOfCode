namespace AdventOfCode.Solutions.Y2022.D08
{
    using AdventOfCode.Common;
    using SharpLog;

    internal class Solution : Solution<byte[,]>
    {
        internal override (object clipboard, string message) Puzzle1(byte[,] input)
        {
            var isVisible = new bool[input.GetLength(0), input.GetLength(1)];

            byte maxHeight = 0;

            for (int x = 1; x < input.GetLength(0) - 1; x++)
            {
                maxHeight = input[x, 0];

                for (int y = 1; y < input.GetLength(1) - 1; y++)
                {
                    if (input[x, y] > maxHeight)
                    {
                        maxHeight = input[x, y];
                        isVisible[x, y] = true;
                    }
                }

                maxHeight = input[x, input.GetLength(1) - 1];

                for (int y = input.GetLength(1) - 2; y > 0; y--)
                {
                    if (input[x, y] > maxHeight)
                    {
                        maxHeight = input[x, y];
                        isVisible[x, y] = true;
                    }
                }
            }

            for (int y = 1; y < input.GetLength(1) - 1; y++)
            {
                maxHeight = input[0, y];

                for (int x = 1; x < input.GetLength(0) - 1; x++)
                {
                    if (input[x, y] > maxHeight)
                    {
                        maxHeight = input[x, y];
                        isVisible[x, y] = true;
                    }
                }

                maxHeight = input[input.GetLength(0) - 1, y];

                for (int x = input.GetLength(0) - 2; x > 0; x--)
                {
                    if (input[x, y] > maxHeight)
                    {
                        maxHeight = input[x, y];
                        isVisible[x, y] = true;
                    }
                }
            }

            int visibilityCounter = (isVisible.GetLength(0) * 4) - 4;

            for (int y = 1; y < isVisible.GetLength(1) - 1; y++)
            {
                for (int x = 1; x < isVisible.GetLength(0) - 1; x++)
                {
                    if (isVisible[x, y])
                    {
                        visibilityCounter++;
                    }
                }
            }

            return (visibilityCounter, $"There are [yellow]{visibilityCounter}[/] visible trees!");
        }

        internal override (object clipboard, string message) Puzzle2(byte[,] input)
        {
            int bestScore = 0;

            for (int y = 1; y < input.GetLength(0) - 1; y++)
            {
                for (int x = 1; x < input.GetLength(1) - 1; x++)
                {
                    int scenicScore = 1;
                    int brancheScore = 0;

                    for (int dx = x + 1; dx < input.GetLength(0); dx++)
                    {
                        brancheScore++;
                        if (input[dx, y] >= input[x, y])
                        {
                            break;
                        }
                    }

                    scenicScore *= brancheScore;
                    brancheScore = 0;

                    for (int dx = x - 1; dx >= 0; dx--)
                    {
                        brancheScore++;
                        if (input[dx, y] >= input[x, y])
                        {
                            break;
                        }
                    }

                    scenicScore *= brancheScore;
                    brancheScore = 0;

                    for (int dy = y + 1; dy < input.GetLength(1); dy++)
                    {
                        brancheScore++;
                        if (input[x, dy] >= input[x, y])
                        {
                            break;
                        }
                    }

                    scenicScore *= brancheScore;
                    brancheScore = 0;

                    for (int dy = y - 1; dy >= 0; dy--)
                    {
                        brancheScore++;
                        if (input[x, dy] >= input[x, y])
                        {
                            break;
                        }
                    }

                    scenicScore *= brancheScore;

                    if (scenicScore > bestScore)
                    {
                        bestScore = scenicScore;
                    }
                }
            }

            return (bestScore, $"The best scenic score is [yellow]{bestScore}[/]");
        }
    }
}
