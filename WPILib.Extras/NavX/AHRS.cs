using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetworkTables.Tables;
using WPILib.Interfaces;
using WPILib.LiveWindows;

namespace WPILib.Extras.NavX
{
    public class AHRS : SensorBase, IPIDSource, ILiveWindowSendable
    {
        public enum BoardAxis
        {
            BoardAxisX,
            BoardAxisY,
            BoardAxisZ,
        }

        public class BoardYawAxis
        {
            public BoardAxis BoardAxis;
            public bool Up;
        }

        public enum SerialDataType
        {
            ProcessedData,
            RawData,
        }

        const byte NavxDefaultUpdateRateHz = 60;
        const int YawHistoryLength = 10;
        const short DefaultAccelFsrG = 2;
        const short DefaultGyroFsrDps = 2000;

        /* Processed Data */

        volatile float m_yaw;
        volatile float m_pitch;
        volatile float m_roll;
        volatile float m_compassHeading;
        volatile float m_worldLinearAccelX;
        volatile float m_worldLinearAccelY;
        volatile float m_worldLinearAccelZ;
        volatile float m_mpuTempC;
        volatile float m_fusedHeading;
        volatile float m_altitude;
        volatile float m_baroPressure;
        volatile bool m_isMoving;
        volatile bool m_isRotating;
        volatile float m_baroSensorTempC;
        volatile bool m_altitudeValid;
        volatile bool m_isMagnetometerCalibrated;
        volatile bool m_magneticDisturbance;
        volatile short m_quaternionW;
        volatile short m_quaternionX;
        volatile short m_quaternionY;
        volatile short m_quaternionZ;

        /* Integrated Data */
        float[] m_velocity = new float[3];
        float[] m_displacement = new float[3];


        /* Raw Data */
        volatile short m_rawGyroX;
        volatile short m_rawGyroY;
        volatile short m_rawGyroZ;
        volatile short m_rawAccelX;
        volatile short m_rawAccelY;
        volatile short m_rawAccelZ;
        volatile short m_calMagX;
        volatile short m_calMagY;
        volatile short m_calMagZ;

        /* Configuration/Status */
        volatile byte m_updateRateHz;
        volatile short m_accelFsrG = DefaultAccelFsrG;
        volatile short m_gyroFsrDps = DefaultGyroFsrDps;
        volatile short m_capabilityFlags;
        volatile byte m_opStatus;
        volatile short m_sensorStatus;
        volatile byte m_calStatus;
        volatile byte m_selftestStatus;

        /* Board ID */
        volatile byte m_boardType;
        volatile byte m_hwRev;
        volatile byte m_fwVerMajor;
        volatile byte m_fwVerMinor;

        long m_lastSensorTimestamp;
        double m_lastUpdateTime;


        InertialDataIntegrator m_integrator;
        ContinuousAngleTracker m_yawAngleTracker;
        OffsetTracker m_yawOffsetTracker;
        IIoProvider m_io;

        BoardCapabilities m_boardCapabilities;
        IoCompleteNotification m_ioCompleteSink;

        private Thread m_ioThread;
        //IOThread io_thread;


        /**
         * Constructs the AHRS class using SPI communication, overriding the 
         * default update rate with a custom rate which may be from 4 to 60, 
         * representing the number of updates per second sent by the sensor.  
         *<p>
         * This constructor should be used if communicating via SPI.
         *<p>
         * Note that increasing the update rate may increase the 
         * CPU utilization.
         *<p>
         * @param spi_port_id SPI Port to use
         * @param update_rate_hz Custom Update Rate (Hz)
         */
        public AHRS(SPI.Port spiPortId, byte updateRateHz)
        {
            CommonInit(updateRateHz);
            m_io = new RegisterIo(new RegisterIoSpi(new SPI(spiPortId)), updateRateHz, m_ioCompleteSink, m_boardCapabilities);
            m_io.Init();
            timer = new System.Threading.Timer(m_io.Run, null, 20, 20);
            //m_ioThread = new Thread(m_io.Run);
            //m_ioThread.Start();
        }

        public void GrabData()
        {
            //m_io.Run(null);
        }

        private System.Threading.Timer timer;
        /**
         * The AHRS class provides an interface to AHRS capabilities
         * of the KauaiLabs navX Robotics Navigation Sensor via SPI, I2C and
         * Serial (TTL UART and USB) communications interfaces on the RoboRIO.
         *
         * The AHRS class enables access to basic connectivity and state information,
         * as well as key 6-axis and 9-axis orientation information (yaw, pitch, roll,
         * compass heading, fused (9-axis) heading and magnetic disturbance detection.
         *
         * Additionally, the ARHS class also provides access to extended information
         * including linear acceleration, motion detection, rotation detection and sensor
         * temperature.
         *
         * If used with the navX Aero, the AHRS class also provides access to
         * altitude, barometric pressure and pressure sensor temperature data
         *
         * This constructor allows the specification of a custom SPI bitrate, in bits/second.
         *
         * @param spi_port_id SPI Port to use
         * @param spi_bitrate SPI bitrate (Maximum:  2,000,000)
         * @param update_rate_hz Custom Update Rate (Hz)
         */

        public AHRS(SPI.Port spiPortId, int spiBitrate, byte updateRateHz)
        {
            CommonInit(updateRateHz);
            m_io = new RegisterIo(new RegisterIoSpi(new SPI(spiPortId), spiBitrate), updateRateHz, m_ioCompleteSink, m_boardCapabilities);
            m_ioThread = new Thread(m_io.Run);
            m_ioThread.Start();
        }/**
     * Constructs the AHRS class using I2C communication, overriding the 
     * default update rate with a custom rate which may be from 4 to 60, 
     * representing the number of updates per second sent by the sensor.  
     *<p>
     * This constructor should be used if communicating via I2C.
     *<p>
     * Note that increasing the update rate may increase the 
     * CPU utilization.
     *<p>
     * @param i2c_port_id I2C Port to use
     * @param update_rate_hz Custom Update Rate (Hz)
     */
        public AHRS(I2C.Port i2CPortId, byte updateRateHz)
        {
            CommonInit(updateRateHz);
            m_io = new RegisterIo(new RegisterIoI2C(new I2C(i2CPortId, 0x32)), updateRateHz, m_ioCompleteSink, m_boardCapabilities);
            m_ioThread = new Thread(m_io.Run);
            m_ioThread.Start();
        }

