namespace AdventOfCode.Solutions.Y2022.D02
{
    using AdventOfCode.Common;

    internal class Solution : Solution<string[]>
    {
        internal override (object clipboard, string message) Puzzle1(string[] input)
        {
            uint score = 0;

            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case "A X":
                        score += 1 + 3;
                        break;
                    case "A Y":
                        score += 2 + 6;
                        break;
                    case "A Z":
                        score += 3 + 0;
                        break;
                    case "B X":
                        score += 1 + 0;
                        break;
                    case "B Y":
                        score += 2 + 3;
                        break;
                    case "B Z":
                        score += 3 + 6;
                        break;
                    case "C X":
                        score += 1 + 6;
                        break;
                    case "C Y":
                        score += 2 + 0;
                        break;
                    case "C Z":
                        score += 3 + 3;
                        break;
                }
            }

            return (score, $"You would score {score}!");
        }

        internal override (object clipboard, string message) Puzzle2(string[] input)
        {
            uint score = 0;

            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case "A X":
                        score += 3 + 0;
                        break;
                    case "A Y":
                        score += 1 + 3;
                        break;
                    case "A Z":
                        score += 2 + 6;
                        break;
                    case "B X":
                        score += 1 + 0;
                        break;
                    case "B Y":
                        score += 2 + 3;
                        break;
                    case "B Z":
                        score += 3 + 6;
                        break;
                    case "C X":
                        score += 2 + 0;
                        break;
                    case "C Y":
                        score += 3 + 3;
                        break;
                    case "C Z":
                        score += 1 + 6;
                        break;
                }
            }

            return (score, $"You would score {score}!");
        }
    }
}
