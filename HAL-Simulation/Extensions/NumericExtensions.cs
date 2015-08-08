using System;

namespace HAL_Simulator.Extensions
{
    public static class ConversionExtensions
    {
        public static double ToRadians(this double degrees)
        {
            return (Math.PI / 180) * degrees;
        }

        public static double ToDegrees(this double radians)
        {
            return (180/Math.PI)*radians;
        }

        public static double ToInches(this double meters)
        {
            return meters * 39.3701;
        }

        public static double ToMeters(this double inches)
        {
            return inches * 0.0254;
        }
    }
}