        /**
         * Constructs the AHRS class using serial communication, overriding the 
         * default update rate with a custom rate which may be from 4 to 60, 
         * representing the number of updates per second sent by the sensor.  
         *<p>
         * This constructor should be used if communicating via either 
         * TTL UART or USB Serial interface.
         *<p>
         * Note that the serial interfaces can communicate either 
         * processed data, or raw data, but not both simultaneously.
         * If simultaneous processed and raw data are needed, use
         * one of the register-based interfaces (SPI or I2C).
         *<p>
         * Note that increasing the update rate may increase the 
         * CPU utilization.
         *<p>
         * @param serial_port_id SerialPort to use
         * @param data_type either kProcessedData or kRawData
         * @param update_rate_hz Custom Update Rate (Hz)
         */
        public AHRS(SerialPort.Port serialPortId, SerialDataType dataType, byte updateRateHz)
        {
            CommonInit(updateRateHz);
            bool processedData = (dataType == SerialDataType.ProcessedData);
            m_io = new SerialIo(serialPortId, updateRateHz, processedData, m_ioCompleteSink, m_boardCapabilities);
            m_ioThread = new Thread(m_io.Run);
            m_ioThread.Start();
        }

        /**
         * Constructs the AHRS class using SPI communication and the default update rate.  
         *<p>
         * This constructor should be used if communicating via SPI.
         *<p>
         * @param spi_port_id SPI port to use.
         */
        public AHRS(SPI.Port spiPortId) : this(spiPortId, NavxDefaultUpdateRateHz)
        {
        }


        /**
         * Constructs the AHRS class using I2C communication and the default update rate.  
         *<p>
         * This constructor should be used if communicating via I2C.
         *<p>
         * @param i2c_port_id I2C port to use
         */
        public AHRS(I2C.Port i2CPortId) : this(i2CPortId, NavxDefaultUpdateRateHz)
        {
            
        }


        /**
         * Constructs the AHRS class using serial communication and the default update rate, 
         * and returning processed (rather than raw) data.  
         *<p>
         * This constructor should be used if communicating via either 
         * TTL UART or USB Serial interface.
         *<p>
         * @param serial_port_id SerialPort to use
         */
        public AHRS(SerialPort.Port serialPortId) : this(serialPortId, SerialDataType.ProcessedData, NavxDefaultUpdateRateHz)
        {
        }

        /**
         * Returns the current pitch value (in degrees, from -180 to 180)
         * reported by the sensor.  Pitch is a measure of rotation around
         * the X Axis.
         * @return The current pitch value in degrees (-180 to 180).
         */
        public float GetPitch()
        {
            return m_pitch;
        }

        /**
         * Returns the current roll value (in degrees, from -180 to 180)
         * reported by the sensor.  Roll is a measure of rotation around
         * the X Axis.
         * @return The current roll value in degrees (-180 to 180).
         */
        public float GetRoll()
        {
            return m_roll;
        }

        /**
         * Returns the current yaw value (in degrees, from -180 to 180)
         * reported by the sensor.  Yaw is a measure of rotation around
         * the Z Axis (which is perpendicular to the earth).
         *<p>
         * Note that the returned yaw value will be offset by a user-specified
         * offset value; this user-specified offset value is set by 
         * invoking the zeroYaw() method.
         * @return The current yaw value in degrees (-180 to 180).
         */
        public float GetYaw()
        {
            if (m_boardCapabilities.IsBoardYawResetSupported())
            {
                return this.m_yaw;
            }
            else
            {
                return (float)m_yawOffsetTracker.ApplyOffset(this.m_yaw);
            }
        }

        /**
         * Returns the current tilt-compensated compass heading 
         * value (in degrees, from 0 to 360) reported by the sensor.
         *<p>
         * Note that this value is sensed by a magnetometer,
         * which can be affected by nearby magnetic fields (e.g., the
         * magnetic fields generated by nearby motors).
         *<p>
         * Before using this value, ensure that (a) the magnetometer
         * has been calibrated and (b) that a magnetic disturbance is
         * not taking place at the instant when the compass heading
         * was generated.
         * @return The current tilt-compensated compass heading, in degrees (0-360).
         */
        public float GetCompassHeading()
        {
            return m_compassHeading;
        }

        /**
         * Sets the user-specified yaw offset to the current
         * yaw value reported by the sensor.
         *<p>
         * This user-specified yaw offset is automatically
         * subtracted from subsequent yaw values reported by
         * the getYaw() method.
         */
        public void ZeroYaw()
        {
            if (m_boardCapabilities.IsBoardYawResetSupported())
            {
                m_io.ZeroYaw();
            }
            else
            {
                m_yawOffsetTracker.SetOffset();
            }
        }


        /**
         * Returns true if the sensor is currently performing automatic
         * gyro/accelerometer calibration.  Automatic calibration occurs 
         * when the sensor is initially powered on, during which time the 
         * sensor should be held still, with the Z-axis pointing up 
         * (perpendicular to the earth).
         *<p>
         * NOTE:  During this automatic calibration, the yaw, pitch and roll
         * values returned may not be accurate.
         *<p>
         * Once calibration is complete, the sensor will automatically remove 
         * an internal yaw offset value from all reported values.
         *<p>
         * @return Returns true if the sensor is currently automatically 
         * calibrating the gyro and accelerometer sensors.
         */
        public bool IsCalibrating()
        {
            return !((m_calStatus &
                        AHRSProtocol.NavxCalStatusIMUCalStateMask) ==
                            AHRSProtocol.NavxCalStatusIMUCalComplete);
        }

