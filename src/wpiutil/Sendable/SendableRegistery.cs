using System.Runtime.CompilerServices;

namespace WPIUtil.Sendable;

public static class SendableRegistery
{
    private static readonly object s_lockObject = new();

    private sealed class Component : IDisposable
    {
        public Component() { }

        public Component(ISendable sendable)
        {
            m_sendable = new WeakReference<ISendable>(sendable);
        }

        public void Dispose()
        {
            m_builder?.Dispose();
            if (m_data is not null)
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
        public IDisposable?[]? m_data;

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
        lock (s_lockObject)
        {
            liveWindowFactory = factory;
        }
    }

    public static void Add(ISendable sendable, string name)
    {
        lock (s_lockObject)
        {
            GetOrAdd(sendable).m_name = name;
        }
    }

    public static void Add(ISendable sendable, string moduleType, int channel)
    {
        lock (s_lockObject)
        {
            GetOrAdd(sendable).SetName(moduleType, channel);
        }
    }

    public static void Add(ISendable sendable, string moduleType, int moduleNumber, int channel)
    {
        lock (s_lockObject)
        {
            GetOrAdd(sendable).SetName(moduleType, moduleNumber, channel);
        }
    }

    public static void Add(ISendable sendable, string subsystem, string name)
    {
        lock (s_lockObject)
        {
            Component component = GetOrAdd(sendable);
            component.m_name = name;
            component.m_subsystem = subsystem;
        }
    }

