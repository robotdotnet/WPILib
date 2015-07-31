@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

REM Package restore

call %nuget% restore WPILib\packages.config -OutputDirectory %cd%\packages -NonInteractive

REM Build
%MsBuildExe% robotdotnet-wpilib.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
mkdir Build
cmd /c %nuget% pack "WPILib\WPILib.csproj" -IncludeReferencedProjects -o Build -p Configuration=%config% %version%

cmd /c %nuget% pack "WPILib.Extras\WPILib.Extras.csproj" -o Build -p Configuration=%config% %version%