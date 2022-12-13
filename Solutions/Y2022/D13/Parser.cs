namespace AdventOfCode.Solutions.Y2022.D13
{
    using System.Collections.Generic;
    using AdventOfCode.Common;

    internal class Parser : Parser<string[]>
    {
        internal override string[] Parse(string input)
        {
            return input.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
