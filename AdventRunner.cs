// <copyright file="AdventRunner.cs" company="Marvin Fuchs">

namespace AdventOfCode
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using SharpLog;
    using Spectre.Console;

    internal static class AdventRunner
    {
        private static Table executionTimes = new Table()
            .AddColumns(new string[] { "[yellow]Name[/]", "[yellow]Time[/]" })
            .AddRow(new string[] { "Parsing", "[red][red]N/A[/][/]" })
            .AddRow(new string[] { "Solution 1", "[red]N/A[/]" })
            .AddRow(new string[] { "Solution 2", "[red]N/A[/]" });

        private static Table solutions = new Table()
            .AddColumns(new string[] { "[yellow]Name[/]", "[yellow]Solution[/]" })
            .AddRow(new string[] { "Solution 1", "[red]N/A[/]" })
            .AddRow(new string[] { "Solution 2", "[red]N/A[/]" });

        internal static string ExcpectedPuzzle1 { get; set; }

        internal static string ExcpectedPuzzle2 { get; set; }

        internal static string GetInput(int year, int day, int test, HttpClient client)
        {
            string filename = test < 0 ? $"{year}/day{day:D2}.txt" : $"{year}/day{day:D2}_{test:D2}.txt";
            if (File.Exists(filename))
            {
                string input = File.ReadAllText(filename);
                input = input.TrimEnd('\n');
                return input;
            }

            if (test >= 0)
            {
                Logging.LogFatal($"Test input {test} does not exist!");
            }

            try
            {
                AnsiConsole.Status()
                    .Spinner(Spinner.Known.Dots2)
                    .Start("[green]Downloading input...[/]", ctx =>
                    {
                        var request = client.GetStringAsync($"{year}/day/{day}/input");

                        new FileInfo($"{year}/day{day:D2}.txt").Directory.Create();
                        File.WriteAllText($"{year}/day{day:D2}.txt", request.Result);
                    });
            }
            catch (AggregateException e)
            {
                if (e.InnerException.GetType() == typeof(HttpRequestException))
                {
                    Logging.LogFatal("Server request failed! Possible reasons might be:\n- The provided day is not available\n- You do not have an internet connection\n- The advent of code server is offline", "RUNNER");
                }
                else
                {
                    throw e;
                }
            }

            return GetInput(year, day, test, client);
        }

        internal static Type GetSolution(int year, int day)
        {
            Type type = null;

            try
            {
                type = Type.GetType($"AdventOfCode.Solutions.Y{year}.D{day:D2}.Solution", true);
            }
            catch (Exception ex)
            {
                Logging.LogError("Solution class was not found!", "RUNNER", exception: ex);
            }

            return type;
        }

        internal static Type GetParser(int year, int day)
        {
            Type type = null;

            try
            {
                type = Type.GetType($"AdventOfCode.Solutions.Y{year}.D{day:D2}.Parser", true);
            }
            catch (Exception ex)
            {
                Logging.LogError("Parser class was not found!", "RUNNER", exception: ex);
            }

            return type;
        }

        internal static string Solve(Type solutionType, object parsedInput, bool debugEnabled, ref bool successfully)
        {
            if (parsedInput == null)
            {
                return null;
            }

            var solution = solutionType.GetConstructor(new Type[0]).Invoke(null);
            Stopwatch stopwatch = new Stopwatch();
            object clipboard = null;
            string message = null;

            var rule = new Rule
            {
                Style = Style.Parse("grey"),
            };

            try
            {
                rule.Title = "[grey]Puzzle 1[/]";
                AnsiConsole.Write(new Padder(rule).Padding(0, 1, 0, 0));

                bool correct = true;
                bool isNumber = false;
                long error = 0;

                AnsiConsole.Status()
                    .Spinner(Spinner.Known.Dots2)
                    .Start("[green]Solving puzzle 1...[/]", ctx =>
                    {
                        stopwatch.Restart();

                        (clipboard, message) = ((object, string))solutionType
                            .GetMethod("Puzzle1", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            .Invoke(solution, new object[] { parsedInput });

                        stopwatch.Stop();
                        executionTimes.UpdateCell(1, 1, $"{stopwatch.Elapsed:G}");
                        solutions.UpdateCell(0, 1, message);

                        if (ExcpectedPuzzle1 != null)
                        {
                            isNumber = long.TryParse(ExcpectedPuzzle1, out var expectedPuzzle1);
                            if (isNumber)
                            {
                                error = long.Parse(clipboard.ToString()) - expectedPuzzle1;
                                correct = error == 0;
                            }
                            else
                            {
                                correct = ExcpectedPuzzle1 == clipboard.ToString();
                            }
                        }
                    });

                if (correct)
                {
                    AnsiConsole.MarkupLine("[#00ff00]+[/] [green]Solving puzzle 1[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[yellow]-[/] [green]Solving puzzle 1. Correct answer: [#00ff00]{ExcpectedPuzzle1}[/] | Yor answer: [red]{clipboard}[/]{(isNumber ? $" | Difference of [red]{error}[/]" : string.Empty)}[/]");
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("SolutionNotImplementedException"))
                {
                    Logging.LogError("Solution 1 is not implemented!", "RUNNER");
                }
                else
                {
                    Logging.LogError("Solution 1 failed!", "RUNNER");
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenPaths | ExceptionFormats.ShortenTypes | ExceptionFormats.ShortenMethods | ExceptionFormats.ShowLinks);
                }

                AnsiConsole.MarkupLine("[red]![/] [green]Solving puzzle 1[/]");
                successfully = false;
            }

            try
            {
                rule.Title = "[grey]Puzzle 2[/]";
                AnsiConsole.Write(new Padder(rule).Padding(0, 1, 0, 0));

                bool correct = true;
                bool isNumber = false;
                long error = 0;

                AnsiConsole.Status()
                    .Spinner(Spinner.Known.Dots2)
                    .Start("[green]Solving puzzle 2...[/]", ctx =>
                    {
                        stopwatch.Restart();

                        (clipboard, message) = ((object, string))solutionType
                        .GetMethod("Puzzle2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        .Invoke(solution, new object[] { parsedInput });

                        stopwatch.Stop();

                        executionTimes.UpdateCell(2, 1, $"{stopwatch.Elapsed:G}");

                        solutions.UpdateCell(1, 1, message);

                        if (ExcpectedPuzzle1 != null)
                        {
                            isNumber = long.TryParse(ExcpectedPuzzle2, out var expectedPuzzle2);
                            if (isNumber)
                            {
                                error = long.Parse(clipboard.ToString()) - expectedPuzzle2;
                                correct = error == 0;
                            }
                            else
                            {
                                correct = ExcpectedPuzzle2 == clipboard.ToString();
                            }
                        }
                    });

                if (correct)
                {
                    AnsiConsole.MarkupLine("[#00ff00]+[/] [green]Solving puzzle 2[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[yellow]-[/] [green]Solving puzzle 2. Correct answer: [#00ff00]{ExcpectedPuzzle2} [/]| Yor answer: [red]{clipboard}[/]{(isNumber ? $" | Difference of [red]{error}[/]" : string.Empty)}[/]");
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("SolutionNotImplementedException"))
                {
                    Logging.LogError("Solution 2 is not implemented!", "RUNNER");
                }
                else
                {
                    Logging.LogError("Solution 2 failed!", "RUNNER");
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenPaths | ExceptionFormats.ShortenTypes | ExceptionFormats.ShortenMethods | ExceptionFormats.ShowLinks);
                }

                AnsiConsole.MarkupLine("[red]![/] [green]Solving puzzle 2[/]");
                successfully = false;
            }

            return clipboard?.ToString();
        }

        internal static object Parse(Type parserType, string input, bool debugEnabled, ref bool successfully)
        {
            try
            {
                var rule = new Rule
                {
                    Style = Style.Parse("grey"),
                    Title = "[grey]Parsing[/]",
                };
                AnsiConsole.Write(rule);

                object parsedInput = null;

                AnsiConsole.Status()
                    .Spinner(Spinner.Known.Dots2)
                    .Start("[green]Parsing...[/]", ctx =>
                    {
                        var parser = parserType.GetConstructor(new Type[0]).Invoke(null);
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        parsedInput = parserType
                            .GetMethod("Parse", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            .Invoke(parser, new string[] { input });
                        stopwatch.Stop();

                        executionTimes.UpdateCell(0, 1, $"{stopwatch.Elapsed:G}");
                    });

                AnsiConsole.MarkupLine("[#00ff00]+[/] [green]Parsing[/]");

                return parsedInput;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ParserNotImplementedException"))
                {
                    Logging.LogError("Parser is not implemented!", "RUNNER");
                }
                else
                {
                    Logging.LogError("Parsing failed!", "RUNNER");
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenPaths | ExceptionFormats.ShortenTypes | ExceptionFormats.ShortenMethods | ExceptionFormats.ShowLinks);
                }

                AnsiConsole.MarkupLine("[red]![/] [green]Parsing[/]");
                successfully = false;
            }

            return null;
        }

        internal static void PrintResults(bool successfully)
        {
            var appendage = successfully ? "[#00ff00]successfully[/]" : "[red]faulty[/]";
            var rule = new Rule($"[green]Execution finished[/] {appendage}[green]![/]")
            {
                Style = successfully ? Style.Parse("green") : Style.Parse("red"),
                Border = BoxBorder.Double,
            };

            AnsiConsole.Write(new Padder(rule).Padding(0, 1, 0, 0));

            executionTimes.Title = new TableTitle("[[ [green]Execution times[/] ]]");
            solutions.Title = new TableTitle("[[ [green]Solutions[/] ]]");
            executionTimes.Expand = true;
            solutions.Expand = true;
            executionTimes.Border = TableBorder.SimpleHeavy;
            solutions.Border = TableBorder.SimpleHeavy;

            var column = new Columns(new Spectre.Console.Rendering.IRenderable[]
            {
                new Padder(executionTimes).PadRight(2),
                new Padder(solutions).PadLeft(2),
            });

            AnsiConsole.Write(column);

            executionTimes = new Table()
                .AddColumns(new string[] { "[yellow]Name[/]", "[yellow]Time[/]" })
                .AddRow(new string[] { "Parsing", "[red][red]N/A[/][/]" })
                .AddRow(new string[] { "Solution 1", "[red]N/A[/]" })
                .AddRow(new string[] { "Solution 2", "[red]N/A[/]" });

            solutions = new Table()
                .AddColumns(new string[] { "[yellow]Name[/]", "[yellow]Solution[/]" })
                .AddRow(new string[] { "Solution 1", "[red]N/A[/]" })
                .AddRow(new string[] { "Solution 2", "[red]N/A[/]" });

            ExcpectedPuzzle1 = null;
            ExcpectedPuzzle2 = null;
        }
    }
}
