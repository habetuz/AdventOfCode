using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using SharpLog;

namespace AdventOfCode.Solutions.Y2021.D10
{
    internal class Parser : Parser<string[]>
    {
        internal override string[] Parse(string input)
        {
            string[] values = input.Split(new char[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);

            return values;
        }
    }
}
