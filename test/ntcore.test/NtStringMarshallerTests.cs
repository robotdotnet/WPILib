namespace NetworkTables;

// public unsafe class NtStringMarshallerTests
// {
//     [Fact]
//     public void TestNullString()
//     {
//         NtString tmp = new();
//         try
//         {
//             tmp = Marshaller.PassToArray.ConvertToUnmanaged(null);
//             Assert.Equal((nuint)0, tmp.len);
//             Assert.True(tmp.str is not null);
//         }
//         finally
//         {
//             Marshaller.PassToArray.Free(tmp);
//         }
//     }

//     [Fact]
//     public void TestEmptyString()
//     {
//         NtString tmp = new();
//         try
//         {
//             tmp = Marshaller.PassToArray.ConvertToUnmanaged("");
//             Assert.Equal((nuint)0, tmp.len);
//             Assert.True(tmp.str is not null);
//         }
//         finally
//         {
//             Marshaller.PassToArray.Free(tmp);
//         }
//     }

//     [Fact]
//     public void TestString()
//     {
//         ReadOnlySpan<byte> str = "Hello World"u8;
//         NtString tmp = new();
//         try
//         {
//             tmp = Marshaller.PassToArray.ConvertToUnmanaged("Hello World");
//             Assert.Equal((nuint)str.Length, tmp.len);
//             Assert.True(tmp.str is not null);
//             Assert.True(str.SequenceEqual(new ReadOnlySpan<byte>(tmp.str, (int)tmp.len)));
//         }
//         finally
//         {
//             Marshaller.PassToArray.Free(tmp);
//         }
//     }

//     [Fact]
//     public void TestStringIntegratedNull()
//     {
//         ReadOnlySpan<byte> str = "Hello\0World"u8;
//         NtString tmp = new();
//         try
//         {
//             tmp = Marshaller.PassToArray.ConvertToUnmanaged("Hello\0World");
//             Assert.Equal((nuint)str.Length, tmp.len);
//             Assert.True(tmp.str is not null);
//             Assert.True(str.SequenceEqual(new ReadOnlySpan<byte>(tmp.str, (int)tmp.len)));
//         }
//         finally
//         {
//             Marshaller.PassToArray.Free(tmp);
//         }
//     }

//     [Fact]
//     public void TestUnmanagedToManagedNull()
//     {
//         NtString tmp = new NtString()
//         {
//             str = null,
//             len = 0,
//         };
//         Assert.Null(Marshaller.ManagedConvert(tmp));
//     }

//     [Fact]
//     public void TestUnmanagedToManaged0Length()
//     {
//         byte b = 0;
//         NtString tmp = new NtString()
//         {
//             str = &b,
//             len = 0,
//         };
//         Assert.Equal("", Marshaller.ManagedConvert(tmp));
//     }

//     [Fact]
//     public void TestUnmanagedToManagedNullLongerLength()
//     {
//         NtString tmp = new NtString()
//         {
//             str = null,
//             len = 42,
//         };
//         Assert.Null(Marshaller.ManagedConvert(tmp));
//     }

//     [Fact]
//     public void TestUnmanagedToManagedString()
//     {
//         ReadOnlySpan<byte> span = "Hello World"u8;
//         fixed (byte* pinned = span)
//         {
//             NtString tmp = new NtString()
//             {
//                 str = pinned,
//                 len = (nuint)span.Length,
//             };
//             Assert.Equal("Hello World", Marshaller.ManagedConvert(tmp));
//         }
//     }

//     [Fact]
//     public void TestUnmanagedToManagedStringContainingNull()
//     {
//         ReadOnlySpan<byte> span = "Hello\0World"u8;
//         fixed (byte* pinned = span)
//         {
//             NtString tmp = new NtString()
//             {
//                 str = pinned,
//                 len = (nuint)span.Length,
//             };
//             Assert.Equal("Hello\0World", Marshaller.ManagedConvert(tmp));
//         }
//     }
// }
