using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D03;

public class Solver : ISolver<char[,]>
{
  public void Parse(string input, IPartSubmitter<char[,]> partSubmitter)
  {
    partSubmitter.Submit(Utils.Array2D.FromString(input));
  }

  public void Solve(char[,] input, IPartSubmitter partSubmitter)
  {
    uint sum = 0;
    for (int y = 0; y < input.GetLength(1); y++)
    {
      for (int x = 0; x < input.GetLength(0); x++)
      {
        // Check for symbol
        if (input[x, y] < '0' || input[x, y] > '9')
        {
          continue;
        }

        bool isPartNumber = false;

        if (
          x - 1 >= 0
          && (
            (y - 1 >= 0 && input[x - 1, y - 1] != '.')
            || input[x - 1, y] != '.'
            || (y + 1 < input.GetLength(1) && input[x - 1, y + 1] != '.')
          )
        )
        {
          isPartNumber = true;
        }

        // Parse the number
        int number = 0;
        int startX = x;
        while (x < input.GetLength(0) && input[x, y] >= '0' && input[x, y] <= '9')
        {
          number = number * 10 + (input[x, y] - '0');
          if (
            !isPartNumber
            && (
              (y - 1 >= 0 && input[x, y - 1] != '.')
              || (y + 1 < input.GetLength(1) && input[x, y + 1] != '.')
            )
          )
          {
            isPartNumber = true;
          }
          x++;
        }

        if (
          !isPartNumber
          && x < input.GetLength(0)
          && (
            (y - 1 >= 0 && input[x, y - 1] != '.')
            || input[x, y] != '.'
            || (y + 1 < input.GetLength(1) && input[x, y + 1] != '.')
          )
        )
        {
          isPartNumber = true;
        }

        if (isPartNumber)
        {
          sum += (uint)number;
        }

        x--;
      }
    }

    partSubmitter.SubmitPart1(sum);

    sum = 0;

    for (int y = 0; y < input.GetLength(1); y++)
    {
      for (int x = 0; x < input.GetLength(0); x++)
      {
        if (input[x, y] != '*')
        {
          continue;
        }

        int number1 = -1;
        int number2 = -1;

        bool valide = true;

        Array2D.IterateAroundCoordinate(
          input,
          (x, y),
          (array, coordinate, direction) =>
          {
            (var x, var y) = coordinate;
            int startX = (int)x;

            if (array[x, y] < '0' || array[x, y] > '9')
            {
              return Direction.None;
            }

            while (x - 1 >= 0 && array[x - 1, y] >= '0' && array[x - 1, y] <= '9')
            {
              x--;
            }

            int number = 0;
            long leftX = x;

            do
            {
              number = number * 10 + (array[x, y] - '0');
              x++;
            } while (x < array.GetLength(0) && array[x, y] >= '0' && array[x, y] <= '9');

            x--;
            long rightX = x;

            if (number1 == -1)
            {
              number1 = number;
            }
            else if (number2 == -1)
            {
              number2 = number;
            }
            else
            {
              valide = false;
              return Direction.All;
            }

            Direction toSkip = Direction.None;

            if (direction == Direction.UpLeft)
            {
              toSkip |= Direction.Up;

              if (rightX - startX >= 1)
              {
                toSkip |= Direction.UpRight;
              }
            }
            else if (direction == Direction.DownRight)
            {
              toSkip |= Direction.Down;

              if (startX - leftX >= 1)
              {
                toSkip |= Direction.DownLeft;
              }
            }
            else if (direction == Direction.Up)
            {
              toSkip |= Direction.UpRight;
            }
            else if (direction == Direction.Down)
            {
              toSkip |= Direction.DownLeft;
            }

            return toSkip;
          }
        );

        if (number1 == -1 || number2 == -1 || !valide)
        {
          continue;
        }

        sum += (uint)(number1 * number2);
      }
    }

    partSubmitter.SubmitPart2(sum);
  }
}
