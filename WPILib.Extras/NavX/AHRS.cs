using System;
using System.Threading;
using NetworkTables.Tables;
using WPILib.Extras.NavX.Protocols;
using WPILib.Interfaces;
using WPILib.LiveWindow;

namespace WPILib.Extras.NavX
{


    /// <summary>
    /// The AHRS class provides access to a KauaiLabs NavX Robotics Navigation Sensor.
    /// </summary>
    /// <remarks>
    /// The Sensor can be connected via the SPI, I2C and Serial (TTL UART and USB) communication interfaces on the RoboRIO.
    /// <para/>
    /// The AHRS class enables access to basic connectivity and state information,
    /// as well as key 6-axis and 9-axis orientation information(yaw, pitch, roll,
    /// compass heading, fused (9-axis) heading and magnetic disturbance detection.
    /// <para/>
    /// Additionally, the ARHS class also provides access to extended information
    /// including linear acceleration, motion detection, rotation detection and sensor
    /// temperature.
    ///<para/>
    /// If used with the navX Aero, the AHRS class also provides access to
    /// altitude, barometric pressure and pressure sensor temperature data
    /// </remarks>
    public class AHRS : GyroBase, IPIDSource, ILiveWindowSendable, IGyro
    {
        /// <summary>
        /// Identifies one of the three sensing axes on the NavX sensor board.
        /// </summary>
        /// <remarks>
        /// Note that these axes are board-relative ("Board Frame"), and are not necessarily the same as the logical axes of the 
        /// chassis on which the sensor is mounted.
        /// <para></para>
        /// For more information on sensor orientation, please see the NavX sensor <see cref="!:http://navx-mxp.kauailabs.com/installation/orientation-2/>Orientation"/> page.
        /// </remarks>
        public enum BoardAxis
        {
            /// <summary>
            /// Board X axis.
            /// </summary>
            KBoardAxisX,
            /// <summary>
            /// Board Y axis.
            /// </summary>
            KBoardAxisY,
            /// <summary>
            /// Board Z axis.
            /// </summary>
            KBoardAxisZ

        }

        /// <summary>
        /// Indicates which sensor board axis is used as the "yaw" (gravity) axis.
        /// </summary>
        /// <remarks>
        /// This selection may be modified via the <see cref="!:http://navx-mxp.kauailabs.com/installation/omnimount/>Omnimount"/> feature.
        /// </remarks>
        public class BoardYawAxis
        {
            /// <summary>
            /// The <see cref="BoardAxis"/>/
            /// </summary>
            public BoardAxis BoardAxis;
            /// <summary>
            /// True if axis is up, otherwise down.
            /// </summary>
            public bool Up;
        };

        /// <summary>
        /// The <see cref="SerialDataType"/> enum specifies the type of data to be streamed from the sensor.
        /// </summary>
        /// <remarks>
        /// Due to limitations in streaming bandwidth on some serial devices, only a subset of all available
        /// data can be steamed.
        /// <para/>
        /// Note that if communicating over I2C/SPI, all available data can be
        /// retreived, so the <see cref="SerialDataType"/> enum need only to be specified
        /// if using serial communications.
        /// </remarks>
        public enum SerialDataType
        {
            /// <summary>
            /// (Default): 6 and 9 axis processed data.
            /// </summary>
            KProcessedData,
            /// <summary>
            /// Unprocessed data from each individual sensor.
            /// </summary>
            KRawData

        }

        const byte NavxDefaultUpdateRateHz = 50;
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
        IoThread m_ioThread;

        /***********************************************************/
        /* Public Interface Implementation                         */
        /***********************************************************/

        /// <summary>
        /// Constructs the AHRS class using SPI Communication
        /// </summary>
        /// <remarks>
        /// The update rate may be between 4 Hz and 60 Hz, representing the number
        /// of updates per second sent by the sensor.
        /// <para/>
        /// This constructor should be used if communicating via SPI.
        /// <para/>
        /// Note that increasing the update rate may increase the CPU utilization.
        /// </remarks>
        /// <param name="spiPortId">The <see cref="SPI.Port">SPI Port</see> to use.</param>
        /// <param name="updateRateHz">The Update Rate (Hz) [4..60] (Default 50)</param>
        public AHRS(SPI.Port spiPortId, byte updateRateHz = NavxDefaultUpdateRateHz)
        {
            CommonInit(updateRateHz);
            if (RobotBase.IsSimulation)
            {
                m_io = new SimulatorIO(updateRateHz, m_ioCompleteSink, m_boardCapabilities);
            }
            else
            {
                m_io = new RegisterIO(new RegisterIO_SPI(new SPI(spiPortId)), updateRateHz, m_ioCompleteSink, m_boardCapabilities);
            }
            m_ioThread.Start();
        }

        /// <summary>
        /// Constructs the AHRS class using SPI Communication, overriding the SPI bitrate.
        /// </summary>
        /// <remarks>
        /// The update rate may be between 4 Hz and 60 Hz, representing the number
        /// of updates per second sent by the sensor.
        /// <para/>
        /// This constructor should be used if communicating via SPI.
        /// <para/>
        /// Note that increasing the update rate may increase the CPU utilization.
        /// </remarks>
        /// <param name="spiPortId">The <see cref="SPI.Port">SPI Port</see> to use.</param>
        /// <param name="spiBitrate">The SPI bitrate to use (bits/seconds) (Maximum: 2,000,000)</param>
        /// <param name="updateRateHz">The Update Rate (Hz) [4..60] (Default 50)</param>
        public AHRS(SPI.Port spiPortId, int spiBitrate, byte updateRateHz = NavxDefaultUpdateRateHz)
        {
            CommonInit(updateRateHz);
            if (RobotBase.IsSimulation)
            {
                m_io = new SimulatorIO(updateRateHz, m_ioCompleteSink, m_boardCapabilities);
            }
            else
            {
                m_io = new RegisterIO(new RegisterIO_SPI(new SPI(spiPortId), spiBitrate), updateRateHz, m_ioCompleteSink,
                    m_boardCapabilities);
            }
            m_ioThread.Start();
        }

