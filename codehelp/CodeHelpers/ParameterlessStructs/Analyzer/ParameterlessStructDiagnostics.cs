namespace WPILib.CodeHelpers.ParameterlessStructs.Analyzer;

#pragma warning disable RS2008 // Enable analyzer release tracking

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using WPILib.CodeHelpers;

public static class ParameterlessStructDiagnostics
{
    public class Ids
    {
        public const string Prefix = "WPILIB";
        public const string ParameterlessConstructorMissing = Prefix + "1100";
    }

    private const string Category = "ParameterlessStructs";

    public static readonly DiagnosticDescriptor ParameterlessConstructorMissing = new(
        Ids.ParameterlessConstructorMissing, "Struct has field initializers but no parameterless constructor", "Struct has field initializers but no parameterless constructor", Category, DiagnosticSeverity.Warning, false);
}
