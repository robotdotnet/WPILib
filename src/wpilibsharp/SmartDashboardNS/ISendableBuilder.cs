using NetworkTables;
using System;

namespace WPILib.SmartDashboardNS
{
    public interface ISendableBuilder
    {
        string SmartDashboardType { set; }
        bool IsActuator { set; }
        Action SafeState { set; }
        Action UpdateTable { set; }
        NetworkTableEntry GetEntry(string key);

        void AddBooleanProperty(string key, Func<bool>? getter, Action<bool>? setter);
        void AddDoubleProperty(string key, Func<double>? getter, Action<double>? setter);
        void AddStringProperty(string key, Func<string>? getter, Action<string>? setter);
        void AddBooleanArrayProperty(string key, Func<bool[]>? getter, Action<bool[]>? setter);
        void AddDoubleArrayProperty(string key, Func<double[]>? getter, Action<double[]>? setter);
        void AddStringArrayProperty(string key, Func<string[]>? getter, Action<string[]>? setter);
        void AddRawProperty(string key, Func<byte[]>? getter, Action<byte[]>? setter);
        void AddValueProperty(string key, Func<NetworkTableValue>? getter, Action<NetworkTableValue>? setter);
    }
}
