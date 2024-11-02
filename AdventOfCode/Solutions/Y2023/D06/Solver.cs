using System.Runtime.InteropServices.Marshalling;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2023.D06;

public class Solver : ISolver<Race[], Race>
{
  public void Parse(string input, IPartSubmitter<Race[], Race> partSubmitter)
  {
    var lines = input
      .Split('\n', StringSplitOptions.RemoveEmptyEntries)
      .Select((line) => line.Split(':')[1]);
    var numbers = lines
      .Select(
        (line) =>
          line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select((number) => int.Parse(number))
            .ToArray()
      )
      .ToArray();
    var races = new Race[numbers[0].Length];

    for (int i = 0; i < races.Length; i++)
    {
      races[i] = new() { Duration = numbers[0][i], RecordDistance = numbers[1][i] };
    }

    partSubmitter.SubmitPart1(races);

    var bigNumbers = lines.Select((line) => long.Parse(line.Replace(" ", string.Empty)));

    partSubmitter.SubmitPart2(
      new Race { Duration = bigNumbers.First(), RecordDistance = bigNumbers.Last() }
    );
  }

  public void Solve(Race[] input1, Race input2, IPartSubmitter partSubmitter)
  {
    int product = 1;
    foreach (var race in input1)
    {
      // ABC Formula where a = -RecordDistance b = Duration and c = -1
      double low =
        (-race.Duration + Math.Sqrt(Math.Pow(race.Duration, 2) - (4 * (race.RecordDistance + 1))))
        / (-2);

      double high =
        (-race.Duration - Math.Sqrt(Math.Pow(race.Duration, 2) - (4 * (race.RecordDistance + 1))))
        / (-2);

      product *= calculateWinningRange(race);
    }

    partSubmitter.SubmitPart1(product);

    partSubmitter.SubmitPart2(calculateWinningRange(input2));
  }

  private int calculateWinningRange(Race race)
  {
    // ABC Formula where a = -RecordDistance b = Duration and c = -1
    double low =
      (-race.Duration + Math.Sqrt(Math.Pow(race.Duration, 2) - (4 * (race.RecordDistance + 1))))
      / (-2);

    double high =
      (-race.Duration - Math.Sqrt(Math.Pow(race.Duration, 2) - (4 * (race.RecordDistance + 1))))
      / (-2);

    return (int)(Math.Floor(high) - Math.Ceiling(low) + 1);
  }
}
