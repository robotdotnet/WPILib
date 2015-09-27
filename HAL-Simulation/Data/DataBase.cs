using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
