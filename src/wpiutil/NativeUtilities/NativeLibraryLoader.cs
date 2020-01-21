using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WPIUtil.ILGeneration;

namespace WPIUtil.NativeUtilities
{
    public static class NativeLibraryLoader
    {

        private class NetCoreFunctionPointerLoader : IFunctionPointerLoader
        {
            private readonly IntPtr libHandle;

            public NetCoreFunctionPointerLoader(IntPtr handle)
            {
                libHandle = handle;
            }

            public IntPtr GetProcAddress(string name)
            {
#if NETSTANDARD
                return IntPtr.Zero;
#else
                return NativeLibrary.GetExport(libHandle, name);
#endif
            }
        }

        public static InterfaceGenerator? LoadNativeLibraryGenerator(string libraryName)
        {
            var fpLoader = LoadNativeLibrary(libraryName);
            if (fpLoader == null)
            {
                return null;
            }

            return new InterfaceGenerator(fpLoader, new CalliILGenerator());
        }

        public static IFunctionPointerLoader? LoadNativeLibrary(string libraryName)
        {
            string fullLibraryName;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                fullLibraryName = $"{libraryName}.dll";
            } 
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                fullLibraryName = $"lib{libraryName}.dylib";
            }
            else
            {
                fullLibraryName = $"lib{libraryName}.so";
            }
#if NETSTANDARD
#else
            if (NativeLibrary.TryLoad(fullLibraryName, out var handle))
            {
                return new NetCoreFunctionPointerLoader(handle);
            }
#endif
            return null;
        }
    }
}
