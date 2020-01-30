using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using WPILib.SmartDashboard;

namespace WPILib2.Commands
{
    public abstract class CommandBase : Sendable, Command
    {
        public HashSet<Subsystem> Requirements { get; protected set; } = new HashSet<Subsystem>();

        protected CommandBase()
        {
            var name = this.GetType().Name;
            SendableRegistry.Instance.Add(this, name);
        }

        public void AddRequirements(params Subsystem[] requirements)
        {
            foreach (var r in requirements)
            {
                Requirements.Add(r);
            }
        }

        public void InitSendable(SendableBuilder builder)
        {
            builder.SmartDashboardType = "Command";
            builder.AddStringProperty(".name", () => Name, null);
            builder.AddBooleanProperty("running", () => ((Command)this).IsScheduled, value =>
            {
                Command c = this;
                if (value)
                {
                    if (!c.IsScheduled)
                    {
                        c.Schedule();
                    }
                }
                else
                {
                    if (c.IsScheduled)
                    {
                        c.Cancel();
                    }
                }
            });
        }

        public string Name
        {
            get => SendableRegistry.Instance.GetName(this) ?? GetType().Name;
            set => SendableRegistry.Instance.SetName(this, value);
        }

        
        [DisallowNull]
        public string? Subsystem
        {
            get => SendableRegistry.Instance.GetSubsystem(this);
            set
            {
                if (value == null) return;
                SendableRegistry.Instance.SetSubsystem(this, value);
            }
        }
    }
}
