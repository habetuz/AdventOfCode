namespace AdventOfCode.Solutions.Y2021.D17
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Common;

    internal class Parser : Parser<Target>
    {
        internal override Target Parse(string input)
        {
            input = input.Substring(13);
            string[] coordninates = input.Split(new string[] { ", ", "=", ".." }, StringSplitOptions.None);

            return new Target(
                int.Parse(coordninates[1]),
                int.Parse(coordninates[4]),
                int.Parse(coordninates[2]) - int.Parse(coordninates[1]),
                int.Parse(coordninates[5]) - int.Parse(coordninates[4]));
        }
    }
}
