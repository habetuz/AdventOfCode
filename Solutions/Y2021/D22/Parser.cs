namespace AdventOfCode.Solutions.Y2021.D22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Common;

    internal class Parser : Parser<(bool, (int, int, int), (int, int, int))[]>
    {
        internal override (bool, (int, int, int), (int, int, int))[] Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            (bool, (int, int, int), (int, int, int))[] instructions = new (bool, (int, int, int), (int, int, int))[lines.Length];

            for (int i = 0; i < instructions.Length; i++)
            {
                string line = lines[i];
                bool on = line[1] == 'n' ? true : false;

                string[] parts = line.Split(new string[] { "=", "..", "," }, StringSplitOptions.None);

                (int, int, int) from = (int.Parse(parts[1]), int.Parse(parts[4]), int.Parse(parts[7]));
                (int, int, int) to = (int.Parse(parts[2]), int.Parse(parts[5]), int.Parse(parts[8]));

                instructions[i] = (on, from, to);
            }

            return instructions;
        }
    }
}
