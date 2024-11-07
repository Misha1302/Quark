using Quark.Vm.DataStructures;
using Quark.Vm.DataStructures.VmValues;
using Quark.Vm.Execution;
using Quark.Vm.Operations;

namespace Quark.Vm.Examples;

public class ConsoleLogger
{
    public void Log(Operation op, int i, VmFuncFrame frame, MyStack<VmValue> stack)
    {
        Console.WriteLine(
            $"Step {i + 1}:".PadRight(20, ' ') +
            $"{op}".PadRight(50, ' ') +
            $"{frame.Ip}".PadRight(10, ' ') +
            $"[{stack}]"
        );
    }
}