using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AdventOfCode.Solver.Runner
{
    public class TimedRunner : ISolverRunner
    {
        private readonly static TimeSpan MaxTime = new TimeSpan(0, 0, 5);
        private readonly static TimeSpan WarmupTime = new TimeSpan(0, 0, 0, 0, 100);

        private readonly ISolver<object, object> solver;
        private readonly string input;

        public TimedRunner(ISolver<object, object> solver, string input)
        {
            this.solver = solver;
            this.input = input;
        }
        public Solution Run()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2);
            }

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            
        }
    }
}