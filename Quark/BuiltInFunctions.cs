using Quark.Vm.DataStructures.VmValues;

namespace Quark;

public static class BuiltInFunctions
{
    public static void PrintLn(VmValue value)
    {
        Print(value);
        Console.WriteLine();
    }

    public static void Print(VmValue value)
    {
        Console.Write(value.ToString());
    }
}