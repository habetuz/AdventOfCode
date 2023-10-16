using System.Reflection;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Time;

namespace AdventOfCode.Solver;

public class GenericSolver : ISolver<object, object>
{
    private readonly object solverInstance;
    private readonly MethodInfo parseMethod;
    private readonly MethodInfo solveMethod;

    public GenericSolver(Date date)
    {
        Type? solverType = Type.GetType($"AdventOfCode.Solutions.Y{date.Year}.D{date.Day:D2}.Solver");
        if (solverType == null)
        {
            throw new SolutionNotImplementedException();
        }

        this.solverInstance = Activator.CreateInstance(solverType)!;
        this.parseMethod = solverType.GetMethod("Parse")!;
        this.parseMethod = solverType.GetMethod("Solve")!;
    }

    public void Parse(string input, IPartSubmitter<object, object> parsedInput)
    {
        throw new NotImplementedException();
    }

    public void Solve(object input1, object input2, IPartSubmitter solution)
    {
        throw new NotImplementedException();
    }

    public class SolutionNotImplementedException : Exception
    {

    }
}