using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator
{
    public class PWMHelpers
    {
        public static double ReverseJaguarPWM(double value)
        {
            ushort max_pos_pwm = 1809;
            ushort min_pos_pwm = 1006;
            ushort pos_scale = 803;
            ushort max_neg_pwm = 1004;
            ushort min_neg_pwm = 196;
            ushort neg_scale = 808;

            return rev_pwm(value, max_pos_pwm, min_pos_pwm, pos_scale, max_neg_pwm, min_neg_pwm, neg_scale);
        }

        public static double ReverseTalonPWM(double value)
        {
            ushort max_pos_pwm = 1536;
            ushort min_pos_pwm = 1012;
            ushort pos_scale = 524;
            ushort max_neg_pwm = 1010;
            ushort min_neg_pwm = 488;
            ushort neg_scale = 522;

            return rev_pwm(value, max_pos_pwm, min_pos_pwm, pos_scale, max_neg_pwm, min_neg_pwm, neg_scale);
        }

        public static double ReverseTalonSRXPWM(double value)
        {
            ushort max_pos_pwm = 1503;
            ushort min_pos_pwm = 1000;
            ushort pos_scale = 503;
            ushort max_neg_pwm = 998;
            ushort min_neg_pwm = 496;
            ushort neg_scale = 502;

            return rev_pwm(value, max_pos_pwm, min_pos_pwm, pos_scale, max_neg_pwm, min_neg_pwm, neg_scale);
        }

        public static double ReverseVictorPWM(double value)
        {
            ushort max_pos_pwm = 1526;
            ushort min_pos_pwm = 1006;
            ushort pos_scale = 520;
            ushort max_neg_pwm = 1004;
            ushort min_neg_pwm = 525;
            ushort neg_scale = 479;

            return rev_pwm(value, max_pos_pwm, min_pos_pwm, pos_scale, max_neg_pwm, min_neg_pwm, neg_scale);
        }

        public static double ReverseVictorSPPWM(double value)
        {
            ushort max_pos_pwm = 1503;
            ushort min_pos_pwm = 1000;
            ushort pos_scale = 503;
            ushort max_neg_pwm = 998;
            ushort min_neg_pwm = 496;
            ushort neg_scale = 502;

            return rev_pwm(value, max_pos_pwm, min_pos_pwm, pos_scale, max_neg_pwm, min_neg_pwm, neg_scale);
        }

        public static double ReverseByType(dynamic defining_val, dynamic value = null)
        {
            var halData = SimData.halData;
            string type;
            double trans_val;
            if (defining_val is string)
            {
                type = defining_val.ToLower();
                if (value != null)
                {
                    trans_val = (double)value;
                }
                else
                {
                    throw new ArgumentNullException(nameof(value), "Must have a value to translate");
                }
            }
            else
            {
                type = halData["pwm"][defining_val]["type"];
                trans_val = (double)halData["pwm"][defining_val]["raw_value"];
            }

            switch (type)
            {
                case "jaguar":
                    return ReverseJaguarPWM((double)trans_val);
                case "talon":
                    return ReverseTalonPWM((double) trans_val);
                case "talonsrx":
                    return ReverseTalonSRXPWM((double)trans_val);
                case "victor":
                    return ReverseVictorPWM((double)trans_val);
                case "victorsp":
                    return ReverseVictorSPPWM((double)trans_val);
                case "servo":
                    return 0.0;
                default:
                    if (type != null)
                        throw new InvalidOperationException($"The type {type} is not a usable motor controller type");
                    return 0.0;
            }
        }

        public static double rev_pwm(double value, ushort max_pos_pwm, ushort min_pos_pwm, ushort pos_scale,
            ushort max_neg_pwm, ushort min_neg_pwm, ushort neg_scale)
        {
            if (value > max_pos_pwm)
                return 1.0;
            else if (value < min_neg_pwm)
                return -1.0;
            else if (value > min_pos_pwm)
                return (value - min_pos_pwm)/ pos_scale;
            else if (value < max_neg_pwm)
                return (value - max_neg_pwm)/neg_scale;
            else
                return 0.0;
        }
    }
}
