namespace WPILib.CodeHelpers.ActionFuncDetector.Analyzer;

#pragma warning disable RS2008 // Enable analyzer release tracking

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using WPILib.CodeHelpers;

public static class ActionFuncDiagnostics
{
    public class Ids
    {
        public const string Prefix = "WPILIB";
        public const string UseOfActionFunc = Prefix + "1100";
    }

    private const string Category = "AllowedPublicTypes";

    public static readonly DiagnosticDescriptor UseOfActionFunc = new(
        Ids.UseOfActionFunc, "Action/Func is used in a public API", "Use of Action or Func in a public API is disallowed", Category, DiagnosticSeverity.Warning, false);
}
