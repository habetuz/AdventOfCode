namespace AdventOfCode.Solutions.Y2022.D03
{
    using AdventOfCode.Common;

    internal class Solution : Solution<string[]>
    {
        internal override (object clipboard, string message) Puzzle1(string[] input)
        {
            var score = 0;

            for (int i = 0; i < input.Length; i++)
            {
                var compartmentA = input[i].Substring(input[i].Length / 2);
                var compartmentB = input[i].Substring(0, input[i].Length / 2);

                for (int c = 0; c < compartmentA.Length; c++)
                {
                    if (compartmentB.Contains(compartmentA[c].ToString()))
                    {
                        if (compartmentA[c] < 'a')
                        {
                            score += compartmentA[c] - 'A' + 27;
                        }
                        else
                        {
                            score += compartmentA[c] - 'a' + 1;
                        }

                        break;
                    }
                }
            }

            return (score, $"The sum of the priorities is {score}!");
        }

        internal override (object clipboard, string message) Puzzle2(string[] input)
        {
            var score = 0;

            for (int i = 0; i < input.Length; i += 3)
            {
                string comparer = null;

                byte[] compareTo = null;

                if (input[i].Length <= input[i + 1].Length && input[i].Length <= input[i + 2].Length)
                {
                    comparer = input[i];
                    compareTo = new byte[] { 1, 2 };
                }
                else if (input[i + 1].Length <= input[i].Length && input[i + 1].Length <= input[i + 2].Length)
                {
                    comparer = input[i + 1];
                    compareTo = new byte[] { 0, 2 };
                }
                else if (input[i + 2].Length <= input[i].Length && input[i + 2].Length <= input[i + 1].Length)
                {
                    comparer = input[i + 2];
                    compareTo = new byte[] { 0, 1 };
                }

                for (int c = 0; c < comparer.Length; c++)
                {
                    if (input[i + compareTo[0]].Contains(comparer[c].ToString()) &&
                        input[i + compareTo[1]].Contains(comparer[c].ToString()))
                    {
                        if (comparer[c] < 'a')
                        {
                            score += comparer[c] - 'A' + 27;
                        }
                        else
                        {
                            score += comparer[c] - 'a' + 1;
                        }

                        break;
                    }
                }
            }

            return (score, $"The sum of the priorities is {score}!");
        }
    }
}