        /// <summary>
        /// Constructs the AHRS class using I2C Communication
        /// </summary>
        /// <remarks>
        /// The update rate may be between 4 Hz and 60 Hz, representing the number
        /// of updates per second sent by the sensor.
        /// <para/>
        /// This constructor should be used if communicating via I2C.
        /// <para/>
        /// Note that increasing the update rate may increase the CPU utilization.
        /// </remarks>
        /// <param name="i2CPortId">The <see cref="I2C.Port">I2C Port</see> to use.</param>
        /// <param name="updateRateHz">The Update Rate (Hz) [4..60] (Default 50)</param>
        public AHRS(I2C.Port i2CPortId, byte updateRateHz = NavxDefaultUpdateRateHz)
        {
            CommonInit(updateRateHz);
            if (RobotBase.IsSimulation)
            {
                m_io = new SimulatorIO(updateRateHz, m_ioCompleteSink, m_boardCapabilities);
            }
            else
            {
                m_io = new RegisterIO(new RegisterIO_I2C(new I2C(i2CPortId, 0x32)), updateRateHz, m_ioCompleteSink,
                    m_boardCapabilities);
            }
            m_ioThread.Start();
        }

        /// <summary>
        /// Constructs the AHRS class using serial communication.
        /// </summary>
        /// <remarks>
        /// This constructor should be used if communicating via either TTL UART 
        /// or USB Serial Interface.
        /// <para></para>
        /// Note that the serial interfaces can communicate either processed data, or raw data,
        /// but not both simultaneously. If simultaneous processed and raw data are needed,
        /// use one of the register-based interfaces (SPI or I2C). The default is processed data.
        /// <para></para>
        ///  The update rate may be between 4 Hz and 60 Hz, representing the number
        /// of updates per second sent by the sensor. Note that increasing the update 
        /// rate may increase the CPU utilization.
        /// </remarks>
        /// <param name="serialPortId">The <see cref="SerialPort.Port">Serial Port</see> to use.</param>
        /// <param name="dataType">Either <see cref="SerialDataType.KProcessedData"/> (Default) or <see cref="SerialDataType.KRawData"/>.</param>
        /// <param name="updateRateHz">The Update Rate (Hz) [4..60] (Default 50)</param>
        public AHRS(SerialPort.Port serialPortId, SerialDataType dataType = SerialDataType.KProcessedData, byte updateRateHz = NavxDefaultUpdateRateHz)
        {
            CommonInit(updateRateHz);
            if (RobotBase.IsSimulation)
            {
                m_io = new SimulatorIO(updateRateHz, m_ioCompleteSink, m_boardCapabilities);
            }
            else
            {
                bool processedData = (dataType == SerialDataType.KProcessedData);
                m_io = new SerialIO(serialPortId, updateRateHz, processedData, m_ioCompleteSink, m_boardCapabilities);
            }
            m_ioThread.Start();
        }

        /// <summary>
        /// Returns the current pitch value (in degrees, from -180 to 180) reported by the sensor.
        /// </summary>
        /// <remarks>Pitch is a measure of rotation around the X Axis.</remarks>
        /// <returns>The current pitch value in degrees (-180 to 180).</returns>
        public float GetPitch()
        {
            return m_pitch;
        }

        /// <summary>
        /// Returns the current roll value (in degrees, from -180 to 180) reported by the sensor.
        /// </summary>
        /// <remarks>Roll is a measure of rotation around the Y Axis.</remarks>
        /// <returns>The current Roll value in degrees (-180 to 180).</returns>
        public float GetRoll()
        {
            return m_roll;
        }

        /// <summary>
        /// Returns the current yaw value (in degrees, from -180 to 180) reported by the sensor.
        /// </summary>
        /// <remarks>
        /// Yaw is a measure of rotation around the Z Axis (which is perpendicular to the earth).
        /// <para></para>
        /// Note that the returned yaw value will be offset by a user-specified
        /// offset value; this user-specified offset value is set by invoking
        /// the <see cref="ZeroYaw"/> method.
        /// </remarks>
        /// <returns>The current yaw value in degrees (-180 to 180).</returns>
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

        /// <summary>
        /// Returns the current tilt-compensated compass heading value (in degrees,
        /// from 0 to 360) reported by the sensor.
        /// </summary>
        /// <remarks>
        /// Note that this vallue is sensed by a magnetometer, which can
        /// be affected by nearby magnetic fields (e.g., the magnetic
        /// fields generated by nearby motors).
        /// <para/>
        /// Before using this value, ensure that (a) the magnetometer has been
        /// calibrated and (b) that a magnetic disturbance is not taking
        /// place at the instant when the compass heading was generated.
        /// </remarks>
        /// <returns>The current tilt-compensated compass heading, in degrees (0-360).</returns>
        public float GetCompassHeading()
        {
            return m_compassHeading;
        }

        /// <summary>
        /// Sets the user-specified yaw offset to the current
        /// yaw value reported by the sensor.
        /// </summary>
        /// <remarks>
        /// This user-specified yaw offset is automatically subtracted from subsequent
        /// yaw values reported by the <see cref="GetYaw"/> method.
        /// </remarks>
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

        /// <summary>
        /// Returns true if the sensor is currently performing automatic
        /// gyro/accelerometer calibration.
        /// </summary>
        /// <remarks>
        /// Automatic calibration occurs when the sensor is initially powered on,
        /// during which time the sensor should be held still, with the Z-axis
        /// pointing up (perpendicular to the earth).
        /// <para>NOTE: During this automatic callication, the yaw, pitch and roll
        /// values returned may not be accurate</para>
        /// <para>Once calibration is complete, the sensor will automatically remove 
        /// an internal yaw offset value from all reported values.</para>
        /// </remarks>
        /// <returns>True if the sensor is currently automatically calibrating the gyro
        /// and accelerometer sensors.</returns>
        public bool IsCalibrating()
        {
            return !((m_calStatus &
                        AHRSProtocol.NAVX_CAL_STATUS_IMU_CAL_STATE_MASK) ==
                            AHRSProtocol.NAVX_CAL_STATUS_IMU_CAL_COMPLETE);
        }

        /// <summary>
        /// Indicates whether the sensor is currently connected to the host system.
        /// </summary>
        /// <remarks>A connection is considered established whenever 
        /// communication with the sensor has occured recently.</remarks>
        /// <returns>True if a valid update has been recently received from the sensor.</returns>
        public bool IsConnected()
        {
            return m_io.IsConnected();
        }

        /// <summary>
        /// Returns the count in bytes of data recieved from the sensor.
        /// </summary>
        /// <remarks>This could be useful for diagnosing connectivity issues.
        /// <para>If the byte count is increasing but the update count (see <see cref="GetUpdateCount"/>)
        /// is not, this indicates a software misconfiguration.</para></remarks>
        /// <returns>The number of bytes received from the sensor.</returns>
        public double GetByteCount()
        {
            return m_io.GetByteCount();
        }

