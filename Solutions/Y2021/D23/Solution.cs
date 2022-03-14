using AdventOfCode.Common;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Y2021.D23
{
    internal class Solution : Solution<char[,]>
    {
        private static readonly (byte, byte) AGoal = (3, 2);
        private static readonly (byte, byte) BGoal = (5, 2);
        private static readonly (byte, byte) CGoal = (7, 2);
        private static readonly (byte, byte) DGoal = (9, 2);

        private static readonly Dictionary<char, (byte, byte)> GoalPos = new Dictionary<char, (byte, byte)>
        {
            {'A', AGoal },
            {'B', BGoal },
            {'C', CGoal },
            {'D', DGoal },
        };

        private static readonly Dictionary<char, ushort> Costs = new Dictionary<char, ushort>
        {
            {'A', 1 },
            {'B', 10 },
            {'C', 100 },
            {'D', 1000 },
        };

        private readonly (byte, byte)[] Positions = new (byte, byte)[]
        {
            (1, 1),
            (2, 1),
            (4, 1),
            (6, 1),
            (8, 1),
            (10, 1),
            (11, 1),
        };

        internal override string Puzzle1(char[,] input)
        {
            return base.Puzzle1(input);
        }
    }
}
