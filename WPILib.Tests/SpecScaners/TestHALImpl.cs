using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HAL_Base;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;

namespace WPILib.Tests.SpecScaners
{
    [TestFixture]
    public class TestHALImpl
    {
        [Test]
        public void TestHALBaseMapsToHALSim()
        {

            List<string> fd = new List<string>();
            FieldInfo[] fields = typeof(HAL).GetFields();

            foreach (var fieldInfo in fields)
            {
                if ((fieldInfo.FieldType).IsSubclassOf(typeof(MulticastDelegate)))
                {
                    var x = fieldInfo.GetValue(null);
                    if (x == null)
                    {
                        fd.Add(fieldInfo.Name);
                    }
                }
            }
            foreach (var VARIABLE in fd)
            {
                Console.WriteLine(VARIABLE);
            }

            Assert.IsTrue(fd.Count == 0);
        }


        private bool CheckForBlittable(List<TypeSyntax> types, List<string> allowedTypes, 
            List<string> nonBlittableFuncs, string nonBlittableLine)
        {
            bool allBlittable = true;
            foreach (TypeSyntax t in types)
            {
                bool found = false;
                foreach (string a in allowedTypes)
                {
                    if (t.ToString().Contains(a))//Contains is OK, since arrays of blittable types are blittable.
                    {
                        found = true;
                    }
                }
                if (!found)
                {
                    allBlittable = false;
                }
            }

            if (!allBlittable)
            {
                nonBlittableFuncs.Add(nonBlittableLine);
            }
            return allBlittable;
        }


        [Test]
        public void TestHALBlittable()
        {

            List<string> allowedTypes = new List<string>()
            {
                "byte",
                "sbyte",
                "short",
                "ushort",
                "int",
                "uint",
                "long",
                "ulong",
                "IntPtr",
                "UIntPtr",
                "float",
                "void",
                "double",
            
                // For now force our enum types to be OK
                "CTR_Code",
                "HALAccelerometerRange",
                "HALAllianceStationID",
                "AnalogTriggerType",
                "Mode",

                //Also allow any structs known to be blittable
                "CANStreamMessage",
                "HALJoystickButtons",
                "HALJoystickPOVs",
                "HALJoystickAxes",
                "HALControlWord",

                //For now allow bool, since it marshalls easily
                //This will change if the native windows HAL is not 1 byte bools
                "bool",
                
                //Going to allow the joystick structure, even though it is not blittable.
                //Still trying to figure out the best way to do it right.
                "HALJoystickDescriptor"
            };

            List<string> notBlittableMethods = new List<string>();


            var funcs2 = NetProjects.GetHalBaseDelegates();
            foreach (var func in funcs2)
            {
                foreach (var syntax in func.Methods)
                {
                    List<TypeSyntax> types = new List<TypeSyntax>();

                    if (syntax.AttributeLists.Count != 0)
                    {
                        if (syntax.AttributeLists[0].Attributes[0].Name.ToString() == nameof(HALAllowNonBlittable)
                            && syntax.ReturnType.ToString().Contains("string"))
                        {
                            //We can ignore it.
                        }
                        else
                        {
                            types.Add(syntax.ReturnType);
                        }
                    }
                    else
                    {
                        types.Add(syntax.ReturnType);
                    }

                    //types.Add(syntax.ReturnType);



                    string ret = syntax.ReturnType.ToString();
                    string id = syntax.Identifier.ToString();
                    List<string> param = new List<string>();

                    StringBuilder builder = new StringBuilder();
                    builder.Append($"\t {syntax.ReturnType} {syntax.Identifier} (");
                    bool first = true;
                    foreach (var parameter in syntax.ParameterList.Parameters)
                    {
                        if (parameter.AttributeLists.Count != 0)
                        {
                            if (parameter.AttributeLists[0].Attributes[0].Name.ToString() == nameof(HALAllowNonBlittable)
                                && parameter.Type.ToString().Contains("string"))
                            {
                                //We can ignore it.
                            }
                            else
                            {
                                types.Add(parameter.Type);
                            }
                        }
                        else
                        {
                            types.Add(parameter.Type);
                        }

                        
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

                    CheckForBlittable(types, allowedTypes, notBlittableMethods, builder.ToString());
                }
            }

            foreach (string s in notBlittableMethods)
            {
                Console.WriteLine(s);
            }

            if (notBlittableMethods.Count != 0)
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
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
