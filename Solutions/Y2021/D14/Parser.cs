namespace AdventOfCode.Solutions.Y2021.D14
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdventOfCode.Common;

    internal class Parser : Parser<(Dictionary<string, char>, string)>
    {
        internal override (Dictionary<string, char>, string) Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, char> rules = new Dictionary<string, char>();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] rule = lines[i].Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                rules.Add(rule[0], rule[1][0]);
            }

            return (rules, lines[0]);
        }
    }
}
