// <copyright file="LiteralNumber.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D18
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class LiteralNumber : SnailfishNumber
    {
        internal int Value { get; set; }

        internal override SnailfishNumber Copy()
        {
            return new LiteralNumber()
            {
                Value = this.Value,
            };
        }
    }
}
