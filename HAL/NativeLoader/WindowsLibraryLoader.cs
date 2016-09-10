using System;
using System.IO;
using System.Runtime.InteropServices;

namespace HAL.NativeLoader
{
    /// <summary>
    /// This class handles native libraries on Windows
    /// </summary>
    public class WindowsLibraryLoader : ILibraryLoader
    {
        /// <inheritdoc/>
        public IntPtr NativeLibraryHandle { get; private set; } = IntPtr.Zero;

        void ILibraryLoader.LoadLibrary(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("The file requested to be loaded could not be found");
            NativeLibraryHandle = LoadLibrary(filename);
        }

        IntPtr ILibraryLoader.GetProcAddress(string name)
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

        [DllImport("kernel32")]
        private static extern IntPtr LoadLibrary(string fileName);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr handle, string procedureName);

        [DllImport("kernel32")]
        private static extern bool FreeLibrary(IntPtr handle);
    }

}
