using System.Reflection;
using AdventOfCode.PartSubmitter;
using AdventOfCode.Time;
using SharpLog;

namespace AdventOfCode.Solver;

public class GenericSolver : ISolver<object, object>
{
    private readonly object solverInstance;
    private readonly Type solverType;
    private readonly Type[] genericTypes;
    private readonly MethodInfo parseMethod;
    private readonly MethodInfo solveMethod;
    private int genericParamCount;

    private readonly Type forwardingPartSubmitterType;

    public GenericSolver(Date date)
    {
        solverType = Type.GetType($"AdventOfCode.Solutions.Y{date.Year}.D{date.Day:D2}.Solver")!;
        if (solverType == null)
        {
            throw new SolutionNotImplementedException();
        }

        int genericParamCount = 0;
        // Check if type extends ISolver<...>
        Type iSolverType = null!;
        Type[] interfaces = solverType.GetInterfaces();
        if (
            !interfaces.Any(
                (interfaceType) =>
                {
                    if (
                        interfaceType.Namespace + "." + interfaceType.Name
                        == typeof(ISolver<object, object>).Namespace
                            + "."
                            + typeof(ISolver<object, object>).Name
                    )
                    {
                        genericParamCount = 2;
                        iSolverType = interfaceType;
                        return true;
                    }
                    else if (
                        interfaceType.Namespace + "." + interfaceType.Name
                        == typeof(ISolver<object>).Namespace + "." + typeof(ISolver<object>).Name
                    )
                    {
                        genericParamCount = 1;
                        iSolverType = interfaceType;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            )
        )
        {
            throw new ISolverNotImplementedException();
        }

        solverInstance = Activator.CreateInstance(solverType)!;

        genericTypes = iSolverType.GenericTypeArguments;
        this.genericParamCount = genericParamCount;

        parseMethod = solverType.GetMethod("Parse")!;
        solveMethod = solverType.GetMethod("Solve")!;

        if (genericParamCount == 1)
        {
            forwardingPartSubmitterType = typeof(ForwardingPartSubmitter<object, object>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(object), genericTypes[0]);
        }
        else
        {
            forwardingPartSubmitterType = typeof(ForwardingPartSubmitter<
                object,
                object,
                object,
                object
            >)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(object), typeof(object), genericTypes[0], genericTypes[1]);
        }
    }

    public void Parse(string input, IPartSubmitter<object, object> parsedInput)
    {
        var forwarder = Activator.CreateInstance(forwardingPartSubmitterType, parsedInput)!;
        parseMethod.Invoke(solverInstance, new object[] { input, forwarder });
    }

    public void Solve(object? input1, object? input2, IPartSubmitter solution)
    {
        if (genericParamCount == 2)
        {
            solveMethod.Invoke(solverInstance, new object?[] { input1, input2, solution });
        }
        else
        {
            solveMethod.Invoke(solverInstance, new object?[] { input1, solution });
        }
    }

    public class SolutionNotImplementedException : Exception { }

    public class ISolverNotImplementedException : Exception { }
}
