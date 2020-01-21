using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using WPIUtil.NativeUtilities;

namespace WPIUtil.ILGeneration
{
    public class InterfaceGenerator
    {
        private readonly IFunctionPointerLoader functionPointerLoader;
        private readonly IILGenerator ilGenerator;

        public InterfaceGenerator(IFunctionPointerLoader functionPointerLoader, IILGenerator ilGenerator)
        {
            this.functionPointerLoader = functionPointerLoader;
            this.ilGenerator = ilGenerator;
        }
        
        public T? GenerateImplementation<T>() where T : class
        {
            return (T?)GenerateImplementation(typeof(T));
        }

        public object? GenerateImplementation(Type t)
        {
            AssemblyBuilder asmBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(t.Name + "Asm"), AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = asmBuilder.DefineDynamicModule(t.Name + "Module");
            TypeBuilder typeBuilder = moduleBuilder.DefineType("Default" + t.Name);
            typeBuilder.AddInterfaceImplementation(t);

            var methods = t.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            foreach (var method in methods)
            {
                var parameters = method.GetParameters().Select(x => x.ParameterType).ToArray();
                var methodBuilder = typeBuilder.DefineMethod(method.Name, MethodAttributes.Virtual | MethodAttributes.Public, method.ReturnType, parameters);
                var nativeCallAttribute = method.GetCustomAttribute<NativeNameAttribute>();
                string nativeName = method.Name;
                if (nativeCallAttribute != null && nativeCallAttribute.NativeName != null)
                {
                    nativeName = nativeCallAttribute.NativeName;
                }
                ilGenerator.GenerateMethod(methodBuilder.GetILGenerator(), methodBuilder.ReturnType, parameters, functionPointerLoader.GetProcAddress(nativeName), true);
            }

            var typeInfo = typeBuilder.CreateTypeInfo();

            return typeInfo?.GetConstructor(new Type[0])?.Invoke(null);
        }
    }
}
