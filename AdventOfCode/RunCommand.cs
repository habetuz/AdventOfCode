using System.Diagnostics.CodeAnalysis;
using SharpLog;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode
{
    public class RunCommand : Command<RunSettings>
    {

        public override int Execute([NotNull] CommandContext context, [NotNull] RunSettings settings)
        {
            CalendarRange calendarRange = new()
            {
                StartDate = settings.StartDate,
                EndDate = settings.EndDate,
            };

            ISolutionSubmitter solutionStatisticsManager = new SolutionStatisticsManager();
            WebRessourceManager webRessourceManager = new();
            IInputRetriever inputRetriever = new InputManager(webRessourceManager);

            AnsiConsole.Write(
                new Rule()
                {
                    Title = $"[purple]Running puzzle [yellow]{(settings.StartDate == settings.EndDate ? settings.StartDate.ToString() : "[/]from [yellow]" + settings.StartDate.ToString() + "[/] to [yellow]" + settings.EndDate.ToString())}[/][/]",
                    Style = "purple",
                });

            foreach (var date in calendarRange)
            {
                AnsiConsole.Write(
                    new Rule()
                    {
                        Title = "[green]Running puzzle [yellow]" + date.ToString() + "[/][/]",
                        Style = "green",
                    });

                ISolver<object, object>? solver = GetSolver(date);
                if (solver is null)
                {
                    Logging.LogWarning("Solution is not implemented!", "RUNNER");
                    continue;
                }

                string input = inputRetriever.RetrieveInput(date, settings.Example);
                Solution? exampleSolution = inputRetriever.RetrieveExampleSolution(date, settings.Example);
                ISolverRunner runner = settings.RunTimed ? new TimedSolverRunner(solver, input) : new SingleTimeRunner(solver, input);
                Solution solution = runner.Run();
                solutionStatisticsManager.Submit(solution);
                PrintResult(solution, exampleSolution);
            }

            ReadMeGenerator readMeGenerator = new(webRessourceManager);
            readMeGenerator.Generate();

            return 1;
        }

        private static ISolver<object, object>? GetSolver(CalendarRange.Date date)
        {
            throw new NotImplementedException();
        }

        private static void PrintResult(Solution solution, Solution? exampleSolution)
        {
            throw new NotImplementedException();
        }
    }
}