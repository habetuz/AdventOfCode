// <copyright file="Program.cs" company="Marvin Fuchs">

namespace AdventOfCode
{
    using CommandLine;
    using Pastel;
    using SharpLog;
    using Spectre.Console;
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

            [Option("debug", HelpText = "Wether debug logs are activated.")]
            public bool Debug { get; set; }
        }

        [STAThread]
        private static void Main(string[] args)
        {
            var options = CommandLine.Parser.Default.ParseArguments<Options>(args).Value;

            SettingsManager.Settings.Levels.Debug.Enabled = options.Debug;

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
            else
            {
                var rule = new Rule($"Running [yellow]{options.Year}[/] / [yellow]{options.Day}[/]")
                {
                    Style = Style.Parse("green"),
                    Border = BoxBorder.Double,
                };
                AnsiConsole.Write(new Padder(rule).Padding(0, 0, 0, 1));
            }

            string input = AdventRunner.GetInput(options.Year, options.Day, options.Test, client);

            bool successfully = true;

            // Parsing
            Type parserType = AdventRunner.GetParser(options.Year, options.Day);
            var parsedInput = AdventRunner.Parse(parserType, input, options.Debug, ref successfully);

            // Solution
            Type solutionType = AdventRunner.GetSolution(options.Year, options.Day);
            string clipboard = AdventRunner.Solve(solutionType, parsedInput, options.Debug, ref successfully);
            if (clipboard != null && clipboard.Length != 0)
            {
                Clipboard.SetText(clipboard);
            }

            AdventRunner.PrintResults(successfully);

            handler.Dispose();
            client.Dispose();
        }
    }
}
