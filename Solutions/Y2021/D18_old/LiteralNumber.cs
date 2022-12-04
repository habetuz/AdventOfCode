// <copyright file="LiteralNumber.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D18_old
{
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
