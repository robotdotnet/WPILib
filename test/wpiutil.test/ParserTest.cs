using WPIUtil.Serialization.Struct.Parsing;
using Xunit;

namespace WPIUtil.Test;

public class ParserTest
{
    [Fact]
    public void TestEmpty()
    {
        Parser p = new(""u8);
        ParsedSchema schema = p.Parse();
        Assert.True(schema.Declarations.IsEmpty);
    }

    [Fact]
    public void TestEmptySemicolon()
    {
        Parser p = new(";"u8);
        ParsedSchema schema = p.Parse();
        Assert.True(schema.Declarations.IsEmpty);
    }

    [Fact]
    public void TestSimple()
    {
        Parser p = new("int32 a"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(1, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        Assert.Equal(1, decl.ArraySize);
    }

    [Fact]
    public void TestSimpleTrailingSemi()
    {
        Parser p = new("int32 a;"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(1, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        Assert.Equal(1, decl.ArraySize);
    }

    [Fact]
    public void TestArray()
    {
        Parser p = new("int32 a[2]"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(1, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        Assert.Equal(2, decl.ArraySize);
    }

    [Fact]
    public void TestArrayTrailingSemi()
    {
        Parser p = new("int32 a[2];"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(1, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        Assert.Equal(2, decl.ArraySize);
    }

    [Fact]
    public void TestBitfield()
    {
        Parser p = new("int32 a:2"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(1, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        Assert.Equal(1, decl.ArraySize);
        Assert.Equal(2, decl.BitWidth);
    }

    [Fact]
    public void TestBitfieldTrailingSemi()
    {
        Parser p = new("int32 a:2;"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(1, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        Assert.Equal(1, decl.ArraySize);
        Assert.Equal(2, decl.BitWidth);
    }

    [Fact]
    public void TestEnumKeyword()
    {
        Parser p = new("enum {x=1} int32 a;"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(1, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        Assert.NotNull(decl.EnumValues);
        Assert.Single(decl.EnumValues);
        Assert.Equal(1, decl.EnumValues["x"]);
    }

    [Fact]
    public void TestEnumNoKeyword()
    {
        Parser p = new("{x=1} int32 a;"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(1, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        Assert.NotNull(decl.EnumValues);
        Assert.Single(decl.EnumValues);
        Assert.Equal(1, decl.EnumValues["x"]);
    }

    [Fact]
    public void TestEnumNoValues()
    {
        Parser p = new("{} int32 a;"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(1, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        Assert.NotNull(decl.EnumValues);
        Assert.Empty(decl.EnumValues);
    }

    [Fact]
    public void TestEnumMultipleValues()
    {
        Parser p = new("{x=1,y=-2} int32 a;"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(1, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        Assert.NotNull(decl.EnumValues);
        Assert.Equal(2, decl.EnumValues.Count);
        Assert.Equal(1, decl.EnumValues["x"]);
        Assert.Equal(-2, decl.EnumValues["y"]);
    }

    [Fact]
    public void TestEnumTrailingComma()
    {
        Parser p = new("{x=1,y=-2,} int32 a;"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(1, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        Assert.NotNull(decl.EnumValues);
        Assert.Equal(2, decl.EnumValues.Count);
        Assert.Equal(1, decl.EnumValues["x"]);
        Assert.Equal(-2, decl.EnumValues["y"]);
    }

    [Fact]
    public void TestMultipleNoTrailingSemi()
    {
        Parser p = new("int32 a; int16 b"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(2, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        decl = ref declarations[1];
        Assert.Equal("int16", decl.TypeString);
        Assert.Equal("b", decl.Name);
    }

    [Fact]
    public void TestMultipleTrailingSemi()
    {
        Parser p = new("int32 a; int16 b;"u8);
        ParsedSchema schema = p.Parse();
        ReadOnlySpan<ParsedDeclaration> declarations = schema.Declarations;
        Assert.Equal(2, declarations.Length);
        ref readonly ParsedDeclaration decl = ref declarations[0];
        Assert.Equal("int32", decl.TypeString);
        Assert.Equal("a", decl.Name);
        decl = ref declarations[1];
        Assert.Equal("int16", decl.TypeString);
        Assert.Equal("b", decl.Name);
    }

    [Fact]
    public void TestErrBitfieldArray()
    {
        Parser p = new("int32 a[1]:2"u8);

        ParseException? exception = null;
        try
        {
            p.Parse();
            Assert.Fail("Parse should have thrown an exception");
        }
        catch (ParseException e)
        {
            exception = e;
        }

        Assert.NotNull(exception);
        Assert.Equal("10: Expected Semicolon, got ':'", exception.ToString());
    }

    [Fact]
    public void TestErrNoArrayValue()
    {
        Parser p = new("int32 a[]"u8);

        ParseException? exception = null;
        try
        {
            p.Parse();
            Assert.Fail("Parse should have thrown an exception");
        }
        catch (ParseException e)
        {
            exception = e;
        }

        Assert.NotNull(exception);
        Assert.Equal("8: Expected Integer, got ']'", exception.ToString());
    }

    [Fact]
    public void TestErrNoBitfieldValue()
    {
        Parser p = new("int32 a:"u8);

        ParseException? exception = null;
        try
        {
            p.Parse();
            Assert.Fail("Parse should have thrown an exception");
        }
        catch (ParseException e)
        {
            exception = e;
        }

        Assert.NotNull(exception);
        Assert.Equal("8: Expected Integer, got ''", exception.ToString());
    }

    [Fact]
    public void TestErrNoNameArray()
    {
        Parser p = new("int32 [2]"u8);

        ParseException? exception = null;
        try
        {
            p.Parse();
            Assert.Fail("Parse should have thrown an exception");
        }
        catch (ParseException e)
        {
            exception = e;
        }

        Assert.NotNull(exception);
        Assert.Equal("6: Expected Identifier, got '['", exception.ToString());
    }

    [Fact]
    public void TestErrNoNameBitfield()
    {
        Parser p = new("int32 :2"u8);

        ParseException? exception = null;
        try
        {
            p.Parse();
            Assert.Fail("Parse should have thrown an exception");
        }
        catch (ParseException e)
        {
            exception = e;
        }

        Assert.NotNull(exception);
        Assert.Equal("6: Expected Identifier, got ':'", exception.ToString());
    }

    [Fact]
    public void TestNegativeBitField()
    {
        Parser p = new("int32 a:-1"u8);

        ParseException? exception = null;
        try
        {
            p.Parse();
            Assert.Fail("Parse should have thrown an exception");
        }
        catch (ParseException e)
        {
            exception = e;
        }

        Assert.NotNull(exception);
        Assert.Equal("8: bitfield width '-1' is not a positive integer", exception.ToString());
    }

    [Fact]
    public void TestNegativeArraySize()
    {
        Parser p = new("int32 a[-1]"u8);

        ParseException? exception = null;
        try
        {
            p.Parse();
            Assert.Fail("Parse should have thrown an exception");
        }
        catch (ParseException e)
        {
            exception = e;
        }

        Assert.NotNull(exception);
        Assert.Equal("8: array size '-1' is not a positive integer", exception.ToString());
    }

    [Fact]
    public void TestZeroBitfield()
    {
        Parser p = new("int32 a:0"u8);

        ParseException? exception = null;
        try
        {
            p.Parse();
            Assert.Fail("Parse should have thrown an exception");
        }
        catch (ParseException e)
        {
            exception = e;
        }

        Assert.NotNull(exception);
        Assert.Equal("8: bitfield width '0' is not a positive integer", exception.ToString());
    }

    [Fact]
    public void TestZeroArraySize()
    {
        Parser p = new("int32 a[0]"u8);

        ParseException? exception = null;
        try
        {
            p.Parse();
            Assert.Fail("Parse should have thrown an exception");
        }
        catch (ParseException e)
        {
            exception = e;
        }

        Assert.NotNull(exception);
        Assert.Equal("8: array size '0' is not a positive integer", exception.ToString());
    }
}
