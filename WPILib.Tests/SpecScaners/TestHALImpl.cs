using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPILib.Exceptions;

namespace WPILib.Tests.SpecScaners
{
    [TestClass]
    public class TestHALImpl
    {
        [TestMethod]
        public void TestHALBaseMapsToHALRIO()
        {
            StringBuilder functionList = new StringBuilder();

            functionList.AppendLine("HAL-roboRIO Functions\n");

            List<Function> rioFunctions = new List<Function>();
            var funcs = HALNet.GetHalRoboRIOFunctions();
            foreach (var func in funcs)
            {
                functionList.AppendLine(func.ClassName);
                foreach (var syntax in func.Methods)
                {
                    //Function
                    string ret = syntax.ReturnType.ToString();
                    string id = syntax.Identifier.ToString();
                    List<string> param = new List<string>();

                    StringBuilder builder = new StringBuilder();
                    builder.Append($"\t {syntax.ReturnType} {syntax.Identifier} (");
                    bool first = true;
                    foreach (var parameter in syntax.ParameterList.Parameters)
                    {
                        param.Add(parameter.Type.ToString());
                        if (first)
                        {
                            first = false;
                            builder.Append($"{parameter.Type} {parameter.Identifier}");
                        }
                        else
                        {
                            builder.Append($", {parameter.Type} {parameter.Identifier}");
                        }
                    }
                    builder.Append(")");
                    functionList.AppendLine(builder.ToString());

                    Function f = new Function(ret, id, param.ToArray());
                    rioFunctions.Add(f);
                }
            }

            functionList.AppendLine("\nBase Functions\n");

            List<Function> baseFunctions = new List<Function>();

            var funcs2 = HALNet.GetHalBaseFunctions();
            foreach (var func in funcs2)
            {
                functionList.AppendLine(func.ClassName);
                foreach (var syntax in func.Methods)
                {
                    string ret = syntax.ReturnType.ToString();
                    string id = syntax.Identifier.ToString();
                    List<string> param = new List<string>();

                    StringBuilder builder = new StringBuilder();
                    builder.Append($"\t {syntax.ReturnType} {syntax.Identifier} (");
                    bool first = true;
                    foreach (var parameter in syntax.ParameterList.Parameters)
                    {
                        param.Add(parameter.Type.ToString());
                        if (first)
                        {
                            first = false;
                            builder.Append($"{parameter.Type} {parameter.Identifier}");
                        }
                        else
                        {
                            builder.Append($", {parameter.Type} {parameter.Identifier}");
                        }
                    }
                    builder.Append(")");
                    functionList.AppendLine(builder.ToString());

                    Function f = new Function(ret, id.Replace("Delegate", ""), param.ToArray());
                    baseFunctions.Add(f);
                }
            }
            List<Function> wrong = (from rioFunction 
                                    in rioFunctions
                                    let found = baseFunctions.Any(rioFunction.Equals)
                                    where !found
                                    select rioFunction).ToList();
            if (wrong.Count == 0)
            {
                Console.WriteLine("All functions pass");
            }

            foreach (var function in wrong)
            {
                Console.WriteLine(function.identifier + " Signature does not match");
            }

            Console.WriteLine("\n\n");

            Console.WriteLine(functionList.ToString());
        }
    }

    internal struct Function
    {
        public bool Equals(Function other)
        {
            return string.Equals(retType, other.retType) && string.Equals(identifier.ToLower(), other.identifier.ToLower()) && parameters.SequenceEqual(other.parameters);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = retType?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (identifier?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (parameters?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public Function(string ret, string id, string[] p)
        {
            retType = ret;
            identifier = id;
            parameters = p;
        }

        public readonly string retType;
        public readonly string identifier;
        public readonly string[] parameters;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Function && Equals((Function)obj);
        }
    }

    public struct HALRIOClass
    {
        public string ClassName;
        public List<MethodDeclarationSyntax> Methods;
    }

    public struct HALBaseClass
    {
        public string ClassName;
        public List<DelegateDeclarationSyntax> Methods;
    }


}
