namespace AdventOfCode.Solutions.Y2022.D09
{
    using System;
    using System.Collections.Generic;
    using AdventOfCode.Common;

    internal class Solution : Solution<(char, int)[]>
    {
        internal override (object clipboard, string message) Puzzle1((char, int)[] input)
        {
            HashSet<(int, int)> tailTrail = new HashSet<(int, int)>();

            tailTrail.Add((0, 0));

            var head = (0, 0);
            var tail = (0, 0);

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Item2; j++)
                {
                    var lastHead = head;

                    switch (input[i].Item1)
                    {
                        case 'U':
                            head.Item2++;
                            break;
                        case 'D':
                            head.Item2--;
                            break;
                        case 'L':
                            head.Item1--;
                            break;
                        case 'R':
                            head.Item1++;
                            break;
                    }

                    var dx = head.Item1 - tail.Item1;
                    var dy = head.Item2 - tail.Item2;
                    if (dx * dx > 1 || dy * dy > 1)
                    {
                        tail = lastHead;
                        tailTrail.Add(tail);
                    }
                }
            }

            var trailCount = tailTrail.Count;

            return (trailCount, $"The tail visited [yellow]{trailCount}[/] fields!");
        }

        internal override (object clipboard, string message) Puzzle2((char, int)[] input)
        {
            HashSet<(int, int)> tailTrail = new HashSet<(int, int)>();

            var rope = new (int, int)[10];

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Item2; j++)
                {
                    switch (input[i].Item1)
                    {
                        case 'U':
                            rope[0].Item2++;
                            break;
                        case 'D':
                            rope[0].Item2--;
                            break;
                        case 'L':
                            rope[0].Item1--;
                            break;
                        case 'R':
                            rope[0].Item1++;
                            break;
                    }

                    for (int k = 1; k < rope.Length; k++)
                    {
                        var dx = rope[k - 1].Item1 - rope[k].Item1;
                        var dy = rope[k - 1].Item2 - rope[k].Item2;
                        if (dx * dx > 1 || dy * dy > 1)
                        {
                            rope[k].Item1 += Math.Sign(dx);
                            rope[k].Item2 += Math.Sign(dy);
                        }
                    }

                    tailTrail.Add(rope[9]);
                }
            }

            var trailCount = tailTrail.Count;

            return (trailCount, $"The tail visited [yellow]{trailCount}[/] fields!");
        }
    }
}
