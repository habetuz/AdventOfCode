using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D18
{
    internal class LiteralNumber : SnailfishNumber
    {
        internal int Value;

        internal override SnailfishNumber Copy()
        {
            return new LiteralNumber()
            {
                Value = this.Value,
            };
        }
    }
}
