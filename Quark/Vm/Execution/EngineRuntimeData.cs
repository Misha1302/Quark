using Quark.Vm.DataStructures;

namespace Quark.Vm.Execution;

public record EngineRuntimeData(VmModule Module)
{
    public readonly List<Interpreter> Interpreters = [];
}