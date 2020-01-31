using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    public unsafe interface ICompressor
    {
        int HAL_CheckCompressorModule(int module);

        [StatusCheckLastParameter] int HAL_GetCompressor(int compressorHandle);

        [StatusCheckLastParameter] int HAL_GetCompressorClosedLoopControl(int compressorHandle);

        [StatusCheckLastParameter] double HAL_GetCompressorCurrent(int compressorHandle);

        [StatusCheckLastParameter] int HAL_GetCompressorCurrentTooHighFault(int compressorHandle);

        [StatusCheckLastParameter] int HAL_GetCompressorCurrentTooHighStickyFault(int compressorHandle);

        [StatusCheckLastParameter] int HAL_GetCompressorNotConnectedFault(int compressorHandle);

        [StatusCheckLastParameter] int HAL_GetCompressorNotConnectedStickyFault(int compressorHandle);

        [StatusCheckLastParameter] int HAL_GetCompressorPressureSwitch(int compressorHandle);

        [StatusCheckLastParameter] int HAL_GetCompressorShortedFault(int compressorHandle);

        [StatusCheckLastParameter] int HAL_GetCompressorShortedStickyFault(int compressorHandle);

        [StatusCheckLastParameter] int HAL_InitializeCompressor(int module);

        [StatusCheckLastParameter] void HAL_SetCompressorClosedLoopControl(int compressorHandle, int value);

    }
}
