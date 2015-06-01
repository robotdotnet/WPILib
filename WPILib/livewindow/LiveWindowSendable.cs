
namespace WPILib.livewindow
{
    /// <summary>
    /// Live Window Sendable is a special type of object sendable to the live window.
    /// </summary>
    public interface LiveWindowSendable : Sendable
    {
        /// <summary>
        /// Update the table for this sendable object with the latest
        /// values.
        /// </summary>
        void UpdateTable();

        /// <summary>
        /// Start having this sendable object automatically respond to
        /// value changes reflect the value on the table.
        /// </summary>
        void StartLiveWindowMode();

        /// <summary>
        /// Stop having this sendable object automatically respond to value changes.
        /// </summary>
        void StopLiveWindowMode();
    }
}
