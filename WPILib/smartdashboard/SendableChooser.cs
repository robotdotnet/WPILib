using System;
using NetworkTablesDotNet.NetworkTables2.Type;
using NetworkTablesDotNet.NetworkTables2.Util;
using NetworkTablesDotNet.Tables;
using WPILib.Commands;

namespace WPILib.smartdashboard
{
    /// <summary>
    /// The <see cref="SendableChooser"/> class is a useful tool for presenting a 
    /// selection of options to the <see cref="SmartDashboard"/>
    /// <para> </para>
    ///     For instance, you may wish to be able to select between multiple
    /// autonomous modes. You can do this by putting every possible <see cref="Command"/>
    /// you want to run as an autonomous into a <see cref="SendableChooser"/> and then put
    /// it into the <see cref="SmartDashboard"/> to have a list of options appear on the
    /// laptop. Once autonomous starts, simply ask the <see cref="SendableChooser"/> what
    /// the selected value is.
    /// </summary>
    public class SendableChooser : ISendable
    {
        private static readonly string DEFAULT = "default";

        private static readonly string SELECTED = "selected";

        private static readonly string OPTIONS = "options";

        private StringArray m_choices = new StringArray();
        private List m_values = new List();
        private string m_defaultChoice = null;
        private object m_defaultValue = null;

        /// <summary>
        /// Instantiates a <see cref="SendableChooser"/>
        /// </summary>
        public SendableChooser()
        {
        }

        /// <summary>
        /// Adds the given object tot the list of options. On the 
        /// <see cref="SmartDashboard"/> on the desktop, the object will appear as the given name.
        /// </summary>
        /// <param name="name">The name of the option</param>
        /// <param name="obj">The option</param>
        public void AddObject(string name, object obj)
        {
            if (m_defaultChoice == null)
            {
                AddDefault(name, obj);
                return;
            }

            for (int i = 0; i < m_choices.Size(); i++)
            {
                if (m_choices.Get(i).Equals(name))
                {
                    m_choices.Set(i, name);
                    m_values.Set(i, obj);
                    return;
                }
            }

            m_choices.Add(name);
            m_values.Add(obj);

            m_table?.PutValue(OPTIONS, m_choices);
        }

        /// <summary>
        /// Add the given object to the list of options and marks it as the default.
        /// Functionally, this is very close to
        /// <see cref="SendableChooser.AddObject(string, object)"/> except that it will
        /// use this as the default option if none other is explicityly selected.
        /// </summary>
        /// <param name="name">The name of the option</param>
        /// <param name="obj">The option</param>
        public void AddDefault(string name, object obj)
        {
            if (name == null)
            {
                throw new NullReferenceException("Name cannot be null");
            }

            m_defaultChoice = name;
            m_defaultValue = obj;
            m_table?.PutString(DEFAULT, m_defaultChoice);
            AddObject(name, obj);
        }

        /// <summary>
        /// Returns the selected option. If there is none selected, it will return
        /// the default. If there is none selected, then it will return null.
        /// </summary>
        /// <returns></returns>
        public object GetSelected()
        {
            string selected = m_table.GetString(SELECTED, null);
            for (int i = 0; i < m_values.Size(); ++i)
            {
                if (m_choices.Get(i).Equals(selected))
                {
                    return m_values.Get(i);
                }
            }
            return m_defaultValue;
        }

        private ITable m_table;
        public void InitTable(ITable subtable)
        {
            m_table = subtable;
            if (m_table != null)
            {
                m_table.PutValue(OPTIONS, m_choices);
                if (m_defaultChoice != null)
                {
                    m_table.PutString(DEFAULT, m_defaultChoice);
                }
            }
        }

        public ITable Table => m_table;

        public string SmartDashboardType => "String Chooser";
    }
}
