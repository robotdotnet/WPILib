using System;

namespace WPILib.Interfaces
{
    /// <summary>
    /// Interface for yaw rate gyros
    /// </summary>
    public interface IGyro : IDisposable
    {
        /// <summary>
        /// Initialize the gyro.
        /// </summary>
        /// <remarks>
        /// Calibrate the gyro by running for a number of samples and
        /// computing the center value. Then use the center value as the
        /// Accumulator center value for subsequent measurements. It's important to
        /// make sure that the robot is not moving while the centering calulations are
        /// in progress, this is typically done when the robot is first turned on while
        /// it's sitting at rest before the competition starts.
        /// </remarks>
        void Calibrate();

        /// <summary>
        /// Reset the gyro.
        /// </summary>
        /// <remarks>
        /// Resets the gyro to a heading of zero. This can be used if there is significant
        /// drift in the gyro and it needs to be recalibrated after it has been running.
        /// </remarks>
        void Reset();

        /// <summary>
        /// Return the actual angle in degrees that the robot is currently facing.
        /// </summary>
        /// <remarks>
        /// The angle is based on the current accumulator value corrected by the
        /// oversampling rate, the gyro type and the A/D calibration values. The angle
        /// is continouus, that is it will continue from 360 to 361 degrees. This
        /// allows algorithms that wouldn't want to see a discontinuity in the gyro
        /// output as it sweeps past from 360 to 0 on the second time around.
        /// </remarks>
        /// <returns>The current heading of the robot in degrees. This heading is
        /// based on integration of the returned rate of the gyro.</returns>
        double GetAngle();

        /// <summary>
        /// Returns the rate of rotation of the gyro.
        /// </summary>
        /// <remarks>
        /// The rate is based on the most recent reading of the gyro analog value.
        /// </remarks>
        /// <returns>The current rate in degrees per second.</returns>
        double GetRate();
    }
}
