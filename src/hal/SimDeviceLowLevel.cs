
using Hal.Natives;
using System;
using System.Runtime.InteropServices;
using System.Text;
using WPIUtil;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class SimDeviceLowLevel
    {
#pragma warning disable CS0649 // Field is never assigned to
        internal static SimDeviceLowLevelNative lowLevel = null!;
#pragma warning restore CS0649 // Field is never assigned to

        public static int Create(string name)
        {
            var nameutf = new UTF8String(name);
            fixed (byte* namep = nameutf.Buffer)
            {
                return lowLevel.HAL_CreateSimDevice(namep);
            }
        }

        public static int CreateSimValue(int device, string name, int rdonly, in Value initialValue)
        {
            var valueCpy = initialValue;
            var nameutf = new UTF8String(name);
            fixed (byte* namep = nameutf.Buffer)
            {
                return lowLevel.HAL_CreateSimValue(device, namep, rdonly, &valueCpy);
            }

        }

        public static int CreateSimValueEnum(int device, string name, int rdonly, string[] options, int initialValue)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            var nameutf = new UTF8String(name);
            byte** optionsPtr = stackalloc byte*[options.Length];
            for (int i = 0; i < options.Length; i++)
            {
                var strLenUTF = Encoding.UTF8.GetByteCount(options[i]);
                optionsPtr[i] = (byte*)Marshal.AllocHGlobal(strLenUTF + 1);
                fixed (char* tmpStr = options[i])
                {
                    Encoding.UTF8.GetBytes(tmpStr, options[i].Length, optionsPtr[i], strLenUTF);
                }
                optionsPtr[i][strLenUTF] = 0;

            }

            fixed (byte* namep = nameutf.Buffer)
            {
                try
                {
                    return lowLevel.HAL_CreateSimValueEnum(device, namep, rdonly, options.Length, optionsPtr, initialValue);
                }
                finally
                {
                    for (int i = 0; i < options.Length; i++)
                    {
                        Marshal.FreeHGlobal((IntPtr)optionsPtr[i]);
                    }
                }
            }

        }

        public static void Free(int handle)
        {
            lowLevel.HAL_FreeSimDevice(handle);
        }

        public static void GetSimValue(int handle, out Value value)
        {
            Value tmp;
            lowLevel.HAL_GetSimValue(handle, &tmp);
            value = tmp;
        }

        public static void SetSimValue(int handle, in Value value)
        {
            var tmp = value;
            lowLevel.HAL_SetSimValue(handle, &tmp);
        }

    }
}
