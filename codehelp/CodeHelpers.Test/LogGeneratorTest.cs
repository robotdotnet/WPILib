namespace CodeHelpers.Test;

using System.Text;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.CSharp.Testing.XUnit;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Microsoft.CodeAnalysis.Text;
using WPILib.CodeHelpers.LogGenerator.SourceGenerator;

public class LogGeneratorTest
{
    const string InternalTypes = @"
namespace Stereologue
{
[System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Method | System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class LogAttribute : System.Attribute
{
}

[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
public sealed class GenerateLogAttribute : System.Attribute
{
}
}
";

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

        string expected = @"partial class MyNewClass : global::Stereologue.ILogged
{
    public void UpdateStereologue(string path, global::Stereologue.Stereologuer logger)
    {
        logger.REPLACEME($""{path}/Variable"", Stereologue.LogType.Nt | Stereologue.LogType.File, Variable(), Stereologue.LogLevel.Default);
    }
}
";
        testString += InternalTypes;
        testString = testString.Replace("\r\n", "\n").Replace("\n", "\r\n");
        testString = testString.Replace("REPLACEME", type);

        expected = expected.Replace("\r\n", "\n").Replace("\n", "\r\n");
        expected = expected.Replace("REPLACEME", output);

        await new CSharpSourceGeneratorTest<LogGeneratorSharp, XUnitVerifier>()
        {
            CompilerDiagnostics = CompilerDiagnostics.None,
            TestState = {
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