        /// <summary>
        /// Returns teh count of valid updates which have been received 
        /// from the sensor.
        /// </summary>
        /// <remarks>This count should increase at the same
        /// rate indicated by the configured update rate.</remarks>
        /// <returns>The number of valid updates received from the sensor.</returns>
        public double GetUpdateCount()
        {
            return m_io.GetUpdateCount();
        }

        /// <summary>
        /// Returns the current linear acceleration in the X-axis (in G).
        /// </summary>
        /// <remarks>
        /// World linear acceleration refers to raw acceleration data, which
        /// has had the gravity component removed, and which has been rotated to
        /// the same reference frame as the current yaw value. The resulting
        /// value represents the current acceleration in the X-Axis of the body
        /// (e.g., the robot) on which the sensor is mounted.
        /// </remarks>
        /// <returns>Current world linear acceleration in the X-axis (in G).</returns>
        public float GetWorldLinearAccelX()
        {
            return this.m_worldLinearAccelX;
        }

        /// <summary>
        /// Returns the current linear acceleration in the Y-axis (in G).
        /// </summary>
        /// <remarks>
        /// World linear acceleration refers to raw acceleration data, which
        /// has had the gravity component removed, and which has been rotated to
        /// the same reference frame as the current yaw value. The resulting
        /// value represents the current acceleration in the Y-Axis of the body
        /// (e.g., the robot) on which the sensor is mounted.
        /// </remarks>
        /// <returns>Current world linear acceleration in the Y-axis (in G).</returns>
        public float GetWorldLinearAccelY()
        {
            return this.m_worldLinearAccelY;
        }

        /// <summary>
        /// Returns the current linear acceleration in the Z-axis (in G).
        /// </summary>
        /// <remarks>
        /// World linear acceleration refers to raw acceleration data, which
        /// has had the gravity component removed, and which has been rotated to
        /// the same reference frame as the current yaw value. The resulting
        /// value represents the current acceleration in the Z-Axis of the body
        /// (e.g., the robot) on which the sensor is mounted.
        /// </remarks>
        /// <returns>Current world linear acceleration in the Z-axis (in G).</returns>
        public float GetWorldLinearAccelZ()
        {
            return this.m_worldLinearAccelZ;
        }

        /// <summary>
        /// Indicates if the sensor is currently detecting motion.
        /// </summary>
        /// <remarks>This detection is based upon the X and Y-axis world linear
        /// acceleration values. If the sum of the absolute alues of the X and Y axis
        /// exceed a "motion threshold", the motion state is indicated.</remarks>
        /// <returns>True if the sensor is currently detecting motion.</returns>
        public bool IsMoving()
        {
            return m_isMoving;
        }

        /// <summary>
        /// Indicates if the sensor is currently detecting yaw rotation.
        /// </summary>
        /// <remarks>The detection is based upon whether the change in yaw over
        /// the last second exceeds the "Rotation Threshold."
        /// <para>Yaw Rotation can occur either when the sensor is rotation, or
        /// when the sensor is not rotating AND the current gyro calibration
        /// is insufficiently calibrated to yield the standard yaw drift rate.
        /// </para></remarks>
        /// <returns>True if the sensor is currently detecting motion.</returns>
        public bool IsRotating()
        {
            return m_isRotating;
        }

        /// <summary>
        /// Returns the current barometric pressure.
        /// </summary>
        /// <remarks>
        /// This value is based upon calibrated reading from the onboard
        /// pressure sensor.
        /// <para>NOTE: This value is only valid for a NavX Aero. To determine
        /// whether this value is valid, see <see cref="IsAltitudeValid"/>.</para>
        /// </remarks>
        /// <returns>Current barometric pressure in millibar (NavX aero only).</returns>
        public float GetBarometricPressure()
        {
            return m_baroPressure;
        }

        /// <summary>
        /// Returns the current altitude.
        /// </summary>
        /// <remarks>This value is based upon calibrated reading from a
        /// barometric pressure sensor, and the currently configured
        /// sea-level barometric pressure [NavX Aero only].
        /// <para>Note: This value is only valid in sensors including a pressure
        /// sensor. To determine whether this value is valid, see <see cref="IsAltitudeValid"/>.
        /// </para></remarks>
        /// <returns>Current altitude in meters (if valid).</returns>
        public float GetAltitude()
        {
            return m_altitude;
        }

        /// <summary>
        /// Indicates whether the current altitude (and barometric pressure) data is valid.
        /// </summary>
        /// <remarks>This value will only be true for a sensor with an onboard
        /// pressure sensor installed.
        /// <para>If this value is false for a board with an installed pressure sensor,
        /// this indicates a malfunction of the onboard pressure sensor.</para></remarks>
        /// <returns>True if a working pressure seonsor is installed.</returns>
        public bool IsAltitudeValid()
        {
            return this.m_altitudeValid;
        }

        /// <summary>
        /// Returns the "fused" (9-axis) heading.
        /// </summary>
        /// <remarks>
        /// The 9-axis heading is the fusion of the yaw angle, the tilt-corrected
        /// compass heading, and magnetic disturbance detection. Note that the 
        /// magnetometer calibraion procedure is required in order to achieve valid 9-axis headings.
        /// <para/>
        /// The 9-axis Heading represents the sensor's best estimate of current heading,
        /// based upon the last known valid Compass Angle, and updated b y the change in the
        /// Yaw Angle since the last known valid Compass Angle. The last known valid Compass
        /// Angle is updated whenever a Calibrated Compass Alge is read and the sensor has 
        /// recently rotate less then the Compass Noise Bandwidth (~2 degrees).
        /// </remarks>
        /// <returns>Fused Heading in Degrees (range 0-360).</returns>
        public float GetFusedHeading()
        {
            return m_fusedHeading;
        }

        /// <summary>
        /// Indicates whether the current magnetic field strength diverges from the calibrated
        /// value for the earth's magnetic field by more than the currently-
        /// configured Magnetic Disturbance Ratio.
        /// </summary>
        /// <remarks>This function will always return false if the sensor's magnetometer
        /// has not yet been calibrated; see <see cref="IsMagnetometerCalibrated"/>.</remarks>
        /// <returns>True if a magnetic disturbance is detected (or the magnetometer is uncalibrated).</returns>
        public bool IsMagneticDisturbance()
        {
            return m_magneticDisturbance;
        }

