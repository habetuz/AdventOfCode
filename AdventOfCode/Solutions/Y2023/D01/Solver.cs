using System;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using SharpLog;

namespace AdventOfCode.Solutions.Y2023.D01;

public class Solver : ISolver<string[]>
{
    private static string[] numbers = new string[]
    {
        "zero",
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine"
    };

    public void Parse(string input, IPartSubmitter<string[]> partSubmitter)
    {
        partSubmitter.Submit(input.Split((char[])['\n'], StringSplitOptions.RemoveEmptyEntries));
    }

    public void Solve(string[] input, IPartSubmitter partSubmitter)
    {
        int sum1 = 0;
        int sum2 = 0;

        for (int i = 0; i < input.Length; i++)
        {
            var line = input[i];
            int number1a = -1;
            int number1b = -1;
            int number2a = -1;
            int number2b = -1;
            for (int k = 0; k < line.Length && (number1a == -1 || number2a == -1); k++)
            {
                if (line[k] >= '1' && line[k] <= '9')
                {
                    if (number1a == -1)
                    {
                        number1a = line[k] - '0';
                    }

                    if (number2a == -1)
                    {
                        number2a = line[k] - '0';
                    }

                    continue;
                }

                if (number2a != -1)
                {
                    continue;
                }

                for (int y = 1; y < numbers.Length; y++)
                {
                    if (line[k..].StartsWith(numbers[y]))
                    {
                        number2a = y;

                        k += numbers[y].Length - 1;
                        break;
                    }
                }
            }

            for (int k = line.Length - 1; k >= 0 && (number1b == -1 || number2b == -1); k--)
            {
                if (line[k] >= '1' && line[k] <= '9')
                {
                    if (number1b == -1)
                    {
                        number1b = line[k] - '0';
                    }

                    if (number2b == -1)
                    {
                        number2b = line[k] - '0';
                    }

                    continue;
                }

                if (number2b != -1)
                {
                    continue;
                }

                for (int y = 1; y < numbers.Length; y++)
                {
                    if (line[..(k + 1)].EndsWith(numbers[y]))
                    {
                        number2b = y;
                        k -= numbers[y].Length - 1;
                        break;
                    }
                }
            }

            sum1 += (number1a * 10) + number1b;
            sum2 += (number2a * 10) + number2b;
        }

        partSubmitter.SubmitPart1(sum1);
        partSubmitter.SubmitPart2(sum2);
    }
}
