using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Stereologue.SourceGenerator;

internal enum DeclarationType
{
    Logged,
    Struct,
    Protobuf,
    Other
}


internal record LogAttributeInfo(string Path, string LogLevel, string LogType, bool UseProtobuf);

internal record LogData(string GetOperation, string? Type, DeclarationType DecelType, LogAttributeInfo AttributeInfo);

internal record ClassData(ImmutableArray<LogData> LoggedItems, string Name, string ClassDeclaration, string? Namespace);

[Generator]
public class LogGenerator : IIncrementalGenerator
{
    static ClassData? GetClassData(SemanticModel semanticModel, SyntaxNode classDeclarationSyntax, CancellationToken token)
    {
        if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol classSymbol)
        {
            return null;
        }
        token.ThrowIfCancellationRequested();

        var ns = classSymbol.ContainingNamespace?.ToDisplayString();
        token.ThrowIfCancellationRequested();
        StringBuilder typeBuilder = new StringBuilder();
        classSymbol.GetTypeDeclaration(typeBuilder, token);
        token.ThrowIfCancellationRequested();

        var classMembers = classSymbol.GetMembers();

        var loggableMembers = ImmutableArray.CreateBuilder<LogData>(classMembers.Length);

        foreach (var member in classMembers)
        {
            token.ThrowIfCancellationRequested();
            var attributes = member.GetAttributes();
            token.ThrowIfCancellationRequested();

            foreach (AttributeData attribute in attributes)
            {
                token.ThrowIfCancellationRequested();
                var attributeClass = attribute.AttributeClass;
                if (attributeClass is null)
                {
                    continue;
                }
                if (attributeClass.ToDisplayString() == "Stereologue.LogAttribute")
                {
                    token.ThrowIfCancellationRequested();

                    string path = member.Name;
                    bool useProtobuf = false;
                    string logTypeEnum = "Stereologue.LogType.Nt | Stereologue.LogType.File";
                    string logLevel = "Stereologue.LogLevel.Default";

                    // Get the log attribute
                    foreach (var named in attribute.NamedArguments)
                    {
                        if (named.Key == "Key")
                        {
                            if (!named.Value.IsNull)
                            {
                                path = SymbolDisplay.FormatPrimitive(named.Value.Value!, false, false);
                            }
                        }
                        else if (named.Key == "LogLevel")
                        {
                            logLevel = named.Value.ToCSharpString();
                        }
                        else if (named.Key == "LogType")
                        {
                            logTypeEnum = named.Value.ToCSharpString();
                        }
                        else if (named.Key == "UseProtobuf")
                        {
                            useProtobuf = (bool)named.Value.Value!;
                        }
                    }

                    var attributeInfo = new LogAttributeInfo(path, logLevel, logTypeEnum, useProtobuf);

                    string getOperation;
                    ITypeSymbol logType;
                    // This is ours
                    if (member is IFieldSymbol field)
                    {
                        getOperation = field.Name;
                        logType = field.Type;
                    }
                    else if (member is IPropertySymbol property)
                    {
                        getOperation = property.Name;
                        logType = property.Type;
                    }
                    else if (member is IMethodSymbol method)
                    {
                        if (method.ReturnsVoid)
                        {
                            throw new InvalidOperationException("Cannot have a void returning method");
                        }
                        if (!method.Parameters.IsEmpty)
                        {
                            throw new InvalidOperationException("Cannot take a parameter");
                        }

                        getOperation = $"{method.Name}()";
                        logType = method.ReturnType;
                    }
                    else
                    {
                        throw new InvalidOperationException("Field is not loggable");
                    }

                    var fullOperation = ComputeOperation(logType, getOperation, attributeInfo);
                    token.ThrowIfCancellationRequested();
                    loggableMembers.Add(fullOperation);
                    break;
                }
            }
        }

        var fmt = new SymbolDisplayFormat(genericsOptions: SymbolDisplayGenericsOptions.None);
        var fileName = $"{classSymbol.ContainingNamespace}{classSymbol.ToDisplayString(fmt)}{classSymbol.MetadataName}";

