namespace AdventOfCode.Solutions.Y2022.D01
{
    using AdventOfCode.Common;

    internal class Solution : Solution<uint[][]>
    {
        internal override (object clipboard, string message) Puzzle1(uint[][] input)
        {
            uint maxCalories = 0;

            for (int i = 0; i < input.Length; i++)
            {
                uint currentCalories = 0;

                for (int j = 0; j < input[i].Length; j++)
                {
                    currentCalories += input[i][j];
                }

                if (currentCalories > maxCalories)
                {
                    maxCalories = currentCalories;
                }
            }

            return (maxCalories, $"The top elve is carrying {maxCalories} calories!");
        }

        internal override (object clipboard, string message) Puzzle2(uint[][] input)
        {
            uint[] topThree = new uint[3];

            for (int i = 0; i < input.Length; i++)
            {
                uint currentCalories = 0;

                for (int j = 0; j < input[i].Length; j++)
                {
                    currentCalories += input[i][j];
                }

                for (int j = 0; j < topThree.Length; j++)
                {
                    if (currentCalories > topThree[j])
                    {
                        var toShift = topThree[j];
                        topThree[j] = currentCalories;

                        for (int k = j + 1; k < topThree.Length; k++)
                        {
                            (toShift, topThree[k]) = (topThree[k], toShift);
                        }

                        break;
                    }
                }
            }

            var topThreeTogether = topThree[0] + topThree[1] + topThree[2];

            return (topThreeTogether, $"The top three elves are carrying {topThreeTogether} calories!");
        }
    }
}
