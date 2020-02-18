using NetworkTables;
using WPILib.SmartDashboardNS;

namespace WPILib.ShuffleboardNS
{
    public sealed class ComplexWidget : ShuffleboardWidget<ComplexWidget>
    {
        private readonly ISendable m_sendable;
        private SendableBuilderImpl? m_builder;

        internal ComplexWidget(IShuffleboardContainer parent, string title, ISendable sendable)
            : base(parent, title)
        {

            m_sendable = sendable;
        }

        public override void BuildInto(NetworkTable parentTable, NetworkTable metaTable)
        {
            BuildMetadata(metaTable);
            if (m_builder == null)
            {
                m_builder = new SendableBuilderImpl();
                m_builder.Table = parentTable.GetSubTable(Title);
                m_sendable.InitSendable(m_builder);
                m_builder.StartListeners();
            }
            m_builder.TriggerUpdateTable();
        }

        internal void EnableIfActuator()
        {
            if (m_builder != null && m_builder.IsActuator)
            {
                m_builder.StartLiveWindowMode();
            }
        }

        internal void DisableIfActuator()
        {
            if (m_builder != null && m_builder.IsActuator)
            {
                m_builder.StopLiveWindowMode();
            }
        }
    }
}
