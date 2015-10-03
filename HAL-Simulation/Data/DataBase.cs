using System.Runtime.CompilerServices;

namespace HAL_Simulator.Data
{
    public abstract class DataBase
    {
        protected virtual void OnPropertyChanged(dynamic value, [CallerMemberName] string propertyName = null)
        {
        }

        public abstract void ResetData();
    }
}
