using System.Collections.Immutable;
using System.Text;
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

public record TypeDeclarationModel(TypeKind Kind, TypeModifiers Modifiers, string TypeName, EquatableArray<TypeParameterModel> TypeParameters, NamespaceModel? Namespace, TypeDeclarationModel? Parent)
{
    public void WriteFileName(IndentedStringBuilder builder)
    {
        if (Parent is not null)
        {
            Parent.WriteFileName(builder);
        }
        else
        {
            Namespace!.WriteFileName(builder);
        }
        builder.Append($"{TypeName}.");
    }

    private string GetClassDeclaration(bool addUnsafe)
    {
        string recordString = (Modifiers & TypeModifiers.IsRecord) != 0 ? "record " : "";
        string unsafeString = addUnsafe ? "unsafe " : "";
#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
        string kindString = Kind switch
        {
            TypeKind.Class => "class",
            TypeKind.Struct => "struct",
            TypeKind.Interface => "interface",
        };
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

        return $"{unsafeString}partial {recordString}{kindString}";
    }

    private void GetGenericParameters(IndentedStringBuilder builder)
    {
        bool first = true;
        foreach (var typeParamter in TypeParameters.AsSpan())
        {
            if (first)
            {
                first = false;
                builder.Append("<");
            }
            else
            {
                builder.Append(", ");
            }
            typeParamter.WriteTypeParameter(builder);
        }
        if (!first)
        {
            builder.Append(">");
        }
    }

    public IndentedStringBuilder.IndentedScope WriteClassDeclaration(IndentedStringBuilder builder, bool addUnsafe, string? inheritanceToAdd)
    {
        // Write all the way up
        IndentedStringBuilder.IndentedScope scope;
        if (Parent is not null)
        {
            scope = Parent.WriteClassDeclaration(builder, false, null);
        }
        else
        {
            // We are guaranteed to have a namespace here
            scope = Namespace!.WriteNamespaceDeclaration(builder);
        }
        builder.StartLine();
        builder.Append($"{GetClassDeclaration(addUnsafe)} {TypeName}");
        GetGenericParameters(builder);
        if (inheritanceToAdd is not null) {
            builder.Append(inheritanceToAdd);
        }
        builder.EndLine();
        scope.AddLineToScope();
        return scope;
    }
}

public static class TypeDeclarationExtensions
{
    public static TypeDeclarationModel GetTypeDeclarationModel(this INamedTypeSymbol symbol)
    {
        TypeModifiers modifiers = TypeModifiers.None;
        if (symbol.IsReadOnly)
        {
            modifiers |= TypeModifiers.IsReadOnly;
        }
        if (symbol.IsRefLikeType)
        {
            modifiers |= TypeModifiers.IsRefLikeType;
        }
        if (symbol.IsRecord)
        {
            modifiers |= TypeModifiers.IsRecord;
        }

        EquatableArray<TypeParameterModel> typeParameters;
        var symbolTypeParameters = symbol.TypeParameters;
        if (!symbolTypeParameters.IsEmpty)
        {
            var builder = ImmutableArray.CreateBuilder<TypeParameterModel>(symbolTypeParameters.Length);
            foreach (var typeParameter in symbolTypeParameters)
            {
                // No need for constraints on types
                builder.Add(typeParameter.GetTypeParameterModel(false));
            }
            typeParameters = builder.ToImmutable();
        }
        else
        {
            typeParameters = [];
        }

        NamespaceModel? nspace = null;

        TypeDeclarationModel? parent = null;
        if (symbol.ContainingType is not null)
        {
            parent = symbol.ContainingType.GetTypeDeclarationModel();
        }
        else
        {
            nspace = symbol.ContainingNamespace.GetNamespaceModel();
        }

        return new(symbol.TypeKind, modifiers, symbol.Name, typeParameters, nspace, parent);
    }
}
