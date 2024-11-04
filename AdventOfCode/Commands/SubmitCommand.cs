using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Commands.Settings;
using SharpLog;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands
{
  public class SubmitCommand : Command<SubmitSettings>
  {
    public override int Execute([NotNull] CommandContext context, [NotNull] SubmitSettings settings)
    {
      SolutionStatisticsManager solutionStatisticsManager = new();
      var solutionStatistics = solutionStatisticsManager.Retrieve(settings.Date);

      if (
        solutionStatistics != null
        && (
          solutionStatistics.Value.Solution1 != null || solutionStatistics.Value.Solution2 != null
        )
      )
      {
        Logging.LogInfo(
          $"Solution for [yellow]{settings.Date}[/] already submitted.\n  [green]Solution 1:[/] {solutionStatistics.Value.Solution1}\n  [green]Solution 2:[/] {solutionStatistics.Value.Solution2}",
          "RUNNER"
        );
        Logging.LogWarning($"Overwriting solutions for [yellow]{settings.Date}[/].", "RUNNER");
      }

      solutionStatisticsManager.SubmitSolutions(
        new Solution { Solution1 = settings.Solution1, Solution2 = settings.Solution2 },
        settings.Date
      );

      Logging.LogInfo($"Submitted solutions for [yellow]{settings.Date}[/].", "RUNNER");

      return 0;
    }
  }
}