        /**
         * Indicates whether the sensor is currently connected
         * to the host computer.  A connection is considered established
         * whenever communication with the sensor has occurred recently.
         *<p>
         * @return Returns true if a valid update has been recently received
         * from the sensor.
         */
        public bool IsConnected()
        {
            return m_io.IsConnected();
        }

        /**
         * Returns the count in bytes of data received from the
         * sensor.  This could can be useful for diagnosing 
         * connectivity issues.
         *<p>
         * If the byte count is increasing, but the update count
         * (see getUpdateCount()) is not, this indicates a software
         * misconfiguration.
         * @return The number of bytes received from the sensor.
         */
        public double GetByteCount()
        {
            return m_io.GetByteCount();
        }

        /**
         * Returns the count of valid updates which have
         * been received from the sensor.  This count should increase
         * at the same rate indicated by the configured update rate.
         * @return The number of valid updates received from the sensor.
         */
        public double GetUpdateCount()
        {
            return m_io.GetUpdateCount();
        }

        /**
         * Returns the current linear acceleration in the X-axis (in G).
         *<p>
         * World linear acceleration refers to raw acceleration data, which
         * has had the gravity component removed, and which has been rotated to
         * the same reference frame as the current yaw value.  The resulting
         * value represents the current acceleration in the x-axis of the
         * body (e.g., the robot) on which the sensor is mounted.
         *<p>
         * @return Current world linear acceleration in the X-axis (in G).
         */
        public float GetWorldLinearAccelX()
        {
            return this.m_worldLinearAccelX;
        }

        /**
         * Returns the current linear acceleration in the Y-axis (in G).
         *<p>
         * World linear acceleration refers to raw acceleration data, which
         * has had the gravity component removed, and which has been rotated to
         * the same reference frame as the current yaw value.  The resulting
         * value represents the current acceleration in the Y-axis of the
         * body (e.g., the robot) on which the sensor is mounted.
         *<p>
         * @return Current world linear acceleration in the Y-axis (in G).
         */
        public float GetWorldLinearAccelY()
        {
            return this.m_worldLinearAccelY;
        }

        /**
         * Returns the current linear acceleration in the Z-axis (in G).
         *<p>
         * World linear acceleration refers to raw acceleration data, which
         * has had the gravity component removed, and which has been rotated to
         * the same reference frame as the current yaw value.  The resulting
         * value represents the current acceleration in the Z-axis of the
         * body (e.g., the robot) on which the sensor is mounted.
         *<p>
         * @return Current world linear acceleration in the Z-axis (in G).
         */
        public float GetWorldLinearAccelZ()
        {
            return this.m_worldLinearAccelZ;
        }

        /**
         * Indicates if the sensor is currently detecting motion,
         * based upon the X and Y-axis world linear acceleration values.
         * If the sum of the absolute values of the X and Y axis exceed
         * a "motion threshold", the motion state is indicated.
         *<p>
         * @return Returns true if the sensor is currently detecting motion.
         */
        public bool IsMoving()
        {
            return m_isMoving;
        }

        /**
         * Indicates if the sensor is currently detecting yaw rotation,
         * based upon whether the change in yaw over the last second 
         * exceeds the "Rotation Threshold."
         *<p>
         * Yaw Rotation can occur either when the sensor is rotating, or
         * when the sensor is not rotating AND the current gyro calibration
         * is insufficiently calibrated to yield the standard yaw drift rate.
         *<p>
         * @return Returns true if the sensor is currently detecting motion.
         */
        public bool IsRotating()
        {
            return m_isRotating;
        }

        /**
         * Returns the current barometric pressure, based upon calibrated readings
         * from the onboard pressure sensor.  This value is in units of millibar.
         *<p>
         * NOTE:  This value is only valid for a navX Aero.  To determine
         * whether this value is valid, see isAltitudeValid().
         * @return Returns current barometric pressure (navX Aero only).
         */
        public float GetBarometricPressure()
        {
            return m_baroPressure;
        }

        /**
         * Returns the current altitude, based upon calibrated readings
         * from a barometric pressure sensor, and the currently-configured
         * sea-level barometric pressure [navX Aero only].  This value is in units of meters.
         *<p>
         * NOTE:  This value is only valid sensors including a pressure
         * sensor.  To determine whether this value is valid, see 
         * isAltitudeValid().
         *<p>
         * @return Returns current altitude in meters (as long as the sensor includes 
         * an installed on-board pressure sensor).
         */
        public float GetAltitude()
        {
            return m_altitude;
        }

        /**
         * Indicates whether the current altitude (and barometric pressure) data is 
         * valid. This value will only be true for a sensor with an onboard
         * pressure sensor installed.
         *<p>
         * If this value is false for a board with an installed pressure sensor, 
         * this indicates a malfunction of the onboard pressure sensor.
         *<p>
         * @return Returns true if a working pressure sensor is installed.
         */
        public bool IsAltitudeValid()
        {
            return this.m_altitudeValid;
        }

        /**
         * Returns the "fused" (9-axis) heading.
         *<p>
         * The 9-axis heading is the fusion of the yaw angle, the tilt-corrected
         * compass heading, and magnetic disturbance detection.  Note that the
         * magnetometer calibration procedure is required in order to 
         * achieve valid 9-axis headings.
         *<p>
         * The 9-axis Heading represents the sensor's best estimate of current heading, 
         * based upon the last known valid Compass Angle, and updated by the change in the 
         * Yaw Angle since the last known valid Compass Angle.  The last known valid Compass 
         * Angle is updated whenever a Calibrated Compass Angle is read and the sensor 
         * has recently rotated less than the Compass Noise Bandwidth (~2 degrees).
         * @return Fused Heading in Degrees (range 0-360)
         */
        public float GetFusedHeading()
        {
            return m_fusedHeading;
        }

