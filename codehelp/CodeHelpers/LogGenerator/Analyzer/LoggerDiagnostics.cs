namespace WPILib.CodeHelpers.LogGenerator.Analyzer;

#pragma warning disable RS2008 // Enable analyzer release tracking

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using WPILib.CodeHelpers;

public static class LoggerDiagnostics
{
    public class Ids
    {
        public const string Prefix = "WPILIB";
        public const string UnknownMemberType = Prefix + "1000";
        public const string ProtobufIsArray = Prefix + "1001";
        public const string UnknownSpecialTypeArray = Prefix + "1002";
        public const string LoggedMethodReturnsVoid = Prefix + "1003";
        public const string LoggedMethodTakeParameters = Prefix + "1004";
        public const string LoggedHasUnknownType = Prefix + "1005";
        public const string UnknownFailureMode = Prefix + "1006";
        public const string NullableStructArray = Prefix + "1007";
        public const string UnknownSpecialTypeIntArray = Prefix + "1008";
        public const string MissingGenerateLog = Prefix + "1008";
    }

    private const string Category = "SourceGeneration";

    public static readonly DiagnosticDescriptor UnknownMemberType = new(
        Ids.UnknownMemberType, "Loggable member call type is unknown", "[Log] attribute cannot be applied to member '{0}'. Member type is unknown.", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");
    public static readonly DiagnosticDescriptor ProtobufIsArray = new(
        Ids.ProtobufIsArray, "Loggable member is array of protobufs", "[Log] attribute cannot be applied to member '{0}'. Cannot log array of Protobufs.", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");
    public static readonly DiagnosticDescriptor UnknownSpecialTypeArray = new(
        Ids.UnknownSpecialTypeArray, "Loggable member has invalid type for array use", "[Log] attribute cannot be applied to member '{0}'. Cannot log arrays of type '{1}'.", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");
    public static readonly DiagnosticDescriptor LoggedMethodReturnsVoid = new(
        Ids.LoggedMethodReturnsVoid, "Loggable method returns void", "[Log] attribute cannot be applied to member '{0}'. Cannot log from a void returning method.", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");
    public static readonly DiagnosticDescriptor LoggedMethodTakeParameters = new(
        Ids.LoggedMethodTakeParameters, "Loggable method takes parameters", "[Log] attribute cannot be applied to member '{0}'. Cannot log from a method taking parameters.", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");
    public static readonly DiagnosticDescriptor LoggedHasUnknownType = new(
        Ids.LoggedHasUnknownType, "Loggable member type is not loggable", "[Log] attribute cannot be applied to member '{0}'; cannot log type '{1}'", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");
    public static readonly DiagnosticDescriptor UnknownFailureMode = new(
        Ids.UnknownFailureMode, "Unknown Failure Mode", "Failure mode has no diagnostic. Report to RobotDotNet.", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");
    public static readonly DiagnosticDescriptor NullableStructArray = new(
        Ids.NullableStructArray, "Loggable member is array of Nullable<Struct>", "[Log] attribute cannot be applied to member '{0}'. Cannot log arrays of Nullable Structs.", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");
    public static readonly DiagnosticDescriptor UnknownSpecialTypeIntArray = new(
        Ids.UnknownSpecialTypeIntArray, "Loggable member has invalid integer type for array use", "[Log] attribute cannot be applied to member '{0}'. Cannot log arrays of type '{1}'. Can only log arrays of 'long'.", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");
    public static readonly DiagnosticDescriptor MissingGenerateLog = new(
        Ids.MissingGenerateLog, "Type has Log member but is not GenerateLog", "Type {0} has a member annotated with [Log], but is not attributed with [GenerateLog]. No logging will be generated for this type.", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");

    public static void ReportDiagnostic(this SymbolAnalysisContext context, FailureMode failureMode, ISymbol symbol)
    {
        ISymbol typeSymbol = symbol switch
        {
            IFieldSymbol field => field.Type,
            IPropertySymbol property => property.Type,
            IMethodSymbol method => method.ReturnType,
            _ => symbol,
        };
        foreach (var location in symbol.Locations)
        {
            switch (failureMode)
            {
                case FailureMode.UnknownTypeToLog:
                    context.ReportDiagnostic(Diagnostic.Create(LoggedHasUnknownType, location, symbol.Name, typeSymbol?.Name));
                    break;
                case FailureMode.AttributeUnknownMemberType:
                    context.ReportDiagnostic(Diagnostic.Create(UnknownMemberType, location, symbol.Name));
                    break;
                case FailureMode.ProtobufArray:
                    context.ReportDiagnostic(Diagnostic.Create(ProtobufIsArray, location, symbol.Name));
                    break;
                case FailureMode.UnknownTypeNonArray:
                    context.ReportDiagnostic(Diagnostic.Create(LoggedHasUnknownType, location, symbol.Name, typeSymbol?.Name));
                    break;
                case FailureMode.UnknownTypeArray:
                    if (typeSymbol is ITypeSymbol actualTypeSymbol)
                    {
                        actualTypeSymbol.GetInnerType(out var innerSymbol);
                        if (innerSymbol.SpecialType.IsIntegerLikeType())
                        {
                            context.ReportDiagnostic(Diagnostic.Create(UnknownSpecialTypeIntArray, location, symbol.Name, typeSymbol?.Name));
                            break;
                        }
                    }
                    context.ReportDiagnostic(Diagnostic.Create(UnknownSpecialTypeArray, location, symbol.Name, typeSymbol?.Name));
                    break;
                case FailureMode.MethodReturnsVoid:
                    context.ReportDiagnostic(Diagnostic.Create(LoggedMethodReturnsVoid, location, symbol.Name));
                    break;
                case FailureMode.MethodHasParameters:
                    context.ReportDiagnostic(Diagnostic.Create(LoggedMethodTakeParameters, location, symbol.Name));
                    break;
                case FailureMode.NullableStructArray:
                    context.ReportDiagnostic(Diagnostic.Create(NullableStructArray, location, symbol.Name));
                    break;
                case FailureMode.MissingGenerateLog:
                    context.ReportDiagnostic(Diagnostic.Create(MissingGenerateLog, location, symbol.Name, symbol.ContainingType?.Name));
                    break;
                default:
                    context.ReportDiagnostic(Diagnostic.Create(UnknownFailureMode, location));
                    break;
            }
        }
    }
}
