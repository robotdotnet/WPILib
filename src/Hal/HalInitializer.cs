using FRC.NativeLibraryUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hal
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HalInterfaceAttribute : Attribute
    {
        public Type InterfaceType { get; }
        public HalInterfaceAttribute(Type interfaceType)
        {
            InterfaceType = interfaceType;
        }
    }

    public static class HalInitializer
    {
        private static void InitializeInterfaces(NativeLibraryLoader loader)
        {
            var types = typeof(HalInitializer).Assembly.GetTypes()
                .Select(x => (type: x, attribute: x.GetCustomAttributes(typeof(HalInterfaceAttribute), false)))
                .Where(x => x.attribute.Length == 1)
                .Select(x => (x.type, attribute: (HalInterfaceAttribute)x.attribute[0]));
            foreach (var type in types)
            {
                // Find the field, and find the class
                var interfaceType = type.attribute.InterfaceType;
                var loadedInterface = loader.LoadNativeInterface(interfaceType);
                if (loadedInterface == null)
                {
                    // TODO: Proper errors
                    continue;
                }
                var fields = type.type.GetFields(BindingFlags.NonPublic | BindingFlags.Static).Where(x => x.FieldType == interfaceType);
                foreach (var field in fields)
                {
                    field.SetValue(null, loadedInterface);
                }
                ;
            }

            ;
        }

        public static bool Initialize()
        {
            // Load natives first
            var nativeLoader = new NativeLibraryLoader();

            string[] commandArgs = Environment.GetCommandLineArgs();
            foreach (var commandArg in commandArgs)
            {
                //search for a line with the prefix "-ntcore:"
                if (commandArg.ToLower().Contains("-hal:"))
                {
                    //Split line to get the library.
                    int splitLoc = commandArg.IndexOf(':');
                    string file = commandArg.Substring(splitLoc + 1);

                    //If the file exists, just return it so dlopen can load it.
                    if (File.Exists(file))
                    {
                        nativeLoader.LoadNativeLibrary<HalInterfaceAttribute>(file, true);
                        InitializeInterfaces(nativeLoader);
                        return true;
                    }
                }
            }

            if (nativeLoader.TryLoadNativeLibraryPath("wpiHalJni"))
            {
                InitializeInterfaces(nativeLoader);
                return true;
            }

            return false;
        }
    }
}
