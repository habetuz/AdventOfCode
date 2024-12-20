using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver.Templates;
using YamlDotNet.Core.Tokens;
using YamlDotNet.Serialization.ValueDeserializers;

namespace AdventOfCode.Solutions.Y2024.D02;

public class Solver : CustomLineSplittingSolver<byte[]>
{
  public override byte[] Convert(string value)
  {
    return value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(byte.Parse).ToArray();
  }

  public override void Solve(byte[][] input, IPartSubmitter partSubmitter)
  {
    int safeReports = 0;
    List<byte[]> unsafeReports = [];

    foreach (var report in input)
    {
      if (IsSafe(report))
      {
        safeReports++;
      }
      else
      {
        unsafeReports.Add(report);
      }
    }

    partSubmitter.SubmitPart1(safeReports);

    foreach (var report in unsafeReports)
    {
      for (int i = 0; i < report.Length; i++)
      {
        var testReport = report.ToList();
        testReport.RemoveAt(i);
        if (IsSafe([.. testReport]))
        {
          safeReports++;
          break;
        }
      }
    }

    partSubmitter.SubmitPart2(safeReports);
  }

  private bool IsSafe(byte[] report)
  {
    bool decreasing = report[1] < report[0];

    return report
      .Skip(1)
      .Select((value, index) => (current: value, previous: report[index]))
      .All(
        (value) =>
          (decreasing ? value.current < value.previous : value.current > value.previous)
          && Math.Abs(value.current - value.previous) <= 3
      );
  }
}
