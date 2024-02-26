using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers.LogGenerator;

public enum MemberType
{
    Field,
    Property,
    Method
}

public enum DeclarationType
{
    None,
    Logged,
    Struct,
    Protobuf,
    SpecialType,
}

public enum DeclarationKind
{
    None,
    ReadOnlySpan,
    Span,
    ReadOnlyMemory,
    Memory,
    Array,
    NullableValueType,
    NullableReferenceType
}

public record MemberDeclaration(DeclarationType LoggedType, SpecialType SpecialType, DeclarationKind LoggedKind);

// Contains all information about a loggable member
public record LoggableMember(string Name, MemberType MemberType, MemberDeclaration MemberDeclaration, LogAttributeInfo AttributeInfo)
{
    public FailureMode WriteLogCall(IndentedStringBuilder? builder)
    {
        var getOperation = MemberType switch
        {
            MemberType.Field => Name,
            MemberType.Property => Name,
            MemberType.Method => $"{Name}()",
            _ => null
        };

        if (getOperation is null)
        {
            // Attribute applied to unknown type
            return FailureMode.AttributeUnknownMemberType;
        }

        var path = string.IsNullOrWhiteSpace(AttributeInfo.Path) ? Name : AttributeInfo.Path;

        if (MemberDeclaration.LoggedType == DeclarationType.Logged)
        {
            if (builder is null)
            {
                // At this point, we cannot error anymore. Just bail early if the string builder is null
                return FailureMode.None;
            }
            if (MemberDeclaration.LoggedKind == DeclarationKind.None || MemberDeclaration.LoggedKind == DeclarationKind.NullableValueType || MemberDeclaration.LoggedKind == DeclarationKind.NullableReferenceType)
            {
                string nullCheck = "";
                if (MemberDeclaration.LoggedKind != DeclarationKind.None)
                {
                    nullCheck = "?";
                }
                string semi = builder.Language == LanguageKind.VisualBasic ? "" : ";";
                builder.AppendFullLine($"{getOperation}{nullCheck}.{Strings.UpdateStereologueName}($\"{{path}}/{path}\", logger){semi}");
            }
            else
            {
                // We're an array, loop
                if (builder.Language == LanguageKind.CSharp)
                {
                    builder.AppendFullLine($"foreach (var __tmpValue in {getOperation})");
                }
                else if (builder.Language == LanguageKind.VisualBasic)
                {
                    builder.AppendFullLine($"For Each __tmpValue in {getOperation}");
                }
                builder.EnterScope(ScopeType.ForEach);
                string nullCheck = "";
                if (MemberDeclaration.LoggedKind != DeclarationKind.None)
                {
                    nullCheck = "?";
                }
                string semi = builder.Language == LanguageKind.VisualBasic ? "" : ";";
                builder.AppendFullLine($"__tmpValue{nullCheck}.{Strings.UpdateStereologueName}($\"{{path}}/{path}\", logger){semi}");
                builder.ExitScope();
            }
            return FailureMode.None;
        }

        string? logMethod;

        if (MemberDeclaration.LoggedType == DeclarationType.Struct)
        {
            if (MemberDeclaration.LoggedKind != DeclarationKind.None && MemberDeclaration.LoggedKind != DeclarationKind.NullableValueType && MemberDeclaration.LoggedKind != DeclarationKind.NullableReferenceType)
            {
                // We're an array
                logMethod = "LogStructArray";
            }
            else
            {
                logMethod = "LogStruct";
            }
        }

        else if (MemberDeclaration.LoggedType == DeclarationType.Protobuf)
        {

            if (MemberDeclaration.LoggedKind != DeclarationKind.None && MemberDeclaration.LoggedKind != DeclarationKind.NullableValueType && MemberDeclaration.LoggedKind != DeclarationKind.NullableReferenceType)
            {
                // Protobuf is array
                return FailureMode.ProtobufArray;
            }
            else
            {
                logMethod = "LogProto";
            }
        }
        else if (MemberDeclaration.LoggedKind == DeclarationKind.None || MemberDeclaration.LoggedKind == DeclarationKind.NullableReferenceType || MemberDeclaration.LoggedKind == DeclarationKind.NullableValueType)
        {
            // We're not an array. We're either Nullable<T> or a plain type
            if (MemberDeclaration.SpecialType == SpecialType.System_UInt64 || MemberDeclaration.SpecialType == SpecialType.System_IntPtr || MemberDeclaration.SpecialType == SpecialType.System_UIntPtr)
            {
                getOperation = $"(long){getOperation}";
            }

            logMethod = MemberDeclaration.SpecialType switch
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
                _ => null
            };

            if (logMethod is null)
            {
                // SpecialType is unknown, for non array
                return FailureMode.UnknownTypeNonArray;
            }
        }
        else
        {
            // We're array of a basic type

            logMethod = MemberDeclaration.SpecialType switch
            {
                SpecialType.System_String => "LogStringArray",
                SpecialType.System_Boolean => "LogBooleanArray",
                SpecialType.System_Single => "LogFloatArray",
                SpecialType.System_Double => "LogDoubleArray",
                SpecialType.System_Byte => "LogRaw",
                SpecialType.System_Int64 => "LogIntegerArray",
                _ => null
            };

            if (logMethod is null)
            {
                // SpecialType is unknown for array
                return FailureMode.UnknownTypeArray;
            }
        }

