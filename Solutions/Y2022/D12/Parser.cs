namespace AdventOfCode.Solutions.Y2022.D12
{
    using AdventOfCode.Common;
    using SharpLog;

    internal class Parser : Parser<(Node[,], Node, Node)>
    {
        internal override (Node[,], Node, Node) Parse(string input)
        {
            var lines = input.Split('\n');

            var map = new Node[lines[0].Length, lines.Length];
            Node start = null;
            Node end = null;

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    map[x, y] = new Node
                    {
                        Height = lines[y][x],
                        X = x,
                        Y = y,
                    };

                    if (map[x, y].Height == 'S')
                    {
                        map[x, y].Height = 'a';
                        start = map[x, y];
                    }
                    else if (map[x, y].Height == 'E')
                    {
                        map[x, y].Height = 'z';
                        end = map[x, y];
                    }
                }
            }

            return (map, start, end);
        }
    }
}
