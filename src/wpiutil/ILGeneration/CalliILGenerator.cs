using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace WPIUtil.ILGeneration
{
    /// <summary>
    /// Generator for generating CallI IL calls
    /// </summary>
    public class CalliILGenerator : IILGenerator
    {
        private delegate void EmitCalliDelegate(ILGenerator generator, OpCode code, CallingConvention convention, Type returnType, Type[] parameterTypes);

        private readonly EmitCalliDelegate emitCalli;

        /// <summary>
        /// Construct a new Calli il generator
        /// </summary>
        public CalliILGenerator()
        {
            var emitMethod = typeof(ILGenerator).GetMethod(nameof(ILGenerator.EmitCalli), new Type[] { typeof(OpCode), typeof(CallingConvention), typeof(Type), typeof(Type[]) });

            var tmp = emitMethod?.CreateDelegate(typeof(EmitCalliDelegate)) ?? throw new PlatformNotSupportedException("This platform does not support calli IL Generation");
            emitCalli = (EmitCalliDelegate)tmp;
        }

        /// <summary>
        /// Generate a native calling method
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="returnType"></param>
        /// <param name="parameters"></param>
        /// <param name="nativeFp"></param>
        /// <param name="isStaticMethod"></param>
        public unsafe void GenerateMethod(ILGenerator generator, Type returnType, Type[] parameters, IntPtr nativeFp, bool isStaticMethod = false)
        {
            int offset = isStaticMethod ? 0 : 1;
            for (int i = 0; i < parameters.Length; i++)
            {
                generator.Emit(OpCodes.Ldarg, i + offset);
            }
            if (sizeof(IntPtr) == 8)
            {
                generator.Emit(OpCodes.Ldc_I8, (long)nativeFp);
            }
            else
            {
                generator.Emit(OpCodes.Ldc_I4, (int)nativeFp);
            }

            emitCalli(generator, OpCodes.Calli, CallingConvention.Cdecl, returnType, parameters);

            generator.Emit(OpCodes.Ret);
        }

        public unsafe void GenerateMethodLastParameterStatusCheck(ILGenerator generator, Type returnType, Type[] parameters, IntPtr nativeFp, MethodInfo checkFunction, bool isStaticMethod = false)
        {
            // Insert hidden last parameter
            generator.DeclareLocal(typeof(int));
            generator.Emit(OpCodes.Ldc_I4_0);
            generator.Emit(OpCodes.Stloc_0);

            int offset = isStaticMethod ? 0 : 1;

            for (int i = 0; i < parameters.Length; i++)
            {
                generator.Emit(OpCodes.Ldarg, i + offset);
            }

            generator.Emit(OpCodes.Ldloca_S, (byte)0);

            if (sizeof(IntPtr) == 8)
            {
                generator.Emit(OpCodes.Ldc_I8, (long)nativeFp);
            }
            else
            {
                generator.Emit(OpCodes.Ldc_I4, (int)nativeFp);
            }

            var adjustedParameters = new Type[parameters.Length + 1];
            Array.Copy(parameters, adjustedParameters, parameters.Length);
            adjustedParameters[adjustedParameters.Length - 1] = typeof(int*);

            emitCalli(generator, OpCodes.Calli, CallingConvention.Cdecl, returnType, adjustedParameters);

            var retLabel = generator.DefineLabel();
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Brfalse_S, retLabel);

            generator.Emit(OpCodes.Ldloc_0);

            var checkFunctionParameters = checkFunction.GetParameters();

            if (checkFunctionParameters.Length != 1 || checkFunctionParameters[0].ParameterType != typeof(int))
            {
                throw new MethodGenerationException("Incompatible method for status check");
            }

            generator.Emit(OpCodes.Call, checkFunction);

            generator.MarkLabel(retLabel);
            generator.Emit(OpCodes.Ret);
        }

        public unsafe void GenerateMethodReturnStatusCheck(ILGenerator generator, Type[] parameters, IntPtr nativeFp, MethodInfo checkFunction, bool isStaticMethod = false)
        {
            generator.DeclareLocal(typeof(int));

            int offset = isStaticMethod ? 0 : 1;
            for (int i = 0; i < parameters.Length; i++)
            {
                generator.Emit(OpCodes.Ldarg, i + offset);
            }

            if (sizeof(IntPtr) == 8)
            {
                generator.Emit(OpCodes.Ldc_I8, (long)nativeFp);
            }
            else
            {
                generator.Emit(OpCodes.Ldc_I4, (int)nativeFp);
            }

            emitCalli(generator, OpCodes.Calli, CallingConvention.Cdecl, typeof(int), parameters);

            var checkFunctionParameters = checkFunction.GetParameters();

            if (checkFunctionParameters.Length != 1 || checkFunctionParameters[0].ParameterType != typeof(int))
            {
                throw new MethodGenerationException("Incompatible method for status check");
            }

            var retLabel = generator.DefineLabel();
            generator.Emit(OpCodes.Stloc_0);
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Brfalse_S, retLabel);

            generator.Emit(OpCodes.Ldloc_0);
            generator.EmitCall(OpCodes.Call, checkFunction, null);

            generator.MarkLabel(retLabel);
            generator.Emit(OpCodes.Ret);
        }

        public unsafe void GenerateMethodRangeStatusCheck(ILGenerator generator, Type returnType, Type[] parameters, IntPtr nativeFp, MethodInfo checkFunction, int checkParameterNumber, bool isStaticMethod = false)
        {
            // Insert hidden last parameter
            generator.DeclareLocal(typeof(int));
            generator.Emit(OpCodes.Ldc_I4_0);
            generator.Emit(OpCodes.Stloc_0);

            int offset = isStaticMethod ? 0 : 1;

            for (int i = 0; i < parameters.Length; i++)
            {
                generator.Emit(OpCodes.Ldarg, i + offset);
            }

            generator.Emit(OpCodes.Ldloca_S, (byte)0);

            if (sizeof(IntPtr) == 8)
            {
                generator.Emit(OpCodes.Ldc_I8, (long)nativeFp);
            }
            else
            {
                generator.Emit(OpCodes.Ldc_I4, (int)nativeFp);
            }

            var adjustedParameters = new Type[parameters.Length + 1];
            Array.Copy(parameters, adjustedParameters, parameters.Length);
            adjustedParameters[adjustedParameters.Length - 1] = typeof(int*);

            emitCalli(generator, OpCodes.Calli, CallingConvention.Cdecl, returnType, adjustedParameters);

            var retLabel = generator.DefineLabel();
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Brfalse_S, retLabel);

            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ldarg, checkParameterNumber + offset);

            var checkFunctionParameters = checkFunction.GetParameters();

            if (checkFunctionParameters.Length != 2 || checkFunctionParameters[0].ParameterType != typeof(int) || checkFunctionParameters[1].ParameterType != parameters[checkParameterNumber])
            {
                throw new MethodGenerationException("Incompatible method for status check");
            }

            generator.Emit(OpCodes.Call, checkFunction);

            generator.MarkLabel(retLabel);
            generator.Emit(OpCodes.Ret);

        }
    }
}
