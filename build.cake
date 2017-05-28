// Target - The task you want to start. Runs the Default task if not specified.
var target = Argument("Target", "Default");
// Configuration - The build configuration (Debug/Release) to use.
// 1. If command line parameter parameter passed, use that.
// 2. Otherwise if an Environment variable exists, use that.
var configuration = 
    HasArgument("Configuration") ? Argument<string>("Configuration") :
    EnvironmentVariable("Configuration") != null ? EnvironmentVariable("BuildNumber") : "Release";
// The build number to use in the version number of the built NuGet packages.
// There are multiple ways this value can be passed, this is a common pattern.
// 1. If command line parameter parameter passed, use that.
// 2. Otherwise if running on AppVeyor, get it's build number.
// 3. Otherwise if running on Travis CI, get it's build number.
// 4. Otherwise if an Environment variable exists, use that.
// 5. Otherwise default the build number to 0.
var buildNumberInt =
    HasArgument("BuildNumber") ? Argument<int>("BuildNumber") :
    AppVeyor.IsRunningOnAppVeyor ? AppVeyor.Environment.Build.Number :
    TravisCI.IsRunningOnTravisCI ? TravisCI.Environment.Build.BuildNumber :
    EnvironmentVariable("BuildNumber") != null ? int.Parse(EnvironmentVariable("BuildNumber")) : 0;

var buildNumber = buildNumberInt.ToString("D4");

var buildVersion = "1.0.0";

var buildType = (AppVeyor.IsRunningOnAppVeyor || TravisCI.IsRunningOnTravisCI) ? "ci-"  : "local-";

buildType = buildType + buildNumber;

var tagName = EnvironmentVariable("APPVEYOR_REPO_TAG_NAME");
if (tagName != null) {
    // On AppVeyor
    buildVersion =  tagName.Substring(1);
    if (!tagName.Contains("-")) {
        // Building a full release
        buildType = "";
    } else {
        buildVersion = buildVersion.Substring(0, (tagName.IndexOf("-") - 1));
        buildType = tagName.Substring(tagName.IndexOf("-") + 1);
    }
}

string msBuildVersionArgs = "/p:VersionPrefix=\"" + buildVersion + "\" /p:VersionSuffix=\"" + buildType + "\"";

Console.WriteLine("BuildNumber: " + msBuildVersionArgs);

// A directory path to an Artifacts directory.
var artifactsDirectory = Directory("./Artifacts");

var current = MakeAbsolute(Directory(".")).ToString();
Console.WriteLine(current);
 
// Deletes the contents of the Artifacts folder if it should contain anything from a previous build.
Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactsDirectory);
    });
// Run dotnet restore to restore all package references.
Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore();
    });
 
// Find all crproj projects and build them using the build configuration specified as an argument.
 Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        var projects = GetFiles("./**/*.csproj");
        foreach(var project in projects)
        {
            DotNetCoreBuild(
                project.GetDirectory().FullPath,
                new DotNetCoreBuildSettings()
                {
                    Configuration = configuration,
                    ArgumentCustomization = args => args
                        .Append(msBuildVersionArgs)
                });
        }
    });
 
// Look under a 'Tests' folder and run dotnet test against all of those projects.
// Then drop the XML test results file in the Artifacts folder at the root.
Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var projects = GetFiles("./test/**/*.csproj");
        foreach(var project in projects)
        {
            Console.WriteLine(MakeAbsolute(artifactsDirectory).ToString());
            Console.WriteLine(MakeAbsolute(artifactsDirectory).CombineWithFilePath(project.GetFilenameWithoutExtension()).FullPath + ".trx");
            DotNetCoreTest(
                project.ToString(),
                new DotNetCoreTestSettings()
                {
                    Configuration = configuration,
                    NoBuild = true,
                    /*
                    ArgumentCustomization = args => args
                        .Append("--logger trx;LogFileName="+ MakeAbsolute(artifactsDirectory).CombineWithFilePath(project.GetFilenameWithoutExtension()).FullPath + ".trx\"\"")*/
                });
        }
    });
 
// Run dotnet pack to produce NuGet packages from our projects. Versions the package
// using the build number argument on the script which is used as the revision number 
// (Last number in 1.0.0.0). The packages are dropped in the Artifacts directory.
Task("Pack")
    .IsDependentOn("Test")
    .Does(() =>
    {
        foreach (var project in GetFiles("./src/**/*.csproj"))
        {
            var sources = "";
            if (!project.FullPath.Contains("DesktopLibraries")) {
                sources = "--include-source";
            }
            DotNetCorePack(
                project.GetDirectory().FullPath,
                new DotNetCorePackSettings()
                {
                    Configuration = configuration,
                    OutputDirectory = artifactsDirectory,
                    ArgumentCustomization = args => args
                        .Append(msBuildVersionArgs)
                        .Append(sources)
                });
        }
    });
 
// The default task to run if none is explicitly specified. In this case, we want
// to run everything starting from Clean, all the way up to Pack.
Task("Default")
    .IsDependentOn("Pack");
 
// Executes the task specified in the target argument.
RunTarget(target);