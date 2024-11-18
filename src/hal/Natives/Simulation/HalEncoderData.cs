using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalEncoderData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_FindEncoderForChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FindForChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetEncoderData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderDigitalChannelA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDigitalChannelA(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderDigitalChannelB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDigitalChannelB(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WPIHal.Handles.HalSimDeviceHandle GetSimDevice(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderCountCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCountCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderCountCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCountCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetCount(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCount(int index, int count);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderPeriodCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPeriodCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderPeriodCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPeriodCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetPeriod(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPeriod(int index, double period);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderResetCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterResetCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderResetCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelResetCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderReset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetReset(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderReset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetReset(int index, [MarshalAs(UnmanagedType.I4)] bool reset);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderMaxPeriodCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterMaxPeriodCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderMaxPeriodCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelMaxPeriodCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderMaxPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetMaxPeriod(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderMaxPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetMaxPeriod(int index, double maxPeriod);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderDirectionCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDirectionCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderDirectionCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDirectionCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetDirection(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDirection(int index, [MarshalAs(UnmanagedType.I4)] bool direction);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderReverseDirectionCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterReverseDirectionCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderReverseDirectionCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelReverseDirectionCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderReverseDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetReverseDirection(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderReverseDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetReverseDirection(int index, [MarshalAs(UnmanagedType.I4)] bool reverseDirection);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderSamplesToAverageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSamplesToAverageCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderSamplesToAverageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSamplesToAverageCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderSamplesToAverage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSamplesToAverage(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderSamplesToAverage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSamplesToAverage(int index, int samplesToAverage);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderDistancePerPulseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDistancePerPulseCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelEncoderDistancePerPulseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDistancePerPulseCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderDistancePerPulse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetDistancePerPulse(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderDistancePerPulse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDistancePerPulse(int index, double distancePerPulse);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterEncoderAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderDistance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDistance(int index, double distance);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderDistance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetDistance(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetEncoderRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRate(int index, double rate);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetEncoderRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetRate(int index);

}
