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
    StructArray,
    Protobuf,
    Boolean,
    Float,
    Double,
    Integer,
    String,
    BooleanArray,
    FloatArray,
    DoubleArray,
    IntegerArray,
    Raw,
    StringArray,
    Char,
}

internal enum DeclarationModifiers
{
    None,
    AsSpan,
    LongCast,
    AllowNullConditionalOperator,
}

// Contains all information about a loggable member
internal record LoggableMember(string Name, MemberType MemberType, DeclarationType LoggedType, DeclarationModifiers LoggedModifiers, LogAttributeInfo AttributeInfo);

internal static class LoggableMemberExtensions
{
    private static (DeclarationType, DeclarationModifiers)? GetDeclarationType(this ITypeSymbol typeSymbol, LogAttributeInfo attributeInfo, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        var modifiers = DeclarationModifiers.None;

        if (typeSymbol.IsReferenceType)
        {
            modifiers = DeclarationModifiers.AllowNullConditionalOperator;
        }
        else if (typeSymbol.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
        {
            // Pull out the inner type
            var namedTypeSymbol = (INamedTypeSymbol)typeSymbol;
            var innerType = namedTypeSymbol.TypeArguments[0];
            typeSymbol = innerType;

            modifiers = DeclarationModifiers.AllowNullConditionalOperator;
        }

        token.ThrowIfCancellationRequested();

        // If we know we're generating a loggable implementation
        if (typeSymbol.GetAttributes().Where(x => x.AttributeClass?.ToDisplayString() == "Stereologue.GenerateLogAttribute").Any())
        {
            return (DeclarationType.Logged, modifiers);
        }
        token.ThrowIfCancellationRequested();
        // If we know we already implement ILogged
        if (typeSymbol.AllInterfaces.Where(x => x.ToDisplayString() == "Stereologue.ILogged").Any())
        {
            return (DeclarationType.Logged, modifiers);
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
                    return (DeclarationType.Logged, modifiers);
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
                // TODO figure out arrays of structs
                if (!attributeInfo.UseProtobuf)
                {
                    return (DeclarationType.Struct, DeclarationModifiers.None);
                }
            }
            else if (interfaceName == protobufName)
            {
                if (attributeInfo.UseProtobuf)
                {
                    return (DeclarationType.Protobuf, DeclarationModifiers.None);
                }
            }
        }

        return fullTypeName switch
        {
            "System.Single" => (DeclarationType.Float, DeclarationModifiers.None),
            "System.Double" => (DeclarationType.Double, DeclarationModifiers.None),
            "System.Byte" => (DeclarationType.Integer, DeclarationModifiers.None),
            "System.SByte" => (DeclarationType.Integer, DeclarationModifiers.None),
            "System.Int16" => (DeclarationType.Integer, DeclarationModifiers.None),
            "System.UInt16" => (DeclarationType.Integer, DeclarationModifiers.None),
            "System.Int32" => (DeclarationType.Integer, DeclarationModifiers.None),
            "System.UInt32" => (DeclarationType.Integer, DeclarationModifiers.None),
            "System.Int64" => (DeclarationType.Integer, DeclarationModifiers.None),
            "System.UInt64" => (DeclarationType.Integer, DeclarationModifiers.LongCast),
            "System.Boolean" => (DeclarationType.Boolean, DeclarationModifiers.None),
            "System.Char" => (DeclarationType.Char, DeclarationModifiers.None),
            "System.String" => (DeclarationType.String, DeclarationModifiers.AsSpan),
            "System.Single[]" => (DeclarationType.FloatArray, DeclarationModifiers.AsSpan),
            "System.Double[]" => (DeclarationType.DoubleArray, DeclarationModifiers.AsSpan),
            "System.Int64[]" => (DeclarationType.IntegerArray, DeclarationModifiers.AsSpan),
            "System.String[]" => (DeclarationType.StringArray, DeclarationModifiers.AsSpan),
            "System.Byte[]" => (DeclarationType.Raw, DeclarationModifiers.AsSpan),
            "System.Boolean[]" => (DeclarationType.BooleanArray, DeclarationModifiers.AsSpan),
            "System.ReadOnlySpan<System.Char>" => (DeclarationType.String, DeclarationModifiers.None),
            "System.ReadOnlySpan<System.Single>" => (DeclarationType.FloatArray, DeclarationModifiers.None),
            "System.ReadOnlySpan<System.Double>" => (DeclarationType.DoubleArray, DeclarationModifiers.None),
            "System.ReadOnlySpan<System.Int64>" => (DeclarationType.IntegerArray, DeclarationModifiers.None),
            "System.ReadOnlySpan<System.String>" => (DeclarationType.StringArray, DeclarationModifiers.None),
            "System.ReadOnlySpan<System.Byte>" => (DeclarationType.Raw, DeclarationModifiers.None),
            "System.ReadOnlySpan<System.Boolean>" => (DeclarationType.BooleanArray, DeclarationModifiers.None),
            "System.Span<System.Single>" => (DeclarationType.FloatArray, DeclarationModifiers.None),
            "System.Span<System.Double>" => (DeclarationType.DoubleArray, DeclarationModifiers.None),
            "System.Span<System.Int64>" => (DeclarationType.IntegerArray, DeclarationModifiers.None),
            "System.Span<System.String>" => (DeclarationType.StringArray, DeclarationModifiers.None),
            "System.Span<System.Byte>" => (DeclarationType.Raw, DeclarationModifiers.None),
            "System.Span<System.Boolean>" => (DeclarationType.BooleanArray, DeclarationModifiers.None),
            "System.ReadOnlyMemory<System.Char>" => (DeclarationType.String, DeclarationModifiers.AsSpan),
            "System.ReadOnlyMemory<System.Single>" => (DeclarationType.FloatArray, DeclarationModifiers.AsSpan),
            "System.ReadOnlyMemory<System.Double>" => (DeclarationType.DoubleArray, DeclarationModifiers.AsSpan),
            "System.ReadOnlyMemory<System.Int64>" => (DeclarationType.IntegerArray, DeclarationModifiers.AsSpan),
            "System.ReadOnlyMemory<System.String>" => (DeclarationType.StringArray, DeclarationModifiers.AsSpan),
            "System.ReadOnlyMemory<System.Byte>" => (DeclarationType.Raw, DeclarationModifiers.AsSpan),
            "System.ReadOnlyMemory<System.Boolean>" => (DeclarationType.BooleanArray, DeclarationModifiers.AsSpan),
            "System.Memory<System.Single>" => (DeclarationType.FloatArray, DeclarationModifiers.AsSpan),
            "System.Memory<System.Double>" => (DeclarationType.DoubleArray, DeclarationModifiers.AsSpan),
            "System.Memory<System.Int64>" => (DeclarationType.IntegerArray, DeclarationModifiers.AsSpan),
            "System.Memory<System.String>" => (DeclarationType.StringArray, DeclarationModifiers.AsSpan),
            "System.Memory<System.Byte>" => (DeclarationType.Raw, DeclarationModifiers.AsSpan),
            "System.Memory<System.Boolean>" => (DeclarationType.BooleanArray, DeclarationModifiers.AsSpan),
            _ => (DeclarationType.None, DeclarationModifiers.None)
        };
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
