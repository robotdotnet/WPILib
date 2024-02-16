using System.Collections.Generic;
using System.Reflection;

namespace Stereologue;

internal static class EvaluateAttributes
{
    public static IEnumerable<LogAttribute> From(MemberInfo member)
    {
        return member.GetCustomAttributes<LogAttribute>();
    }
}
