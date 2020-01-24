using REV.SparkMax.Natives;
using System;
using System.Collections.Generic;
using System.Text;
using WPIUtil.NativeUtilities;

namespace REV.SparkMax
{
    [NativeInterface(typeof(ICANSparkMaxDriver))]
    public unsafe class CANSparkMaxDriver
    {
        private static ICANSparkMaxDriver driver;

        public static ErrorCode SetAnalogMode(void* handle, AnalogMode mode)
        {
            return driver.c_SparkMax_SetAnalogMode(handle, mode);
        }
        public static ErrorCode GetAnalogMode(void* handle, AnalogMode* mode)
        {
            return driver.c_SparkMax_GetAnalogMode(handle, mode);
        }
        public static ErrorCode GetEncoderPosition(void* handle, float* position)
        {
            return driver.c_SparkMax_GetEncoderPosition(handle, position);
        }
        public static ErrorCode GetEncoderVelocity(void* handle, float* velocity)
        {
            return driver.c_SparkMax_GetEncoderVelocity(handle, velocity);
        }
        public static ErrorCode SetPositionConversionFactor(void* handle, float conversion)
        {
            return driver.c_SparkMax_SetPositionConversionFactor(handle, conversion);
        }
        public static ErrorCode SetVelocityConversionFactor(void* handle, float conversion)
        {
            return driver.c_SparkMax_SetVelocityConversionFactor(handle, conversion);
        }
        public static ErrorCode GetPositionConversionFactor(void* handle, float* conversion)
        {
            return driver.c_SparkMax_GetPositionConversionFactor(handle, conversion);
        }
        public static ErrorCode GetVelocityConversionFactor(void* handle, float* conversion)
        {
            return driver.c_SparkMax_GetVelocityConversionFactor(handle, conversion);
        }
        public static ErrorCode SetAverageDepth(void* handle, uint depth)
        {
            return driver.c_SparkMax_SetAverageDepth(handle, depth);
        }
        public static ErrorCode GetAverageDepth(void* handle, uint* depth)
        {
            return driver.c_SparkMax_GetAverageDepth(handle, depth);
        }
        public static ErrorCode SetMeasurementPeriod(void* handle, uint samples)
        {
            return driver.c_SparkMax_SetMeasurementPeriod(handle, samples);
        }
        public static ErrorCode GetMeasurementPeriod(void* handle, uint* samples)
        {
            return driver.c_SparkMax_GetMeasurementPeriod(handle, samples);
        }
        public static ErrorCode SetCountsPerRevolution(void* handle, uint cpr)
        {
            return driver.c_SparkMax_SetCountsPerRevolution(handle, cpr);
        }
        public static ErrorCode GetCountsPerRevolution(void* handle, uint* cpr)
        {
            return driver.c_SparkMax_GetCountsPerRevolution(handle, cpr);
        }
        public static ErrorCode SetEncoderInverted(void* handle, byte inverted)
        {
            return driver.c_SparkMax_SetEncoderInverted(handle, inverted);
        }
        public static ErrorCode GetEncoderInverted(void* handle, byte* inverted)
        {
            return driver.c_SparkMax_GetEncoderInverted(handle, inverted);
        }
        public static ErrorCode GetAltEncoderPosition(void* handle, float* position)
        {
            return driver.c_SparkMax_GetAltEncoderPosition(handle, position);
        }
        public static ErrorCode GetAltEncoderVelocity(void* handle, float* velocity)
        {
            return driver.c_SparkMax_GetAltEncoderVelocity(handle, velocity);
        }
        public static ErrorCode SetAltEncoderPositionFactor(void* handle, float conversion)
        {
            return driver.c_SparkMax_SetAltEncoderPositionFactor(handle, conversion);
        }
        public static ErrorCode SetAltEncoderVelocityFactor(void* handle, float conversion)
        {
            return driver.c_SparkMax_SetAltEncoderVelocityFactor(handle, conversion);
        }
        public static ErrorCode GetAltEncoderPositionFactor(void* handle, float* conversion)
        {
            return driver.c_SparkMax_GetAltEncoderPositionFactor(handle, conversion);
        }
        public static ErrorCode GetAltEncoderVelocityFactor(void* handle, float* conversion)
        {
            return driver.c_SparkMax_GetAltEncoderVelocityFactor(handle, conversion);
        }
        public static ErrorCode SetAltEncoderAverageDepth(void* handle, uint depth)
        {
            return driver.c_SparkMax_SetAltEncoderAverageDepth(handle, depth);
        }
        public static ErrorCode GetAltEncoderAverageDepth(void* handle, uint* depth)
        {
            return driver.c_SparkMax_GetAltEncoderAverageDepth(handle, depth);
        }
        public static ErrorCode SetAltEncoderMeasurementPeriod(void* handle, uint samples)
        {
            return driver.c_SparkMax_SetAltEncoderMeasurementPeriod(handle, samples);
        }
        public static ErrorCode GetAltEncoderMeasurementPeriod(void* handle, uint* samples)
        {
            return driver.c_SparkMax_GetAltEncoderMeasurementPeriod(handle, samples);
        }
        public static ErrorCode SetAltEncoderCountsPerRevolution(void* handle, uint cpr)
        {
            return driver.c_SparkMax_SetAltEncoderCountsPerRevolution(handle, cpr);
        }
        public static ErrorCode GetAltEncoderCountsPerRevolution(void* handle, uint* cpr)
        {
            return driver.c_SparkMax_GetAltEncoderCountsPerRevolution(handle, cpr);
        }
        public static ErrorCode SetAltEncoderInverted(void* handle, byte inverted)
        {
            return driver.c_SparkMax_SetAltEncoderInverted(handle, inverted);
        }
        public static ErrorCode GetAltEncoderInverted(void* handle, byte* inverted)
        {
            return driver.c_SparkMax_GetAltEncoderInverted(handle, inverted);
        }
        public static ErrorCode SetDataPortConfig(void* handle, DataPortConfig config)
        {
            return driver.c_SparkMax_SetDataPortConfig(handle, config);
        }
        public static ErrorCode GetDataPortConfig(void* handle, DataPortConfig* config)
        {
            return driver.c_SparkMax_GetDataPortConfig(handle, config);
        }
        public static ErrorCode SetP(void* handle, int slotID, float gain)
        {
            return driver.c_SparkMax_SetP(handle, slotID, gain);
        }
        public static ErrorCode SetI(void* handle, int slotID, float gain)
        {
            return driver.c_SparkMax_SetI(handle, slotID, gain);
        }
        public static ErrorCode SetD(void* handle, int slotID, float gain)
        {
            return driver.c_SparkMax_SetD(handle, slotID, gain);
        }
        public static ErrorCode SetDFilter(void* handle, int slotID, float gain)
        {
            return driver.c_SparkMax_SetDFilter(handle, slotID, gain);
        }
        public static ErrorCode SetFF(void* handle, int slotID, float gain)
        {
            return driver.c_SparkMax_SetFF(handle, slotID, gain);
        }
        public static ErrorCode SetIZone(void* handle, int slotID, float IZone)
        {
            return driver.c_SparkMax_SetIZone(handle, slotID, IZone);
        }
        public static ErrorCode SetOutputRange(void* handle, int slotID, float min, float max)
        {
            return driver.c_SparkMax_SetOutputRange(handle, slotID, min, max);
        }
        public static ErrorCode GetP(void* handle, int slotID, float* gain)
        {
            return driver.c_SparkMax_GetP(handle, slotID, gain);
        }
        public static ErrorCode GetI(void* handle, int slotID, float* gain)
        {
            return driver.c_SparkMax_GetI(handle, slotID, gain);
        }
        public static ErrorCode GetD(void* handle, int slotID, float* gain)
        {
            return driver.c_SparkMax_GetD(handle, slotID, gain);
        }
        public static ErrorCode GetDFilter(void* handle, int slotID, float* gain)
        {
            return driver.c_SparkMax_GetDFilter(handle, slotID, gain);
        }
        public static ErrorCode GetFF(void* handle, int slotID, float* gain)
        {
            return driver.c_SparkMax_GetFF(handle, slotID, gain);
        }
        public static ErrorCode GetIZone(void* handle, int slotID, float* IZone)
        {
            return driver.c_SparkMax_GetIZone(handle, slotID, IZone);
        }
        public static ErrorCode GetOutputMin(void* handle, int slotID, float* min)
        {
            return driver.c_SparkMax_GetOutputMin(handle, slotID, min);
        }
        public static ErrorCode GetOutputMax(void* handle, int slotID, float* max)
        {
            return driver.c_SparkMax_GetOutputMax(handle, slotID, max);
        }
        public static ErrorCode SetSmartMotionMaxVelocity(void* handle, int slotID, float maxVel)
        {
            return driver.c_SparkMax_SetSmartMotionMaxVelocity(handle, slotID, maxVel);
        }
        public static ErrorCode SetSmartMotionMaxAccel(void* handle, int slotID, float maxAccel)
        {
            return driver.c_SparkMax_SetSmartMotionMaxAccel(handle, slotID, maxAccel);
        }
        public static ErrorCode SetSmartMotionMinOutputVelocity(void* handle, int slotID, float minVel)
        {
            return driver.c_SparkMax_SetSmartMotionMinOutputVelocity(handle, slotID, minVel);
        }
        public static ErrorCode SetSmartMotionAccelStrategy(void* handle, int slotID, AccelStrategy accelStrategy)
        {
            return driver.c_SparkMax_SetSmartMotionAccelStrategy(handle, slotID, accelStrategy);
        }
        public static ErrorCode SetSmartMotionAllowedClosedLoopError(void* handle, int slotID, float allowedError)
        {
            return driver.c_SparkMax_SetSmartMotionAllowedClosedLoopError(handle, slotID, allowedError);
        }
        public static ErrorCode GetSmartMotionMaxVelocity(void* handle, int slotID, float* maxVel)
        {
            return driver.c_SparkMax_GetSmartMotionMaxVelocity(handle, slotID, maxVel);
        }
        public static ErrorCode GetSmartMotionMaxAccel(void* handle, int slotID, float* maxAccel)
        {
            return driver.c_SparkMax_GetSmartMotionMaxAccel(handle, slotID, maxAccel);
        }
        public static ErrorCode GetSmartMotionMinOutputVelocity(void* handle, int slotID, float* minVel)
        {
            return driver.c_SparkMax_GetSmartMotionMinOutputVelocity(handle, slotID, minVel);
        }
        public static ErrorCode GetSmartMotionAccelStrategy(void* handle, int slotID, AccelStrategy* accelStrategy)
        {
            return driver.c_SparkMax_GetSmartMotionAccelStrategy(handle, slotID, accelStrategy);
        }
        public static ErrorCode GetSmartMotionAllowedClosedLoopError(void* handle, int slotID, float* allowedError)
        {
            return driver.c_SparkMax_GetSmartMotionAllowedClosedLoopError(handle, slotID, allowedError);
        }
        public static ErrorCode SetIMaxAccum(void* handle, int slotID, float iMaxAccum)
        {
            return driver.c_SparkMax_SetIMaxAccum(handle, slotID, iMaxAccum);
        }
        public static ErrorCode GetIMaxAccum(void* handle, int slotID, float* iMaxAccum)
        {
            return driver.c_SparkMax_GetIMaxAccum(handle, slotID, iMaxAccum);
        }
        public static ErrorCode SetIAccum(void* handle, float iAccum)
        {
            return driver.c_SparkMax_SetIAccum(handle, iAccum);
        }
        public static ErrorCode GetIAccum(void* handle, float* iAccum)
        {
            return driver.c_SparkMax_GetIAccum(handle, iAccum);
        }
        public static ErrorCode SetFeedbackDevice(void* handle, uint sensorID)
        {
            return driver.c_SparkMax_SetFeedbackDevice(handle, sensorID);
        }
        public static ErrorCode GetFeedbackDeviceID(void* handle, uint* id)
        {
            return driver.c_SparkMax_GetFeedbackDeviceID(handle, id);
        }
        public static ErrorCode SetFeedbackDeviceRange(void* handle, float min, float max)
        {
            return driver.c_SparkMax_SetFeedbackDeviceRange(handle, min, max);
        }
        public static APIVersion GetAPIVersion()
        {
            return driver.c_SparkMax_GetAPIVersion();
        }
        public static void SetLastError(void* handle, ErrorCode error)
        {
            driver.c_SparkMax_SetLastError(
         handle, error);
        }
        public static ErrorCode GetLastError(void* handle)
        {
            return driver.c_SparkMax_GetLastError(handle);
        }
        public static ErrorCode GenerateError(int deviceID, ErrorCode error)
        {
            return driver.c_SparkMax_GenerateError(deviceID, error);
        }
        public static void* Create(int deviceId, MotorType type)
        {
            return driver.c_SparkMax_Create(deviceId, type);
        }
        public static void* Create_Inplace(int deviceId)
        {
            return driver.c_SparkMax_Create_Inplace(deviceId);
        }
        public static void Destroy(void* handle)
        {
            driver.c_SparkMax_Destroy(
         handle);
        }
        public static ErrorCode GetFirmwareVersion(void* handle, FirmwareVersion* fwVersion)
        {
            return driver.c_SparkMax_GetFirmwareVersion(handle, fwVersion);
        }
        public static ErrorCode GetDeviceId(void* handle, int* deviceId)
        {
            return driver.c_SparkMax_GetDeviceId(handle, deviceId);
        }
        public static ErrorCode SetMotorType(void* handle, MotorType type)
        {
            return driver.c_SparkMax_SetMotorType(handle, type);
        }
        public static ErrorCode GetMotorType(void* handle, MotorType* type)
        {
            return driver.c_SparkMax_GetMotorType(handle, type);
        }
        public static ErrorCode SetPeriodicFramePeriod(void* handle, PeriodicFrame frameId, int periodMs)
        {
            return driver.c_SparkMax_SetPeriodicFramePeriod(handle, frameId, periodMs);
        }
        public static void SetControlFramePeriod(void* handle, int periodMs)
        {
            driver.c_SparkMax_SetControlFramePeriod(
         handle, periodMs);
        }
        public static int GetControlFramePeriod(void* handle)
        {
            return driver.c_SparkMax_GetControlFramePeriod(handle);
        }
        public static ErrorCode SetParameterFloat32(void* handle, ConfigParameter paramId, float value)
        {
            return driver.c_SparkMax_SetParameterFloat32(handle, paramId, value);
        }
        public static ErrorCode SetParameterInt32(void* handle, ConfigParameter paramId, int value)
        {
            return driver.c_SparkMax_SetParameterInt32(handle, paramId, value);
        }
        public static ErrorCode SetParameterUint32(void* handle, ConfigParameter paramId, uint value)
        {
            return driver.c_SparkMax_SetParameterUint32(handle, paramId, value);
        }
        public static ErrorCode SetParameterBool(void* handle, ConfigParameter paramId, byte value)
        {
            return driver.c_SparkMax_SetParameterBool(handle, paramId, value);
        }
        public static ErrorCode GetParameterFloat32(void* handle, ConfigParameter paramId, float* value)
        {
            return driver.c_SparkMax_GetParameterFloat32(handle, paramId, value);
        }
        public static ErrorCode GetParameterInt32(void* handle, ConfigParameter paramId, int* value)
        {
            return driver.c_SparkMax_GetParameterInt32(handle, paramId, value);
        }
        public static ErrorCode GetParameterUint32(void* handle, ConfigParameter paramId, uint* value)
        {
            return driver.c_SparkMax_GetParameterUint32(handle, paramId, value);
        }
        public static ErrorCode GetParameterBool(void* handle, ConfigParameter paramId, byte* value)
        {
            return driver.c_SparkMax_GetParameterBool(handle, paramId, value);
        }
        public static ErrorCode GetPeriodicStatus0(void* handle, PeriodicStatus0* rawframe)
        {
            return driver.c_SparkMax_GetPeriodicStatus0(handle, rawframe);
        }
        public static ErrorCode GetPeriodicStatus1(void* handle, PeriodicStatus1* rawframe)
        {
            return driver.c_SparkMax_GetPeriodicStatus1(handle, rawframe);
        }
        public static ErrorCode GetPeriodicStatus2(void* handle, PeriodicStatus2* rawframe)
        {
            return driver.c_SparkMax_GetPeriodicStatus2(handle, rawframe);
        }
        public static ErrorCode GetPeriodicStatus3(void* handle, PeriodicStatus3* rawframe)
        {
            return driver.c_SparkMax_GetPeriodicStatus3(handle, rawframe);
        }
        public static ErrorCode GetPeriodicStatus4(void* handle, PeriodicStatus4* rawframe)
        {
            return driver.c_SparkMax_GetPeriodicStatus4(handle, rawframe);
        }
        public static ErrorCode SetEncoderPosition(void* handle, float position)
        {
            return driver.c_SparkMax_SetEncoderPosition(handle, position);
        }
        public static ErrorCode SetAltEncoderPosition(void* handle, float position)
        {
            return driver.c_SparkMax_SetAltEncoderPosition(handle, position);
        }
        public static ErrorCode RestoreFactoryDefaults(void* handle, byte persist)
        {
            return driver.c_SparkMax_RestoreFactoryDefaults(handle, persist);
        }
        public static ErrorCode FactoryWipe(void* handle, byte persist)
        {
            return driver.c_SparkMax_FactoryWipe(handle, persist);
        }
        public static ErrorCode SetFollow(void* handle, uint followerArbId, uint followerCfg)
        {
            return driver.c_SparkMax_SetFollow(handle, followerArbId, followerCfg);
        }
        public static float SafeFloat(float f)
        {
            return driver.c_SparkMax_SafeFloat(f);
        }
        public static ErrorCode SetpointCommand(void* handle, float value, ControlType ctrl, int pidSlot, float arbFeedforward, int arbFFUnits)
        {
            return driver.c_SparkMax_SetpointCommand(handle, value, ctrl, pidSlot, arbFeedforward, arbFFUnits);
        }
        public static ErrorCode GetDRVStatus(void* handle, DRVStatus* drvStatus)
        {
            return driver.c_SparkMax_GetDRVStatus(handle, drvStatus);
        }
        public static ErrorCode SetInverted(void* handle, byte inverted)
        {
            return driver.c_SparkMax_SetInverted(handle, inverted);
        }
        public static ErrorCode GetInverted(void* handle, byte* inverted)
        {
            return driver.c_SparkMax_GetInverted(handle, inverted);
        }
        public static ErrorCode SetSmartCurrentLimit(void* handle, byte stallLimit, byte freeLimit, uint limitRPM)
        {
            return driver.c_SparkMax_SetSmartCurrentLimit(handle, stallLimit, freeLimit, limitRPM);
        }
        public static ErrorCode GetSmartCurrentLimit(void* handle, byte* stallLimit, byte* freeLimit, uint* limitRPM)
        {
            return driver.c_SparkMax_GetSmartCurrentLimit(handle, stallLimit, freeLimit, limitRPM);
        }
        public static ErrorCode SetSecondaryCurrentLimit(void* handle, float limit, int chopCycles)
        {
            return driver.c_SparkMax_SetSecondaryCurrentLimit(handle, limit, chopCycles);
        }
        public static ErrorCode GetSecondaryCurrentLimit(void* handle, float* limit, int* chopCycles)
        {
            return driver.c_SparkMax_GetSecondaryCurrentLimit(handle, limit, chopCycles);
        }
        public static ErrorCode SetIdleMode(void* handle, IdleMode idlemode)
        {
            return driver.c_SparkMax_SetIdleMode(handle, idlemode);
        }
        public static ErrorCode GetIdleMode(void* handle, IdleMode* idlemode)
        {
            return driver.c_SparkMax_GetIdleMode(handle, idlemode);
        }
        public static ErrorCode EnableVoltageCompensation(void* handle, float nominalVoltage)
        {
            return driver.c_SparkMax_EnableVoltageCompensation(handle, nominalVoltage);
        }
        public static ErrorCode GetVoltageCompensationNominalVoltage(void* handle, float* nominalVoltage)
        {
            return driver.c_SparkMax_GetVoltageCompensationNominalVoltage(handle, nominalVoltage);
        }
        public static ErrorCode DisableVoltageCompensation(void* handle)
        {
            return driver.c_SparkMax_DisableVoltageCompensation(handle);
        }
        public static ErrorCode SetOpenLoopRampRate(void* handle, float rate)
        {
            return driver.c_SparkMax_SetOpenLoopRampRate(handle, rate);
        }
        public static ErrorCode GetOpenLoopRampRate(void* handle, float* rate)
        {
            return driver.c_SparkMax_GetOpenLoopRampRate(handle, rate);
        }
        public static ErrorCode SetClosedLoopRampRate(void* handle, float rate)
        {
            return driver.c_SparkMax_SetClosedLoopRampRate(handle, rate);
        }
        public static ErrorCode GetClosedLoopRampRate(void* handle, float* rate)
        {
            return driver.c_SparkMax_GetClosedLoopRampRate(handle, rate);
        }
        public static ErrorCode IsFollower(void* handle, byte* isFollower)
        {
            return driver.c_SparkMax_IsFollower(handle, isFollower);
        }
        public static ErrorCode GetFaults(void* handle, ushort* faults)
        {
            return driver.c_SparkMax_GetFaults(handle, faults);
        }
        public static ErrorCode GetStickyFaults(void* handle, ushort* stickyFaults)
        {
            return driver.c_SparkMax_GetStickyFaults(handle, stickyFaults);
        }
        public static ErrorCode GetFault(void* handle, FaultID faultId, byte* fault)
        {
            return driver.c_SparkMax_GetFault(handle, faultId, fault);
        }
        public static ErrorCode GetStickyFault(void* handle, FaultID faultId, byte* stickyfault)
        {
            return driver.c_SparkMax_GetStickyFault(handle, faultId, stickyfault);
        }
        public static ErrorCode GetBusVoltage(void* handle, float* busVoltage)
        {
            return driver.c_SparkMax_GetBusVoltage(handle, busVoltage);
        }
        public static ErrorCode GetAppliedOutput(void* handle, float* appliedOutput)
        {
            return driver.c_SparkMax_GetAppliedOutput(handle, appliedOutput);
        }
        public static ErrorCode GetOutputCurrent(void* handle, float* outputCurrent)
        {
            return driver.c_SparkMax_GetOutputCurrent(handle, outputCurrent);
        }
        public static ErrorCode GetMotorTemperature(void* handle, float* motorTemperature)
        {
            return driver.c_SparkMax_GetMotorTemperature(handle, motorTemperature);
        }
        public static ErrorCode ClearFaults(void* handle)
        {
            return driver.c_SparkMax_ClearFaults(handle);
        }
        public static ErrorCode BurnFlash(void* handle)
        {
            return driver.c_SparkMax_BurnFlash(handle);
        }
        public static ErrorCode SetCANTimeout(void* handle, int timeoutMs)
        {
            return driver.c_SparkMax_SetCANTimeout(handle, timeoutMs);
        }
        public static ErrorCode EnableSoftLimit(void* handle, LimitDirection dir, byte enable)
        {
            return driver.c_SparkMax_EnableSoftLimit(handle, dir, enable);
        }
        public static ErrorCode IsSoftLimitEnabled(void* handle, LimitDirection dir, byte* enabled)
        {
            return driver.c_SparkMax_IsSoftLimitEnabled(handle, dir, enabled);
        }
        public static ErrorCode SetSoftLimit(void* handle, LimitDirection dir, float limit)
        {
            return driver.c_SparkMax_SetSoftLimit(handle, dir, limit);
        }
        public static ErrorCode GetSoftLimit(void* handle, LimitDirection dir, float* limit)
        {
            return driver.c_SparkMax_GetSoftLimit(handle, dir, limit);
        }
        public static ErrorCode SetSensorType(void* handle, EncoderType sensorType)
        {
            return driver.c_SparkMax_SetSensorType(handle, sensorType);
        }
        public static ErrorCode IDQuery(uint* uniqueIdArray, IntPtr uniqueIdArraySize, IntPtr* numberOfDevices)
        {
            return driver.c_SparkMax_IDQuery(uniqueIdArray, uniqueIdArraySize, numberOfDevices);
        }
        public static ErrorCode IDAssign(uint uniqueId, byte deviceId)
        {
            return driver.c_SparkMax_IDAssign(uniqueId, deviceId);
        }
        public static ErrorCode Identify(void* handle)
        {
            return driver.c_SparkMax_Identify(handle);
        }
        public static ErrorCode IdentifyUniqueId(uint uniqueId)
        {
            return driver.c_SparkMax_IdentifyUniqueId(uniqueId);
        }
        public static ErrorCode SetLimitPolarity(void* handle, LimitDirection sw, LimitPolarity polarity)
        {
            return driver.c_SparkMax_SetLimitPolarity(handle, sw, polarity);
        }
        public static ErrorCode GetLimitPolarity(void* handle, LimitDirection sw, LimitPolarity* polarity)
        {
            return driver.c_SparkMax_GetLimitPolarity(handle, sw, polarity);
        }
        public static ErrorCode GetLimitSwitch(void* handle, LimitDirection sw, byte* limit)
        {
            return driver.c_SparkMax_GetLimitSwitch(handle, sw, limit);
        }
        public static ErrorCode EnableLimitSwitch(void* handle, LimitDirection sw, byte enable)
        {
            return driver.c_SparkMax_EnableLimitSwitch(handle, sw, enable);
        }
        public static ErrorCode IsLimitEnabled(void* handle, LimitDirection sw, byte* enabled)
        {
            return driver.c_SparkMax_IsLimitEnabled(handle, sw, enabled);
        }
        public static ErrorCode GetAnalogPosition(void* handle, float* position)
        {
            return driver.c_SparkMax_GetAnalogPosition(handle, position);
        }
        public static ErrorCode GetAnalogVelocity(void* handle, float* velocity)
        {
            return driver.c_SparkMax_GetAnalogVelocity(handle, velocity);
        }
        public static ErrorCode GetAnalogVoltage(void* handle, float* voltage)
        {
            return driver.c_SparkMax_GetAnalogVoltage(handle, voltage);
        }
        public static ErrorCode SetAnalogPositionConversionFactor(void* handle, float conversion)
        {
            return driver.c_SparkMax_SetAnalogPositionConversionFactor(handle, conversion);
        }
        public static ErrorCode SetAnalogVelocityConversionFactor(void* handle, float conversion)
        {
            return driver.c_SparkMax_SetAnalogVelocityConversionFactor(handle, conversion);
        }
        public static ErrorCode GetAnalogPositionConversionFactor(void* handle, float* conversion)
        {
            return driver.c_SparkMax_GetAnalogPositionConversionFactor(handle, conversion);
        }
        public static ErrorCode GetAnalogVelocityConversionFactor(void* handle, float* conversion)
        {
            return driver.c_SparkMax_GetAnalogVelocityConversionFactor(handle, conversion);
        }
        public static ErrorCode SetAnalogInverted(void* handle, byte inverted)
        {
            return driver.c_SparkMax_SetAnalogInverted(handle, inverted);
        }
        public static ErrorCode GetAnalogInverted(void* handle, byte* inverted)
        {
            return driver.c_SparkMax_GetAnalogInverted(handle, inverted);
        }
        public static ErrorCode SetAnalogAverageDepth(void* handle, uint depth)
        {
            return driver.c_SparkMax_SetAnalogAverageDepth(handle, depth);
        }
        public static ErrorCode GetAnalogAverageDepth(void* handle, uint* depth)
        {
            return driver.c_SparkMax_GetAnalogAverageDepth(handle, depth);
        }
        public static ErrorCode SetAnalogMeasurementPeriod(void* handle, uint samples)
        {
            return driver.c_SparkMax_SetAnalogMeasurementPeriod(handle, samples);
        }
        public static ErrorCode GetAnalogMeasurementPeriod(void* handle, uint* samples)
        {
            return driver.c_SparkMax_GetAnalogMeasurementPeriod(handle, samples);
        }

    }
}
