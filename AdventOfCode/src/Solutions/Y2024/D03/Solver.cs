using System.Text.RegularExpressions;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;

namespace AdventOfCode.Solutions.Y2024.D03;

public class Solver : UnmodifingSolver
{
  public override void Solve(string input, IPartSubmitter partSubmitter)
  {
    string pattern = @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)";
    int result1 = 0;
    int result2 = 0;

    bool enabled = true;
    foreach (Match match in Regex.Matches(input, pattern))
    {
      if (match.Value == "do()")
        enabled = true;
      else if (match.Value == "don't()")
        enabled = false;
      else
      {
        var value = int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        result1 += value;

        if (enabled)
          result2 += value;
      }
    }

    partSubmitter.SubmitPart1(result1);
    partSubmitter.SubmitPart2(result2);
  }
}
