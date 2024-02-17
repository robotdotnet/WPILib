using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Stereologue.SourceGenerator;


// Contains what type of type declaration a loggable type is
[Flags]
internal enum TypeDeclType
{
    None = 0x0,
    Class = 0x1,
    Struct = 0x2,
    Interface = 0x4,
    Record = 0x8,
    Readonly = 0x10,
    Ref = 0x20,
}

// Contains all information on a loggable type
internal record LoggableType(TypeDeclType TypeDeclType, string FileName, string TypeName, string? TypeNamespace, EquatableArray<LoggableMember> LoggableMembers);

internal record LoggableTypeDiagnostics(LoggableType? LoggableType, EquatableArray<DiagnosticInfo> Diagnostics);

internal static class LoggableTypeExtensions
{
    public static TypeDeclType GetTypeDeclType(this INamedTypeSymbol symbol, CancellationToken token)
    {
        var declType = symbol.TypeKind switch
        {
            TypeKind.Class => TypeDeclType.Class,
            TypeKind.Struct => TypeDeclType.Struct,
            TypeKind.Interface => TypeDeclType.Interface,
            _ => TypeDeclType.None
        };

        token.ThrowIfCancellationRequested();
        if (declType == TypeDeclType.None)
        {
            return declType;
        }
        token.ThrowIfCancellationRequested();

        if (symbol.IsReadOnly)
        {
            declType |= TypeDeclType.Readonly;
        }
        token.ThrowIfCancellationRequested();

        if (symbol.IsRefLikeType)
        {
            declType |= TypeDeclType.Ref;
        }
        token.ThrowIfCancellationRequested();

        if (symbol.IsRecord)
        {
            declType |= TypeDeclType.Record;
        }

        return declType;
    }

    public static LoggableTypeDiagnostics? GetLoggableType(this GeneratorAttributeSyntaxContext context, CancellationToken token)
    {
        if (context.SemanticModel.GetDeclaredSymbol(context.TargetNode) is not INamedTypeSymbol classSymbol)
        {
            return null;
        }
        token.ThrowIfCancellationRequested();

        var diagnosticList = ImmutableArray.CreateBuilder<DiagnosticInfo>();

        var diagnostic = GetDiagnosticIfInvalidClassForGeneration((TypeDeclarationSyntax)context.TargetNode, classSymbol);
        token.ThrowIfCancellationRequested();
        if (diagnostic is { } ds)
        {
            diagnosticList.Add(ds);
            return new(null, diagnosticList.ToImmutable());
        }

        var typeDeclType = classSymbol.GetTypeDeclType(token);
        token.ThrowIfCancellationRequested();
        if (typeDeclType == TypeDeclType.None)
        {
            // TODO better diagnostic
            diagnosticList.Add(DiagnosticInfo.Create(GeneratorDiagnostics.GeneratedTypeIsInterface, null, null));
            return new(null, diagnosticList.ToImmutable());
        }

        var classMembers = classSymbol.GetMembers();
        token.ThrowIfCancellationRequested();

        var loggableMembers = ImmutableArray.CreateBuilder<LoggableMember>(classMembers.Length);

        foreach (var member in classMembers)
        {
            token.ThrowIfCancellationRequested();

            var loggableMember = member.ToLoggableMember(token, out diagnostic);
            token.ThrowIfCancellationRequested();
            if (loggableMember is null)
            {
                if (diagnostic is not null)
                {
                    diagnosticList.Add(diagnostic.Value);
                }
                continue;
            }

            loggableMembers.Add(loggableMember);
        }

        var displayFormat = new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters | SymbolDisplayGenericsOptions.IncludeVariance);

        var nameString = classSymbol.ToDisplayString(displayFormat);
        token.ThrowIfCancellationRequested();

        var ns = classSymbol.ContainingNamespace?.ToDisplayString();
        token.ThrowIfCancellationRequested();

