using System.Runtime.CompilerServices;

namespace HAL.Simulator.Data
{
    /// <summary>
    /// The data base, without a notifier
    /// </summary>
    public abstract class DataBase
    {
        /// <summary>
        /// Called when any property is changed.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(dynamic value, [CallerMemberName] string propertyName = null)
        {
        }


        /// <summary>
        /// Resets the data for the specified set of data.
        /// </summary>
        public abstract void ResetData();
    }
}