        /**
         * Indicates whether the current magnetic field strength diverges from the 
         * calibrated value for the earth's magnetic field by more than the currently-
         * configured Magnetic Disturbance Ratio.
         *<p>
         * This function will always return false if the sensor's magnetometer has
         * not yet been calibrated; see isMagnetometerCalibrated().
         * @return true if a magnetic disturbance is detected (or the magnetometer is uncalibrated).
         */
        public bool IsMagneticDisturbance()
        {
            return m_magneticDisturbance;
        }

        /**
         * Indicates whether the magnetometer has been calibrated.  
         *<p>
         * Magnetometer Calibration must be performed by the user.
         *<p>
         * Note that if this function does indicate the magnetometer is calibrated,
         * this does not necessarily mean that the calibration quality is sufficient
         * to yield valid compass headings.
         *<p>
         * @return Returns true if magnetometer calibration has been performed.
         */
        public bool IsMagnetometerCalibrated()
        {
            return m_isMagnetometerCalibrated;
        }

        /* Unit Quaternions */

        /**
         * Returns the imaginary portion (W) of the Orientation Quaternion which 
         * fully describes the current sensor orientation with respect to the 
         * reference angle defined as the angle at which the yaw was last "zeroed".  
         *<p>
         * Each quaternion value (W,X,Y,Z) is expressed as a value ranging from -2
         * to 2.  This total range (4) can be associated with a unit circle, since
         * each circle is comprised of 4 PI Radians.
         * <p>
         * For more information on Quaternions and their use, please see this <a href=https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation>definition</a>.
         * @return Returns the imaginary portion (W) of the quaternion.
         */
        public float GetQuaternionW()
        {
            return ((float)m_quaternionW / 16384.0f);
        }
        /**
         * Returns the real portion (X axis) of the Orientation Quaternion which 
         * fully describes the current sensor orientation with respect to the 
         * reference angle defined as the angle at which the yaw was last "zeroed".  
         * <p>
         * Each quaternion value (W,X,Y,Z) is expressed as a value ranging from -2
         * to 2.  This total range (4) can be associated with a unit circle, since
         * each circle is comprised of 4 PI Radians.
         * <p>
         * For more information on Quaternions and their use, please see this <a href=https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation>description</a>. 
         * @return Returns the real portion (X) of the quaternion.
         */
        public float GetQuaternionX()
        {
            return ((float)m_quaternionX / 16384.0f);
        }
        /**
         * Returns the real portion (X axis) of the Orientation Quaternion which 
         * fully describes the current sensor orientation with respect to the 
         * reference angle defined as the angle at which the yaw was last "zeroed".  
         * 
         * Each quaternion value (W,X,Y,Z) is expressed as a value ranging from -2
         * to 2.  This total range (4) can be associated with a unit circle, since
         * each circle is comprised of 4 PI Radians.
         * 
         * For more information on Quaternions and their use, please see:
         * 
         *   https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation
         * 
         * @return Returns the real portion (X) of the quaternion.
         */
        public float GetQuaternionY()
        {
            return ((float)m_quaternionY / 16384.0f);
        }
        /**
         * Returns the real portion (X axis) of the Orientation Quaternion which 
         * fully describes the current sensor orientation with respect to the 
         * reference angle defined as the angle at which the yaw was last "zeroed".  
         * 
         * Each quaternion value (W,X,Y,Z) is expressed as a value ranging from -2
         * to 2.  This total range (4) can be associated with a unit circle, since
         * each circle is comprised of 4 PI Radians.
         * 
         * For more information on Quaternions and their use, please see:
         * 
         *   https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation
         * 
         * @return Returns the real portion (X) of the quaternion.
         */
        public float GetQuaternionZ()
        {
            return ((float)m_quaternionZ / 16384.0f);
        }

        /**
         * Zeros the displacement integration variables.   Invoke this at the moment when
         * integration begins.
         */
        public void ResetDisplacement()
        {
            if (m_boardCapabilities.IsDisplacementSupported())
            {
                m_io.ZeroDisplacement();
            }
            else
            {
                m_integrator.ResetDisplacement();
            }
        }

        /**
         * Each time new linear acceleration samples are received, this function should be invoked.
         * This function transforms acceleration in G to meters/sec^2, then converts this value to
         * Velocity in meters/sec (based upon velocity in the previous sample).  Finally, this value
         * is converted to displacement in meters, and integrated.
         * @return none.
         */

        private void UpdateDisplacement(float accelXG, float accelYG,
                                            int updateRateHz, bool is_moving)
        {
            m_integrator.UpdateDisplacement(accelXG, accelYG, updateRateHz, is_moving);
        }

        /**
         * Returns the velocity (in meters/sec) of the X axis [Experimental].
         *
         * NOTE:  This feature is experimental.  Velocity measures rely on integration
         * of acceleration values from MEMS accelerometers which yield "noisy" values.  The
         * resulting velocities are not known to be very accurate.
         * @return Current Velocity (in meters/squared).
         */
        public float GetVelocityX()
        {
            return (m_boardCapabilities.IsDisplacementSupported() ? m_velocity[0] : m_integrator.GetVelocityX());
        }

        /**
         * Returns the velocity (in meters/sec) of the Y axis [Experimental].
         *
         * NOTE:  This feature is experimental.  Velocity measures rely on integration
         * of acceleration values from MEMS accelerometers which yield "noisy" values.  The
         * resulting velocities are not known to be very accurate.
         * @return Current Velocity (in meters/squared).
         */
        public float GetVelocityY()
        {
            return (m_boardCapabilities.IsDisplacementSupported() ? m_velocity[1] : m_integrator.GetVelocityY());
        }

