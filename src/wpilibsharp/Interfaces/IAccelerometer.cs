using Hal;
using UnitsNet;

namespace WPILib.Interfaces
{
    public interface IAccelerometer
    {
        AccelerometerRange Range { set; }
        Acceleration X { get; }
        Acceleration Y { get; }
        Acceleration Z { get; }
    }
}
