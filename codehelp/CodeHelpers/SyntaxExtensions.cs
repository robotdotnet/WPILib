using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WPILib.CodeHelpers;

public static class SyntaxExtensions
{
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
