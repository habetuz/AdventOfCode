using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D22
{

    internal class Solution : Solution<(bool, (int, int, int), (int, int, int))[]>
    {
        const int HalfMax = int.MaxValue / 2;

        internal override string Puzzle1((bool, (int, int, int), (int, int, int))[] input)
        {
            bool[,,] reactor = new bool[int.MaxValue, int.MaxValue, int.MaxValue];

            return base.Puzzle1(input);
        }

        private bool GetValue(int x, int y, int z, bool[,,] reactor)
        {
            return reactor[HalfMax + x, HalfMax + y, HalfMax + z];
        }

        private void SetValue(bool value, int x, int y, int z, ref bool[,,] reactor)
        {
            reactor[HalfMax + x, HalfMax + y, HalfMax + z] = value;
        }
    }
}
