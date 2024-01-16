using System;
using System.Text.Json;
using UnitsNet.NumberExtensions.NumberToAngle;
using WPIMath.Geometry;

namespace DesktopDev;

class Program
{
    static void Main(string[] args)
    {
        Rotation2d rot = new(5.Radians());
        string serialized = JsonSerializer.Serialize(rot);
        Console.WriteLine(serialized);

        Rotation2d r = JsonSerializer.Deserialize<Rotation2d>(serialized);
        Console.WriteLine(r.Angle.Radians);
    }
}