        /// <summary>
        /// Indicates whether the magnetometer has been calibrated.
        /// </summary>
        /// <remarks>Magnetometer Calibration must be performed by the user.
        /// <para/>
        /// Note that if this function does indicate the magnetometer is calibrated,
        /// this does not necessarily mean that the calibration quality is sufficient
        /// to yield valid compass headings.
        /// </remarks>
        /// <returns>True if the magnetometer calibration has been performed.</returns>
        public bool IsMagnetometerCalibrated()
        {
            return m_isMagnetometerCalibrated;
        }

        /* Unit Quaternions */

        /// <summary>
        /// Returns the imaginary porton (W) of the Orientation Quanterion.
        /// </summary>
        /// <remarks>W is the Orientation Quanterion which fully describes
        /// the current sensor orientation with respect to the reference angle
        /// defined as the angle at which the yaw was last "zeroed".
        /// <para/>
        /// Each quaternion value (W,X,Y,Z) is expressed as a value ranging from -2
        /// to 2.  This total range(4) can be associated with a unit circle, since
        /// each circle is comprised of 4 PI Radians.
        /// <para/>
        /// For more information on Quanterions and their use, please see this <see cref="!:https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation">description</see>.
        /// </remarks>
        /// <returns>The imaginary portion (W) of the quanterion.</returns>
        public float GetQuaternionW()
        {
            return ((float)m_quaternionW / 16384.0f);
        }

        /// <summary>
        /// Returns the real portion (X axis) of the Orientation Quanterion.
        /// </summary>
        /// <remarks>X is the Orientation Quanterion which fully describes
        /// the current sensor orientation with respect to the reference angle
        /// defined as the angle at which the yaw was last "zeroed".
        /// <para/>
        /// Each quaternion value (W,X,Y,Z) is expressed as a value ranging from -2
        /// to 2.  This total range(4) can be associated with a unit circle, since
        /// each circle is comprised of 4 PI Radians.
        /// <para/>
        /// For more information on Quanterions and their use, please see this <see cref="!:https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation">description</see>.
        /// </remarks>
        /// <returns>The real portion (X) of the quanterion.</returns>
        public float GetQuaternionX()
        {
            return ((float)m_quaternionX / 16384.0f);
        }

        /// <summary>
        /// Returns the real portion (Y axis) of the Orientation Quanterion.
        /// </summary>
        /// <remarks>Y is the Orientation Quanterion which fully describes
        /// the current sensor orientation with respect to the reference angle
        /// defined as the angle at which the yaw was last "zeroed".
        /// <para/>
        /// Each quaternion value (W,X,Y,Z) is expressed as a value ranging from -2
        /// to 2.  This total range(4) can be associated with a unit circle, since
        /// each circle is comprised of 4 PI Radians.
        /// <para/>
        /// For more information on Quanterions and their use, please see this <see cref="!:https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation">description</see>.
        /// </remarks>
        /// <returns>The real portion (Y) of the quanterion.</returns>
        public float GetQuaternionY()
        {
            return ((float)m_quaternionY / 16384.0f);
        }

        /// <summary>
        /// Returns the real portion (Z axis) of the Orientation Quanterion.
        /// </summary>
        /// <remarks>Z is the Orientation Quanterion which fully describes
        /// the current sensor orientation with respect to the reference angle
        /// defined as the angle at which the yaw was last "zeroed".
        /// <para/>
        /// Each quaternion value (W,X,Y,Z) is expressed as a value ranging from -2
        /// to 2.  This total range(4) can be associated with a unit circle, since
        /// each circle is comprised of 4 PI Radians.
        /// <para/>
        /// For more information on Quanterions and their use, please see this <see cref="!:https://en.wikipedia.org/wiki/Quaternions_and_spatial_rotation">description</see>.
        /// </remarks>
        /// <returns>The real portion (Z) of the quanterion.</returns>
        public float GetQuaternionZ()
        {
            return ((float)m_quaternionZ / 16384.0f);
        }

        /// <summary>
        /// Zeros the displacement integration variables.
        /// </summary>
        /// <remarks>Invoke this at the moment integration begins.</remarks>
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

        /// <summary>
        /// Updates the displacement of the sensor based on the latest acceleration samples.
        /// </summary>
        /// <remarks>
        /// Each time new linear acceleration samples are received, this function should be invoked.
        /// This function transforms acceleration in G to meters/sec^2, then converts this value to
        /// Velocity in meters/sec(based upon velocity in the previous sample).  Finally, this value
        /// is converted to displacement in meters, and integrated.
        /// </remarks>
        /// <param name="accelXG">The X acceleration in Gs</param>
        /// <param name="accelYG">The Y acceleration in Gs</param>
        /// <param name="updateRateHz">The update rate of the data.</param>
        /// <param name="is_moving">True if moving.</param>
        private void UpdateDisplacement(float accelXG, float accelYG,
                                            int updateRateHz, bool is_moving)
        {
            m_integrator.UpdateDisplacement(accelXG, accelYG, updateRateHz, is_moving);
        }

        /// <summary>
        /// Returns the velocity of the X axis [Experimental].
        /// </summary>
        /// <remarks>
        /// NOTE: This feature is experimental.  Velocity measures rely on integration
        /// of acceleration values from MEMS accelerometers which yield "noisy" values.The
        /// resulting velocities are not known to be very accurate.
        /// </remarks>
        /// <returns>The Current X axis velocity in meters/sec</returns>
        public float GetVelocityX()
        {
            return (m_boardCapabilities.IsDisplacementSupported() ? m_velocity[0] : m_integrator.GetVelocityX());
        }

        /// <summary>
        /// Returns the velocity of the Y axis [Experimental].
        /// </summary>
        /// <remarks>
        /// NOTE: This feature is experimental.  Velocity measures rely on integration
        /// of acceleration values from MEMS accelerometers which yield "noisy" values.The
        /// resulting velocities are not known to be very accurate.
        /// </remarks>
        /// <returns>The Current Y axis velocity in meters/sec</returns>
        public float GetVelocityY()
        {
            return (m_boardCapabilities.IsDisplacementSupported() ? m_velocity[1] : m_integrator.GetVelocityY());
        }

        /// <summary>
        /// Returns the velocity of the Z axis [Experimental].
        /// </summary>
        /// <remarks>
        /// NOTE: This feature is experimental.  Velocity measures rely on integration
        /// of acceleration values from MEMS accelerometers which yield "noisy" values.The
        /// resulting velocities are not known to be very accurate.
        /// </remarks>
        /// <returns>The Current Z axis velocity in meters/sec</returns>
        public float GetVelocityZ()
        {
            return (m_boardCapabilities.IsDisplacementSupported() ? m_velocity[2] : 0.0f);
        }

