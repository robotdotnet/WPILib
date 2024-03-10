using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_CAN_CloseStreamSessionCallback = delegate* unmanaged[Cdecl]<byte*, void*, uint, void>;
using unsafe HAL_CAN_GetCANStatusCallback = delegate* unmanaged[Cdecl]<byte*, void*, float*, uint*, uint*, uint*, uint*, int*, void>;
using unsafe HAL_CAN_OpenStreamSessionCallback = delegate* unmanaged[Cdecl]<byte*, void*, uint*, uint, uint, uint, int*, void>;
using unsafe HAL_CAN_ReadStreamSessionCallback = delegate* unmanaged[Cdecl]<byte*, void*, uint, WPIHal.CANStreamMessage*, uint, uint*, int*, void>;
using unsafe HAL_CAN_ReceiveMessageCallback = delegate* unmanaged[Cdecl]<byte*, void*, uint*, uint, byte*, byte*, uint*, int*, void>;
using unsafe HAL_CAN_SendMessageCallback = delegate* unmanaged[Cdecl]<byte*, void*, uint, byte*, byte, int, int*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalCanData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetCanData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCanSendMessageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSendMessageCallback(HAL_CAN_SendMessageCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCanSendMessageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void celSendMessageCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCanReceiveMessageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterReceiveMessageCallback(HAL_CAN_ReceiveMessageCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCanReceiveMessageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void celReceiveMessageCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCanOpenStreamCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterOpenStreamCallback(HAL_CAN_OpenStreamSessionCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCanOpenStreamCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void celOpenStreamCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCanCloseStreamCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCloseStreamCallback(HAL_CAN_CloseStreamSessionCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCanCloseStreamCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void celCloseStreamCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCanReadStreamCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterReadStreamCallback(HAL_CAN_ReadStreamSessionCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCanReadStreamCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void celReadStreamCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCanGetCANStatusCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterGetCANStatusCallback(HAL_CAN_GetCANStatusCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCanGetCANStatusCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void celGetCANStatusCallback(int uid);

}
