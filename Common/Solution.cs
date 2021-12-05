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
        protected static readonly Logger s_logger = new Logger()
        {
            Ident = "Solution",
        };

        protected ProgressTracker s_progressTracker;

        internal virtual string Puzzle1(T input)
        {
            throw new NotImplementedException();
        }

        internal virtual string Puzzle2(T input)
        {
            throw new NotImplementedException();
        }
    }
}
