using Quark.Vm.Operations;

namespace Quark.Vm.DataStructures;

public record VmFrame(string Name, List<Op> Ops, int Ip, List<VmVariable> Variables, List<Label> Labels)
{
    public int Ip = Ip;

    public VmFrame(VmFunction func) : this(func.Name, func.Ops, 0, [], func.Labels)
    {
        foreach (var variable in func.Variables)
            Variables.Add(new VmVariable(variable.Name, variable.VarType));
    }
}