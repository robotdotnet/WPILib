using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CInterfaceGenerator
{
    class Program
    {
        class FunctionsJson
        {
            public string[] Files { get; set; } = new string[0];
            public Function[] Functions { get; set; } = new Function[0];
        }
        class Function
        {
            public string Name { get; set; } = "";
            public string? StatusCheck { get; set; }


        }

        class HeaderTuple
        {
            public string HeaderName { get; set; }
            public List<string> HeaderText { get; } = new List<string>();
        }

        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("You must pass a path to where the header files are located, and the file to parse");
                return;
            }

            var headerFiles = Directory.EnumerateFiles(args[0]);
            var functionsFile = args[1];

            var loadedFunctions = JsonConvert.DeserializeObject<FunctionsJson>(File.ReadAllText(functionsFile));
            var filesToCheck = headerFiles.Where(x => loadedFunctions.Files.Any(y => x.EndsWith(y)))
                .Select(x => (name: x, contents: File.ReadAllText(x)))
                .ToArray();

            List<string> generatedFunctions = new List<string>();

            Dictionary<string, List<string>> generatedInterfaces = new Dictionary<string, List<string>>();

            foreach (var functionToGenerate in loadedFunctions.Functions)
            {
                // Enumerate each header file looking for the function
                foreach (var (name, contents) in filesToCheck)
                {

                    if (contents.Contains(functionToGenerate.Name))
                    {
                        if (generatedFunctions.Contains(functionToGenerate.Name))
                        {
                            continue;
                        }
                        generatedFunctions.Add(functionToGenerate.Name);

                        var headerSpan = contents.AsSpan();
                        var functionStart = headerSpan.IndexOf(functionToGenerate.Name);
                        while (true)
                        {
                            if (headerSpan[functionStart] == '\n') break;
                            functionStart--;
                        }
                        // Go back to the start of the line 
                        var functionDef = headerSpan.Slice(functionStart);
                        functionDef = string.Join(' ', functionDef.Slice(0, functionDef.IndexOf(";")).ToString().Split("\n").Select(x => x.Trim()));

                        functionDef = GetCSharpDef(functionDef.ToString());

                        if (!generatedInterfaces.TryGetValue(Path.GetFileName(name), out var funcList))
                        {
                            funcList = new List<string>();
                            generatedInterfaces.Add(Path.GetFileName(name), funcList);
                        }

                        if (functionDef.EndsWith("status)"))
                        {

                            // Remove status, mark as a status check
                            if (functionDef.Contains(','))
                            {
                                funcList.Add($"[StatusCheckLastParameter] {functionDef.Slice(0, functionDef.LastIndexOf(',')).ToString()});");
                            }
                            else
                            {
                                funcList.Add($"[StatusCheckLastParameter] {functionDef.Slice(0, functionDef.LastIndexOf('(')).ToString()}();");
                            }



                        }
                        else
                        {
                            funcList.Add(functionDef.ToString() + ';');
                        }

                        ;
                    }
                }
            }

            var toWrite = WriteFiles(generatedInterfaces);


            foreach (var f in toWrite)
            {
                File.WriteAllText(f.fileName, f.toWrite);
            }

            ;
        }

        public static List<(string fileName, string toWrite)> WriteFiles(Dictionary<string, List<string>> files)
        {
            List<(string fileName, string toWrite)> outputFiles = new List<(string fileName, string toWrite)>();

            foreach (var file in files)
            {
                string interfaceName = $"I{file.Key.Replace(".h", "")}";
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("using WPIUtil.ILGeneration;\n\nnamespace Hal.Natives\n{");
                builder.AppendLine($"   public unsafe interface {interfaceName}\n    {{");

                foreach (var function in file.Value)
                {
                    builder.AppendLine($"        {function}\n");
                }


                builder.AppendLine("    }\n}");
                outputFiles.Add(($"{interfaceName}.cs", builder.ToString().Replace("\r\n", "\n")));
            }

            return outputFiles;
        }

        public static Dictionary<string, string> TypeMap = new Dictionary<string, string>()
        {
            ["int32_t"] = "int",
            ["int64_t"] = "long",
            ["int16_t"] = "short",
            ["uint8_t"] = "byte",
            ["char"] = "byte",
            ["const"] = "",
            ["HAL_Bool"] = "int",
            ["HAL_Handle"] = "int",
            ["HAL_PortHandle"] = "int",
            ["HAL_AnalogInputHandle"] = "int",
            ["HAL_AnalogOutputHandle"] = "int",
            ["HAL_AnalogTriggerHandle"] = "int",
            ["HAL_CompressorHandle"] = "int",
            ["HAL_CounterHandle"] = "int",
            ["HAL_DigitalHandle"] = "int",
            ["HAL_DigitalPWMHandle"] = "int",
            ["HAL_EncoderHandle"] = "int",
            ["HAL_FPGAEncoderHandle"] = "int",
            ["HAL_GyroHandle"] = "int",
            ["HAL_InterruptHandle"] = "int",
            ["HAL_NotifierHandle"] = "int",
            ["HAL_RelayHandle"] = "int",
            ["HAL_SolenoidHandle"] = "int",
            ["HAL_SerialPortHandle"] = "int",
            ["HAL_CANHandle"] = "int",
            ["HAL_SimDeviceHandle"] = "int",
            ["HAL_SimValueHandle"] = "int",
            ["HAL_DMAHandle"] = "int",
            ["HAL_DutyCycleHandle"] = "int",
            ["HAL_AddressableLEDHandle"] = "int",
            ["HAL_PDPHandle"] = "int",
            ["size_t"] = "IntPtr",
            ["struct"] = "",
            ["readonly"] = "rdonly"

        };

        public static string GetCSharpDef(string function)
        {
            foreach (var t in TypeMap)
            {
                function = function.Replace(t.Key, t.Value);
            }
            return function;
        }
    }
}
