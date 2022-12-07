namespace AdventOfCode.Solutions.Y2021.D21
{
    using System.Linq;
    using AdventOfCode.Common;

    internal class Parser : Parser<(int, int)>
    {
        internal override (int, int) Parse(string input)
        {
            string[] lines = input.Split('\n');
            (int, int) startingPositions = (
                int.Parse(lines[0].Last().ToString()),
                int.Parse(lines[1].Last().ToString()));
            return startingPositions;
        }
    }
}
