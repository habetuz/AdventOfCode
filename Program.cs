using SharpLog;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AdventOfCode
{
    internal static class Program
    {
        private static readonly Logger s_logger = new Logger()
        {
            Ident = "Program",
            LogDebug = true,
        };

        [STAThread]
        static void Main(string[] args)
        {
            string input = AdventRunner.GetInput();
            Stopwatch stopwatch = new Stopwatch();

            // Parsing
            Type parserType = AdventRunner.GetParser();
            var parsedInput = AdventRunner.Parse(parserType, input);

            // Solution
            Type solutionType = AdventRunner.GetSolution();
            Clipboard.SetText(AdventRunner.Solve(solutionType, parsedInput));

            Console.ReadLine();
        }
    }
}
