// <copyright file="Pair.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D18
{
    internal class Pair : SnailfishNumber
    {
        internal SnailfishNumber Left { get; set; }

        internal SnailfishNumber Right { get; set; }

        internal override SnailfishNumber Copy()
        {
            return new Pair()
            {
                Left = this.Left.Copy(),
                Right = this.Right.Copy(),
            };
        }
    }
}
