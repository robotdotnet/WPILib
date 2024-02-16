using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Monologue.SourceGenerator;

internal record LogData(string? PreComputed, string? GetOperation, string? Path, string? Type);

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
                if (attributeClass.ToDisplayString() == "Monologue.LogAttribute")
                {
                    token.ThrowIfCancellationRequested();
                    string getOperation;
                    string defaultPathName;
                    ITypeSymbol logType;
                    // This is ours
                    if (member is IFieldSymbol field)
                    {
                        getOperation = field.Name;
                        defaultPathName = field.Name;
                        logType = field.Type;
                    }
                    else if (member is IPropertySymbol property)
                    {
                        getOperation = property.Name;
                        defaultPathName = property.Name;
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
                        defaultPathName = method.Name;
                        logType = method.ReturnType;
                    }
                    else
                    {
                        throw new InvalidOperationException("Field is not loggable");
                    }

                    var fullOperation = ComputeOperation(logType, getOperation, defaultPathName);
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

    private static LogData ComputeOperation(ITypeSymbol logType, string getOp, string path)
    {
        if (logType.GetAttributes().Where(x => x.AttributeClass?.ToDisplayString() == "Monologue.GenerateLogAttribute").Any())
        {
            return new($"{getOp}.UpdateMonologue($\"{{path}}/{path}\", logger);", null, null, null);
        }
        if (logType.AllInterfaces.Where(x => x.ToDisplayString() == "Monologue.ILogged").Any())
        {
            return new($"{getOp}.UpdateMonologue($\"{{path}}/{path}\", logger);", null, null, null);
            //return $"{getOp}.UpdateMonologue($\"{{path}}/{path}\", logger);";
        }
        var fullName = logType.ToDisplayString();
        var structName = $"WPIUtil.Serialization.Struct.IStructSerializable<{fullName}>";
        var protobufName = $"WPIUtil.Serialization.Protobuf.IProtobufSerializable<{fullName}>";
        foreach (var inf in logType.AllInterfaces)
        {
            var interfaceName = inf.ToDisplayString();
            // For now prefer struct
            if (interfaceName == structName)
            {
                return new($"logger.LogStruct($\"{{path}}/{path}\", LogType.Nt, {getOp});", null, null, null);
            }
            else if (interfaceName == protobufName)
            {
                return new($"logger.LogProto($\"{{path}}/{path}\", LogType.Nt, {getOp});", null, null, null);
            }
        }

        return new(null, getOp, path, fullName);
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var attributedTypes = context.SyntaxProvider
            .ForAttributeWithMetadataName(
                "Monologue.GenerateLogAttribute",
                predicate: static (s, _) => s is TypeDeclarationSyntax,
                transform: static (ctx, token) => GetClassData(ctx.SemanticModel, ctx.TargetNode, token))
            .Where(static m => m is not null);

        context.RegisterSourceOutput(attributedTypes,
            static (spc, source) => Execute(source, spc));
    }

    static void ConstructCall(LogData data, StringBuilder builder)
    {
        if (data.PreComputed is not null) {

            builder.AppendLine($"        {data.PreComputed}");
            return;
        }

        var ret = data.Type switch {
            "float" => ("LogFloat", ""),
            "double" => ("LogDouble", ""),
            "byte" => ("LogInteger", ""),
            "sbyte" => ("LogInteger", ""),
            "short" => ("LogInteger", ""),
            "ushort" => ("LogInteger", ""),
            "int" => ("LogInteger", ""),
            "uint" => ("LogInteger", ""),
            "long" => ("LogInteger", ""),
            "ulong" => ("LogInteger", "(long)"),
            "bool" => ("LogBoolean", ""),
            "char" => ("LogChar", ""),
            _ => (data.Type, "")
        };

        builder.AppendLine($"        logger.{ret.Item1}($\"{{path}}/{data.Path}\", LogType.Nt, {ret.Item2}{data.GetOperation});");
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
                builder.AppendLine("    public void UpdateMonologue(string path, Monologue.Monologuer logger)");
                builder.AppendLine("    {");
                foreach (var call in value.LoggedItems)
                {
                    ConstructCall(call, builder);
                }
                builder.AppendLine("    }");
                builder.AppendLine("}");
            }
            context.AddSource($"Monologue.{value.Name}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
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