        /// <summary>
        /// Returns the last displacement of the X axis since <see cref="ResetDisplacement"/> was last 
        /// invoked [Experimental].
        /// </summary>
        /// <remarks>
        /// NOTE:  This feature is experimental.  Displacement measures rely on double-integration
        /// of acceleration values from MEMS accelerometers which yield "noisy" values.The
        /// resulting displacement are not known to be very accurate, and the amount of error 
        /// increases quickly as time progresses.
        /// </remarks>
        /// <returns>The Current X axis displacement since last reset (meters).</returns>
        public float GetDisplacementX()
        {
            return (m_boardCapabilities.IsDisplacementSupported() ? m_displacement[0] : m_integrator.GetVelocityX());
        }

        /// <summary>
        /// Returns the last displacement of the Y axis since <see cref="ResetDisplacement"/> was last 
        /// invoked [Experimental].
        /// </summary>
        /// <remarks>
        /// NOTE:  This feature is experimental.  Displacement measures rely on double-integration
        /// of acceleration values from MEMS accelerometers which yield "noisy" values.The
        /// resulting displacement are not known to be very accurate, and the amount of error 
        /// increases quickly as time progresses.
        /// </remarks>
        /// <returns>The Current Y axis displacement since last reset (meters).</returns>
        public float GetDisplacementY()
        {
            return (m_boardCapabilities.IsDisplacementSupported() ? m_displacement[1] : m_integrator.GetVelocityY());
        }

        /// <summary>
        /// Returns the last displacement of the Z axis since <see cref="ResetDisplacement"/> was last 
        /// invoked [Experimental].
        /// </summary>
        /// <remarks>
        /// NOTE:  This feature is experimental.  Displacement measures rely on double-integration
        /// of acceleration values from MEMS accelerometers which yield "noisy" values.The
        /// resulting displacement are not known to be very accurate, and the amount of error 
        /// increases quickly as time progresses.
        /// </remarks>
        /// <returns>The Current Z axis displacement since last reset (meters).</returns>
        public float GetDisplacementZ()
        {
            return (m_boardCapabilities.IsDisplacementSupported() ? m_displacement[2] : 0.0f);
        }

        /***********************************************************/
        /* Internal Implementation                                  */
        /***********************************************************/

        private void CommonInit(byte updateRateHz)
        {
            this.m_boardCapabilities = new BoardCapabilities(this);
            this.m_ioCompleteSink = new IoCompleteNotification(this);
            this.m_ioThread = new IoThread(this);
            this.m_updateRateHz = updateRateHz;
            m_integrator = new InertialDataIntegrator();
            m_yawOffsetTracker = new OffsetTracker(YawHistoryLength);
            m_yawAngleTracker = new ContinuousAngleTracker();
        }

        /***********************************************************/
        /* PIDSource Interface Implementation                      */
        /***********************************************************/

        /// <summary>
        /// Returns the current value to use for PID, based on <see cref="PIDSourceType"/>.
        /// </summary>
        /// <remarks>
        /// If <see cref="PIDSourceType"/> is <see cref="PIDSourceType.Displacement"/>, this
        /// returns the Yaw angle in degrees (-180 to 180) <see cref="GetYaw"/>.
        /// If <see cref="PIDSourceType"/> is <see cref="PIDSourceType.Rate"/>, this
        /// returns the rate of rotation in degrees per second. <see cref="GetRate"/>. 
        /// </remarks>
        /// <returns>The current yaw angle or rate.</returns>
        public double PidGet()
        {
            switch (PIDSourceType)
            {
                case PIDSourceType.Rate:
                    return GetRate();
                case PIDSourceType.Displacement:
                    return GetYaw();
                default:
                    return 0.0;
            }
        }

        /// <inheritdoc/>
        public PIDSourceType PIDSourceType { get; set; } = PIDSourceType.Displacement;

        /// <summary>
        /// Returns the total accumulated yaw angle (Z Axis, in degrees) reported by
        /// the sensor.
        /// </summary>
        /// <remarks>
        /// NOTE: The angle is continuous, meaning it's range is beyond 360 degrees.
        /// This ensures that algorithms that wouldn't want to see a discontinuity 
        /// in the gyro output as it sweeps past 0 on the second time around.
        /// <para/>
        /// Note that the returned yaw value will be offset by a user-specified
        /// offset value; this user-specified offset value is set by 
        /// invoking the <see cref="ZeroYaw"/> method.
        /// <para/>The returned value is based on integration of the returned rate
        /// from the Z-axis (yaw) gyro.
        /// </remarks>
        /// <returns>The total accumulated yaw angle (Z axis) of the robot in degrees.</returns>
        public override double GetAngle()
        {
            return m_yawAngleTracker.GetAngle();
        }

        /// <summary>
        /// Return the rate of rotation of the yaw (Z-axis) gyro.
        /// </summary>
        /// <remarks>The rate is based on the most recent reading of the yaw gyro angle.</remarks>
        /// <returns>The current rate of change in yaw angle (degrees/second).</returns>
        public override double GetRate()
        {
            return m_yawAngleTracker.GetRate();
        }

        /// <summary>
        /// Reset the Yaw gyro.
        /// </summary>
        /// <remarks>Resets the Gyro Z (yaw) axis to a heading of zero. This can be used
        /// if there is significant drift in the gyro and it needs to be recallibrated
        /// after it has been running.</remarks>
        public override void Reset()
        {
            ZeroYaw();
        }

        private const float DevUnitsMax = 32768.0f;

        /// <summary>
        /// Returns the current raw (unprocessed) X-axis gyro rotation rate.
        /// </summary>
        /// <remarks>
        /// NOTE:  this value is un-processed, and should only be accessed by advanced users.
        /// Typically, rotation about the X Axis is referred to as "Pitch".  Calibrated
        /// and Integrated Pitch data is accessible via the <see cref="GetPitch"/> method.
        /// </remarks>
        /// <returns>The current X rotation rate (degrees/sec).</returns>
        public float GetRawGyroX()
        {
            return this.m_rawGyroX / (DevUnitsMax / (float)m_gyroFsrDps);
        }

