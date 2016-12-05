param (
  [switch]$build = $false,
  [switch]$test = $false,
  [switch]$pack = $false
)

If (Test-Path Env:APPVEYOR_REPO_TAG_NAME) {
  $version = ($env:APPVEYOR_REPO_TAG_NAME).Substring(1)  
  if (($env:APPVEYOR_REPO_TAG_NAME).Contains("-") -eq $false) {
    #Building a Full Release
     $type = ""
     $buildNumber = ""
     echo "Tagged Release"
    } Else {
      #Building a Beta
      $version = ($version).Substring(0, (($env:APPVEYOR_REPO_TAG_NAME).IndexOf("-") - 1))
      $type = ($env:APPVEYOR_REPO_TAG_NAME).Substring((($env:APPVEYOR_REPO_TAG_NAME).IndexOf("-")))
      $buildNumber = ""
      echo "Tag but not release"
    }
} Else {
  $version = "2017.0.0"
  $type = "-ci-"
  $buildNumber = @{ $true = $env:APPVEYOR_BUILD_NUMBER; $false = 1 }[$env:APPVEYOR_BUILD_NUMBER -ne $NULL];
  $buildNumber = "{0:D4}" -f [convert]::ToInt32($buildNumber, 10), $buildNumber
}


echo $version
echo $type
echo $buildNumber


if ($build) {
  ./NuGet restore
  ./gradlew build -PbuildVersion="$version" -PbuildType="$type" -PbuildNumber="$buildNumber"
}

function Exec  
{
    [CmdletBinding()]
    param(
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)
    )
    & $cmd
    if ($lastexitcode -ne 0) {
        throw ("Exec: " + $errorMessage)
    }
}

if ($test) {
  $OpenCoverVersion = "4.6.519"
    
  $openCoverRun = ".\buildTemp\OpenCover.$OpenCoverVersion\tools\OpenCover.Console.exe"
  
  
  # install CodeCov
  .\NuGet.exe install OpenCover -Version $OpenCoverVersion -OutputDirectory buildTemp
  
  exec { & $openCoverRun -register:user -target:nunit3-console.exe -targetargs:".\WPILib.Tests\Output\WPILib.Tests.dll --framework=net-4.5 " -filter:"+[WPILib*]* -[HAL*]* -[WPILib.T*]* -[WPILib.IntegrationT*]* -[NIVision*]*" -output:coverage.xml -mergeoutput -returntargetcode }
  exec { & $openCoverRun -register:user -target:nunit3-console.exe -targetargs:".\WPILib.IntegrationTests\Output\WPILib.IntegrationTests.dll --framework=net-4.5 " -filter:"+[WPILib*]* -[HAL*]* -[WPILib.T*]* -[WPILib.IntegrationT*]* -[NIVision*]*" -output:coverageIntegration.xml }
  
  $env:Path = "C:\Python34;C:\\Python34\Scripts;" + $env:Path
  
  & pip install codecov
  
  & codecov -f "coverage.xml"
}

if ($pack) {
  If (Test-Path Env:APPVEYOR_BUILD_FOLDER) {
    $startPath = $env:APPVEYOR_BUILD_FOLDER
  } Else {
    $startPath = "."
  }

  ./nuget pack "$startPath\WPILib\WPILib.csproj" -Properties "Configuration=Release;Platform=AnyCPU" -Symbols -IncludeReferencedProjects -Version "$version$type$buildNumber"

  ./nuget pack "$startPath\WPILib.Extras\WPILib.Extras.csproj" -Properties "Configuration=Release;Platform=AnyCPU" -Symbols -IncludeReferencedProjects -Version "$version$type$buildNumber"

  ./nuget pack "$startPath\FRC.HAL.DesktopLibraries\FRC.HAL.DesktopLibraries.csproj" -Properties "Configuration=Release;Platform=AnyCPU" -Symbols -IncludeReferencedProjects -Version "$version$type$buildNumber"

  If (Test-Path Env:APPVEYOR_BUILD_FOLDER) {
    Get-ChildItem .\*.nupkg | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }
  }
}
