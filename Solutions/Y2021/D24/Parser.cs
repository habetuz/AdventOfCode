namespace AdventOfCode.Solutions.Y2021.D24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Common;

    internal class Parser : Parser<Instruction[]>
    {
        internal override Instruction[] Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            Instruction[] instructions = new Instruction[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] operation = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                instructions[i] = new Instruction()
                {
                    Operation = operation[0],
                    Result = operation[1][0],
                    Value = 'I',
                };

                if (operation.Length >= 3)
                {
                    instructions[i].Value = int.TryParse(operation[2], out instructions[i].IntegerValue) ? 'I' : operation[2][0];
                }
            }

            return instructions;
        }
    }
}
