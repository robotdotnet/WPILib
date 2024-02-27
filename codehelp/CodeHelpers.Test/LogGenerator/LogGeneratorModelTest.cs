using Microsoft.CodeAnalysis;
using WPILib.CodeHelpers;
using WPILib.CodeHelpers.LogGenerator;

namespace CodeHelpers.Test.LogGenerator;

public class LogGeneratorModelTest
{
    [Fact]
    public void TestEqualModel()
    {
        var a = new LoggableType(
            TypeDeclaration: new(
                Kind: TypeKind.Struct,
                Modifiers: TypeModifiers.IsReadOnly,
                Namespace: new("ABC", new("NS", null)),
                Parent: null,
                TypeName: "ABC",
                TypeParameters: []
            ),
            LoggableMembers: []
        );
        var b = new LoggableType(
            TypeDeclaration: new(
                Kind: TypeKind.Struct,
                Modifiers: TypeModifiers.IsReadOnly,
                Namespace: new("ABC", new("NS", null)),
                Parent: null,
                TypeName: "ABC",
                TypeParameters: []
            ),
            LoggableMembers: []
        );

        Assert.Equal(a, b);
    }
}
