using WPIMath.Geometry;

namespace Stereologue.Test;

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

[GenerateLog]
public ref partial struct GenerateRefStruct
{
    [Log]
    public int Variable { get; }
}

[GenerateLog]
public readonly ref partial struct GenerateReadonlyRefStruct
{
    [Log]
    public int Variable { get; }
}

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
    [Log(Key = "CustomKey")]
    public char charVar;
    [Log(LogLevel = LogLevel.OverrideFileOnly)]
    public byte byteVar;
    [Log(LogType = LogType.File | LogType.Nt | LogType.Once)]
    public sbyte sbyteVar;
    public short shortVar;
    [Log(Key = null!)]
    public ushort ushortVar;
    [Log(LogType = LogType.File)]
    public int intVar;
    [Log(LogType = LogType.File)]
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
    public IntPtr intptrVar;
    [Log]
    public UIntPtr uintptrVar;
    [Log]
    public nint nintVar;
    [Log]
    public nuint nuintVar;
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

    [Log]
    public Rotation2d Rotation => new();

    [Log(UseProtobuf = true)]
    public Rotation2d RotationProto => new();

    [Log(UseProtobuf = true)]
    public Rotation2d? RotationProtoNullable => null;

    [Log()]
    public Rotation2d[] RotationArray = [];

    // [Log()]
    // public Rotation2d?[] RotationNullableArray = [];

    // [Log(UseProtobuf = true)]
    // public Rotation2d[] RotationProtoArray = [];

    // [Log(UseProtobuf = true)]
    // public Rotation2d?[] RotationProtoNullableArray = [];

    // [Log]
    // public object Object = null!;

    // [Log]
    // public unsafe int* PtrValue;

    // [Log]
    // public IEnumerable<bool> BoolEnumerable = null!;

    // [Log]
    // public int[] intArr = null!;

    // [Log]
    // public DateTime dateTime;

    // [Log]
    // public DateTime[] dateTimeArray = null!;

    // [Log]
    // public void GetThing() { }

    // [Log]
    // public int GetIntThing(int x) => x;

    [Log]
    public int? NullableInt;

    [Log]
    public GenerateStruct NonNullStruct;
    [Log]
    public GenerateStruct? NullStruct;

    [Log]
    public GenerateClass NonNullClass = null!;
    [Log]
    public GenerateClass? NullClass;
    [Log]
    public GenerateClass[] ClassArray = null!;
    [Log]
    public GenerateClass?[] NullableClassArray = null!;
    [Log]
    public GenerateStruct?[] NullableStructArray = null!;
}
