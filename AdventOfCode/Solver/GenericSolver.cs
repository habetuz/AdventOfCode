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
        this.solverType = Type.GetType(
            $"AdventOfCode.Solutions.Y{date.Year}.D{date.Day:D2}.Solver"
        )!;
        if (this.solverType == null)
        {
            throw new SolutionNotImplementedException();
        }

        int genericParamCount = 0;
        // Check if type extends ISolver<...>
        Type iSolverType = null!;
        Type[] interfaces = this.solverType.GetInterfaces();
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

        this.solverInstance = Activator.CreateInstance(this.solverType)!;

        this.genericTypes = iSolverType.GenericTypeArguments;
        this.genericParamCount = genericParamCount;

        this.parseMethod = solverType.GetMethod("Parse")!;
        this.solveMethod = solverType.GetMethod("Solve")!;

        if (genericParamCount == 1)
        {
            this.forwardingPartSubmitterType = typeof(ForwardingPartSubmitter<object, object>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(object), this.genericTypes[0]);
        }
        else
        {
            this.forwardingPartSubmitterType = typeof(ForwardingPartSubmitter<
                object,
                object,
                object,
                object
            >)
                .GetGenericTypeDefinition()
                .MakeGenericType(
                    typeof(object),
                    typeof(object),
                    this.genericTypes[0],
                    this.genericTypes[1]
                );
        }
    }

    public void Parse(string input, IPartSubmitter<object, object> parsedInput)
    {
        var forwarder = Activator.CreateInstance(this.forwardingPartSubmitterType, parsedInput)!;
        this.parseMethod.Invoke(this.solverInstance, new object[] { input, forwarder });
    }

    public void Solve(object? input1, object? input2, IPartSubmitter solution)
    {
        if (this.genericParamCount == 2)
        {
            this.solveMethod.Invoke(
                this.solverInstance,
                new object?[] { input1, input2, solution }
            );
        }
        else
        {
            this.solveMethod.Invoke(this.solverInstance, new object?[] { input1, solution });
        }
    }

    public class SolutionNotImplementedException : Exception { }

    public class ISolverNotImplementedException : Exception { }
}
