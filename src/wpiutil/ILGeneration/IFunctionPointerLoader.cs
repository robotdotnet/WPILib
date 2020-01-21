using System;

namespace WPIUtil.ILGeneration {
    /// <summary>
    /// Interface for loading a function pointer
    /// </summary>
    public interface IFunctionPointerLoader {
        /// <summary>
        /// Get a function pointer for a function name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IntPtr GetProcAddress(string name);
    }
}
