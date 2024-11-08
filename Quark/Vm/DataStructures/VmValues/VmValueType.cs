namespace Quark.Vm.DataStructures.VmValues;

/// <summary>
///     because we provide the ability to have complex types (for example [Number | Str] or just [Any]),
///     we need to provide the ability to combine VmValueType values
/// </summary>
[Flags]
public enum VmValueType : long
{
    // every enum value is the power of two. To have an ability to mix types we need to take every bit by order 
    Nil = 1 << 0,
    Number = 1 << 1,
    Str = 1 << 2,
    Map = 1 << 3,
    List = 1 << 4,
    VmFunction = 1 << 5,
    SharpFunction = 1 << 6,
    NativeI64 = 1 << 7,

    // long.MaxValue - 0b111111111111111111111111111111111111111111111111111111111111111
    // just 63 enabled bits. It means that Any contains any type
    Any = long.MaxValue,
}