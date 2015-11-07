using System;

namespace WPILib.Extras.AttributedCommandModel
{
    /// <summary>
    /// Run a command on a <see cref="Buttons.NetworkButton"/> event.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class RunCommandOnNetworkKeyAttribute : RunCommandAttribute
    {
        /// <summary>
        /// Gets the table name the command runs on. 
        /// </summary>
        public string TableName { get; }
        /// <summary>
        /// Gets the network tables key the command runs on.
        /// </summary>
        public string Key { get; }
        /// <summary>
        /// Gets the <see cref="ButtonMethod"/> the command runs on.
        /// </summary>
        public ButtonMethod ButtonMethod { get; }

        /// <summary>
        /// Creates a new <see cref="RunCommandOnNetworkKeyAttribute"/>.
        /// </summary>
        /// <param name="tableName">The network table to run on.</param>
        /// <param name="key">The network table key to run the command on.</param>
        /// <param name="method">The <see cref="ButtonMethod"/> to run the command on.</param>
        public RunCommandOnNetworkKeyAttribute(string tableName, string key, ButtonMethod method)
        {
            TableName = tableName;
            Key = key;
            ButtonMethod = method;
        }
    }
}
