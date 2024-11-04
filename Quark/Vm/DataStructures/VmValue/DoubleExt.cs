namespace Quark.Vm.DataStructures.VmValue;

public static class DoubleExt
{
    public static long ToLong(this double value) => (long)(value + 0.5);

    public static bool ApproxEq(this double a, double b, double epsilon = 0.00001) =>
        Math.Abs(a - b) <= (Math.Abs(a) < Math.Abs(b) ? Math.Abs(b) : Math.Abs(a)) * epsilon;
}