        /// <summary>
        /// Returns the current raw (unprocessed) Y-axis gyro rotation rate.
        /// </summary>
        /// <remarks>
        /// NOTE:  this value is un-processed, and should only be accessed by advanced users.
        /// Typically, rotation about the Y Axis is referred to as "Roll".  Calibrated
        /// and Integrated Roll data is accessible via the <see cref="GetRoll"/> method.
        /// </remarks>
        /// <returns>The current Y rotation rate (degrees/sec).</returns>
        public float GetRawGyroY()
        {
            return this.m_rawGyroY / (DevUnitsMax / (float)m_gyroFsrDps);
        }

        /// <summary>
        /// Returns the current raw (unprocessed) Z-axis gyro rotation rate.
        /// </summary>
        /// <remarks>
        /// NOTE:  this value is un-processed, and should only be accessed by advanced users.
        /// Typically, rotation about the Z Axis is referred to as "Yaw".  Calibrated
        /// and Integrated Yaw data is accessible via the <see cref="GetYaw"/> method.
        /// </remarks>
        /// <returns>The current Z rotation rate (degrees/sec).</returns>
        public float GetRawGyroZ()
        {
            return this.m_rawGyroZ / (DevUnitsMax / (float)m_gyroFsrDps);
        }

        /// <summary>
        /// Returns the current raw (unprocessed) X-Axis acceleration rate (In G).
        /// </summary>
        /// <remarks>
        /// NOTE: this value is unprocessed, and should only be accessed by advanced users.  This raw value
        /// has not had acceleration due to gravity removed from it, and has not been rotated to
        /// the world reference frame. Gravity-corrected, world reference frame-corrected
        /// X axis acceleration data is accessible via the <see cref="GetWorldLinearAccelX"/> method.
        /// </remarks>
        /// <returns>The current X acceleration rate (in G)</returns>
        public float GetRawAccelX()
        {
            return this.m_rawAccelX / (DevUnitsMax / (float)m_accelFsrG);
        }

        /// <summary>
        /// Returns the current raw (unprocessed) Y-Axis acceleration rate (In G).
        /// </summary>
        /// <remarks>
        /// NOTE: this value is unprocessed, and should only be accessed by advanced users.  This raw value
        /// has not had acceleration due to gravity removed from it, and has not been rotated to
        /// the world reference frame. Gravity-corrected, world reference frame-corrected
        /// Y axis acceleration data is accessible via the <see cref="GetWorldLinearAccelY"/> method.
        /// </remarks>
        /// <returns>The current Y acceleration rate (in G)</returns>
        public float GetRawAccelY()
        {
            return this.m_rawAccelY / (DevUnitsMax / (float)m_accelFsrG);
        }

        /// <summary>
        /// Returns the current raw (unprocessed) Z-Axis acceleration rate (In G).
        /// </summary>
        /// <remarks>
        /// NOTE: this value is unprocessed, and should only be accessed by advanced users.  This raw value
        /// has not had acceleration due to gravity removed from it, and has not been rotated to
        /// the world reference frame. Gravity-corrected, world reference frame-corrected
        /// Z axis acceleration data is accessible via the <see cref="GetWorldLinearAccelZ"/> method.
        /// </remarks>
        /// <returns>The current Z acceleration rate (in G)</returns>
        public float GetRawAccelZ()
        {
            return this.m_rawAccelZ / (DevUnitsMax / (float)m_accelFsrG);
        }

        private const float UteslaPerDevUnit = 0.15f;

        /// <summary>
        /// Returns the current raw (unprocessed) X-axis magnetometer reading.
        /// </summary>
        /// <remarks>
        /// NOTE: this value is unprocessed, and should only be accessed by advanced users.  This raw value
        /// has not been tilt-corrected, and has not been combined with the other magnetometer axis
        /// data to yield a compass heading.Tilt-corrected compass heading data is accessible
        /// via the <see cref="GetCompassHeading"/> method.
        /// </remarks>
        /// <returns>Returns the X mag field strenth (in uTesla).</returns>
        public float GetRawMagX()
        {
            return this.m_calMagX / UteslaPerDevUnit;
        }

        /// <summary>
        /// Returns the current raw (unprocessed) Y-axis magnetometer reading.
        /// </summary>
        /// <remarks>
        /// NOTE: this value is unprocessed, and should only be accessed by advanced users.  This raw value
        /// has not been tilt-corrected, and has not been combined with the other magnetometer axis
        /// data to yield a compass heading.Tilt-corrected compass heading data is accessible
        /// via the <see cref="GetCompassHeading"/> method.
        /// </remarks>
        /// <returns>Returns the Y mag field strenth (in uTesla).</returns>
        public float GetRawMagY()
        {
            return this.m_calMagY / UteslaPerDevUnit;
        }

        /// <summary>
        /// Returns the current raw (unprocessed) Z-axis magnetometer reading.
        /// </summary>
        /// <remarks>
        /// NOTE: this value is unprocessed, and should only be accessed by advanced users.  This raw value
        /// has not been tilt-corrected, and has not been combined with the other magnetometer axis
        /// data to yield a compass heading.Tilt-corrected compass heading data is accessible
        /// via the <see cref="GetCompassHeading"/> method.
        /// </remarks>
        /// <returns>Returns the Z mag field strenth (in uTesla).</returns>
        public float GetRawMagZ()
        {
            return this.m_calMagZ / UteslaPerDevUnit;
        }

        /// <summary>
        /// Returns the current barometric pressure [NavX Aero only].
        /// </summary>
        /// <remarks>
        /// This value is valid only if a barometric pressure sensor is onboard.
        /// </remarks>
        /// <returns>The current barometric pressure (millibar).</returns>
        public float GetPressure()
        {
            // TODO implement for navX-Aero.
            return 0;
        }

        /// <summary>
        /// Returns the current reported temperature.
        /// </summary>
        /// <remarks>
        /// This value may be useful in order to perfomr advanced temperature-
        /// correction of raw gyroscope and acceleraometer values.
        /// </remarks>
        /// <returns>The current temperature (degrees centigrade).</returns>
        public float GetTempC()
        {
            return this.m_mpuTempC;
        }

