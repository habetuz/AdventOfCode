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
            string clipboard = AdventRunner.Solve(solutionType, parsedInput);
            if (clipboard != null && clipboard.Length != 0) Clipboard.SetText(clipboard);

            Console.ReadLine();
        }
    }
}
