using WPIMath.Geometry;

namespace Monologue.Test;

[GenerateLog]
public partial class GenerateGenericClass<T>
{
    [Log]
    public int Variable { get; }
    [Log]
    public Int32 Variable2 { get; }
}

[GenerateLog]
public partial class GenerateGenericClassConstraint<T> where T : struct
{
    [Log]
    public int Variable { get; }
}


[GenerateLog]
public partial class GenerateClass
{
    [Log]
    public int Variable { get; }
}

[GenerateLog]
public partial struct GenerateStruct
{
    [Log]
    public int Variable { get; }
}

[GenerateLog]
public readonly partial struct GenerateReadonlyStruct
{
    [Log]
    public int Variable { get; }
}

// [GenerateLog]
// public ref partial struct GenerateRefStruct
// {
//     [Log]
//     public int Variable { get; }
// }

// [GenerateLog]
// public readonly ref partial struct GenerateReadonlyRefStruct
// {
//     [Log]
//     public int Variable { get; }
// }

// [GenerateLog]
// public partial interface GenerateInterface
// {
//     [Log]
//     public int Variable { get; }
// }

[GenerateLog]
public partial record GenerateRecord
{
    [Log]
    public int Variable { get; }
}

[GenerateLog]
public partial record struct GenerateRecordStruct
{
    [Log]
    public int Variable { get; }
}

[GenerateLog]
public readonly partial record struct GenerateReadonlyRecordStruct
{
    [Log]
    public int Variable { get; }
}

[GenerateLog]
public partial record class GenerateRecordClass
{
    [Log]
    public int Variable { get; }
}

[GenerateLog]
public partial class GenerateAllKnownTypes
{
    [Log(LogType = LogType.None)]
    public bool boolVar;
    [Log]
    public char charVar;
    [Log]
    public byte byteVar;
    [Log]
    public sbyte sbyteVar;
    [Log]
    public short shortVar;
    [Log]
    public ushort ushortVar;
    [Log]
    public int intVar;
    [Log]
    public uint uintVar;
    [Log]
    public long longVar;
    [Log]
    public ulong ulongVar;
    [Log]
    public float floatVar;
    [Log]
    public double doubleVar;
    [Log]
    public string? stringVar;

    [Log]
    public long[]? longArrVar;
    [Log]
    public float[]? floatArrVar;
    [Log]
    public double[]? doubleArrVar;
    [Log]
    public string?[]? stringArrVar;
    [Log]
    public byte[]? rawVar;
    [Log]
    public bool[]? boolArrVar;

    [Log]
    public ReadOnlySpan<float> FloatROSpan => new();
    [Log]
    public ReadOnlySpan<double> DoubleROSpan => new();
    [Log]
    public ReadOnlySpan<long> LongROSpan => new();
    [Log]
    public ReadOnlySpan<string?> StringROSpan => new();
    [Log]
    public ReadOnlySpan<byte> RawROSpan => new();
    [Log]
    public ReadOnlySpan<bool> BoolROSpan => new();

    [Log]
    public Span<float> FloatSpan => new();
    [Log]
    public Span<double> DoubleSpan => new();
    [Log]
    public Span<long> LongSpan => new();
    [Log]
    public Span<string?> StringSpan => new();
    [Log]
    public Span<byte> RawSpan => new();
    [Log]
    public Span<bool> BoolSpan => new();
}
