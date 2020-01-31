using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace WPIUtil.ILGeneration
{
    public interface IILGenerator
    {
        void GenerateMethod(ILGenerator generator, Type returnType, Type[] parameters, IntPtr nativeFp, bool isStaticMethod = false);

        void GenerateMethodLastParameterStatusCheck(ILGenerator generator, Type returnType, Type[] parameters, IntPtr nativeFp, MethodInfo checkFunction, bool isStaticMethod = false);

        void GenerateMethodReturnStatusCheck(ILGenerator generator, Type[] parameters, IntPtr nativeFp, MethodInfo checkFunction, bool isStaticMethod = false);
    }
}
