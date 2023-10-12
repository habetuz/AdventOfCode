using System.Diagnostics.CodeAnalysis;
using SharpLog;
using Spectre.Console;
using Spectre.Console.Cli;
using AdventOfCode.Commands.Settings;
using AdventOfCode.Time;
using AdventOfCode.Solver;
using AdventOfCode.Solver.Runner;

namespace AdventOfCode.Commands
{
    public class RunCommand : Command<RunSettings>
    {

        public override int Execute([NotNull] CommandContext context, [NotNull] RunSettings settings)
        {

            var calendarRange = settings.RunRange;

            ISolutionSubmitter solutionStatisticsManager = new SolutionStatisticsManager();
            WebResourceManager webResourceManager = new();
            IInputRetriever inputRetriever = new InputManager(webResourceManager);

            AnsiConsole.Write(
                new Rule()
                {
                    Title = $"[purple]Running puzzle [yellow]{(calendarRange.StartDate == calendarRange.EndDate ? calendarRange.StartDate.ToString() : "[/]from [yellow]" + calendarRange.StartDate.ToString() + "[/] to [yellow]" + calendarRange.EndDate.ToString())}[/][/]",
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
                    Logging.LogError("Solution is not implemented!", "RUNNER");
                    continue;
                }

                string input = inputRetriever.RetrieveInput(date, settings.Example);
                Solution? exampleSolution = inputRetriever.RetrieveExampleSolution(date, settings.Example);
                ISolverRunner runner = settings.RunTimed ? new TimedSolverRunner(solver, input) : new SingleTimeRunner(solver, input);
                Solution solution = runner.Run();
                solutionStatisticsManager.Submit(solution);
                PrintResult(solution, exampleSolution);
            }

            ReadMeGenerator readMeGenerator = new(webResourceManager);
            readMeGenerator.Generate();

            return 1;
        }

        private static ISolver<object, object>? GetSolver(Date date)
        {
            Type? solverType = Type.GetType($"AdventOfCode.Solutions.Y{date.Year}.D{date.Day}.Solver");
            if (solverType == null)
            {
                return null;
            }
            
            return (ISolver<object, object>)Activator.CreateInstance(solverType)!;
        }

        private static void PrintResult(Solution solution, Solution? exampleSolution)
        {
            throw new NotImplementedException();
        }
    }
}