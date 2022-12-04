// <copyright file="Program.cs" company="Marvin Fuchs">

namespace AdventOfCode
{
    using CommandLine;
    using SharpLog;
    using Spectre.Console;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Windows.Forms;

    internal static class Program
    {
        public class Options
        {
            [Option('y', "year", Required = true, HelpText = "The year you want to load. 20XX...20XX for a span of years.")]
            public string Year { get; set; }

            [Option('d', "day", Required = true, HelpText = "The day you want to load. XX...XX for a span of days.")]
            public string Day { get; set; }

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
            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("(github.com/habetuz/AdventOfCode by mail@marvin-fuchs.de)"));

            string[] years = options.Year.Split(new string[] { "..." }, StringSplitOptions.None);
            string[] days = options.Day.Split(new string[] { "..." }, StringSplitOptions.None);

            int startYear = 0;
            int startDay = 0;
            int endYear = 0;
            int endDay = 0;

            // Parse and valided year(s)
            if (!int.TryParse(years[0], out startYear))
            {
                Logging.LogFatal($"The format of the year option is not valide! Value should be a number: {years[0]}");
            }

            if (years.Length < 2)
            {
                endYear = startYear;
            }
            else if (!int.TryParse(years[1], out endYear))
            {
                Logging.LogFatal($"The format of the year option is not valide! Value should be a number: {years[1]}");
            }

            if (startYear < 2015 || startYear > 2022)
            {
                Logging.LogFatal($"The start year has to be between 2015 and 2022! Provided year: {startYear}");
            }

            if (endYear < startYear || endYear > 2022)
            {
                Logging.LogFatal($"The end year has to be between the start year and 2022! Provided year: {endYear}");
            }

            // Parse and validate day(s)
            if (!int.TryParse(days[0], out startDay))
            {
                Logging.LogFatal($"The format of the year option is not valide! Value should be a number: {days[0]}");
            }

            if (days.Length < 2)
            {
                endDay = startDay;
            }
            else if (!int.TryParse(days[1], out endDay))
            {
                Logging.LogFatal($"The format of the day option is not valide! Value should be a number: {days[1]}");
            }

            if (startDay < 0 || startDay > 25)
            {
                Logging.LogFatal($"The start day has to be between 0 and 25! Provided day: {startDay}");
            }

            if (endDay < startDay || endDay > 25)
            {
                Logging.LogFatal($"The end day has to be between the start day and 25! Provided day: {endDay}");
            }

            for (int y = startYear; y <= endYear; y++)
            {
                if (!options.Fast)
                {
                    WelcomePrinter.Print(y, client);
                }

                for (int d = startDay; d <= endDay; d++)
                {
                    var rule = new Rule($"Running [yellow]{y}[/] / [yellow]{d}[/]")
                    {
                        Style = Style.Parse("green"),
                        Border = BoxBorder.Double,
                    };

                    AnsiConsole.Write(new Padder(rule).Padding(0, 0, 0, 1));

                    string input = AdventRunner.GetInput(y, d, options.Test, client);

                    input = input.Replace("\r", string.Empty);

                    // Parse input (if it is a test input)
                    if (options.Test >= 0)
                    {
                        string[] lines = input.Split('\n');
                        string[] settings = lines[0].Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);
                        settings[0] = settings[0].Substring(3);

                        AdventRunner.ExcpectedPuzzle1 = settings[0] == "-" ? null : settings[0];
                        AdventRunner.ExcpectedPuzzle2 = settings[1] == "-" ? null : settings[1];

                        input = string.Join("\n", lines.Skip(1).ToArray());
                    }

                    bool successfully = true;

                    // Parsing
                    Type parserType = AdventRunner.GetParser(y, d);
                    if (parserType == null)
                    {
                        AdventRunner.PrintResults(successfully: false);
                        continue;
                    }

                    var parsedInput = AdventRunner.Parse(parserType, input, options.Debug, ref successfully);

                    // Solution
                    Type solutionType = AdventRunner.GetSolution(y, d);
                    if (solutionType == null)
                    {
                        AdventRunner.PrintResults(successfully: false);
                        continue;
                    }

                    string clipboard = AdventRunner.Solve(solutionType, parsedInput, options.Debug, ref successfully);
                    if (clipboard != null && clipboard.Length != 0)
                    {
                        Clipboard.SetText(clipboard);
                    }

                    AdventRunner.PrintResults(successfully);
                }
            }

            handler.Dispose();
            client.Dispose();
        }
    }
}
