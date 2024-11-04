namespace Quark.Vm.DataStructures.VmValue;

[Flags]
public enum VmValueType : long
{
    Nil,
    Number,
    Str,
    Map,
    List,
    VmFunction,
    SharpFunction,
    NativeI64,
    Any = long.MaxValue,
}