namespace AdventOfCode.Solutions.Y2022.D11
{
    using System;
    using AdventOfCode.Common;

    internal class Solution : Solution<Monkey[]>
    {
        internal override (object clipboard, string message) Puzzle1(Monkey[] monkeys)
        {
            var monkeysCopy = new Monkey[monkeys.Length];
            for (int i = 0; i < monkeys.Length; i++)
            {
                monkeysCopy[i] = monkeys[i].Clone();
            }

            monkeys = monkeysCopy;

            int[] inspections = new int[monkeys.Length];

            for (int i = 0; i < 20; i++)
            {
                for (int m = 0; m < monkeys.Length; m++)
                {
                    while (monkeys[m].Items.Count > 0)
                    {
                        (var item, var to) = monkeys[m].Inspect();
                        monkeys[to].Items.Enqueue(item);

                        inspections[m]++;
                    }
                }
            }

            Array.Sort(inspections);
            Array.Reverse(inspections);

            return (inspections[0] * inspections[1], $"The monkey business is [yellow]{inspections[0] * inspections[1]}[/]!");
        }

        internal override (object clipboard, string message) Puzzle2(Monkey[] monkeys)
        {
            int[] inspections = new int[monkeys.Length];

            var dividends = new int[monkeys.Length];

            for (int i = 0; i < monkeys.Length; i++)
            {
                dividends[i] = monkeys[i].Dividend;
            }

            Array.Sort(dividends);
            Array.Reverse(dividends);

            for (int i = 0; i < 10000; i++)
            {
                for (int m = 0; m < monkeys.Length; m++)
                {
                    while (monkeys[m].Items.Count > 0)
                    {
                        (var item, var to) = monkeys[m].InspectHard(dividends);
                        monkeys[to].Items.Enqueue(item);

                        inspections[m]++;
                    }
                }
            }

            Array.Sort(inspections);
            Array.Reverse(inspections);

            return (inspections[0] * inspections[1], $"The monkey business is [yellow]{inspections[0] * inspections[1]}[/]!");
        }
    }
}
