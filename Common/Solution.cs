using SharpLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    internal abstract class Solution<T>
    {
        internal class SolutionNotImplementedException : Exception { }

        protected static readonly Logger s_logger = new Logger()
        {
            Ident = "Solution",
        };

        internal virtual string Puzzle1(T input)
        {
            throw new SolutionNotImplementedException();
        }

        internal virtual string Puzzle2(T input)
        {
            throw new SolutionNotImplementedException();
        }
    }
}
