using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using SharpLog;

namespace AdventOfCode.Solutions.Y2023.D12;

public class Solver : ISolver<Line[], Line[]>
{
    public void Parse(string input, IPartSubmitter<Line[], Line[]> partSubmitter)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var result1 = new Line[lines.Length];
        var result2 = new Line[lines.Length];
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var data = parts[0];
            var bytes = parts[1].Split(',').Select((x) => byte.Parse(x)).ToArray();

            result1[i] = new Line(data, bytes);
            result2[i] = new Line(
                string.Join('?', Enumerable.Repeat(data, 5)),
                Enumerable.Repeat(bytes, 5).Aggregate((x, y) => x.Concat(y).ToArray())
            );
        }

        partSubmitter.SubmitPart1(result1);
        partSubmitter.SubmitPart2(result2);
    }

    public void Solve(Line[] input1, Line[] input2, IPartSubmitter partSubmitter)
    {
        long combinations = input1.Sum(
            (line) =>
            {
                Dictionary<State, long> cache = [];
                var arrangements = GetArrangements(
                    new State { CurrentChar = line.Data[0] },
                    line,
                    cache
                );
                return arrangements;
            }
        );

        partSubmitter.SubmitPart1(combinations);

        combinations = input2.Sum(
            (line) =>
            {
                Dictionary<State, long> cache = [];
                var arrangements = GetArrangements(
                    new State { CurrentChar = line.Data[0] },
                    line,
                    cache
                );
                return arrangements;
            }
        );

        partSubmitter.SubmitPart2(combinations);
    }

    private long GetArrangements(State state, Line line, Dictionary<State, long> cache)
    {
        switch (state.CurrentChar)
        {
            case '?':
                state.CurrentChar = '#';
                long a = GetArrangements(state, line, cache);
                state.CurrentChar = '.';
                long b = GetArrangements(state, line, cache);

                return a + b;

            case '.':
                if (state.DamagedCounter > 0)
                {
                    if (
                        state.GroupPointer == line.Groups.Length
                        || line.Groups[state.GroupPointer] != state.DamagedCounter
                    )
                    {
                        return 0;
                    }

                    state.DamagedCounter = 0;
                    state.GroupPointer++;
                }
                break;
            case '#':
                state.DamagedCounter++;
                if (
                    state.GroupPointer == line.Groups.Length
                    || state.DamagedCounter > line.Groups[state.GroupPointer]
                )
                {
                    return 0;
                }
                break;
        }

        state.DataPointer++;

        if (state.DataPointer == line.Data.Length)
        {
            if (
                state.GroupPointer == line.Groups.Length
                || (
                    state.GroupPointer == line.Groups.Length - 1
                    && state.DamagedCounter == line.Groups[state.GroupPointer]
                )
            )
            {
                return 1;
            }

            return 0;
        }
        else
        {
            state.CurrentChar = line.Data[state.DataPointer];
            if (cache.TryGetValue(state, out var value))
            {
                return value;
            }

            long arrangements = GetArrangements(state, line, cache);
            cache.Add(state, arrangements);
            return arrangements;
        }
    }

    private string FormatArrangement(string data, uint arrangement, bool valid = false)
    {
        var result = new char[data.Length];
        byte unknownCounter = 0;

        for (int i = 0; i < data.Length; i++)
        {
            switch (data[i])
            {
                case '?':
                    if ((arrangement & (1 << unknownCounter)) != 0)
                    {
                        result[i] = '#';
                    }
                    else
                    {
                        result[i] = '.';
                    }

                    unknownCounter++;
                    break;
                case '#':
                    result[i] = '#';
                    break;
                case '.':
                    result[i] = '.';
                    break;
            }
        }

        return new string(result);
    }
}
