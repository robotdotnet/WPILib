using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace Stereologue.SourceGenerator;


internal record TypeDeclType(TypeKind TypeKind, bool IsReadOnly, bool IsRefLikeType, bool IsRecord);

// Contains all information on a loggable type
internal record LoggableType(TypeDeclType TypeDeclType, string FileName, string TypeName, string? TypeNamespace, EquatableArray<LoggableMember> LoggableMembers);

internal static class LoggableTypeExtensions
{
    public static TypeDeclType GetTypeDeclType(this INamedTypeSymbol symbol)
    {
        return new TypeDeclType(symbol.TypeKind, symbol.IsReadOnly, symbol.IsRefLikeType, symbol.IsRecord);
    }

    public static LoggableType GetLoggableType(this INamedTypeSymbol classSymbol, CancellationToken token, List<(FailureMode, ISymbol)>? failures, Dictionary<LoggableMember, ISymbol>? symbolMap)
    {
        token.ThrowIfCancellationRequested();

        var typeDeclType = classSymbol.GetTypeDeclType();
        token.ThrowIfCancellationRequested();

        var classMembers = classSymbol.GetMembers();
        token.ThrowIfCancellationRequested();

        var loggableMembers = ImmutableArray.CreateBuilder<LoggableMember>(classMembers.Length);

        foreach (var member in classMembers)
        {
            token.ThrowIfCancellationRequested();

            var loggableMemberFailure = member.ToLoggableMember(token, out var loggableMember);
            token.ThrowIfCancellationRequested();
            if (loggableMemberFailure == FailureMode.None)
            {
                // This is either a valid log, or has no attribute
                if (loggableMember is not null)
                {
                    loggableMembers.Add(loggableMember);
                    symbolMap?.Add(loggableMember, member);
                }
                continue;
            }
            // Getting here means we errored
            failures?.Add((loggableMemberFailure, member));
        }

        var nameString = classSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
        token.ThrowIfCancellationRequested();

        var nspace = classSymbol.ContainingNamespace is { IsGlobalNamespace: false } ns ? ns.ToDisplayString() : null;
        token.ThrowIfCancellationRequested();

        var fmt = new SymbolDisplayFormat(genericsOptions: SymbolDisplayGenericsOptions.None);
        var loggableType = new LoggableType(typeDeclType, $"{nspace}{classSymbol.ToDisplayString(fmt)}{classSymbol.MetadataName}", nameString, nspace, loggableMembers.ToImmutable());

        return loggableType;
    }

    public static void AddClassDeclaration(LoggableType type, StringBuilder builder)
    {
        if (type.TypeDeclType.IsReadOnly)
        {
            builder.Append("readonly ");
        }

        if (type.TypeDeclType.IsRefLikeType)
        {
            builder.Append("ref ");
        }

        builder.Append("partial ");

        if (type.TypeDeclType.IsRecord)
        {
            builder.Append("record ");
        }

        if (type.TypeDeclType.TypeKind == TypeKind.Class)
        {
            builder.Append("class ");
        }
        else if (type.TypeDeclType.TypeKind == TypeKind.Struct)
        {
            builder.Append("struct ");
        }
        else if (type.TypeDeclType.TypeKind == TypeKind.Interface)
        {
            builder.Append("interface ");
        }

        builder.Append(type.TypeName);

        if (type.TypeDeclType.IsRefLikeType)
        {
            builder.AppendLine();
        }
        else
        {
            builder.AppendLine(" : ILogged");
        }
    }

    public static void ExecuteAnalysis(this LoggableType? maybeType, SymbolAnalysisContext context, Dictionary<LoggableMember, ISymbol> symbolMap)
    {
        if (maybeType is { } loggableType)
        {
            foreach (var call in loggableType.LoggableMembers)
            {
                var failureMode = call.ConstructLogCall(null);
                if (failureMode == FailureMode.None)
                {
                    continue;
                }
                // We're errored
                context.ReportDiagnostic(failureMode, symbolMap[call]);
            }
        }
    }

    public static void ExecuteSourceGeneration(this LoggableType? maybeType, SourceProductionContext context)
    {

        if (maybeType is { } loggableType)
        {
            StringBuilder builder = new StringBuilder();
            if (loggableType.TypeNamespace is not null)
            {
                builder.AppendLine($"namespace {loggableType.TypeNamespace};");
                builder.AppendLine();
            }

            AddClassDeclaration(loggableType, builder);
            builder.AppendLine("{");
            builder.AppendLine("    public void UpdateStereologue(string path, Stereologue.Stereologuer logger)");
            builder.AppendLine("    {");
            foreach (var call in loggableType.LoggableMembers)
            {
                call.ConstructLogCall(builder);
                builder.AppendLine();
            }
            builder.AppendLine("    }");
            builder.AppendLine("}");

            context.AddSource($"Stereologue.{loggableType.FileName}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
