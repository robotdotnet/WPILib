using System.Text;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Microsoft.CodeAnalysis.Text;
using Stereologue;
using WPILib.CodeHelpers.LogGenerator.Analyzer;
using WPILib.CodeHelpers.LogGenerator.CodeFixer;
using Verify = Microsoft.CodeAnalysis.CSharp.Testing.XUnit.CodeFixVerifier<WPILib.CodeHelpers.LogGenerator.Analyzer.LogGeneratorAnalyzer, WPILib.CodeHelpers.LogGenerator.CodeFixer.LogGeneratorFixer>;

namespace CodeHelpers.Test.LogGenerator;

public class LogGeneratorFixerTest
{
    [Fact]
    public async void Test1()
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

        await new CSharpCodeFixTest<LogGeneratorAnalyzer, LogGeneratorFixer, XUnitVerifier>()
        {
            TestState = {
                AdditionalReferences = {
                    typeof(LogAttribute).Assembly
                },
                ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
                Sources = {
                    testString,
                },
                AnalyzerConfigFiles = {
                    ("/.editorconfig", SourceText.From(TestHelpers.EditorConfig, Encoding.UTF8))
                }
            },
            FixedCode = fixedCode,
            ExpectedDiagnostics = {
                Verify.Diagnostic(LoggerDiagnostics.MissingGenerateLog).WithLocation(4, 22).WithArguments(["MyNewClass"])
            }

        }.RunAsync();
    }
}
