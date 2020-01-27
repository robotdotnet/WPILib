using Hal;
using REV.SparkMax.Natives;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using WPILib;

namespace desktopDev
{
    public class Robot : TimedRobot
    {
        public override void RobotPeriodic()
        {
            base.RobotPeriodic();
        }
    }

    class Program
    {


        static void Main(string[] args)
        {
            var types = typeof(CANAPI).Assembly.GetTypes().Where(x => x.Namespace == "Hal.Natives").Where(x => x.IsInterface);

            foreach (var type in types)
            {
                StringBuilder builder = new StringBuilder();
                string className = type.Name.Substring(1);
                builder.Append(@$"
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{{
    [NativeInterface(typeof({type.Name}))]
    public unsafe static class {className}
    {{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static {type.Name} lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

");

                foreach (var method in type.GetMethods())
                {
                    if (method.ReturnType == typeof(void))
                    {
                        builder.Append("public static void ");
                    }
                    else
                    {
                        builder.Append($"public static {method.ReturnType.Name} ");
                    }
                    builder.Append($"{method.Name.Replace("HAL_", "").Replace(className, "")}(");

                    bool first = true;

                    foreach (var p in method.GetParameters())
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            builder.Append(", ");
                        }
                        builder.Append($"{p.ParameterType.Name} {p.Name}");
                    }

                    builder.AppendLine(")");
                    builder.AppendLine("{");

                    if (method.ReturnType != typeof(void))
                    {
                        builder.Append("return ");
                    }

                    builder.Append($"lowLevel.{method.Name}(");

                    first = true;

                    foreach (var p in method.GetParameters())
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            builder.Append(", ");
                        }
                        builder.Append(p.Name);
                    }

                    builder.AppendLine(");");

                    builder.AppendLine("}");
                    builder.AppendLine();
                }



                builder.AppendLine("}");
                builder.AppendLine("}");


                Directory.CreateDirectory("classes");
                File.WriteAllText($"classes\\{className}.cs", builder.ToString());
            }
            ;
        }
    }
}
