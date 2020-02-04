using System;
using System.Collections.Generic;
using NetworkTables;

namespace WPILib.Shuffleboard
{
    public abstract class ShuffleboardComponent : IShuffleboardValue
    {
        private string? m_type;

        protected Dictionary<string, object>? m_properties;

        protected bool m_metadataDirty = true;

        protected int m_column = -1;
        protected int m_row = -1;
        protected int m_width = -1;
        protected int m_height = -1;

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
                m_metadataDirty = true;
            }
        }

        public string Title { get; }

        internal Dictionary<string, object>? Properites => m_properties;

        public abstract void BuildInto(NetworkTable parentTable, NetworkTable metaTable);
    }

    public abstract class ShuffleboardComponent<T> : ShuffleboardComponent where T : ShuffleboardComponent<T>
    {
        protected ShuffleboardComponent(IShuffleboardContainer parent, string title, string? type = null)
          : base(parent, title, type)
        {
        }


        public T WithProperties(Dictionary<string, object>? properties)
        {
            m_properties = properties;
            m_metadataDirty = true;
            return (T)this;
        }

        public T WithPosition(int columnIndex, int rowIndex)
        {
            m_column = columnIndex;
            m_row = rowIndex;
            m_metadataDirty = true;
            return (T)this;
        }

        public T WithSize(int width, int height)
        {
            m_width = width;
            m_height = height;
            m_metadataDirty = true;
            return (T)this;
        }

        protected void BuildMetadata(NetworkTable metaTable)
        {
            if (!m_metadataDirty) return;

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

            if (m_width <= 0 || m_height <= 0)
            {
                sizeEntry.Delete();
            }
            else
            {
                sizeEntry.SetDoubleArray(stackalloc double[] { m_width, m_height });
            }

            var positionEntry = metaTable.GetEntry("Position");
            if (m_column < 0 || m_row < 0)
            {
                positionEntry.Delete();
            }
            else
            {
                positionEntry.SetDoubleArray(stackalloc double[] { m_column, m_row });
            }

            if (Properites != null)
            {
                var propTable = metaTable.GetSubTable("Properties");
                foreach (var prop in Properites)
                {
                    propTable.GetEntry(prop.Key).SetValue(prop.Value);
                }
            }
            m_metadataDirty = false;
        }


    }
}
