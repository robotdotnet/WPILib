namespace WPILib.CodeHelpers.ParameterlessStructs.Analyzer;

#pragma warning disable RS2008 // Enable analyzer release tracking

using Microsoft.CodeAnalysis;

public static class ParameterlessStructDiagnostics
{
    public class Ids
    {
        public const string Prefix = "WPILIB";
        public const string WPIMathHasFieldInitializers = Prefix + "1100";
        public const string WPIMathMustHaveParameterlessConstructor = Prefix + "1101";
    }

    private const string Category = "ParameterlessStructs";

    public static readonly DiagnosticDescriptor WPIMathHasFieldInitializers = new(
        Ids.WPIMathHasFieldInitializers, "WPIMath struct has field initializers", "All structs in WPIMath must not have field or property initializers", Category, DiagnosticSeverity.Warning, false);
    public static readonly DiagnosticDescriptor WPIMathMustHaveParameterlessConstructor = new(
        Ids.WPIMathMustHaveParameterlessConstructor, "WPIMath struct does not have explicit parameterless constructor", "All structs in WPIMath must have explicit parameterless constructors", Category, DiagnosticSeverity.Warning, false);
}
