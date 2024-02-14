using System;
using System.Reflection;

namespace Monologue;

internal class EvaluateField {
    private static Func<object> GetSupplier(FieldInfo field, ILogged logged) {
        return () => {
            return field.GetValue(logged)!;
        };
    }

    private static object? GetField(FieldInfo field, ILogged logged) {
        return field.GetValue(logged);
    }

    public static void EvalField(FieldInfo field, ILogged loggable, string rootPath) {
    }

    private static bool EvalNestedLogged(FieldInfo field, ILogged loggable, string rootPath)
    {
        var fieldValue = GetField(field, loggable);
        if (fieldValue == null || field.GetCustomAttribute<IgnoreLoggedAttribute>() != null) {
            return false;
        }

        bool recursed = false;

        if (typeof(ILogged).IsAssignableFrom(field.FieldType)) {
            ILogged logged = (ILogged)fieldValue;

        }
    }
}
