using Quark.Vm.Execution;

namespace Quark.Vm.DataStructures.VmValue;

public readonly struct VmValue
{
    public readonly VmValueType Type = Nil;

    private readonly long _value = 0;
    private readonly object _ref = null!;

    private VmValue(object @ref, VmValueType type)
    {
        _ref = @ref;
        Type = type;
    }

    private VmValue(long value, VmValueType type)
    {
        _value = value;
        Type = type;
    }

    public static readonly VmValue NilValue = new();

    public static VmValue New(Func<MyStack<VmValue>, VmValue> f) =>
        new(f.Method.MethodHandle.GetFunctionPointer(), SharpFunction);

    public static VmValue New<T>(T value, VmValueType type) where T : unmanaged
    {
        if (typeof(T) == typeof(int))
            return new VmValue((int)(object)value, type);

        if (typeof(T).IsEnum)
            Throw.Assert(Marshal.SizeOf(Enum.GetUnderlyingType(typeof(T))) == 8);

        else if (Marshal.SizeOf<T>() != 8)
            Throw.InvalidOpEx();

        return new VmValue(Unsafe.BitCast<T, long>(value), type);
    }

    public static VmValue NewRef<T>(T value, VmValueType type) where T : class => new(value, type);

    public T Get<T>() where T : unmanaged => Unsafe.BitCast<long, T>(_value);

    public T GetRef<T>() where T : class => (T)_ref;

    public override string ToString() => this.ToStringExt();

    // ReSharper disable once CompareOfFloatsByEqualityOperator
    public bool IsTrue() => Get<double>() == 1.0;
    public bool IsFalse() => Get<double>() == 0.0;
}