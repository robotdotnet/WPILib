using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface ISimDevice
    {
        int HAL_CreateSimDevice(byte* name);

        int HAL_CreateSimValue(int device, byte* name, int rdonly, Value* initialValue);

        int HAL_CreateSimValueEnum(int device, byte* name, int rdonly, int numOptions, byte** options, int initialValue);

        void HAL_FreeSimDevice(int handle);

        void HAL_GetSimValue(int handle, Value* value);

        void HAL_SetSimValue(int handle, Value* value);

    }
}
