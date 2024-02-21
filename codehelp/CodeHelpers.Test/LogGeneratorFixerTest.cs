using WPILib.CodeHelpers.LogGenerator.Analyzer;
using Verify = Microsoft.CodeAnalysis.CSharp.Testing.XUnit.CodeFixVerifier<WPILib.CodeHelpers.LogGenerator.Analyzer.LogGeneratorAnalyzer, WPILib.CodeHelpers.LogGenerator.CodeFixer.LogGeneratorFixer>;

namespace CodeHelpers.Test;

public class LogGeneratorFixerTest
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
        testString += InternalTypes;
        fixedCode += InternalTypes;
        var expected = Verify.Diagnostic(LoggerDiagnostics.MissingGenerateLog).WithLocation(4, 22).WithArguments(["MyNewClass"]);
        await Verify.VerifyCodeFixAsync(testString, expected, fixedCode);
    }
}
