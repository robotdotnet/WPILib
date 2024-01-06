using System;

namespace WPIUtil.Marshal;

public interface INativeArrayFree {
    static abstract unsafe void Free(void* ptr, int len);
}