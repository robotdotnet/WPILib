
using Hal.Natives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static class HalInitializer
    {
        public static bool Initialize()
        {
            var generator = NativeLibraryLoader.LoadNativeLibraryGenerator("wpiHal");
            if (generator == null)
            {
                return false;
            }

            NativeInterfaceInitializer.InitializeNativeTypes(typeof(HalInitializer).Assembly, generator);

            return true;
        }
    }
}