        /**
         * Returns the velocity (in meters/sec) of the Z axis [Experimental].
         *
         * NOTE:  This feature is experimental.  Velocity measures rely on integration
         * of acceleration values from MEMS accelerometers which yield "noisy" values.  The
         * resulting velocities are not known to be very accurate.
         * @return Current Velocity (in meters/squared).
         */
        public float GetVelocityZ()
        {
            return (m_boardCapabilities.IsDisplacementSupported() ? m_velocity[2] : 0.0f);
        }

        /**
         * Returns the displacement (in meters) of the X axis since resetDisplacement()
         * was last invoked [Experimental].
         * 
         * NOTE:  This feature is experimental.  Displacement measures rely on double-integration
         * of acceleration values from MEMS accelerometers which yield "noisy" values.  The
         * resulting displacement are not known to be very accurate, and the amount of error 
         * increases quickly as time progresses.
         * @return Displacement since last reset (in meters).
         */
        public float GetDisplacementX()
        {
            return (m_boardCapabilities.IsDisplacementSupported() ? m_displacement[0] : m_integrator.GetVelocityX());
        }

        /**
         * Returns the displacement (in meters) of the Y axis since resetDisplacement()
         * was last invoked [Experimental].
         * 
         * NOTE:  This feature is experimental.  Displacement measures rely on double-integration
         * of acceleration values from MEMS accelerometers which yield "noisy" values.  The
         * resulting displacement are not known to be very accurate, and the amount of error 
         * increases quickly as time progresses.
         * @return Displacement since last reset (in meters).
         */
        public float GetDisplacementY()
        {
            return (m_boardCapabilities.IsDisplacementSupported() ? m_displacement[1] : m_integrator.GetVelocityY());
        }

        /**
         * Returns the displacement (in meters) of the Z axis since resetDisplacement()
         * was last invoked [Experimental].
         * 
         * NOTE:  This feature is experimental.  Displacement measures rely on double-integration
         * of acceleration values from MEMS accelerometers which yield "noisy" values.  The
         * resulting displacement are not known to be very accurate, and the amount of error 
         * increases quickly as time progresses.
         * @return Displacement since last reset (in meters).
         */
        public float GetDisplacementZ()
        {
            return (m_boardCapabilities.IsDisplacementSupported() ? m_displacement[2] : 0.0f);
        }


        private void CommonInit(byte updateRateHz)
        {
            this.m_boardCapabilities = new BoardCapabilities(this);
            this.m_ioCompleteSink = new IoCompleteNotification(this);
            this.m_updateRateHz = updateRateHz;
            m_integrator = new InertialDataIntegrator();
            m_yawOffsetTracker = new OffsetTracker(YawHistoryLength);
            m_yawAngleTracker = new ContinuousAngleTracker();
        }


        /**
         * Returns the current yaw value reported by the sensor.  This
         * yaw value is useful for implementing features including "auto rotate 
         * to a known angle".
         * @return The current yaw angle in degrees (-180 to 180).
         *
        public double pidGet()
        {
            return getYaw();
        }

        /**
         * Returns the total accumulated yaw angle (Z Axis, in degrees)
         * reported by the sensor.
         *<p>
         * NOTE: The angle is continuous, meaning it's range is beyond 360 degrees.
         * This ensures that algorithms that wouldn't want to see a discontinuity 
         * in the gyro output as it sweeps past 0 on the second time around.
         *<p>
         * Note that the returned yaw value will be offset by a user-specified
         * offset value; this user-specified offset value is set by 
         * invoking the zeroYaw() method.
         *<p>
         * @return The current total accumulated yaw angle (Z axis) of the robot 
         * in degrees. This heading is based on integration of the returned rate 
         * from the Z-axis (yaw) gyro.
         *
        public double getAngle()
        {
            return yaw_angle_tracker.getAngle();
        }

        /**
         * Return the rate of rotation of the yaw (Z-axis) gyro, in degrees per second.
         *<p>
         * The rate is based on the most recent reading of the yaw gyro angle.
         *<p>
         * @return The current rate of change in yaw angle (in degrees per second)
         *

        public double getRate()
        {
            return yaw_angle_tracker.getRate();
        }

            */

        

        /**
         * Reset the Yaw gyro.
         *<p>
         * Resets the Gyro Z (Yaw) axis to a heading of zero. This can be used if 
         * there is significant drift in the gyro and it needs to be recalibrated 
         * after it has been running.
         */
        public void Reset()
        {
            ZeroYaw();
        }

        private const float DevUnitsMax = 32768.0f;

        /**
         * Returns the current raw (unprocessed) X-axis gyro rotation rate (in degrees/sec).  NOTE:  this
         * value is un-processed, and should only be accessed by advanced users.
         * Typically, rotation about the X Axis is referred to as "Pitch".  Calibrated
         * and Integrated Pitch data is accessible via the {@link #getPitch()} method.  
         *<p>
         * @return Returns the current rotation rate (in degrees/sec).
         */
        public float GetRawGyroX()
        {
            return this.m_rawGyroX / (DevUnitsMax / (float)m_gyroFsrDps);
        }

        /**
         * Returns the current raw (unprocessed) Y-axis gyro rotation rate (in degrees/sec).  NOTE:  this
         * value is un-processed, and should only be accessed by advanced users.
         * Typically, rotation about the T Axis is referred to as "Roll".  Calibrated
         * and Integrated Pitch data is accessible via the {@link #getRoll()} method.  
         *<p>
         * @return Returns the current rotation rate (in degrees/sec).
         */
        public float GetRawGyroY()
        {
            return this.m_rawGyroY / (DevUnitsMax / (float)m_gyroFsrDps);
        }

        /**
         * Returns the current raw (unprocessed) Z-axis gyro rotation rate (in degrees/sec).  NOTE:  this
         * value is un-processed, and should only be accessed by advanced users.
         * Typically, rotation about the T Axis is referred to as "Yaw".  Calibrated
         * and Integrated Pitch data is accessible via the {@link #getYaw()} method.  
         *<p>
         * @return Returns the current rotation rate (in degrees/sec).
         */
        public float GetRawGyroZ()
        {
            return this.m_rawGyroZ / (DevUnitsMax / (float)m_gyroFsrDps);
        }

