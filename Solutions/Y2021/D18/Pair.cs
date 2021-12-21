using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D18
{
    internal class Pair : SnailfishNumber
    {
        internal SnailfishNumber Left;
        internal SnailfishNumber Right;

        internal override SnailfishNumber Copy()
        {
            return new Pair()
            {
                Left = Left.Copy(),
                Right = Right.Copy(),
            };
        }
    }
}
