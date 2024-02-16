using System;

namespace Stereologue;

[Flags]
public enum LogType
{
    File = 1,
    Nt = 2,
    Once = 4,
}
