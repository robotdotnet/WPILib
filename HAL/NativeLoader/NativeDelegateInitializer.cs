using System.Reflection;
using System.Runtime.InteropServices;

namespace HAL.NativeLoader
{
    /// <summary>
    /// This class contains methods to initialize delegates 
    /// </summary>
    public static class NativeDelegateInitializer
    {
        /// <summary>
        /// Sets up all native delegate in the type passed as the generic parameter
        /// </summary>
        /// <typeparam name="T">The type to setup the native delegates in</typeparam>
        /// <param name="library">The object containing the native library to load from</param>
        public static void SetupNativeDelegates<T>(ILibraryInformation library)
        {
            TypeInfo info = typeof(T).GetTypeInfo();
#if NETSTANDARD
            MethodInfo getDelegateForFunctionPointer =
                typeof(Marshal).GetTypeInfo().GetMethods(BindingFlags.Static | BindingFlags.Public)
                .First(m => m.Name == "GetDelegateForFunctionPointer" && m.IsGenericMethod);
#endif
            foreach (FieldInfo field in info.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attribute = (NativeDelegateAttribute)field.GetCustomAttribute(typeof(NativeDelegateAttribute));
                if (attribute == null) continue;
                string nativeName = attribute.NativeName ?? field.Name;
#if NETSTANDARD
                MethodInfo delegateGetter = getDelegateForFunctionPointer.MakeGenericMethod(field.FieldType);
                object setVal = delegateGetter.Invoke(null, new object[] { library.LibraryLoader.GetProcAddress(nativeName) });
#else
                object setVal = Marshal.GetDelegateForFunctionPointer(library.LibraryLoader.GetProcAddress(nativeName),
                    field.FieldType);
#endif
                field.SetValue(null, setVal);
            }
        }
    }
}
