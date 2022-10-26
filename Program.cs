// <copyright file="Program.cs" company="Marvin Fuchs">

namespace AdventOfCode
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using SharpLog;

    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            int year = 0;
            int day = 0;
            string cookie = string.Empty;

            /// Args:
            /// 0: year or day (20XX or XX) or -h/-help/h/help for argument explenation.
            /// 1: year or day (20XX or XX)
            /// 2: cookie

            try
            {
                if (args[0] == "-h" || args[0] == "-help" || args[0] == "h" || args[0] == "help")
                {
                    Logging.LogInfo(
                        "Usage of the advent of code runner:\n" +
                        "Run one day:\n" +
                        ">> AdventOfCode.exe <Year> <Day> <Session Cookie>", "RUNNER");

                    return;
                }

                year = int.Parse(args[0]);
                day = int.Parse(args[1]);
                cookie = args[2];
            }
            catch (FormatException e)
            {
                Logging.LogFatal("Day or year (argument 0 or 1) are not integers! Use '-h' for help.", "RUNNER", exception: e);
            }
            catch (IndexOutOfRangeException)
            {
                Logging.LogFatal("Not all arguemts are provided! Use '-h' for help.", "RUNNER");
            }

            string input = AdventRunner.GetInput(year, day, cookie);

            // Parsing
            Type parserType = AdventRunner.GetParser(year, day);
            var parsedInput = AdventRunner.Parse(parserType, input);

            // Solution
            Type solutionType = AdventRunner.GetSolution(year, day);
            string clipboard = AdventRunner.Solve(solutionType, parsedInput);
            if (clipboard != null && clipboard.Length != 0)
            {
                Clipboard.SetText(clipboard);
            }
        }
    }
}
