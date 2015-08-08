using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator
{
    public class DCMotor
    {

        // Motor constants
        public double MaxTorque { get; protected set; } // in N-m
        public double MaxSpeed { get; protected set; } // in Radians per second

        public double Efficiency { get; set; } // Efficiency percentage

        public double Load { get; set; }// Load in Newtons

        public static double PoundsToNewtons(double pounds)
        {
            return pounds*4.4482216;
        }


        // Convenience methods for constructing common motors.
        public static DCMotor MakeRS775()
        {
            double maxTorque = 0.796543046799624;
            double maxSpeed = 1361.3568149999999;
            return new DCMotor(maxTorque, maxSpeed);
        }

        public static DCMotor MakeRS550()
        {
            double maxTorque = 0.49819248184143144;
            double maxSpeed = 2021.0912715;
            return new DCMotor(maxTorque, maxSpeed);
        }

        public static DCMotor MakeCIM()
        {
            double maxTorque = 2.42917383066552;
            double maxSpeed = 556.06189905;
            return new DCMotor(maxTorque, maxSpeed);
        }

        public static DCMotor MakeMiniCIM()
        {
            double maxTorque = 1.4;
            double maxSpeed = 649.262481;
            return new DCMotor(maxTorque, maxSpeed);
        }

        public static DCMotor MakeBag()
        {
            double maxTorque = 0.4;
            double maxSpeed = 1466.07657;
            return new DCMotor(maxTorque, maxSpeed);
        }

        /*
         * Make a transmission.
         * 
         * @param motors The motor type attached to the transmission.
         * @param num_motors The number of motors in this transmission.
         * @param gear_reduction The reduction of the transmission.
         * @param efficiency The efficiency of the transmission.
         * @return A DCMotor representing the combined transmission.
         */
        public static DCMotor MakeTransmission(DCMotor motor, int num_motors,
                double gear_reduction, double efficiency)
        {
            return new DCMotor(motor.MaxTorque*num_motors*gear_reduction, motor.MaxSpeed/gear_reduction)
            {
                Efficiency = efficiency
            };
        }

        /**
         * Simulate a simple DC motor.
         * 
         * @param kt
         *            Torque constant (N*m / amp)
         * @param kv
         *            Voltage constant (rad/sec / V)
         * @param resistance
         *            (ohms)
         * @param inertia
         *            (kg*m^2)
         */
        public DCMotor(double maxTorque, double maxSpeed)
        {
            MaxSpeed = maxSpeed;
            MaxTorque = maxTorque;
        }
    }
}
