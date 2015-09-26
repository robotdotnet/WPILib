using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HAL_Base;

namespace HAL_Simulator
{
    enum OsType
    {
        Windows32,
        Windows64,
        Linux32,
        Linux64,
        RoboRIO
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
                    return File.Exists("/usr/local/frc/bin/frcRunRobot.sh") ? OsType.RoboRIO : OsType.Linux32;
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
                    return true;
                case OsType.Linux32:
                    return true;
                case OsType.Linux64:
                    return true;
                case OsType.RoboRIO:
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
                    outputName = "FRC_NetworkCommunication.dll";
                    break;
                case OsType.Windows64:
                    inputName = "HAL_Simulator.FRC_NetworkCommunication64.dll";
                    outputName = "ntcore.dll";
                    break;
                case OsType.Linux32:
                    inputName = "HAL_Simulator.libFRC_NetworkCommunication32.so";
                    outputName = "libFRC_NetworkCommunication.so";
                    break;
                case OsType.Linux64:
                    inputName = "HAL_Simulator.libFRC_NetworkCommunication64.so";
                    outputName = "libFRC_NetworkCommunication.so";
                    break;
                case OsType.RoboRIO:
                    return null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            byte[] bytes = null;
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(inputName))
            {
                if (s == null || s.Length == 0)
                    return null;
                bytes = new byte[(int) s.Length];
                s.Read(bytes, 0, (int) s.Length);

                if (File.Exists(outputName))
                    File.Delete(outputName);
                File.WriteAllBytes(outputName, bytes);
            }
            return outputName;

        }

        internal static IntPtr LoadDll(string dllLoc, OsType type)
        {
            ILibraryLoader loader = null;
            switch (type)
            {
                case OsType.Windows32:
                case OsType.Windows64:
                    loader = new WindowsLibraryLoader();
                    return loader.LoadLibrary(dllLoc);
                case OsType.Linux32:
                case OsType.Linux64:
                case OsType.RoboRIO:
                    loader = new LinuxLibraryLoader();
                    return loader.LoadLibrary(dllLoc);
                default:
                    return IntPtr.Zero;
            }
        }
    }
}
