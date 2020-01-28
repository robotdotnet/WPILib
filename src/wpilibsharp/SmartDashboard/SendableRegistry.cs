using NetworkTables;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace WPILib.SmartDashboard
{
    public class SendableRegistry
    {
        private static Lazy<SendableRegistry> instance = new Lazy<SendableRegistry>(() => new SendableRegistry());
        public static SendableRegistry Instance => instance.Value;

        private class Component
        {
            public WeakReference<Sendable>? Sendable { get; set; }
            public SendableBuilderImpl Builder { get; } = new SendableBuilderImpl();
            public string? Name { get; set; }
            public string Subsystem { get; set; } = "Ungrouped";
            public WeakReference<Sendable>? Parent { get; set; }
            public bool LiveWindow { get; set; }
            public object?[]? Data { get; set; }

            public Component()
            {

            }

            public Component(Sendable sendable)
            {
                Sendable = new WeakReference<Sendable>(sendable);
            }

            public void SetName(string moduleType, int channel)
            {
                Name = $"{moduleType} [{channel.ToString()}]";
            }

            public void SetName(string moduleType, int moduleNumber, int channel)
            {
                Name = $"{moduleType} [{moduleNumber.ToString()},{channel.ToString()}]";
            }
        }

        private readonly object mutex = new object();
        private readonly Dictionary<Sendable, Component> components = new Dictionary<Sendable, Component>();
        private int nextDataHandle = 0;

        private Component GetOrAdd(Sendable sendable)
        {
            if (!components.TryGetValue(sendable, out var comp))
            {
                comp = new Component(sendable);
                components.Add(sendable, comp);
            }
            else
            {
                if (comp.Sendable == null)
                {
                    comp.Sendable = new WeakReference<Sendable>(sendable);
                }
            }
            return comp;
        }
        

        public struct CallbackData
        {
            public Sendable Sendable { get; }
            public string? Name { get; }
            public string Subsystem { get; }
            public Sendable? Parent { get; }
            public object? Data { get; set; }
            public SendableBuilderImpl Builder { get; }
            public CallbackData(Sendable sendable, string? name, string subsystem, Sendable? parent, object? data, SendableBuilderImpl builder)
            {
                Sendable = sendable;
                Name = name;
                Subsystem = subsystem;
                Parent = parent;
                Data = data;
                Builder = builder;
            }
        }

        private SendableRegistry()
        {

        }

        public void Add(Sendable sendable, string name)
        {
            lock (mutex)
            {
                var comp = GetOrAdd(sendable);
                comp.Name = name;
            }
        }

        public void Add(Sendable sendable, string moduleType, int channel)
        {
            lock (mutex)
            {
                GetOrAdd(sendable).SetName(moduleType, channel);
            }
        }

        public void Add(Sendable sendable, string moduleType, int moduleNumber, int channel)
        {
            lock (mutex)
            {
                GetOrAdd(sendable).SetName(moduleType, moduleNumber, channel);
            }
        }

        public void Add(Sendable sendable, string subsystem, string name)
        {
            lock (mutex)
            {
                var comp = GetOrAdd(sendable);
                comp.Name = name;
                comp.Subsystem = subsystem;
            }
        }

        public void AddLW(Sendable sendable, string name)
        {
            lock (mutex)
            {
                var comp = GetOrAdd(sendable);
                comp.Name = name;
                comp.LiveWindow = true;
            }
        }

        public void AddLW(Sendable sendable, string moduleType, int channel)
        {
            lock (mutex)
            {
                var comp = GetOrAdd(sendable);
                comp.SetName(moduleType, channel);
                comp.LiveWindow = true;
            }
        }

        public void AddLW(Sendable sendable, string moduleType, int moduleNumber, int channel)
        {
            lock (mutex)
            {
                var comp = GetOrAdd(sendable);
                comp.SetName(moduleType, moduleNumber, channel);
                comp.LiveWindow = true;
            }
        }

        public void AddLW(Sendable sendable, string subsystem, string name)
        {
            lock (mutex)
            {
                var comp = GetOrAdd(sendable);
                comp.Name = name;
                comp.Subsystem = subsystem;
                comp.LiveWindow = true;
            }
        }

        public void AddChild(Sendable parent, Sendable child)
        {
            lock (mutex)
            {
                if (!components.TryGetValue(child, out var comp))
                {
                    comp = new Component();
                    components.Add(child, comp);
                }
                comp.Parent = new WeakReference<Sendable>(parent);
            }
        }

        public bool Remove(Sendable sendable)
        {
            lock (mutex)
            {
                return components.Remove(sendable);
            }
        }

        public bool Contains(Sendable sendable)
        {
            lock (mutex)
            {
                return components.ContainsKey(sendable);
            }
        }

        public string? GetName(Sendable sendable)
        {
            lock (mutex)
            {
                if (components.TryGetValue(sendable, out var comp))
                {
                    return comp.Name;
                }
                return string.Empty;
            }
        }

        public void SetName(Sendable sendable, string name)
        {
            lock (mutex)
            {
                if (components.TryGetValue(sendable, out var comp))
                {
                    comp.Name = name;
                }
            }
        }

        public void SetName(Sendable sendable, string moduleType, int channel)
        {
            lock (mutex)
            {
                if (components.TryGetValue(sendable, out var comp))
                {
                    comp.SetName(moduleType, channel);
                }
            }
        }

        public void SetName(Sendable sendable, string moduleType, int moduleNumber, int channel)
        {
            lock (mutex)
            {
                if (components.TryGetValue(sendable, out var comp))
                {
                    comp.SetName(moduleType, moduleNumber, channel);
                }
            }
        }

        public void SetName(Sendable sendable, string subsystem, string name)
        {
            lock (mutex)
            {
                if (components.TryGetValue(sendable, out var comp))
                {
                    comp.Name = name;
                    comp.Subsystem = subsystem;
                }
            }
        }

        public string? GetSubsystem(Sendable sendable)
        {
            lock (mutex)
            {
                if (components.TryGetValue(sendable, out var comp))
                {
                    return comp.Subsystem;
                }
                return string.Empty;
            }
        }

        public void SetSubsystem(Sendable sendable, string subsystem)
        {
            lock (mutex)
            {
                if (components.TryGetValue(sendable, out var comp))
                {
                    comp.Subsystem = subsystem;
                }
            }
        }

        public int DataHandle
        {
            get
            {
                lock (mutex)
                {
                    return nextDataHandle++;
                }
            }
        }

        public object? SetData(Sendable sendable, int handle, object? data)
        {
            lock (mutex)
            {
                if (!components.TryGetValue(sendable, out var comp))
                {
                    return null;
                }
                object? rv = null;
                if (comp.Data == null)
                {
                    comp.Data = new object?[handle + 1];
                }
                else if (handle < comp.Data.Length)
                {
                    rv = comp.Data[handle];
                }
                else
                {
                    object?[] copy = new object?[handle + 1];
                    comp.Data.CopyTo(copy.AsSpan());
                    comp.Data = copy;
                }
                comp.Data[handle] = data;
                return rv;
            }
        }

        public object? GetData(Sendable sendable, int handle)
        {
            lock (mutex)
            {
                if (!components.TryGetValue(sendable, out var comp))
                {
                    return null;
                }
                if (comp.Data == null || handle >= comp.Data.Length)
                {
                    return null;
                }
                return comp.Data[handle];
            }
        }

        public void EnableLiveWindow(Sendable sendable)
        {
            lock (mutex)
            {
                if (components.TryGetValue(sendable, out var comp))
                {
                    comp.LiveWindow = true;
                }
            }
        }

        public void DisableLiveWindow(Sendable sendable)
        {
            lock (mutex)
            {
                if (components.TryGetValue(sendable, out var comp))
                {
                    comp.LiveWindow = false;
                }
            }
        }

        public void Publish(Sendable sendable, NetworkTable table)
        {
            lock (mutex)
            {
                var comp = GetOrAdd(sendable);
                comp.Builder.ClearProperties();
                comp.Builder.Table = table;
                sendable.InitSendable(comp.Builder);
                comp.Builder.TriggerUpdateTable();
                comp.Builder.StartListeners();
            }
        }

        public void Update(Sendable sendable)
        {
            if (components.TryGetValue(sendable, out var comp))
            {
                comp.Builder.TriggerUpdateTable();
            }
        }

        public delegate void ForeachLiveWindowCallback(ref CallbackData data);
        private readonly List<Component> foreachComponents = new List<Component>();

        public void ForeachLiveWindow(int dataHandle, ForeachLiveWindowCallback callback)
        {
            lock (mutex)
            {
                foreachComponents.Clear();
                foreachComponents.AddRange(components.Values);
                foreach (var comp in foreachComponents)
                {
                    if (comp.Sendable == null) continue;
                    if (comp.Sendable == null || !comp.LiveWindow || !comp.Sendable.TryGetTarget(out var sendable))
                    {
                        continue;
                    }
                    Sendable? parent = null;
                    if (comp.Parent != null)
                    {
                        comp.Parent.TryGetTarget(out parent);
                    }
                    object? data = null;
                    if (comp.Data != null && dataHandle < comp.Data.Length)
                    {
                        data = comp.Data[dataHandle];
                    }

                    CallbackData cbData = new CallbackData(sendable, comp.Name, comp.Subsystem, parent, data, comp.Builder);

                    try
                    {
                        callback(ref cbData);
                    }
                    catch (Exception ex)
                    {
                        DriverStation.ReportError("Unhandled exception calling LiveWindow for " + comp.Name + ": " + ex.Message, false);
                    }
                    if (cbData.Data != null)
                    {
                        if (comp.Data == null)
                        {
                            comp.Data = new object?[dataHandle + 1];
                        } 
                        else if (dataHandle >= comp.Data.Length)
                        {
                            object?[] copy = new object?[dataHandle + 1];
                            comp.Data.CopyTo(copy.AsSpan());
                            comp.Data = copy;
                        }
                        comp.Data[dataHandle] = cbData.Data;
                    }
                }
            }
        }
    }
}
