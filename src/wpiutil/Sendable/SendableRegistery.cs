using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace WPIUtil.Sendable;

public static class SendableRegistery
{
    private class Component : IDisposable
    {
        public Component() { }

        public Component(ISendable sendable)
        {
            m_sendable = new WeakReference<ISendable>(sendable);
        }

        public void Dispose()
        {
            m_builder?.Dispose();
            if (m_data != null)
            {
                foreach (var data in m_data)
                {
                    data?.Dispose();
                }
            }
        }

        public WeakReference<ISendable>? m_sendable;
        public ISendableBuilder? m_builder;
        public string? m_name;
        public string m_subsystem = "Ungrouped";
        public WeakReference<ISendable>? m_parent;
        public bool m_liveWindow;
        public IDisposable[]? m_data;

        public void SetName(string moduleType, int channel)
        {
            m_name = $"{moduleType}[{channel}]";
        }

        public void SetName(string moduleType, int moduleNumber, int channel)
        {
            m_name = $"{moduleType}[{moduleNumber},{channel}]";
        }
    }

    private static Func<ISendableBuilder>? liveWindowFactory;
    private static readonly ConditionalWeakTable<object, Component> components = [];
    private static int nextDataHandle;

    private static Component GetOrAdd(ISendable sendable)
    {
        if (components.TryGetValue(sendable, out Component? comp))
        {
            comp.m_sendable ??= new WeakReference<ISendable>(sendable);
        }
        else
        {
            comp = new Component(sendable);
            components.Add(sendable, comp);
        }
        return comp;
    }

    public static void SetLiveWindowBuilderFactory(Func<ISendableBuilder> factory)
    {
        lock (typeof(SendableRegistery))
        {
            liveWindowFactory = factory;
        }
    }

    public static void Add(ISendable sendable, string name)
    {
        lock (typeof(SendableRegistery))
        {
            GetOrAdd(sendable).m_name = name;
        }
    }

    public static void Add(ISendable sendable, string moduleType, int channel)
    {
        lock (typeof(SendableRegistery))
        {
            GetOrAdd(sendable).SetName(moduleType, channel);
        }
    }

    public static void Add(ISendable sendable, string moduleType, int moduleNumber, int channel)
    {
        lock (typeof(SendableRegistery))
        {
            GetOrAdd(sendable).SetName(moduleType, moduleNumber, channel);
        }
    }

    public static void Add(ISendable sendable, string subsystem, string name)
    {
        lock (typeof(SendableRegistery))
        {
            Component component = GetOrAdd(sendable);
            component.m_name = name;
            component.m_subsystem = subsystem;
        }
    }

    public static void AddLW(ISendable sendable, string name)
    {
        lock (typeof(SendableRegistery))
        {
            Component comp = GetOrAdd(sendable);
            if (liveWindowFactory != null)
            {
                try
                {
                    comp.m_builder?.Dispose();
                }
                catch
                {
                    // Ignore
                }
                comp.m_builder = liveWindowFactory();
            }
            comp.m_liveWindow = true;
            comp.m_name = name;
        }
    }
}