        return new ClassData(loggableMembers.ToImmutable(), $"{classSymbol.ContainingNamespace}{classSymbol.ToDisplayString(fmt)}{classSymbol.MetadataName}", typeBuilder.ToString(), ns);
    }

    private static LogData ComputeOperation(ITypeSymbol logType, string getOp, LogAttributeInfo attributeInfo)
    {
        if (logType.GetAttributes().Where(x => x.AttributeClass?.ToDisplayString() == "Stereologue.GenerateLogAttribute").Any())
        {
            return new LogData(getOp, null, DeclarationType.Logged, attributeInfo);
        }
        if (logType.AllInterfaces.Where(x => x.ToDisplayString() == "Stereologue.ILogged").Any())
        {
            return new LogData(getOp, null, DeclarationType.Logged, attributeInfo);
        }
        var fmt = new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters);
        var fullTypeName = logType.ToDisplayString(fmt);
        var structName = $"WPIUtil.Serialization.Struct.IStructSerializable<{fullTypeName}>";
        var protobufName = $"WPIUtil.Serialization.Protobuf.IProtobufSerializable<{fullTypeName}>";

        foreach (var inf in logType.AllInterfaces)
        {
            var interfaceName = inf.ToDisplayString();
            if (interfaceName == structName)
            {
                if (!attributeInfo.UseProtobuf)
                {
                    return new LogData(getOp, null, DeclarationType.Struct, attributeInfo);
                }
            }
            else if (interfaceName == protobufName)
            {
                if (attributeInfo.UseProtobuf)
                {
                    return new LogData(getOp, null, DeclarationType.Protobuf, attributeInfo);
                }
            }
        }

        return new LogData(getOp, fullTypeName, DeclarationType.Other, attributeInfo);
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var attributedTypes = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "Stereologue.GenerateLogAttribute",
                predicate: static (s, _) => s is TypeDeclarationSyntax,
                transform: static (ctx, token) => GetClassData(ctx.SemanticModel, ctx.TargetNode, token))
            .Where(static m => m is not null);

        context.RegisterSourceOutput(attributedTypes,
            static (spc, source) => Execute(source, spc));
    }

    static void ConstructCall(LogData data, StringBuilder builder)
    {
        builder.Append("        ");

        switch (data.DecelType)
        {
            case DeclarationType.Logged:
                builder.AppendLine($"{data.GetOperation}?.UpdateStereologue($\"{{path}}/{data.AttributeInfo.Path}\", logger, {data.AttributeInfo.LogLevel});");
                return;
            case DeclarationType.Struct:
                builder.AppendLine($"logger.LogStruct($\"{{path}}/{data.AttributeInfo.Path}\", {data.AttributeInfo.LogType}, {data.GetOperation}, {data.AttributeInfo.LogLevel});");
                return;
            case DeclarationType.Protobuf:
                builder.AppendLine($"logger.LogProto($\"{{path}}/{data.AttributeInfo.Path}\", {data.AttributeInfo.LogType}, {data.GetOperation}, {data.AttributeInfo.LogLevel});");
                return;
        }

        (string? LogMethod, string Cast, string Conversion) ret = data.Type switch
        {
            "System.Single" => ("LogFloat", "", ""),
            "System.Double" => ("LogDouble", "", ""),
            "System.Byte" => ("LogInteger", "", ""),
            "System.SByte" => ("LogInteger", "", ""),
            "System.Int16" => ("LogInteger", "", ""),
            "System.UInt16" => ("LogInteger", "", ""),
            "System.Int32" => ("LogInteger", "", ""),
            "System.UInt32" => ("LogInteger", "", ""),
            "System.Int64" => ("LogInteger", "", ""),
            "System.UInt64" => ("LogInteger", "(long)", ""),
            "System.Boolean" => ("LogBoolean", "", ""),
            "System.Char" => ("LogChar", "", ""),
            "System.String" => ("LogString", "", ""),
            "System.Single[]" => ("LogFloatArray", "", ".AsSpan()"),
            "System.Double[]" => ("LogDoubleArray", "", ".AsSpan()"),
            "System.Int64[]" => ("LogIntegerArray", "", ".AsSpan()"),
            "System.String[]" => ("LogStringArray", "", ".AsSpan()"),
            "System.Byte[]" => ("LogRaw", "", ".AsSpan()"),
            "System.Boolean[]" => ("LogBooleanArray", "", ".AsSpan()"),
            "System.ReadOnlySpan<System.Char>" => ("LogString", "", ""),
            "System.ReadOnlySpan<System.Single>" => ("LogFloatArray", "", ""),
            "System.ReadOnlySpan<System.Double>" => ("LogDoubleArray", "", ""),
            "System.ReadOnlySpan<System.Int64>" => ("LogIntegerArray", "", ""),
            "System.ReadOnlySpan<System.String>" => ("LogStringArray", "", ""),
            "System.ReadOnlySpan<System.Byte>" => ("LogRaw", "", ""),
            "System.ReadOnlySpan<System.Boolean>" => ("LogBooleanArray", "", ""),
            "System.Span<System.Single>" => ("LogFloatArray", "", ""),
            "System.Span<System.Double>" => ("LogDoubleArray", "", ""),
            "System.Span<System.Int64>" => ("LogIntegerArray", "", ""),
            "System.Span<System.String>" => ("LogStringArray", "", ""),
            "System.Span<System.Byte>" => ("LogRaw", "", ""),
            "System.Span<System.Boolean>" => ("LogBooleanArray", "", ""),
            _ => (data.Type, "", "")
        };

        builder.AppendLine($"logger.{ret.LogMethod}($\"{{path}}/{data.AttributeInfo.Path}\", {data.AttributeInfo.LogType}, {ret.Cast}{data.GetOperation}{ret.Conversion}, {data.AttributeInfo.LogLevel});");
    }

    static void Execute(ClassData? classData, SourceProductionContext context)
    {
        if (classData is { } value)
        {
            StringBuilder builder = new StringBuilder();
            if (value.Namespace is not null)
            {
                builder.AppendLine($"namespace {value.Namespace};");
                builder.AppendLine();

                builder.AppendLine(value.ClassDeclaration);
                builder.AppendLine("{");
                builder.AppendLine("    public void UpdateStereologue(string path, Stereologue.Stereologuer logger)");
                builder.AppendLine("    {");
                foreach (var call in value.LoggedItems)
                {
                    ConstructCall(call, builder);
                }
                builder.AppendLine("    }");
                builder.AppendLine("}");
            }
            context.AddSource($"Stereologue.{value.Name}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }

    private static object? GetDiagnosticIfInvalidClassForGeneration(TypeDeclarationSyntax syntax, ITypeSymbol symbol)
    {
        // Ensure class is partial
        if (!syntax.IsInPartialContext(out var nonPartialIdentifier))
        {
            return new object();
        }

        return null;
    }
}
