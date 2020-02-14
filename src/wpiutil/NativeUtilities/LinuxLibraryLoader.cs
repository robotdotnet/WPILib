using System;
using System.IO;
using System.Runtime.InteropServices;
using WPIUtil.ILGeneration;

namespace WPIUtil.NativeUtilities
{
    /// <summary>
    /// This class handles native libraries on Linux
    /// </summary>
#pragma warning disable CA1060 // Move pinvokes to native methods class
    public sealed class LinuxLibraryLoader : ILibraryLoader
#pragma warning restore CA1060 // Move pinvokes to native methods class
    {
        /// <inheritdoc/>
        public IntPtr NativeLibraryHandle { get; private set; } = IntPtr.Zero;

        /// <inheritdoc/>
        void ILibraryLoader.LoadLibrary(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("The file requested to be loaded could not be found");
            IntPtr dl = dlopen(filename, 2);
            if (dl != IntPtr.Zero)
            {
                NativeLibraryHandle = dl;
                return;
            }
            IntPtr err = dlerror();
            if (err != IntPtr.Zero)
            {
                throw new DllNotFoundException($"Library Could not be opened: {Marshal.PtrToStringAnsi(err)}");
            }
        }

        /// <summary>
        /// Try to load a native library from a path
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool TryLoadLibrary(string filename)
        {
            IntPtr dl = dlopen(filename, 2);
            if (dl != IntPtr.Zero)
            {
                NativeLibraryHandle = dl;
                return true;
            };
            return false;
        }

        /// <inheritdoc/>
        IntPtr IFunctionPointerLoader.GetProcAddress(string name)
        {
            dlerror();
            IntPtr result = dlsym(NativeLibraryHandle, name);
            IntPtr err = dlerror();
            if (err != IntPtr.Zero)
            {
                throw new TypeLoadException($"Method not found: {Marshal.PtrToStringAnsi(err)}");
            }
            return result;
        }

        /// <inheritdoc/>
        void ILibraryLoader.UnloadLibrary()
        {
            _ = dlclose(NativeLibraryHandle);
        }

#pragma warning disable CA2101 // Specify marshaling for P/Invoke string arguments
        [DllImport("libdl.so.2")]

#pragma warning disable IDE1006 // Naming Styles
        private static extern IntPtr dlopen(string fileName, int flags);

        [DllImport("libdl.so.2")]
#pragma warning restore CA2101 // Specify marshaling for P/Invoke string arguments
        private static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport("libdl.so.2")]
        private static extern IntPtr dlerror();

        [DllImport("libdl.so.2")]
        private static extern int dlclose(IntPtr handle);
#pragma warning restore IDE1006 // Naming Styles
    }
}
