using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers;

public static class SpecialTypeExtensions
{
    public static bool IsIntegerLikeType(this SpecialType type)
    {
        return
            type == SpecialType.System_Byte ||
            type == SpecialType.System_SByte ||
            type == SpecialType.System_Int16 ||
            type == SpecialType.System_UInt16 ||
            type == SpecialType.System_Int32 ||
            type == SpecialType.System_UInt32 ||
            type == SpecialType.System_Int64 ||
            type == SpecialType.System_UInt64 ||
            type == SpecialType.System_IntPtr ||
            type == SpecialType.System_UIntPtr;
    }
}
