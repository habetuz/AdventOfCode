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
            SharpLog.Logging.LogInfo(
            "         |                                                             \n" +
            "        -+-                                                            ", "RED");
            SharpLog.Logging.LogInfo(
            "         A                                                             \n" +
            "        /=\\               /\\  /\\    ___  _ __  _ __ __    __        \n" +
            "      i/ O \\i            /  \\/  \\  / _ \\| '__|| '__|\\ \\  / /     \n" +
            "      /=====\\           / /\\  /\\ \\|  __/| |   | |    \\ \\/ /      \n" +
            "      /  i  \\           \\ \\ \\/ / / \\___/|_|   |_|     \\  /       \n" +
            "    i/ O * O \\i                                       / /             \n" +
            "    /=========\\        __  __                        /_/    _         \n" +
            "    /  *   *  \\        \\ \\/ /        /\\  /\\    __ _  ____  | |    \n" +
            "  i/ O   i   O \\i       \\  /   __   /  \\/  \\  / _` |/ ___\\ |_|    \n" +
            "  /=============\\       /  \\  |__| / /\\  /\\ \\| (_| |\\___ \\  _   \n" +
            "  /  O   i   O  \\      /_/\\_\\      \\ \\ \\/ / / \\__,_|\\____/ |_| \n" +
            "i/ *   O   O   * \\i                                                   \n" +
            "/=================\\                                                   ", "GREEN");
            SharpLog.Logging.LogInfo(
            "       |___|                                                           ", "YELLOW");

            string input = AdventRunner.GetInput();

            // Parsing
            Type parserType = AdventRunner.GetParser();
            var parsedInput = AdventRunner.Parse(parserType, input);

            // Solution
            Type solutionType = AdventRunner.GetSolution();
            string clipboard = AdventRunner.Solve(solutionType, parsedInput);
            if (clipboard != null && clipboard.Length != 0)
            {
                Clipboard.SetText(clipboard);
            }

            Console.ReadLine();
        }
    }
}
