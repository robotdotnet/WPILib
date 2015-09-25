using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Base
{
    public interface IDllLoader
    {
        IntPtr LoadLibrary(string filename);
        IntPtr GetProcAddress(IntPtr dllHandle, string name);
    }

    public class WindowsDllLoader : IDllLoader
    {
        IntPtr IDllLoader.LoadLibrary(string filename)
        {
            return LoadLibrary(filename);
        }

        IntPtr IDllLoader.GetProcAddress(IntPtr dllHandle, string name)
        {
            return GetProcAddress(dllHandle, name);
        }

        [DllImport("kernel32")]
        private static extern IntPtr LoadLibrary(string fileName);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr handle, string procedureName);
    }

    public class LinuxDllLoader : IDllLoader
    {
        IntPtr IDllLoader.LoadLibrary(string filename)
        {
            return dlopen(filename, 2);
        }

        IntPtr IDllLoader.GetProcAddress(IntPtr dllHandle, string name)
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
}
