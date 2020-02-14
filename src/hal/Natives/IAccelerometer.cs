using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface IAccelerometer
    {
        double HAL_GetAccelerometerX();

        double HAL_GetAccelerometerY();

        double HAL_GetAccelerometerZ();

        void HAL_SetAccelerometerActive(int active);

        void HAL_SetAccelerometerRange(AccelerometerRange range);

    }
}
