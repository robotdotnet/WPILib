using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using NetworkTables.Natives;
using WPIUtil.ILGeneration;
using WPIUtil.NativeUtilities;

namespace CalliGenerator
{
    class Program
    {
       // private unsafe delegate* unmanaged[Cdecl]<NetworkTables.Natives.NtEntryNotification*, System.UIntPtr, void> NT_DisposeEntryNotificationArray;



        public static string GetReturnType(MethodInfo method)
        {
            if (method.ReturnType == typeof(void))
            {
                return "void";
            }
            else
            {
                return method.ReturnType.ToString();
            }
        }

        public static void GenerateNoStatusMethod(MethodInfo method, StringBuilder builder, List<string> inits)
        {
            string GenerateSignature(MethodInfo method)
            {
                StringBuilder builder = new StringBuilder();

                builder.Append("delegate* unmanaged[Cdecl]<");

                foreach (var param in method.GetParameters())
                {
                    builder.Append(param.ParameterType.ToString());
                    builder.Append(", ");
                }
                builder.Append(GetReturnType(method));
                builder.Append(">");
                return builder.ToString();
            }

            inits.Add($"{method.Name}Func = ({GenerateSignature(method)})loader.GetProcAddress(\"{method.Name}\");");

            builder.AppendLine($"private {GenerateSignature(method)} {method.Name}Func;");
            builder.AppendLine();

            builder.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            builder.Append($"public {GetReturnType(method)} {method.Name}(");
            bool first = true;
            foreach (var param in method.GetParameters())
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder.Append(", ");
                }
                builder.Append(param.ParameterType.ToString());
                builder.Append(" ");
                builder.Append(param.Name);
            }
            builder.AppendLine(")");
            builder.AppendLine("{");

            if (method.ReturnType != typeof(void))
            {
                builder.Append("return ");

            }

            builder.Append($"{method.Name}Func(");

