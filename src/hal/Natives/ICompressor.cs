using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface ICompressor
    {
#pragma warning disable CA1716 // Identifiers should not match keywords
        int HAL_CheckCompressorModule(int module);
#pragma warning restore CA1716 // Identifiers should not match keywords

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

#pragma warning disable CA1716 // Identifiers should not match keywords
        [StatusCheckRange(0, typeof(StatusHandling), "CompressorStatusCheck")] int HAL_InitializeCompressor(int module);
#pragma warning restore CA1716 // Identifiers should not match keywords

        [StatusCheckLastParameter] void HAL_SetCompressorClosedLoopControl(int compressorHandle, int value);

    }
}
