using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALCAN
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage = (global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_sendMessage"), typeof(global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate));

            global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessage = (global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_receiveMessage"), typeof(global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate));

            global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSession = (global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_openStreamSession"), typeof(global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate));

            global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSession = (global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_closeStreamSession"), typeof(global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate));

            global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSession = (global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_readStreamSession"), typeof(global::HAL.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate));

        }
    }
}
