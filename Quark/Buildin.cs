using Quark.Vm.DataStructures.VmValue;
using Quark.Vm.Execution;

namespace Quark;

public static class Buildin
{
    public static VmValue PrintLn(MyStack<VmValue> stack)
    {
        Print(stack);
        Console.WriteLine();
        return VmValue.NilValue;
    }

    public static VmValue Print(MyStack<VmValue> stack)
    {
        Console.Write(stack.Pop().ToString());
        return VmValue.NilValue;
    }
}