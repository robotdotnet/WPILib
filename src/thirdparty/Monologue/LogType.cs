using System;

namespace Monologue;

[Flags]
public enum LogType
{
    None = 0,
    File = 1,
    Nt = 2,
    Once = 4,
}
