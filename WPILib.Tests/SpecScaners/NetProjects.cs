using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace WPILib.Tests.SpecScaners
{
    /// <summary>
    /// This class contains methods to get specific methods from DotNet projects
    /// </summary>
    public static class NetProjects
    {
        /// <summary>
        /// Gets a list of all the native symbols needed by HAL-RoboRIO
        /// </summary>
        /// <returns></returns>
        public static List<string> GetHALRoboRioNativeSymbols()
        {
            List<string> nativeFunctions = new List<string>();

            var assembly = Assembly.GetExecutingAssembly();
            var ps = Path.DirectorySeparatorChar;
            var path = assembly.CodeBase.Replace("file:///", "").Replace("/", ps.ToString());
            path = Path.GetDirectoryName(path);

            var dir = path + "\\..\\..\\HAL\\AthenaHAL";
            foreach (var file in Directory.GetFiles(dir, "*.cs"))
            {
                if (!file.ToLower().Contains("hal")) continue;
                using (StreamReader reader = new StreamReader(file))
                {
                    bool foundInitialize = false;
                    string line;
                    while (!foundInitialize)
                    {
                        line = reader.ReadLine();
                        if (line == null) break;
                        if (line.ToLower().Contains("static void initialize")) foundInitialize = true;
                    }
                    if (!foundInitialize) continue;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.TrimStart(' ').StartsWith("//")) continue;
                        if (line.ToLower().Contains("marshal.getdelegateforfunctionpointer"))
                        {
                            int startParam = line.IndexOf('"');
                            int endParam = line.IndexOf('"', startParam + 1);
                            nativeFunctions.Add(line.Substring(startParam + 1, endParam - startParam - 1));
                        }
                    }
                }
            }
            return nativeFunctions;
        }

        // Gets a list of all of our delegates used by the HAL
        public static List<HALDelegateClass> GetHalBaseDelegates()
        {
            List<HALDelegateClass> halBaseMethods = new List<HALDelegateClass>();

            var assembly = Assembly.GetExecutingAssembly();
            var ps = Path.DirectorySeparatorChar;
            var path = assembly.CodeBase.Replace("file:///", "").Replace("/", ps.ToString());
            path = Path.GetDirectoryName(path);

            var dir = path + "\\..\\..\\HAL\\Delegates";
            foreach (var file in Directory.GetFiles(dir, "*.cs"))
            {
                HALDelegateClass cs = new HALDelegateClass
                {
                    ClassName = "",
                    Methods = new List<DelegateDeclarationSyntax>()
                };
                using (var stream = File.OpenRead(file))
                {
                    var tree = CSharpSyntaxTree.ParseText(SourceText.From(stream), path: file);
                    cs.ClassName = Path.GetFileName(file);
                    var methods =
                        tree.GetRoot()
                            .DescendantNodes()
                            .OfType<DelegateDeclarationSyntax>()
                            .Select(a => a).ToList();
                    cs.Methods.AddRange(methods);
                }
                halBaseMethods.Add(cs);
            }

            return halBaseMethods;
        }
    }
}
