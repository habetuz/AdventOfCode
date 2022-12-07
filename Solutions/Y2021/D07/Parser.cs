namespace AdventOfCode.Solutions.Y2021.D10
{
    using System;
    using AdventOfCode.Common;

    internal class Parser : Parser<string[]>
    {
        internal override string[] Parse(string input)
        {
            string[] values = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            return values;
        }
    }
}
