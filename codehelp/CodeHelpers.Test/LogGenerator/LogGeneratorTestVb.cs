namespace CodeHelpers.Test.LogGenerator;

using System.Text;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.VisualBasic.Testing;
using Stereologue;
using WPILib.CodeHelpers.LogGenerator.SourceGenerator;

public class LogGeneratorTestVb
{
    [Theory]
    [InlineData("Boolean", "LogBoolean")]
    [InlineData("Integer", "LogInteger")]
    [InlineData("Long", "LogInteger")]
    public async Task TestPrimitives(string type, string output)
    {
        string testString = @"
Imports Stereologue

<GenerateLog()>
Public Partial Class MyNewClass
    <Log()>
    Public Function Variable() as REPLACEME
            Dim ret as REPLACEME
            Return ret
    End Function
End Class
";

        string expected = @"Partial Class MyNewClass
    Implements Global.Stereologue.ILogged
    Public Sub UpdateStereologue(path as String, logger as Global.Stereologue.Stereologuer) Implements Global.Stereologue.ILogged.UpdateStereologue
        logger.REPLACEME($""{path}/Variable"", Global.Stereologue.LogType.File Or Global.Stereologue.LogType.Nt, Variable(), Global.Stereologue.LogLevel.Default)
    End Sub
End Class
";
        testString = testString.Replace("REPLACEME", type);

        // Due to StringBuilder in the source generator,
        // We must normalize the output line endings
        expected = expected.NormalizeLineEndings();
        expected = expected.Replace("REPLACEME", output);

        await new VisualBasicSourceGeneratorTest<LogGeneratorVb, DefaultVerifier>()
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
                },
                GeneratedSources = {
                    ($"WPILib.CodeHelpers{Path.DirectorySeparatorChar}WPILib.CodeHelpers.LogGenerator.SourceGenerator.LogGeneratorVb{Path.DirectorySeparatorChar}MyNewClass.g.vb", SourceText.From(expected, Encoding.UTF8))
                },
            },
        }.RunAsync();
    }
}
