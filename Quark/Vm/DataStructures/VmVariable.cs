using Quark.Vm.DataStructures.VmValues;

namespace Quark.Vm.DataStructures;

public class VmVariable(string name, VmValueType varType)
{
    private VmValue _value;
    public string Name { get; } = name;
    public VmValueType VarType { get; } = varType;

    public VmValue Value
    {
        get => _value;
        set
        {
            if ((value.Type & VarType) == 0) Throw.InvalidOpEx();
            _value = value;
        }
    }

    public override string ToString() => this.ToStringExtension();
}