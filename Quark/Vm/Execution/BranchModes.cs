using Quark.Vm.DataStructures.VmValues;

namespace Quark.Vm.Execution;

public static class BranchModes
{
    public static VmValue Basic => VmValue.Create(BranchMode.Basic, NativeI64);
    public static VmValue IfTrue => VmValue.Create(BranchMode.IfTrue, NativeI64);
    public static VmValue IfFalse => VmValue.Create(BranchMode.IfFalse, NativeI64);
}