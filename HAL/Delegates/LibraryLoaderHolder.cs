using System;
using System.IO;
using System.Reflection;
using NativeLibraryUtilities;

namespace HAL.Base
{
    internal class LibraryLoaderHolder
    {
        private static readonly bool s_libraryLoaded;
        private static readonly NativeLibraryLoader s_nativeLoader;
        private static readonly string s_libraryLocation;
        private static readonly bool s_useCommandLineFile;
        private static readonly bool s_runFinalizer;
        private static readonly object s_lockObject = new object();

        private LibraryLoaderHolder()
        {
        }

        private void Ping()
        {
        }

        private static readonly LibraryLoaderHolder finalizeLibraryLoader = new LibraryLoaderHolder();

        ~LibraryLoaderHolder()
        {
            // If we did not successfully get constructed, we don't need to destruct
            if (!s_runFinalizer) return;

            s_nativeLoader.LibraryLoader.UnloadLibrary();

            try
            {
                //Don't delete file if we are using a specified file.
                if (!s_useCommandLineFile && File.Exists(s_libraryLocation))
                {
                    File.Delete(s_libraryLocation);
                }
            }
            catch
            {
                //Any errors just ignore.
            }
        }

        static LibraryLoaderHolder()
        {
            if (!s_libraryLoaded)
            {
                try
                {
                    finalizeLibraryLoader.Ping();
                    string[] commandArgs = Environment.GetCommandLineArgs();
                    foreach (var commandArg in commandArgs)
                    {
                        //search for a line with the prefix "-wpilib:"
                        if (commandArg.ToLower().Contains("-wpilib:"))
                        {
                            //Split line to get the library.
                            int splitLoc = commandArg.IndexOf(':');
                            string file = commandArg.Substring(splitLoc + 1);

                            //If the file exists, just return it so dlopen can load it.
                            if (File.Exists(file))
                            {
                                s_libraryLocation = file;
                                s_useCommandLineFile = true;
                            }
                        }
                    }

                    const string resourceRoot = "FRC.HAL.DesktopLibraries.Libraries.";

#if FALSE
                    s_useCommandLineFile = true;
                    s_libraryLocation = IntPtr.Size == 8 ?
                        @"C:\Users\thadh\Documents\GitHub\ThadHouse\MockHalTesting\native\build\binaries\mockhalSharedLibrary\x64\mockhal.dll" :
                        @"C:\Users\thadh\Documents\GitHub\ThadHouse\MockHalTesting\native\build\binaries\mockhalSharedLibrary\x86\mockhal.dll";
#endif

                    
                    if (File.Exists("/usr/local/frc/bin/frcRunRobot.sh"))
                    {

                        s_nativeLoader = new NativeLibraryLoader();
                        if (s_useCommandLineFile)
                        {
                            Console.WriteLine(s_libraryLocation);
                            s_nativeLoader.LoadNativeLibrary<NativeLibraryLoader>(new RoboRioLibraryLoader(), s_libraryLocation, true);
                        }
                        else
                        {
                            // RoboRIO
                            s_nativeLoader.LoadNativeLibrary<NativeLibraryLoader>(new RoboRioLibraryLoader(), "libHALAthena.so", true);
                            s_libraryLocation = s_nativeLoader.LibraryLocation;
                        }
                    }
                    else
                    {
                        s_nativeLoader = new NativeLibraryLoader();
                        s_nativeLoader.AddLibraryLocation(OsType.Windows32,
                            resourceRoot + "x86.mockhal.dll");
                        s_nativeLoader.AddLibraryLocation(OsType.Windows64,
                            resourceRoot + "amd64.mockhal.dll");
                        /*
                    NativeLoader.AddLibraryLocation(OsType.Linux32,
                        resourceRoot + "x86.libcscore.so");
                    NativeLoader.AddLibraryLocation(OsType.Linux64,
                        resourceRoot + "amd64.libcscore.so");
                    NativeLoader.AddLibraryLocation(OsType.MacOs32,
                        resourceRoot + "x86.libcscore.dylib");
                    NativeLoader.AddLibraryLocation(OsType.MacOs64,
                        resourceRoot + "amd64.libcscore.dylib");
                        */

                        if (s_useCommandLineFile)
                        {
                            s_nativeLoader.LoadNativeLibrary<LibraryLoaderHolder>(s_libraryLocation, true);
                        }
                        else
                        {
                            // Load Reflection type from Native Libraries
                            AssemblyName name = new AssemblyName("FRC.HAL.DesktopLibraries");
                            Assembly asm;
                            try
                            {
                                asm = Assembly.Load(name);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Failed to load desktop libraries. Please ensure that the FRC.HAL.DesktopLibraries is installed and referenced by your project");
                                throw;
                            }

                            Type type = asm.GetType("FRC.HAL.DesktopLibraries.Natives");

                            s_nativeLoader.LoadNativeLibraryFromReflectedAssembly(type);
                            s_libraryLocation = s_nativeLoader.LibraryLocation;
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    Environment.Exit(1);
                }
                s_runFinalizer = true;
                s_libraryLoaded = true;
            }
        }

        internal static NativeLibraryLoader NativeLoader
        {
            get
            {
                lock (s_lockObject)
                {
                    return s_nativeLoader;
                }
            }
        }
    }
}