        if (builder is null)
        {
            // At this point, we cannot error anymore. Just bail early if the string builder is null
            return FailureMode.None;
        }

        if (MemberDeclaration.LoggedKind == DeclarationKind.Array || MemberDeclaration.LoggedKind == DeclarationKind.NullableReferenceType || MemberDeclaration.LoggedKind == DeclarationKind.NullableValueType)
        {
            builder.EnterScope(ScopeType.Empty);
            if (builder.Language == LanguageKind.CSharp)
            {
                builder.AppendFullLine($"var __tmpValue = {getOperation};");
                builder.AppendFullLine($"if (__tmpValue is not null)");
            }
            else if (builder.Language == LanguageKind.VisualBasic)
            {
                builder.AppendFullLine($"Dim __tmpValue = {getOperation}");
                builder.AppendFullLine($"If __tmpValue IsNot Nothing");
            }
            builder.EnterScope(ScopeType.If);
            getOperation = "__tmpValue";
            if (MemberDeclaration.SpecialType == SpecialType.System_String || MemberDeclaration.LoggedKind == DeclarationKind.Array)
            {
                getOperation = $"{getOperation}.AsSpan()";
            }
            if (MemberDeclaration.LoggedKind == DeclarationKind.NullableValueType)
            {
                getOperation = $"{getOperation}.Value";
            }
            string semi = builder.Language == LanguageKind.VisualBasic ? "" : ";";
            builder.AppendFullLine($"logger.{logMethod}($\"{{path}}/{path}\", {AttributeInfo.GetLogTypeString(builder.Language)}, {getOperation}, {AttributeInfo.GetLogLevelString(builder.Language)}){semi}");
            builder.ExitScope(); // If
            builder.ExitScope(); // Empty
        }
        else if (MemberDeclaration.LoggedKind == DeclarationKind.ReadOnlyMemory || MemberDeclaration.LoggedKind == DeclarationKind.Memory)
        {
            string semi = builder.Language == LanguageKind.VisualBasic ? "" : ";";
            builder.AppendFullLine($"logger.{logMethod}($\"{{path}}/{path}\", {AttributeInfo.GetLogTypeString(builder.Language)}, {getOperation}.Span, {AttributeInfo.GetLogLevelString(builder.Language)}){semi}");
        }
        else
        {
            string semi = builder.Language == LanguageKind.VisualBasic ? "" : ";";
            builder.AppendFullLine($"logger.{logMethod}($\"{{path}}/{path}\", {AttributeInfo.GetLogTypeString(builder.Language)}, {getOperation}, {AttributeInfo.GetLogLevelString(builder.Language)}){semi}");
        }

        return FailureMode.None;
    }
}

