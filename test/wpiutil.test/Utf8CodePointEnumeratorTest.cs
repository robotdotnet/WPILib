using System.Text;
using WPIUtil.Serialization.Struct.Parsing;
using Xunit;

namespace WPIUtil.Test;

public class Utf8CodePointEnumeratorTest
{
    [Fact]
    public void TestBeginningIsReplacementChar()
    {
        Utf8CodePointEnumerator e = new("hello"u8);
        Assert.Equal(Rune.ReplacementChar.Value, e.Current.Value);
    }

    [Fact]
    public void TestForwardIteration()
    {
        Utf8CodePointEnumerator e = new("hello"u8);
        Assert.True(e.MoveNext());
        Assert.Equal('h', e.Current.Value);
        Assert.True(e.MoveNext());
        Assert.Equal('e', e.Current.Value);
        Assert.True(e.MoveNext());
        Assert.Equal('l', e.Current.Value);
        Assert.True(e.MoveNext());
        Assert.Equal('l', e.Current.Value);
        Assert.True(e.MoveNext());
        Assert.Equal('o', e.Current.Value);
        Assert.False(e.MoveNext());
        Assert.Equal(Rune.ReplacementChar.Value, e.Current.Value);
    }

    [Fact]
    public void TestForwardAndReverseIteration()
    {
        Utf8CodePointEnumerator e = new("hello"u8);
        while (e.MoveNext()) ;
        Assert.Equal(Rune.ReplacementChar.Value, e.Current.Value);
        Assert.True(e.MovePrevious());
        Assert.Equal('o', e.Current.Value);
        Assert.True(e.MovePrevious());
        Assert.Equal('l', e.Current.Value);
        Assert.True(e.MovePrevious());
        Assert.Equal('l', e.Current.Value);
        Assert.True(e.MovePrevious());
        Assert.Equal('e', e.Current.Value);
        Assert.True(e.MovePrevious());
        Assert.Equal('h', e.Current.Value);
        Assert.False(e.MovePrevious());
        Assert.Equal(Rune.ReplacementChar.Value, e.Current.Value);
    }

    [Fact]
    public void TestForwardAndReverseMixedIteration()
    {
        Utf8CodePointEnumerator e = new("hello"u8);
        Assert.True(e.MoveNext());
        Assert.Equal('h', e.Current.Value);
        Assert.True(e.MovePrevious());
        Assert.Equal('h', e.Current.Value);
        Assert.True(e.MoveNext());
        Assert.Equal('h', e.Current.Value);
    }

    [Fact]
    public void TestMarking()
    {
        Utf8CodePointEnumerator e = new("hello"u8);
        e.Mark();
        Assert.True(e.MoveNext());
        Assert.Equal('h', e.Current.Value);
        Assert.True(e.MoveNext());
        Assert.Equal('e', e.Current.Value);
        Assert.True(e.MarkedSpan.SequenceEqual("he"u8));
        e.Mark();
        Assert.True(e.MoveNext());
        Assert.Equal('l', e.Current.Value);
        Assert.True(e.MoveNext());
        Assert.Equal('l', e.Current.Value);
        Assert.True(e.MoveNext());
        Assert.Equal('o', e.Current.Value);
        Assert.False(e.MoveNext());
        Assert.True(e.MarkedSpan.SequenceEqual("llo"u8));
    }
}
