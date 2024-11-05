using Quark.Vm.DataStructures;
using Quark.Vm.DataStructures.VmValues;
using Quark.Vm.Execution;
using Quark.Vm.Operations;

namespace Quark.Examples;

public class Example2
{
    /// <summary>
    ///     Runs bytecode that prints numbers from 1 to 10 separated by space
    /// </summary>
    public void Execute()
    {
        var i = VmValue.Create(0, NativeI64);
        var endLabel = VmValue.Create(0, NativeI64);
        var startLabel = VmValue.Create(1, NativeI64);

        var forLoop = (List<Operation>)
        [
            new Operation(OpType.PushConst, [VmValue.Create(0.0, Number)]),
            new Operation(OpType.SetLocal, [i]),

            new Operation(OpType.Label, [VmValue.CreateRef("start", Str), startLabel]),

            new Operation(OpType.LoadLocal, [i]),
            new Operation(OpType.PushConst, [VmValue.Create(10.0, Number)]),
            new Operation(OpType.MathOrLogicOp, [MathLogicOps.Eq]),
            new Operation(OpType.BrOp, [BranchModes.IfTrue, endLabel]),

            new Operation(OpType.LoadLocal, [i]),
            new Operation(OpType.PushConst, [VmValue.Create(1.0, Number)]),
            new Operation(OpType.MathOrLogicOp, [MathLogicOps.Sum]),
            new Operation(OpType.SetLocal, [i]),

            new Operation(OpType.LoadLocal, [i]),
            new Operation(OpType.CallSharp, [..SharpCall.MakeCallSharpOperationArguments(BuiltInFunctions.Print)]),

            new Operation(OpType.PushConst, [VmValue.CreateRef(" ", Str)]),
            new Operation(OpType.CallSharp, [..SharpCall.MakeCallSharpOperationArguments(BuiltInFunctions.Print)]),

            new Operation(OpType.BrOp, [BranchModes.Basic, startLabel]),

            new Operation(OpType.Label, [VmValue.CreateRef("end", Str), endLabel]),

            new Operation(OpType.PushConst, [VmValue.CreateRef("\n", Str)]),
            new Operation(OpType.CallSharp, [..SharpCall.MakeCallSharpOperationArguments(BuiltInFunctions.Print)]),
            new Operation(OpType.PushConst, [VmValue.NilValue]),
            new Operation(OpType.Ret, []),
        ];

        var module = new VmModule([new VmFunction(forLoop, "Main", [new VmVariable("i", Number)])]);

        var engine = new Engine();
        var results = engine.Run(module);

        Console.WriteLine(string.Join(", ", results));
    }
}