namespace AdventOfCode.Solutions.Y2022.D10
{
    using AdventOfCode.Common;

    internal class Solution : Solution<(string, int)[]>
    {
        internal override (object clipboard, string message) Puzzle1((string, int)[] input)
        {
            int cycleCounter = 0;
            int xRegister = 1;
            int combinedSignalStrength = 0;

            for (int i = 0; i < input.Length; i++)
            {
                cycleCounter++;
                if (cycleCounter == 20 || cycleCounter == 60 || cycleCounter == 100 || cycleCounter == 140 || cycleCounter == 180 || cycleCounter == 220)
                {
                    combinedSignalStrength += cycleCounter * xRegister;
                }

                if (input[i].Item1 == "addx")
                {
                    cycleCounter++;
                    if (cycleCounter == 20 || cycleCounter == 60 || cycleCounter == 100 || cycleCounter == 140 || cycleCounter == 180 || cycleCounter == 220)
                    {
                        combinedSignalStrength += cycleCounter * xRegister;
                    }

                    xRegister += input[i].Item2;
                }
            }

            return (combinedSignalStrength, $"The combined signal strength is [yellow]{combinedSignalStrength}[/]!");
        }

        internal override (object clipboard, string message) Puzzle2((string, int)[] input)
        {
            int xRegister = 1;
            int yIndex = 0;
            int xIndex = 0;

            bool[,] display = new bool[40, 6];

            for (int i = 0; i < input.Length; i++)
            {
                if (xIndex >= xRegister - 1 && xIndex <= xRegister + 1)
                {
                    display[xIndex, yIndex] = true;
                }

                xIndex++;
                if (xIndex % 40 == 0)
                {
                    yIndex++;
                    xIndex = 0;
                }

                if (input[i].Item1 == "addx")
                {
                    if (xIndex >= xRegister - 1 && xIndex <= xRegister + 1)
                    {
                        display[xIndex, yIndex] = true;
                    }

                    xIndex++;
                    if (xIndex % 40 == 0)
                    {
                        yIndex++;
                        xIndex = 0;
                    }

                    xRegister += input[i].Item2;
                }
            }

            Spectre.Console.AnsiConsole.MarkupLine($"[yellow]{Tools.Format(display)}[/]");

            return (null, "Look at the image above for your solution!");
        }
    }
}
