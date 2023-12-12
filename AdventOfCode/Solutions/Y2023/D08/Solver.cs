using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D08;

public class Solver : ISolver<Input>
{
    public void Parse(string input, IPartSubmitter<Input> partSubmitter)
    {
        var lines = input.Split(
            '\n',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
        );
        var directionSequence = new Direction[lines[0].Length];
        foreach (var (c, i) in lines[0].Select((c, i) => (c, i)))
        {
            directionSequence[i] = c switch
            {
                'L' => Direction.Left,
                'R' => Direction.Right,
                _ => throw new Exception("Invalid direction")
            };
        }

        lines = lines[1..];
        string[][] nodesSerialized = lines
            .Select(
                l => l.Split([" = (", ", ", ")"], StringSplitOptions.RemoveEmptyEntries).ToArray()
            )
            .ToArray();

        var nodes = new BiNode<string>[nodesSerialized.Length];

        foreach (var (node, i) in nodesSerialized.Select((node, i) => (node, i)))
        {
            nodes[i] = new BiNode<string>(node[0]);
        }

        foreach (var (node, i) in nodesSerialized.Select((node, i) => (node, i)))
        {
            var left = nodes.First(n => n.Value == node[1]);
            var right = nodes.First(n => n.Value == node[2]);
            nodes[i].Left = left;
            nodes[i].Right = right;
        }

        partSubmitter.Submit(new Input(directionSequence, nodes));
    }

    public void Solve(Input input, IPartSubmitter partSubmitter)
    {
        int steps = 0;
        BiNode<string> position = input.Nodes.First(n => n.Value == "AAA");
        do
        {
            position = position.GetDirection(
                input.DirectionSequence[steps % input.DirectionSequence.Length]
            )!;
            steps++;
        } while (position.Value != "ZZZ");

        partSubmitter.SubmitPart1(steps);

        BiNode<string>[] positions = input
            .Nodes
            .Where((node) => node.Value.EndsWith('A'))
            .ToArray();
        var stepsPerPosition = new int[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            steps = 0;
            position = positions[i];
            do
            {
                position = position.GetDirection(
                    input.DirectionSequence[steps % input.DirectionSequence.Length]
                )!;
                steps++;
            } while (!position.Value.EndsWith('Z'));
            stepsPerPosition[i] = steps;
        }

        partSubmitter.SubmitPart2(AdventMath.lcm(stepsPerPosition));
    }
}
