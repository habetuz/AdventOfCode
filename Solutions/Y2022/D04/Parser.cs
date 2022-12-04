namespace AdventOfCode.Solutions.Y2022.D04
{
    using AdventOfCode.Common;

    internal class Parser : Parser<((byte, byte), (byte, byte))[]>
    {
        internal override ((byte, byte), (byte, byte))[] Parse(string input)
        {
            var lines = input.Split('\n');

            var output = new ((byte, byte), (byte, byte))[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                var pair = lines[i].Split(',');
                var leftRange = pair[0].Split('-');
                var rightRange = pair[1].Split('-');
                output[i] = (
                    (byte.Parse(leftRange[0]), byte.Parse(leftRange[1])),
                    (byte.Parse(rightRange[0]), byte.Parse(rightRange[1])));
            }

            return output;
        }
    }
}
