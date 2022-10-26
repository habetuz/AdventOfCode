namespace AdventOfCode.Solutions.Y2021.D18
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Common;

    internal class Parser : Parser<SnailfishNumber[]>
    {
        internal override SnailfishNumber[] Parse(string input)
        {
            string[] lines = input.Split('\n');

            List<SnailfishNumber> snailfishNumbers = new List<SnailfishNumber>();

            foreach (string line in lines)
            {
                snailfishNumbers.Add(this.RecursiveParsing(line));
            }

            return snailfishNumbers.ToArray();
        }

        private SnailfishNumber RecursiveParsing(string numberString)
        {
            return this.RecursiveParsing(numberString, 0).Item1;
        }

        private (SnailfishNumber, int) RecursiveParsing(string numberString, int index)
        {
            if (numberString[index] == '[')
            {
                index++;
                Pair number = new Pair();
                (number.Left, index) = this.RecursiveParsing(numberString, index);
                (number.Right, index) = this.RecursiveParsing(numberString, index + 1);

                return (number, index + 1);
            }
            else
            {
                LiteralNumber number = new LiteralNumber();

                number.Value = int.Parse(numberString[index].ToString());
                return (number, index + 1);
            }
        }
    }
}
