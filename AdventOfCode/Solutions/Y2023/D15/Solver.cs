using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2023.D15;

public class Solver : ISolver<string[], Instruction[]>
{
    public void Parse(string input, IPartSubmitter<string[], Instruction[]> partSubmitter)
    {
        partSubmitter.SubmitPart1(
            input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        );

        partSubmitter.SubmitPart2(
            input
                .Split(',')
                .Select(
                    (value, index) =>
                    {
                        var parts = value.Split(
                            (char[])['-', '='],
                            StringSplitOptions.RemoveEmptyEntries
                        );
                        return new Instruction
                        {
                            Operation = parts.Length > 1 ? true : false,
                            Data = new Data
                            {
                                Label = parts[0],
                                Strength = parts.Length > 1 ? byte.Parse(parts[1]) : (byte)0
                            }
                        };
                    }
                )
                .ToArray()
        );
    }

    public void Solve(string[] input1, Instruction[] input2, IPartSubmitter partSubmitter)
    {
        int hashes = input1.Sum(part => Hash(part));
        partSubmitter.SubmitPart1(hashes);

        List<Data>[] boxes = new List<Data>[256];

        foreach (Instruction instruction in input2)
        {
            var box = boxes[Hash(instruction.Data.Label)];

            if (box is null)
            {
                boxes[Hash(instruction.Data.Label)] = new List<Data>() { instruction.Data };

                continue;
            }

            var indexOf = box.IndexOf(instruction.Data);
            if (indexOf == -1)
            {
                if (instruction.Operation)
                {
                    box.Add(instruction.Data);
                }
            }
            else
            {
                if (instruction.Operation)
                {
                    box[indexOf] = instruction.Data;
                }
                else
                {
                    box.RemoveAt(indexOf);
                }
            }
        }

        long focusingPower = 0;

        for (int b = 0; b < boxes.Length; b++)
        {
            var box = boxes[b];
            if (box is null)
            {
                continue;
            }

            for (int i = 0; i < box.Count; i++)
            {
                focusingPower += (b + 1) * box[i].Strength * (i + 1);
            }
        }

        partSubmitter.SubmitPart2(focusingPower);
    }

    private byte Hash(string input)
    {
        return (byte)input.Aggregate(0, (hash, value) => (hash + value) * 17 % 256);
    }
}
