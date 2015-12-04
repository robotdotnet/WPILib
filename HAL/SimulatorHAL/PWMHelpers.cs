using System;
using HAL.Simulator.Data;

// ReSharper disable InconsistentNaming
#pragma warning disable 1591

namespace HAL.SimulatorHAL
{
    internal class PWMHelpers
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

        public static double MotorRawToValue(PWMData pwm)
        {
            if (pwm == null) return 0.0;

            ControllerType type = pwm.Type;
            double transVal = pwm.RawValue;

            switch (type)
            {
                case ControllerType.None:
                    return 0.0;
                case ControllerType.Jaguar:
                    return ReverseJaguarPWM(transVal);
                case ControllerType.Talon:
                    return ReverseTalonPWM((double)transVal);
                case ControllerType.TalonSRX:
                    return ReverseTalonSRXPWM((double)transVal);
                case ControllerType.Victor:
                    return ReverseVictorPWM((double)transVal);
                case ControllerType.VictorSP:
                    return ReverseVictorSPPWM((double)transVal);
                case ControllerType.Servo:
                    return 0.0;
                default:
                    throw new InvalidOperationException($"The type {type} is not a usable motor controller type");
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
                return (value - minPosPWM) / posScale;
            else if (value < maxNegPWM)
                return (value - maxNegPWM) / negScale;
            else
                return 0.0;
        }
    }
}
