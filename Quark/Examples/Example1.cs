using Quark.Vm.DataStructures;
using Quark.Vm.DataStructures.VmValue;
using Quark.Vm.Execution;
using Quark.Vm.Operations;

namespace Quark.Examples;

public class Example1
{
    public void Execute()
    {
        var i = VmValue.New(0, NativeI64);

        var fivePowTwo = (List<Op>)
        [
            new Op(OpType.PushConst, [VmValue.New(5.0, Number)]),
            new Op(OpType.SetLocal, [i]),

            new Op(OpType.LoadLocal, [i]),
            new Op(OpType.PushConst, [VmValue.New(2.0, Number)]),
            new Op(OpType.MathOrLogicOp, [VmValue.New(MathLogicOp.Pow, NativeI64)]),

            new Op(OpType.CallSharp, [..SharpCall.New(BuiltInFunctions.PrintLn)]),

            new Op(OpType.Ret, []),
        ];

        var module = new VmModule([new VmFunction(fivePowTwo, "Main", [new VmVariable("i", Number)])]);

        var engine = new Engine();
        var results = engine.Run(module);

        Console.WriteLine(string.Join(", ", results));
    }
}