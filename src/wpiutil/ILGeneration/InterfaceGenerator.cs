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

        public object?[] GenerateImplementations(Type[] types, MethodInfo statusCheckFunc)
        {
            if (types.Length == 0) return Array.Empty<object>();

            object?[] toRet = new object?[types.Length];

            AssemblyBuilder asmBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(types[0].Name + "Asm"), AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = asmBuilder.DefineDynamicModule(types[0].Name + "Module");


            // Generate a type for containing our action.

            int count = 0;
            foreach (var t in types)
            {
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

                    // Check to see if function has status check attributes
                    if (method.GetCustomAttribute<StatusCheckLastParameterAttribute>() != null)
                    {
                        ilGenerator.GenerateMethodLastParameterStatusCheck(methodBuilder.GetILGenerator(), methodBuilder.ReturnType, parameters, functionPointerLoader.GetProcAddress(nativeName), statusCheckFunc);
                    }
                    else if (method.GetCustomAttribute<StatusCheckReturnValueAttribute>() != null)
                    {
                        ilGenerator.GenerateMethodReturnStatusCheck(methodBuilder.GetILGenerator(), parameters, functionPointerLoader.GetProcAddress(nativeName), statusCheckFunc);
                    }
                    else
                    {
                        ilGenerator.GenerateMethod(methodBuilder.GetILGenerator(), methodBuilder.ReturnType, parameters, functionPointerLoader.GetProcAddress(nativeName));
                    }


                }

                var typeInfo = typeBuilder.CreateTypeInfo();

                toRet[count] = typeInfo?.GetConstructor(new Type[0])?.Invoke(null);
                count++;
            }


            return toRet;
        }
    }
}
