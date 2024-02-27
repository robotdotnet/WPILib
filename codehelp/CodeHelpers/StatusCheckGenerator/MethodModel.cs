using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace WPILib.CodeHelpers.StatusCheckGenerator;

internal enum ReturnKind
{
    None,
    Value,
    Ref
}

internal record MethodModel(TypeDeclarationModel TypeDeclaration, string MethodDeclaration, string NameForCall, string TypeConstraints, string ReturnType, ReturnKind ReturnKind, bool NeedsUnsafe, string StatusCheckMethod, EquatableArray<ParameterModel> Parameters)
{
    public int AddClassDeclaration(IndentedStringBuilder builder)
    {
        return TypeDeclaration.WriteClassDeclaration(builder, NeedsUnsafe, null);
    }

    public void WriteMethod(IndentedStringBuilder builder)
    {
        int classScopes = AddClassDeclaration(builder);
        WriteMethodDeclaration(builder);
        builder.EnterScope(ScopeType.NonReturningMethod);
        WriteCallString(builder);
        WriteStatusCheck(builder);
        WriteReturn(builder);
        builder.ExitScope(); // Method Scope
        for (int i = 0; i < classScopes; i++)
        {
            builder.ExitScope(); // Class Scopes
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
        if (string.IsNullOrWhiteSpace(StatusCheckMethod))
        {
            builder.AppendFullLine("__tmpStatus");
        }
        else
        {
            builder.AppendFullLine(string.Format(StatusCheckMethod, "__tmpStatus"));
        }
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
            memberOptions: SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeModifiers | SymbolDisplayMemberOptions.IncludeAccessibility | SymbolDisplayMemberOptions.IncludeRef,
            globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
            );

        var methodDeclaration = symbol.ToDisplayString(methodDeclarationFormat);

        var callDeclaration = symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat.WithMemberOptions(SymbolDisplayMemberOptions.IncludeRef | SymbolDisplayMemberOptions.IncludeContainingType));

        var constraintsFmt = new SymbolDisplayFormat(
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeConstraints,
            globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
        );

        var constraints = symbol.ToDisplayString(constraintsFmt).Replace(symbol.Name, "");

        var returnTypeFmt = new SymbolDisplayFormat(
            memberOptions: SymbolDisplayMemberOptions.IncludeType | SymbolDisplayMemberOptions.IncludeRef,
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
            globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Included,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
        );

        var returnType = symbol.ToDisplayString(returnTypeFmt).Replace(symbol.Name, "");

        if (symbol.ReturnType.RequiresUnsafe())
        {
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

        string statusCheckMethod = "";

        foreach (var attribute in symbol.GetAttributes())
        {
            if (attribute.AttributeClass?.ToDisplayString() != Strings.StatusCheckAttribute)
            {
                continue;
            }
            bool found = false;
            foreach (var named in attribute.NamedArguments)
            {
                if (named.Key == "StatusCheckMethod")
                {
                    if (!named.Value.IsNull)
                    {
                        statusCheckMethod = SymbolDisplay.FormatPrimitive(named.Value.Value!, false, false);
                        found = true;
                        break;
                    }
                }
            }
            if (found)
            {
                break;
            }
        }

        return new(classSymbol.GetTypeDeclarationModel(), methodDeclaration, callDeclaration, constraints, returnType, retKind, needsUnsafe, statusCheckMethod, parameters.ToImmutable());
    }
}
