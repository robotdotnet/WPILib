nuget install EWSoftware.SHFB -Version 2015.10.10.0 -o sbpackages
nuget install EWSoftware.SHFB.NETFramework -Version 4.6 -o sbpackages

"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild" Sandcastle\SandcastleLocalBuildsOnly.shfbproj /property:Configuration=Release