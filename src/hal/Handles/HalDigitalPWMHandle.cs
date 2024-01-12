﻿using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIHal.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<HalDigitalPWMHandle>))]
public record struct HalDigitalPWMHandle(int Handle) : IWPIIntHandle;
