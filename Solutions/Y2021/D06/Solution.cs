namespace AdventOfCode.Solutions.Y2021.D06
{
    using AdventOfCode.Common;

    internal class Solution : Solution<int[]>
    {
        private readonly long[] fishCounter = new long[256 + 10];

        internal override (object, string) Puzzle1(int[] input)
        {
            this.fishCounter[0] = input.Length;
            foreach (int fish in input)
            {
                this.fishCounter[fish + 1]++;
            }

            for (int day = 1; day <= 80; day++)
            {
                this.fishCounter[day + 7] += this.fishCounter[day];
                this.fishCounter[day + 9] += this.fishCounter[day];
                this.fishCounter[day] += this.fishCounter[day - 1];
                //// SharpLog.Logging.LogDebug($"After {day:D2} days there are {_fishCounter[day]} fish.");
            }

            return (this.fishCounter[80].ToString(), $"After 80 days there will be {this.fishCounter[80]} laternfish!");
        }

        internal override (object, string) Puzzle2(int[] input)
        {
            for (int day = 81; day <= 256; day++)
            {
                this.fishCounter[day + 7] += this.fishCounter[day];
                this.fishCounter[day + 9] += this.fishCounter[day];
                this.fishCounter[day] += this.fishCounter[day - 1];
            }

            return (this.fishCounter[80].ToString(), $"After 256 days there will be {this.fishCounter[80]} laternfish!");
        }
    }
}
