namespace AdventOfCode.Solutions.Y2022.D11
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;

    internal class Parser : Parser<Monkey[]>
    {
        internal override Monkey[] Parse(string input)
        {
            var lines = input.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            var output = new Monkey[lines.Length / 6];

            for (int i = 0; i < lines.Length; i += 6)
            {
                var items = new Queue<int>(Regex.Matches(lines[i + 1], "[0-9]+")
                    .Cast<Match>()
                    .Select(match => int.Parse(match.Value))
                    .ToArray());

                var operation = lines[i + 2][23];
                var operatorString = lines[i + 2].Substring(25);

                var @operator = operatorString == "old" ? -1 : int.Parse(operatorString);

                var dividend = int.Parse(lines[i + 3].Substring(21));

                var caseTrue = int.Parse(lines[i + 4].Substring(29));
                var caseFalse = int.Parse(lines[i + 5].Substring(30));

                output[i / 6] = new Monkey(
                    items,
                    operation,
                    @operator,
                    dividend,
                    caseTrue,
                    caseFalse,
                    i / 6);
            }

            return output;
        }
    }
}
