using System;
using System.Collections.Generic;
using System.Reflection;
using NetworkTables;

namespace WPILib.Oblog
{
    public static class Logger
    {
        internal enum LogType
        {
            LOG, CONFIG
        }

        private static readonly Dictionary<NetworkTableEntry, Func<object>> entrySupplierMap = new Dictionary<NetworkTableEntry, Func<object>>();

        public static bool CycleWarningsEnabled { get; set; }

        public static void ConfigureLogging(object rootContainer)
        {
            if (rootContainer == null)
            {
                throw new ArgumentNullException(nameof(rootContainer));
            }
            ConfigureLogging(LogType.LOG, true, rootContainer, new WrappedShuffleboard(), NetworkTableInstance.Default);
        }

        public static void ConfigureConfig(object rootContainer)
        {
            if (rootContainer == null)
            {
                throw new ArgumentNullException(nameof(rootContainer));
            }
            ConfigureLogging(LogType.CONFIG, true, rootContainer, new WrappedShuffleboard(), NetworkTableInstance.Default);
        }

        public static void ConfigureLoggingAndConfig(object rootContainer, bool separate)
        {
            if (rootContainer == null)
            {
                throw new ArgumentNullException(nameof(rootContainer));
            }
            var shuffleboard = new WrappedShuffleboard();
            ConfigureLogging(LogType.LOG, separate, rootContainer, shuffleboard, NetworkTableInstance.Default);
            ConfigureLogging(LogType.CONFIG, separate, rootContainer, shuffleboard, NetworkTableInstance.Default);
        }

        public static void ConfigureLoggingNTOnly(object rootContainer, string rootName)
        {

        }

        public static void UpdateEntries()
        {
            foreach (var item in entrySupplierMap)
            {
                item.Key.SetValue(item.Value());
            }
            //setterRunner.RunSynchronous();
        }

        private static void ConfigureLogging(LogType logType,
             bool separate, object rootContainer, IShuffleboardWrapper shuffleboard, NetworkTableInstance nt)
        {
            IShuffleboardContainerWrapper bin;

            if (rootContainer is ILoggable loggable)
            {
                bin = shuffleboard.GetTab(loggable.ConfigureLogName());
            }
            else
            {
                bin = shuffleboard.GetTab(rootContainer.GetType().Name);
            }

            switch (logType)
            {
                case LogType.LOG:
                    LogFieldsPropertiesAndMethods(rootContainer, rootContainer.GetType(), bin, new HashSet<FieldInfo>(), new HashSet<MethodInfo>(), new HashSet<PropertyInfo>());
                    break;
                case LogType.CONFIG:
                    //ConfigFieldsPropertiesAndMethods(rootContainer, rootContainer.GetType(), bin, new HashSet<FieldInfo>(), new HashSet<MethodInfo>(), new HashSet<PropertyInfo>());
                    break;
            }

            Action<ILoggable> log = (toLog) =>
            {

            };
        }

        private static void LogFieldsPropertiesAndMethods(
            object loggable, Type loggableType, IShuffleboardContainerWrapper bin, ISet<FieldInfo> registeredFields, ISet<MethodInfo> registeredMethods, ISet<PropertyInfo> registeredProperties)
        {
            var fields = loggableType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var methods = loggableType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var properties = loggableType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            // Process fields
            foreach (var field in fields)
            {
                if (IsNull(field, loggable) || registeredFields.Contains(field))
                {
                    continue;
                }


                registeredFields.Add(field);

                foreach (var type in logHandler.Keys)
                {
                    foreach (var attribute in field.GetCustomAttributes(type))
                    {
                        if (!logHandler.TryGetValue(attribute.GetType(), out var process))
                        {
                            continue;
                        }

                        process.ProcessField(() => field.GetValue(loggable),
                            attribute, bin, field.Name);
                    }
                }
            }

