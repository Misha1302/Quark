using Quark.Vm.DataStructures.VmValue;

namespace Quark.Vm.Operations;

public readonly struct Op(OpType type, List<VmValue> args)
{
    public readonly OpType Type = type;
    public readonly List<VmValue> Args = args;

    public override string ToString() => $"{Type} [{string.Join(", ", Args)}]";
}