internal static class LoggableMemberExtensions
{
    public static DeclarationKind GetInnerType(this ITypeSymbol typeSymbol, out ITypeSymbol innerType)
    {
        // Check if we're an array
        if (typeSymbol is IArrayTypeSymbol arrayTypeSymbol)
        {
            innerType = arrayTypeSymbol.ElementType;
            return DeclarationKind.Array;
        }

        INamedTypeSymbol namedTypeSymbol;

        // Check if we're a nullable
        if (typeSymbol.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
        {
            namedTypeSymbol = (INamedTypeSymbol)typeSymbol;
            innerType = namedTypeSymbol.TypeArguments[0];
            return DeclarationKind.NullableValueType;
        }

        if (typeSymbol.IsReadOnlySpan())
        {
            namedTypeSymbol = (INamedTypeSymbol)typeSymbol;
            innerType = namedTypeSymbol.TypeArguments[0];
            return DeclarationKind.ReadOnlySpan;
        }
        else if (typeSymbol.IsSpan())
        {
            namedTypeSymbol = (INamedTypeSymbol)typeSymbol;
            innerType = namedTypeSymbol.TypeArguments[0];
            return DeclarationKind.Span;
        }
        else if (typeSymbol.IsReadOnlyMemory())
        {
            namedTypeSymbol = (INamedTypeSymbol)typeSymbol;
            innerType = namedTypeSymbol.TypeArguments[0];
            return DeclarationKind.ReadOnlyMemory;
        }
        else if (typeSymbol.IsMemory())
        {
            namedTypeSymbol = (INamedTypeSymbol)typeSymbol;
            innerType = namedTypeSymbol.TypeArguments[0];
            return DeclarationKind.Memory;
        }

        innerType = typeSymbol;
        return innerType.IsReferenceType ? DeclarationKind.NullableReferenceType : DeclarationKind.None;
    }

    private static MemberDeclaration? GetDeclarationType(this ITypeSymbol typeSymbol, LogAttributeInfo attributeInfo, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var nestedKind = typeSymbol.GetInnerType(out typeSymbol);

        token.ThrowIfCancellationRequested();

        if (typeSymbol.SpecialType != SpecialType.None)
        {
            // We're a built in special type, no need to check for anything else
            return new(DeclarationType.SpecialType, typeSymbol.SpecialType, nestedKind);
        }

        // See if we need to unwrap a nullable array
        bool innerNullable = false;
        if (typeSymbol.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
        {
            var namedTypeSymbol = (INamedTypeSymbol)typeSymbol;
            typeSymbol = namedTypeSymbol.TypeArguments[0];
            innerNullable = true;
        }

        // If we know we're generating a loggable implementation
        if (typeSymbol.HasGenerateLogAttribute())
        {
            return new(DeclarationType.Logged, (innerNullable | typeSymbol.IsReferenceType) ? SpecialType.System_Nullable_T : SpecialType.None, nestedKind);
        }
        token.ThrowIfCancellationRequested();
        // If we know we already implement ILogged
        if (typeSymbol.HasILoggedInterface())
        {
            return new(DeclarationType.Logged, (innerNullable | typeSymbol.IsReferenceType) ? SpecialType.System_Nullable_T : SpecialType.None, nestedKind);
        }
        token.ThrowIfCancellationRequested();
        // If we have an UpdateMonologue function
        var members = typeSymbol.GetMembers(Strings.UpdateStereologueName);
        foreach (var member in members)
        {
            token.ThrowIfCancellationRequested();
            // Must be a method
            if (member is IMethodSymbol method)
            {
                // Must return void
                if (!method.ReturnsVoid)
                {
                    continue;
                }
                // Must have a string first parameter, and a Stereologue.Stereologuer second paramter
                var parameters = method.Parameters;
                if (parameters.Length != 2)
                {
                    continue;
                }
                if (parameters[0].Type.SpecialType == SpecialType.System_String && parameters[1].Type.IsLoggerType())
                {
                    return new(DeclarationType.Logged, (innerNullable | typeSymbol.IsReferenceType) ? SpecialType.System_Nullable_T : SpecialType.None, nestedKind);
                }
            }
        }

        token.ThrowIfCancellationRequested();

        foreach (var inf in typeSymbol.AllInterfaces)
        {
            token.ThrowIfCancellationRequested();
            if (inf.IsStructSerializable())
            {
                if (!attributeInfo.UseProtobuf)
                {
                    // If we're an array, make sure we're not a nullable
                    if (nestedKind == DeclarationKind.ReadOnlySpan || nestedKind == DeclarationKind.ReadOnlyMemory || nestedKind == DeclarationKind.Array || nestedKind == DeclarationKind.Memory || nestedKind == DeclarationKind.Span)
                    {
                        if (innerNullable)
                        {
                            return new(DeclarationType.Struct, SpecialType.System_Nullable_T, nestedKind);
                        }
                    }
                    return new(DeclarationType.Struct, SpecialType.None, nestedKind);
                }
            }
            else if (inf.IsProtobufSerializable())
            {
                if (attributeInfo.UseProtobuf)
                {
                    return new(DeclarationType.Protobuf, SpecialType.None, nestedKind);
                }
            }
        }

        // We get here by attempting to log a type we have no clue about
        return null;
    }

    public static FailureMode ToLoggableMember(this ISymbol member, CancellationToken token, out LoggableMember? loggableMember)
    {
        loggableMember = null;
        var attributes = member.GetAttributes();
        foreach (AttributeData attribute in attributes)
        {
            token.ThrowIfCancellationRequested();
            var attributeClass = attribute.AttributeClass;
            if (attributeClass is null)
            {
                continue;
            }
            var attributeInfo = attribute.ToAttributeInfo(attributeClass, token);
            if (attributeInfo is null)
            {
                continue;
            }
            token.ThrowIfCancellationRequested();

            ITypeSymbol logType;
            MemberType memberType;
            if (member is IFieldSymbol field)
            {
                logType = field.Type;
                memberType = MemberType.Field;
            }
            else if (member is IPropertySymbol property)
            {
                logType = property.Type;
                memberType = MemberType.Property;
            }
            else if (member is IMethodSymbol method)
            {
                if (method.ReturnsVoid)
                {
                    return FailureMode.MethodReturnsVoid;
                }
                if (!method.Parameters.IsEmpty)
                {
                    return FailureMode.MethodHasParameters;
                }
                logType = method.ReturnType;
                memberType = MemberType.Method;
            }
            else
            {
                return FailureMode.AttributeUnknownMemberType;
            }
            token.ThrowIfCancellationRequested();

            var declType = logType.GetDeclarationType(attributeInfo, token);
            token.ThrowIfCancellationRequested();
            if (declType is null)
            {
                return FailureMode.UnknownTypeToLog;
            }
            else if (declType.LoggedType == DeclarationType.Struct && declType.SpecialType == SpecialType.System_Nullable_T)
            {
                // Special case of logging an array of Nullable<IStructSerializable> which is not supported
                return FailureMode.NullableStructArray;
            }

            loggableMember = new LoggableMember(member.Name, memberType, declType, attributeInfo);
            return FailureMode.None;
        }
        return FailureMode.None;
    }
}