            foreach (var method in methods)
            {
                // Only look at getters
                if (method.ReturnType == typeof(void) || method.GetParameters().Length > 0 || registeredMethods.Contains(method))
                {
                    continue;
                }

                registeredMethods.Add(method);

                foreach (var type in logHandler.Keys)
                {
                    foreach (var attribute in method.GetCustomAttributes(type))
                    {
                        if (!logHandler.TryGetValue(attribute.GetType(), out var process))
                        {
                            continue;
                        }

                        process.ProcessField(() => method.Invoke(loggable, null),
                            attribute, bin, method.Name);
                    }
                }
            }

            foreach (var property in properties)
            {

                // Only look at getters
                if (!property.CanRead || registeredProperties.Contains(property))
                {
                    continue;
                }

                registeredProperties.Add(property);

                foreach (var type in logHandler.Keys)
                {
                    foreach (var attribute in property.GetCustomAttributes(type))
                    {
                        if (!logHandler.TryGetValue(attribute.GetType(), out var process))
                        {
                            continue;
                        }

                        process.ProcessField(() => property.GetValue(loggable),
                            attribute, bin, property.Name);
                    }
                }
            }
        }

        private static void ConfigFieldsPropertiesAndMethods(
            object loggable, Type loggableType, IShuffleboardContainerWrapper bin, NetworkTableInstance nt, ISet<FieldInfo> registeredFields, ISet<MethodInfo> registeredMethods, ISet<PropertyInfo> registeredProperties)
        {
            var fields = loggableType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var methods = loggableType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var properties = loggableType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            // Process fields
            foreach (var field in fields)
            {
                if (IsNull(field, loggable) || registeredFields.Contains(field))
                {
                    continue;
                }


                registeredFields.Add(field);

                foreach (var type in logHandler.Keys)
                {
                    foreach (var attribute in field.GetCustomAttributes(type))
                    {
                        HandleMethodNameAttribute(loggable, bin, nt, field, attribute);
                    }
                }


            }

            foreach (var method in methods)
            {
                // Only look at getters
                if (method.ReturnType == typeof(void) || method.GetParameters().Length > 0 || registeredMethods.Contains(method))
                {
                    continue;
                }

                registeredMethods.Add(method);

                foreach (var type in logHandler.Keys)
                {
                    foreach (var attribute in method.GetCustomAttributes(type))
                    {
                        if (!logHandler.TryGetValue(attribute.GetType(), out var process))
                        {
                            continue;
                        }

                        process.ProcessField(() => method.Invoke(loggable, null),
                            attribute, bin, method.Name);
                    }
                }
            }

            foreach (var property in properties)
            {

                // Only look at getters
                if (!property.CanRead || registeredProperties.Contains(property))
                {
                    continue;
                }

                registeredProperties.Add(property);

                foreach (var type in logHandler.Keys)
                {
                    foreach (var attribute in property.GetCustomAttributes(type))
                    {
                        if (!logHandler.TryGetValue(attribute.GetType(), out var process))
                        {
                            continue;
                        }

                        process.ProcessField(() => property.GetValue(loggable),
                            attribute, bin, property.Name);
                    }
                }
            }
        }

        private static bool IsNull(FieldInfo field, object obj)
        {
            return field.GetValue(obj) == null;
        }

        private interface FieldProcessor
        {
            void ProcessField(Func<object> supplier, Attribute parameters, IShuffleboardContainerWrapper bin, string name);
        }

        private static readonly Dictionary<Type, FieldProcessor> logHandler = new Dictionary<Type, FieldProcessor>();

        private static readonly Dictionary<Type, Func<FieldInfo, Attribute, MethodInfo>> methodHandler = new Dictionary<Type, Func<FieldInfo, Attribute, MethodInfo>>();

        private static void HandleMethodNameAttribute(object loggable, IShuffleboardContainerWrapper bin, NetworkTableInstance nt, FieldInfo field, Attribute attribute)
        {
            if (methodHandler.ContainsKey(attribute.GetType()))
            {
                return;
            }


        }
    }
}
