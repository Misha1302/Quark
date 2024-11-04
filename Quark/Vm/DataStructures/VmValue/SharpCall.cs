namespace Quark.Vm.DataStructures.VmValue;

public static class SharpCall
{
    public static List<VmValue> New(Delegate func) =>
    [
        VmValue.New(func.Method.MethodHandle.GetFunctionPointer(), SharpFunction),
        VmValue.New(func.Method.GetParameters().Length, NativeI64),
        VmValue.New(func.Method.ReturnType == typeof(VmValue) ? 1.0 : 0.0, Number),
    ];
}