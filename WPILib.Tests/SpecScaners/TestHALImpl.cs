using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace WPILib.Tests.SpecScaners
{
    [TestFixture]
    public class TestHALImpl
    {
        [Test]
        public void TestHALRioMapsToAthena()
        {
            NativeProjects.clanger();

            StringBuilder functionList = new StringBuilder();

            functionList.AppendLine("HAL-roboRIO Functions\n");

            List<Function> rioFunctions = new List<Function>();
            var funcs = NetProjects.GetHalRoboRIOMethods();
            foreach (var func in funcs)
            {
                functionList.AppendLine(func.ClassName);
                foreach (var syntax in func.Methods)
                {
                    //Console.WriteLine(syntax.GetNativeMethod());
                    
                    //Function
                    string ret = syntax.ReturnType.ToString();
                    string id = syntax.GetNativeMethod();//syntax.Identifier.ToString();
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
                            builder.Append($"{parameter.Type}");
                        }
                        else
                        {
                            builder.Append($", {parameter.Type}");
                        }
                    }
                    builder.Append(")");
                    functionList.AppendLine(builder.ToString());

                    Function f = new Function(ret, id, param.ToArray());
                    rioFunctions.Add(f);
                }
            }

            functionList.AppendLine("\nAthena Functions\n");
            var baseFunctions = NativeProjects.funcs;

            foreach (var b in baseFunctions)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append($"\t {b.retType} {b.identifier} (");

                bool first = true;
                foreach (var parameter in b.parameters)
                {
                    if (first)
                    {
                        first = false;
                        builder.Append(parameter);
                    }
                    else
                    {
                        builder.Append(", " + parameter);
                    }
                }

                builder.Append(")");
                functionList.AppendLine(builder.ToString());
            }
            /*
            List<Function> baseFunctions = new List<Function>();

            var funcs2 = NetProjects.GetHalBaseDelegates();
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
            }rioFunctions
            */
            
            List<Function> wrong = (from baseFunction
                                    in baseFunctions
                                    let found = rioFunctions.Any(baseFunction.Equals)
                                    where !found
                                    select baseFunction).ToList();
            bool pass = false;
            if (wrong.Count == 0)
            {
                pass = true;
                Console.WriteLine("All functions pass");
            }

            foreach (var function in wrong)
            {
                Console.WriteLine(function.identifier + " Signature does not match");
            }

            Console.WriteLine("\n\n");

            Console.WriteLine(functionList.ToString());

            Assert.IsTrue(pass);
        }

        [Test]
        public void TestHALBaseMapsToHALSim()
        {
            StringBuilder functionList = new StringBuilder();

            functionList.AppendLine("HAL-Simulator Functions\n");

            List<Function> SimFunctions = new List<Function>();
            var funcs = NetProjects.GetHALSimMethods();
            foreach (var func in funcs)
            {
                functionList.AppendLine(func.ClassName);
                foreach (var syntax in func.Methods)
                {
                    //Console.WriteLine(syntax.GetNativeMethod());
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
                    SimFunctions.Add(f);
                }
            }

            functionList.AppendLine("\nBase Functions\n");

            List<Function> baseFunctions = new List<Function>();

            var funcs2 = NetProjects.GetHalBaseDelegates();
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
            List<Function> wrong = (from baseFunction
                                    in baseFunctions
                                    let found = SimFunctions.Any(baseFunction.Equals)
                                    where !found
                                    select baseFunction).ToList();

            bool pass = false;
            if (wrong.Count == 0)
            {
                pass = true;
                Console.WriteLine("All functions pass");
            }

            foreach (var function in wrong)
            {
                Console.WriteLine(function.identifier + " Signature does not match");
            }

            Console.WriteLine("\n\n");

            Console.WriteLine(functionList.ToString());

            Assert.IsTrue(pass);
        }

        [Test]
        public void TestHALBaseMapsToHALRIO()
        {
            StringBuilder functionList = new StringBuilder();

            functionList.AppendLine("HAL-roboRIO Functions\n");

            List<Function> rioFunctions = new List<Function>();
            var funcs = NetProjects.GetHalRoboRIOMethods();
            foreach (var func in funcs)
            {
                functionList.AppendLine(func.ClassName);
                foreach (var syntax in func.Methods)
                {
                    //Console.WriteLine(syntax.GetNativeMethod());
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

            var funcs2 = NetProjects.GetHalBaseDelegates();
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
            List<Function> wrong = (from baseFunction 
                                    in baseFunctions
                                    let found = rioFunctions.Any(baseFunction.Equals)
                                    where !found
                                    select baseFunction).ToList();
            bool pass = false;              
            if (wrong.Count == 0)
            {
                pass = true;
                Console.WriteLine("All functions pass");
            }

            foreach (var function in wrong)
            {
                Console.WriteLine(function.identifier + " Signature does not match");
            }

            Console.WriteLine("\n\n");

            Console.WriteLine(functionList.ToString());

            Assert.IsTrue(pass);
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

    public struct HALMethodClass
    {
        public string ClassName;
        public List<MethodDeclarationSyntax> Methods;
    }

    public struct HALDelegateClass
    {
        public string ClassName;
        public List<DelegateDeclarationSyntax> Methods;
    }


}
