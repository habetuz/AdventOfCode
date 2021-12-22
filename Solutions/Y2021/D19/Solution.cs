using AdventOfCode.Common;
using SharpLog.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D19
{
    internal class Solution : Solution<Scanner[]>
    {
        internal override string Puzzle1(Scanner[] input)
        {
            // Fill neighbour dictionaries
            foreach (Scanner scanner in input)
            {
                FillNeighbours(scanner);
            }

            List<Scanner> unresolved = new List<Scanner>(input);
            unresolved.Remove(input[0]);

            List<Scanner> resolved = new List<Scanner>()
            {
                input[0]
            };

            while (unresolved.Count > 0)
            {
                for (int i = 0; i < unresolved.Count; i++)
                {
                    Scanner b = unresolved[i];
                    for (int j = 0; j < resolved.Count; j++)
                    {
                        Scanner a = resolved[j];
                        (Beacon, Beacon)[] intersecting = GetIntersecting(a, b);

                        if (intersecting.Length < 12)
                        {
                            continue;
                        }

                        AdjustScanner(b, intersecting);
                        Translate(b, intersecting[0].Item2);
                        resolved.Add(b);
                        unresolved.Remove(b);

                        i = -1;

                        break;
                    }
                }
            }

            int uniqueBeacons = GetUniqueBeacons(input).Length;

            s_logger.Log($"There are {uniqueBeacons} unique beacons!", SharpLog.LogType.Info);

            return uniqueBeacons.ToString();
        }

        private void FillNeighbours(Scanner scanner)
        {
            foreach (Beacon beacon in scanner.Beacons)
            {
                foreach (Beacon neighbour in scanner.Beacons)
                {
                    if (neighbour == beacon) continue;
                    double distance = Math.Sqrt(
                            Math.Pow(beacon.X - neighbour.X, 2) +
                            Math.Pow(beacon.Y - neighbour.Y, 2) +
                            Math.Pow(beacon.Z - neighbour.Z, 2));
                    beacon.Neighbours.Add(
                        distance,
                        neighbour);
                    if (beacon.MaxDistance < distance) beacon.MaxDistance = distance;
                }
            }
        }

        private (Beacon, Beacon)[] GetIntersecting(Scanner a, Scanner b)
        {
            List<(Beacon, Beacon)> intersecting = new List<(Beacon, Beacon)>();

            List<Beacon> bBeacons = b.Beacons.ToList();

            foreach (Beacon aBeacon in a.Beacons)
            {
                Beacon toRemove = null;
                foreach (Beacon bBeacon in bBeacons)
                {
                    int EqualDistanceCounter = 0;

                    foreach (double distanceA in aBeacon.Neighbours.Keys)
                    {
                        if (distanceA > aBeacon.MaxDistance || distanceA > bBeacon.MaxDistance) continue;

                        foreach (double distanceB in bBeacon.Neighbours.Keys)
                        {
                            if (distanceA == distanceB) EqualDistanceCounter++;
                        }
                    }

                    if (EqualDistanceCounter >= 11)
                    {
                        intersecting.Add((aBeacon, bBeacon));
                        toRemove = bBeacon;
                        break;
                    }
                }

                if (toRemove != null) bBeacons.Remove(toRemove);
            }

            return intersecting.ToArray();
        }

        private void AdjustScanner(Scanner toAdjust, (Beacon, Beacon)[] intersecting)
        {
            Beacon a1 = intersecting[0].Item1.Clone();
            Beacon a2 = intersecting[0].Item2.Clone();

            Beacon b1 = null;
            Beacon b2 = null;

            for (int i = 1; i < intersecting.Length; i++)
            {
                if (
                    intersecting[i].Item1.X != a1.X &&
                    intersecting[i].Item1.Y != a1.Y &&
                    intersecting[i].Item1.Z != a1.Z &&
                    intersecting[i].Item1.X - a1.X != intersecting[i].Item1.Y - a1.Y &&
                    intersecting[i].Item1.X - a1.X != intersecting[i].Item1.Z - a1.Z &&
                    intersecting[i].Item1.Y - a1.Y != intersecting[i].Item1.Z - a1.Z)
                {
                    b1 = intersecting[i].Item1.Clone();
                    b2 = intersecting[i].Item2.Clone();
                    break;
                }
            }

            toAdjust.X = a1.X - a2.X;
            toAdjust.Y = a1.Y - a2.Y;
            toAdjust.Z = a1.Z - a2.Z;

            // Translate beacons
            a2.X += toAdjust.X;
            a2.Y += toAdjust.Y;
            a2.Z += toAdjust.Z;

            b2.X += toAdjust.X;
            b2.Y += toAdjust.Y;
            b2.Z += toAdjust.Z;

            b1.X -= a2.X;
            b1.Y -= a2.Y;
            b1.Z -= a2.Z;

            b2.X -= a2.X;
            b2.Y -= a2.Y;
            b2.Z -= a2.Z;

            a1.X = 0;
            a1.Y = 0;
            a1.Z = 0;

            a2.X = 0;
            a2.Y = 0;
            a2.Z = 0;

            //Print(new (Beacon, Beacon)[]
            //{
            //    (a1, a2),
            //    (b1, b2),
            //});

            int lengthX1 = Math.Abs(b1.X);
            int lengthY1 = Math.Abs(b1.Y);
            int lengthZ1 = Math.Abs(b1.Z);

            int lengthX2 = Math.Abs(b2.X);
            int lengthY2 = Math.Abs(b2.Y);
            int lengthZ2 = Math.Abs(b2.Z);

            // XDir
            if (lengthX1 == lengthX2)
            {
                string sign = b1.X * b2.X > 0 ? "+" : "-";
                string dimention = "X";
                toAdjust.XDir = sign + dimention;
            }
            else if (lengthX1 == lengthY2)
            {
                string sign = b1.X * b2.Y > 0 ? "+" : "-";
                string dimention = "Y";
                toAdjust.XDir = sign + dimention;
            }
            else if (lengthX1 == lengthZ2)
            {
                string sign = b1.X * b2.Z > 0 ? "+" : "-";
                string dimention = "Z";
                toAdjust.XDir = sign + dimention;
            }

            // YDir
            if (lengthY1 == lengthX2)
            {
                string sign = b1.Y * b2.X > 0 ? "+" : "-";
                string dimention = "X";
                toAdjust.YDir = sign + dimention;
            }
            else if (lengthY1 == lengthY2)
            {
                string sign = b1.Y * b2.Y > 0 ? "+" : "-";
                string dimention = "Y";
                toAdjust.YDir = sign + dimention;
            }
            else if (lengthY1 == lengthZ2)
            {
                string sign = b1.Y * b2.Z > 0 ? "+" : "-";
                string dimention = "Z";
                toAdjust.YDir = sign + dimention;
            }

            // ZDir
            if (lengthZ1 == lengthX2)
            {
                string sign = b1.Z * b2.X > 0 ? "+" : "-";
                string dimention = "X";
                toAdjust.ZDir = sign + dimention;
            }
            else if (lengthZ1 == lengthY2)
            {
                string sign = b1.Z * b2.Y > 0 ? "+" : "-";
                string dimention = "Y";
                toAdjust.ZDir = sign + dimention;
            }
            else if (lengthZ1 == lengthZ2)
            {
                string sign = b1.Z * b2.Z > 0 ? "+" : "-";
                string dimention = "Z";
                toAdjust.ZDir = sign + dimention;
            }
        }

        private void Translate(Scanner scanner, Beacon referenceBeacon)
        {
            // Translate to new position
            foreach (Beacon beacon in scanner.Beacons)
            {
                beacon.X += scanner.X;
                beacon.Y += scanner.Y;
                beacon.Z += scanner.Z;
            }

            // Translate to new zero point at beacon[0]
            int translateX = referenceBeacon.X;
            int translateY = referenceBeacon.Y;
            int translateZ = referenceBeacon.Z;

            scanner.X -= translateX;
            scanner.Y -= translateY;
            scanner.Z -= translateZ;

            foreach (Beacon beacon in scanner.Beacons)
            {
                beacon.X -= translateX;
                beacon.Y -= translateY;
                beacon.Z -= translateZ;
            }

            Rotate(ref scanner.X, ref scanner.Y, ref scanner.Z, scanner.XDir, scanner.YDir, scanner.ZDir);

            foreach (Beacon beacon in scanner.Beacons)
            {
                Rotate(ref beacon.X, ref beacon.Y, ref beacon.Z, scanner.XDir, scanner.YDir, scanner.ZDir);
            }

            scanner.X += translateX;
            scanner.Y += translateY;
            scanner.Z += translateZ;

            foreach (Beacon beacon in scanner.Beacons)
            {
                beacon.X += translateX;
                beacon.Y += translateY;
                beacon.Z += translateZ;
            }
        }

        private void Rotate(ref int x, ref int y, ref int z, string xDir, string yDir, string zDir)
        {
            int tmpX = x;
            int tmpY = y;
            int tmpZ = z;

            // Translate X
            switch (xDir[1])
            {
                case 'X':
                    x = tmpX;
                    break;
                case 'Y':
                    x = tmpY;
                    break;
                case 'Z':
                    x = tmpZ;
                    break;
            }

            if (xDir[0] == '-') x *= -1;

            // Translate Y
            switch (yDir[1])
            {
                case 'X':
                    y = tmpX;
                    break;
                case 'Y':
                    y = tmpY;
                    break;
                case 'Z':
                    y = tmpZ;
                    break;
            }

            if (yDir[0] == '-') y *= -1;

            // Translate Z
            switch (zDir[1])
            {
                case 'X':
                    z = tmpX;
                    break;
                case 'Y':
                    z = tmpY;
                    break;
                case 'Z':
                    z = tmpZ;
                    break;
            }

            if (zDir[0] == '-') z *= -1;
        }

        internal override string Puzzle2(Scanner[] input)
        {
            int largestDistance = 0;
            
            foreach (Scanner fromScanner in input)
            {
                foreach (Scanner toScanner in input)
                {
                    if (fromScanner == toScanner) continue;

                    int distance = Math.Abs(fromScanner.X - toScanner.X) + Math.Abs(fromScanner.Y - toScanner.Y) + Math.Abs(fromScanner.Z - toScanner.Z);

                    if (distance > largestDistance) largestDistance = distance;
                }
            }

            s_logger.Log($"The largest distance is {largestDistance}", SharpLog.LogType.Info);
            return largestDistance.ToString();
        }

        private Beacon[] GetUniqueBeacons(Scanner[] scanners)
        {
            List<Beacon> uniqueBeacons = new List<Beacon>();

            foreach (Scanner scanner in scanners)
            {
                foreach (Beacon beacon in scanner.Beacons)
                {
                    if (!uniqueBeacons.Any(uniqueBeacon =>
                    {
                        return
                            uniqueBeacon.X == beacon.X &&
                            uniqueBeacon.Y == beacon.Y &&
                            uniqueBeacon.Z == beacon.Z;
                    }))
                    {
                        uniqueBeacons.Add(beacon);
                    }

                }
            }

            return uniqueBeacons.ToArray();
        }
    }
}
