using System;
using System.IO;
using System.Reflection;

namespace HAL.SimulatorHAL
{
    enum OsType
    {
        Windows32,
        Windows64,
        Linux32,
        Linux64,
        ArmLinux,
        RoboRio
    }

    internal static class LoaderUtilities
    {
        internal static OsType GetOsType()
        {
            var platform = (int) Environment.OSVersion.Platform;
            if (platform == 4 || platform == 6 || platform == 128)
            {
                //These 3 mean we are running on a unix based system
                if (Environment.Is64BitProcess)
                {
                    //We are 64 bit. RIO is not 64 bit, so we can force return.
                    return OsType.Linux64;
                }
                else
                {
                    //We need to check for the RIO
                    return File.Exists("/usr/local/frc/bin/frcRunRobot.sh") ? OsType.RoboRio : OsType.Linux32;
                }
            }
            else
            {
                //Assume we are on windows otherwise
                return Environment.Is64BitProcess ? OsType.Windows64 : OsType.Windows32;
            }
        }

        internal static bool CheckOsValid(OsType type)
        {
            switch (type)
            {
                case OsType.Windows32:
                    return true;
                case OsType.Windows64:
                    return false;
                case OsType.Linux32:
                    return true;
                case OsType.Linux64:
                    return false;
                case OsType.RoboRio:
                    return false;
                default:
                    return false;
            }
        }

        internal static string ExtractLibrary(OsType type)
        {
            string inputName = "";
            string outputName = "";
            switch (type)
            {
                case OsType.Windows32:
                    inputName = "HAL_Simulator.FRC_NetworkCommunication32.dll";
                    outputName = "FRC_NetworkCommunication.dlln";
                    break;
                case OsType.Windows64:
                    inputName = "HAL_Simulator.FRC_NetworkCommunication64.dll";
                    outputName = "FRC_NetworkCommunication.dlln";
                    break;
                case OsType.Linux32:
                    inputName = "HAL_Simulator.libFRC_NetworkCommunication32.so";
                    outputName = "libFRC_NetworkCommunication.so";
                    break;
                case OsType.Linux64:
                    inputName = "HAL_Simulator.libFRC_NetworkCommunication64.so";
                    outputName = "libFRC_NetworkCommunication.so";
                    break;
                case OsType.RoboRio:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            outputName = Path.GetTempPath() + outputName;
            byte[] bytes = null;
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(inputName))
            {
                if (s == null || s.Length == 0)
                    return null;
                bytes = new byte[(int) s.Length];
                s.Read(bytes, 0, (int) s.Length);
            }
            bool isFileSame = true;

            //If file exists
            if (File.Exists(outputName))
            {
                //Load existing file into memory
                byte[] existingFile = File.ReadAllBytes(outputName);
                //If files are different length they are different,
                //and we can automatically assume they are different.
                if (existingFile.Length != bytes.Length)
                {
                    isFileSame = false;
                }
                else
                {
                    //Otherwise directly compare the files
                    //I first tried hashing, but that took 1.5-2.0 seconds,
                    //wheras this took 0.3 seconds.
                    for (int i = 0; i < existingFile.Length; i++)
                    {
                        if (bytes[i] != existingFile[i])
                        {
                            isFileSame = false;
                        }
                    }
                }
            }
            else
            {
                isFileSame = false;
            }

            //If file is different write the new file
            if (!isFileSame)
            {
                if (File.Exists(outputName))
                    File.Delete(outputName);
                File.WriteAllBytes(outputName, bytes);
            }
            //Force a garbage collection, since we just wasted about 12 MB of RAM.
            GC.Collect();

            return outputName;

        }

        internal static IntPtr LoadLibrary(string dllLoc, OsType type, out ILibraryLoader loader)
        {
            switch (type)
            {
                case OsType.Windows32:
                case OsType.Windows64:
                    loader = new WindowsLibraryLoader();
                    return loader.LoadLibrary(dllLoc);
                case OsType.Linux32:
                case OsType.Linux64:
                    loader = new LinuxLibraryLoader();
                    return loader.LoadLibrary(dllLoc);
                case OsType.RoboRio:
                    loader = new RoboRioLibraryLoader();
                    return loader.LoadLibrary(dllLoc);
                default:
                    loader = null;
                    return IntPtr.Zero;
            }
        }
    }
}
