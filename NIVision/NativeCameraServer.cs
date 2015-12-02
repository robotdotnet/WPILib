using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HAL.Base;

namespace NIVision
{

    public class NativeCameraServer
    {
        private static bool libraryLoaded = false;
        private static IntPtr library;

        private static string ExtractLibrary()
        {
            string inputName = "";
            string outputName = "";
            inputName = "NIVision.libcameraserver.so";
            outputName = "libcameraserver.so";
            outputName = Path.GetTempPath() + outputName;
            byte[] bytes = null;
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(inputName))
            {
                if (s == null || s.Length == 0)
                    return null;
                bytes = new byte[(int)s.Length];
                s.Read(bytes, 0, (int)s.Length);
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

        static NativeCameraServer()
        {
            if (!libraryLoaded)
            {
                if (HAL.Base.HAL.HALType == HAL.Base.HAL.HALTypes.RoboRIO)
                {
                    try
                    {
                        ILibraryLoader loader = new RoboRioLibraryLoader();

                        string loadedPath = ExtractLibrary();
                        if (string.IsNullOrEmpty(loadedPath))
                        {
                            //If fail to load, throw exception
                            throw new FileNotFoundException($"Library file could not be found in the resorces. Please contact RobotDotNet for support for this issue");
                        }
                        library = loader.LoadLibrary(loadedPath);
                        if (library == IntPtr.Zero)
                        {
                            //If library could not be loaded
                            throw new BadImageFormatException($"Library file {loadedPath} could not be loaded successfully.");
                        }
                        SetupRoboRio(library, loader);
                    }
                    catch (Exception)
                    {
                        SetupSimulation();
                    }
                }
                else
                {
                    SetupSimulation();
                }
            }
        }

        private static void SetupRoboRio(IntPtr library, ILibraryLoader loader)
        {
            StartImageCapture = StartImageCaptureRio;
            s_nativeStartImageCapture = (NativeStartImageCaptureDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_CameraServer_StartImageCapture"), typeof(NativeStartImageCaptureDelegate));
            IsAutoCaptureStarted = (IsAutoCaptureStartedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_CameraServer_IsAutoCaptureStarted"), typeof(IsAutoCaptureStartedDelegate));
            SetQuality = (SetQualityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_CameraServer_SetQuality"), typeof(SetQualityDelegate));
            GetQuality = (GetQualityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_CameraServer_GetQuality"), typeof(GetQualityDelegate));
            SetSize = (SetSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "c_CameraServer_SetSize"), typeof(SetSizeDelegate));
        }

        private delegate void NativeStartImageCaptureDelegate(byte[] cameraName);
        private static NativeStartImageCaptureDelegate s_nativeStartImageCapture;

        private static void StartImageCaptureRio(string cameraName)
        {
            byte[] name = IMAQdx.PublicMethods.CreateUTF8String(cameraName);
            s_nativeStartImageCapture(name);
        }

        private static void SetupSimulation()
        {
            StartImageCapture = StartImageCaptureSim;
            IsAutoCaptureStarted = IsAutoCaptureStartedSim;
            SetQuality = SetQualitySim;
            GetQuality = GetQualitySim;
            SetSize = SetSizeSim;
        }

        private static void SetSizeSim(uint size)
        {
        }

        private static uint s_simQuality = 5;
        private static uint GetQualitySim()
        {
            return s_simQuality;
        }

        private static void SetQualitySim(uint quality)
        {
            s_simQuality = quality;
        }

        private static bool IsAutoCaptureStartedSim()
        {
            return true;
        }

        private static void StartImageCaptureSim(string cameraName)
        {
        }

        public delegate void StartImageCaptureDelegate(string cameraName);

        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool IsAutoCaptureStartedDelegate();

        public delegate void SetQualityDelegate(uint quality);

        public delegate uint GetQualityDelegate();

        public delegate void SetSizeDelegate(uint size);

        public static StartImageCaptureDelegate StartImageCapture;
        public static IsAutoCaptureStartedDelegate IsAutoCaptureStarted;
        public static SetQualityDelegate SetQuality;
        public static GetQualityDelegate GetQuality;
        public static SetSizeDelegate SetSize;
    }
}
