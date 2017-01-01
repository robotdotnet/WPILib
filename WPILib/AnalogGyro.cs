using System;
using HAL.Base;
using WPILib.Interfaces;
using WPILib.LiveWindow;
using static HAL.Base.HAL;
using static HAL.Base.HALAnalogGyro;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// Class for interfacing with an analog gyro to get robot heading.
    /// </summary>
    /// <remarks>
    /// Use a rate gyro to return the robots heading relative to a starting position.
    /// The Gyro class tracks the robots heading based on the starting position.As
    /// the robot rotates the new heading is computed by integrating the rate of
    /// rotation returned by the sensor.When the class is instantiated, it does a
    /// short calibration routine where it samples the gyro while at rest to
    /// determine the default offset.This is subtracted from each sample to
    /// determine the heading.
    /// </remarks>
    public class AnalogGyro : GyroBase
    {
        private const double DefaultVoltsPerDegreePerSecond = 0.007;

        /// <summary>
        /// The <see cref="WPILib.AnalogInput"/> that this gyro uses.
        /// </summary>
        protected AnalogInput AnalogInput;

        private int m_gyroHandle = 0;
        private readonly bool m_channelAllocated = false;

        /// <summary>
        /// Initialize they gyro. Calibration is handled by <see cref="Calibrate"/>.
        /// </summary>
        public void InitGyro()
        {
            int status = 0;
            if (m_gyroHandle == 0)
            {
                m_gyroHandle = HAL_InitializeAnalogGyro(AnalogInput.m_halHandle, ref status);
                CheckStatus(status);
            }

            HAL_SetupAnalogGyro(m_gyroHandle, ref status);
            CheckStatusForceThrow(status);

            HAL.Base.HAL.Report(ResourceType.kResourceType_Gyro, AnalogInput.Channel);
            LiveWindow.LiveWindow.AddSensor("AnalogGyro", AnalogInput.Channel, this);
        }

        /// <inheritdoc/>
        public override void Calibrate()
        {
            if (RobotBase.IsSimulation)
            {
                //In simulation, we do not have to do anything here.
                return;
            }

            int status = 0;
            HAL_CalibrateAnalogGyro(m_gyroHandle, ref status);

        }

        /// <summary>
        /// Creates a new Analog Gyro on the specified channel.
        /// </summary>
        /// <param name="channel">The channel the gyro is on (Must be an accumulator channel). [0..1] on RIO.</param>
        public AnalogGyro(int channel)
        {
            AnalogInput = new AnalogInput(channel);
            try
            {
                InitGyro();
            }
            catch
            {
                AnalogInput.Dispose();
                throw;
            }
            Calibrate();
            m_channelAllocated = true;
        }

        /// <summary>
        /// Creates a new Analog Gyro with an existing <see cref="WPILib.AnalogInput"/>.
        /// </summary>
        /// <param name="channel">The analog input this gyro is attached to.</param>
        public AnalogGyro(AnalogInput channel)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel), "Analog input must not be null");
            AnalogInput = channel;
            InitGyro();
            Calibrate();
        }

        // TODO: Add offset and center taking constructors

        ///<inheritdoc/>
        public override void Reset()
        {
            int status = 0;
            HAL_ResetAnalogGyro(m_gyroHandle, ref status);
            CheckStatus(status);
        }

        ///<inheritdoc/>
        public override void Dispose()
        {
            if (AnalogInput != null && m_channelAllocated)
            {
                AnalogInput.Dispose();
            }
            HAL_FreeAnalogGyro(m_gyroHandle);
            m_gyroHandle = HALInvalidHandle;
            AnalogInput = null;
            base.Dispose();
        }

        ///<inheritdoc/>
        public override double GetAngle()
        {
            if (m_gyroHandle == HALInvalidHandle)
            {
                return 0.0;
            }
            else
            {
                int status = 0;
                double retVal = HAL_GetAnalogGyroAngle(m_gyroHandle, ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        ///<inheritdoc/>
        public override double GetRate()
        {
            if (m_gyroHandle == HALInvalidHandle)
            {
                return 0.0;
            }
            else
            {
                int status = 0;
                double retVal = HAL_GetAnalogGyroRate(m_gyroHandle, ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        /// <summary>
        /// Gets the offset for the AnalogGyro
        /// </summary>
        public double Offset
        {
            get
            {
                int status = 0;
                double retVal = HAL_GetAnalogGyroOffset(m_gyroHandle, ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        /// <summary>
        /// Gets the center for the AnalogGyro
        /// </summary>
        public int Center
        {
            get
            {
                int status = 0;
                int retVal = HAL_GetAnalogGyroCenter(m_gyroHandle, ref status);
                CheckStatus(status);
                return retVal;
            }
        }

        /// <summary>
        /// Gets or sets the sensitivity of the gyroscope.
        /// </summary>
        public double Sensitivity
        {
            set
            {
                int status = 0;
                HAL_SetAnalogGyroVoltsPerDegreePerSecond(m_gyroHandle, value, ref status);
            }
        }

        internal double Deadband
        {
            set
            {
                int status = 0;
                HAL_SetAnalogGyroDeadband(m_gyroHandle, value, ref status);
                CheckStatus(status);
            }
        }
    }
}
