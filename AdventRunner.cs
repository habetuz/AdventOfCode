// <copyright file="AdventRunner.cs" company="Marvin Fuchs">

namespace AdventOfCode
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text.Json;
    using SharpLog;

    internal static class AdventRunner
    {
        private static readonly string Cookie;
        private static readonly int Year;
        private static readonly int Day;

        static AdventRunner()
        {
            if (!File.Exists("settings.json"))
            {
                SharpLog.Logging.LogFatal(string.Empty, exception: new FileNotFoundException("Settings file not found! Add a 'settings.json' file!"));
            }

            string settingsJsonString = File.ReadAllText("settings.json");
            JsonElement settings = JsonSerializer.Deserialize<JsonElement>(settingsJsonString);
            Cookie = settings.GetProperty("SessionCookie").GetString();
            Year = settings.GetProperty("Year").GetInt32();
            Day = settings.GetProperty("Day").GetInt32();
        }

        internal static string GetInput()
        {
            string filename = $"input/{Year}/day{Day:D2}.txt";
            if (File.Exists(filename))
            {
                SharpLog.Logging.LogTrace($"Loading input for year {Year} day {Day}...");
                string input = File.ReadAllText(filename);
                input = input.TrimEnd('\n');
                return input;
            }

            SharpLog.Logging.LogTrace($"Downloading input for year {Year} day {Day}...");

            using (WebClient client = new WebClient())
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
                client.Headers.Add(HttpRequestHeader.Cookie, Cookie);
                client.DownloadFile($"https://adventofcode.com/{Year}/day/{Day}/input", filename);
            }

            return GetInput();
        }

        internal static Type GetSolution()
        {
            Type type = null;

            try
            {
                type = Type.GetType($"AdventOfCode.Solutions.Y{Year}.D{Day:D2}.Solution", true);
            }
            catch (Exception ex)
            {
                SharpLog.Logging.LogFatal("Solution class was not found!", exception: ex);
            }

            return type;
        }

        internal static Type GetParser()
        {
            Type type = null;

            try
            {
                type = Type.GetType($"AdventOfCode.Solutions.Y{Year}.D{Day:D2}.Parser", true);
            }
            catch (Exception ex)
            {
                SharpLog.Logging.LogFatal("Solution class was not found!", exception: ex);
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
            string clipboard = string.Empty;

            try
            {
                SharpLog.Logging.LogDebug("Solving puzzle 1...");
                stopwatch.Restart();
                clipboard = (string)solutionType
                    .GetMethod("Puzzle1", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(solution, new object[] { parsedInput });
                stopwatch.Stop();
                SharpLog.Logging.LogDebug($"Took {stopwatch.ElapsedMilliseconds}ms to excecute.");

                SharpLog.Logging.LogDebug("Solving puzzle 2...");
                stopwatch.Restart();
                clipboard = (string)solutionType
                    .GetMethod("Puzzle2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(solution, new object[] { parsedInput });
                stopwatch.Stop();
                SharpLog.Logging.LogDebug($"Took {stopwatch.ElapsedMilliseconds}ms to excecute.");
            }
            catch (Exception exc)
            {
                if (exc.ToString().Contains("SolutionNotImplementedException"))
                {
                    SharpLog.Logging.LogWarning("Solution is not implemented!");
                }
                else
                {
                    throw exc;
                }
            }

            return clipboard;
        }

        internal static object Parse(Type parserType, string input)
        {
            SharpLog.Logging.LogDebug("Parsing input...");

            try
            {
                var parser = parserType.GetConstructor(new Type[0]).Invoke(null);
                Stopwatch stopwatch = Stopwatch.StartNew();
                var parsedInput = parserType
                    .GetMethod("Parse", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(parser, new string[] { input });
                stopwatch.Stop();
                SharpLog.Logging.LogDebug($"Took {stopwatch.ElapsedMilliseconds}ms to excecute.");
                return parsedInput;
            }
            catch (Exception exc)
            {
                if (exc.Message.Contains("ParserNotImplementedException"))
                {
                    SharpLog.Logging.LogError("Parser is not implemented!");
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
