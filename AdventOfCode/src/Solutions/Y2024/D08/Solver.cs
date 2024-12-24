using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2024.D08;

public class Solver : ISolver<(HashSet<(Coordinate Position, char ID)> Antennas, Coordinate Bound)>
{
  public void Parse(
    string input,
    IPartSubmitter<(
      HashSet<(Coordinate Position, char ID)> Antennas,
      Coordinate Bound
    )> partSubmitter
  )
  {
    char[,] map = Array2D.FromString(input);
    Coordinate bound = (map.GetLength(0) - 1, map.GetLength(1) - 1);
    HashSet<(Coordinate, char)> antennas = [];
    foreach ((char value, Coordinate coordinate) in Array2D.Enumerate(map))
    {
      if (value != '.')
      {
        antennas.Add((coordinate, value));
      }
    }

    partSubmitter.Submit((antennas, bound));
  }

  private IEnumerable<Coordinate> GenerateAntinodes(Coordinate a, Coordinate b, Coordinate bound)
  {
    var offset = b - a;
    var antinode = b + offset;
    while (antinode.IsInSpace(bound))
    {
      yield return antinode;
      antinode += offset;
    }
  }

  public void Solve(
    (HashSet<(Coordinate Position, char ID)> Antennas, Coordinate Bound) input,
    IPartSubmitter partSubmitter
  )
  {
    (HashSet<(Coordinate Position, char ID)> antennas, Coordinate bound) = input;

    var groups = antennas.GroupBy((value) => value.ID);

    var flattened = groups.SelectMany(
      (group) =>
        Permutation
          .Permutate(group.Select((antenna) => antenna.Position).ToArray(), 2, false)
          .Select((permutation) => (permutation[0], permutation[1]))
    );
    var antinodes = flattened
      .Select((pair) => 2 * pair.Item2 - pair.Item1)
      .Where((antinode) => antinode.IsInSpace(bound))
      .Distinct();

    partSubmitter.SubmitPart1(antinodes.Count());

    antinodes = flattened
      .SelectMany((pair) => GenerateAntinodes(pair.Item1, pair.Item2, bound))
      .Concat(antennas.Select(antenna => antenna.Position))
      .Distinct();

    partSubmitter.SubmitPart2(antinodes.Count());
  }
}
