using System.Globalization;

namespace Quark.Vm.DataStructures.VmValue;

public static class VmValueExt
{
    public static string ToStringExt(this VmValue value)
    {
        return value.Type switch
        {
            Nil => "Nil",
            Number => value.Get<double>().ToString(CultureInfo.InvariantCulture),
            Str => value.GetRef<string>(),
            Map => Throw.InvalidOpEx<string>(),
            List => string.Join(", ", value.GetRef<List<VmValue>>()),
            VmValueType.VmFunction => value.GetRef<VmFunction>().Name,
            SharpFunction => value.Get<nint>().ToString("X"),
            NativeI64 => $"n_{value.Get<long>()}",
            Any => $"any: {value.Get<long>()}",
            _ => Throw.InvalidOpEx<string>()
        };
    }
}