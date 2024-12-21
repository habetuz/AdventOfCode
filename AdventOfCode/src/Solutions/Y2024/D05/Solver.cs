using System.Collections;
using System.Data;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;
using SQLitePCL;

namespace AdventOfCode.Solutions.Y2024.D05;

public class Solver : ISolver<((byte Before, byte After)[] Rules, byte[][] Updates)>
{
  public void Parse(
    string input,
    IPartSubmitter<((byte Before, byte After)[] Rules, byte[][] Updates)> partSubmitter
  )
  {
    List<(byte Before, byte After)> rules = new(input.Length);
    List<byte[]> updates = new(input.Length / 2);

    var lines = new Queue<string>(input.Split('\n'));

    // Rules
    while (lines.TryDequeue(out string? line))
    {
      if (line == string.Empty)
        break;

      var values = line.Split('|');
      rules.Add((byte.Parse(values[0]), byte.Parse(values[1])));
    }

    // Updates
    while (lines.TryDequeue(out string? line))
    {
      updates.Add(line.Split(',').Select(byte.Parse).ToArray());
    }

    partSubmitter.Submit((rules.ToArray(), updates.ToArray()));
  }

  public void Solve(
    ((byte Before, byte After)[] Rules, byte[][] Updates) input,
    IPartSubmitter partSubmitter
  )
  {
    List<byte[]> incorrectUpdates = new(input.Updates.Length);

    int correctUpdatesScore = 0;
    foreach (var update in input.Updates)
    {
      var correct = true;
      for (int a = 0; a < update.Length - 1 && correct; a++)
      {
        for (int b = a + 1; b < update.Length; b++)
        {
          if (input.Rules.Contains((update[b], update[a])))
          {
            correct = false;
            break;
          }
        }
      }

      if (correct)
      {
        correctUpdatesScore += update[update.Length / 2];
      }
      else
      {
        incorrectUpdates.Add(update);
      }
    }

    partSubmitter.SubmitPart1(correctUpdatesScore);
    correctUpdatesScore = 0;

    foreach (var update in incorrectUpdates)
    {
      Array.Sort(
        update,
        (a, b) =>
        {
          if (input.Rules.Contains((a, b)))
            return -1;
          if (input.Rules.Contains((b, a)))
            return 1;
          return 0;
        }
      );

      correctUpdatesScore += update[update.Length / 2];
    }

    partSubmitter.SubmitPart2(correctUpdatesScore);
  }
}
