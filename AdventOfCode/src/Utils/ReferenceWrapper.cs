namespace AdventOfCode.Utils;

public class ReferenceWrapper<T>(T value)
{
  public T Value { get; set; } = value;
}
