using System;
using System.Reflection;

namespace Stereologue;

internal class EvaluateField
{
    private static Func<object> GetSupplier(FieldInfo field, ILogged logged)
    {
        return () =>
        {
            return field.GetValue(logged)!;
        };
    }

    private static object? GetField(FieldInfo field, ILogged logged)
    {
        return field.GetValue(logged);
    }

    public static void EvalField(FieldInfo field, ILogged loggable, string rootPath)
    {
    }
}
