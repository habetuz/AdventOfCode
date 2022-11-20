namespace AdventOfCode.Common
{
    using System;

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
