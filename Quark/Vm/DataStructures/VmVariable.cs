using Quark.Vm.DataStructures.VmValue;

namespace Quark.Vm.DataStructures;

public class VmVariable(string name, VmValueType varType)
{
    public readonly string Name = name;
    public readonly VmValueType VarType = varType;
    private VmValue.VmValue _value;

    public VmValue.VmValue Value
    {
        get => _value;
        set
        {
            if ((value.Type & VarType) == 0) Throw.InvalidOpEx();
            _value = value;
        }
    }

    public override string ToString() => $"{Name}: {Value} ({VarType})";
}