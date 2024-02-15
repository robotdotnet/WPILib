using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Monologue.SourceGenerator;

public static class SyntaxExtensions
{
    public static void GetTypeDeclaration(this ITypeSymbol symbol, StringBuilder builder)
    {
        var displayFormat = new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeVariance);

        var nameString = symbol.ToDisplayString(displayFormat);

        if (symbol.IsReadOnly)
        {
            builder.Append("readonly ");
        }

        if (symbol.IsRefLikeType)
        {
            builder.Append("ref ");
        }

        builder.Append("partial ");

        if (symbol.IsRecord)
        {
            builder.Append("record ");
        }

        builder.Append(symbol.TypeKind switch
        {
            TypeKind.Class => "class ",
            TypeKind.Struct => "struct ",
            TypeKind.Interface => "interface ",
            _ => throw new InvalidOperationException(), // Or however you want to handle that
        });

        builder.Append(nameString);
    }

    public static bool IsInPartialContext(this TypeDeclarationSyntax syntax, out SyntaxToken? nonPartialIdentifier)
    {
        for (SyntaxNode? parentNode = syntax; parentNode is TypeDeclarationSyntax typeDecl; parentNode = parentNode.Parent)
        {
            if (!typeDecl.Modifiers.Any(SyntaxKind.PartialKeyword))
            {
                nonPartialIdentifier = typeDecl.Identifier;
                return false;
            }
        }
        nonPartialIdentifier = null;
        return true;
    }
}
