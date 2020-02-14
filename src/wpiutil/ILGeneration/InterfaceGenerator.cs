using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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

        private static MethodInfo FindStatusCheckRangeMethod(StatusCheckRangeAttribute statusCheckRangeAttribute)
        {
            MethodInfo? statusCheckMethod = statusCheckRangeAttribute.RangeCheckType.GetMethod(statusCheckRangeAttribute.RangeCheckFunctionName, BindingFlags.Public | BindingFlags.Static);
            if (statusCheckMethod == null)
            {
                throw new StatusCheckMethodNotFoundException("Method Not Found", statusCheckRangeAttribute.RangeCheckType, statusCheckRangeAttribute.RangeCheckFunctionName);
            }
            var parameters = statusCheckMethod.GetParameters();
            if (parameters.Length != 2 || parameters[0].ParameterType != typeof(int))
            {
                throw new StatusCheckMethodNotFoundException("Method incompatible, must have 2 parameters, taking the first as an int.", statusCheckRangeAttribute.RangeCheckType, statusCheckRangeAttribute.RangeCheckFunctionName);
            }
            return statusCheckMethod;
        }

        private static MethodInfo FindStatusCheckMethod(StatusCheckedByAttribute statusCheckedByAttribute)
        {
            if (string.IsNullOrEmpty(statusCheckedByAttribute.StatusCheckFunctionName))
            {
                // Find attributed method
                MethodInfo? statusCheckMethod = null;
                foreach (var method in statusCheckedByAttribute.StatusCheckType.GetMethods(BindingFlags.Public | BindingFlags.Static))
                {
                    if (method.GetCustomAttribute<StatusCheckFunctionAttribute>() != null)
                    {
                        if (statusCheckMethod != null)
                        {
                            throw new AmbiguousMatchException("Multiple Status Matching Methods Found");
                        }
                        statusCheckMethod = method;
                    }
                }

                if (statusCheckMethod == null)
                {
                    throw new StatusCheckMethodNotFoundException("Status Check Attributed Method Not Found", statusCheckedByAttribute.StatusCheckType, "");
                }
                var parameters = statusCheckMethod.GetParameters();
                if (parameters.Length != 1 || parameters[0].ParameterType != typeof(int))
                {
                    throw new StatusCheckMethodNotFoundException("Method incompatible, must take 1 int as it's only parameter.", statusCheckedByAttribute.StatusCheckType, statusCheckMethod.Name);
                }
                return statusCheckMethod;
            }
            else
            {
                MethodInfo? statusCheckMethod = statusCheckedByAttribute.StatusCheckType.GetMethod(statusCheckedByAttribute.StatusCheckFunctionName, BindingFlags.Public | BindingFlags.Static);
                if (statusCheckMethod == null)
                {
                    throw new StatusCheckMethodNotFoundException("Method Not Found", statusCheckedByAttribute.StatusCheckType, statusCheckedByAttribute.StatusCheckFunctionName);
                }
                var parameters = statusCheckMethod.GetParameters();
                if (parameters.Length != 1 || parameters[0].ParameterType != typeof(int))
                {
                    throw new StatusCheckMethodNotFoundException("Method incompatible, must take 1 int as its only parameter.", statusCheckedByAttribute.StatusCheckType, statusCheckedByAttribute.StatusCheckFunctionName);
                }
                return statusCheckMethod;
            }
        }

        private static MethodInfo? FindStatusCheckMethod(Type type)
        {
            StatusCheckedByAttribute? statusCheckedByAttribute = type.GetCustomAttribute<StatusCheckedByAttribute>();
            if (statusCheckedByAttribute == null)
            {
                return null;
            }

            return FindStatusCheckMethod(statusCheckedByAttribute);
        }

        public object?[] GenerateImplementations(Type[] types)
        {
            if (types == null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            if (types.Length == 0) return Array.Empty<object>();

            object?[] toRet = new object?[types.Length];

            var asmBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(types[0].Name + "Asm"), AssemblyBuilderAccess.Run);
            var moduleBuilder = asmBuilder.DefineDynamicModule(types[0].Name + "Module");


            // Generate a type for containing our action.

            int count = 0;
            foreach (var t in types)
            {
                var typeBuilder = moduleBuilder.DefineType("Default" + t.Name);
                typeBuilder.AddInterfaceImplementation(t);

                // Check if interface is globally checked by something. If so, load the checked by method ref
                var statusCheckMethod = FindStatusCheckMethod(t);

                var methods = t.GetMethods(BindingFlags.Public | BindingFlags.Instance);

                foreach (var method in methods.Where(x => x.IsAbstract))
                {
                    var parameters = method.GetParameters().Select(x => x.ParameterType).ToArray();
                    var methodBuilder = typeBuilder.DefineMethod(method.Name, MethodAttributes.Virtual | MethodAttributes.Public, method.ReturnType, parameters);
                    methodBuilder.SetImplementationFlags(MethodImplAttributes.AggressiveInlining);
                    var nativeCallAttribute = method.GetCustomAttribute<NativeNameAttribute>();
                    string nativeName = method.Name;
                    if (nativeCallAttribute != null && nativeCallAttribute.NativeName != null)
                    {
                        nativeName = nativeCallAttribute.NativeName;
                    }


                    MethodInfo? localStatusCheckMethod = statusCheckMethod;

                    StatusCheckedByAttribute? localStatusCheckAttribute = method.GetCustomAttribute<StatusCheckedByAttribute>();
                    if (localStatusCheckAttribute != null)
                    {
                        localStatusCheckMethod = FindStatusCheckMethod(localStatusCheckAttribute);
                    }

                    StatusCheckRangeAttribute? statusCheckRangeAttribute = method.GetCustomAttribute<StatusCheckRangeAttribute>();

                    // Check to see if function has status check attributes
                    if (method.GetCustomAttribute<StatusCheckLastParameterAttribute>() != null)
                    {
                        if (localStatusCheckMethod == null)
                        {
                            throw new StatusCheckMethodNotDefinedException(t, method.Name);
                        }
                        ilGenerator.GenerateMethodLastParameterStatusCheck(methodBuilder.GetILGenerator(), methodBuilder.ReturnType, parameters, functionPointerLoader.GetProcAddress(nativeName), localStatusCheckMethod);
                    }
                    else if (method.GetCustomAttribute<StatusCheckReturnValueAttribute>() != null)
                    {
                        if (localStatusCheckMethod == null)
                        {
                            throw new StatusCheckMethodNotDefinedException(t, method.Name);
                        }
                        ilGenerator.GenerateMethodReturnStatusCheck(methodBuilder.GetILGenerator(), parameters, functionPointerLoader.GetProcAddress(nativeName), localStatusCheckMethod);
                    }
                    else if (statusCheckRangeAttribute != null)
                    {
                        localStatusCheckMethod = FindStatusCheckRangeMethod(statusCheckRangeAttribute);
                        ilGenerator.GenerateMethodRangeStatusCheck(methodBuilder.GetILGenerator(), methodBuilder.ReturnType, parameters, functionPointerLoader.GetProcAddress(nativeName), localStatusCheckMethod, statusCheckRangeAttribute.RangeParameterNumber);
                    }
                    else
                    {
                        ilGenerator.GenerateMethod(methodBuilder.GetILGenerator(), methodBuilder.ReturnType, parameters, functionPointerLoader.GetProcAddress(nativeName));
                    }


                }

                var typeInfo = typeBuilder.CreateTypeInfo();

                toRet[count] = typeInfo?.GetConstructor(Array.Empty<Type>())?.Invoke(null);
                count++;
            }


            return toRet;
        }
    }
}
