using SharpLog;
using Spectre.Console;

namespace AdventOfCode.Utils;

public static class Array2D
{
  /// <summary>
  /// Creates a 2D array from a string.
  /// </summary>
  /// <param name="input">The string to convert.</param>
  /// <returns>The converted 2D array.</returns>
  public static char[,] FromString(string input)
  {
    var lines = input.Split((char[])['\n'], StringSplitOptions.RemoveEmptyEntries);
    var array = new char[lines[0].Length, lines.Length];
    for (int y = 0; y < lines.Length; y++)
    {
      for (int x = 0; x < lines[y].Length; x++)
      {
        array[x, y] = lines[y][x];
      }
    }
    return array;
  }

  public static T[,] FromString<T>(string input, ConvertCallback<char, T> callback)
  {
    var lines = input.Split((char[])['\n'], StringSplitOptions.RemoveEmptyEntries);
    var array = new T[lines[0].Length, lines.Length];
    for (int y = 0; y < lines.Length; y++)
    {
      for (int x = 0; x < lines[y].Length; x++)
      {
        array[x, y] = callback(lines[y][x], x, y);
      }
    }
    return array;
  }

  public delegate TTo ConvertCallback<TFrom, TTo>(TFrom from, int x, int y);

  /// <summary>
  /// Iterates around a coordinate in a 2D array.
  /// Iterates from left to right.
  /// </summary>
  /// <typeparam name="T">The type of the 2D array.</typeparam>
  /// <param name="array">The array to iterate on.</param>
  /// <param name="x">The x coordinate to iterate around.</param>
  /// <param name="y">The y coordinate to iterate around.</param>
  /// <param name="callback">The callback to call for each coordinate.</param>
  /// <param name="toSkip">The directions to skip.</param>
  /// <example>
  /// 123
  /// 4x5
  /// 678
  /// </example>
  public static void IterateAroundCoordinate<T>(
    T[,] array,
    int x,
    int y,
    IterateAroundCoordinateCallback<T> callback,
    Direction toSkip = Direction.None
  )
  {
    if (!toSkip.HasFlag(Direction.UpLeft) && x - 1 >= 0 && y - 1 >= 0)
    {
      toSkip |= callback(array, x - 1, y - 1, Direction.UpLeft);
    }

    if (!toSkip.HasFlag(Direction.Up) && y - 1 >= 0)
    {
      toSkip |= callback(array, x, y - 1, Direction.Up);
    }

    if (!toSkip.HasFlag(Direction.UpRight) && x + 1 < array.GetLength(0) && y - 1 >= 0)
    {
      toSkip |= callback(array, x + 1, y - 1, Direction.UpRight);
    }

    if (!toSkip.HasFlag(Direction.Left) && x - 1 >= 0)
    {
      toSkip |= callback(array, x - 1, y, Direction.Left);
    }

    if (!toSkip.HasFlag(Direction.Left) && x + 1 < array.GetLength(0))
    {
      toSkip |= callback(array, x + 1, y, Direction.Right);
    }

    if (!toSkip.HasFlag(Direction.DownLeft) && x - 1 >= 0 && y + 1 < array.GetLength(1))
    {
      toSkip |= callback(array, x - 1, y + 1, Direction.DownLeft);
    }

    if (!toSkip.HasFlag(Direction.Down) && y + 1 < array.GetLength(1))
    {
      toSkip |= callback(array, x, y + 1, Direction.Down);
    }

    if (
      !toSkip.HasFlag(Direction.DownRight)
      && x + 1 < array.GetLength(0)
      && y + 1 < array.GetLength(1)
    )
    {
      toSkip |= callback(array, x + 1, y + 1, Direction.DownRight);
    }
  }

  /// <summary>
  /// Callback for <see cref="IterateAroundCoordinate"/>.
  /// </summary>
  /// <typeparam name="T">The type of the 2D array.</typeparam>
  /// <param name="array">The array to iterate on.</param>
  /// <param name="x">The x coordinate to iterate around.</param>
  /// <param name="y">The y coordinate to iterate around.</param>
  /// <returns>The directions to skip.</returns>
  public delegate Direction IterateAroundCoordinateCallback<T>(
    T[,] array,
    int x,
    int y,
    Direction direction
  );

  public static void Print<T>(int v, T[,] array, ConvertCallback<T, string> convertCallback)
  {
    Logging.LogInfo($"Array size: {array.GetLength(0)}x{array.GetLength(1)}");

    for (int y = 0; y < array.GetLength(1); y++)
    {
      string line = "";
      for (int x = 0; x < array.GetLength(0); x++)
      {
        line += convertCallback(array[x, y], x, y);
      }
      AnsiConsole.MarkupLine(line);
    }
  }

  public static void Print(uint sizeX, uint sizeY, PrintCallback printCallback)
  {
    Logging.LogInfo($"Array size: {sizeX}x{sizeY}");
    string str = "";

    for (int y = 0; y < sizeY; y++)
    {
      string line = "";
      for (int x = 0; x < sizeX; x++)
      {
        line += printCallback(x, y);
      }

      str += line + "\n";
    }

    AnsiConsole.Markup(str);
  }

  public delegate string PrintCallback(int x, int y);

  public static bool IsEqual<T>(T[,] array1, T[,] array2)
  {
    if (array1.GetLength(0) != array2.GetLength(0))
    {
      return false;
    }

    if (array1.GetLength(1) != array2.GetLength(1))
    {
      return false;
    }

    for (int y = 0; y < array1.GetLength(1); y++)
    {
      for (int x = 0; x < array1.GetLength(0); x++)
      {
        if (!array1[x, y]!.Equals(array2[x, y]))
        {
          return false;
        }
      }
    }

    return true;
  }

  public static void Copy<T>(T[,] source, T[,] destination)
  {
    int xLength = Math.Min(source.GetLength(0), destination.GetLength(0));
    int yLength = Math.Min(source.GetLength(1), destination.GetLength(1));

    for (int y = 0; y < yLength; y++)
    {
      for (int x = 0; x < xLength; x++)
      {
        destination[x, y] = source[x, y];
      }
    }
  }
}
