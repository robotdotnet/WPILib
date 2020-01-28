using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace WPILib.SmartDashboard
{
    public static class SendableRegistry
    {
        private class Component
        {
            public WeakReference<Sendable>? m_sendable;
            public SendableBuilderImpl m_builder = new SendableBuilderImpl();
            public string? m_name;
            public string m_subsystem = "Ungrouped";
            public WeakReference<Sendable>? m_parent;
            public bool m_liveWindow;
            public object[]? m_data;

            public Component() { }

            public Component(Sendable sendable)
            {
                m_sendable = new WeakReference<Sendable>(sendable);
            }

            public void SetName(string moduleType, int channel)
            {
                m_name = $"{moduleType} [{channel.ToString()}]";
            }

            public void SetName(string moduleType, int moduleNumber, int channel)
            {
                m_name = $"{moduleType} [{moduleNumber.ToString()},{channel.ToString()}]";
            }
        }

        private static readonly Dictionary<Sendable, Component> components = new Dictionary<Sendable, Component>();
        private static readonly object m_lockObject = new object();
        private static int nextDataHandle;

        public static int DataHandle => Interlocked.Increment(ref nextDataHandle);

        private static Component getOrAdd(Sendable sendable)
        {
            if (!components.TryGetValue(sendable, out var comp))
            {
                comp = new Component(sendable);
                components.Add(sendable, comp);
            } 
            else
            {
                comp.m_sendable = new WeakReference<Sendable>(sendable);
            }
            return comp;
        }

        public static void Add(Sendable sendable, string name)
        {
            lock(m_lockObject)
            {
                getOrAdd(sendable).m_name = name;
            }
        }

        public static void Add(Sendable sendable, string moduleType, int channel)
        {
            lock(m_lockObject)
            {
                getOrAdd(sendable).SetName(moduleType, channel);
            }
        }

        public static object? GetData(Sendable sendable, int handle)
        {
            lock (m_lockObject)
            {
                if (!components.TryGetValue(sendable, out var comp))
                {
                    return null;
                }
                if (comp.m_data == null || handle >= comp.m_data.Length)
                {
                    return null;
                }
                return comp.m_data[handle];
            }
        }

        public static object? SetData(Sendable sendable, int handle, object data)
        {
            lock (m_lockObject)
            {
                if (!components.TryGetValue(sendable, out var comp))
                {
                    return null;
                }
                object? rv = null;
                if (comp.m_data == null)
                {
                    comp.m_data = new object[handle + 1];
                } 
                else if (handle < comp.m_data.Length)
                {
                    rv = comp.m_data[handle];
                }
                else
                {
                    object[] copy = new object[handle + 1];
                    comp.m_data.CopyTo(copy.AsSpan());
                    comp.m_data = copy;
                }
                comp.m_data[handle] = data;
                return rv;
            }
        }

        public readonly struct CallbackData
        {
            public Sendable Sendable { get; }
            public string Name { get; }
            public string Subsystem { get; }
            public Sendable Parent { get; }
            public object Data { get; }
            public SendableBuilderImpl Builder { get; }
            public CallbackData(Sendable sendable, string name, string subsystem, Sendable parent, object data, SendableBuilderImpl builder)
            {
                Sendable = sendable;
                Name = name;
                Subsystem = subsystem;
                Parent = parent;
                Data = data;
                Builder = builder;
            }
        }

        private static readonly List<Component> foreachComponents = new List<Component>();

        //public static IEnumerable<CallbackData> ForeachLiveWindow(int dataHandle)
        //{
        //    foreachComponents.Clear();
        //    foreachComponents.AddRange(components.Values);
        //    foreach (var comp in foreachComponents)
        //    {
        //        if (comp.m_sendable == null)
        //        {
        //            continue;
        //        }
        //        CallbackData cbData;
        //        if (comp.m_sendable.TryGetTarget(out var component))
        //        {

        //        }
        //    }
        //}
    }
}
