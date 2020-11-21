using System.Runtime.InteropServices;
using WPIUtil.ILGeneration;

namespace WPIUtil.NativeUtilities
{
    public static class NativeLibraryLoader
    {
        public static IFunctionPointerLoader? LoadNativeLibrary(string libraryName)
        {
            string fullLibraryName;
            ILibraryLoader loader;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                fullLibraryName = $"{libraryName}.dll";
                loader = new WindowsLibraryLoader();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                fullLibraryName = $"lib{libraryName}.dylib";
                loader = new MacOsLibraryLoader();
            }
            else
            {
                fullLibraryName = $"lib{libraryName}.so";
                loader = new LinuxLibraryLoader();
            }

            return loader.TryLoadLibrary(fullLibraryName) ? loader : null;
        }
    }
}
