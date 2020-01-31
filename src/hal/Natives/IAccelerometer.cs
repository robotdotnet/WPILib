using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public unsafe interface IAccelerometer
    {
        double HAL_GetAccelerometerX();

        double HAL_GetAccelerometerY();

        double HAL_GetAccelerometerZ();

        void HAL_SetAccelerometerActive(int active);

        void HAL_SetAccelerometerRange(AccelerometerRange range);

    }
}
