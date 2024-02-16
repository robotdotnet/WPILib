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
public partial class TestRootClass
{
    [Log]
    public readonly TestClass TCField = new();


    [Log]
    public TestClass TCMethod() => new();

    [Log]
    public TestClass TC { get; } = new();
    [Log]
    public TestStruct TS { get; } = new();
}

[GenerateLog]
public partial class TestClass
{
    [Log]
    public TestStruct TS { get; } = new();
}

[GenerateLog]
public partial struct TestStruct
{
    [Log]
    public Rotation2d Rotation { get; set; } = new();

    public TestStruct() { }
}

[GenerateLog]
public partial class TestDemo
{
    [Log]
    public Rotation2d Rotation { get; set; } = new();
}
