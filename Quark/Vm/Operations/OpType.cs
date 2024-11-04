namespace Quark.Vm.Operations;

public enum OpType
{
    PushConst,
    MathOrLogicOp,
    LoadLocal,
    SetLocal,
    BrOp,
    CallSharp,
    CallFunc,
    Ret,
    Label,
    Drop
}