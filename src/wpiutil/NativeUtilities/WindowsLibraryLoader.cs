using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using WPIUtil.ILGeneration;

namespace WPIUtil.NativeUtilities
{
    /// <summary>
    /// This class handles native libraries on Windows
    /// </summary>
    public sealed class WindowsLibraryLoader : ILibraryLoader
    {
        /// <inheritdoc/>
        public IntPtr NativeLibraryHandle { get; private set; } = IntPtr.Zero;

        void ILibraryLoader.LoadLibrary(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("The file requested to be loaded could not be found");
            IntPtr dl = LoadLibrary(filename);
            if (dl != IntPtr.Zero)
            {
                NativeLibraryHandle = dl;
                return;
            }
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Try to load a native library from a path
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool TryLoadLibrary(string filename)
        {
            IntPtr dl = LoadLibrary(filename);
            if (dl != IntPtr.Zero)
            {
                NativeLibraryHandle = dl;
                return true;
            };
            return false;
        }

        IntPtr IFunctionPointerLoader.GetProcAddress(string name)
        {
            IntPtr addr = GetProcAddress(NativeLibraryHandle, name);
            if (addr == IntPtr.Zero)
            {
                //Address not found. Throw Exception
                throw new Exception($"Method not found: {name}");
            }
            return addr;
        }

        void ILibraryLoader.UnloadLibrary()
        {
            FreeLibrary(NativeLibraryHandle);
        }

#pragma warning disable CA2101 // Specify marshaling for P/Invoke string arguments
        [DllImport("kernel32", SetLastError = true)]
#pragma warning restore CA2101 // Specify marshaling for P/Invoke string arguments
        private static extern IntPtr LoadLibrary(string fileName);

#pragma warning disable CA2101 // Specify marshaling for P/Invoke string arguments
        [DllImport("kernel32.dll")]
#pragma warning restore CA2101 // Specify marshaling for P/Invoke string arguments
        private static extern IntPtr GetProcAddress(IntPtr handle, string procedureName);

        [DllImport("kernel32")]
        private static extern bool FreeLibrary(IntPtr handle);
    }

}
