using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers;

[Flags]
public enum TypeModifiers
{
    None = 0,
    IsReadOnly = 1,
    IsRefLikeType = 2,
    IsRecord = 4,
}

public record TypeDeclarationModel(TypeKind Kind, TypeModifiers Modifiers)
{
    public string GetClassDeclaration(bool addUnsafe, bool addPartial)
    {
        string readonlyString = (Modifiers & TypeModifiers.IsReadOnly) != 0 ? "readonly " : "";
        string refString = (Modifiers & TypeModifiers.IsRefLikeType) != 0 ? "ref " : "";
        string recordString = (Modifiers & TypeModifiers.IsRecord) != 0 ? "record " : "";
        string unsafeString = addUnsafe ? "unsafe " : "";
        string partialString = addPartial ? "partial " : "";
#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
        string kindString = Kind switch
        {
            TypeKind.Class => "class",
            TypeKind.Struct => "struct",
            TypeKind.Interface => "interface",
        };
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

        return $"{readonlyString}{refString}{unsafeString}{partialString}{recordString}{kindString}";
    }
}
