using NetworkTables.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using WPILib.Interfaces;

namespace WPILib.SmartDashboard
{
    /// <summary>
    /// The <see cref="SendableChooser"/> class is a useful tool for presenting a
    /// selection of options to the <see cref="SmartDashboard"/>.
    /// </summary><remarks>
    /// One use for this is to be able to select between multiple
    /// autonomous modes. You can do this by putting every possible <see cref="Command"/>
    /// you want to run as an autonomous into a <see cref="SendableChooser"/> and then put
    /// it into the <see cref="SmartDashboard"/> to have a list of options appear on the
    /// laptop. Once autonomous starts, simply ask the <see cref="SendableChooser"/> what
    /// the selected value is.
    /// </remarks>
    public class SendableChooser : ISendable
    {
        /// <summary>
        /// Adds the given object to the list of options. On the
        /// <see cref="SmartDashboard"/> on the desktop, the object will appear as the given name.
        /// </summary>
        /// <param name="name">The name of the option</param>
        /// <param name="obj">The option</param>
        public void AddObject(string name, object obj)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Name cannot be null");
            }

            if (m_defaultChoice == null)
            {
                AddDefault(name, obj);
                return;
            }

            m_data[name] = obj;

            Table?.PutStringArray(Options, m_data.Keys.ToArray());
        }

        /// <summary>
        /// Add the given object to the list of options and marks it as the default.
        /// Functionally, this is very close to
        /// <see cref="SendableChooser.AddObject(string, object)">AddObject(...)</see> except that it will
        /// use this as the default option if none other is explicitly selected.
        /// </summary>
        /// <param name="name">The name of the option</param>
        /// <param name="obj">The option</param>
        public void AddDefault(string name, object obj)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name), "Name cannot be null");
            }

            m_defaultChoice = name;
            Table?.PutString(Default, m_defaultChoice);
            AddObject(name, obj);
        }

        /// <summary>
        /// Returns the selected option. If there is none selected, it will return
        /// the default. If there is none selected, then it will return null.
        /// </summary>
        /// <returns>The option selected</returns>
        public object GetSelected()
        {
            object result;
            var contained = m_data.TryGetValue(Table.GetString(Selected, null), out result);
            return contained ? result : m_data[m_defaultChoice];
        }


        /// <inheritdoc/>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            if (Table != null)
            {
                Table.PutStringArray(Options, m_data.Keys.ToArray());
                if (m_defaultChoice != null)
                {
                    Table.PutString(Default, m_defaultChoice);
                }
            }
        }

        /// <inheritdoc/>
        public ITable Table { get; private set; }

        /// <inheritdoc/>
        public string SmartDashboardType => "String Chooser";


        private const string Default = "default";
        private const string Selected = "selected";
        private const string Options = "options";

        private string m_defaultChoice = null;
        private readonly Dictionary<string, object> m_data = new Dictionary<string, object>();
    }
}
