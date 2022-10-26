namespace AdventOfCode.Solutions.Y2021.D24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Common;

    internal class Solution : Solution<Instruction[]>
    {
        private readonly Dictionary<char, int> memory = new Dictionary<char, int>()
        {
            { 'x', 0 },
            { 'y', 0 },
            { 'z', 0 },
            { 'w', 0 },
        };

        internal override (object, string) Puzzle1(Instruction[] input)
        {
            var numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < numbers.Length; i++)
            {
                int num = numbers[i];
                this.memory['z'] = 429;
                this.Run(input, new int[] { num });

                numbers[i] = this.memory['z'];

                SharpLog.Logging.LogDebug($"{num} -> {numbers[i]}");
            }

            return base.Puzzle1(input);
        }

        private void Run(Instruction[] instructions, int[] inputs)
        {
            byte inputIndex = 0;

            for (int i = 0; i < instructions.Length; i++)
            {
                var instruction = instructions[i];

                int value = instruction.Value == 'I' ? instruction.IntegerValue : this.memory[instruction.Value];

                switch (instruction.Operation)
                {
                    case "inp":
                        this.memory[instruction.Result] = inputs[inputIndex];
                        inputIndex++;
                        break;

                    case "add":
                        this.memory[instruction.Result] = this.memory[instruction.Result] + value;
                        break;
                    case "mul":
                        this.memory[instruction.Result] = this.memory[instruction.Result] * value;
                        break;
                    case "div":
                        this.memory[instruction.Result] = this.memory[instruction.Result] / value;
                        break;
                    case "mod":
                        this.memory[instruction.Result] = this.memory[instruction.Result] % value;
                        break;
                    case "eql":
                        this.memory[instruction.Result] = this.memory[instruction.Result] == value ? 1 : 0;
                        break;
                }
            }
        }
    }
}
