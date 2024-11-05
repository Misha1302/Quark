using Quark.Vm.DataStructures;
using Quark.Vm.DataStructures.VmValues;
using Quark.Vm.Execution;
using Quark.Vm.Operations;

namespace Quark.Examples;

public class Example1
{
    /// <summary>
    ///     Runs bytecode, that executing like this pseudocode:<br></br>
    ///     1. push 5.0 to stack<br></br>
    ///     2. pop the value from stack and set local variable i<br></br>
    ///     3. load local variable value into stack<br></br>
    ///     4. push 2.0 to stack<br></br>
    ///     5. pow two stack values (i**2)<br></br>
    ///     6. call csharp static function PrintLn to print the result<br></br>
    ///     7. return
    /// </summary>
    public void Execute()
    {
        var i = VmValue.Create(0, NativeI64);

        var fivePowTwo = (List<Operation>)
        [
            new Operation(OpType.PushConst, [VmValue.Create(5.0, Number)]),
            new Operation(OpType.SetLocal, [i]),

            new Operation(OpType.LoadLocal, [i]),
            new Operation(OpType.PushConst, [VmValue.Create(2.0, Number)]),
            new Operation(OpType.MathOrLogicOp, [VmValue.Create(MathLogicOp.Pow, NativeI64)]),

            new Operation(OpType.CallSharp, [..SharpCall.MakeCallSharpOperationArguments(BuiltInFunctions.PrintLn)]),

            new Operation(OpType.PushConst, [VmValue.NilValue]),
            new Operation(OpType.Ret, []),
        ];

        var module = new VmModule([new VmFunction(fivePowTwo, "Main", [new VmVariable("i", Number)])]);

        var engine = new Engine();
        var results = engine.Run(module);

        Console.WriteLine(string.Join(", ", results));
    }
}