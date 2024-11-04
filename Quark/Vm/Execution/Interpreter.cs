using Quark.Vm.DataStructures;
using Quark.Vm.DataStructures.VmValue;
using Quark.Vm.Operations;
using static Quark.Vm.Execution.MathLogicOp;

namespace Quark.Vm.Execution;

public class Interpreter
{
    public readonly Stack<VmFrame> Frames = new();
    public readonly MyStack<VmValue> Stack = new();
    public bool Halted => Frames.Count == 0;

    public void Step(int stepsCount)
    {
        for (var i = 0; i < stepsCount && Frames.Count != 0; i++)
        {
            var func = Frames.Peek();
            var op = func.Ops[func.Ip];
            // Console.WriteLine($"Step {i + 1}: {op}, {func.Ip}");
            ExecuteOp(op);
            func.Ip++;
        }
    }

    private void ExecuteOp(Op op)
    {
        if (op.Type == OpType.PushConst) Stack.Push(op.Args[0]);
        else if (op.Type == OpType.MathOrLogicOp) DoMathOrLogic(op.Args[0].Get<MathLogicOp>());
        else if (op.Type == OpType.Ret) Frames.Pop();
        else if (op.Type == OpType.CallSharp)
            CallSharp(op.Args[0].Get<nint>(), op.Args[1].Get<long>(), op.Args[2].IsTrue());
        else if (op.Type == OpType.SetLocal) Frames.Peek().Variables[(int)op.Args[0].Get<long>()].Value = Stack.Pop();
        else if (op.Type == OpType.LoadLocal) Stack.Push(Frames.Peek().Variables[(int)op.Args[0].Get<long>()].Value);
        else if (op.Type == OpType.BrOp) DoBranch(op.Args[0].Get<BranchMode>(), op.Args[1].Get<long>());
        else if (op.Type == OpType.Label) DoNothing();
        else if (op.Type == OpType.Drop) Stack.Pop();
        else Throw.InvalidOpEx();
    }

    private unsafe void CallSharp(nint ptr, long argsCount, bool returnsValue)
    {
        if (!returnsValue)
        {
            if (argsCount == 0)
                ((delegate*<void>)ptr)();
            else if (argsCount == 1)
                ((delegate*<VmValue, void>)ptr)(Stack.Get(^1));
            else if (argsCount == 2)
                ((delegate*<VmValue, VmValue, void>)ptr)(Stack.Get(^1), Stack.Get(^2));
            else if (argsCount == 3)
                ((delegate*<VmValue, VmValue, VmValue, void>)ptr)(Stack.Get(^1), Stack.Get(^2), Stack.Get(^3));
            else Throw.InvalidOpEx();
        }
        else
        {
            var result = argsCount switch
            {
                0 => ((delegate*<VmValue>)ptr)(),
                1 => ((delegate*<VmValue, VmValue>)ptr)(Stack.Get(^1)),
                2 => ((delegate*<VmValue, VmValue, VmValue>)ptr)(Stack.Get(^1), Stack.Get(^2)),
                3 => ((delegate*<VmValue, VmValue, VmValue, VmValue>)ptr)(Stack.Get(^1), Stack.Get(^2), Stack.Get(^3)),
                _ => Throw.InvalidOpEx<VmValue>(),
            };
            Stack.Push(result);
        }

    }

    private void DoNothing()
    {
    }

    private void DoBranch(BranchMode branchMode, long labelIndex)
    {
        var vmFrame = Frames.Peek();

        if (branchMode == BranchMode.Basic)
        {
            vmFrame.Ip = vmFrame.Labels[(int)labelIndex].Ip;
            return;
        }

        var value = Stack.Pop();

        var isTrue = branchMode == BranchMode.IfTrue && value.IsTrue();
        var isFalse = branchMode == BranchMode.IfFalse && value.IsFalse();
        if (isTrue || isFalse)
            vmFrame.Ip = vmFrame.Labels[(int)labelIndex].Ip;
    }

    private void DoMathOrLogic(MathLogicOp op)
    {
        if (op == Not)
        {
            Stack.Push(VmCalc.Not(Stack.Pop()));
            return;
        }

        var b = Stack.Pop();
        var a = Stack.Pop();
        var c = op switch
        {
            Sum => VmCalc.Sum(a, b),
            Sub => VmCalc.Sub(a, b),
            Mul => VmCalc.Mul(a, b),
            Div => VmCalc.Div(a, b),
            Pow => VmCalc.Pow(a, b),
            And => VmCalc.And(a, b),
            Or => VmCalc.Or(a, b),
            Xor => VmCalc.Xor(a, b),
            Eq => VmCalc.Eq(a, b),
            NotEq => VmCalc.NotEq(a, b),
            _ => Throw.InvalidOpEx<VmValue>(),
        };

        Stack.Push(c);
    }
}