using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WPIUtil.ILGeneration;

namespace WPIUtil.NativeUtilities
{
    public static class NativeInterfaceInitializer
    {
        public static bool LoadAndInitializeNativeTypes(Assembly asm, string nativeLibraryName)
        {
            var generator = NativeLibraryLoader.LoadNativeLibraryGenerator(nativeLibraryName);
            if (generator == null)
            {
                return false;
            }

            InitializeNativeTypes(asm, generator);

            return true;
        }

        public static void InitializeNativeTypes(Assembly asm, InterfaceGenerator generator)
        {
            var types = asm.GetTypes()
                .Select(x => (type: x, attribute: x.GetCustomAttributes(typeof(NativeInterfaceAttribute), false)))
                .Where(x => x.attribute.Length == 1)
                .Select(x => (x.type, attribute: (NativeInterfaceAttribute)x.attribute[0]));
            foreach (var type in types)
            {
                // Find the field, and find the class
                var interfaceType = type.attribute.InterfaceType;
                
                var loadedInterface = generator.GenerateImplementation(interfaceType);
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
        }
    }
}
