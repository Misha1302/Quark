using Quark.Vm.DataStructures.VmValue;

namespace Quark.Vm.Execution;

public static class BranchModes
{
    public static VmValue Basic => VmValue.New(BranchMode.Basic, NativeI64);
    public static VmValue IfTrue => VmValue.New(BranchMode.IfTrue, NativeI64);
    public static VmValue IfFalse => VmValue.New(BranchMode.IfFalse, NativeI64);
}