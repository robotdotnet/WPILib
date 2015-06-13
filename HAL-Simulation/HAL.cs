

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using HAL_Base;

namespace HAL_FRC
{
    /*
    public enum HALAllianceStationID
    {
// ReSharper disable InconsistentNaming
        kHALAllianceStationID_red1,


        kHALAllianceStationID_red2,

        kHALAllianceStationID_red3,

        kHALAllianceStationID_blue1,

        kHALAllianceStationID_blue2,

        kHALAllianceStationID_blue3,
// ReSharper restore InconsistentNaming
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct HALJoystickAxes
    {
        /// unsigned short
        public ushort count;

        /// short[12]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I2)]
        public short[] axes;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct HALJoystickPOVs
    {
        /// unsigned short
        public ushort count;

        /// short[12]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I2)]
        public short[] povs;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct HALJoystickButtons
    {
        /// unsigned int
        public uint buttons;

        /// byte
        public byte count;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct HALJoystickDescriptor
    {
        /// byte
        public byte isXbox;

        /// byte
        public byte type;

        /// char[256]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 256)]
        public string name;

        /// byte
        public byte axisCount;

        /// byte
        public byte axisTypes;

        /// byte
        public byte buttonCount;

        /// byte
        public byte povCount;
    }
     * */

    public class HAL
    {
         static Dictionary<string, object> halData = new Dictionary<string, object>();
        //public const string "libHALAthena_shared.so" = "libHALAthena_shared.so"; 

        /// Return Type: void*
        ///pin: byte
        //[System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "getPort")]
        //public static extern System.IntPtr GetPort(byte pin);

        public static IntPtr getPort(byte pin)
        {
            return getPortWithModule(1, pin);
        }

        public static IntPtr getPortWithModule(byte module, byte pin)
        {
            Port port = new Port
            {
                pin = pin,
                module = module
            };
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(port));
            Marshal.StructureToPtr(port, ptr, true);
            return ptr;
        }

        public static void HALSetNewDataSem(IntPtr sem)
        {
            MULTIWAIT_ID p = (MULTIWAIT_ID)Marshal.PtrToStructure(sem, typeof(MULTIWAIT_ID));
            SimData.halNewDataSem = p;
        }

