using System.Text;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.VisualBasic.Testing;
using Stereologue;
using WPILib.CodeHelpers.Core.LogGenerator.Analyzer;
using CSharpLogGeneratorFixer = WPILib.CodeHelpers.CodeFixes.CSharp.LogGenerator.CodeFixer.LogGeneratorFixer;
using CSharpVerify = Microsoft.CodeAnalysis.CSharp.Testing.CSharpCodeFixVerifier<WPILib.CodeHelpers.Core.LogGenerator.Analyzer.LogGeneratorAnalyzer, WPILib.CodeHelpers.CodeFixes.CSharp.LogGenerator.CodeFixer.LogGeneratorFixer, Microsoft.CodeAnalysis.Testing.DefaultVerifier>;
using VisualBasicLogGeneratorFixer = WPILib.CodeHelpers.CodeFixes.VisualBasic.LogGenerator.CodeFixer.LogGeneratorFixer;
using VisualBasicVerify = Microsoft.CodeAnalysis.VisualBasic.Testing.VisualBasicCodeFixVerifier<WPILib.CodeHelpers.Core.LogGenerator.Analyzer.LogGeneratorAnalyzer, WPILib.CodeHelpers.CodeFixes.VisualBasic.LogGenerator.CodeFixer.LogGeneratorFixer, Microsoft.CodeAnalysis.Testing.DefaultVerifier>;

namespace CodeHelpers.Test.LogGenerator;

public class LogGeneratorFixerTest
{
    [Fact]
    public async Task Test1()
    {
        string testString = @"
using Stereologue;

public partial class MyNewClass
{
    [Log]
    public int Variable { get; }
}
";
        string fixedCode = @"
using Stereologue;

[GenerateLog]
public partial class MyNewClass
{
    [Log]
    public int Variable { get; }
}
";

        await new CSharpCodeFixTest<LogGeneratorAnalyzer, CSharpLogGeneratorFixer, DefaultVerifier>()
        {
            TestState = {
                AdditionalReferences = {
                    typeof(LogAttribute).Assembly
                },
                ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
                Sources = {
                    testString,
                },
                AnalyzerConfigFiles = {
                    ("/.editorconfig", SourceText.From(TestHelpers.EditorConfig, Encoding.UTF8))
                }
            },
            FixedCode = fixedCode,
            ExpectedDiagnostics = {
                CSharpVerify.Diagnostic(LoggerDiagnostics.MissingGenerateLog).WithLocation(4, 22).WithArguments(["MyNewClass"])
            }

        }.RunAsync();
    }

    [Fact]
    public async Task TestVb()
    {
        string testString = @"
Imports Stereologue

Public Partial Class MyNewClass
    <Log>
    Public ReadOnly Property Variable As Integer
End Class
";
        string fixedCode = @"
Imports Stereologue

<GenerateLog>
Public Partial Class MyNewClass
    <Log>
    Public ReadOnly Property Variable As Integer
End Class
";

        await new VisualBasicCodeFixTest<LogGeneratorAnalyzer, VisualBasicLogGeneratorFixer, DefaultVerifier>()
        {
            TestState = {
                AdditionalReferences = {
                    typeof(LogAttribute).Assembly
                },
                ReferenceAssemblies = ReferenceAssemblies.Net.Net100,
                Sources = {
                    testString,
                },
                AnalyzerConfigFiles = {
                    ("/.editorconfig", SourceText.From(TestHelpers.EditorConfig, Encoding.UTF8))
                }
            },
            FixedCode = fixedCode,
            ExpectedDiagnostics = {
                VisualBasicVerify.Diagnostic(LoggerDiagnostics.MissingGenerateLog).WithLocation(4, 22).WithArguments(["MyNewClass"])
            }

        }.RunAsync();
    }
}
