using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using MathNet.Numerics;
using SharpLog;

namespace AdventOfCode.Solutions.Y2023.D21;

public class Solver
    : ISolver<(HashSet<Coordinate> rocks, HashSet<Coordinate> positions, int width, int height)>
{
    public void Parse(
        string input,
        IPartSubmitter<(
            HashSet<Coordinate> rocks,
            HashSet<Coordinate> positions,
            int width,
            int height
        )> partSubmitter
    )
    {
        var map = Array2D.FromString(input);
        HashSet<Coordinate> rocks = new();
        HashSet<Coordinate> positions = new();
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                if (map[x, y] == 'S')
                {
                    positions.Add(new Coordinate(x, y));
                }
                else if (map[x, y] == '#')
                {
                    rocks.Add(new Coordinate(x, y));
                }
            }
        }

        partSubmitter.Submit((rocks, positions, map.GetLength(0), map.GetLength(1)));
    }

    public void Solve(
        (HashSet<Coordinate> rocks, HashSet<Coordinate> positions, int width, int height) input,
        IPartSubmitter partSubmitter
    )
    {
        int steps = 64;

        Coordinate start = input.positions.First();

        for (int i = 0; i < steps; i++)
        {
            var newPositions = new HashSet<Coordinate>();
            foreach (var position in input.positions)
            {
                var neighbors = position.GetNeighbors(
                    Direction.Left,
                    Direction.Right,
                    Direction.Up,
                    Direction.Down
                );
                foreach (var neighbor in neighbors)
                {
                    if (
                        neighbor.X < 0
                        || neighbor.Y < 0
                        || neighbor.X >= input.width
                        || neighbor.Y >= input.height
                    )
                    {
                        continue;
                    }

                    if (newPositions.Contains(neighbor))
                    {
                        continue;
                    }

                    if (input.rocks.Contains(neighbor))
                    {
                        continue;
                    }

                    newPositions.Add(neighbor);
                }
            }

            input.positions = newPositions;
        }

        partSubmitter.SubmitPart1(input.positions.Count);

        // input.positions = new HashSet<Coordinate>() { start };

        // for (int i = 0; i < 500; i++)
        // {
        //     var newPositions = new HashSet<Coordinate>();
        //     foreach (var position in input.positions)
        //     {
        //         var neighbors = position.GetNeighbors(
        //             Direction.Left,
        //             Direction.Right,
        //             Direction.Up,
        //             Direction.Down
        //         );
        //         foreach (var neighbor in neighbors)
        //         {
        //             if (newPositions.Contains(neighbor))
        //             {
        //                 continue;
        //             }

        //             if (
        //                 input
        //                     .rocks
        //                     .Contains(
        //                         new Coordinate(
        //                             Euclid.Modulus(neighbor.X, input.width),
        //                             Euclid.Modulus(neighbor.Y, input.height)
        //                         )
        //                     )
        //             )
        //             {
        //                 continue;
        //             }

        //             newPositions.Add(neighbor);
        //         }
        //     }

        //     input.positions = newPositions;
        //     Logging.LogDebug(input.positions.Count, "FILE");
        //     Array2D.Print(
        //         (uint)(input.width * 5),
        //         (uint)(input.height * 5),
        //         (x, y) =>
        //         {
        //             x -= input.width * 2;
        //             y -= input.height * 2;
        //             if (
        //                 input
        //                     .rocks
        //                     .Contains(
        //                         new Coordinate(
        //                             Euclid.Modulus(x, input.width),
        //                             Euclid.Modulus(y, input.height)
        //                         )
        //                     )
        //             )
        //             {
        //                 return "[white]#[/]";
        //             }

        //             if (input.positions.Contains(new Coordinate(x, y)))
        //             {
        //                 return "[green]O[/]";
        //             }

        //             return "[gray].[/]";
        //         }
        //     );
        // }
    }
}
