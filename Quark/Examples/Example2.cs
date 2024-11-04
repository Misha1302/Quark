using Quark.Vm.DataStructures;
using Quark.Vm.DataStructures.VmValue;
using Quark.Vm.Execution;
using Quark.Vm.Operations;

namespace Quark.Examples;

public class Example2
{
    public void Execute()
    {
        var i = VmValue.New(0, NativeI64);
        var endLabel = VmValue.New(0, NativeI64);
        var startLabel = VmValue.New(1, NativeI64);

        var forLoop = (List<Op>)
        [
            new Op(OpType.PushConst, [VmValue.New(0.0, Number)]),
            new Op(OpType.SetLocal, [i]),

            new Op(OpType.Label, [VmValue.NewRef("start", Str), startLabel]),

            new Op(OpType.LoadLocal, [i]),
            new Op(OpType.PushConst, [VmValue.New(10.0, Number)]),
            new Op(OpType.MathOrLogicOp, [MathLogicOps.Eq]),
            new Op(OpType.BrOp, [BranchModes.IfTrue, endLabel]),

            new Op(OpType.LoadLocal, [i]),
            new Op(OpType.PushConst, [VmValue.New(1.0, Number)]),
            new Op(OpType.MathOrLogicOp, [MathLogicOps.Sum]),
            new Op(OpType.SetLocal, [i]),

            new Op(OpType.LoadLocal, [i]),
            new Op(OpType.CallSharp, [VmValue.New(Buildin.Print)]),
            new Op(OpType.Drop, []),

            new Op(OpType.PushConst, [VmValue.NewRef(" ", Str)]),
            new Op(OpType.CallSharp, [VmValue.New(Buildin.Print)]),

            new Op(OpType.Drop, []),
            new Op(OpType.BrOp, [BranchModes.Basic, startLabel]),

            new Op(OpType.Label, [VmValue.NewRef("end", Str), endLabel]),
            
            new Op(OpType.PushConst, [VmValue.NewRef("\n", Str)]),
            new Op(OpType.CallSharp, [VmValue.New(Buildin.Print)]),
            new Op(OpType.PushConst, [VmValue.NilValue]),
            new Op(OpType.Ret, []),
        ];

        var module = new VmModule([new VmFunction(forLoop, "Main", [new VmVariable("i", Number)])]);

        var engine = new Engine();
        var results = engine.Run(module);

        Console.WriteLine(string.Join(", ", results));
    }
}