using Quark.Vm.DataStructures.VmValues;

namespace Quark.Vm.Operations;

public readonly struct Operation(OpType type, List<VmValue> args)
{
    public readonly OpType Type = type;
    public readonly List<VmValue> Args = args;

    public override string ToString() => $"{Type} [{string.Join(", ", Args)}]";
}