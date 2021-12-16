using SharpLog;
using System;

namespace AdventOfCode.Common
{
    internal abstract class Parser<T>
    {
        internal class ParserNotImplementedException : Exception { }

        protected static readonly Logger s_logger = new Logger()
        {
            Ident = "Parser",
        };

        internal virtual T Parse(string input)
        {
            throw new ParserNotImplementedException();
        }
    }
}
