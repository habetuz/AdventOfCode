using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2024.D01;

public class Solver : ISolver<(int[] Left, int[] Right)>
{
  public void Parse(string input, IPartSubmitter<(int[] Left, int[] Right)> partSubmitter)
  {
    var lines = input.Split('\n');
    int[] left = new int[lines.Length];
    int[] right = new int[lines.Length];
    for (int i = 0; i < lines.Length; i++)
    {
      var split = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
      left[i] = int.Parse(split[0]);
      right[i] = int.Parse(split[1]);
    }

    partSubmitter.Submit((left, right));
  }

  public void Solve((int[] Left, int[] Right) input, IPartSubmitter partSubmitter)
  {
    Array.Sort(input.Left);
    Array.Sort(input.Right);

    int difference = 0;
    Dictionary<int, (int Left, int Right)> scores = [];
    for (int i = 0; i < input.Left.Length; i++)
    {
      if (scores.TryGetValue(input.Left[i], out var count))
      {
        scores[input.Left[i]] = (count.Left + 1, count.Right);
      }
      else
      {
        scores[input.Left[i]] = (1, 0);
      }

      if (scores.TryGetValue(input.Right[i], out count))
      {
        scores[input.Right[i]] = (count.Left, count.Right + 1);
      }
      else
      {
        scores[input.Right[i]] = (0, 1);
      }

      difference += Math.Abs(input.Left[i] - input.Right[i]);
    }

    partSubmitter.SubmitPart1(difference);

    int similarityScore = 0;
    foreach (var entry in scores)
    {
      similarityScore += entry.Key * entry.Value.Left * entry.Value.Right;
    }

    partSubmitter.SubmitPart2(similarityScore);
  }
}