        /// <summary>
        /// Returns information regarding which sensor board axis (X, Y or Z) and direction
        /// (up/down) is currently configured to report angle values.
        /// </summary>
        /// <remarks>If the board firmware configuration supports Omnimount, the board yaw
        /// axis/direction are configurable.
        /// <para/>
        /// For more information on Omnimount, please see: 
        /// <see cref="!:http://navx-mxp.kauailabs.com/installation/omnimount/>Omnimount"/>
        /// </remarks>
        /// <returns>The currently-configured board yaw axis/direction.</returns>
        public BoardYawAxis GetBoardYawAxis()
        {
            BoardYawAxis yawAxis = new BoardYawAxis();
            short yawAxisInfo = (short)(m_capabilityFlags >> 3);
            yawAxisInfo &= 7;
            if (yawAxisInfo == AHRSProtocol.OMNIMOUNT_DEFAULT)
            {
                yawAxis.Up = true;
                yawAxis.BoardAxis = BoardAxis.KBoardAxisZ;
            }
            else
            {
                yawAxis.Up = (((yawAxisInfo & 0x01) != 0) ? true : false);
                yawAxisInfo >>= 1;
                switch ((byte)yawAxisInfo)
                {
                    case 0:
                        yawAxis.BoardAxis = BoardAxis.KBoardAxisX;
                        break;
                    case 1:
                        yawAxis.BoardAxis = BoardAxis.KBoardAxisY;
                        break;
                    case 2:
                    default:
                        yawAxis.BoardAxis = BoardAxis.KBoardAxisZ;
                        break;
                }
            }
            return yawAxis;
        }

        /// <summary>
        /// Returns the version number of the firmware currently executing on the sensor.
        /// </summary>
        /// <remarks>
        /// To update the firmware to the latest version, please see:
        /// <see cref="!:http://navx-mxp.kauailabs.com/navx-mxp/support/updating-firmware/"/>
        /// </remarks>
        /// <returns>The firmware version in the format [MajorVersion].[MinorVersion]</returns>
        public string GetFirmwareVersion()
        {
            double versionNumber = (double)m_fwVerMajor;
            versionNumber += ((double)m_fwVerMinor / 10);
            string fwVersion = versionNumber.ToString();
            return fwVersion;
        }

        /***********************************************************/
        /* Runnable Interface Implementation                       */
        /***********************************************************/

        class IoThread
        {
            private AHRS m_parent;
            public IoThread(AHRS parent)
            {
                this.m_parent = parent;
            }

            Thread m_thread;
            bool m_stop;


            public void Start()
            {
                m_thread = new Thread(Run);
                m_thread.Start();
            }

            public void Run()
            {
                m_parent.m_io.Run();
            }

            public void Stop()
            {
            }
        }


        /***********************************************************/
        /* IBoardCapabilities Interface Implementation             */
        /***********************************************************/

        class BoardCapabilities : IBoardCapabilities
        {
            private AHRS m_parent;
            public BoardCapabilities(AHRS parent)
            {
                this.m_parent = parent;
            }


            public bool IsOmniMountSupported()
            {
                return (((m_parent.m_capabilityFlags & AHRSProtocol.NAVX_CAPABILITY_FLAG_OMNIMOUNT) != 0) ? true : false);
            }


            public bool IsBoardYawResetSupported()
            {
                return (((m_parent.m_capabilityFlags & AHRSProtocol.NAVX_CAPABILITY_FLAG_YAW_RESET) != 0) ? true : false);
            }


            public bool IsDisplacementSupported()
            {
                return (((m_parent.m_capabilityFlags & AHRSProtocol.NAVX_CAPABILITY_FLAG_VEL_AND_DISP) != 0) ? true : false);
            }
        }
        /***********************************************************/
        /* IIOCompleteNotification Interface Implementation        */
        /***********************************************************/

        class IoCompleteNotification : IIoCompleteNotification
        {
            private AHRS m_parent;
            public IoCompleteNotification(AHRS parent)
            {
                this.m_parent = parent;
            }


            public void SetYawPitchRoll(IMUProtocol.YPRUpdate yprUpdate)
            {
                lock (this)
                { // synchronized block
                    m_parent.m_yaw = yprUpdate.yaw;
                    m_parent.m_pitch = yprUpdate.pitch;
                    m_parent.m_roll = yprUpdate.roll;
                    m_parent.m_compassHeading = yprUpdate.compass_heading;
                }
            }


            public void SetAHRSPosData(AHRSProtocol.AHRSPosUpdate ahrsUpdate)
            {
                lock (this)
                { // synchronized block

                    /* Update base IMU class variables */

                    m_parent.m_yaw = ahrsUpdate.yaw;
                    m_parent.m_pitch = ahrsUpdate.pitch;
                    m_parent.m_roll = ahrsUpdate.roll;
                    m_parent.m_compassHeading = ahrsUpdate.compass_heading;
                    m_parent.m_yawOffsetTracker.UpdateHistory(ahrsUpdate.yaw);

                    /* Update AHRS class variables */

                    // 9-axis data
                    m_parent.m_fusedHeading = ahrsUpdate.fused_heading;

                    // Gravity-corrected linear acceleration (world-frame)
                    m_parent.m_worldLinearAccelX = ahrsUpdate.linear_accel_x;
                    m_parent.m_worldLinearAccelY = ahrsUpdate.linear_accel_y;
                    m_parent.m_worldLinearAccelZ = ahrsUpdate.linear_accel_z;

                    // Gyro/Accelerometer Die Temperature
                    m_parent.m_mpuTempC = ahrsUpdate.mpu_temp;

                    // Barometric Pressure/Altitude
                    m_parent.m_altitude = ahrsUpdate.altitude;
                    m_parent.m_baroPressure = ahrsUpdate.barometric_pressure;

                    // Status/Motion Detection
                    m_parent.m_isMoving =
                            (((ahrsUpdate.sensor_status &
                                    AHRSProtocol.NAVX_SENSOR_STATUS_MOVING) != 0)
                                    ? true : false);
                    m_parent.m_isRotating =
                            (((ahrsUpdate.sensor_status &
                                    AHRSProtocol.NAVX_SENSOR_STATUS_YAW_STABLE) != 0)
                                    ? false : true);
                    m_parent.m_altitudeValid =
                            (((ahrsUpdate.sensor_status &
                                    AHRSProtocol.NAVX_SENSOR_STATUS_ALTITUDE_VALID) != 0)
                                    ? true : false);
                    m_parent.m_isMagnetometerCalibrated =
                            (((ahrsUpdate.cal_status &
                                    AHRSProtocol.NAVX_CAL_STATUS_MAG_CAL_COMPLETE) != 0)
                                    ? true : false);
                    m_parent.m_magneticDisturbance =
                            (((ahrsUpdate.sensor_status &
                                    AHRSProtocol.NAVX_SENSOR_STATUS_MAG_DISTURBANCE) != 0)
                                    ? true : false);

                    m_parent.m_quaternionW = ahrsUpdate.quat_w;
                    m_parent.m_quaternionX = ahrsUpdate.quat_x;
                    m_parent.m_quaternionY = ahrsUpdate.quat_y;
                    m_parent.m_quaternionZ = ahrsUpdate.quat_z;

                    m_parent.m_velocity[0] = ahrsUpdate.vel_x;
                    m_parent.m_velocity[1] = ahrsUpdate.vel_y;
                    m_parent.m_velocity[2] = ahrsUpdate.vel_z;
                    m_parent.m_displacement[0] = ahrsUpdate.disp_x;
                    m_parent.m_displacement[1] = ahrsUpdate.disp_y;
                    m_parent.m_displacement[2] = ahrsUpdate.disp_z;

                    m_parent.m_yawAngleTracker.NextAngle(m_parent.GetYaw());
                }
            }


