using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;

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

            generator.Emit(OpCodes.Ldloc_0);

            generator.Emit(OpCodes.Call, checkFunction);

            generator.Emit(OpCodes.Ret);
        }

        public unsafe void GenerateMethodReturnStatusCheck(ILGenerator generator, Type[] parameters, IntPtr nativeFp, MethodInfo checkFunction, bool isStaticMethod = false)
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

            emitCalli(generator, OpCodes.Calli, CallingConvention.Cdecl, typeof(int), parameters);

            generator.EmitCall(OpCodes.Call, checkFunction, null);

            generator.Emit(OpCodes.Ret);
        }
    }
}
