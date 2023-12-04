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
    public static void IterateAroundCoordinate<T>(T[,] array, int x, int y, IterateAroundCoordinateCallback<T> callback, Direction toSkip = Direction.None)
    {
        if (!toSkip.HasFlag(Direction.TopLeft) && x - 1 >= 0 && y - 1 >= 0)
        {
            toSkip |= callback(array, x - 1, y - 1, Direction.TopLeft);
        }

        if (!toSkip.HasFlag(Direction.Top) && y - 1 >= 0)
        {
            toSkip |= callback(array, x, y - 1, Direction.Top);
        }

        if (!toSkip.HasFlag(Direction.TopRight) && x + 1 < array.GetLength(0) && y - 1 >= 0)
        {
            toSkip |= callback(array, x + 1, y - 1, Direction.TopRight);
        }

        if (!toSkip.HasFlag(Direction.Left) && x - 1 >= 0)
        {
            toSkip |= callback(array, x - 1, y, Direction.Left);
        }

        if (!toSkip.HasFlag(Direction.Left) && x + 1 < array.GetLength(0))
        {
            toSkip |= callback(array, x + 1, y, Direction.Right);
        }

        if (!toSkip.HasFlag(Direction.BottomLeft) && x - 1 >= 0 && y + 1 < array.GetLength(1))
        {
            toSkip |= callback(array, x - 1, y + 1, Direction.BottomLeft);
        }

        if (!toSkip.HasFlag(Direction.Bottom) && y + 1 < array.GetLength(1))
        {
            toSkip |= callback(array, x, y + 1, Direction.Bottom);
        }

        if (!toSkip.HasFlag(Direction.BottomRight) && x + 1 < array.GetLength(0) && y + 1 < array.GetLength(1))
        {
            toSkip |= callback(array, x + 1, y + 1, Direction.BottomRight);
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
    public delegate Direction IterateAroundCoordinateCallback<T>(T[,] array, int x, int y, Direction direction);
}
