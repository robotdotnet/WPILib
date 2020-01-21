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
        private delegate void GetMethodDelegate(ILGenerator generator, OpCode code, CallingConvention convention, Type returnType, Type[] parameterTypes);

        private readonly GetMethodDelegate getMethod;

        /// <summary>
        /// Construct a new Calli il generator
        /// </summary>
        public CalliILGenerator()
        {
            var emitMethod = typeof(ILGenerator).GetMethod(nameof(ILGenerator.EmitCalli), new Type[] { typeof(OpCode), typeof(CallingConvention), typeof(Type), typeof(Type[]) });

            var tmp = emitMethod?.CreateDelegate(typeof(GetMethodDelegate)) ?? throw new PlatformNotSupportedException("This platform does not support calli IL Generation");
            getMethod = (GetMethodDelegate)tmp;
        }

        /// <summary>
        /// Generate a native calling method
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="returnType"></param>
        /// <param name="parameters"></param>
        /// <param name="nativeFp"></param>
        /// <param name="isInstance"></param>
        public unsafe void GenerateMethod(ILGenerator generator, Type returnType, Type[] parameters, IntPtr nativeFp, bool isInstance = false)
        {
            if (isInstance)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    generator.Emit(OpCodes.Ldarg, i + 1);
                }
            }
            else
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    generator.Emit(OpCodes.Ldarg, i);
                }
            }
            if (sizeof(IntPtr) == 8)
            {
                generator.Emit(OpCodes.Ldc_I8, (long)nativeFp);
            } else
            {
                generator.Emit(OpCodes.Ldc_I4, (int)nativeFp);
            }

            getMethod(generator, OpCodes.Calli, CallingConvention.Cdecl, returnType, parameters);

            generator.Emit(OpCodes.Ret);
        }
    }
}
