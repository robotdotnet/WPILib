using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
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

    public static LoggableType? GetLoggableType(this GeneratorAttributeSyntaxContext context, CancellationToken token)
    {
        if (context.SemanticModel.GetDeclaredSymbol(context.TargetNode) is not INamedTypeSymbol classSymbol)
        {
            return null;
        }
        token.ThrowIfCancellationRequested();

        var typeDeclType = classSymbol.GetTypeDeclType();
        token.ThrowIfCancellationRequested();

        var classMembers = classSymbol.GetMembers();
        token.ThrowIfCancellationRequested();

        var loggableMembers = ImmutableArray.CreateBuilder<LoggableMember>(classMembers.Length);

        foreach (var member in classMembers)
        {
            token.ThrowIfCancellationRequested();

            var loggableMember = member.ToLoggableMember(token);
            token.ThrowIfCancellationRequested();
            if (loggableMember is null)
            {
                continue;
            }

            loggableMembers.Add(loggableMember);
        }

        var nameString = classSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
        token.ThrowIfCancellationRequested();

        var nspace = classSymbol.ContainingNamespace is { IsGlobalNamespace: false } ns ? ns.ToDisplayString() : null;
        token.ThrowIfCancellationRequested();

        var fmt = new SymbolDisplayFormat(genericsOptions: SymbolDisplayGenericsOptions.None);
        var loggableType = new LoggableType(typeDeclType, $"{nspace}{classSymbol.ToDisplayString(fmt)}{classSymbol.MetadataName}", nameString, nspace, loggableMembers.ToImmutable());

        return loggableType;
    }

    private static void ConstructCall(LoggableMember data, StringBuilder builder, SourceProductionContext context)
    {
        builder.Append("        ");

        var getOperation = data.MemberType switch
        {
            MemberType.Field => data.Name,
            MemberType.Property => data.Name,
            MemberType.Method => $"{data.Name}()",
            _ => "Unknown member type"
        };

        var path = string.IsNullOrWhiteSpace(data.AttributeInfo.Path) ? data.Name : data.AttributeInfo.Path;

        if (data.MemberDeclaration.LoggedType == DeclarationType.Logged)
        {
            return;
        }

        string logMethod;

        if (data.MemberDeclaration.LoggedType == DeclarationType.Struct)
        {
            if (data.MemberDeclaration.LoggedKind != DeclarationKind.None && data.MemberDeclaration.LoggedKind != DeclarationKind.NullableValueType && data.MemberDeclaration.LoggedKind != DeclarationKind.NullableReferenceType)
            {
                // We're an array
                logMethod = "LogStructArray";
            }
            else
            {
                logMethod = "LogStruct";
            }
            if (data.MemberDeclaration.LoggedKind == DeclarationKind.NullableValueType)
            {
                getOperation = $"{getOperation}.GetValueOrDefault()";
            }
        }

        else if (data.MemberDeclaration.LoggedType == DeclarationType.Protobuf)
        {

            if (data.MemberDeclaration.LoggedKind != DeclarationKind.None && data.MemberDeclaration.LoggedKind != DeclarationKind.NullableValueType && data.MemberDeclaration.LoggedKind != DeclarationKind.NullableReferenceType)
            {
                logMethod = "Cannot log an array of protobufs";
            }
            else
            {
                logMethod = "LogProto";
            }
            if (data.MemberDeclaration.LoggedKind == DeclarationKind.NullableValueType)
            {
                getOperation = $"{getOperation}.GetValueOrDefault()";
            }
        }
        else if (data.MemberDeclaration.LoggedKind == DeclarationKind.None || data.MemberDeclaration.LoggedKind == DeclarationKind.NullableReferenceType || data.MemberDeclaration.LoggedKind == DeclarationKind.NullableValueType)
        {
            // We're not an array. We're either Nullable<T> or a plain type
            if (data.MemberDeclaration.SpecialType == SpecialType.System_String)
            {
                getOperation = $"{getOperation}.AsSpan()";
            }
            else if (data.MemberDeclaration.LoggedKind == DeclarationKind.NullableValueType)
            {
                getOperation = $"{getOperation}.GetValueOrDefault()";
            }
            if (data.MemberDeclaration.SpecialType == SpecialType.System_UInt64 || data.MemberDeclaration.SpecialType == SpecialType.System_IntPtr || data.MemberDeclaration.SpecialType == SpecialType.System_UIntPtr)
            {
                getOperation = $"(long){getOperation}";
            }

            logMethod = data.MemberDeclaration.SpecialType switch
            {
                SpecialType.System_Char => "LogChar",
                SpecialType.System_String => "LogString",
                SpecialType.System_Boolean => "LogBoolean",
                SpecialType.System_Single => "LogFloat",
                SpecialType.System_Double => "LogDouble",
                SpecialType.System_Byte => "LogInteger",
                SpecialType.System_SByte => "LogInteger",
                SpecialType.System_Int16 => "LogInteger",
                SpecialType.System_UInt16 => "LogInteger",
                SpecialType.System_Int32 => "LogInteger",
                SpecialType.System_UInt32 => "LogInteger",
                SpecialType.System_Int64 => "LogInteger",
                SpecialType.System_UInt64 => "LogInteger",
                SpecialType.System_IntPtr => "LogInteger",
                SpecialType.System_UIntPtr => "LogInteger",
                _ => $"Unknown Type: {data.MemberDeclaration}"
            };
        }
        else
        {
            // We're array of a basic type
            if (data.MemberDeclaration.LoggedKind != DeclarationKind.ReadOnlySpan && data.MemberDeclaration.LoggedKind != DeclarationKind.Span)
            {
                getOperation = $"{getOperation}.AsSpan()";
            }

            logMethod = data.MemberDeclaration.SpecialType switch
            {
                SpecialType.System_String => "LogStringArray",
                SpecialType.System_Boolean => "LogBooleanArray",
                SpecialType.System_Single => "LogFloatArray",
                SpecialType.System_Double => "LogDoubleArray",
                SpecialType.System_Byte => "LogRaw",
                SpecialType.System_Int64 => "LogIntegerArray",
                _ => $"Unknown Array: {data.MemberDeclaration}"
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
                ConstructCall(call, builder, context);
                builder.AppendLine();
            }
            builder.AppendLine("    }");
            builder.AppendLine("}");

            context.AddSource($"Stereologue.{loggableType.FileName}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