            public void SetRawData(AHRSProtocol.GyroUpdate rawDataUpdate)
            {
                lock (this)
                { // synchronized block
                    m_parent.m_rawGyroX = rawDataUpdate.gyro_x;
                    m_parent.m_rawGyroY = rawDataUpdate.gyro_y;
                    m_parent.m_rawGyroZ = rawDataUpdate.gyro_z;
                    m_parent.m_rawAccelX = rawDataUpdate.accel_x;
                    m_parent.m_rawAccelY = rawDataUpdate.accel_y;
                    m_parent.m_rawAccelZ = rawDataUpdate.accel_z;
                    m_parent.m_calMagX = rawDataUpdate.mag_x;
                    m_parent.m_calMagY = rawDataUpdate.mag_y;
                    m_parent.m_calMagZ = rawDataUpdate.mag_z;
                    m_parent.m_mpuTempC = rawDataUpdate.temp_c;
                }
            }


            public void SetAHRSData(AHRSProtocol.AHRSUpdate ahrsUpdate)
            {
                lock (this)
                { // synchronized block

                    /* Update base IMU class variables */

                    m_parent.m_yaw = ahrsUpdate.yaw;
                    m_parent.m_pitch = ahrsUpdate.pitch;
                    m_parent.m_roll = ahrsUpdate.roll;
                    m_parent.m_compassHeading = ahrsUpdate.compass_heading;
                    m_parent.m_yawOffsetTracker.UpdateHistory(ahrsUpdate.yaw);

                    /* Update AHRS class variables */

                    // 9-axis data
                    m_parent.m_fusedHeading = ahrsUpdate.fused_heading;

                    // Gravity-corrected linear acceleration (world-frame)
                    m_parent.m_worldLinearAccelX = ahrsUpdate.linear_accel_x;
                    m_parent.m_worldLinearAccelY = ahrsUpdate.linear_accel_y;
                    m_parent.m_worldLinearAccelZ = ahrsUpdate.linear_accel_z;

                    // Gyro/Accelerometer Die Temperature
                    m_parent.m_mpuTempC = ahrsUpdate.mpu_temp;

                    // Barometric Pressure/Altitude
                    m_parent.m_altitude = ahrsUpdate.altitude;
                    m_parent.m_baroPressure = ahrsUpdate.barometric_pressure;

                    // Magnetometer Data
                    m_parent.m_calMagX = ahrsUpdate.cal_mag_x;
                    m_parent.m_calMagY = ahrsUpdate.cal_mag_y;
                    m_parent.m_calMagZ = ahrsUpdate.cal_mag_z;

                    // Status/Motion Detection
                    m_parent.m_isMoving =
                            (((ahrsUpdate.sensor_status &
                                    AHRSProtocol.NAVX_SENSOR_STATUS_MOVING) != 0)
                                    ? true : false);
                    m_parent.m_isRotating =
                            (((ahrsUpdate.sensor_status &
                                    AHRSProtocol.NAVX_SENSOR_STATUS_YAW_STABLE) != 0)
                                    ? false : true);
                    m_parent.m_altitudeValid =
                            (((ahrsUpdate.sensor_status &
                                    AHRSProtocol.NAVX_SENSOR_STATUS_ALTITUDE_VALID) != 0)
                                    ? true : false);
                    m_parent.m_isMagnetometerCalibrated =
                            (((ahrsUpdate.cal_status &
                                    AHRSProtocol.NAVX_CAL_STATUS_MAG_CAL_COMPLETE) != 0)
                                    ? true : false);
                    m_parent.m_magneticDisturbance =
                            (((ahrsUpdate.sensor_status &
                                    AHRSProtocol.NAVX_SENSOR_STATUS_MAG_DISTURBANCE) != 0)
                                    ? true : false);

                    m_parent.m_quaternionW = ahrsUpdate.quat_w;
                    m_parent.m_quaternionX = ahrsUpdate.quat_x;
                    m_parent.m_quaternionY = ahrsUpdate.quat_y;
                    m_parent.m_quaternionZ = ahrsUpdate.quat_z;

                    m_parent.UpdateDisplacement(m_parent.m_worldLinearAccelX,
                            m_parent.m_worldLinearAccelY,
                            m_parent.m_updateRateHz,
                            m_parent.m_isMoving);

                    m_parent.m_yawAngleTracker.NextAngle(m_parent.GetYaw());
                }
            }


            public void SetBoardID(AHRSProtocol.BoardID boardId)
            {
                lock (this)
                { // synchronized block
                    m_parent.m_boardType = boardId.type;
                    m_parent.m_hwRev = boardId.hw_rev;
                    m_parent.m_fwVerMajor = boardId.fw_ver_major;
                    m_parent.m_fwVerMinor = boardId.fw_ver_minor;
                }
            }


            public void SetBoardState(BoardState boardState)
            {
                lock (this)
                { // synchronized block
                    m_parent.m_updateRateHz = boardState.UpdateRateHz;
                    m_parent.m_accelFsrG = boardState.AccelFsrG;
                    m_parent.m_gyroFsrDps = boardState.GyroFsrDps;
                    m_parent.m_capabilityFlags = boardState.CapabilityFlags;
                    m_parent.m_opStatus = boardState.OpStatus;
                    m_parent.m_sensorStatus = boardState.SensorStatus;
                    m_parent.m_calStatus = boardState.CalStatus;
                    m_parent.m_selftestStatus = boardState.SelftestStatus;
                }
            }
        };

        /// <summary>
        /// Not needed on the NavX. Calibration is Automatic.
        /// </summary>
        public override void Calibrate()
        {
            //Ignore because calibration is not needed.
        }
    }
}
