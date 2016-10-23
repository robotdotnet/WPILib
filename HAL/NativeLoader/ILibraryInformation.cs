namespace HAL.NativeLoader
{
    /// <summary>
    /// Interface for getting information about a loaded native library.
    /// </summary>
    public interface ILibraryInformation
    {
        /// <summary>
        /// The LibraryLoader used to load this library
        /// </summary>
        ILibraryLoader LibraryLoader { get; }
        /// <summary>
        /// The location on disk of the native library
        /// </summary>
        string LibraryLocation { get; }
        /// <summary>
        /// The OS Type of the loaded system.
        /// </summary>
        OsType OsType { get; }
        /// <summary>
        /// Gets if the native library was extracted to the temp directory
        /// </summary>
        bool UsingTempFile { get; }
    }
}
