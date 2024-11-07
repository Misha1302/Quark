using Quark.Vm.DataStructures;
using Quark.Vm.DataStructures.VmValues;
using Quark.Vm.Execution;
using Quark.Vm.Operations;

namespace Quark.Vm.Examples;

public class Example3
{
    public void Execute()
    {
        var i = VmValue.Create(0, NativeI64);
        var endLabel = VmValue.Create(0, NativeI64);
        var startLabel = VmValue.Create(1, NativeI64);

        var forLoop = (List<Operation>)
        [
            // new Operation(OpType.PushConst, [VmValue.CreateRef("", Str)]),
            // new Operation(OpType.CallSharp, [..SharpCall.MakeCallSharpOperationArguments(BuiltInFunctions.Import)]),

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
            new Operation(OpType.LoadLocal, [i]),
            new Operation(OpType.PushConst, [VmValue.Create(2.0, Number)]),
            new Operation(OpType.MathOrLogicOp, [MathLogicOps.Sub]),
            new Operation(OpType.CallFunc, [VmValue.Create(1, NativeI64)]),
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


        var x = VmValue.Create(0, NativeI64);
        var y = VmValue.Create(1, NativeI64);

        var fxyOps = (List<Operation>)
        [
            new Operation(OpType.SetLocal, [y]),
            new Operation(OpType.SetLocal, [x]),

            new Operation(OpType.LoadLocal, [x]),
            new Operation(OpType.PushConst, [VmValue.Create(2.0, Number)]),
            new Operation(OpType.MathOrLogicOp, [MathLogicOps.Pow]),

            new Operation(OpType.LoadLocal, [y]),
            new Operation(OpType.MathOrLogicOp, [MathLogicOps.Div]),
            new Operation(OpType.Ret, []),
        ];


        var main = new VmFunction(forLoop, "Main", [new VmVariable("i", Number)]);
        var fxy = new VmFunction(fxyOps, "Fxy", [new VmVariable("x", Number), new VmVariable("y", Number)]);
        var module = new VmModule([main, fxy]);

        var engine = new Engine();
        var logger = new ConsoleLogger();
        var results = engine.Run(module, logger.Log);

        Console.WriteLine(string.Join(", ", results));
    }
}