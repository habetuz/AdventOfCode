using SharpLog;
using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Diagnostics;

namespace AdventOfCode
{
    internal static class AdventRunner
    {
        private static readonly string s_cookie;
        private static readonly int s_year;
        private static readonly int s_day;

        private static readonly Logger s_logger = new Logger()
        {
            Ident = "AdventRunner",
            LogDebug = false,
        };

        static AdventRunner()
        {
            if (!File.Exists("settings.json")) throw new FileNotFoundException("Settings file not found! Add a 'settings.json' file!");
            string settingsJsonString = File.ReadAllText("settings.json");
            JsonElement settings = JsonSerializer.Deserialize<JsonElement>(settingsJsonString);
            s_cookie = settings.GetProperty("SessionCookie").GetString();
            s_year = settings.GetProperty("Year").GetInt32();
            s_day  = settings.GetProperty("Day").GetInt32();
        }

        internal static string GetInput()
        {
            string filename = $"input/{s_year}/day{s_day:D2}.txt";
            if (File.Exists(filename))
            {
                s_logger.Log($"Loading input for year {s_year} day {s_day}...", LogType.Info);
                string input =  File.ReadAllText(filename);
                input = input.TrimEnd('\n');
                return input;
            }

            s_logger.Log($"Downloading input for year {s_year} day {s_day}...", LogType.Info);

            using (WebClient client = new WebClient())
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
                client.Headers.Add(HttpRequestHeader.Cookie, s_cookie);
                client.DownloadFile($"https://adventofcode.com/{s_year}/day/{s_day}/input", filename);
            }

            return GetInput();
        }

        internal static Type GetSolution()
        {
            return Type.GetType($"AdventOfCode.Solutions.Y{s_year}.D{s_day:D2}.Solution", true);
        }

        internal static string Solve(Type solutionType, object parsedInput)
        {
            if (parsedInput == null) return null;
            var solution = solutionType.GetConstructor(new Type[0]).Invoke(null);
            Stopwatch stopwatch = new Stopwatch();
            string clipboard = string.Empty;

            try
            {
                s_logger.Log("Solving puzzle 1...", LogType.Info);
                stopwatch.Restart();
                clipboard = (string)solutionType
                    .GetMethod("Puzzle1", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(solution, new object[] { parsedInput });
                stopwatch.Stop();
                s_logger.Log($"Took {stopwatch.ElapsedMilliseconds}ms to excecute.", LogType.Info);

                s_logger.Log("Solving puzzle 2...", LogType.Info);
                stopwatch.Restart();
                clipboard = (string)solutionType
                    .GetMethod("Puzzle2", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(solution, new object[] { parsedInput });
                stopwatch.Stop();
                s_logger.Log($"Took {stopwatch.ElapsedMilliseconds}ms to excecute.", LogType.Info);
            }
            catch (Exception exc)
            {
                if (exc.ToString().Contains("SolutionNotImplementedException"))
                {
                    s_logger.Log("Solution is not implemented!", LogType.Warning);
                }
                else
                {
                    throw exc;
                }
            }

            return clipboard;
        }

        internal static Type GetParser()
        {
            return Type.GetType($"AdventOfCode.Solutions.Y{s_year}.D{s_day:D2}.Parser", true);
        }

        internal static object Parse(Type parserType, string input)
        {
            s_logger.Log("Parsing input...", LogType.Info);

            try
            {
                var parser = parserType.GetConstructor(new Type[0]).Invoke(null);
                Stopwatch stopwatch = Stopwatch.StartNew();
                var parsedInput = parserType
                    .GetMethod("Parse", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .Invoke(parser, new string[] { input });
                stopwatch.Stop();
                s_logger.Log($"Took {stopwatch.ElapsedMilliseconds}ms to excecute.", LogType.Info);
                return parsedInput;
            } catch (Exception exc)
            {
                if (exc.Message.Contains("ParserNotImplementedException"))
                {
                    s_logger.Log("Parser is not implemented!", LogType.Error);
                } else
                {
                    throw exc;
                }
            }

            return null;
            
        }
    }
}
