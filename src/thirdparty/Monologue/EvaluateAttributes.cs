using System.Collections.Generic;
using System.Reflection;

namespace Monologue;

internal static class EvaluateAttributes
{
    public static IEnumerable<LogAttribute> From(MemberInfo member) {
        return member.GetCustomAttributes<LogAttribute>();
    }
}
