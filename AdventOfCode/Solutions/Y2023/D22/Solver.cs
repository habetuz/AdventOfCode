using System.Collections.ObjectModel;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using SharpLog;

namespace AdventOfCode.Solutions.Y2023.D22;

public class Solver : ISolver<SandCuboid[]>
{
    public void Parse(string input, IPartSubmitter<SandCuboid[]> partSubmitter)
    {
        partSubmitter.Submit(
            input
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(
                    (line) =>
                    {
                        long[][] parts = line.Split('~')
                            .Select(
                                (part) =>
                                    part.Split(',').Select((number) => long.Parse(number)).ToArray()
                            )
                            .ToArray();

                        Coordinate3D a = new(parts[0][0], parts[0][1], parts[0][2]);
                        Coordinate3D b = new(parts[1][0], parts[1][1], parts[1][2]);

                        return new SandCuboid(a, b);
                    }
                )
                .ToArray()
        );
    }

    public void Solve(SandCuboid[] input, IPartSubmitter partSubmitter)
    {
        Array.Sort(input, (a, b) => (short)a[Direction3D.Bottom] - (short)b[Direction3D.Bottom]);

        // Settle sand cubes
        Dictionary<short, List<SandCuboid>> collisionReferences = [];

        foreach (var cuboid in input)
        {
            if (collisionReferences.TryGetValue((short)cuboid[Direction3D.Top], out var cuboids))
            {
                cuboids.Add(cuboid);
            }
            else
            {
                collisionReferences[(short)cuboid[Direction3D.Top]] = [cuboid];
            }
        }

        foreach (SandCuboid cuboid in input)
        {
            collisionReferences[(short)cuboid[Direction3D.Top]].Remove(cuboid);

            do
            {
                cuboid.Move(Direction3D.Bottom);
            } while (
                cuboid[Direction3D.Bottom] >= 1
                && (
                    !collisionReferences.ContainsKey((short)cuboid[Direction3D.Bottom])
                    || !collisionReferences[(short)cuboid[Direction3D.Bottom]]
                        .Any((toCheck) => cuboid.Intersects(toCheck))
                )
            );

            if (cuboid[Direction3D.Bottom] >= 1)
            {
                foreach (var toCheck in collisionReferences[(short)cuboid[Direction3D.Bottom]])
                {
                    if (cuboid.Intersects(toCheck))
                    {
                        cuboid.SupportedBy.Add(toCheck);
                        toCheck.Supporting.Add(cuboid);
                    }
                }
            }

            cuboid.Move(Direction3D.Top);

            if (collisionReferences.TryGetValue((short)cuboid[Direction3D.Top], out var cuboids))
            {
                cuboids.Add(cuboid);
            }
        }

        int saveCuboids = 0;
        int fallCountSum = 0;

        foreach (var cuboid in input)
        {
            int fallCount = cuboid.RemoveSelf();

            fallCountSum += fallCount;

            if (fallCount == 0)
            {
                saveCuboids++;
            }

            cuboid.Reset();
        }

        partSubmitter.SubmitPart1(saveCuboids);
        partSubmitter.SubmitPart2(fallCountSum);
    }
}
