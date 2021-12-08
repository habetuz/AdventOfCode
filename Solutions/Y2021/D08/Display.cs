using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D08
{
    internal class Display
    {
        internal static readonly Dictionary<short, char[]> DigitSegments = new Dictionary<short, char[]>()
        {
            {0, new char[] {'a', 'b', 'c', 'e', 'f', 'g'} },
            {1, new char[] {'c', 'f'} },
            {2, new char[] {'a', 'c', 'd', 'e', 'g'} },
            {3, new char[] {'a', 'c', 'd', 'f', 'g'} },
            {4, new char[] {'b', 'c', 'd', 'f'} },
            {5, new char[] {'a', 'b', 'd', 'f', 'g'} },
            {6, new char[] {'a', 'b', 'd', 'e', 'f', 'g'} },
            {7, new char[] {'a', 'c', 'g'} },
            {8, new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g'} },
            {9, new char[] {'a', 'b', 'c', 'd', 'f', 'g' } }
        };

        internal readonly Dictionary<char, char[]> PossibleWiring = new Dictionary<char, char[]>()
        {
            {'a', new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g'} },
            {'b', new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g'} },
            {'c', new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g'} },
            {'d', new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g'} },
            {'e', new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g'} },
            {'f', new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g'} },
            {'g', new char[] {'a', 'b', 'c', 'd', 'e', 'f', 'g'} },
        };

        internal string[] Inputs;

        private string[] _digits;

        internal string[] Digits
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        internal Display(string[] inputs, string[] digits)
        {
            Inputs = inputs;
            _digits = digits;
        }
    }
}
