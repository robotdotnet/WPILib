using System;

namespace HAL.NativeLoader
{
    /// <summary>
    /// Interface for platform specific native interface to the library
    /// </summary>
    public interface ILibraryLoader
    {
        /// <summary>
        /// Gets the native library handle for the library
        /// </summary>
        IntPtr NativeLibraryHandle { get; }
        /// <summary>
        /// Loads the library from the specified file name
        /// </summary>
        /// <param name="filename"></param>
        void LoadLibrary(string filename);
        /// <summary>
        /// Gets the address of a specific entry point in the native library
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IntPtr GetProcAddress(string name);

        /// <summary>
        /// Unloads the native library
        /// </summary>
        void UnloadLibrary();
    }
}
