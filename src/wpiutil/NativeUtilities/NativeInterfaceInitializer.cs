using System;
using System.IO;
using System.Linq;
using System.Reflection;
using WPIUtil.ILGeneration;

namespace WPIUtil.NativeUtilities
{
    public static class NativeInterfaceInitializer
    {
        public static void LoadAndInitializeNativeTypes(Assembly asm, string nativeLibraryName, out InterfaceGenerator? generator)
        {
            generator = NativeLibraryLoader.LoadNativeLibraryGenerator(nativeLibraryName);
            if (generator == null)
            {
                throw new Exception("Library Load Exception");
            }

            InitializeNativeTypes(asm, generator);
        }

        public static void InitializeNativeTypes(Assembly asm, InterfaceGenerator generator)
        {
            bool isRoboRIO = File.Exists("/usr/local/frc/bin/frcRunRobot.sh");

            var types = asm.GetTypes()
                .Select(x => (type: x, attribute: x.GetCustomAttribute<NativeInterfaceAttribute>(), skipAttribute: x.GetCustomAttribute<SkipOnRoboRIOAttribute>()))
                .Where(x => x.attribute != null)
                .Where(x => !isRoboRIO || (isRoboRIO && x.skipAttribute == null))
                .ToArray();

            var typesActual = types.Select(x => x.attribute!.InterfaceType).ToArray();

            var interfaces = generator.GenerateImplementations(typesActual);
            for (int i = 0; i < types.Length; i++)
            {
                var type = types[i];
                // Find the field, and find the class
                var interfaceType = type.attribute!.InterfaceType;

                var loadedInterface = interfaces[i];
                if (loadedInterface == null)
                {
                    // TODO: Proper errors
                    continue;
                }
                var fields = type.type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).Where(x => x.FieldType == interfaceType);
                foreach (var field in fields)
                {
                    field.SetValue(null, loadedInterface);
                }
                ;
            }
        }
    }
}
