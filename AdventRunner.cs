// <copyright file="AdventRunner.cs" company="Marvin Fuchs">

namespace AdventOfCode
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using SharpLog;

    internal static class AdventRunner
    {
        internal static string GetInput(int year, int day, string cookie)
        {
            string filename = $"input/{year}/day{day:D2}.txt";
            if (File.Exists(filename))
            {
                Logging.LogTrace($"Loading input for year {year} day {day}...", "RUNNER");
                string input = File.ReadAllText(filename);
                input = input.TrimEnd('\n');
                return input;
            }

            Logging.LogTrace($"Downloading input for year {year} day {day}...", "RUNNER");

            using (WebClient client = new WebClient())
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
                client.Headers.Add(HttpRequestHeader.Cookie, $"session={cookie}");
                client.DownloadFile($"https://adventofcode.com/{year}/day/{day}/input", filename);
            }

            return GetInput(year, day, cookie);
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
                Logging.LogTrace("Solving puzzle 1...", "RUNNER");
                stopwatch.Restart();
                (clipboard, message) = ((object, string))solutionType
                    .GetMethod("Puzzle1", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(solution, new object[] { parsedInput });
                stopwatch.Stop();
                Logging.LogTrace($"Took {stopwatch.ElapsedMilliseconds}ms to excecute.", "RUNNER");

                Logging.LogInfo(message, "RUNNER");

                Logging.LogTrace("Solving puzzle 2...", "RUNNER");
                stopwatch.Restart();
                (clipboard, message) = ((object, string))solutionType
                    .GetMethod("Puzzle2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(solution, new object[] { parsedInput });
                stopwatch.Stop();
                Logging.LogTrace($"Took {stopwatch.ElapsedMilliseconds}ms to excecute.", "RUNNER");

                Logging.LogInfo(message, "RUNNER");
            }
            catch (Exception exc)
            {
                if (exc.ToString().Contains("SolutionNotImplementedException"))
                {
                    Logging.LogError("Solution is not implemented!", "RUNNER");
                }
                else
                {
                    throw exc;
                }
            }

            return clipboard.ToString();
        }

        internal static object Parse(Type parserType, string input)
        {
            Logging.LogTrace("Parsing input...", "RUNNER");

            try
            {
                var parser = parserType.GetConstructor(new Type[0]).Invoke(null);
                Stopwatch stopwatch = Stopwatch.StartNew();
                var parsedInput = parserType
                    .GetMethod("Parse", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(parser, new string[] { input });
                stopwatch.Stop();
                Logging.LogTrace($"Took {stopwatch.ElapsedMilliseconds}ms to excecute.", "RUNNER");
                return parsedInput;
            }
            catch (Exception exc)
            {
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
