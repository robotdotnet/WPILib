using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace HAL.Base
{
    public interface ILibraryLoader
    {
        IntPtr LoadLibrary(string filename);
        IntPtr GetProcAddress(IntPtr dllHandle, string name);
    }

    [ExcludeFromCodeCoverage]
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

        [DllImport("kernel32")]
        private static extern IntPtr LoadLibrary(string fileName);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr handle, string procedureName);
    }

    [ExcludeFromCodeCoverage]
    public class LinuxLibraryLoader : ILibraryLoader
    {
        IntPtr ILibraryLoader.LoadLibrary(string filename)
        {
            IntPtr dl = dlopen(filename, 2);
            if (dl != IntPtr.Zero) return dl;
            IntPtr err = dlerror();
            if (err != IntPtr.Zero)
            {
                throw new DllNotFoundException($"Library Could not be opened: {Marshal.PtrToStringAnsi(err)}");
            }
            return dl;
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

    [ExcludeFromCodeCoverage]
    public class RoboRioLibraryLoader : ILibraryLoader
    {
        IntPtr ILibraryLoader.LoadLibrary(string filename)
        {
            IntPtr dl = dlopen(filename, 2);
            if (dl != IntPtr.Zero) return dl;
            IntPtr err = dlerror();
            if (err != IntPtr.Zero)
            {
                throw new DllNotFoundException($"Library Could not be opened: {Marshal.PtrToStringAnsi(err)}");
            }
            return dl;
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

        [DllImport("libdl-2.20.so")]
        private static extern IntPtr dlopen(string fileName, int flags);

        [DllImport("libdl-2.20.so")]
        private static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport("libdl-2.20.so")]
        private static extern IntPtr dlerror();
    }
}
