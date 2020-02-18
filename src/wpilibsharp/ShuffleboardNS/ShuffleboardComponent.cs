using System;
using System.Collections.Generic;
using NetworkTables;

namespace WPILib.ShuffleboardNS
{
    public abstract class ShuffleboardComponent : IShuffleboardValue
    {
        private string? m_type;

        //protected Dictionary<string, object>? m_properties;

        protected bool MetadataDirty { get; set; } = true;

        protected ShuffleboardComponent(IShuffleboardContainer parent, string title, string? type = null)
        {
            Parent = parent;
            Title = title;
            m_type = type;
        }

        public IShuffleboardContainer Parent { get; }

        public string? Type
        {
            get => m_type;
            protected set
            {
                m_type = value;
                MetadataDirty = true;
            }
        }

        public string Title { get; }

        protected IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        protected int Column { get; set; } = -1;
        protected int Row { get; set; } = -1;
        protected int Width { get; set; } = -1;

        protected int Height { get; set; } = -1;

        public abstract void BuildInto(NetworkTable parentTable, NetworkTable metaTable);
    }

    public abstract class ShuffleboardComponent<T> : ShuffleboardComponent where T : ShuffleboardComponent<T>
    {
        protected ShuffleboardComponent(IShuffleboardContainer parent, string title, string? type = null)
          : base(parent, title, type)
        {
        }


        public T WithProperties(IDictionary<string, object> properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            Properties.Clear();
            foreach (var prop in properties)
            {
                Properties.Add(prop.Key, prop.Value);
            }
            MetadataDirty = true;
            return (T)this;
        }

        public T WithPosition(int columnIndex, int rowIndex)
        {
            Column = columnIndex;
            Row = rowIndex;
            MetadataDirty = true;
            return (T)this;
        }

        public T WithSize(int width, int height)
        {
            Width = width;
            Height = height;
            MetadataDirty = true;
            return (T)this;
        }

        protected void BuildMetadata(NetworkTable metaTable)
        {
            if (metaTable == null)
            {
                throw new ArgumentNullException(nameof(metaTable));
            }

            if (!MetadataDirty) return;

            var preferredComponentEntry = metaTable.GetEntry("PreferredComponent");

            if (Type == null)
            {
                preferredComponentEntry.Delete();
            }
            else
            {
                preferredComponentEntry.ForceSetString(Type);
            }

            var sizeEntry = metaTable.GetEntry("Size");

            if (Width <= 0 || Height <= 0)
            {
                sizeEntry.Delete();
            }
            else
            {
                sizeEntry.SetDoubleArray(stackalloc double[] { Width, Height });
            }

            var positionEntry = metaTable.GetEntry("Position");
            if (Column < 0 || Row < 0)
            {
                positionEntry.Delete();
            }
            else
            {
                positionEntry.SetDoubleArray(stackalloc double[] { Column, Row });
            }

            if (Properties != null)
            {
                var propTable = metaTable.GetSubTable("Properties");
                foreach (var prop in Properties)
                {
                    propTable.GetEntry(prop.Key).SetValue(prop.Value);
                }
            }
            MetadataDirty = false;
        }


    }
}
