using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Stereologue.SourceGenerator;

internal enum MemberType
{
    Field,
    Property,
    Method
}

internal enum DeclarationType
{
    None,
    Logged,
    Struct,
    Protobuf,
    Boolean,
    Float,
    Double,
    Integer,
    String,
    Raw,
    Char,
    ULong,
}

internal enum DeclarationKind
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

// Contains all information about a loggable member
internal record LoggableMember(string Name, MemberType MemberType, DeclarationType LoggedType, DeclarationKind LoggedKind, LogAttributeInfo AttributeInfo);

internal static class LoggableMemberExtensions
{
    private static DeclarationKind GetInnerType(this ITypeSymbol typeSymbol, out ITypeSymbol innerType)
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

        var fmt = new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);
        switch (typeSymbol.ToDisplayString(fmt))
        {
            case "System.ReadOnlySpan":
                namedTypeSymbol = (INamedTypeSymbol)typeSymbol;
                innerType = namedTypeSymbol.TypeArguments[0];
                return DeclarationKind.ReadOnlySpan;
            case "System.Span":
                namedTypeSymbol = (INamedTypeSymbol)typeSymbol;
                innerType = namedTypeSymbol.TypeArguments[0];
                return DeclarationKind.Span;
            case "System.ReadOnlyMemory":
                namedTypeSymbol = (INamedTypeSymbol)typeSymbol;
                innerType = namedTypeSymbol.TypeArguments[0];
                return DeclarationKind.ReadOnlyMemory;
            case "System.Memory":
                namedTypeSymbol = (INamedTypeSymbol)typeSymbol;
                innerType = namedTypeSymbol.TypeArguments[0];
                return DeclarationKind.Memory;
        }

        innerType = typeSymbol;
        return innerType.IsReferenceType ? DeclarationKind.NullableReferenceType : DeclarationKind.None;
    }

    private static (DeclarationType, DeclarationKind)? GetDeclarationType(this ITypeSymbol typeSymbol, LogAttributeInfo attributeInfo, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var nestedKind = typeSymbol.GetInnerType(out typeSymbol);

        token.ThrowIfCancellationRequested();

        // If we know we're generating a loggable implementation
        if (typeSymbol.GetAttributes().Where(x => x.AttributeClass?.ToDisplayString() == "Stereologue.GenerateLogAttribute").Any())
        {
            return (DeclarationType.Logged, nestedKind);
        }
        token.ThrowIfCancellationRequested();
        // If we know we already implement ILogged
        if (typeSymbol.AllInterfaces.Where(x => x.ToDisplayString() == "Stereologue.ILogged").Any())
        {
            return (DeclarationType.Logged, nestedKind);
        }
        token.ThrowIfCancellationRequested();
        // If we have an UpdateMonologue function
        var members = typeSymbol.GetMembers("UpdateStereologue");
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
                if (parameters[0].Type.SpecialType == SpecialType.System_String && parameters[1].Type.ToDisplayString() == "Stereologue.Stereologuer")
                {
                    return (DeclarationType.Logged, nestedKind);
                }
            }
        }

        var fmt = new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters);
        token.ThrowIfCancellationRequested();
        var fullTypeName = typeSymbol.ToDisplayString(fmt);
        token.ThrowIfCancellationRequested();
        var structName = $"WPIUtil.Serialization.Struct.IStructSerializable<{fullTypeName}>";
        var protobufName = $"WPIUtil.Serialization.Protobuf.IProtobufSerializable<{fullTypeName}>";

        foreach (var inf in typeSymbol.AllInterfaces)
        {
            token.ThrowIfCancellationRequested();
            var interfaceName = inf.ToDisplayString();
            token.ThrowIfCancellationRequested();
            if (interfaceName == structName)
            {
                if (!attributeInfo.UseProtobuf)
                {
                    return (DeclarationType.Struct, nestedKind);
                }
            }
            else if (interfaceName == protobufName)
            {
                if (attributeInfo.UseProtobuf)
                {
                    // TODO disallow arrays of protobuf types
                    return (DeclarationType.Protobuf, nestedKind);
                }
            }
        }

        // Special case non nested and nullables
        if (nestedKind == DeclarationKind.None || nestedKind == DeclarationKind.NullableReferenceType || nestedKind == DeclarationKind.NullableValueType)
        {
            return (fullTypeName switch
            {
                "System.Single" => DeclarationType.Float,
                "System.Double" => DeclarationType.Double,
                "System.Byte" => DeclarationType.Integer,
                "System.SByte" => DeclarationType.Integer,
                "System.Int16" => DeclarationType.Integer,
                "System.UInt16" => DeclarationType.Integer,
                "System.Int32" => DeclarationType.Integer,
                "System.UInt32" => DeclarationType.Integer,
                "System.Int64" => DeclarationType.Integer,
                "System.UInt64" => DeclarationType.ULong,
                "System.Boolean" => DeclarationType.Boolean,
                "System.Char" => DeclarationType.Char,
                "System.String" => DeclarationType.String,
                _ => DeclarationType.None
            }, nestedKind);
        }


        return (fullTypeName switch
        {
            "System.Single" => DeclarationType.Float,
            "System.Double" => DeclarationType.Double,
            "System.Byte" => DeclarationType.Raw,
            "System.Int64" => DeclarationType.Integer,
            "System.Boolean" => DeclarationType.Boolean,
            "System.String" => DeclarationType.String,
            _ => DeclarationType.None
        }, nestedKind);
    }

    public static LoggableMember? ToLoggableMember(this ISymbol member, CancellationToken token, out DiagnosticInfo? diagnostic)
    {
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
                    diagnostic = DiagnosticInfo.Create(GeneratorDiagnostics.LoggedMethodDoesntReturnVoid, null, [method.Name]);
                    return null;
                }
                if (!method.Parameters.IsEmpty)
                {
                    diagnostic = DiagnosticInfo.Create(GeneratorDiagnostics.LoggedMethodTakesArguments, null, [method.Name]);
                    return null;
                }
                logType = method.ReturnType;
                memberType = MemberType.Method;
            }
            else
            {
                diagnostic = DiagnosticInfo.Create(GeneratorDiagnostics.LoggedMemberTypeNotSupported, null, [member.Name]);
                return null;
            }
            token.ThrowIfCancellationRequested();

            var declType = logType.GetDeclarationType(attributeInfo, token);
            token.ThrowIfCancellationRequested();
            if (declType is null)
            {
                // TODO change this to unsupported type
                diagnostic = DiagnosticInfo.Create(GeneratorDiagnostics.GeneratedTypeIsInterface, null, null);
                return null;
            }

            diagnostic = null;
            return new LoggableMember(member.Name, memberType, declType.Value.Item1, declType.Value.Item2, attributeInfo);
        }
        diagnostic = null;
        return null;
    }
}
