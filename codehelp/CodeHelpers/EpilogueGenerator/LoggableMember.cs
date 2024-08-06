using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using WPILib.Logging;

namespace WPILib.CodeHelpers.EpilogueGenerator;

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
    SpecialType,
    PotentialCustomLogger,
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

public record MemberDeclaration(DeclarationType LoggedType, TypeDeclarationModel? CustomLogger, SpecialType SpecialType, DeclarationKind LoggedKind);

// Contains all information about a loggable member
public record LoggableMember(string Name, MemberType MemberType, MemberDeclaration MemberDeclaration, LogAttributeInfo AttributeInfo)
{
    public FailureMode WriteLogCall(IndentedStringBuilder? builder, ImmutableArray<CustomLoggerType?> customLoggers)
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

        var path = string.IsNullOrWhiteSpace(AttributeInfo.Name) ? Name : AttributeInfo.Name;

        if (MemberDeclaration.LoggedType == DeclarationType.Logged || MemberDeclaration.LoggedType == DeclarationType.PotentialCustomLogger)
        {
            TypeDeclarationModel toUseModel = MemberDeclaration.CustomLogger!;
            // If we're a potential custom logger, see if its in our list.
            if (MemberDeclaration.LoggedType == DeclarationType.PotentialCustomLogger)
            {
                bool found = false;
                foreach (var custom in customLoggers)
                {
                    foreach (var supported in custom!.SupportedTypes)
                    {
                        if (supported.Equals(toUseModel))
                        {
                            toUseModel = supported;
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
                if (!found)
                {
                    // We were not able to find a matching logger
                    return FailureMode.None;
                }
            }

            if (builder is null)
            {
                // At this point, we cannot error anymore. Just bail early if the string builder is null
                return FailureMode.None;
            }
            if (MemberDeclaration.LoggedKind == DeclarationKind.None || MemberDeclaration.LoggedKind == DeclarationKind.NullableValueType || MemberDeclaration.LoggedKind == DeclarationKind.NullableReferenceType)
            {
                builder.StartLine();
                builder.Append("global::Epilogue.");
                toUseModel.WriteLoggerName(builder, true);
                builder.Append($".TryUpdate(dataLogger.GetSubLogger(\"{path}\"), value.{Name}, global::Epilogue.Config.ErrorHandler);");
                builder.EndLine();
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
                builder.StartLine();
                builder.Append("global::Epilogue.");
                toUseModel.WriteLoggerName(builder, true);
                builder.Append($".TryUpdate(dataLogger.GetSubLogger(\"{path}\"), value.{Name}, global::Epilogue.Config.ErrorHandler);");
                builder.EndLine();
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
            builder.AppendFullLine($"dataLogger.Log(\"{path}\", value.{getOperation}){semi}");
            builder.ExitScope(); // If
            builder.ExitScope(); // Empty
        }
        else if (MemberDeclaration.LoggedKind == DeclarationKind.ReadOnlyMemory || MemberDeclaration.LoggedKind == DeclarationKind.Memory)
        {
            string semi = builder.Language == LanguageKind.VisualBasic ? "" : ";";
            builder.AppendFullLine($"dataLogger.Log(\"{path}\", value.{getOperation}.Span){semi}");
        }
        else
        {
            string semi = builder.Language == LanguageKind.VisualBasic ? "" : ";";
            builder.AppendFullLine($"dataLogger.Log(\"{path}\", value.{getOperation}){semi}");
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

    private static MemberDeclaration? GetDeclarationType(this ITypeSymbol typeSymbol, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var nestedKind = typeSymbol.GetInnerType(out typeSymbol);

        token.ThrowIfCancellationRequested();

        if (typeSymbol.SpecialType != SpecialType.None)
        {
            // We're a built in special type, no need to check for anything else
            return new(DeclarationType.SpecialType, null, typeSymbol.SpecialType, nestedKind);
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
        if (typeSymbol.HasLoggedAttribute())
        {
            // Can't have attributes on anonymous types, so this must be named
            return new(DeclarationType.Logged, ((INamedTypeSymbol)typeSymbol).GetTypeDeclarationModel(), (innerNullable | typeSymbol.IsReferenceType) ? SpecialType.System_Nullable_T : SpecialType.None, nestedKind);
        }
        // token.ThrowIfCancellationRequested();
        // // If we know we already implement ILogged
        // if (typeSymbol.HasILoggedInterface())
        // {
        //     return new(DeclarationType.Logged, (innerNullable | typeSymbol.IsReferenceType) ? SpecialType.System_Nullable_T : SpecialType.None, nestedKind);
        // }
        // token.ThrowIfCancellationRequested();
        // // If we have an UpdateMonologue function
        // var members = typeSymbol.GetMembers(Strings.UpdateStereologueName);
        // foreach (var member in members)
        // {
        //     token.ThrowIfCancellationRequested();
        //     // Must be a method
        //     if (member is IMethodSymbol method)
        //     {
        //         // Must return void
        //         if (!method.ReturnsVoid)
        //         {
        //             continue;
        //         }
        //         // Must have a string first parameter, and a Stereologue.Stereologuer second paramter
        //         var parameters = method.Parameters;
        //         if (parameters.Length != 2)
        //         {
        //             continue;
        //         }
        //         if (parameters[0].Type.SpecialType == SpecialType.System_String && parameters[1].Type.IsLoggerType())
        //         {
        //             return new(DeclarationType.Logged, (innerNullable | typeSymbol.IsReferenceType) ? SpecialType.System_Nullable_T : SpecialType.None, nestedKind);
        //         }
        //     }
        // }

        token.ThrowIfCancellationRequested();

        foreach (var inf in typeSymbol.AllInterfaces)
        {
            token.ThrowIfCancellationRequested();
            if (inf.IsStructSerializable())
            {
                // If we're an array, make sure we're not a nullable
                if (nestedKind == DeclarationKind.ReadOnlySpan || nestedKind == DeclarationKind.ReadOnlyMemory || nestedKind == DeclarationKind.Array || nestedKind == DeclarationKind.Memory || nestedKind == DeclarationKind.Span)
                {
                    if (innerNullable)
                    {
                        return new(DeclarationType.Struct, null, SpecialType.System_Nullable_T, nestedKind);
                    }
                }
                return new(DeclarationType.Struct, null, SpecialType.None, nestedKind);
            }
        }

        // We get here by attempting to log a type we have no clue about. It could be a custom logger
        // For now, only custom loggers are supported on bare symbols
        if (nestedKind is DeclarationKind.None || nestedKind is DeclarationKind.NullableReferenceType)
        {
            // Must be an INamedTypeSymbol
            if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
            {
                return new(DeclarationType.PotentialCustomLogger, namedTypeSymbol.GetTypeDeclarationModel(), SpecialType.None, nestedKind);
            }
        }
        // Otherwise, we can't log it
        return null;
    }

    public static FailureMode ToLoggableMember(this ISymbol member, CancellationToken token, LogStrategy logStrategy, out LoggableMember? loggableMember)
    {
        loggableMember = null;

        // Ignore anything implicitly declared. As it both cannot be attributed, and cannot be called by code
        if (member.IsImplicitlyDeclared)
        {
            return FailureMode.None;
        }

        var attributes = member.GetAttributes();
        LogAttributeInfo? attributeInfo = null;
        // Search for either NotLogged or Logged.
        // If NotLogged is found, return no failure mode
        // Otherwise use LogStrategy to determine what to do
        foreach (AttributeData attribute in attributes)
        {
            token.ThrowIfCancellationRequested();
            var attributeClass = attribute.AttributeClass;
            if (attributeClass is null)
            {
                continue;
            }
            else if (attributeClass.IsNotLoggedAttributeClass())
            {
                return FailureMode.None;
            }
            else if (attributeClass.IsLoggedAttributeClass())
            {
                attributeInfo = attribute.ToAttributeInfo(token);
            }
            token.ThrowIfCancellationRequested();
            if (attributeInfo is not null)
            {
                break;
            }
        }

        // We didn't find a "Logged", and we're running opt in.
        if (attributeInfo is null && logStrategy == LogStrategy.OptIn)
        {
            return FailureMode.None;
        }

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
                if (attributeInfo is not null)
                {
                    // Explicitly asked for logged, error
                    return FailureMode.MethodReturnsVoid;
                }
                else
                {
                    // Opt in won't pick this up
                    return FailureMode.None;
                }
            }

            if (!method.Parameters.IsEmpty)
            {
                if (attributeInfo is not null)
                {
                    // Explicitly asked for logged, error
                    return FailureMode.MethodReturnsVoid;
                }
                else
                {
                    // Opt in won't pick this up
                    return FailureMode.None;
                }
            }

            // Here, we know we could be a valid method
            if (attributeInfo is not null)
            {
                // Explicitly logged, access doesn't matter
                logType = method.ReturnType;
                memberType = MemberType.Method;
            }
            else
            {
                // Implicitly logged, ensure public
                if (method.DeclaredAccessibility == Accessibility.Public)
                {
                    logType = method.ReturnType;
                    memberType = MemberType.Method;
                }
                else
                {
                    // Not a public method, don't log implicitly
                    return FailureMode.None;
                }
            }
        }
        else
        {
            return FailureMode.AttributeUnknownMemberType;
        }
        token.ThrowIfCancellationRequested();

        var declType = logType.GetDeclarationType(token);
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

        // At this point, we will need a fake attribute info, even if implicitly logged.
        attributeInfo ??= new LogAttributeInfo(member.Name, LogStrategy.OptIn, LogImportance.Debug);

        loggableMember = new LoggableMember(member.Name, memberType, declType, attributeInfo);
        return FailureMode.None;
    }
}
