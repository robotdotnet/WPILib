using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using HAL;
using HAL.Base;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;

namespace WPILib.Tests.SpecScaners
{
    [TestFixture]
    public class TestHALImpl
    {
        [Test]
        //[Ignore("Ignoring until we get a good reliable drop")]
        public void TestHALRoboRioMapsToNativeAssemblySymbols()
        {
            var halRoboRioSymbols = NetProjects.GetHALRoboRioNativeSymbols();

            var assembly = Assembly.GetExecutingAssembly();
            var ps = Path.DirectorySeparatorChar;
            var path = assembly.CodeBase.Replace("file:///", "").Replace("/", ps.ToString());
            path = Path.GetDirectoryName(path);


            // Start the child process.
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = path + "\\..\\..\\HAL\\AthenaHAL\\Native\\frcnm.exe";
            p.StartInfo.Arguments = path + "\\..\\..\\HAL\\AthenaHAL\\Native\\libHALAthena.so";
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            bool found = true;


            string[] nativeSymbols = output.Split('\r');

            foreach (var halSymbol in halRoboRioSymbols)
            {
                bool foundSymbol = nativeSymbols.Any(nativeSymbol => nativeSymbol.EndsWith(halSymbol));
                if (!foundSymbol)
                {
                    found = false;
                    Console.WriteLine(halSymbol);
                }
            }

            Assert.That(found);
        }


        [Test, Ignore("Need to determine if this is OK")]
        public void TestHALBaseMapsToHALSim()
        {
            // Load assembly with HAL Base
            Assembly halBaseAssembly = typeof(HAL.Base.HAL).Assembly;

            List<string> nullTypes = (from type in halBaseAssembly.GetTypes()
                                      where type.Name.ToLower().Contains("hal")
                                      let fields = type.GetFields()
                                      let nullDelegateFields = (from fieldInfo in fields
                                                                where fieldInfo.FieldType.IsSubclassOf(typeof (MulticastDelegate))
                                                                let x = fieldInfo.GetValue(null) where x == null select fieldInfo.Name).ToList()
                                      from nullDelegateField in nullDelegateFields select type.Name + ": " + nullDelegateField).ToList();

            foreach(var s in nullTypes)
            {
                Console.WriteLine(s);
            }
            Assert.That(nullTypes.Count == 0);            
        }

        //Checks all our types for blittable
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
            // If is array
            if (type.IsArray)
            {
                //Check that the elements are value type, and that the element itself is blittable.
                var elements = type.GetElementType();
                return elements.IsValueType && IsBlittable(elements);
            }
            try
            {
                //Otherwise try and pin the type. If it pins, it is blittable.
                //If exception is thrown, it is not blittable, and do not allow.
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
            Assert.That(Marshal.SizeOf(typeof(HALJoystickAxes)), Is.EqualTo(52));
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
                // Allowed types with arrays are also allowed
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
                "HALAnalogTriggerType",
                "HALEncoderEncodingType",
                "HALEncoderIndexingType",
                "HALRuntimeType",
                "Mode",

                //Also allow any structs known to be blittable
                "CANStreamMessage",
                "HALJoystickButtons",
                "HALJoystickPOVs",
                "HALJoystickAxes",
                "HALControlWord",
                "HALJoystickDescriptor",

                // Make our structs blittable
                "HAL_NotifierProcess",
                "HAL_InterruptHandlerFunction",

                //For now allow bool, since it marshalls easily
                //This will change if the native windows HAL is not 1 byte bools
                "bool",
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

    public struct HALDelegateClass
    {
        public string ClassName;
        public List<DelegateDeclarationSyntax> Methods;
    }

    
}
