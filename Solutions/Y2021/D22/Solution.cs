using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D22
{

    internal class Solution : Solution<(bool, (int, int, int), (int, int, int))[]>
    {
        const int HalfMax = 50;

        const int MaxSize = 2_000_000_000;

        internal override string Puzzle1((bool, (int, int, int), (int, int, int))[] input)
        {
            bool[,,] reactor = new bool[101, 101, 101];

            foreach (var instruction in input)
            {
                for (int x = instruction.Item2.Item1; x <= instruction.Item3.Item1; x++)
                {
                    for (int y = instruction.Item2.Item2; y <= instruction.Item3.Item2; y++)
                    {
                        for (int z = instruction.Item2.Item3; z <= instruction.Item3.Item3; z++)
                        {
                            try
                            {
                                SetValue(instruction.Item1, x, y, z, ref reactor);
                            }
                            catch (IndexOutOfRangeException) 
                            {
                                goto exit;
                            }
                        }
                    }
                }
            }

            exit:
            
            int activeCubes = 0;

            for (int x = -50; x <= 50; x++)
            {
                for (int y = -50; y <= 50; y++)
                {
                    for (int z = -50; z <= 50; z++)
                    {
                        if (GetValue(x, y, z, reactor)) activeCubes++;
                    }
                }
            }

            s_logger.Log($"{activeCubes} cubes are active!", SharpLog.LogType.Info);

            return activeCubes.ToString();
        }

        internal override string Puzzle2((bool, (int, int, int), (int, int, int))[] input)
        {
            s_logger.LogDebug = true;

            long activeCubes = 0;

            List<((int, int, int), (int, int, int))> reactor = new List<((int, int, int), (int, int, int))>();

            for (int i = input.Length - 1; i >= 0; i--)
            {
                var instruction = input[i];

                long volume = UniqueIntersection((instruction.Item2, instruction.Item3), reactor);

                if (volume > 0)
                {
                    reactor.Add((instruction.Item2, instruction.Item3));
                    if (instruction.Item1)
                    {
                        activeCubes += volume;
                    }
                }
            }

            s_logger.Log($"{activeCubes} cubes are active!", SharpLog.LogType.Info);

            return activeCubes.ToString();
        }

        private long UniqueIntersection(((int, int, int), (int, int, int)) intersection, List<((int, int, int), (int, int, int))> toIntersect)
        {
            long volume = Volume(intersection);

            Queue<((int, int, int), (int, int, int))> intersections = new Queue<((int, int, int), (int, int, int))>();

            foreach (var cube in toIntersect)
            {
                try
                {
                    intersections.Enqueue(Intersect(intersection, cube));
                }
                catch (IndexOutOfRangeException) { }
            }

            while (intersections.Count > 1)
            {
                volume -= UniqueIntersection(intersections.Dequeue(), intersections.ToList());
            }

            if (intersections.Count > 0)
            {
                volume -= Volume(intersections.Dequeue());
            }

            return volume;
        }

        private ((int, int, int), (int, int, int)) Intersect(((int, int, int), (int, int, int)) a, ((int, int, int), (int, int, int)) b)
        {
            (int, int, int) from = (0, 0, 0);
            (int, int, int) to = (0, 0, 0);

            from.Item1 = a.Item1.Item1 < b.Item1.Item1? b.Item1.Item1 : a.Item1.Item1;
            to.Item1 = a.Item2.Item1 > b.Item2.Item1? b.Item2.Item1 : a.Item2.Item1;
            if (from.Item1 > to.Item1) throw new IndexOutOfRangeException();

            from.Item2 = a.Item1.Item2 < b.Item1.Item2 ? b.Item1.Item2 : a.Item1.Item2;
            to.Item2 = a.Item2.Item2 > b.Item2.Item2 ? b.Item2.Item2 : a.Item2.Item2;
            if (from.Item2 > to.Item2) throw new IndexOutOfRangeException();

            from.Item3 = a.Item1.Item3 < b.Item1.Item3 ? b.Item1.Item3 : a.Item1.Item3;
            to.Item3 = a.Item2.Item3 > b.Item2.Item3 ? b.Item2.Item3 : a.Item2.Item3;
            if (from.Item3 > to.Item3) throw new IndexOutOfRangeException();

            return (from, to);
        }

        private long Volume((bool, (int, int, int), (int, int, int)) instruction)
        {
            return Volume((instruction.Item2, instruction.Item3));
        }

        private long Volume(((int, int, int), (int, int, int)) cube)
        {
            return (long) (cube.Item2.Item1 - cube.Item1.Item1 + 1) * (long) (cube.Item2.Item2 - cube.Item1.Item2 + 1) * (long) (cube.Item2.Item3 - cube.Item1.Item3 + 1);
        }

        private bool GetValue(int x, int y, int z, bool[,,] reactor)
        {
            return reactor[HalfMax + x, HalfMax + y, HalfMax + z];
        }

        private bool SetValue(bool value, int x, int y, int z, ref bool[,,] reactor)
        {
            if (value == reactor[HalfMax + x, HalfMax + y, HalfMax + z]) return false;
            reactor[HalfMax + x, HalfMax + y, HalfMax + z] = value;
            return true;
        }
    }
}
