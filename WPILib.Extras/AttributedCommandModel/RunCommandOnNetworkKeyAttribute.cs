using System;

namespace WPILib.Extras.AttributedCommandModel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class RunCommandOnNetworkKeyAttribute : RunCommandAttribute
    {
        public string TableName { get; }
        public string Key { get; }
        public ButtonMethod ButtonMethod { get; }

        public RunCommandOnNetworkKeyAttribute(string tableName, string key, ButtonMethod method)
        {
            TableName = tableName;
            Key = key;
            ButtonMethod = method;
        }
    }
}
