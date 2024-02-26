namespace CodeHelpers.Test.LogGenerator;

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Microsoft.CodeAnalysis.Text;
using WPILib.CodeHelpers.LogGenerator.SourceGenerator;

public class LogGeneratorTest
{
    [Theory]
    [InlineData("bool", "LogBoolean")]
    [InlineData("int", "LogInteger")]
    [InlineData("long", "LogInteger")]
    public async Task TestPrimitives(string type, string output)
    {
        string testString = @"
using Stereologue;

[GenerateLog]
public partial class MyNewClass
{
    [Log]
    public REPLACEME Variable() { return default; }
}
";

        string expected = @"partial class MyNewClass
    : global::Stereologue.ILogged
{
    public void UpdateStereologue(string path, global::Stereologue.Stereologuer logger)
    {
        logger.REPLACEME($""{path}/Variable"", global::Stereologue.LogType.File | global::Stereologue.LogType.Nt, Variable(), global::Stereologue.LogLevel.Default);
    }
}
";
        testString = testString.Replace("\r\n", "\n").Replace("\n", "\r\n");
        testString = testString.Replace("REPLACEME", type);

        expected = expected.Replace("\r\n", "\n").Replace("\n", "\r\n");
        expected = expected.Replace("REPLACEME", output);

        await new CSharpSourceGeneratorTest<LogGeneratorSharp, XUnitVerifier>()
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
                    ("WPILib.CodeHelpers\\WPILib.CodeHelpers.LogGenerator.SourceGenerator.LogGeneratorSharp\\MyNewClass.g.cs", SourceText.From(expected, Encoding.UTF8))
                },
            },
        }.RunAsync();
    }
}
