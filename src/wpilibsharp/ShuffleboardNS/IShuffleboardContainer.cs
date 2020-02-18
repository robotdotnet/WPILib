using System;
using System.Collections.Generic;
using WPILib.SmartDashboardNS;

namespace WPILib.ShuffleboardNS
{
    public interface IShuffleboardContainer : IShuffleboardValue
    {
        List<ShuffleboardComponent> Components { get; }

        ShuffleboardLayout GetLayout(string title, string type);

        ShuffleboardLayout GetLayout(string title, ILayoutType layoutType);

        ShuffleboardLayout GetLayout(string title);

        ComplexWidget Add(string title, ISendable sendable);

        ComplexWidget Add(ISendable sendable);

        SimpleWidget Add<T>(string title, T defaultValue);

        SuppliedValueWidget<T> Add<T>(string title, Func<T> valueSupplier);

        SuppliedValueWidget<string> AddString(string title, Func<string> valueSupplier);

        SuppliedValueWidget<double> AddNumber(string title, Func<double> valueSupplier);

        SuppliedValueWidget<bool> AddBoolean(string title, Func<bool> valueSupplier);

        SuppliedValueWidget<string[]> AddStringArray(string title, Func<string[]> valueSupplier);

        SuppliedValueWidget<double[]> AddDoubleArray(string title, Func<double[]> valueSupplier);

        SuppliedValueWidget<bool[]> AddBooleanArray(string title, Func<bool[]> valueSupplier);

        SuppliedValueWidget<byte[]> AddRaw(string title, Func<byte[]> valueSupplier);

        SimpleWidget AddPersistent<T>(string title, T defaultValue);
    }
}
