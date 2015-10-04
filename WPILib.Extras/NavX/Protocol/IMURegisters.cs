using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Extras.NavX.Protocol
{
    public class IMURegisters
    {
        /**********************************************/
        /* Device Identification Registers            */
        /**********************************************/

        public const byte NavxRegWhoami = 0x00; /* IMU_MODEL_XXX */
        public const byte NavxRegHwRev = 0x01;
        public const byte NavxRegFwVerMajor = 0x02;
        public const byte NavxRegFwVerMinor = 0x03;

        /**********************************************/
        /* Status and Control Registers               */
        /**********************************************/

        /* Read-write */
        public const byte NavxRegUpdateRateHz = 0x04; /* Range:  4 - 50 [unsigned byte] */
                                                                 /* Read-only */
                                                                 /* Accelerometer Full-Scale Range:  in units of G [unsigned byte] */
        public const byte NavxRegAccelFsrG = 0x05;
        /* Gyro Full-Scale Range (Degrees/Sec):  Range:  250, 500, 1000 or 2000 [unsigned short] */
        public const byte NavxRegGyroFsrDpsL = 0x06; /* Lower 8-bits of Gyro Full-Scale Range */
        public const byte NavxRegGyroFsrDpsH = 0x07; /* Upper 8-bits of Gyro Full-Scale Range */
        public const byte NavxRegOpStatus = 0x08; /* NAVX_OP_STATUS_XXX */
        public const byte NavxRegCalStatus = 0x09; /* NAVX_CAL_STATUS_XXX */
        public const byte NavxRegSelftestStatus = 0x0A; /* NAVX_SELFTEST_STATUS_XXX */
        public const byte NavxRegCapabilityFlagsL = 0x0B;
        public const byte NavxRegCapabilityFlagsH = 0x0C;

        /**********************************************/
        /* Processed Data Registers                   */
        /**********************************************/

        public const byte NavxRegSensorStatusL = 0x10; /* NAVX_SENSOR_STATUS_XXX */
        public const byte NavxRegSensorStatusH = 0x11;
        /* Timestamp:  [unsigned long] */
        public const byte NavxRegTimestampLL = 0x12;
        public const byte NavxRegTimestampLH = 0x13;
        public const byte NavxRegTimestampHL = 0x14;
        public const byte NavxRegTimestampHH = 0x15;

        /* Yaw, Pitch, Roll:  Range: -180.00 to 180.00 [signed hundredths] */
        /* Compass Heading:   Range: 0.00 to 360.00 [unsigned hundredths] */
        /* Altitude in Meters:  In units of meters [16:16] */

        public const byte NavxRegYawL = 0x16; /* Lower 8 bits of Yaw     */
        public const byte NavxRegYawH = 0x17; /* Upper 8 bits of Yaw     */
        public const byte NavxRegPitchL = 0x18; /* Lower 8 bits of Pitch   */
        public const byte NavxRegPitchH = 0x19; /* Upper 8 bits of Pitch   */
        public const byte NavxRegRollL = 0x1A; /* Lower 8 bits of Roll    */
        public const byte NavxRegRollH = 0x1B; /* Upper 8 bits of Roll    */
        public const byte NavxRegHeadingL = 0x1C; /* Lower 8 bits of Heading */
        public const byte NavxRegHeadingH = 0x1D; /* Upper 8 bits of Heading */
        public const byte NavxRegFusedHeadingL = 0x1E; /* Upper 8 bits of Fused Heading */
        public const byte NavxRegFusedHeadingH = 0x1F; /* Upper 8 bits of Fused Heading */
        public const byte NavxRegAltitudeIL = 0x20;
        public const byte NavxRegAltitudeIH = 0x21;
        public const byte NavxRegAltitudeDL = 0x22;
        public const byte NavxRegAltitudeDH = 0x23;

        /* World-frame Linear Acceleration: In units of +/- G * 1000 [signed thousandths] */

        public const byte NavxRegLinearAccXL = 0x24; /* Lower 8 bits of Linear Acceleration X */
        public const byte NavxRegLinearAccXH = 0x25; /* Upper 8 bits of Linear Acceleration X */
        public const byte NavxRegLinearAccYL = 0x26; /* Lower 8 bits of Linear Acceleration Y */
        public const byte NavxRegLinearAccYH = 0x27; /* Upper 8 bits of Linear Acceleration Y */
        public const byte NavxRegLinearAccZL = 0x28; /* Lower 8 bits of Linear Acceleration Z */
        public const byte NavxRegLinearAccZH = 0x29; /* Upper 8 bits of Linear Acceleration Z */

        /* Quaternion:  Range -1 to 1 [signed short ratio] */

        public const byte NavxRegQuatWL = 0x2A; /* Lower 8 bits of Quaternion W */
        public const byte NavxRegQuatWH = 0x2B; /* Upper 8 bits of Quaternion W */
        public const byte NavxRegQuatXL = 0x2C; /* Lower 8 bits of Quaternion X */
        public const byte NavxRegQuatXH = 0x2D; /* Upper 8 bits of Quaternion X */
        public const byte NavxRegQuatYL = 0x2E; /* Lower 8 bits of Quaternion Y */
        public const byte NavxRegQuatYH = 0x2F; /* Upper 8 bits of Quaternion Y */
        public const byte NavxRegQuatZL = 0x30; /* Lower 8 bits of Quaternion Z */
        public const byte NavxRegQuatZH = 0x31; /* Upper 8 bits of Quaternion Z */

        /**********************************************/
        /* Raw Data Registers                         */
        /**********************************************/

        /* Sensor Die Temperature:  Range +/- 150, In units of Centigrade * 100 [signed hundredths float */

        public const byte NavxRegMpuTempCL = 0x32; /* Lower 8 bits of Temperature */
        public const byte NavxRegMpuTempCH = 0x33; /* Upper 8 bits of Temperature */

        /* Raw, Calibrated Angular Rotation, in device units.  Value in DPS = units / GYRO_FSR_DPS [signed short] */

        public const byte NavxRegGyroXL = 0x34;
        public const byte NavxRegGyroXH = 0x35;
        public const byte NavxRegGyroYL = 0x36;
        public const byte NavxRegGyroYH = 0x37;
        public const byte NavxRegGyroZL = 0x38;
        public const byte NavxRegGyroZH = 0x39;

        /* Raw, Calibrated, Acceleration Data, in device units.  Value in G = units / ACCEL_FSR_G [signed short] */

        public const byte NavxRegAccXL = 0x3A;
        public const byte NavxRegAccXH = 0x3B;
        public const byte NavxRegAccYL = 0x3C;
        public const byte NavxRegAccYH = 0x3D;
        public const byte NavxRegAccZL = 0x3E;
        public const byte NavxRegAccZH = 0x3F;

        /* Raw, Calibrated, Un-tilt corrected Magnetometer Data, in device units.  1 unit = 0.15 uTesla [signed short] */

        public const byte NavxRegMagXL = 0x40;
        public const byte NavxRegMagXH = 0x41;
        public const byte NavxRegMagYL = 0x42;
        public const byte NavxRegMagYH = 0x43;
        public const byte NavxRegMagZL = 0x44;
        public const byte NavxRegMagZH = 0x45;

        /* Calibrated Pressure in millibars Valid Range:  10.00 Max:  1200.00 [16:16 float]  */

        public const byte NavxRegPressureIl = 0x46;
        public const byte NavxRegPressureIh = 0x47;
        public const byte NavxRegPressureDl = 0x48;
        public const byte NavxRegPressureDh = 0x49;

        /* Pressure Sensor Die Temperature:  Range +/- 150.00C [signed hundredths] */

        public const byte NavxRegPressureTempL = 0x4A;
        public const byte NavxRegPressureTempH = 0x4B;

        /**********************************************/
        /* Calibration Registers                      */
        /**********************************************/

        /* Yaw Offset: Range -180.00 to 180.00 [signed hundredths] */

        public const byte NavxRegYawOffsetL = 0x4C; /* Lower 8 bits of Yaw Offset */
        public const byte NavxRegYawOffsetH = 0x4D; /* Upper 8 bits of Yaw Offset */

        /* Quaternion Offset:  Range: -1 to 1 [signed short ratio]  */

        public const byte NavxRegQuatOffsetWL = 0x4E; /* Lower 8 bits of Quaternion W */
        public const byte NavxRegQuatOffsetWH = 0x4F; /* Upper 8 bits of Quaternion W */
        public const byte NavxRegQuatOffsetXL = 0x50; /* Lower 8 bits of Quaternion X */
        public const byte NavxRegQuatOffsetXH = 0x51; /* Upper 8 bits of Quaternion X */
        public const byte NavxRegQuatOffsetYL = 0x52; /* Lower 8 bits of Quaternion Y */
        public const byte NavxRegQuatOffsetYH = 0x53; /* Upper 8 bits of Quaternion Y */
        public const byte NavxRegQuatOffsetZL = 0x54; /* Lower 8 bits of Quaternion Z */
        public const byte NavxRegQuatOffsetZH = 0x55; /* Upper 8 bits of Quaternion Z */

        /**********************************************/
        /* Integrated Data Registers                  */
        /**********************************************/

        /* Integration Control (Write-Only)           */
        public const byte NavxRegIntegrationCtl = 0x56;
        public const byte NavxRegPadUnused = 0x57;

        /* Velocity:  Range -32768.9999 - 32767.9999 in units of Meters/Sec      */

        public const byte NavxRegVelXIL = 0x58;
        public const byte NavxRegVelXIH = 0x59;
        public const byte NavxRegVelXDL = 0x5A;
        public const byte NavxRegVelXDH = 0x5B;
        public const byte NavxRegVelYIL = 0x5C;
        public const byte NavxRegVelYIH = 0x5D;
        public const byte NavxRegVelYDL = 0x5E;
        public const byte NavxRegVelYDH = 0x5F;
        public const byte NavxRegVelZIL = 0x60;
        public const byte NavxRegVelZIH = 0x61;
        public const byte NavxRegVelZDL = 0x62;
        public const byte NavxRegVelZDH = 0x63;

        /* Displacement:  Range -32768.9999 - 32767.9999 in units of Meters      */

        public const byte NavxRegDispXIL = 0x64;
        public const byte NavxRegDispXIH = 0x65;
        public const byte NavxRegDispXDL = 0x66;
        public const byte NavxRegDispXDH = 0x67;
        public const byte NavxRegDispYIL = 0x68;
        public const byte NavxRegDispYIH = 0x69;
        public const byte NavxRegDispYDL = 0x6A;
        public const byte NavxRegDispYDH = 0x6B;
        public const byte NavxRegDispZIL = 0x6C;
        public const byte NavxRegDispZIH = 0x6D;
        public const byte NavxRegDispZDL = 0x6E;
        public const byte NavxRegDispZDH = 0x6F;

        public const byte NavxRegLast = NavxRegDispZDH;
    }
}
