// <copyright file="AdventRunner.cs" company="Marvin Fuchs">

namespace AdventOfCode
{
    using AdventOfCode.Common;
    using Pastel;
    using SharpLog;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Net.Http;

    internal static class AdventRunner
    {
        private const byte PAD = 30;

        private static readonly Dictionary<string, string> ExecutionTimes = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> Solutions = new Dictionary<string, string>();

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
                Tools.RegisterLine("download", $"{$"Downloading input",-PAD} - {"Pending".Pastel(Color.Yellow)}", "RUNNER", level: LogLevel.Trace);

                var request = client.GetStringAsync($"{year}/day/{day}/input");

                new FileInfo($"{year}/day{day:D2}.txt").Directory.Create();
                File.WriteAllText($"{year}/day{day:D2}.txt", request.Result);

                Tools.OverwriteLine("download", $"{$"Downloading input",-PAD} - {"Success".Pastel(Color.SeaGreen)}", "RUNNER", level: LogLevel.Trace);
            }
            catch (AggregateException e)
            {
                Tools.OverwriteLine("download", $"{$"Downloading input",-PAD} - {"Failed".Pastel(Color.Red)}", "RUNNER", level: LogLevel.Trace);
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
                Logging.LogFatal("Solution class was not found!", "RUNNER", exception: ex);
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
                Logging.LogFatal("Parser class was not found!", "RUNNER", exception: ex);
            }

            return type;
        }

        internal static string Solve(Type solutionType, object parsedInput)
        {
            Tools.RegisterLine("solve", $"{$"Solving puzzles",-PAD} - {"Pending".Pastel(Color.Yellow)}", "RUNNER", level: LogLevel.Trace);

            if (parsedInput == null)
            {
                return null;
            }

            var solution = solutionType.GetConstructor(new Type[0]).Invoke(null);
            Stopwatch stopwatch = new Stopwatch();
            object clipboard = string.Empty;
            string message = string.Empty;

            try
            {
                Tools.RegisterLine("solve1", $"{$"Solving puzzle 1",-PAD} - {"Pending".Pastel(Color.Yellow)}", "RUNNER", level: LogLevel.Trace);

                stopwatch.Restart();
                (clipboard, message) = ((object, string))solutionType
                    .GetMethod("Puzzle1", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(solution, new object[] { parsedInput });
                stopwatch.Stop();

                ExecutionTimes["Solution 1"] = $"{stopwatch.Elapsed:G}";
                Tools.OverwriteLine("solve1", $"{$"Solving puzzle 1",-PAD} - {"Success".Pastel(Color.SeaGreen)}", "RUNNER", level: LogLevel.Trace);

                Solutions["Solution 1"] = message;

                Tools.RegisterLine("solve2", $"{$"Solving puzzle 2",-PAD} - {"Pending".Pastel(Color.Yellow)}", "RUNNER", level: LogLevel.Trace);

                stopwatch.Restart();
                (clipboard, message) = ((object, string))solutionType
                    .GetMethod("Puzzle2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(solution, new object[] { parsedInput });
                stopwatch.Stop();

                ExecutionTimes["Solution 2"] = $"{stopwatch.Elapsed:G}";
                Tools.OverwriteLine("solve2", $"{$"Solving puzzle 2",-PAD} - {"Success".Pastel(Color.SeaGreen)}", "RUNNER", level: LogLevel.Trace);

                Solutions["Solution 2"] = message;
            }
            catch (Exception exc)
            {
                Tools.OverwriteLine("solve", $"{$"Solving puzzles",-PAD} - {"Failed".Pastel(Color.Red)}", "RUNNER", level: LogLevel.Trace);
                Tools.OverwriteLine("solve1", $"{$"Solving puzzle 1",-PAD} - {"Failed".Pastel(Color.Red)}", "RUNNER", level: LogLevel.Trace);
                Tools.OverwriteLine("solve2", $"{$"Solving puzzle 1",-PAD} - {"Failed".Pastel(Color.Red)}", "RUNNER", level: LogLevel.Trace);

                if (exc.ToString().Contains("SolutionNotImplementedException"))
                {
                    Logging.LogFatal("Solution is not implemented!", "RUNNER");
                }
                else
                {
                    throw exc;
                }
            }

            Tools.OverwriteLine("solve", $"{$"Solving puzzles",-PAD} - {"Success".Pastel(Color.SeaGreen)}", "RUNNER", level: LogLevel.Trace);

            Logging.LogInfo("Execution times:\n" + Tools.Formatt(ExecutionTimes), "RUNNER");
            Logging.LogInfo("Solutions:\n" + Tools.Formatt(Solutions), "RUNNER");

            return clipboard.ToString();
        }

        internal static object Parse(Type parserType, string input)
        {
            Tools.RegisterLine("parse", $"{$"Parsing inputs",-PAD} - {"Pending".Pastel(Color.Yellow)}", "RUNNER", level: LogLevel.Trace);

            try
            {
                var parser = parserType.GetConstructor(new Type[0]).Invoke(null);
                Stopwatch stopwatch = Stopwatch.StartNew();
                var parsedInput = parserType
                    .GetMethod("Parse", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(parser, new string[] { input });
                stopwatch.Stop();

                ExecutionTimes["Parsing"] = $"{stopwatch.Elapsed:G}";

                Tools.OverwriteLine("parse", $"{$"Parsing inputs",-PAD} - {"Success".Pastel(Color.SeaGreen)}", "RUNNER", level: LogLevel.Trace);

                return parsedInput;
            }
            catch (Exception exc)
            {
                Tools.OverwriteLine("parse", $"{$"Parsing inputs",-PAD} - {"Failed".Pastel(Color.Red)}", "RUNNER", level: LogLevel.Trace);
                if (exc.Message.Contains("ParserNotImplementedException"))
                {
                    Logging.LogFatal("Parser is not implemented!", "RUNNER");
                }
                else
                {
                    throw exc;
                }
            }

            return null;
        }
    }
}
