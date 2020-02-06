using NetworkTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.SmartDashboard
{
    public class SendableBuilderImpl : ISendableBuilder
    {
        public string SmartDashboardType { set => throw new NotImplementedException(); }
        public bool Actuator { set => throw new NotImplementedException(); }
        public Action SafeState { set => throw new NotImplementedException(); }
        public Action UpdateTable { set => throw new NotImplementedException(); }

        public NetworkTable? Table { get; set; }

        public void TriggerUpdateTable()
        {

        }

        public void StartListeners()
        {

        }

        public void StopListeners()
        {

        }

        public void ClearProperties()
        {

        }

        public void AddBooleanArrayProperty(string key, Func<bool[]>? getter, Action<bool[]>? setter)
        {
            throw new NotImplementedException();
        }

        public void AddBooleanProperty(string key, Func<bool>? getter, Action<bool>? setter)
        {
            throw new NotImplementedException();
        }

        public void AddDoubleArrayProperty(string key, Func<double[]>? getter, Action<double[]>? setter)
        {
            throw new NotImplementedException();
        }

        public void AddDoubleProperty(string key, Func<double>? getter, Action<double>? setter)
        {
            throw new NotImplementedException();
        }

        public void AddRawProperty(string key, Func<byte[]>? getter, Action<byte[]>? setter)
        {
            throw new NotImplementedException();
        }

        public void AddStringArrayProperty(string key, Func<string[]>? getter, Action<string[]>? setter)
        {
            throw new NotImplementedException();
        }

        public void AddStringProperty(string key, Func<string>? getter, Action<string>? setter)
        {
            throw new NotImplementedException();
        }

        public void AddValueProperty(string key, Func<NetworkTableValue>? getter, Action<NetworkTableValue>? setter)
        {
            throw new NotImplementedException();
        }

        public NetworkTableEntry GetEntry(string key)
        {
            throw new NotImplementedException();
        }
    }
}
