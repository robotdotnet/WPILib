//using Marshaller = NetworkTables.Natives.NtLengthStringMarshaller<byte>;

namespace NetworkTables;

// public unsafe class StringUtf8ReturnMarshallerTests
// {
//     [Fact]
//     public unsafe void CopyWorks()
//     {
//         Marshaller.GetUnmanagedValuesSource(null, 0).CopyTo(Marshaller.GetManagedValuesDestination(""));
//     }

//     [Fact]
//     public unsafe void NullIsEmpty()
//     {
//         Assert.Equal("", Marshaller.AllocateContainerForManagedElements(null, 0));
//     }

//     [Fact]
//     public unsafe void ValidStringWorks()
//     {
//         ReadOnlySpan<byte> testString = "UTF8String"u8;
//         byte* allocate = NtCore.AllocateCharArray((nuint)testString.Length);
//         testString.CopyTo(new Span<byte>(allocate, testString.Length));
//         Assert.Equal("UTF8String", Marshaller.AllocateContainerForManagedElements(allocate, testString.Length));
//     }

//     [Fact]
//     public unsafe void IntegratedNullTerminator()
//     {
//         ReadOnlySpan<byte> testString = "UTF8\0String"u8;
//         byte* allocate = NtCore.AllocateCharArray((nuint)testString.Length);
//         testString.CopyTo(new Span<byte>(allocate, testString.Length));
//         Assert.Equal("UTF8\0String", Marshaller.AllocateContainerForManagedElements(allocate, testString.Length));
//     }
// }
