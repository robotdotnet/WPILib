param (
  [switch]$build = $false,
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

if ($pack) {
  If (Test-Path Env:APPVEYOR_BUILD_FOLDER) {
    $startPath = $env:APPVEYOR_BUILD_FOLDER
  } Else {
    $startPath = "."
  }

  ./nuget pack "$startPath\WPILib\WPILib.csproj" -Properties "Configuration=Release;Platform=AnyCPU" -Symbols -IncludeReferencedProjects -Version "$version$type$buildNumber"

  ./nuget pack "$startPath\WPILib.Extras\WPILib.Extras.csproj" -Properties "Configuration=Release;Platform=AnyCPU" -Symbols -IncludeReferencedProjects -Version "$version$type$buildNumber"

  If (Test-Path Env:APPVEYOR_BUILD_FOLDER) {
    Get-ChildItem .\*.nupkg | % { Push-AppveyorArtifact $_.FullName -FileName $_.Name }
  }
}
