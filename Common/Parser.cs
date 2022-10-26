namespace AdventOfCode.Common
{
    using System;
    using SharpLog;

    internal abstract class Parser<T>
    {
        internal virtual T Parse(string input)
        {
            throw new ParserNotImplementedException();
        }

        internal class ParserNotImplementedException : Exception { }
    }
}
