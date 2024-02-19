using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using WPILib.CodeHelpers.LogGenerator;

namespace WPILib.CodeHelpers.StatusCheckGenerator;

internal enum ReturnKind
{
    None,
    Value,
    Ref
}

internal record MethodModel(TypeDeclType TypeDeclType, string TypeName, string? TypeNamespace, string MethodDeclaration, string NameForCall, string TypeConstraints, string ReturnType, ReturnKind ReturnKind, bool NeedsUnsafe, EquatableArray<ParameterModel> Parameters)
{
    public void AddClassDeclaration(IndentedStringBuilder builder)
    {
        builder.StartLine();
        if (TypeDeclType.IsReadOnly)
        {
            builder.Append("readonly ");
        }

        if (TypeDeclType.IsRefLikeType)
        {
            builder.Append("ref ");
        }

        if (NeedsUnsafe) {
            builder.Append("unsafe ");
        }

        builder.Append("partial ");

        if (TypeDeclType.IsRecord)
        {
            builder.Append("record ");
        }

        if (TypeDeclType.TypeKind == TypeKind.Class)
        {
            builder.Append("class ");
        }
        else if (TypeDeclType.TypeKind == TypeKind.Struct)
        {
            builder.Append("struct ");
        }
        else if (TypeDeclType.TypeKind == TypeKind.Interface)
        {
            builder.Append("interface ");
        }

        builder.Append(TypeName);
        builder.EndLine();
    }

    public void WriteMethod(IndentedStringBuilder builder)
    {
        if (TypeNamespace is not null)
        {
            builder.AppendFullLine($"namespace {TypeNamespace}");
            builder.EnterManualScope();
        }

        {
            AddClassDeclaration(builder);
            using var classScope = builder.EnterScope();
            WriteMethodDeclaration(builder);
            using var methodScope = builder.EnterScope();
            WriteCallString(builder);
            WriteStatusCheck(builder);
            WriteReturn(builder);
        }
        if (TypeNamespace is not null)
        {
            builder.ExitManualScope();
        }
    }

    public void WriteMethodDeclaration(IndentedStringBuilder builder)
    {
        builder.StartLine();
        builder.Append(MethodDeclaration);
        builder.Append("(");
        bool first = true;
        foreach (var parameter in Parameters)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                builder.Append(", ");
            }
            builder.Append(parameter.ParameterString);
        }
        builder.Append(")");
        builder.Append(TypeConstraints);
        builder.EndLine();
    }

    public void WriteCallString(IndentedStringBuilder builder)
    {
        builder.StartLine();
        if (ReturnKind != ReturnKind.None)
        {
            builder.Append(ReturnType);
            builder.Append(" __tmpValue = ");
            if (ReturnKind == ReturnKind.Ref)
            {
                builder.Append("ref ");
            }
        }
        builder.Append(NameForCall);
        builder.Append("(");
        bool first = true;
        foreach (var parameter in Parameters)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                builder.Append(", ");
            }
            parameter.WriteCallString(builder);
        }
        if (!first)
        {
            builder.Append(", ");
        }
        builder.Append("out var __tmpStatus);");
        builder.EndLine();
    }

    public void WriteStatusCheck(IndentedStringBuilder builder)
    {
        builder.AppendFullLine("__tmpStatus.ThrowIfFailed();");
    }

    public void WriteReturn(IndentedStringBuilder builder)
    {
        if (ReturnKind == ReturnKind.None)
        {
            return;
        }
        builder.StartLine();
        builder.Append("return ");
        if (ReturnKind == ReturnKind.Ref)
        {
            builder.Append("ref ");
        }
        builder.Append("__tmpValue;");
        builder.EndLine();
    }
}

internal static class MethodModelExtensions
{
    public static MethodModel? GetMethodModel(this IMethodSymbol symbol)
    {
        var parameters = ImmutableArray.CreateBuilder<ParameterModel>(symbol.Parameters.Length);

        if (symbol.Parameters.IsEmpty)
        {
            return null;
        }

        if (symbol.Parameters[^1].RefKind != RefKind.Out)
        {
            return null;
        }

        var symbolParameters = symbol.Parameters;
        bool needsUnsafe = false;

        for (int i = 0; i < symbolParameters.Length - 1; i++)
        {
            parameters.Add(symbolParameters[i].GetParameterModel(ref needsUnsafe));
        }

        var methodDeclarationFormat = new SymbolDisplayFormat(
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            memberOptions: SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeModifiers | SymbolDisplayMemberOptions.IncludeAccessibility | SymbolDisplayMemberOptions.IncludeRef

            );

        var methodDeclaration = symbol.ToDisplayString(methodDeclarationFormat);

        var callDeclarationFormat = new SymbolDisplayFormat(
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            memberOptions: SymbolDisplayMemberOptions.IncludeRef
            );

        var callDeclaration = symbol.ToDisplayString(callDeclarationFormat);

        var constraintsFmt = new SymbolDisplayFormat(
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeConstraints
        );

        var constraints = symbol.ToDisplayString(constraintsFmt).Replace(symbol.Name, "");

        var returnTypeFmt = new SymbolDisplayFormat(
            memberOptions: SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeRef,
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces
        );

        var returnType = symbol.ToDisplayString(returnTypeFmt).Replace(symbol.Name, "");

        if (symbol.ReturnType.RequiresUnsafe()) {
            needsUnsafe = true;
        }

        ReturnKind retKind;

        if (symbol.RefKind == RefKind.RefReadOnly || symbol.RefKind == RefKind.Ref)
        {
            retKind = ReturnKind.Ref;
        }
        else if (symbol.ReturnsVoid)
        {
            retKind = ReturnKind.None;
            returnType = "void";
        }
        else
        {
            retKind = ReturnKind.Value;
        }

        var classSymbol = symbol.ContainingType;

        var nameString = classSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
        var nspace = classSymbol.ContainingNamespace is { IsGlobalNamespace: false } ns ? ns.ToDisplayString() : null;

        return new(classSymbol.GetTypeDeclType(), nameString, nspace, methodDeclaration, callDeclaration, constraints, returnType, retKind, needsUnsafe, parameters.ToImmutable());
    }
}
