using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using WPILib.SmartDashboard;

namespace WPILib2.Commands
{
    public abstract class CommandBase : ISendable, ICommand
    {
        public HashSet<ISubsystem> Requirements { get; protected set; } = new HashSet<ISubsystem>();

        protected CommandBase()
        {
            var name = this.GetType().Name;
            SendableRegistry.Instance.Add(this, name);
        }

        public void AddRequirements(params ISubsystem[] requirements)
        {
            foreach (var r in requirements)
            {
                Requirements.Add(r);
            }
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "Command";
            builder.AddStringProperty(".name", () => Name, null);
            builder.AddBooleanProperty("running", () => ((ICommand)this).IsScheduled, value =>
            {
                ICommand c = this;
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

        public virtual void Initialize()
        {
        }

        public virtual void Execute()
        {
        }

        public virtual void End(bool interrupted)
        {

        }

        public virtual void Schedule(bool interruptible = true)
        {
        }

        public virtual void Cancel()
        {
        }

        public virtual bool HasRequirement(ISubsystem requirement)
        {
            return Requirements.Contains(requirement);
        }

        public virtual string Name
        {
            get => SendableRegistry.Instance.GetName(this) ?? GetType().Name;
            set => SendableRegistry.Instance.SetName(this, value);
        }


        [DisallowNull]
        public virtual string? Subsystem
        {
            get => SendableRegistry.Instance.GetSubsystem(this);
            set
            {
                if (value == null) return;
                SendableRegistry.Instance.SetSubsystem(this, value);
            }
        }

        public virtual bool RunsWhenDisabled { get; set; } = false;

        public virtual bool IsFinished { get; set; } = false;

        public virtual bool IsScheduled => false;
    }
}
