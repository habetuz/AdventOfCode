namespace AdventOfCode.Solutions.Y2022.D02
{
    using System.Collections.Generic;
    using AdventOfCode.Common;

    internal class Parser : Parser<string[]>
    {
        internal override string[] Parse(string input)
        {
            return input.Split('\n');
        }
    }
}
