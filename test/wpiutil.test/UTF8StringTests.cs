using System;
using WPIUtil;
using Xunit;

namespace wpiutil.test
{
    public class UTF8StringTests
    {
        [Fact]
        public void TestEmptyStringHasZeroLength()
        {
            var str = new UTF8String(string.Empty);
            Assert.Equal((UIntPtr)0, str.Length);
        }

        [Fact]
        public void TestEmptyStringHasNullTerminator()
        {
            var str = new UTF8String(string.Empty);
            Assert.Equal(0, str.Buffer[0]);
        }

        [Fact]
        public void TestEmptySpanHasZeroLength()
        {
            var str = new UTF8String(ReadOnlySpan<char>.Empty);
            Assert.Equal((UIntPtr)0, str.Length);
        }

        [Fact]
        public void TestEmptySpanHasNullTerminator()
        {
            var str = new UTF8String(ReadOnlySpan<char>.Empty);
            Assert.Equal(0, str.Buffer[0]);
        }

        [Fact]
        public void TestStringHasNullTerminator()
        {
            var str = new UTF8String("Hello");
            Assert.Equal(0, str.Buffer[(int)str.Length]);
        }

        [Fact]
        public void TestStringHasProperLength()
        {
            var str = new UTF8String("Hello");
            Assert.Equal(6, str.Buffer.Length);
            Assert.Equal(5, (int)str.Length);
        }

        [Fact]
        public void TestStringGivesValidString()
        {
            var str = new UTF8String("Hello");
            byte[] expectedBytes = new byte[] {
                72, 101, 108, 108, 111, 0
            };
            Assert.Equal(expectedBytes, str.Buffer);
        }

        [Fact]
        public void TestSpanHasNullTerminator()
        {
            var str = new UTF8String("Hello".AsSpan());
            Assert.Equal(0, str.Buffer[(int)str.Length]);
        }

        [Fact]
        public void TestSpanHasProperLength()
        {
            var str = new UTF8String("Hello".AsSpan());
            Assert.Equal(6, str.Buffer.Length);
            Assert.Equal(5, (int)str.Length);
        }

        [Fact]
        public void TestSpanGivesValidString()
        {
            var str = new UTF8String("Hello".AsSpan());
            byte[] expectedBytes = new byte[] {
                72, 101, 108, 108, 111, 0
            };
            Assert.Equal(expectedBytes, str.Buffer);
        }
    }
}
