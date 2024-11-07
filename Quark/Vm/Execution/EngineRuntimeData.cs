using Quark.Vm.DataStructures;
using Quark.Vm.DataStructures.VmValues;
using Quark.Vm.Operations;

namespace Quark.Vm.Execution;

public record EngineRuntimeData(
    VmModule Module,
    Action<Operation, int, VmFuncFrame, MyStack<VmValue>>? LogAction,
    List<Interpreter> Interpreters);