using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Commands.Settings;
using Config.Net;
using SharpLog;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands
{
    public class SubmitCommand : Command<SubmitSettings>
    {
        public override int Execute(
            [NotNull] CommandContext context,
            [NotNull] SubmitSettings settings
        )
        {
            SolutionStatisticsManager solutionStatisticsManager = new();
            var solutionStatistics = solutionStatisticsManager.Retrieve(settings.Date);

            if (solutionStatistics != null)
            {
                Logging.LogInfo(
                    $"Solution for [yellow]{settings.Date}[/] already submitted.\n [green]Solution 1:[/] {solutionStatistics.Value.Solution1}\n  [green]Solution 2:[/] {solutionStatistics.Value.Solution2}",
                    "SUBMITTER"
                );
            }
            
            return 0;
        }
    }
}
