using System.Diagnostics;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using SharpLog;

namespace AdventOfCode.Solutions.Y2023.D14;

public class Solver : ISolver<char[,]>
{
    public void Parse(string input, IPartSubmitter<char[,]> partSubmitter)
    {
        partSubmitter.Submit(Array2D.FromString(input));
    }

    public void Solve(char[,] input, IPartSubmitter partSubmitter)
    {
        int weight = Shift(input, Direction.Up);

        partSubmitter.SubmitPart1(weight);

        Shift(input, Direction.Left);
        Shift(input, Direction.Down);
        Shift(input, Direction.Right);

        Dictionary<int, (int first, int second)> memory = new();

        // Go to ready state
        for (int i = 1; i < 999_999_999; i++)
        {
            Shift(input, Direction.Up);
            Shift(input, Direction.Left);
            Shift(input, Direction.Down);
            weight = Shift(input, Direction.Right);

            if (memory.TryGetValue(weight, out var occurrences))
            {
                if (occurrences.first == -1)
                {
                    memory[weight] = (i, -1);
                }
                else if (occurrences.second == -1)
                {
                    memory[weight] = (occurrences.first, i);
                }
                else if (occurrences.second - occurrences.first == i - occurrences.second)
                {
                    int cycleLength = occurrences.second - occurrences.first;
                    int cycleIndex =
                        (999_999_999 - occurrences.first) % cycleLength + occurrences.first;
                    int cycleWeight = memory.First(pair => pair.Value.first == cycleIndex).Key;

                    partSubmitter.SubmitPart2(cycleWeight);
                    break;
                }
                else
                {
                    memory[weight] = (occurrences.second, i);
                }
            }
            else
            {
                memory.Add(weight, (-1, -1));
            }
        }
    }

    private int Shift(char[,] platform, Direction direction)
    {
        int weight = 0;

        switch (direction)
        {
            case Direction.Up:
                for (int x = 0; x < platform.GetLength(0); x++)
                {
                    byte rockCount = 0;

                    for (int y = platform.GetLength(1) - 1; y >= 0; y--)
                    {
                        if (platform[x, y] == 'O')
                        {
                            rockCount++;
                            platform[x, y] = '.';
                        }
                        else if (platform[x, y] == '#')
                        {
                            for (; rockCount > 0; rockCount--)
                            {
                                platform[x, y + rockCount] = 'O';
                                weight += platform.GetLength(1) - (y + rockCount);
                            }
                        }
                    }

                    for (; rockCount > 0; rockCount--)
                    {
                        platform[x, rockCount - 1] = 'O';
                        weight += platform.GetLength(1) - (rockCount - 1);
                    }
                }
                break;
            case Direction.Down:
                for (int x = 0; x < platform.GetLength(0); x++)
                {
                    byte rockCount = 0;

                    for (int y = 0; y < platform.GetLength(1); y++)
                    {
                        if (platform[x, y] == 'O')
                        {
                            rockCount++;
                            platform[x, y] = '.';
                        }
                        else if (platform[x, y] == '#')
                        {
                            for (; rockCount > 0; rockCount--)
                            {
                                platform[x, y - rockCount] = 'O';
                                weight += y - rockCount;
                            }
                        }
                    }

                    for (; rockCount > 0; rockCount--)
                    {
                        platform[x, platform.GetLength(1) - rockCount] = 'O';
                        weight += platform.GetLength(1) - rockCount;
                    }
                }
                break;
            case Direction.Left:
                for (int y = 0; y < platform.GetLength(1); y++)
                {
                    byte rockCount = 0;

                    for (int x = platform.GetLength(0) - 1; x >= 0; x--)
                    {
                        if (platform[x, y] == 'O')
                        {
                            rockCount++;
                            platform[x, y] = '.';
                        }
                        else if (platform[x, y] == '#')
                        {
                            for (; rockCount > 0; rockCount--)
                            {
                                platform[x + rockCount, y] = 'O';
                                weight += platform.GetLength(1) - y;
                            }
                        }
                    }

                    for (; rockCount > 0; rockCount--)
                    {
                        platform[rockCount - 1, y] = 'O';
                        weight += platform.GetLength(1) - y;
                    }
                }
                break;
            case Direction.Right:
                for (int y = 0; y < platform.GetLength(1); y++)
                {
                    byte rockCount = 0;

                    for (int x = 0; x < platform.GetLength(0); x++)
                    {
                        if (platform[x, y] == 'O')
                        {
                            rockCount++;
                            platform[x, y] = '.';
                        }
                        else if (platform[x, y] == '#')
                        {
                            for (; rockCount > 0; rockCount--)
                            {
                                platform[x - rockCount, y] = 'O';
                                weight += platform.GetLength(1) - y;
                            }
                        }
                    }

                    for (; rockCount > 0; rockCount--)
                    {
                        platform[platform.GetLength(0) - rockCount, y] = 'O';
                        weight += platform.GetLength(1) - y;
                    }
                }
                break;
        }

        return weight;
    }
}
