using Quark.Vm.DataStructures;
using Quark.Vm.DataStructures.VmValues;
using Quark.Vm.Operations;

namespace Quark.Vm.Execution;

public class Engine
{
    private EngineRuntimeData _engineRuntimeData = null!;

    public List<VmValue> Run(VmModule module,
        Action<Operation, int, VmFuncFrame, MyStack<VmValue>>? logIntoConsole = null)
    {
        var output = new List<VmValue>();

        InitRuntimeData(module, logIntoConsole);
        InitMainInterpreter(module);
        ExecuteEveryInterpreter(output);

        return output;
    }

    private void InitMainInterpreter(VmModule module)
    {
        var item = new Interpreter();
        item.Frames.Push(new VmFuncFrame(module["Main"]));
        _engineRuntimeData.Interpreters.Add(item);
    }

    private void InitRuntimeData(VmModule module, Action<Operation, int, VmFuncFrame, MyStack<VmValue>>? logIntoConsole)
    {
        _engineRuntimeData = new EngineRuntimeData(module, logIntoConsole, []);
    }

    private void ExecuteEveryInterpreter(List<VmValue> output)
    {
        while (_engineRuntimeData.Interpreters.Count > 0)
        {
            foreach (var interpreter in _engineRuntimeData.Interpreters)
                interpreter.Step(1000, _engineRuntimeData);

            RemoveHaltedInterpreters(output);
        }
    }

    private void RemoveHaltedInterpreters(List<VmValue> output)
    {
        for (var i = _engineRuntimeData.Interpreters.Count - 1; i >= 0; i--)
            if (_engineRuntimeData.Interpreters[i].Halted)
            {
                output.Add(_engineRuntimeData.Interpreters[i].Stack.Pop());
                _engineRuntimeData.Interpreters.RemoveAt(i);
            }
    }
}