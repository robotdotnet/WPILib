using System;
using System.Text;

namespace NIVision.IMAQdx
{
    public static class PublicMethods
    {
        private static byte[] CreateUTF8String(this string str, out UIntPtr size)
        {
            var bytes = Encoding.UTF8.GetByteCount(str);

            var buffer = new byte[bytes + 1];
            size = (UIntPtr)bytes;
            Encoding.UTF8.GetBytes(str, 0, str.Length, buffer, 0);
            buffer[bytes] = 0;
            return buffer;
        }

        internal static byte[] CreateUTF8String(this string str)
        {
            var bytes = Encoding.UTF8.GetByteCount(str);

            var buffer = new byte[bytes + 1];
            UIntPtr size = (UIntPtr)bytes;
            Encoding.UTF8.GetBytes(str, 0, str.Length, buffer, 0);
            buffer[bytes] = 0;
            return buffer;
        }


        public static uint IMAQdxOpenCamera(string name, IMAQdxCameraControlMode mode)
        {
            byte[] name_buf = name?.CreateUTF8String();
            uint id = 0;
            Interop.IMAQdxOpenCamera(name_buf ?? new byte[] {0}, mode, ref id);
            return id;
        }

        public static void IMAQdxCloseCamera(uint id)
        {
            Interop.IMAQdxCloseCamera(id);
        }

        public static void IMAQdxConfigureGrab(uint id)
        {
            Interop.IMAQdxConfigureGrab(id);
        }

        public static void IMAQdxStartAcquisition(uint id)
        {
            Interop.IMAQdxStartAcquisition(id);
        }

        public static void IMAQdxStopAcquisition(uint id)
        {
            Interop.IMAQdxStopAcquisition(id);
        }

        public static void IMAQdxUnconfigureAcquisition(uint id)
        {
            Interop.IMAQdxUnconfigureAcquisition(id);
        }

        public static uint IMAQdxGrab(uint id, Image image, int waitForNextBuffer)
        {
            uint status = 0;
            Interop.IMAQdxGrab(id, image.NativeObj, waitForNextBuffer, ref status);
            return status;
        }

        public static uint IMAQdxGetImageData(uint id, IntPtr buffer, uint size, IMAQdxBufferNumberMode mode,
            uint desiredBufferNumber)
        {
            uint bufNum = 0;
            Interop.IMAQdxGetImageData(id, buffer, size, mode, desiredBufferNumber, ref bufNum);
            return bufNum;
        }

    }
}
