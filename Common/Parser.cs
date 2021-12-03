﻿using SharpLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    internal abstract class Parser<T>
    {
        protected static readonly Logger s_logger = new Logger()
        {
            Ident = "Parser",
            LogDebug = true,
        };

        protected ProgressTracker s_progressTracker;

        internal virtual T Parse(string input)
        {
            throw new NotImplementedException();
        }
    }
}
