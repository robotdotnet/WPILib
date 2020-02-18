using System;
using System.Collections.Generic;
using NetworkTables;
using WPILib.SmartDashboardNS;

namespace WPILib.ShuffleboardNS
{
    public class ShuffleboardLayout : ShuffleboardComponent<ShuffleboardLayout>, IShuffleboardContainer
    {
        private readonly ContainerHelper m_helper;

        internal ShuffleboardLayout(IShuffleboardContainer parent, string? name, string type)
          : base(parent, type, name)
        {
            m_helper = new ContainerHelper(this);
        }

        public List<ShuffleboardComponent> Components => m_helper.Components;

        public ComplexWidget Add(string title, ISendable sendable)
        {
            return m_helper.Add(title, sendable);
        }

        public ComplexWidget Add(ISendable sendable)
        {
            return m_helper.Add(sendable);
        }

        public SimpleWidget Add<T>(string title, T defaultValue)
        {
            return m_helper.Add(title, defaultValue);
        }

        public SuppliedValueWidget<T> Add<T>(string title, Func<T> valueSupplier)
        {
            return m_helper.Add(title, valueSupplier);
        }

        public SuppliedValueWidget<bool> AddBoolean(string title, Func<bool> valueSupplier)
        {
            return m_helper.AddBoolean(title, valueSupplier);
        }

        public SuppliedValueWidget<bool[]> AddBooleanArray(string title, Func<bool[]> valueSupplier)
        {
            return m_helper.AddBooleanArray(title, valueSupplier);
        }

        public SuppliedValueWidget<double[]> AddDoubleArray(string title, Func<double[]> valueSupplier)
        {
            return m_helper.AddDoubleArray(title, valueSupplier);
        }

        public SuppliedValueWidget<double> AddNumber(string title, Func<double> valueSupplier)
        {
            return m_helper.AddNumber(title, valueSupplier);
        }

        public SimpleWidget AddPersistent<T>(string title, T defaultValue)
        {
            var widget = Add(title, defaultValue);
            widget.Entry.SetPersistent();
            return widget;
        }

        public SuppliedValueWidget<byte[]> AddRaw(string title, Func<byte[]> valueSupplier)
        {
            return m_helper.AddRaw(title, valueSupplier);
        }

        public SuppliedValueWidget<string> AddString(string title, Func<string> valueSupplier)
        {
            return m_helper.AddString(title, valueSupplier);
        }

        public SuppliedValueWidget<string[]> AddStringArray(string title, Func<string[]> valueSupplier)
        {
            return m_helper.AddStringArray(title, valueSupplier);
        }

        public override void BuildInto(NetworkTable parentTable, NetworkTable metaTable)
        {
            BuildMetadata(metaTable);
            var table = parentTable.GetSubTable(Title);
            table.GetEntry(".type").SetString("ShuffleboardLayout");
            foreach (var component in Components)
            {
                component.BuildInto(table, metaTable.GetSubTable(component.Title));
            }
        }

        public ShuffleboardLayout GetLayout(string title, string type)
        {
            return m_helper.GetLayout(title, type);
        }

        public ShuffleboardLayout GetLayout(string title, ILayoutType layoutType)
        {
            return m_helper.GetLayout(title, layoutType.LayoutName);
        }

        public ShuffleboardLayout GetLayout(string title)
        {
            return m_helper.GetLayout(title);
        }
    }
}
