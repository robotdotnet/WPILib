using System;
using System.Reflection;
using System.Reflection.Emit;

namespace WPIUtil.ILGeneration
{
    public interface IILGenerator
    {
        void GenerateMethod(ILGenerator generator, Type returnType, Type[] parameters, IntPtr nativeFp, bool isStaticMethod = false);

        void GenerateMethodLastParameterStatusCheck(ILGenerator generator, Type returnType, Type[] parameters, IntPtr nativeFp, MethodInfo checkFunction, bool isStaticMethod = false);

        void GenerateMethodReturnStatusCheck(ILGenerator generator, Type[] parameters, IntPtr nativeFp, MethodInfo checkFunction, bool isStaticMethod = false);

        void GenerateMethodRangeStatusCheck(ILGenerator generator, Type returnType, Type[] parameters, IntPtr nativeFp, MethodInfo checkFunction, int checkParameterNumber, bool isStaticMethod = false);
    }
}
