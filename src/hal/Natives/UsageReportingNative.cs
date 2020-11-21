using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
public unsafe class UsageReportingNative : IUsageReporting
{
[NativeFunctionPointer("HAL_Report")]
private readonly delegate* unmanaged[Cdecl]<int, int, int, byte*, int> HAL_ReportFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_Report(int resource, int instanceNumber, int context, byte* feature)
{
return HAL_ReportFunc(resource, instanceNumber, context, feature);
}



}
}
