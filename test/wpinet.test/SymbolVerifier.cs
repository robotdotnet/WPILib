using System.Reflection;
using System.Runtime.InteropServices;
using Xunit;

namespace WPINet.Test;

public class SymbolVerifier
{
    [Fact]
    public void VerifySymbols()
    {
        Assembly assemblyType = typeof(ServiceData).Assembly;
        // Find all native methods
        var foundMethods =
            assemblyType.GetTypes()
                .SelectMany(x => x.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                .Where(x => x.Attributes.HasFlag(MethodAttributes.PinvokeImpl))
                .Select(x => (x, x.GetCustomAttribute<DllImportAttribute>()))
                .Where(x => x.Item2 != null)
                .Select(x => (Method: x.x, Dll: x.Item2!.Value, EntryPoint: x.Item2.EntryPoint ?? x.x.Name));

        Dictionary<string, nint> libraries = [];
        foreach (var method in foundMethods)
        {
            if (!libraries.ContainsKey(method.Dll))
            {
                libraries[method.Dll] = NativeLibrary.Load(method.Dll, assemblyType, DllImportSearchPath.UseDllDirectoryForDependencies);
            }

            var library = libraries[method.Dll];
            Assert.True(NativeLibrary.TryGetExport(library, method.EntryPoint, out _),
                $"Failed to find symbol {method.EntryPoint} in {method.Dll}");
        }
    }
}
