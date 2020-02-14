using System;
using System.Collections.Generic;
using System.Linq;
using Hal;
using NetworkTables;

namespace WPILib.Shuffleboard
{
    internal sealed class ShuffleboardInstance : IShuffleboardRoot
    {
        private readonly Dictionary<string, ShuffleboardTab> m_tabs = new Dictionary<string, ShuffleboardTab>();

        private bool m_tabsChanged = false;
        private readonly NetworkTable m_rootTable;
        private readonly NetworkTable m_rootMetaTable;
        private readonly NetworkTableEntry m_selectedTabEntry;

        internal ShuffleboardInstance(NetworkTableInstance ntInstance)
        {
            m_rootTable = ntInstance.GetTable(Shuffleboard.kBaseTableName);
            m_rootMetaTable = m_rootTable.GetSubTable(".metadata");
            m_selectedTabEntry = m_rootMetaTable.GetEntry("Selected");
            UsageReporting.Report(ResourceType.Shuffleboard, 0);
        }

        public void DisableActuatorWidgets()
        {
            ApplyToAllComplexWidgets(x => x.DisableIfActuator());
        }

        public void EnableActuatorWidgets()
        {
            ApplyToAllComplexWidgets(x => x.EnableIfActuator());
        }

        public ShuffleboardTab GetTab(string title)
        {
            if (!m_tabs.TryGetValue(title, out var tab))
            {
                tab = new ShuffleboardTab(this, title);
                m_tabs.Add(title, tab);
                m_tabsChanged = true;
            }
            return tab;
        }

        public void SelectTab(int index)
        {
            m_selectedTabEntry.ForceSetDouble(index);
        }

        public void SelectTab(string title)
        {
            m_selectedTabEntry.ForceSetString(title);
        }

        public void Update()
        {
            if (m_tabsChanged)
            {
                var tabTitles = m_tabs.Values.Select(x => x.Title).ToArray();
                m_rootMetaTable.GetEntry("Tabs").ForceSetStringArray(tabTitles);
                m_tabsChanged = false;
            }

            foreach (var tab in m_tabs.Values)
            {
                var title = tab.Title;
                tab.BuildInto(m_rootTable, m_rootMetaTable.GetSubTable(title));
            }
        }

        private void ApplyToAllComplexWidgets(Action<ComplexWidget> func)
        {
            foreach (var tab in m_tabs.Values)
            {
                Apply(tab, func);
            }
        }

        private void Apply(IShuffleboardContainer container, Action<ComplexWidget> func)
        {
            foreach (var component in container.Components)
            {
                if (component is ComplexWidget complexWidget)
                {
                    func(complexWidget);
                }
                if (component is IShuffleboardContainer shuffleContainer)
                {
                    Apply(shuffleContainer, func);
                }
            }
        }
    }
}
