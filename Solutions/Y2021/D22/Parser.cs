using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D22
{
    internal class Parser : Parser<(bool, (int, int, int), (int, int, int))[]>
    {
        internal override (bool, (int, int, int), (int, int, int))[] Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            (bool, (int, int, int), (int, int, int))[] instructions = new (bool, (int, int, int), (int, int, int))[lines.Length];

            for(int i = 0; i < instructions.Length; i++)
            {
                string line = lines[i];
                bool on = line[2] == 'n'? true : false;

                string[] parts = line.Split(new string[] { "=", "..", "," }, StringSplitOptions.None);

                (int, int, int) from = (Int32.Parse(parts[1]), Int32.Parse(parts[4]), Int32.Parse(parts[7]));
                (int, int, int) to = (Int32.Parse(parts[2]), Int32.Parse(parts[5]), Int32.Parse(parts[8]));

                instructions[i] = (on, from, to);
            }

            return instructions;
        }
    }
}
