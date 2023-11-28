using System.Collections;
using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Time;

namespace AdventOfCode
{
    internal class ReadMeGenerator
    {
        private const string EMOJI_UNSOLVED = "❌";
        private const string EMOJI_LIGHT_SPEED = "🟩";
        private const string EMOJI_SOUND_SPEED = "🟦";
        private const string EMOJI_MODERAD_SPEED = "🟨";
        private const string EMOJI_SLOW_SPEED = "🟧";
        private const string EMOJI_SNAIL_SPEED = "🟥";

        private static readonly Dictionary<TimeSpan, string> speedMap =
            new()
            {
                { new TimeSpan(000010000), EMOJI_LIGHT_SPEED },
                { new TimeSpan(000100000), EMOJI_SOUND_SPEED },
                { new TimeSpan(001000000), EMOJI_MODERAD_SPEED },
                { new TimeSpan(010000000), EMOJI_SLOW_SPEED },
                { TimeSpan.MaxValue, EMOJI_SNAIL_SPEED },
            };

        private readonly ISolutionRetriever solutions;

        public ReadMeGenerator(ISolutionRetriever solutions)
        {
            this.solutions = solutions;
        }

        internal void Generate()
        {
            string markdown =
                $@"# My AdventOfCode solutions! 🎄

## Solutions

> [!NOTE]  <br/>
> ❌ -> Not solved yet<br/>
> 🟩 -> `< 00:00,0010000`<br/>
> 🟦 -> `< 00:00,0100000`<br/>
> 🟨 -> `< 00:00,1000000`<br/>
> 🟧 -> `< 00:01,0000000`<br/>
> 🟥 -> `> 00:01,0000000` or not timed.
";

            int year = 0;
            foreach (var date in CalendarRange.Full)
            {
                if (date.Year != year)
                {
                    year = date.Year;
                    markdown +=
                        $@"### {year}

| Day   | Evaluation | Time  | Parsing 1 | Parsing 2 | Puzzle 1 | Puzzle 2 |
| :---: | :--------: | :---: | :-------: | :-------: | :------: | :------: |
";
                }

                markdown +=
                    $"| [{date.Day:D2}](/Solutions/Y{date.Year}/D{date.Day:D2}/Solver.cs) | ";

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
                            value.Parse1.Value
                            + value.Parse2.Value
                            + value.Solve1.Value
                            + value.Solve2.Value;
                        time = $"`{timeValue:c}`";
                        emoji = speedMap[timeValue];
                    }

                    var parse1 = value.Parse1.HasValue ? $"`{value.Parse1.Value:c}`" : "`N/A`";
                    var parse2 = value.Parse2.HasValue ? $"`{value.Parse2.Value:c}`" : "`N/A`";
                    var solve1 = value.Solve1.HasValue ? $"`{value.Solve1.Value:c}`" : "`N/A`";
                    var solve2 = value.Solve2.HasValue ? $"`{value.Solve2.Value:c}`" : "`N/A`";

                    markdown += $"{emoji} | {time} | {parse1} | {parse2} | {solve1} | {solve2} |\n";
                }
                else
                {
                    markdown += $"{EMOJI_UNSOLVED} | `N/A` | `N/A` | `N/A` | `N/A` | `N/A` |\n";
                }
            }

            File.WriteAllText("README.md", markdown);
        }

        private class ClosestTimeSpanDictionary<TValue> : IDictionary<TimeSpan, TValue>
        {
            private Dictionary<TimeSpan, TValue> dictionary = new();

            public TValue this[TimeSpan key]
            {
                get
                {
                    if (this.TryGetValue(key, out TValue? value))
                    {
                        return value;
                    }

                    TimeSpan best = TimeSpan.MaxValue;
                    TimeSpan leastDifference = TimeSpan.MaxValue;
                    foreach (var timeSpan in dictionary)
                    {
                        TimeSpan difference = timeSpan.Key - key;
                        if (difference < leastDifference)
                        {
                            leastDifference = difference;
                            best = timeSpan.Key;
                        }
                    }

                    return this[best];
                }
                set { this.dictionary[key] = value; }
            }

            public ICollection<TimeSpan> Keys => this.dictionary.Keys;

            public ICollection<TValue> Values => this.dictionary.Values;

            public int Count => this.dictionary.Count;

            public bool IsReadOnly => false;

            public void Add(TimeSpan key, TValue value)
            {
                this.dictionary.Add(key, value);
            }

            public void Add(KeyValuePair<TimeSpan, TValue> item)
            {
                this.dictionary.Add(item.Key, item.Value);
            }

            public void Clear()
            {
                this.dictionary.Clear();
            }

            public bool Contains(KeyValuePair<TimeSpan, TValue> item)
            {
                return this.dictionary.Contains(item);
            }

            public bool ContainsKey(TimeSpan key)
            {
                return this.dictionary.ContainsKey(key);
            }

            public void CopyTo(KeyValuePair<TimeSpan, TValue>[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<KeyValuePair<TimeSpan, TValue>> GetEnumerator()
            {
                return this.dictionary.GetEnumerator();
            }

            public bool Remove(TimeSpan key)
            {
                return this.dictionary.Remove(key);
            }

            public bool Remove(KeyValuePair<TimeSpan, TValue> item)
            {
                if (
                    this.dictionary.TryGetValue(item.Key, out TValue? value)
                    && value is not null
                    && item.Value is not null
                )
                {
                    if (item.Value.Equals(value))
                    {
                        return this.dictionary.Remove(item.Key);
                    }

                    return false;
                }

                return this.dictionary.Remove(item.Key);
            }

            public bool TryGetValue(TimeSpan key, [MaybeNullWhen(false)] out TValue value)
            {
                return TryGetValue(key, out value);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.dictionary.GetEnumerator();
            }
        }
    }
}
