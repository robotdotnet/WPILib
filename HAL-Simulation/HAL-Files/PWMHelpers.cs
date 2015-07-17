using System;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
#pragma warning disable 1591

namespace HAL_Simulator
{
    public class PWMHelpers
    {
        public static double ReverseJaguarPWM(double value)
        {
            ushort maxPosPWM = 1809;
            ushort minPosPWM = 1006;
            ushort posScale = 803;
            ushort maxNegPWM = 1004;
            ushort minNegPWM = 196;
            ushort negScale = 808;

            return rev_pwm(value, maxPosPWM, minPosPWM, posScale, maxNegPWM, minNegPWM, negScale);
        }

        public static double ReverseTalonPWM(double value)
        {
            ushort maxPosPWM = 1536;
            ushort minPosPWM = 1012;
            ushort posScale = 524;
            ushort maxNegPWM = 1010;
            ushort minNegPWM = 488;
            ushort negScale = 522;

            return rev_pwm(value, maxPosPWM, minPosPWM, posScale, maxNegPWM, minNegPWM, negScale);
        }

        public static double ReverseTalonSRXPWM(double value)
        {
            ushort maxPosPWM = 1503;
            ushort minPosPWM = 1000;
            ushort posScale = 503;
            ushort maxNegPWM = 998;
            ushort minNegPWM = 496;
            ushort negScale = 502;

            return rev_pwm(value, maxPosPWM, minPosPWM, posScale, maxNegPWM, minNegPWM, negScale);
        }

        public static double ReverseVictorPWM(double value)
        {
            ushort maxPosPWM = 1526;
            ushort minPosPWM = 1006;
            ushort posScale = 520;
            ushort maxNegPWM = 1004;
            ushort minNegPWM = 525;
            ushort negScale = 479;

            return rev_pwm(value, maxPosPWM, minPosPWM, posScale, maxNegPWM, minNegPWM, negScale);
        }

        public static double ReverseVictorSPPWM(double value)
        {
            ushort maxPosPWM = 1503;
            ushort minPosPWM = 1000;
            ushort posScale = 503;
            ushort maxNegPWM = 998;
            ushort minNegPWM = 496;
            ushort negScale = 502;

            return rev_pwm(value, maxPosPWM, minPosPWM, posScale, maxNegPWM, minNegPWM, negScale);
        }


        public static double ReverseByType(dynamic definingVal, dynamic value = null)
        {
            var halData = SimData.halData;
            string type;
            double transVal;
            if (definingVal is string)
            {
                type = definingVal.ToLower();
                if (value != null)
                {
                    transVal = (double)value;
                }
                else
                {
                    throw new ArgumentNullException(nameof(value), "Must have a value to translate");
                }
            }
            else
            {
                type = halData["pwm"][definingVal]["type"];
                transVal = (double)halData["pwm"][definingVal]["raw_value"];
            }

            switch (type)
            {
                case "jaguar":
                    return ReverseJaguarPWM((double)transVal);
                case "talon":
                    return ReverseTalonPWM((double) transVal);
                case "talonsrx":
                    return ReverseTalonSRXPWM((double)transVal);
                case "victor":
                    return ReverseVictorPWM((double)transVal);
                case "victorsp":
                    return ReverseVictorSPPWM((double)transVal);
                case "servo":
                    return 0.0;
                default:
                    if (type != null)
                        throw new InvalidOperationException($"The type {type} is not a usable motor controller type");
                    return 0.0;
            }
        }

        public static double rev_pwm(double value, ushort maxPosPWM, ushort minPosPWM, ushort posScale,
            ushort maxNegPWM, ushort minNegPWM, ushort negScale)
        {
            if (value > maxPosPWM)
                return 1.0;
            else if (value < minNegPWM)
                return -1.0;
            else if (value > minPosPWM)
                return (value - minPosPWM)/ posScale;
            else if (value < maxNegPWM)
                return (value - maxNegPWM)/negScale;
            else
                return 0.0;
        }
    }
}
