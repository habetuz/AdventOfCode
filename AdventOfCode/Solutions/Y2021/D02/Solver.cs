using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2021.D02;

public class Solver : ISolver<KeyValuePair<Direction, int>[]>
{
    public void Parse(string input, IPartSubmitter<KeyValuePair<Direction, int>[]> partSubmitter)
    {
        // Split file into lines
        string[] lines = input.Split('\n');

        // Parsing to KeyValuePair
        List<KeyValuePair<Direction, int>> inputArray = new List<KeyValuePair<Direction, int>>();
        for (int i = 0; i < lines.Length; i++)
        {
            string[] inputPair = lines[i].Split(' ');
            if (inputPair.Length == 2 && int.TryParse(inputPair[1], out int number))
            {
                inputArray.Add(
                    new KeyValuePair<Direction, int>(
                        (Direction)Enum.Parse(typeof(Direction), inputPair[0], ignoreCase: true),
                        number
                    )
                );
            }
        }

        partSubmitter.Submit(inputArray.ToArray());
    }

    public void Solve(KeyValuePair<Direction, int>[] input, IPartSubmitter partSubmitter)
    {
        int posHorizontal = 0;
        int posVertical = 0;
        for (int i = 0; i < input.Length; i++)
        {
            switch (input[i].Key)
            {
                case Direction.Forward:
                    posHorizontal += input[i].Value;
                    break;
                case Direction.Up:
                    posVertical -= input[i].Value;
                    break;
                case Direction.Down:
                    posVertical += input[i].Value;
                    break;
            }
        }

        partSubmitter.SubmitPart1(posHorizontal * posVertical);

        posHorizontal = 0;
        posVertical = 0;
        int aim = 0;
        for (int i = 0; i < input.Length; i++)
        {
            switch (input[i].Key)
            {
                case Direction.Forward:
                    posHorizontal += input[i].Value;
                    posVertical += input[i].Value * aim;
                    break;
                case Direction.Up:
                    aim -= input[i].Value;
                    break;
                case Direction.Down:
                    aim += input[i].Value;
                    break;
            }
        }

        partSubmitter.SubmitPart2(posHorizontal * posVertical);
    }
}

public enum Direction
{
    Forward,
    Up,
    Down,
}