            first = true;
            foreach (var param in method.GetParameters())
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder.Append(", ");
                }
                builder.Append(param.Name);
            }

            builder.AppendLine(");");

            builder.AppendLine("}");
        }

        public static void GenerateStatusCheckLastParameterMethod(MethodInfo method, StringBuilder builder, List<string> inits)
        {
            string GenerateSignature(MethodInfo method)
            {
                StringBuilder builder = new StringBuilder();

                builder.Append("delegate* unmanaged[Cdecl]<");

                foreach (var param in method.GetParameters())
                {
                    builder.Append(param.ParameterType.ToString());
                    builder.Append(", ");
                }
                builder.Append("int*, ");
                builder.Append(GetReturnType(method));
                builder.Append(">");
                return builder.ToString();
            }

            inits.Add($"{method.Name}Func = ({GenerateSignature(method)})loader.GetProcAddress(\"{method.Name}\");");

            builder.AppendLine($"private {GenerateSignature(method)} {method.Name}Func;");
            builder.AppendLine();

            builder.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            builder.Append($"public {GetReturnType(method)} {method.Name}(");
            var parameters = method.GetParameters();
            bool first = true;
            foreach (var param in parameters)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder.Append(", ");
                }
                builder.Append(param.ParameterType.ToString());
                builder.Append(" ");
                builder.Append(param.Name);
            }
            builder.AppendLine(")");
            builder.AppendLine("{");

            builder.AppendLine("int status = 0;");

            if (method.ReturnType != typeof(void))
            {
                builder.Append("var retVal = ");
            }

            builder.Append($"{method.Name}Func(");

            first = true;
            foreach (var param in parameters)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder.Append(", ");
                }
                builder.Append(param.Name);
            }

            if (parameters.Length == 0)
            {
                builder.Append("&status");
            }
            else
            {
                builder.Append(", &status");
            }

            builder.AppendLine(");");
            builder.AppendLine("StatusCheck(status);");

            if (method.ReturnType != typeof(void))
            {
                builder.AppendLine("return retVal;");
            }

            builder.AppendLine("}");
        }

        public static void GenerateStatusCheckReturnMethod(MethodInfo method, StringBuilder builder, List<string> inits)
        {
            if (method.ReturnType == typeof(void))
            {
                throw new InvalidOperationException("Cannot return status check void");

            }

            string GenerateSignature(MethodInfo method)
            {
                StringBuilder builder = new StringBuilder();

                builder.Append("delegate* unmanaged[Cdecl]<");

                foreach (var param in method.GetParameters())
                {
                    builder.Append(param.ParameterType.ToString());
                    builder.Append(", ");
                }
                builder.Append("int*>");
                return builder.ToString();
            }

            inits.Add($"{method.Name}Func = ({GenerateSignature(method)})loader.GetProcAddress(\"{method.Name}\");");

            builder.AppendLine($"private {GenerateSignature(method)} {method.Name}Func;");
            builder.AppendLine();

            builder.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            builder.Append($"public {GetReturnType(method)} {method.Name}(");
            bool first = true;
            foreach (var param in method.GetParameters())
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder.Append(", ");
                }
                builder.Append(param.ParameterType.ToString());
                builder.Append(" ");
                builder.Append(param.Name);
            }
            builder.AppendLine(")");
            builder.AppendLine("{");

            

            builder.Append($"var statusVal = {method.Name}Func(");

            first = true;
            foreach (var param in method.GetParameters())
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder.Append(", ");
                }
                builder.Append(param.Name);
            }

            builder.AppendLine(");");
            builder.AppendLine("StatusCheck(statusVal);");

            builder.AppendLine("}");
        }

        public static void GenerateStatusCheckRangeMethod(MethodInfo method, StringBuilder builder, StatusCheckRangeAttribute range, List<string> inits)
        {
            string GenerateSignature(MethodInfo method)
            {
                StringBuilder builder = new StringBuilder();

                builder.Append("delegate* unmanaged[Cdecl]<");

                foreach (var param in method.GetParameters())
                {
                    builder.Append(param.ParameterType.ToString());
                    builder.Append(", ");
                }
                builder.Append("int*, ");
                builder.Append(GetReturnType(method));
                builder.Append(">");
                return builder.ToString();
            }

            inits.Add($"{method.Name}Func = ({GenerateSignature(method)})loader.GetProcAddress(\"{method.Name}\");");

            builder.AppendLine($"private {GenerateSignature(method)} {method.Name}Func;");
            builder.AppendLine();

            builder.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            builder.Append($"public {GetReturnType(method)} {method.Name}(");
            var parameters = method.GetParameters();
            bool first = true;
            foreach (var param in parameters)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder.Append(", ");
                }
                builder.Append(param.ParameterType.ToString());
                builder.Append(" ");
                builder.Append(param.Name);
            }
            builder.AppendLine(")");
            builder.AppendLine("{");

            builder.AppendLine("int status = 0;");

            if (method.ReturnType != typeof(void))
            {
                builder.Append("var retVal = ");
            }

            builder.Append($"{method.Name}Func(");

            first = true;
            foreach (var param in parameters)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder.Append(", ");
                }
                builder.Append(param.Name);
            }

            if (parameters.Length == 0)
            {
                builder.Append("&status");
            }
            else
            {
                builder.Append(", &status");
            }

            builder.AppendLine(");");
            builder.AppendLine($"{range.RangeCheckFunctionName}(status, {parameters[range.RangeParameterNumber].Name});");

            if (method.ReturnType != typeof(void))
            {
                builder.AppendLine("return retVal;");
            }

            builder.AppendLine("}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GenerateForAssembly(Assembly asm)
        {
            var types = asm.GetTypes()
                .Select(x => (type: x, attribute: x.GetCustomAttribute<NativeInterfaceAttribute>()))
                .Where(x => x.attribute is not null)
                .ToArray();

            var typesActual = types.Select(x => x.attribute!.InterfaceType).ToArray();

            var folderName = asm.GetName().Name! + "Files";

            Directory.CreateDirectory(folderName);

            foreach (var t in types)
            {
                StringBuilder builder = new StringBuilder();

                builder.AppendLine("using WPIUtil.ILGeneration;");
                builder.AppendLine("using System.Runtime.CompilerServices;");

                builder.AppendLine($"namespace {t.attribute!.InterfaceType.Namespace}");
                builder.AppendLine("{");

                builder.AppendLine($"public unsafe class {t.type.Name}Native : {t.attribute!.InterfaceType.Name}");
                builder.AppendLine("{");

                var methods = t.attribute!.InterfaceType.GetMethods(BindingFlags.Public | BindingFlags.Instance);

                List<string> inits = new List<string>();

                foreach (var method in methods.Where(x => x.IsAbstract))
                {
                    var statusCheckRange = method.GetCustomAttribute<StatusCheckRangeAttribute>();
                    var statusCheckRet = method.GetCustomAttribute<StatusCheckReturnValueAttribute>();
                    var statusCheckLastParam = method.GetCustomAttribute<StatusCheckLastParameterAttribute>();
                    if (statusCheckRange != null)
                    {
                        GenerateStatusCheckRangeMethod(method, builder, statusCheckRange, inits);
                    }
                    else if (statusCheckRet != null)
                    {
                        GenerateStatusCheckReturnMethod(method, builder, inits);
                    }
                    else if (statusCheckLastParam != null)
                    {
                        GenerateStatusCheckLastParameterMethod(method, builder, inits);
                    }
                    else
                    {
                        GenerateNoStatusMethod(method, builder, inits);
                    }
                    builder.AppendLine();
                    builder.AppendLine();
                }
                builder.AppendLine();

                builder.AppendLine($"public {t.type.Name}Native(IFunctionPointerLoader loader)");
                builder.AppendLine("{");
                builder.AppendLine("if (loader == null)");
                builder.AppendLine("{");
                builder.AppendLine("throw new ArgumentNullException(nameof(loader));");
                builder.AppendLine("}");
                builder.AppendLine();

                foreach (var method in inits)
                {
                    builder.AppendLine(method);
                }

                builder.AppendLine("}");
                builder.AppendLine();

                builder.AppendLine("}");

                builder.AppendLine("}");

                File.WriteAllText(Path.Join(folderName, t.type.Name + "Native.cs"), builder.ToString().Replace("System.Void", "void"));
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GenerateForAssembly(typeof(REV.SparkMax.CANSparkMaxDriver).Assembly);
        }
    }
}

