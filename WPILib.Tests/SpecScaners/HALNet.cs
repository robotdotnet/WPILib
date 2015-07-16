using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WPILib.Tests.SpecScaners
{
    [TestClass]
    public class HALNet
    {
        public static List<HALRIOClass> GetHalRoboRIOFunctions()
        {
            List<HALRIOClass> HalRoboRIOMethods = new List<HALRIOClass>();
            var dir = "..\\..\\..\\HAL-RoboRIO";
            foreach (var file in Directory.GetFiles(dir, "*.cs"))
            {
                //List<MethodDeclarationSyntax> methodList = new List<MethodDeclarationSyntax>();
                HALRIOClass cs = new HALRIOClass
                {
                    ClassName = "",
                    Methods = new List<MethodDeclarationSyntax>()
                };
                using (var stream = File.OpenRead(file))
                {
                    var tree = CSharpSyntaxTree.ParseText(SourceText.From(stream), path: file);
                    cs.ClassName = Path.GetFileName(file);
                    var methods =
                        tree.GetRoot()
                            .DescendantNodes()
                            .OfType<AttributeSyntax>()
                            .Where(a => a.Name.ToString() == "DllImport")
                            .Select(a => a.Parent.Parent)
                            .Cast<MethodDeclarationSyntax>();
                    cs.Methods.AddRange(methods);
                }
                HalRoboRIOMethods.Add(cs);
            }

            return HalRoboRIOMethods;
        }

        public static List<HALBaseClass> GetHalBaseFunctions()
        {
            List<HALBaseClass> HalBaseMethods = new List<HALBaseClass>();
            var dir = "..\\..\\..\\HAL-Base\\Generated";
            foreach (var file in Directory.GetFiles(dir, "*.cs"))
            {
                //List<MethodDeclarationSyntax> methodList = new List<MethodDeclarationSyntax>();
                HALBaseClass cs = new HALBaseClass
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
                HalBaseMethods.Add(cs);
            }

            return HalBaseMethods;
        }
    }
}
