namespace CodeHelpers.Test.LogGenerator;

using System.Text;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using Stereologue;
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
        testString = testString.Replace("REPLACEME", type);

        // Due to StringBuilder in the source generator,
        // We must normalize the output line endings
        expected = expected.NormalizeLineEndings();
        expected = expected.Replace("REPLACEME", output);

        await new CSharpSourceGeneratorTest<LogGeneratorSharp, DefaultVerifier>()
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
                    ($"WPILib.CodeHelpers{Path.DirectorySeparatorChar}WPILib.CodeHelpers.LogGenerator.SourceGenerator.LogGeneratorSharp{Path.DirectorySeparatorChar}MyNewClass.g.cs", SourceText.From(expected, Encoding.UTF8))
                },
            },
        }.RunAsync();
    }
}
