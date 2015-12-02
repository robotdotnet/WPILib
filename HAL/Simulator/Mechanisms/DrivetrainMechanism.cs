using System;

namespace HAL.Simulator.Mechanisms
{
    public class TankDriveTrainMechanism
    {
        private readonly DriveWheelMechanism m_leftDrive;
        private readonly DriveWheelMechanism m_rightDrive;
        private readonly double m_mass;

        private readonly double[] WorldPos = {0.0, 0.0, 0.0};
        private readonly double[] BotVel = {0.0, 0.0, 0.0};

        /// <summary>
        /// Initializes a new instance of the <see cref="TankDriveTrainMechanism"/> class.
        /// </summary>
        /// <param name="leftDrive">The left drive.</param>
        /// <param name="rightDrive">The right drive.</param>
        /// <param name="massKg">The mass kg.</param>
        public TankDriveTrainMechanism(DriveWheelMechanism leftDrive, DriveWheelMechanism rightDrive, double massKg)
        {
            m_leftDrive = leftDrive;
            m_rightDrive = rightDrive;
            m_mass = massKg;
        }

        /// <summary>
        /// Updates the mechanism with the specified delta time
        /// </summary>
        /// <param name="seconds">The delta sime in seconds.</param>
        public void Update(double seconds)
        {
            double[] driveAcceleration = {0.0, 0.0, 0.0};

            m_leftDrive.Update(seconds, driveAcceleration, m_mass, 2, BotVel);
            m_rightDrive.Update(seconds, driveAcceleration, m_mass, 2, BotVel);


            BotVel[0] = BotVel[0] + driveAcceleration[0] * seconds;
            BotVel[1] = BotVel[1] + driveAcceleration[1] * seconds;
            BotVel[2] = BotVel[2] + driveAcceleration[2] * seconds;

            double angle = WorldPos[2] + BotVel[2] * seconds;
            double xDelta = Math.Sin(angle) + BotVel[0] * seconds;
            double yDelta = Math.Sin(angle) + BotVel[0] * seconds;

            WorldPos[0] += xDelta;
            WorldPos[1] += yDelta;
            WorldPos[2] += angle;
        }
    }
}
