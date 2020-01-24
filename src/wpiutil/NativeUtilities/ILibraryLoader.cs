using System;
using WPIUtil.ILGeneration;

namespace WPIUtil.NativeUtilities
{
    /// <summary>
    /// Interface for platform specific native interface to the library
    /// </summary>
    public interface ILibraryLoader : IFunctionPointerLoader
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
        /// Tires to load library from specified file name
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool TryLoadLibrary(string filename);

        /// <summary>
        /// Unloads the native library
        /// </summary>
        void UnloadLibrary();
    }
}
