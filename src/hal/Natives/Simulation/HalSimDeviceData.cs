using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HALSIM_SimDeviceCallback = delegate* unmanaged[Cdecl]<byte*, void*, int, void>;
using unsafe HALSIM_SimValueCallback = delegate* unmanaged[Cdecl]<byte*, void*, int, int, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalSimDeviceData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSimDeviceEnabled", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEnabled(string prefix, [MarshalAs(UnmanagedType.I4)] bool enabled);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_IsSimDeviceEnabled", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool IsEnabled(string name);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSimDeviceCreatedCallback", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCreatedCallback(string prefix, void* param, HALSIM_SimDeviceCallback callback, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSimDeviceCreatedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCreatedCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSimDeviceFreedCallback", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterFreedCallback(string prefix, void* param, HALSIM_SimDeviceCallback callback, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSimDeviceFreedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelFreedCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSimDeviceHandle", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WPIHal.Handles.HalSimDeviceHandle GetHandle(string name);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSimDeviceName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetName(WPIHal.Handles.HalSimDeviceHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSimValueDeviceHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WPIHal.Handles.HalSimDeviceHandle GetSimValueDeviceHandle(WPIHal.Handles.HalSimDeviceHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_EnumerateSimDevices", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Enumerates(string prefix, void* param, HALSIM_SimDeviceCallback callback);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSimValueCreatedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSimValueCreatedCallback(WPIHal.Handles.HalSimDeviceHandle device, void* param, HALSIM_SimValueCallback callback, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSimValueCreatedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSimValueCreatedCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSimValueChangedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSimValueChangedCallback(WPIHal.Handles.HalSimDeviceHandle handle, void* param, HALSIM_SimValueCallback callback, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSimValueChangedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSimValueChangedCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSimValueResetCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSimValueResetCallback(WPIHal.Handles.HalSimDeviceHandle handle, void* param, HALSIM_SimValueCallback callback, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSimValueResetCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSimValueResetCallback(int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSimValueHandle", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WPIHal.Handles.HalSimDeviceHandle GetSimValueHandle(WPIHal.Handles.HalSimDeviceHandle device, string name);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_EnumerateSimValues")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void EnumerateSimValues(WPIHal.Handles.HalSimDeviceHandle device, void* param, HALSIM_SimValueCallback callback);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSimValueEnumOptions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte** GetSimValueEnumOptions(WPIHal.Handles.HalSimDeviceHandle handle, int* numOptions);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSimValueEnumDoubleValues")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double* GetSimValueEnumDoubleValues(WPIHal.Handles.HalSimDeviceHandle handle, int* numOptions);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetSimDeviceData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData();

}
