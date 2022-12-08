namespace AdventOfCode.Solutions.Y2022.D07
{
    using AdventOfCode.Common;

    internal class Solution : Solution<Directory>
    {
        internal override (object clipboard, string message) Puzzle1(Directory input)
        {
            uint combinedSize = 0;
            input.GetCombinedSizeOfDirectories(ref combinedSize, 100_000);

            return (combinedSize, $"The combined size of directories with a size under [grey]100000[/] is [yellow]{combinedSize}[/]!");
        }

        internal override (object clipboard, string message) Puzzle2(Directory input)
        {
            uint closestSize = uint.MaxValue;
            uint goalSize = 30_000_000 - (70_000_000 - input.Size);
            input.GetClosestToSize(ref closestSize, goalSize);

            return (closestSize, $"The smallest directory you could delete has the size [yellow]{closestSize}[/]!");
        }
    }
}
