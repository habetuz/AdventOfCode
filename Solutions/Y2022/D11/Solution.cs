namespace AdventOfCode.Solutions.Y2022.D11
{
    using System;
    using System.Linq;
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
            ulong[] inspections = new ulong[monkeys.Length];

            for (int i = 0; i < monkeys.Length; i++)
            {
                var items = monkeys[i].Items.ToArray();

                for (int j = 0; j < items.Length; j++)
                {
                    var array = Enumerable.Repeat(items[j], monkeys.Length).ToArray();
                    monkeys[i].ItemsCompressed.Enqueue(array);
                }
            }

            for (int i = 0; i < 10000; i++)
            {
                for (int m = 0; m < monkeys.Length; m++)
                {
                    while (monkeys[m].ItemsCompressed.Count > 0)
                    {
                        (var item, var to) = monkeys[m].InspectHard();

                        // Compress
                        for (int j = 0; j < monkeys.Length; j++)
                        {
                            item[j] %= monkeys[j].Dividend;
                        }

                        monkeys[to].ItemsCompressed.Enqueue(item);

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
