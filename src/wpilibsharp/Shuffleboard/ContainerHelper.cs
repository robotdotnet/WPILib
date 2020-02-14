using System;
using System.Collections.Generic;
using NetworkTables;
using WPILib.SmartDashboard;

namespace WPILib.Shuffleboard
{
    internal sealed class ContainerHelper
    {
        private readonly IShuffleboardContainer m_container;
        private readonly HashSet<string> m_usedTitles = new HashSet<string>();
        private readonly List<ShuffleboardComponent> m_components = new List<ShuffleboardComponent>();
        private readonly Dictionary<string, ShuffleboardLayout> m_layouts = new Dictionary<string, ShuffleboardLayout>();

        internal ContainerHelper(IShuffleboardContainer container)
        {
            m_container = container;
        }

        public List<ShuffleboardComponent> Components => m_components;

        public ShuffleboardLayout GetLayout(string title, string type)
        {
            if (m_layouts.TryGetValue(title, out var layout))
            {
                return layout;
            }

            layout = new ShuffleboardLayout(m_container, type, title);
            m_components.Add(layout);
            m_layouts.Add(title, layout);
            return layout;
        }

        public ShuffleboardLayout GetLayout(string title)
        {
            if (m_layouts.TryGetValue(title, out var layout))
            {
                return layout;
            }
            throw new KeyNotFoundException($"No layout has been defined with the title '{title}'");
        }

        public ComplexWidget Add(string title, ISendable sendable)
        {
            CheckTitle(title);
            var widget = new ComplexWidget(m_container, title, sendable);
            m_components.Add(widget);
            return widget;
        }

        public ComplexWidget Add(ISendable sendable)
        {
            var name = SendableRegistry.Instance.GetName(sendable);
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentOutOfRangeException(nameof(sendable), "Sendable must have a name");
            }
            return Add(name, sendable);
        }

        public SimpleWidget Add<T>(string title, T defaultValue)
        {
            CheckTitle(title);
            CheckNtType(defaultValue);

            var widget = new SimpleWidget(m_container, title);
            m_components.Add(widget);
            widget.Entry.SetDefaultValue(defaultValue);
            return widget;
        }

        public SuppliedValueWidget<T> Add<T>(string title, Func<T> valueSupplier)
        {
            Precheck(title, valueSupplier);
            return AddSupplied(title, valueSupplier, (e, v) => e.SetValue(v));
        }

        public SuppliedValueWidget<string> AddString(string title, Func<string> valueSupplier)
        {
            Precheck(title, valueSupplier);
            return AddSupplied(title, valueSupplier, (e, v) => e.SetString(v));
        }

        public SuppliedValueWidget<double> AddNumber(string title, Func<double> valueSupplier)
        {
            Precheck(title, valueSupplier);
            return AddSupplied(title, valueSupplier, (e, v) => e.SetDouble(v));
        }

        public SuppliedValueWidget<bool> AddBoolean(string title, Func<bool> valueSupplier)
        {
            Precheck(title, valueSupplier);
            return AddSupplied(title, valueSupplier, (e, v) => e.SetBoolean(v));
        }

        public SuppliedValueWidget<string[]> AddStringArray(string title, Func<string[]> valueSupplier)
        {
            Precheck(title, valueSupplier);
            return AddSupplied(title, valueSupplier, (e, v) => e.SetStringArray(v));
        }

        public SuppliedValueWidget<double[]> AddDoubleArray(string title, Func<double[]> valueSupplier)
        {
            Precheck(title, valueSupplier);
            return AddSupplied(title, valueSupplier, (e, v) => e.SetDoubleArray(v));
        }

        public SuppliedValueWidget<bool[]> AddBooleanArray(string title, Func<bool[]> valueSupplier)
        {
            Precheck(title, valueSupplier);
            return AddSupplied(title, valueSupplier, (e, v) => e.SetBooleanArray(v));
        }

        public SuppliedValueWidget<byte[]> AddRaw(string title, Func<byte[]> valueSupplier)
        {
            Precheck(title, valueSupplier);
            return AddSupplied(title, valueSupplier, (e, v) => e.SetRaw(v));
        }

        private SuppliedValueWidget<T> AddSupplied<T>(string title, Func<T> supplier, Action<NetworkTableEntry, T> setter)
        {
            var widget = new SuppliedValueWidget<T>(m_container, title, supplier, setter);
            m_components.Add(widget);
            return widget;
        }

        private void Precheck<T>(string title, Func<T> valueSupplier)
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }
            if (valueSupplier == null)
            {
                throw new ArgumentNullException(nameof(valueSupplier));
            }
            CheckTitle(title);
        }

        private static void CheckNtType<T>(T data)
        {

        }

        private void CheckTitle(string title)
        {
            if (m_usedTitles.Contains(title))
            {
                throw new ArgumentException($"Title is already in use: {title}");
            }
            m_usedTitles.Add(title);
        }
    }
}
