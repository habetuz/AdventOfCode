using System.Reflection;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Time;
using SharpLog;

namespace AdventOfCode.Solver;

public class GenericSolver : ISolver<object, object>
{
    private readonly object solverInstance;
    private readonly Type solverType;
    private readonly MethodInfo parseMethod;
    private readonly MethodInfo solveMethod;
    private readonly int solveMethodParamCount;

    public GenericSolver(Date date)
    {
        this.solverType = Type.GetType($"AdventOfCode.Solutions.Y{date.Year}.D{date.Day:D2}.Solver")!;
        if (this.solverType == null)
        {
            throw new SolutionNotImplementedException();
        }

        // Check if type extends ISolver<...>
        Type[] interfaces = this.solverType.GetInterfaces();
        if (!interfaces.Any((interfaceType) =>
        {
            //Logging.LogDebug($"{interfaceType.Namespace + "." + interfaceType.Name} == {typeof(ISolver<object, object>).Namespace + "." + typeof(ISolver<object, object>).Name}");
            return interfaceType.Namespace + "." + interfaceType.Name == typeof(ISolver<object, object>).Namespace + "." + typeof(ISolver<object, object>).Name ||
            interfaceType.Namespace + "." + interfaceType.Name == typeof(ISolver<object>).Namespace + "." + typeof(ISolver<object>).Name;
        }))
        {
            throw new ISolverNotImplementedException();
        }

        this.solverInstance = Activator.CreateInstance(this.solverType)!;

        this.parseMethod = solverType.GetMethod("Parse")!;
        this.solveMethod = solverType.GetMethod("Solve")!;
        this.solveMethodParamCount = this.solveMethod.GetParameters().Length;
    }

    public void Parse(string input, IPartSubmitter<object, object> parsedInput)
    {
        this.parseMethod.Invoke(this.solverInstance, new object[] { input, parsedInput });
    }

    public void Solve(object input1, object input2, IPartSubmitter solution)
    {
        if (this.solveMethodParamCount == 3)
        {
            this.solveMethod.Invoke(this.solverInstance, new object[] { input1, input2, solution });
        }
        else
        {
            this.solveMethod.Invoke(this.solverInstance, new object[] { input1, solution });
        }
    }

    public class SolutionNotImplementedException : Exception
    {

    }

    public class ISolverNotImplementedException : Exception
    {

    }
}