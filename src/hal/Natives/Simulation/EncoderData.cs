using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalEncoderData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_FindEncoderForChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FindEncoderForChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetEncoderData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetEncoderData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderDigitalChannelA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetEncoderDigitalChannelA(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderDigitalChannelB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetEncoderDigitalChannelB(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterEncoderInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelEncoderInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetEncoderInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WPIHal.Handles.HalSimDeviceHandle GetEncoderSimDevice(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderCountCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterEncoderCountCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderCountCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelEncoderCountCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetEncoderCount(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderCount(int index, int count);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderPeriodCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterEncoderPeriodCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderPeriodCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelEncoderPeriodCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetEncoderPeriod(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderPeriod(int index, double period);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderResetCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterEncoderResetCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderResetCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelEncoderResetCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderReset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetEncoderReset(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderReset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderReset(int index, [MarshalAs(UnmanagedType.I4)] bool reset);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderMaxPeriodCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterEncoderMaxPeriodCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderMaxPeriodCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelEncoderMaxPeriodCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderMaxPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetEncoderMaxPeriod(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderMaxPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderMaxPeriod(int index, double maxPeriod);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderDirectionCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterEncoderDirectionCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderDirectionCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelEncoderDirectionCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetEncoderDirection(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderDirection(int index, [MarshalAs(UnmanagedType.I4)] bool direction);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderReverseDirectionCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterEncoderReverseDirectionCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderReverseDirectionCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelEncoderReverseDirectionCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderReverseDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetEncoderReverseDirection(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderReverseDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderReverseDirection(int index, [MarshalAs(UnmanagedType.I4)] bool reverseDirection);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderSamplesToAverageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterEncoderSamplesToAverageCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderSamplesToAverageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelEncoderSamplesToAverageCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderSamplesToAverage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetEncoderSamplesToAverage(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderSamplesToAverage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderSamplesToAverage(int index, int samplesToAverage);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderDistancePerPulseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterEncoderDistancePerPulseCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderDistancePerPulseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelEncoderDistancePerPulseCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderDistancePerPulse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetEncoderDistancePerPulse(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderDistancePerPulse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderDistancePerPulse(int index, double distancePerPulse);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterEncoderAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderDistance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderDistance(int index, double distance);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderDistance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetEncoderDistance(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderRate(int index, double rate);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetEncoderRate(int index);

}
