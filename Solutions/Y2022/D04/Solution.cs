namespace AdventOfCode.Solutions.Y2022.D04
{
    using AdventOfCode.Common;

    internal class Solution : Solution<((byte, byte), (byte, byte))[]>
    {
        internal override (object clipboard, string message) Puzzle1(((byte, byte), (byte, byte))[] input)
        {
            uint containing = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i].Item1.Item1 >= input[i].Item2.Item1 &&
                    input[i].Item1.Item2 <= input[i].Item2.Item2) ||
                    (input[i].Item2.Item1 >= input[i].Item1.Item1 &&
                    input[i].Item2.Item2 <= input[i].Item1.Item2))
                {
                    containing++;
                }
            }

            return (containing, $"There are {containing} fully contained ranges.");
        }

        internal override (object clipboard, string message) Puzzle2(((byte, byte), (byte, byte))[] input)
        {
            uint overlaping = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i].Item1.Item1 >= input[i].Item2.Item1 &&
                    input[i].Item1.Item1 <= input[i].Item2.Item2) ||
                    (input[i].Item1.Item2 >= input[i].Item2.Item1 &&
                    input[i].Item1.Item2 <= input[i].Item2.Item2) ||
                    (input[i].Item2.Item1 >= input[i].Item1.Item1 &&
                    input[i].Item2.Item1 <= input[i].Item1.Item2) ||
                    (input[i].Item2.Item2 >= input[i].Item1.Item1 &&
                    input[i].Item2.Item2 <= input[i].Item1.Item2))
                {
                    overlaping++;
                }
            }

            return (overlaping, $"There are {overlaping} overlaping pairs.");
        }
    }
}
