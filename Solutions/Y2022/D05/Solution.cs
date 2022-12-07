namespace AdventOfCode.Solutions.Y2022.D05
{
    using AdventOfCode.Common;
    using SharpLog;

    internal class Solution : Solution<(char[,], int[], (int, int, int)[])>
    {
        internal override (object clipboard, string message) Puzzle1((char[,], int[], (int, int, int)[]) input)
        {
            var cargo = (char[,])input.Item1.Clone();
            var cargoHeight = (int[])input.Item2.Clone();

            var moves = input.Item3;

            for (int move = 0; move < input.Item3.Length; move++)
            {
                (var amount, var from, var to) = moves[move];

                for (int i = 0; i < amount; i++)
                {
                    cargo[to, cargoHeight[to]] = cargo[from, cargoHeight[from] - 1];
                    cargoHeight[to]++;
                    cargoHeight[from]--;
                }
            }

            var message = string.Empty;

            for (int i = 0; i < cargo.GetLength(0); i++)
            {
                message += cargo[i, cargoHeight[i] - 1];
            }

            return (message, $"The message is {message}!");
        }

        internal override (object clipboard, string message) Puzzle2((char[,], int[], (int, int, int)[]) input)
        {
            var cargo = (char[,])input.Item1.Clone();
            var cargoHeight = (int[])input.Item2.Clone();

            var moves = input.Item3;

            for (int move = 0; move < input.Item3.Length; move++)
            {
                (var amount, var from, var to) = moves[move];

                for (int i = 0; i < amount; i++)
                {
                    cargo[to, cargoHeight[to]] = cargo[from, cargoHeight[from] - amount + i];
                    cargoHeight[to]++;
                }

                cargoHeight[from] -= amount;
            }

            var message = string.Empty;

            for (int i = 0; i < cargo.GetLength(0); i++)
            {
                message += cargo[i, cargoHeight[i] - 1];
            }

            return (message, $"The message is {message}!");
        }
    }
}
