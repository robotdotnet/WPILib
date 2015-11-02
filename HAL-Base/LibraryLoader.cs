using System;
using System.Runtime.InteropServices;

namespace HAL_Base
{
    public interface ILibraryLoader
    {
        IntPtr LoadLibrary(string filename);
        IntPtr GetProcAddress(IntPtr dllHandle, string name);
    }

    public class WindowsLibraryLoader : ILibraryLoader
    {
        IntPtr ILibraryLoader.LoadLibrary(string filename)
        {
            return LoadLibrary(filename);
        }

        IntPtr ILibraryLoader.GetProcAddress(IntPtr dllHandle, string name)
        {
            IntPtr addr = GetProcAddress(dllHandle, name);
            if (addr == IntPtr.Zero)
            {
                //Address not found. Throw Exception
                throw new EntryPointNotFoundException($"Method not found: {name}");
            }
            return addr;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string fileName);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr handle, string procedureName);
    }

    public class LinuxLibraryLoader : ILibraryLoader
    {
        IntPtr ILibraryLoader.LoadLibrary(string filename)
        {
            return dlopen(filename, 2);
        }

        IntPtr ILibraryLoader.GetProcAddress(IntPtr dllHandle, string name)
        {
            dlerror();
            IntPtr result = dlsym(dllHandle, name);
            IntPtr err = dlerror();
            if (err != IntPtr.Zero)
            {
                throw new EntryPointNotFoundException($"Method not found: {Marshal.PtrToStringAnsi(err)}");
            }
            return result;
        }

        [DllImport("libdl.so")]
        private static extern IntPtr dlopen(string fileName, int flags);

        [DllImport("libdl.so")]
        private static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport("libdl.so")]
        private static extern IntPtr dlerror();
    }

    public class RoboRioLibraryLoader : ILibraryLoader
    {
        IntPtr ILibraryLoader.LoadLibrary(string filename)
        {
            return dlopen(filename, 2);
        }

        IntPtr ILibraryLoader.GetProcAddress(IntPtr dllHandle, string name)
        {
            dlerror();
            IntPtr result = dlsym(dllHandle, name);
            IntPtr err = dlerror();
            if (err != IntPtr.Zero)
            {
                throw new EntryPointNotFoundException($"Method not found: {Marshal.PtrToStringAnsi(err)}");
            }
            return result;
        }

        [DllImport("libdl-2.17.so")]
        private static extern IntPtr dlopen(string fileName, int flags);

        [DllImport("libdl-2.17.so")]
        private static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport("libdl-2.17.so")]
        private static extern IntPtr dlerror();
    }
}
