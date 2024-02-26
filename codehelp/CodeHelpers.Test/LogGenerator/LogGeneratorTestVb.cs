namespace CodeHelpers.Test.LogGenerator;

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Microsoft.CodeAnalysis.Text;
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
        testString = testString.Replace("\r\n", "\n").Replace("\n", "\r\n");
        testString = testString.Replace("REPLACEME", type);

        expected = expected.Replace("\r\n", "\n").Replace("\n", "\r\n");
        expected = expected.Replace("REPLACEME", output);

        await new VisualBasicSourceGeneratorTest<LogGeneratorVb, XUnitVerifier>()
        {
            CompilerDiagnostics = CompilerDiagnostics.None,
            TestState = {
                AdditionalProjectReferences = { "AttributesAssembly", },
                AdditionalProjects = {
                    ["AttributesAssembly", LanguageNames.CSharp] = {
                        Sources = {
                            LogResources.Attributes,
                            LogResources.LogLevel,
                            LogResources.LogType,
                            LogResources.ExternalInit,
                        }
                    }
                },
                Sources = {
                    testString,
                },
                GeneratedSources = {
                    ("WPILib.CodeHelpers\\WPILib.CodeHelpers.LogGenerator.SourceGenerator.LogGeneratorVb\\MyNewClass.g.vb", SourceText.From(expected, Encoding.UTF8))
                },
            },
        }.RunAsync();
    }
}