        var fmt = new SymbolDisplayFormat(genericsOptions: SymbolDisplayGenericsOptions.None);
        var loggableType = new LoggableType(typeDeclType, $"{classSymbol.ContainingNamespace}{classSymbol.ToDisplayString(fmt)}{classSymbol.MetadataName}", nameString, ns, loggableMembers.ToImmutable());

        return new(loggableType, diagnosticList.ToImmutable());
    }

    private static DiagnosticInfo? GetDiagnosticIfInvalidClassForGeneration(TypeDeclarationSyntax syntax, ITypeSymbol symbol)
    {
        // Ensure class is partial
        if (!syntax.IsInPartialContext(out var nonPartialIdentifier))
        {
            return DiagnosticInfo.Create(GeneratorDiagnostics.GeneratedTypeNotPartial, syntax.Identifier.GetLocation(), [symbol.Name, nonPartialIdentifier]);
        }

        // Ensure implementation isn't interface
        if (symbol.TypeKind == TypeKind.Interface)
        {
            return DiagnosticInfo.Create(GeneratorDiagnostics.GeneratedTypeIsInterface, syntax.Identifier.GetLocation(), [symbol.Name]);
        }

        return null;
    }

    private static void ConstructCall(LoggableMember data, StringBuilder builder, SourceProductionContext context)
    {
        builder.Append("        ");

        var getOperation = data.MemberType switch
        {
            MemberType.Field => data.Name,
            MemberType.Property => data.Name,
            MemberType.Method => $"{data.Name}()",
            _ => "UNKNOWN"
        };

        var path = string.IsNullOrWhiteSpace(data.AttributeInfo.Path) ? data.Name : data.AttributeInfo.Path;

        if (data.LoggedType == DeclarationType.Logged)
        {
            return;
        }

        if (data.LoggedType == DeclarationType.Struct)
        {
            return;
        }

        if (data.LoggedType == DeclarationType.Protobuf)
        {
            return;
        }

        string logMethod;

        if (data.LoggedKind == DeclarationKind.None || data.LoggedKind == DeclarationKind.Nullable)
        {
            if (data.LoggedType == DeclarationType.String)
            {
                getOperation = $"{getOperation}.AsSpan()";
            }
            else if (data.LoggedKind == DeclarationKind.Nullable)
            {
                getOperation = $"{getOperation}.GetValueOrDefault()";
            }
            if (data.LoggedType == DeclarationType.ULong)
            {
                getOperation = $"(long){getOperation}";
            }

            logMethod = data.LoggedType switch
            {
                DeclarationType.Struct => "LogStruct",
                DeclarationType.Protobuf => "LogProto",
                DeclarationType.Char => "LogChar",
                DeclarationType.String => "LogString",
                DeclarationType.Boolean => "LogBoolean",
                DeclarationType.Float => "LogFloat",
                DeclarationType.Double => "LogDouble",
                DeclarationType.Integer => "LogInteger",
                DeclarationType.ULong => "LogInteger",
                _ => $"Unknown Type: {data.LoggedType}"
            };
        }
        else
        {
            // We're array of a basic type
            if (data.LoggedKind != DeclarationKind.ReadOnlySpan && data.LoggedKind != DeclarationKind.Span)
            {
                getOperation = $"{getOperation}.AsSpan()";
            }

            logMethod = data.LoggedType switch
            {
                DeclarationType.String => "LogStringArray",
                DeclarationType.Boolean => "LogBooleanArray",
                DeclarationType.Float => "LogFloatArray",
                DeclarationType.Double => "LogDoubleArray",
                DeclarationType.Integer => "LogIntegerArray",
                DeclarationType.Raw => "LogRaw",
                _ => $"Unknown Array: {data.LoggedType}"
            };
        }

        builder.Append("logger.");
        builder.Append(logMethod);
        builder.Append("($\"{path}/");
        builder.Append(path);
        builder.Append("\", ");
        builder.Append(data.AttributeInfo.LogType);
        builder.Append(", ");
        builder.Append(getOperation);
        builder.Append(", ");
        builder.Append(data.AttributeInfo.LogLevel);
        builder.Append(");");


        // getOperation = data.LoggedModifiers switch
        // {
        //     DeclarationModifiers.AsSpan => $"{getOperation}.AsSpan()",
        //     DeclarationModifiers.LongCast => $"(long){getOperation}",
        //     _ => getOperation
        // };

        //

        // if (data.LoggedType == DeclarationType.Logged)
        // {
        //     // TODO check log type to see if we should actually do this
        //     // TODO arrays of loggables
        //     builder.Append(getOperation);
        //     if (data.LoggedModifiers == DeclarationModifiers.AllowNullConditionalOperator)
        //     {
        //         builder.Append("?");
        //     }
        //     builder.Append(".UpdateStereologue($\"{path}/");
        //     builder.Append(path);
        //     builder.Append("\", logger);");
        //     return;
        // }

        // var logCall = data.LoggedType switch
        // {
        //     DeclarationType.Struct => "LogStruct",
        //     DeclarationType.StructArray => "LogStructArray",
        //     DeclarationType.Protobuf => "LogProto",
        //     DeclarationType.Char => "LogChar",
        //     DeclarationType.String => "LogString",
        //     DeclarationType.StringArray => "LogStringArray",
        //     DeclarationType.Boolean => "LogBoolean",
        //     DeclarationType.BooleanArray => "LogBooleanArray",
        //     DeclarationType.Float => "LogFloat",
        //     DeclarationType.FloatArray => "LogFloatArray",
        //     DeclarationType.Double => "LogDouble",
        //     DeclarationType.DoubleArray => "LogDoubleArray",
        //     DeclarationType.Integer => "LogInteger",
        //     DeclarationType.IntegerArray => "LogIntegerArray",
        //     DeclarationType.Raw => "LogRaw",
        //     _ => "UNLOGGABLE_TYPE"
        // };

        // builder.Append("logger.");
        // builder.Append(logCall);
        // builder.Append("($\"{path}/");
        // builder.Append(path);
        // builder.Append("\", ");
        // builder.Append(data.AttributeInfo.LogType);
        // builder.Append(", ");
        // builder.Append(getOperation);
        // builder.Append(", ");
        // builder.Append(data.AttributeInfo.LogLevel);
        // builder.Append(");");
    }

    public static void AddClassDeclaration(LoggableType type, StringBuilder builder)
    {
        if ((type.TypeDeclType & TypeDeclType.Readonly) != 0)
        {
            builder.Append("readonly ");
        }

        if ((type.TypeDeclType & TypeDeclType.Ref) != 0)
        {
            builder.Append("ref ");
        }

        builder.Append("partial ");

        if ((type.TypeDeclType & TypeDeclType.Record) != 0)
        {
            builder.Append("record ");
        }

        if ((type.TypeDeclType & TypeDeclType.Class) != 0)
        {
            builder.Append("class ");
        }
        else if ((type.TypeDeclType & TypeDeclType.Struct) != 0)
        {
            builder.Append("struct ");
        }
        else if ((type.TypeDeclType & TypeDeclType.Interface) != 0)
        {
            builder.Append("interface ");
        }

        builder.Append(type.TypeName);

        if ((type.TypeDeclType & TypeDeclType.Ref) != 0)
        {
            builder.AppendLine();
        }
        else
        {
            builder.AppendLine(" : ILogged");
        }
    }

    public static void ExecuteSourceGeneration(this LoggableTypeDiagnostics? typeDiagnostics, SourceProductionContext context)
    {
        if (typeDiagnostics?.Diagnostics is { } diagnostics)
        {
            foreach (var diagnostic in diagnostics)
            {
                context.ReportDiagnostic(diagnostic.CreateDiagnostic());
            }
        }

        if (typeDiagnostics?.LoggableType is { } loggableType)
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
                ConstructCall(call, builder, context);
                builder.AppendLine();
            }
            builder.AppendLine("    }");
            builder.AppendLine("}");

            context.AddSource($"Stereologue.{loggableType.FileName}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
