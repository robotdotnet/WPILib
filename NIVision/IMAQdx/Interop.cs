using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NIVision.IMAQdx
{
    internal class Interop
    {
        private const string libraryPath = "libclan.dll";
        
        //[DllImport(libraryPath, EntryPoint = "IMAQdxSnap", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxSnap(IMAQdxSession @id, out Image @image);

        [DllImport(libraryPath, EntryPoint = "IMAQdxConfigureGrab", CallingConvention = CallingConvention.Cdecl)]
        public static extern IMAQdxError IMAQdxConfigureGrab(uint id);

        [DllImport(libraryPath, EntryPoint = "IMAQdxGrab", CallingConvention = CallingConvention.Cdecl)]
        public static extern IMAQdxError IMAQdxGrab(uint id, IntPtr image, int waitForNextBuffer, ref uint actualBufferNumber);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxSequence", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxSequence(IMAQdxSession @id, IntPtr[] @images, uInt32 @count);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxDiscoverEthernetCameras", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxDiscoverEthernetCameras([MarshalAs(UnmanagedType.LPStr)] string @address, uInt32 @timeout);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxEnumerateCameras", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxEnumerateCameras(IMAQdxCameraInformation[] @cameraInformationArray, out uInt32 @count, bool32 @connectedOnly);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxResetCamera", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxResetCamera([MarshalAs(UnmanagedType.LPStr)] string @name, bool32 @resetAll);

        [DllImport(libraryPath, EntryPoint = "IMAQdxOpenCamera", CallingConvention = CallingConvention.Cdecl)]
        public static extern IMAQdxError IMAQdxOpenCamera(byte[] name, IMAQdxCameraControlMode mode, ref uint id);

        [DllImport(libraryPath, EntryPoint = "IMAQdxCloseCamera", CallingConvention = CallingConvention.Cdecl)]
        public static extern IMAQdxError IMAQdxCloseCamera(uint id);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxConfigureAcquisition", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxConfigureAcquisition(IMAQdxSession @id, bool32 @continuous, uInt32 @bufferCount);

        [DllImport(libraryPath, EntryPoint = "IMAQdxStartAcquisition", CallingConvention = CallingConvention.Cdecl)]
        public static extern IMAQdxError IMAQdxStartAcquisition(uint id);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxGetImage", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxGetImage(IMAQdxSession @id, out Image @image, IMAQdxBufferNumberMode @mode, uInt32 @desiredBufferNumber, out uInt32 @actualBufferNumber);

        [DllImport(libraryPath, EntryPoint = "IMAQdxGetImageData", CallingConvention = CallingConvention.Cdecl)]
        public static extern IMAQdxError IMAQdxGetImageData(uint @id, IntPtr @buffer, uint @bufferSize, IMAQdxBufferNumberMode @mode, uint @desiredBufferNumber, ref uint @actualBufferNumber);

        [DllImport(libraryPath, EntryPoint = "IMAQdxStopAcquisition", CallingConvention = CallingConvention.Cdecl)]
        public static extern IMAQdxError IMAQdxStopAcquisition(uint id);

        [DllImport(libraryPath, EntryPoint = "IMAQdxUnconfigureAcquisition", CallingConvention = CallingConvention.Cdecl)]
        public static extern IMAQdxError IMAQdxUnconfigureAcquisition(uint id);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxEnumerateVideoModes", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxEnumerateVideoModes(IMAQdxSession @id, IMAQdxVideoMode[] @videoModeArray, out uInt32 @count, out uInt32 @currentMode);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxEnumerateAttributes", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxEnumerateAttributes(IMAQdxSession @id, IMAQdxAttributeInformation[] @attributeInformationArray, out uInt32 @count, [MarshalAs(UnmanagedType.LPStr)] string @root);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxGetAttribute", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxGetAttribute(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, IMAQdxValueType @type, IntPtr @value);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxSetAttribute", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxSetAttribute(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, IMAQdxValueType @type);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxGetAttributeMinimum", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxGetAttributeMinimum(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, IMAQdxValueType @type, IntPtr @value);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxGetAttributeMaximum", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxGetAttributeMaximum(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, IMAQdxValueType @type, IntPtr @value);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxGetAttributeIncrement", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxGetAttributeIncrement(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, IMAQdxValueType @type, IntPtr @value);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxGetAttributeType", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxGetAttributeType(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, out IMAQdxAttributeType @type);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxIsAttributeReadable", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxIsAttributeReadable(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, out bool32 @readable);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxIsAttributeWritable", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxIsAttributeWritable(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, out bool32 @writable);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxEnumerateAttributeValues", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxEnumerateAttributeValues(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, IMAQdxEnumItem[] @list, out uInt32 @size);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxGetAttributeTooltip", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxGetAttributeTooltip(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, IntPtr @tooltip, uInt32 @length);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxGetAttributeUnits", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxGetAttributeUnits(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, IntPtr @units, uInt32 @length);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxRegisterFrameDoneEvent", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxRegisterFrameDoneEvent(IMAQdxSession @id, uInt32 @bufferInterval, FrameDoneEventCallbackPtr @callbackFunction, IntPtr @callbackData);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxRegisterPnpEvent", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxRegisterPnpEvent(IMAQdxSession @id, IMAQdxPnpEvent @event, PnpEventCallbackPtr @callbackFunction, IntPtr @callbackData);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxWriteRegister", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxWriteRegister(IMAQdxSession @id, uInt32 @offset, uInt32 @value);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxReadRegister", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxReadRegister(IMAQdxSession @id, uInt32 @offset, out uInt32 @value);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxWriteMemory", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxWriteMemory(IMAQdxSession @id, uInt32 @offset, [MarshalAs(UnmanagedType.LPStr)] string @values, uInt32 @count);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxReadMemory", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxReadMemory(IMAQdxSession @id, uInt32 @offset, IntPtr @values, uInt32 @count);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxGetErrorString", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxGetErrorString(IMAQdxError @error, IntPtr @message, uInt32 @messageLength);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxWriteAttributes", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxWriteAttributes(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @filename);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxReadAttributes", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxReadAttributes(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @filename);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxResetEthernetCameraAddress", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxResetEthernetCameraAddress([MarshalAs(UnmanagedType.LPStr)] string @name, [MarshalAs(UnmanagedType.LPStr)] string @address, [MarshalAs(UnmanagedType.LPStr)] string @subnet, [MarshalAs(UnmanagedType.LPStr)] string @gateway, uInt32 @timeout);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxEnumerateAttributes2", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxEnumerateAttributes2(IMAQdxSession @id, IMAQdxAttributeInformation[] @attributeInformationArray, out uInt32 @count, [MarshalAs(UnmanagedType.LPStr)] string @root, IMAQdxAttributeVisibility @visibility);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxGetAttributeVisibility", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxGetAttributeVisibility(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, out IMAQdxAttributeVisibility @visibility);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxGetAttributeDescription", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxGetAttributeDescription(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, IntPtr @description, uInt32 @length);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxGetAttributeDisplayName", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxGetAttributeDisplayName(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, IntPtr @displayName, uInt32 @length);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxDispose", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxDispose(IntPtr @buffer);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxRegisterAttributeUpdatedEvent", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxRegisterAttributeUpdatedEvent(IMAQdxSession @id, [MarshalAs(UnmanagedType.LPStr)] string @name, AttributeUpdatedEventCallbackPtr @callbackFunction, IntPtr @callbackData);

        //[DllImport(libraryPath, EntryPoint = "IMAQdxEnumerateAttributes3", CallingConvention = CallingConvention.Cdecl)]
        //public static extern IMAQdxError IMAQdxEnumerateAttributes3(IMAQdxSession @id, IMAQdxAttributeInformation[] @attributeInformationArray, out uInt32 @count, [MarshalAs(UnmanagedType.LPStr)] string @root, IMAQdxAttributeVisibility @visibility);

    }
}
