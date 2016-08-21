using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.AthenaHAL
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALCAN
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage = (Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_sendMessage"), typeof(Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessageDelegate));

            Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessage = (Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_receiveMessage"), typeof(Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessageDelegate));

            Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSession = (Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_openStreamSession"), typeof(Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSessionDelegate));

            Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSession = (Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_closeStreamSession"), typeof(Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSessionDelegate));

            Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSession = (Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "FRC_NetworkCommunication_CANSessionMux_readStreamSession"), typeof(Base.HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSessionDelegate));

        }
    }
}