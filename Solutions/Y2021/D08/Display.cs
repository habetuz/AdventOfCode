namespace AdventOfCode.Solutions.Y2021.D08
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Display
    {
        internal static readonly Dictionary<short, char[]> DigitSegments = new Dictionary<short, char[]>()
        {
            { 0, new char[] { 'a', 'b', 'c', 'e', 'f', 'g' } },
            { 1, new char[] { 'c', 'f' } },
            { 2, new char[] { 'a', 'c', 'd', 'e', 'g' } },
            { 3, new char[] { 'a', 'c', 'd', 'f', 'g' } },
            { 4, new char[] { 'b', 'c', 'd', 'f' } },
            { 5, new char[] { 'a', 'b', 'd', 'f', 'g' } },
            { 6, new char[] { 'a', 'b', 'd', 'e', 'f', 'g' } },
            { 7, new char[] { 'a', 'c', 'f' } },
            { 8, new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
            { 9, new char[] { 'a', 'b', 'c', 'd', 'f', 'g' } },
        };

        internal Dictionary<char, char[]> PossibleWiring = new Dictionary<char, char[]>()
        {
            { 'a', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
            { 'b', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
            { 'c', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
            { 'd', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
            { 'e', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
            { 'f', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
            { 'g', new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' } },
        };

        internal string[] Inputs;

        private readonly string[] digits;

        internal int Value
        {
            get
            {
                Dictionary<char, char> wiring = new Dictionary<char, char>();
                foreach (char c in this.PossibleWiring.Keys)
                {
                    if (this.PossibleWiring[c].Length > 1)
                    {
                        throw new Exception("Wiring is not solved yet!");
                    }

                    wiring[c] = this.PossibleWiring[c][0];
                }

                int value = 0;
                value += GetDigit(Decode(wiring, this.digits[0])) * 1000;
                value += GetDigit(Decode(wiring, this.digits[1])) * 100;
                value += GetDigit(Decode(wiring, this.digits[2])) * 10;
                value += GetDigit(Decode(wiring, this.digits[3])) * 1;

                return value;
            }
        }

        internal string[] Outputs
        {
            get { return this.digits; }
        }

        internal Display(string[] inputs, string[] digits)
        {
            this.Inputs = inputs;
            this.digits = digits;
        }

        internal static bool IsDigit(Dictionary<char, char> wiring, string segments)
        {
            segments = Decode(wiring, segments);
            return GetDigit(segments) != -1;
        }

        internal static short GetDigit(string segments)
        {
            foreach (KeyValuePair<short, char[]> digit in DigitSegments)
            {
                if (digit.Value.Length == segments.Length &&
                    digit.Value.All(c => { return segments.Contains(c); }))
                {
                    return digit.Key;
                }
            }

            return -1;
        }

        internal static string Decode(Dictionary<char, char> wiring, string segments)
        {
            string decodedSegments = string.Empty;

            foreach (char segment in segments)
            {
                decodedSegments += wiring[segment];
            }

            return decodedSegments;
        }
    }
}