        /**
         * Returns the current raw (unprocessed) X-axis acceleration rate (in G).  NOTE:  this
         * value is unprocessed, and should only be accessed by advanced users.  This raw value
         * has not had acceleration due to gravity removed from it, and has not been rotated to
         * the world reference frame.  Gravity-corrected, world reference frame-corrected 
         * X axis acceleration data is accessible via the {@link #getWorldLinearAccelX()} method.
         *<p>
         * @return Returns the current acceleration rate (in G).
         */
        public float GetRawAccelX()
        {
            return this.m_rawAccelX / (DevUnitsMax / (float)m_accelFsrG);
        }

        /**
         * Returns the current raw (unprocessed) Y-axis acceleration rate (in G).  NOTE:  this
         * value is unprocessed, and should only be accessed by advanced users.  This raw value
         * has not had acceleration due to gravity removed from it, and has not been rotated to
         * the world reference frame.  Gravity-corrected, world reference frame-corrected 
         * Y axis acceleration data is accessible via the {@link #getWorldLinearAccelY()} method.
         *<p>
         * @return Returns the current acceleration rate (in G).
         */
        public float GetRawAccelY()
        {
            return this.m_rawAccelY / (DevUnitsMax / (float)m_accelFsrG);
        }

        /**
         * Returns the current raw (unprocessed) Z-axis acceleration rate (in G).  NOTE:  this
         * value is unprocessed, and should only be accessed by advanced users.  This raw value
         * has not had acceleration due to gravity removed from it, and has not been rotated to
         * the world reference frame.  Gravity-corrected, world reference frame-corrected 
         * Z axis acceleration data is accessible via the {@link #getWorldLinearAccelZ()} method.
         *<p>
         * @return Returns the current acceleration rate (in G).
         */
        public float GetRawAccelZ()
        {
            return this.m_rawAccelZ / (DevUnitsMax / (float)m_accelFsrG);
        }

        private const float UteslaPerDevUnit = 0.15f;

        /**
         * Returns the current raw (unprocessed) X-axis magnetometer reading (in uTesla).  NOTE:
         * this value is unprocessed, and should only be accessed by advanced users.  This raw value
         * has not been tilt-corrected, and has not been combined with the other magnetometer axis
         * data to yield a compass heading.  Tilt-corrected compass heading data is accessible 
         * via the {@link #getCompassHeading()} method.
         *<p>
         * @return Returns the mag field strength (in uTesla).
         */
        public float GetRawMagX()
        {
            return this.m_calMagX / UteslaPerDevUnit;
        }

        /**
         * Returns the current raw (unprocessed) Y-axis magnetometer reading (in uTesla).  NOTE:
         * this value is unprocessed, and should only be accessed by advanced users.  This raw value
         * has not been tilt-corrected, and has not been combined with the other magnetometer axis
         * data to yield a compass heading.  Tilt-corrected compass heading data is accessible 
         * via the {@link #getCompassHeading()} method.
         *<p>
         * @return Returns the mag field strength (in uTesla).
         */
        public float GetRawMagY()
        {
            return this.m_calMagY / UteslaPerDevUnit;
        }

        /**
         * Returns the current raw (unprocessed) Z-axis magnetometer reading (in uTesla).  NOTE:
         * this value is unprocessed, and should only be accessed by advanced users.  This raw value
         * has not been tilt-corrected, and has not been combined with the other magnetometer axis
         * data to yield a compass heading.  Tilt-corrected compass heading data is accessible 
         * via the {@link #getCompassHeading()} method.
         *<p>
         * @return Returns the mag field strength (in uTesla).
         */
        public float GetRawMagZ()
        {
            return this.m_calMagZ / UteslaPerDevUnit;
        }

        /**
         * Returns the current barometric pressure (in millibar) [navX Aero only].
         *<p>
         *This value is valid only if a barometric pressure sensor is onboard.
         *
         * @return Returns the current barometric pressure (in millibar).
         */
        public float GetPressure()
        {
            // TODO implement for navX-Aero.
            return 0;
        }


        /**
         * Returns the current temperature (in degrees centigrade) reported by
         * the sensor's gyro/accelerometer circuit.
         *<p>
         * This value may be useful in order to perform advanced temperature-
         * correction of raw gyroscope and accelerometer values.
         *<p>
         * @return The current temperature (in degrees centigrade).
         */
        public float GetTempC()
        {
            return this.m_mpuTempC;
        }

        /**
         * Returns information regarding which sensor board axis (X,Y or Z) and
         * direction (up/down) is currently configured to report Yaw (Z) angle 
         * values.   NOTE:  If the board firmware supports Omnimount, the board yaw 
         * axis/direction are configurable.
         *<p>
         * For more information on Omnimount, please see:
         *<p>
         * http://navx-mxp.kauailabs.com/navx-mxp/installation/omnimount/
         *<p>
         * @return The currently-configured board yaw axis/direction.
         */
        public BoardYawAxis GetBoardYawAxis()
        {
            BoardYawAxis yawAxis = new BoardYawAxis();
            short yawAxisInfo = (short)(m_capabilityFlags >> 3);
            yawAxisInfo &= 7;
            if (yawAxisInfo == AHRSProtocol.OmnimountDefault)
            {
                yawAxis.Up = true;
                yawAxis.BoardAxis = BoardAxis.BoardAxisZ;
            }
            else
            {
                yawAxis.Up = (((yawAxisInfo & 0x01) != 0) ? true : false);
                yawAxisInfo >>= 1;
                switch ((byte)yawAxisInfo)
                {
                    case 0:
                        yawAxis.BoardAxis = BoardAxis.BoardAxisX;
                        break;
                    case 1:
                        yawAxis.BoardAxis = BoardAxis.BoardAxisY;
                        break;
                    case 2:
                    default:
                        yawAxis.BoardAxis = BoardAxis.BoardAxisZ;
                        break;
                }
            }
            return yawAxis;
        }

