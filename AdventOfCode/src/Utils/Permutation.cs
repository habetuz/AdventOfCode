namespace AdventOfCode.Utils;

public static class Permutation
{
  public static IEnumerable<T[]> Permutate<T>(T[] values, int count, bool allowDuplicates = true)
  {
    int maxValue = (int)(Math.Pow(values.Length, count) - 1);
    for (int permutationIndex = 0; permutationIndex <= maxValue; permutationIndex++)
    {
      T[] permutation = Enumerable.Repeat(values[0], count).ToArray();
      int value = permutationIndex;
      int valueIndex = count;
      do
      {
        permutation[--valueIndex] = values[value % values.Length];
        value /= values.Length;
      } while (value > 0);

      if (!allowDuplicates && HasDuplicates(permutation)) {
        continue;
      }

      yield return permutation;
    }
  }

  public static bool HasDuplicates<T>(IEnumerable<T> values) {
    HashSet<T> registered = [];
    return values.Any((value) => {
      if (registered.Contains(value)) {
        return true;
      } else {
        registered.Add(value);
        return false;
      }
    });
  }
}
