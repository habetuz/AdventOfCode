using System.Collections;
using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Time;

namespace AdventOfCode;

internal class ReadMeGenerator(ISolutionRetriever solutions)
{
private const string EMOJI_UNSOLVED = "❌";

private const string EMOJI_LIGHT_SPEED = "🟩";
private static readonly TimeSpan TIME_LIGHT_SPEED = new(000010000);
private const string EMOJI_SOUND_SPEED = "🟦";
private static readonly TimeSpan TIME_SOUND_SPEED = new(000100000);
private const string EMOJI_MODERAD_SPEED = "🟨";
private static readonly TimeSpan TIME_MODERAT_SPEED = new(001000000);
private const string EMOJI_SLOW_SPEED = "🟧";
private static readonly TimeSpan TIME_SLOW_SPEED = new(010000000);
private const string EMOJI_SNAIL_SPEED = "🟥";
private static readonly TimeSpan TIME_SNAIL_SPEED = TimeSpan.MaxValue;

private static readonly ClosestTimeSpanDictionary<string> speedMap =
  new()
  {
    { TIME_LIGHT_SPEED, EMOJI_LIGHT_SPEED },
    { TIME_SOUND_SPEED, EMOJI_SOUND_SPEED },
    { TIME_MODERAT_SPEED, EMOJI_MODERAD_SPEED },
    { TIME_SLOW_SPEED, EMOJI_SLOW_SPEED },
    { TIME_SNAIL_SPEED, EMOJI_SNAIL_SPEED },
  };

private readonly ISolutionRetriever solutions = solutions;

internal void Generate()
{
  string markdown = "";

  int year = 0;

  string yearTable = "";

  foreach (var date in CalendarRange.Full)
  {
    if (date.Year != year)
    {
      year = date.Year;
      markdown = yearTable + markdown;
      yearTable =
        $@"### {year}

| Day   | Evaluation | Time  | Parsing 1 | Parsing 2 | Puzzle 1 | Puzzle 2 |
| :---: | :--------: | :---: | :-------: | :-------: | :------: | :------: |
";
    }

    yearTable +=
      $"| [{date.Day:D2}](AdventOfCode/Solutions/Y{date.Year}/D{date.Day:D2}/Solver.cs) | ";

    var solution = solutions.Retrieve(date);
    if (solution.HasValue)
    {
      var value = solution.Value;
      var emoji = EMOJI_SNAIL_SPEED;
      var time = "`N/A`";
      if (
        value.Parse1.HasValue
        && value.Parse2.HasValue
        && value.Solve1.HasValue
        && value.Solve2.HasValue
      )
      {
        var timeValue =
          value.Parse1.Value + value.Parse2.Value + value.Solve1.Value + value.Solve2.Value;
        time = $"`{timeValue:c}`";
        emoji = speedMap[timeValue];
      }

      var parse1 = value.Parse1.HasValue ? $"`{value.Parse1.Value:c}`" : "`N/A`";
      var parse2 = value.Parse2.HasValue ? $"`{value.Parse2.Value:c}`" : "`N/A`";
      var solve1 = value.Solve1.HasValue ? $"`{value.Solve1.Value:c}`" : "`N/A`";
      var solve2 = value.Solve2.HasValue ? $"`{value.Solve2.Value:c}`" : "`N/A`";

      yearTable += $"{emoji} | {time} | {parse1} | {parse2} | {solve1} | {solve2} |\n";
    }
    else
    {
      yearTable += $"{EMOJI_UNSOLVED} | `N/A` | `N/A` | `N/A` | `N/A` | `N/A` |\n";
    }
  }

  markdown = yearTable + markdown;

  markdown =
    $@"# My AdventOfCode solutions! 🎄

## Solutions

> [!NOTE]  
> ❌ -> Not solved yet<br/>
> 🟩 -> `< {TIME_LIGHT_SPEED:c}`<br/>
> 🟦 -> `< {TIME_SOUND_SPEED:c}`<br/>
> 🟨 -> `< {TIME_MODERAT_SPEED:c}`<br/>
> 🟧 -> `< {TIME_SLOW_SPEED:c}`<br/>
> 🟥 -> `> {TIME_SLOW_SPEED:c}` or not timed.
" + markdown;

  File.WriteAllText("README.md", markdown);
}

public void Save()
{
  string sourceFilePath = "README.md";
  string? destinationFilePath = ApplicationSettings.Instance.ReadmePath;
  if (destinationFilePath is null)
  {
    return;
  }

  File.Copy(sourceFilePath, destinationFilePath, true);
}

private class ClosestTimeSpanDictionary<TValue> : IDictionary<TimeSpan, TValue>
{
  private Dictionary<TimeSpan, TValue> dictionary = [];

  public TValue this[TimeSpan key]
  {
    get
    {
      if (TryGetValue(key, out TValue? value))
      {
        return value;
      }

      TimeSpan best = TimeSpan.MaxValue;
      TimeSpan leastDifference = TimeSpan.MaxValue;
      foreach (var timeSpan in dictionary)
      {
        TimeSpan difference = timeSpan.Key - key;
        if (difference >= TimeSpan.Zero && difference < leastDifference)
        {
          leastDifference = difference;
          best = timeSpan.Key;
        }
      }

      return dictionary[best];
    }
    set { dictionary[key] = value; }
  }

  public ICollection<TimeSpan> Keys => dictionary.Keys;

  public ICollection<TValue> Values => dictionary.Values;

  public int Count => dictionary.Count;

  public bool IsReadOnly => false;

  public void Add(TimeSpan key, TValue value)
  {
    dictionary.Add(key, value);
  }

  public void Add(KeyValuePair<TimeSpan, TValue> item)
  {
    dictionary.Add(item.Key, item.Value);
  }

  public void Clear()
  {
    dictionary.Clear();
  }

  public bool Contains(KeyValuePair<TimeSpan, TValue> item)
  {
    return dictionary.Contains(item);
  }

  public bool ContainsKey(TimeSpan key)
  {
    return dictionary.ContainsKey(key);
  }

  public void CopyTo(KeyValuePair<TimeSpan, TValue>[] array, int arrayIndex)
  {
    throw new NotImplementedException();
  }

  public IEnumerator<KeyValuePair<TimeSpan, TValue>> GetEnumerator()
  {
    return dictionary.GetEnumerator();
  }

  public bool Remove(TimeSpan key)
  {
    return dictionary.Remove(key);
  }

  public bool Remove(KeyValuePair<TimeSpan, TValue> item)
  {
    if (
      dictionary.TryGetValue(item.Key, out TValue? value)
      && value is not null
      && item.Value is not null
    )
    {
      if (item.Value.Equals(value))
      {
        return dictionary.Remove(item.Key);
      }

      return false;
    }

    return dictionary.Remove(item.Key);
  }

  public bool TryGetValue(TimeSpan key, [MaybeNullWhen(false)] out TValue value)
  {
    return dictionary.TryGetValue(key, out value);
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return dictionary.GetEnumerator();
  }
}
}
