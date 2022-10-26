namespace AdventOfCode.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SharpLog;

    internal abstract class Solution<T>
    {
        internal virtual (object clipboard, string message) Puzzle1(T input)
        {
            throw new SolutionNotImplementedException();
        }

        internal virtual (object clipboard, string message) Puzzle2(T input)
        {
            throw new SolutionNotImplementedException();
        }

        internal class SolutionNotImplementedException : Exception { }
    }
}
