namespace AdventOfCode.Solutions.Y2022.D05
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using AdventOfCode.Common;
    using SharpLog;

    internal class Parser : Parser<(char[,], int[], (int, int, int)[])>
    {
        internal override (char[,], int[], (int, int, int)[]) Parse(string input)
        {
            var cargoInput = input
                .Substring(0, input.IndexOf("\n\n"))
                .Split('\n')
                .Reverse()
                .ToArray();

            var movementInput = input
                .Substring(input.IndexOf("\n\n") + 2);

            var movementString = Regex.Matches(movementInput, "[0-9]+")
                .Cast<Match>()
                .Select(match => match.Value)
                .ToArray();

            var movement = new (int, int, int)[movementString.Length / 3];

            var cargo = new char[(cargoInput[0].Length + 1) / 4, cargoInput.Length * cargoInput[0].Length];

            var cargoHeight = new int[cargo.GetLength(0)];

            for (int y = 0; y < cargoInput.Length - 1; y++)
            {
                for (int x = 0; x < cargo.GetLength(0); x++)
                {
                    cargo[x, y] = cargoInput[y + 1][(x * 4) + 1];

                    if (cargo[x, y] != ' ')
                    {
                        cargoHeight[x] = y + 1;
                    }
                }
            }

            for (int i = 0; i < movement.Length; i++)
            {
                movement[i] = (
                    movementString[i * 3].Length == 1 ? movementString[i * 3][0] - '0' : ((movementString[i * 3][0] - '0') * 10) + movementString[i * 3][1] - '0',
                    movementString[(i * 3) + 1][0] - '1',
                    movementString[(i * 3) + 2][0] - '1');
            }

            return (cargo, cargoHeight, movement);
        }
    }
}
