// <copyright file="Program.cs" company="Marvin Fuchs">

namespace AdventOfCode
{
    using CommandLine;
    using Pastel;
    using SharpLog;
    using System;
    using System.Net.Http;
    using System.Windows.Forms;

    internal static class Program
    {
        public class Options
        {
            [Option('y', "year", Required = true, HelpText = "The year you want to load")]
            public int Year { get; set; }

            [Option('d', "day", Required = true, HelpText = "The day you want to load")]
            public int Day { get; set; }

            [Option('c', "cookie", Required = true, HelpText = "Your session cookie")]
            public string Cookie { get; set; }

            [Option('f', "fast", HelpText = "Wether speed is the goal (welcome screen will not be printed)")]
            public bool Fast { get; set; }

            [Option('t', "test", HelpText = "Wich test input should be used or -1 if the real input should be used.", Default = -1)]
            public int Test { get; set; }
        }

        [STAThread]
        private static void Main(string[] args)
        {
            Console.WriteLine();

            var options = CommandLine.Parser.Default.ParseArguments<Options>(args).Value;

            // Create http client
            var handler = new HttpClientHandler()
            {
                UseCookies = false,
            };

            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://adventofcode.com/)"),
            };

            client.DefaultRequestHeaders.Add("Cookie", $"session={options.Cookie}");

            if (!options.Fast)
            {
                WelcomePrinter.Print(options.Year, options.Day, options.Test, client);
            }

            if (options.Test < 0)
            {
                Logging.LogInfo($"Running {options.Year.ToString().Pastel("#ffff66")}{" / ".Pastel("#ffffff")}{options.Day.ToString().Pastel("#ffff66")}", "RUNNER");
            }
            else
            {
                Logging.LogInfo($"Running test input {options.Test.ToString().Pastel("#ffff66")}{" - y".Pastel("#ffffff")}{options.Year.ToString().Pastel("#ffff66")}{" / d".Pastel("#ffffff")}{options.Day.ToString().Pastel("#ffff66")}", "RUNNER");
            }

            string input = AdventRunner.GetInput(options.Year, options.Day, options.Test, client);

            // Parsing
            Type parserType = AdventRunner.GetParser(options.Year, options.Day);
            var parsedInput = AdventRunner.Parse(parserType, input);

            // Solution
            Type solutionType = AdventRunner.GetSolution(options.Year, options.Day);
            string clipboard = AdventRunner.Solve(solutionType, parsedInput);
            if (clipboard != null && clipboard.Length != 0)
            {
                Clipboard.SetText(clipboard);
            }

            handler.Dispose();
            client.Dispose();
        }
    }
}