        /**
         * Returns the version number of the firmware currently executing
         * on the sensor.
         *<p>
         * To update the firmware to the latest version, please see:
         *<p>
         *   http://navx-mxp.kauailabs.com/navx-mxp/support/updating-firmware/
         *<p>
         * @return The firmware version in the format [MajorVersion].[MinorVersion]
         */
        public String GetFirmwareVersion()
        {
            double versionNumber = (double)m_fwVerMajor;
            versionNumber += ((double)m_fwVerMinor / 10);
            String fwVersion = versionNumber.ToString();
            return fwVersion;
        }


        class BoardCapabilities : IBoardCapabilities
        {
            private AHRS m_ahrs;
            public BoardCapabilities(AHRS ahrs)
            {
                m_ahrs = ahrs;
            }

            public bool IsOmniMountSupported()
            {
                return (((m_ahrs.m_capabilityFlags & AHRSProtocol.NavxCapabilityFlagOmnimount) != 0) ? true : false);
            }


            public bool IsBoardYawResetSupported()
            {
                return (((m_ahrs.m_capabilityFlags & AHRSProtocol.NavxCapabilityFlagYawReset) != 0) ? true : false);
            }


            public bool IsDisplacementSupported()
            {
                return (((m_ahrs.m_capabilityFlags & AHRSProtocol.NavxCapabilityFlagVelAndDisp) != 0) ? true : false);
            }
        }


        class IoCompleteNotification : IIoCompleteNotification
        {
            private AHRS m_ahrs;
            public IoCompleteNotification(AHRS ahrs)
            {
                m_ahrs = ahrs;
            }

            public void SetYawPitchRoll(IMUProtocol.YprUpdate yprUpdate)
            {
                lock (this)
                { // lock block
                    m_ahrs.m_yaw = yprUpdate.Yaw;
                    m_ahrs.m_pitch = yprUpdate.Pitch;
                    m_ahrs.m_roll = yprUpdate.Roll;
                    m_ahrs.m_compassHeading = yprUpdate.CompassHeading;
                }
            }


            public void SetAHRSPosData(AHRSProtocol.AHRSPosUpdate ahrsUpdate)
            {
                lock (this)
                { // lock block

                    /* Update base IMU class variables */

                    m_ahrs.m_yaw = ahrsUpdate.Yaw;
                    m_ahrs.m_pitch = ahrsUpdate.Pitch;
                    m_ahrs.m_roll = ahrsUpdate.Roll;
                    m_ahrs.m_compassHeading = ahrsUpdate.CompassHeading;
                    m_ahrs.m_yawOffsetTracker.UpdateHistory(ahrsUpdate.Yaw);

                    /* Update AHRS class variables */

                    // 9-axis data
                    m_ahrs.m_fusedHeading = ahrsUpdate.FusedHeading;

                    // Gravity-corrected linear acceleration (world-frame)
                    m_ahrs.m_worldLinearAccelX = ahrsUpdate.LinearAccelX;
                    m_ahrs.m_worldLinearAccelY = ahrsUpdate.LinearAccelY;
                    m_ahrs.m_worldLinearAccelZ = ahrsUpdate.LinearAccelZ;

                    // Gyro/Accelerometer Die Temperature
                    m_ahrs.m_mpuTempC = ahrsUpdate.MpuTemp;

                    // Barometric Pressure/Altitude
                    m_ahrs.m_altitude = ahrsUpdate.Altitude;
                    m_ahrs.m_baroPressure = ahrsUpdate.BarometricPressure;

                    // Status/Motion Detection
                    m_ahrs.m_isMoving =
                            (((ahrsUpdate.SensorStatus &
                                    AHRSProtocol.NavxSensorStatusMoving) != 0)
                                    ? true : false);
                    m_ahrs.m_isRotating =
                            (((ahrsUpdate.SensorStatus &
                                    AHRSProtocol.NavxSensorStatusYawStable) != 0)
                                    ? false : true);
                    m_ahrs.m_altitudeValid =
                            (((ahrsUpdate.SensorStatus &
                                    AHRSProtocol.NavxSensorStatusAltitudeValid) != 0)
                                    ? true : false);
                    m_ahrs.m_isMagnetometerCalibrated =
                            (((ahrsUpdate.CalStatus &
                                    AHRSProtocol.NavxCalStatusMagCalComplete) != 0)
                                    ? true : false);
                    m_ahrs.m_magneticDisturbance =
                            (((ahrsUpdate.SensorStatus &
                                    AHRSProtocol.NavxSensorStatusMagDisturbance) != 0)
                                    ? true : false);

                    m_ahrs.m_quaternionW = ahrsUpdate.QuatW;
                    m_ahrs.m_quaternionX = ahrsUpdate.QuatX;
                    m_ahrs.m_quaternionY = ahrsUpdate.QuatY;
                    m_ahrs.m_quaternionZ = ahrsUpdate.QuatZ;

                    m_ahrs.m_velocity[0] = ahrsUpdate.VelX;
                    m_ahrs.m_velocity[1] = ahrsUpdate.VelY;
                    m_ahrs.m_velocity[2] = ahrsUpdate.VelZ;
                    m_ahrs.m_displacement[0] = ahrsUpdate.DispX;
                    m_ahrs.m_displacement[1] = ahrsUpdate.DispY;
                    m_ahrs.m_displacement[2] = ahrsUpdate.DispZ;

                    m_ahrs.m_yawAngleTracker.NextAngle(m_ahrs.GetYaw());
                }
            }


