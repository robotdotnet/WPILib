using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Diagnostics;

namespace WPIUtil;

public static class WpiGuard
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T RequireNotNull<T>([NotNull] T? value, [CallerArgumentExpression(nameof(value))] string name = "")
    {
        if (value == null)
        {
            ThrowHelper.ThrowArgumentNullExceptionForIsNotNull<T>(name);
        }
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T RequireNotNull<T>([NotNull] T? value, [CallerArgumentExpression(nameof(value))] string name = "") where T : struct
    {
        if (!value.HasValue)
        {
            ThrowHelper.ThrowArgumentNullExceptionForIsNotNull<T?>(name);
        }
        return value.Value;
    }

    private static class ThrowHelper
    {
        [DoesNotReturn]
        public static void ThrowArgumentExceptionForIsNull<T>(T? value, string name) where T : struct
        {
            throw new ArgumentException($"Parameter {AssertString(name)} ({typeof(T?).ToTypeString()}) must be null, was {AssertString(value)} ({typeof(T).ToTypeString()}).", name);
        }

        private static string AssertString(object? obj)
        {
            if (obj is not string)
            {
                if (obj != null)
                {
                    return $"<{obj}>";
                }

                return "null";
            }

            return $"\"{obj}\"";
        }

        [DoesNotReturn]
        public static void ThrowArgumentNullExceptionForIsNotNull<T>(string name)
        {
            throw new ArgumentNullException(name, $"Parameter {AssertString(name)} ({typeof(T).ToTypeString()}) must be not null).");
        }
    }
}
