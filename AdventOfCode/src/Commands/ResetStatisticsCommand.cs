using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Commands.Settings;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands;

public class ResetStatisticsCommand : Command
{
  public override int Execute(CommandContext context)
  {
    new SolutionStatisticsManager().DropStatistics();
    return 0;
  }
}