            public void SetRawData(AHRSProtocol.GyroUpdate rawDataUpdate)
            {
                lock (this)
                { // lock block
                    m_ahrs.m_rawGyroX = rawDataUpdate.GyroX;
                    m_ahrs.m_rawGyroY = rawDataUpdate.GyroY;
                    m_ahrs.m_rawGyroZ = rawDataUpdate.GyroZ;
                    m_ahrs.m_rawAccelX = rawDataUpdate.AccelX;
                    m_ahrs.m_rawAccelY = rawDataUpdate.AccelY;
                    m_ahrs.m_rawAccelZ = rawDataUpdate.AccelZ;
                    m_ahrs.m_calMagX = rawDataUpdate.MagX;
                    m_ahrs.m_calMagY = rawDataUpdate.MagY;
                    m_ahrs.m_calMagZ = rawDataUpdate.MagZ;
                    m_ahrs.m_mpuTempC = rawDataUpdate.TempC;
                }
            }


            public void SetAHRSData(AHRSProtocol.AHRSUpdate ahrsUpdate)
            {
                lock (this)
                { // lock block

                    /* Update base IMU class variables */

                    m_ahrs.m_yaw = ahrsUpdate.Yaw;
                    m_ahrs.m_pitch = ahrsUpdate.Pitch;
                    m_ahrs.m_roll = ahrsUpdate.Roll;
                    m_ahrs.m_compassHeading = ahrsUpdate.CompassHeading;
                    m_ahrs.m_yawOffsetTracker.UpdateHistory(ahrsUpdate.Yaw);

                    /* Update AHRS class variables */

                    // 9-axis data
                    m_ahrs.m_fusedHeading = ahrsUpdate.FusedHeading;

                    // Gravity-corrected linear acceleration (world-frame)
                    m_ahrs.m_worldLinearAccelX = ahrsUpdate.LinearAccelX;
                    m_ahrs.m_worldLinearAccelY = ahrsUpdate.LinearAccelY;
                    m_ahrs.m_worldLinearAccelZ = ahrsUpdate.LinearAccelZ;

                    // Gyro/Accelerometer Die Temperature
                    m_ahrs.m_mpuTempC = ahrsUpdate.MpuTemp;

                    // Barometric Pressure/Altitude
                    m_ahrs.m_altitude = ahrsUpdate.Altitude;
                    m_ahrs.m_baroPressure = ahrsUpdate.BarometricPressure;

                    // Magnetometer Data
                    m_ahrs.m_calMagX = ahrsUpdate.CalMagX;
                    m_ahrs.m_calMagY = ahrsUpdate.CalMagY;
                    m_ahrs.m_calMagZ = ahrsUpdate.CalMagZ;

                    // Status/Motion Detection
                    m_ahrs.m_isMoving =
                            (((ahrsUpdate.SensorStatus &
                                    AHRSProtocol.NavxSensorStatusMoving) != 0)
                                    ? true : false);
                    m_ahrs.m_isRotating =
                            (((ahrsUpdate.SensorStatus &
                                    AHRSProtocol.NavxSensorStatusYawStable) != 0)
                                    ? false : true);
                    m_ahrs.m_altitudeValid =
                            (((ahrsUpdate.SensorStatus &
                                    AHRSProtocol.NavxSensorStatusAltitudeValid) != 0)
                                    ? true : false);
                    m_ahrs.m_isMagnetometerCalibrated =
                            (((ahrsUpdate.CalStatus &
                                    AHRSProtocol.NavxCalStatusMagCalComplete) != 0)
                                    ? true : false);
                    m_ahrs.m_magneticDisturbance =
                            (((ahrsUpdate.SensorStatus &
                                    AHRSProtocol.NavxSensorStatusMagDisturbance) != 0)
                                    ? true : false);

                    m_ahrs.m_quaternionW = ahrsUpdate.QuatW;
                    m_ahrs.m_quaternionX = ahrsUpdate.QuatX;
                    m_ahrs.m_quaternionY = ahrsUpdate.QuatY;
                    m_ahrs.m_quaternionZ = ahrsUpdate.QuatZ;

                    m_ahrs.UpdateDisplacement(m_ahrs.m_worldLinearAccelX,
                        m_ahrs.m_worldLinearAccelY,
                        m_ahrs.m_updateRateHz,
                        m_ahrs.m_isMoving);

                    m_ahrs.m_yawAngleTracker.NextAngle(m_ahrs.GetYaw());
                }
            }


            public void SetBoardId(AHRSProtocol.BoardId boardId)
            {
                lock (this)
                { // lock block
                    m_ahrs.m_boardType = boardId.Type;
                    m_ahrs.m_hwRev = boardId.HwRev;
                    m_ahrs.m_fwVerMajor = boardId.FwVerMajor;
                    m_ahrs.m_fwVerMinor = boardId.FwVerMinor;
                }
            }


            public void SetBoardState(BoardState boardState)
            {
                lock (this)
                { // lock block
                    m_ahrs.m_updateRateHz = boardState.UpdateRateHz;
                    m_ahrs.m_accelFsrG = boardState.AccelFsrG;
                    m_ahrs.m_gyroFsrDps = boardState.GyroFsrDps;
                    m_ahrs.m_capabilityFlags = boardState.CapabilityFlags;
                    m_ahrs.m_opStatus = boardState.OpStatus;
                    m_ahrs.m_sensorStatus = boardState.SensorStatus;
                    m_ahrs.m_calStatus = boardState.CalStatus;
                    m_ahrs.m_selftestStatus = boardState.SelftestStatus;
                }
            }
        };



        public void UpdateTable()
        {
            Table?.PutNumber("Value", GetYaw());
        }

        public void StartLiveWindowMode()
        {
        }

        public void StopLiveWindowMode()
        {
        }

        public void InitTable(ITable itable)
        {
            Table = itable;
            UpdateTable();
        }

        public ITable Table { get; private set; }

        public string SmartDashboardType => "Gyro";

        public double PidGet()
        {
            return GetYaw();
        }

        public void SetPIDSourceType(PIDSourceType pidSource)
        {
            throw new NotImplementedException();
        }

        public PIDSourceType GetPIDSourceType()
        {
            throw new NotImplementedException();
        }
    }
}
