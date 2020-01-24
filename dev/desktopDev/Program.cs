using REV.SparkMax.Natives;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace desktopDev
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var methods = typeof(ICANSparkMaxDriver).GetMethods();

            StringBuilder builder = new StringBuilder();

            foreach (var method in methods)
            {
                if (method.ReturnType == typeof(void))
                {
                    WriteVoidMethod(method, builder);
                }
                else
                {
                    WriteReturningMethod(method, builder);
                }
            }
            File.WriteAllText("methods.txt", builder.ToString());
            ;
        }

        static string ToCSharpName(Type type)
        {
            if (type == typeof(void*))
            {
                return "void*";
            }
            return type.Name;
        }

        static void WriteParameterList(MethodInfo info, StringBuilder builder)
        {
            bool first = true;
            foreach (var parameter in info.GetParameters())
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder.Append(", ");
                }

                builder.Append($"{ToCSharpName(parameter.ParameterType)} {parameter.Name}");
            }
            builder.AppendLine(")");
        }

        static void WriteCallParameterList(MethodInfo info, StringBuilder builder)
        {
            bool first = true;
            foreach (var parameter in info.GetParameters())
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    builder.Append(", ");
                }

                builder.Append($"{parameter.Name}");
            }
            builder.AppendLine(");");
        }

        static void WriteVoidMethod(MethodInfo info, StringBuilder builder)
        {
            builder.Append($"public static void {info.Name.Replace("c_SparkMax_", "")}(");
            WriteParameterList(info, builder);
            builder.Append("{");
            builder.AppendLine($"  driver.{info.Name}(");
            WriteCallParameterList(info, builder);

            builder.AppendLine("}");

        }

        static void WriteReturningMethod(MethodInfo info, StringBuilder builder)
        {
            builder.Append($"public static {ToCSharpName(info.ReturnType)} {info.Name.Replace("c_SparkMax_", "")}(");
            WriteParameterList(info, builder);



            builder.AppendLine("{");

            builder.Append($"  return driver.{info.Name}(");
            WriteCallParameterList(info, builder);

            builder.AppendLine("}");
        }
    }
}