    public static void AddLW(ISendable sendable, string name)
    {
        lock (s_lockObject)
        {
            Component comp = GetOrAdd(sendable);
            if (liveWindowFactory is not null)
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

    public static void AddLW(ISendable sendable, string moduleType, int channel)
    {
        lock (s_lockObject)
        {
            Component comp = GetOrAdd(sendable);
            if (liveWindowFactory is not null)
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
            comp.SetName(moduleType, channel);
        }
    }

    public static void AddLW(ISendable sendable, string moduleType, int moduleNumber, int channel)
    {
        lock (s_lockObject)
        {
            Component comp = GetOrAdd(sendable);
            if (liveWindowFactory is not null)
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
            comp.SetName(moduleType, moduleNumber, channel);
        }
    }

    public static void AddLW(ISendable sendable, string subsystem, string name)
    {
        lock (s_lockObject)
        {
            Component comp = GetOrAdd(sendable);
            if (liveWindowFactory is not null)
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
            comp.m_subsystem = subsystem;
        }
    }

    public static void AddChild(ISendable parent, object child)
    {
        lock (s_lockObject)
        {
            if (!components.TryGetValue(child, out var comp))
            {
                comp = new Component();
                components.Add(child, comp);
            }
            comp.m_parent = new(parent);
        }
    }

    public static bool Remove(ISendable sendable)
    {
        lock (s_lockObject)
        {
            if (components.TryGetValue(sendable, out var comp))
            {
                try
                {
                    comp.Dispose();
                }
                catch (Exception)
                {
                    // Ignore
                }
                components.Remove(sendable);
                return true;
            }
            return false;
        }
    }

    public static bool Contains(ISendable sendable)
    {
        lock (s_lockObject)
        {
            return components.TryGetValue(sendable, out var _);
        }
    }

    public static string GetName(ISendable sendable)
    {
        lock (s_lockObject)
        {
            if (components.TryGetValue(sendable, out var comp))
            {
                return comp.m_name ?? "";
            }
            return "";
        }
    }

    public static void SetName(ISendable sendable, string name)
    {
        lock (s_lockObject)
        {
            if (components.TryGetValue(sendable, out var comp))
            {
                comp.m_name = name;
            }
        }
    }

    public static void SetName(ISendable sendable, string moduleType, int channel)
    {
        lock (s_lockObject)
        {
            if (components.TryGetValue(sendable, out var comp))
            {
                comp.SetName(moduleType, channel);
            }
        }
    }

    public static void SetName(ISendable sendable, string moduleType, int moduleNumber, int channel)
    {
        lock (s_lockObject)
        {
            if (components.TryGetValue(sendable, out var comp))
            {
                comp.SetName(moduleType, moduleNumber, channel);
            }
        }
    }

    public static void SetName(ISendable sendable, string subsystem, string name)
    {
        lock (s_lockObject)
        {
            if (components.TryGetValue(sendable, out var comp))
            {
                comp.m_name = name;
                comp.m_subsystem = subsystem;
            }
        }
    }

    public static string GetSubsystem(ISendable sendable)
    {
        lock (s_lockObject)
        {
            if (components.TryGetValue(sendable, out var comp))
            {
                return comp.m_subsystem ?? "";
            }
            return "";
        }
    }

    public static void SetSubsystem(ISendable sendable, string subsystem)
    {
        lock (s_lockObject)
        {
            if (components.TryGetValue(sendable, out var comp))
            {
                comp.m_subsystem = subsystem;
            }
        }
    }

    public static int GetDataHandle()
    {
        lock (s_lockObject)
        {
            return nextDataHandle++;
        }
    }

    public static IDisposable? SetData(ISendable sendable, int handle, IDisposable? data)
    {
        lock (s_lockObject)
        {
            if (!components.TryGetValue(sendable, out var comp))
            {
                return null;
            }

            IDisposable? rv = null;
            if (comp.m_data is null)
            {
                comp.m_data = new IDisposable?[handle + 1];
            }
            else if (handle < comp.m_data.Length)
            {
                rv = comp.m_data[handle];
            }
            else
            {
                var tmp = comp.m_data;
                comp.m_data = new IDisposable?[handle + 1];
                tmp.CopyTo(comp.m_data, 0);
            }
            if (comp.m_data[handle] != data)
            {
                try
                {
                    comp.m_data[handle]?.Dispose();
                }
                catch (Exception)
                {
                    // ignore
                }
                comp.m_data[handle] = data;
            }
            return rv;
        }
    }

    public static object? GetData(ISendable sendable, int handle)
    {
        lock (s_lockObject)
        {
            if (components.TryGetValue(sendable, out var comp))
            {
                if (comp.m_data is not null && handle < comp.m_data.Length)
                {
                    return comp.m_data[handle];
                }
            }
            return null;
        }
    }

    public static void EnableLiveWindow(ISendable sendable)
    {
        lock (s_lockObject)
        {
            if (components.TryGetValue(sendable, out var comp))
            {
                comp.m_liveWindow = true;
            }
        }
    }

    public static void DisableLiveWindow(ISendable sendable)
    {
        lock (s_lockObject)
        {
            if (components.TryGetValue(sendable, out var comp))
            {
                comp.m_liveWindow = false;
            }
        }
    }

    public static void Publish(ISendable sendable, ISendableBuilder builder)
    {
        lock (s_lockObject)
        {
            Component comp = GetOrAdd(sendable);
            if (comp.m_builder is not null)
            {
                try
                {
                    comp.m_builder.Dispose();
                }
                catch (Exception)
                {
                    // ignore
                }
            }
            comp.m_builder = builder;
            sendable.InitSendable(comp.m_builder);
            comp.m_builder.Update();
        }
    }

    public static void Update(ISendable senable)
    {
        lock (s_lockObject)
        {
            if (components.TryGetValue(senable, out var comp))
            {
                comp.m_builder?.Update();
            }
        }
    }

    public class CallbackData
    {
        public ISendable Sendable { get; internal set; } = null!;
        public string? Name { get; internal set; }
        public string? Subsystem { get; internal set; }
        public ISendable? Parent { get; internal set; }
        public IDisposable? Data { get; set; }
        public ISendableBuilder? Builder { get; internal set; }
    }

    private static readonly List<Component> ForeachComponents = [];

    public static void ForeachLiveWindow(int dataHandle, Action<CallbackData> callback)
    {
        lock (s_lockObject)
        {
            CallbackData cbdata = new();
            ForeachComponents.Clear();
            ForeachComponents.AddRange(components.Select(x => x.Value));
            foreach (var comp in ForeachComponents)
            {
                if (comp.m_builder is null || comp.m_sendable is null)
                {
                    continue;
                }
                cbdata.Name = comp.m_name;
                cbdata.Subsystem = comp.m_subsystem;
                cbdata.Parent = null;
                if (comp.m_parent is not null && comp.m_parent.TryGetTarget(out var parent))
                {
                    cbdata.Parent = parent;
                }
                if (comp.m_data is not null && dataHandle < comp.m_data.Length)
                {
                    cbdata.Data = comp.m_data[dataHandle];
                }
                else
                {
                    cbdata.Data = null;
                }
                cbdata.Builder = comp.m_builder;
                try
                {
                    callback(cbdata);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Unhandled exception calling LiveWindow for {comp.m_name} : {ex}");
                    comp.m_liveWindow = false;
                }
                if (cbdata.Data is not null)
                {
                    if (comp.m_data is null)
                    {
                        comp.m_data = new IDisposable[dataHandle + 1];
                    }
                    else if (dataHandle >= comp.m_data.Length)
                    {
                        var tmp = comp.m_data;
                        comp.m_data = new IDisposable[dataHandle + 1];
                        tmp.CopyTo(comp.m_data, 0);
                    }
                    if (!ReferenceEquals(comp.m_data[dataHandle], cbdata.Data))
                    {
                        IDisposable? disposable = comp.m_data[dataHandle];
                        if (disposable is not null)
                        {
                            try
                            {
                                disposable.Dispose();
                            }
                            catch (Exception)
                            {
                                // ignore
                            }
                        }
                        comp.m_data[dataHandle] = cbdata.Data;
                    }
                }
            }
        }

        ForeachComponents.Clear();
    }
}