        [DllImport("libHALAthena_shared.so", EntryPoint = "getHALErrorMessage")]
        public static extern IntPtr getHALErrorMessage(int code);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getFPGAVersion")]
        public static extern ushort getFPGAVersion(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getFPGARevision")]
        public static extern uint getFPGARevision(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getFPGATime")]
        public static extern uint getFPGATime(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "getFPGAButton")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool getFPGAButton(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALSetErrorData")]
        public static extern int HALSetErrorData(string errors, int errorsLength, int wait_ms);

        //[System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALGetControlWord")]
        public static int HALGetControlWord(ref HALControlWord data)
        {
            return 0;
        }

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetAllianceStation")]
        public static extern int HALGetAllianceStation(ref HALAllianceStationID allianceStation);

        public static int HALGetJoystickAxes(byte joystickNum, ref HALJoystickAxes axes)
        {
            axes.axes = new HALJoystickAxesArray();
            axes.count = 12; //Need to make this netter.
            return 0;
        }

        public static int HALGetJoystickPOVs(byte joystickNum, ref HALJoystickPOVs povs)
        {
            povs.povs = new HALJoystickPOVArray();
            povs.count = 12;
            return 0;
        }

        public static int HALGetJoystickButtons(byte joystickNum, ref HALJoystickButtons buttons)
        {
            buttons.buttons = 123;
            buttons.count = 12;
            return 0;
        }

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetJoystickDescriptor")]
        public static extern int HALGetJoystickDescriptor(byte joystickNum, ref HALJoystickDescriptor desc);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALSetJoystickOutputs")]
        public static extern int HALSetJoystickOutputs(byte joystickNum, uint outputs, ushort leftRumble, ushort rightRumble);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetMatchTime")]
        public static extern int HALGetMatchTime(ref float matchTime);

        //[System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALSetNewDataSem")]
        //public static extern void HALSetNewDataSem(System.IntPtr sem);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetSystemActive")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool HALGetSystemActive(ref int status);

        [DllImport("libHALAthena_shared.so", EntryPoint = "HALGetBrownedOut")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool HALGetBrownedOut(ref int status);

        //[System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALInitialize")]
        public static int HALInitialize(int mode)
        {
            SimData.ResetHALData();

            timer = new Timer((sender) =>
            {
                if (SimData.halNewDataSem != null)
                {
                    lock (SimData.halNewDataSem.Value.lockObject)
                    {
                        try
                        {
                            Monitor.PulseAll(SimData.halNewDataSem.Value.lockObject);
                        }
                        catch (ThreadInterruptedException)
                        {

                        }
                    }
                }
            },null, 20, 20);

            return 1;
        }

        private static Timer timer;

        public static void HALNetworkCommunicationObserveUserProgramStarting()
        {
        }

        public static void HALNetworkCommunicationObserveUserProgramDisabled()
        {
            
        }

        public static void HALNetworkCommunicationObserveUserProgramAutonomous()
        {
        }

        public static void HALNetworkCommunicationObserveUserProgramTeleop()
        {
            
        }

        public static void HALNetworkCommunicationObserveUserProgramTest()
        {
        }

        //[System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "HALReport")]
        //public static extern uint HALReport(byte resource, byte instanceNumber, byte context, string feature = null);

        [DllImport("libHALAthena_shared.so", EntryPoint = "NumericArrayResize")]
        public static extern void NumericArrayResize();

        [DllImport("libHALAthena_shared.so", EntryPoint = "RTSetCleanupProc")]
        public static extern void RTSetCleanupProc();

        [DllImport("libHALAthena_shared.so", EntryPoint = "EDVR_CreateReference")]
        public static extern void EDVR_CreateReference();

        [DllImport("libHALAthena_shared.so", EntryPoint = "Occur")]
        public static extern void Occur();

        /// Return Type: void
        //[System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramStarting")]
        public static void NetworkCommunicationObserveUserProgramStarting()
        {
        }


        /// Return Type: void
        //[System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramDisabled")]
        public static void NetworkCommunicationObserveUserProgramDisabled()
        {
        }


        /// Return Type: void
        //[System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramAutonomous")]
        public static void NetworkCommunicationObserveUserProgramAutonomous()
        {
        }


        /// Return Type: void
        //[System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramTeleop")]
        public static void NetworkCommunicationObserveUserProgramTeleop()
        {
        }


        /// Return Type: void
        //[System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALNetworkCommunicationObserveUserProgramTest")]
        public static void NetworkCommunicationObserveUserProgramTest()
        {
        }


        /// Return Type: unsigned int
        ///resource: byte
        ///instanceNumber: byte
        ///context: byte
        ///feature: char*
        //[System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "HALReport")]
        //private static extern uint HALReport(byte resource, byte instanceNumber, byte context = 0, [System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string feature = null);
        public static uint HALReport(byte resource, byte instanceNumber, byte context = 0, string feature = null)
        {
            return 0;
        }

        


        //Move to WPILib

        public static uint Report(ResourceType resource, Instances instanceNumber, byte context = 0,
            string feature = null)
        {
            return HALReport((byte)resource, (byte)instanceNumber, context, feature);
        }

        public static uint Report(ResourceType resource, byte instanceNumber, byte context = 0,
            string feature = null)
        {
            return HALReport((byte)resource, (byte)instanceNumber, context, feature);
        }

        public static uint Report(byte resource, Instances instanceNumber, byte context = 0,
            string feature = null)
        {
            return HALReport((byte)resource, (byte)instanceNumber, context, feature);
        }

        public static uint Report(byte resource, byte instanceNumber, byte context = 0,
            string feature = null)
        {
            return HALReport((byte)resource, (byte)instanceNumber, context, feature);
        }

        /*
        public static void CheckStatus(int status)
        {
            if (status < 0)
            {
                string message = GetHALErrorMessage(status);
                throw new SystemException(" Code: " + status + ". " + message);
            }
            else if (status > 0)
            {
                string message = GetHALErrorMessage(status);
                DriverStation.ReportError(message, true);
            }
        }
         * */
        /*
        public static HALControlWord HALGetControlWord()
        {
            //HALControlWord temp = new HALControlWord();
            uint temp = 0;
            //GetControlWord(ref temp);
            return new HALControlWord(temp);
        }
         */
    }
    /*
    public struct HALControlWord
    {
        private uint _wordData;

        public HALControlWord(uint data)
        {
            _wordData = data;
        }

        public bool GetEnabled()
        {
            return (_wordData & (1 << 1 - 1)) != 0;
        }

        public bool GetAutonomous()
        {
            return (_wordData & (1 << 2 - 1)) != 0;
        }

        public bool GetTest()
        {
            return (_wordData & (1 << 3 - 1)) != 0;
        }

        public bool GetEStop()
        {
            return (_wordData & (1 << 4 - 1)) != 0;
        }

        public bool GetFMSAttached()
        {
            return (_wordData & (1 << 5 - 1)) != 0;
        }

        public bool GetDSAttached()
        {
            return (_wordData & (1 << 6 - 1)) != 0;
        }
    }
     * */
}
