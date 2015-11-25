using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
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
            // Load assembly with HAL Base
            Assembly HalBaseAssembly = typeof(HAL_Base.HAL).Assembly;

            List<string> nullTypes = new List<string>();

            foreach(Type type in HalBaseAssembly.GetTypes())
            {
                if (!type.Name.ToLower().Contains("hal")) continue;

                FieldInfo[] fields = type.GetFields();

                List<string> nullDelegateFields = (from fieldInfo in fields where fieldInfo.FieldType.IsSubclassOf(typeof(MulticastDelegate)) let x = fieldInfo.GetValue(null) where x == null select fieldInfo.Name).ToList();
                foreach (var nullDelegateField in nullDelegateFields)
                {
                    nullTypes.Add(type.Name + ": " + nullDelegateField);
                }
            }

            foreach(var s in nullTypes)
            {
                Console.WriteLine(s);
            }
            Assert.That(nullTypes.Count == 0);            
        }


        private void CheckForBlittable(List<TypeSyntax> types, List<string> allowedTypes, List<string> nonBlittableFuncs, string nonBlittableLine)
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
        }

        private static bool IsBlittable(Type type)
        {
            if (type.IsArray)
            {
                var elements = type.GetElementType();
                return elements.IsValueType && IsBlittable(elements);
            }
            try
            {
                object obj = FormatterServices.GetUninitializedObject(type);
                GCHandle.Alloc(obj, GCHandleType.Pinned).Free();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [Test]
        public void TestHALJoystickAxes()
        {
            Assert.That(Marshal.SizeOf(typeof(HALJoystickAxes)), Is.EqualTo(26));
            Assert.That(IsBlittable(typeof(HALJoystickAxes)));
        }

        [Test]
        public void TestHALJoystickPOVs()
        {
            Assert.That(Marshal.SizeOf(typeof(HALJoystickPOVs)), Is.EqualTo(26));
            Assert.That(IsBlittable(typeof(HALJoystickPOVs)));
        }

        [Test]
        public void TestHALJoystickButtons()
        {
            Assert.That(Marshal.SizeOf(typeof(HALJoystickButtons)), Is.EqualTo(8));
            Assert.That(IsBlittable(typeof(HALJoystickButtons)));
        }

        [Test]
        public void TestHALJoystickDescriptor()
        {
            Assert.That(Marshal.SizeOf(typeof(HALJoystickDescriptor)), Is.EqualTo(273));
            Assert.That(IsBlittable(typeof(HALJoystickDescriptor)));
        }

        [Test]
        public void TestCANStreamMessage()
        {
            Assert.That(Marshal.SizeOf(typeof(CANStreamMessage)), Is.EqualTo(20));
            Assert.That(IsBlittable(typeof(CANStreamMessage)));
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


            var halBaseDelegates = NetProjects.GetHalBaseDelegates();
            foreach (var halDelegate in halBaseDelegates)
            {
                foreach (var methodSyntax in halDelegate.Methods)
                {
                    List<TypeSyntax> types = new List<TypeSyntax>();

                    if (methodSyntax.AttributeLists.Count != 0)
                    {
                        if (methodSyntax.AttributeLists[0].Attributes[0].Name.ToString() == nameof(HALAllowNonBlittable))
                        {
                            //We can ignore it.
                        }
                        else
                        {
                            types.Add(methodSyntax.ReturnType);
                        }
                    }
                    else
                    {
                        types.Add(methodSyntax.ReturnType);
                    }

                    List<string> param = new List<string>();

                    StringBuilder builder = new StringBuilder();
                    builder.Append($"\t {methodSyntax.ReturnType} {methodSyntax.Identifier} (");
                    bool first = true;
                    foreach (var parameter in methodSyntax.ParameterList.Parameters)
                    {
                        if (parameter.AttributeLists.Count != 0)
                        {
                            if (parameter.AttributeLists[0].Attributes[0].Name.ToString() == nameof(HALAllowNonBlittable))
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
            return string.Equals(m_retType, other.m_retType) && string.Equals(m_identifier.ToLower(), other.m_identifier.ToLower()) && m_parameters.SequenceEqual(other.m_parameters);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = m_retType?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (m_identifier?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (m_parameters?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public Function(string ret, string id, string[] p)
        {
            m_retType = ret;
            m_identifier = id;
            m_parameters = p;
        }

        private readonly string m_retType;
        private readonly string m_identifier;
        private readonly string[] m_parameters;

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
