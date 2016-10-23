using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace HAL.NativeLoader
{
    /// <summary>
    /// This class handles loading of a native library
    /// </summary>
    public class NativeLibraryLoader : ILibraryInformation
    {
        private readonly Dictionary<OsType, string> m_nativeLibraryName = new Dictionary<OsType, string>();

        /// <inheritdoc/>
        public ILibraryLoader LibraryLoader { get; private set; }
        /// <inheritdoc/>
        public OsType OsType { get; } = GetOsType();
        /// <inheritdoc/>
        public bool UsingTempFile { get; private set; }

        /// <inheritdoc/>
        public string LibraryLocation { get; private set; }

        /// <summary>
        /// Add a file location to be used when automatically searching for a library to load
        /// </summary>
        /// <param name="osType">The OsType to associate with the file</param>
        /// <param name="libraryName">The file to load on that OS</param>
        public void AddLibraryLocation(OsType osType, string libraryName)
        {
            m_nativeLibraryName.Add(osType, libraryName);
        }

        /// <summary>
        /// Loads a native library using the specified loader and file
        /// </summary>
        /// <typeparam name="T">The type containing the native resource, if it is embedded.</typeparam>
        /// <param name="loader">The LibraryLoader to use</param>
        /// <param name="location">The file location. Can be either an embedded resource, or a direct file location</param>
        /// <param name="directLoad">True to load the file directly from disk, otherwise false to extract from embedded</param>
        /// <param name="extractLocation">The location to extract to if the file is embedded. On null, it extracts to a temp file</param>
        public void LoadNativeLibrary<T>(ILibraryLoader loader, string location, bool directLoad = false, string extractLocation = null)
        {
            if (loader == null)
                throw new ArgumentNullException(nameof(loader), "Library loader cannot be null");
            if (location == null)
                throw new ArgumentNullException(nameof(location), "Library location cannot be null");

            // Set to use temp file if extractLocation is null
            if (string.IsNullOrWhiteSpace(extractLocation) && !directLoad)
            {
                extractLocation = Path.GetTempFileName();
                UsingTempFile = true;
            }

            // If we are loading from extraction, extract then load
            if (!directLoad)
            {
                ExtractNativeLibrary<T>(location, extractLocation);
                LibraryLoader = loader;
                loader.LoadLibrary(extractLocation);
                LibraryLocation = extractLocation;
            }
            else
            {
                // Otherwise directly load.
                LibraryLoader = loader;
                loader.LoadLibrary(location);
                LibraryLocation = location;
            }
        }

        /// <summary>
        /// Loads a native library using the specified file. The OS is determined automatically
        /// </summary>
        /// <typeparam name="T">The type containing the native resource, if it is embedded.</typeparam>
        /// <param name="location">The file location. Can be either an embedded resource, or a direct file location</param>
        /// <param name="directLoad">True to load the file directly from disk, otherwise false to extract from embedded</param>
        /// <param name="extractLocation">The location to extract to if the file is embedded. On null, it extracts to a temp file</param>
        public void LoadNativeLibrary<T>(string location, bool directLoad = false, string extractLocation = null)
        {
            if (location == null)
                throw new ArgumentNullException(nameof(location), "Library location cannot be null");

            if (OsType == OsType.None)
                throw new InvalidOperationException(
                    "OS type is unknown. Must use the overload to manually load the file");

            if (!m_nativeLibraryName.ContainsKey(OsType) && !directLoad)
                throw new InvalidOperationException("OS Type not contained in dictionary");

            switch (OsType)
            {
                case OsType.Windows32:
                case OsType.Windows64:
                    LibraryLoader = new WindowsLibraryLoader();
                    break;
                case OsType.Linux32:
                case OsType.Linux64:
                    LibraryLoader = new LinuxLibraryLoader();
                    break;
                case OsType.MacOs32:
                case OsType.MacOs64:
                    LibraryLoader = new LinuxLibraryLoader();
                    break;
            }

            LoadNativeLibrary<T>(LibraryLoader, location, directLoad, extractLocation);
        }

        /// <summary>
        /// Loads a native library, using locations added using <see cref="AddLibraryLocation"/>
        /// </summary>
        /// <typeparam name="T">The type containing the native resource, if it is embedded.</typeparam>
        /// <param name="directLoad">True to load the file directly from disk, otherwise false to extract from embedded</param>
        /// <param name="extractLocation">The location to extract to if the file is embedded. On null, it extracts to a temp file</param>
        public void LoadNativeLibrary<T>(bool directLoad = false, string extractLocation = null)
        {
            if (OsType == OsType.None)
                throw new InvalidOperationException(
                    "OS type is unknown. Must use the overload to manually load the file");

            if (!m_nativeLibraryName.ContainsKey(OsType) && !directLoad)
                throw new InvalidOperationException("OS Type not contained in dictionary");

            switch (OsType)
            {
                case OsType.Windows32:
                case OsType.Windows64:
                    LibraryLoader = new WindowsLibraryLoader();
                    break;
                case OsType.Linux32:
                case OsType.Linux64:
                    LibraryLoader = new LinuxLibraryLoader();
                    break;
                case OsType.MacOs32:
                case OsType.MacOs64:
                    LibraryLoader = new LinuxLibraryLoader();
                    break;
            }

            LoadNativeLibrary<T>(LibraryLoader, m_nativeLibraryName[OsType], directLoad, extractLocation);

        }

        private void ExtractNativeLibrary<T>(string resourceLocation, string extractLocation)
        {
            byte[] bytes;
            //Load our resource file into memory
            using (Stream s = typeof(T).GetTypeInfo().Assembly.GetManifestResourceStream(resourceLocation))
            {
                if (s == null || s.Length == 0)
                    throw new InvalidOperationException("File to extract cannot be null or empty");
                bytes = new byte[(int)s.Length];
                s.Read(bytes, 0, (int)s.Length);
            }
            File.WriteAllBytes(extractLocation, bytes);
            GC.Collect();
        }

        private static bool Is64BitOs()
        {
            return IntPtr.Size != sizeof(int);
        }

        private static bool IsWindows()
        {
#if NETSTANDARD
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#else
            return Path.DirectorySeparatorChar == '\\';
#endif
        }

        /// <summary>
        /// Gets the OS Type of the current running system.
        /// </summary>
        /// <returns></returns>
        public static OsType GetOsType()
        {
            if (IsWindows())
            {
                return Is64BitOs() ? OsType.Windows64 : OsType.Windows32;
            }
            else
            {
#if NETSTANDARD
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    if (Is64BitOs()) return OsType.Linux64;
                    else return OsType.Linux32;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    if (Is64BitOs()) return OsType.MacOs64;
                    else return OsType.MacOs32;
                }
                else
                {
                    return OsType.None;
                }
#else
                Utsname uname;
                try
                {
                    Uname.uname(out uname);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return OsType.None;
                }


                Console.WriteLine(uname.ToString());

                bool mac = uname.sysname == "Darwin";

                //Check for Bitness
                if (Is64BitOs())
                {
                    //We are 64 bit.
                    if (mac) return OsType.MacOs64;
                    return OsType.Linux64;
                }
                else
                {
                    //We are 64 32 bit process.
                    if (mac) return OsType.MacOs32;
                    return OsType.Linux32;
                }
#endif
            }
        }
    }
}
