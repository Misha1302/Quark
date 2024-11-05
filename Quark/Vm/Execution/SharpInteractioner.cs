using Quark.Vm.DataStructures.VmValues;

namespace Quark.Vm.Execution;

public static class SharpInteractioner
{
    public static unsafe void CallStaticSharpFunction(
        MyStack<VmValue> stack, nint ptr, long argsCount, bool returnsValue
    )
    {
        // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/function-pointers
        // use function-pointers to call it faster than usual csharp delegates
        if (!returnsValue)
        {
            // call with no return value
            if (argsCount == 0)
                ((delegate*<void>)ptr)();
            else if (argsCount == 1)
                ((delegate*<VmValue, void>)ptr)(stack.Get(^1));
            else if (argsCount == 2)
                ((delegate*<VmValue, VmValue, void>)ptr)(stack.Get(^1), stack.Get(^2));
            else if (argsCount == 3)
                ((delegate*<VmValue, VmValue, VmValue, void>)ptr)(stack.Get(^1), stack.Get(^2), stack.Get(^3));
            else Throw.InvalidOpEx();
        }
        else
        {
            // call with VmValue return type
            var result = argsCount switch
            {
                0 => ((delegate*<VmValue>)ptr)(),
                1 => ((delegate*<VmValue, VmValue>)ptr)(stack.Get(^1)),
                2 => ((delegate*<VmValue, VmValue, VmValue>)ptr)(stack.Get(^1), stack.Get(^2)),
                3 => ((delegate*<VmValue, VmValue, VmValue, VmValue>)ptr)(stack.Get(^1), stack.Get(^2), stack.Get(^3)),
                _ => Throw.InvalidOpEx<VmValue>(),
            };
            stack.Push(result);
        }

        stack.DropMany(argsCount);
    }
}