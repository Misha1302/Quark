using Quark.Vm.DataStructures.VmValue;

namespace Quark.Vm.Execution;

public static class MathLogicOps
{
    public static VmValue Sum => VmValue.New(MathLogicOp.Sum, NativeI64);
    public static VmValue Sub => VmValue.New(MathLogicOp.Sub, NativeI64);
    public static VmValue Mul => VmValue.New(MathLogicOp.Mul, NativeI64);
    public static VmValue Div => VmValue.New(MathLogicOp.Div, NativeI64);
    public static VmValue Pow => VmValue.New(MathLogicOp.Pow, NativeI64);
    public static VmValue And => VmValue.New(MathLogicOp.And, NativeI64);
    public static VmValue Or => VmValue.New(MathLogicOp.Or, NativeI64);
    public static VmValue Xor => VmValue.New(MathLogicOp.Xor, NativeI64);
    public static VmValue Not => VmValue.New(MathLogicOp.Not, NativeI64);
    public static VmValue Eq => VmValue.New(MathLogicOp.Eq, NativeI64);
    public static VmValue NotEq => VmValue.New(MathLogicOp.NotEq, NativeI64);
}