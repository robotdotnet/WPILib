using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALCAN
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage = (HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_sendMessage"), typeof(HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate));

            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessage = (HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_receiveMessage"), typeof(HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate));

            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSession = (HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_openStreamSession"), typeof(HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate));

            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSession = (HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_closeStreamSession"), typeof(HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate));

            HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSession = (HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_readStreamSession"), typeof(HAL_Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate));

        }
    }
}
