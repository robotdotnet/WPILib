using System.Collections.Immutable;
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

    private static string GetClassNameForLanguage(LanguageKind language)
    {
        if (language == LanguageKind.CSharp)
        {
            return "class";
        }
        else if (language == LanguageKind.VisualBasic)
        {
            return "Class";
        }
        return "";
    }

    private static string GetStructNameForLanguage(LanguageKind language)
    {
        if (language == LanguageKind.CSharp)
        {
            return "struct";
        }
        else if (language == LanguageKind.VisualBasic)
        {
            return "Structure";
        }
        return "";
    }

    private static string GetInterfaceNameForLanguage(LanguageKind language)
    {
        if (language == LanguageKind.CSharp)
        {
            return "interface";
        }
        else if (language == LanguageKind.VisualBasic)
        {
            return "Interface";
        }
        return "";
    }

    private static string GetPartialNameForLanguage(LanguageKind language)
    {
        if (language == LanguageKind.CSharp)
        {
            return "partial";
        }
        else if (language == LanguageKind.VisualBasic)
        {
            return "Partial";
        }
        return "";
    }

    private string GetClassDeclaration(bool addUnsafe, LanguageKind language)
    {
        string recordString = (Modifiers & TypeModifiers.IsRecord) != 0 ? "record " : "";
        string unsafeString = addUnsafe ? "unsafe " : "";
#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
        string kindString = Kind switch
        {
            TypeKind.Class => GetClassNameForLanguage(language),
            TypeKind.Struct => GetStructNameForLanguage(language),
            TypeKind.Interface => GetInterfaceNameForLanguage(language),
        };
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

        return $"{unsafeString}{GetPartialNameForLanguage(language)} {recordString}{kindString}";
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

    public int WriteClassDeclaration(IndentedStringBuilder builder, bool addUnsafe, string[]? inheritanceToAdd)
    {
        // Write all the way up
        int scopeCount;
        if (Parent is not null)
        {
            scopeCount = Parent.WriteClassDeclaration(builder, false, null);
        }
        else
        {
            // We are guaranteed to have a namespace here
            scopeCount = Namespace!.WriteNamespaceDeclaration(builder);
        }
        builder.StartLine();
        builder.Append($"{GetClassDeclaration(addUnsafe, builder.Language)} {TypeName}");
        GetGenericParameters(builder);
        builder.EndLine();
        var inheritanceSpan = inheritanceToAdd.AsSpan();
        if (!inheritanceSpan.IsEmpty)
        {
            if (builder.Language == LanguageKind.CSharp)
            {
                builder.StartLine();
                builder.Append("    : ");
                bool first = false;
                foreach (var item in inheritanceSpan)
                {
                    if (!first)
                    {
                        first = true;
                    }
                    else
                    {
                        builder.Append(", ");
                    }
                    builder.Append(item);
                }
                builder.EndLine();
            }
            else if (builder.Language == LanguageKind.VisualBasic)
            {
                builder.StartLine();
                builder.Append("    Implements ");
                bool first = false;
                foreach (var item in inheritanceSpan)
                {
                    if (!first)
                    {
                        first = true;
                    }
                    else
                    {
                        builder.Append(", ");
                    }
                    builder.Append(item);
                }
                builder.EndLine();
            }
        }

        if (Kind == TypeKind.Class)
        {
            builder.EnterScope(ScopeType.Class);
        }
        else if (Kind == TypeKind.Struct)
        {
            builder.EnterScope(ScopeType.Struct);
        }
        else if (Kind == TypeKind.Interface)
        {
            builder.EnterScope(ScopeType.Interface);
        }

        return scopeCount + 1;
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
