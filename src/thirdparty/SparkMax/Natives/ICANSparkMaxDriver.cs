using System;
using System.Collections.Generic;
using System.Text;

namespace REV.SparkMax.Natives
{
#pragma warning disable IDE1006 // Naming Styles
    public unsafe interface ICANSparkMaxDriver
    {
        // CANSparkMaxLowLevel
        void* c_SparkMax_Create(int deviceId, MotorType type);
        void* c_SparkMax_Create_Inplace(int deviceId);
        void c_SparkMax_Destroy(void* handle);
        ErrorCode c_SparkMax_GetFirmwareVersion(void* handle, FirmwareVersion* fwVersion);
        //ErrorCode c_SparkMax_GetSerialNumber(void* handle, uint* serialNumber[3]);
        ErrorCode c_SparkMax_GetDeviceId(void* handle, int* deviceId);
        ErrorCode c_SparkMax_SetMotorType(void* handle, MotorType type);
        ErrorCode c_SparkMax_GetMotorType(void* handle, MotorType* type);
        ErrorCode c_SparkMax_SetPeriodicFramePeriod(void* handle, PeriodicFrame frameId, int periodMs);

        void c_SparkMax_SetControlFramePeriod(void* handle, int periodMs);
        int c_SparkMax_GetControlFramePeriod(void* handle);

        ErrorCode c_SparkMax_SetParameterFloat32(void* handle, ConfigParameter paramId, float value);
        ErrorCode c_SparkMax_SetParameterInt32(void* handle, ConfigParameter paramId, int value);
        ErrorCode c_SparkMax_SetParameterUint32(void* handle, ConfigParameter paramId, uint value);
        ErrorCode c_SparkMax_SetParameterBool(void* handle, ConfigParameter paramId, byte value);
        ErrorCode c_SparkMax_GetParameterFloat32(void* handle, ConfigParameter paramId, float* value);
        ErrorCode c_SparkMax_GetParameterInt32(void* handle, ConfigParameter paramId, int* value);
        ErrorCode c_SparkMax_GetParameterUint32(void* handle, ConfigParameter paramId, uint* value);
        ErrorCode c_SparkMax_GetParameterBool(void* handle, ConfigParameter paramId, byte* value);
        //c_SparkMax_ParameterType c_SparkMax_GetParameterType(c_SparkMax_ConfigParameter paramId);
        ErrorCode c_SparkMax_GetPeriodicStatus0(void* handle, PeriodicStatus0* rawframe);
        ErrorCode c_SparkMax_GetPeriodicStatus1(void* handle, PeriodicStatus1* rawframe);
        ErrorCode c_SparkMax_GetPeriodicStatus2(void* handle, PeriodicStatus2* rawframe);
        ErrorCode c_SparkMax_GetPeriodicStatus3(void* handle, PeriodicStatus3* rawframe);
        ErrorCode c_SparkMax_GetPeriodicStatus4(void* handle, PeriodicStatus4* rawframe);

        ErrorCode c_SparkMax_SetEncoderPosition(void* handle, float position);
        ErrorCode c_SparkMax_SetAltEncoderPosition(void* handle, float position);
        ErrorCode c_SparkMax_RestoreFactoryDefaults(void* handle, byte persist);
        ErrorCode c_SparkMax_FactoryWipe(void* handle, byte persist);

        ErrorCode c_SparkMax_SetFollow(void* handle, uint followerArbId, uint followerCfg);
        //ErrorCode c_SparkMax_FollowerInvert(void* handle, byte invert);
        float c_SparkMax_SafeFloat(float f);

        ErrorCode c_SparkMax_SetpointCommand(void* handle, float value, ControlType ctrl,
                                            int pidSlot, float arbFeedforward, int arbFFUnits);
        ErrorCode c_SparkMax_GetDRVStatus(void* handle, DRVStatus* drvStatus);

        // CANSparkMax
        ErrorCode c_SparkMax_SetInverted(void* handle, byte inverted);
        ErrorCode c_SparkMax_GetInverted(void* handle, byte* inverted);
        ErrorCode c_SparkMax_SetSmartCurrentLimit(void* handle, byte stallLimit, byte freeLimit, uint limitRPM);
        ErrorCode c_SparkMax_GetSmartCurrentLimit(void* handle, byte* stallLimit, byte* freeLimit, uint* limitRPM);
        ErrorCode c_SparkMax_SetSecondaryCurrentLimit(void* handle, float limit, int chopCycles);
        ErrorCode c_SparkMax_GetSecondaryCurrentLimit(void* handle, float* limit, int* chopCycles);
        ErrorCode c_SparkMax_SetIdleMode(void* handle, IdleMode idlemode);
        ErrorCode c_SparkMax_GetIdleMode(void* handle, IdleMode* idlemode);
        ErrorCode c_SparkMax_EnableVoltageCompensation(void* handle, float nominalVoltage);
        ErrorCode c_SparkMax_GetVoltageCompensationNominalVoltage(void* handle, float* nominalVoltage);
        ErrorCode c_SparkMax_DisableVoltageCompensation(void* handle);
        ErrorCode c_SparkMax_SetOpenLoopRampRate(void* handle, float rate);
        ErrorCode c_SparkMax_GetOpenLoopRampRate(void* handle, float* rate);
        ErrorCode c_SparkMax_SetClosedLoopRampRate(void* handle, float rate);
        ErrorCode c_SparkMax_GetClosedLoopRampRate(void* handle, float* rate);
        ErrorCode c_SparkMax_IsFollower(void* handle, byte* isFollower);
        ErrorCode c_SparkMax_GetFaults(void* handle, ushort* faults);
        ErrorCode c_SparkMax_GetStickyFaults(void* handle, ushort* stickyFaults);
        ErrorCode c_SparkMax_GetFault(void* handle, FaultID faultId, byte* fault);
        ErrorCode c_SparkMax_GetStickyFault(void* handle, FaultID faultId, byte* stickyfault);
        ErrorCode c_SparkMax_GetBusVoltage(void* handle, float* busVoltage);
        ErrorCode c_SparkMax_GetAppliedOutput(void* handle, float* appliedOutput);
        ErrorCode c_SparkMax_GetOutputCurrent(void* handle, float* outputCurrent);
        ErrorCode c_SparkMax_GetMotorTemperature(void* handle, float* motorTemperature);
        ErrorCode c_SparkMax_ClearFaults(void* handle);
        ErrorCode c_SparkMax_BurnFlash(void* handle);
        ErrorCode c_SparkMax_SetCANTimeout(void* handle, int timeoutMs);
        ErrorCode c_SparkMax_EnableSoftLimit(void* handle, LimitDirection dir, byte enable);
        ErrorCode c_SparkMax_IsSoftLimitEnabled(void* handle, LimitDirection dir, byte* enabled);
        ErrorCode c_SparkMax_SetSoftLimit(void* handle, LimitDirection dir, float limit);
        ErrorCode c_SparkMax_GetSoftLimit(void* handle, LimitDirection dir, float* limit);
        ErrorCode c_SparkMax_SetSensorType(void* handle, EncoderType sensorType);
        ErrorCode c_SparkMax_IDQuery(uint* uniqueIdArray, IntPtr uniqueIdArraySize, IntPtr* numberOfDevices);
        ErrorCode c_SparkMax_IDAssign(uint uniqueId, byte deviceId);
        ErrorCode c_SparkMax_Identify(void* handle);
        ErrorCode c_SparkMax_IdentifyUniqueId(uint uniqueId);

        // Digital Input
        ErrorCode c_SparkMax_SetLimitPolarity(void* handle, LimitDirection sw, LimitPolarity polarity);
        ErrorCode c_SparkMax_GetLimitPolarity(void* handle, LimitDirection sw, LimitPolarity* polarity);
        ErrorCode c_SparkMax_GetLimitSwitch(void* handle, LimitDirection sw, byte* limit);
        ErrorCode c_SparkMax_EnableLimitSwitch(void* handle, LimitDirection sw, byte enable);
        ErrorCode c_SparkMax_IsLimitEnabled(void* handle, LimitDirection sw, byte* enabled);

        // CANAnalog 
        ErrorCode c_SparkMax_GetAnalogPosition(void* handle, float* position);
        ErrorCode c_SparkMax_GetAnalogVelocity(void* handle, float* velocity);
        ErrorCode c_SparkMax_GetAnalogVoltage(void* handle, float* voltage);
        ErrorCode c_SparkMax_SetAnalogPositionConversionFactor(void* handle, float conversion);
        ErrorCode c_SparkMax_SetAnalogVelocityConversionFactor(void* handle, float conversion);
        ErrorCode c_SparkMax_GetAnalogPositionConversionFactor(void* handle, float* conversion);
        ErrorCode c_SparkMax_GetAnalogVelocityConversionFactor(void* handle, float* conversion);
        ErrorCode c_SparkMax_SetAnalogInverted(void* handle, byte inverted);
        ErrorCode c_SparkMax_GetAnalogInverted(void* handle, byte* inverted);
        ErrorCode c_SparkMax_SetAnalogAverageDepth(void* handle, uint depth);
        ErrorCode c_SparkMax_GetAnalogAverageDepth(void* handle, uint* depth);
        ErrorCode c_SparkMax_SetAnalogMeasurementPeriod(void* handle, uint samples);
        ErrorCode c_SparkMax_GetAnalogMeasurementPeriod(void* handle, uint* samples);
        ErrorCode c_SparkMax_SetAnalogMode(void* handle, AnalogMode mode);
        ErrorCode c_SparkMax_GetAnalogMode(void* handle, AnalogMode* mode);

        // CANEncoder
        ErrorCode c_SparkMax_GetEncoderPosition(void* handle, float* position);
        ErrorCode c_SparkMax_GetEncoderVelocity(void* handle, float* velocity);
        ErrorCode c_SparkMax_SetPositionConversionFactor(void* handle, float conversion);
        ErrorCode c_SparkMax_SetVelocityConversionFactor(void* handle, float conversion);
        ErrorCode c_SparkMax_GetPositionConversionFactor(void* handle, float* conversion);
        ErrorCode c_SparkMax_GetVelocityConversionFactor(void* handle, float* conversion);
        ErrorCode c_SparkMax_SetAverageDepth(void* handle, uint depth);
        ErrorCode c_SparkMax_GetAverageDepth(void* handle, uint* depth);
        ErrorCode c_SparkMax_SetMeasurementPeriod(void* handle, uint samples);
        ErrorCode c_SparkMax_GetMeasurementPeriod(void* handle, uint* samples);
        ErrorCode c_SparkMax_SetCountsPerRevolution(void* handle, uint cpr);
        ErrorCode c_SparkMax_GetCountsPerRevolution(void* handle, uint* cpr);
        ErrorCode c_SparkMax_SetEncoderInverted(void* handle, byte inverted);
        ErrorCode c_SparkMax_GetEncoderInverted(void* handle, byte* inverted);

        // Alt Encoder
        ErrorCode c_SparkMax_GetAltEncoderPosition(void* handle, float* position);
        ErrorCode c_SparkMax_GetAltEncoderVelocity(void* handle, float* velocity);
        ErrorCode c_SparkMax_SetAltEncoderPositionFactor(void* handle, float conversion);
        ErrorCode c_SparkMax_SetAltEncoderVelocityFactor(void* handle, float conversion);
        ErrorCode c_SparkMax_GetAltEncoderPositionFactor(void* handle, float* conversion);
        ErrorCode c_SparkMax_GetAltEncoderVelocityFactor(void* handle, float* conversion);
        ErrorCode c_SparkMax_SetAltEncoderAverageDepth(void* handle, uint depth);
        ErrorCode c_SparkMax_GetAltEncoderAverageDepth(void* handle, uint* depth);
        ErrorCode c_SparkMax_SetAltEncoderMeasurementPeriod(void* handle, uint samples);
        ErrorCode c_SparkMax_GetAltEncoderMeasurementPeriod(void* handle, uint* samples);
        ErrorCode c_SparkMax_SetAltEncoderCountsPerRevolution(void* handle, uint cpr);
        ErrorCode c_SparkMax_GetAltEncoderCountsPerRevolution(void* handle, uint* cpr);
        ErrorCode c_SparkMax_SetAltEncoderInverted(void* handle, byte inverted);
        ErrorCode c_SparkMax_GetAltEncoderInverted(void* handle, byte* inverted);

        ErrorCode c_SparkMax_SetDataPortConfig(void* handle, DataPortConfig config);
        ErrorCode c_SparkMax_GetDataPortConfig(void* handle, DataPortConfig* config);

        // CANPIDController
        ErrorCode c_SparkMax_SetP(void* handle, int slotID, float gain);
        ErrorCode c_SparkMax_SetI(void* handle, int slotID, float gain);
        ErrorCode c_SparkMax_SetD(void* handle, int slotID, float gain);
        ErrorCode c_SparkMax_SetDFilter(void* handle, int slotID, float gain);
        ErrorCode c_SparkMax_SetFF(void* handle, int slotID, float gain);
        ErrorCode c_SparkMax_SetIZone(void* handle, int slotID, float IZone);
        ErrorCode c_SparkMax_SetOutputRange(void* handle, int slotID, float min, float max);
        ErrorCode c_SparkMax_GetP(void* handle, int slotID, float* gain);
        ErrorCode c_SparkMax_GetI(void* handle, int slotID, float* gain);
        ErrorCode c_SparkMax_GetD(void* handle, int slotID, float* gain);
        ErrorCode c_SparkMax_GetDFilter(void* handle, int slotID, float* gain);
        ErrorCode c_SparkMax_GetFF(void* handle, int slotID, float* gain);
        ErrorCode c_SparkMax_GetIZone(void* handle, int slotID, float* IZone);
        ErrorCode c_SparkMax_GetOutputMin(void* handle, int slotID, float* min);
        ErrorCode c_SparkMax_GetOutputMax(void* handle, int slotID, float* max);


        ErrorCode c_SparkMax_SetSmartMotionMaxVelocity(void* handle, int slotID, float maxVel);
        ErrorCode c_SparkMax_SetSmartMotionMaxAccel(void* handle, int slotID, float maxAccel);
        ErrorCode c_SparkMax_SetSmartMotionMinOutputVelocity(void* handle, int slotID, float minVel);
        ErrorCode c_SparkMax_SetSmartMotionAccelStrategy(void* handle, int slotID, AccelStrategy accelStrategy);
        ErrorCode c_SparkMax_SetSmartMotionAllowedClosedLoopError(void* handle, int slotID, float allowedError);
        ErrorCode c_SparkMax_GetSmartMotionMaxVelocity(void* handle, int slotID, float* maxVel);
        ErrorCode c_SparkMax_GetSmartMotionMaxAccel(void* handle, int slotID, float* maxAccel);
        ErrorCode c_SparkMax_GetSmartMotionMinOutputVelocity(void* handle, int slotID, float* minVel);
        ErrorCode c_SparkMax_GetSmartMotionAccelStrategy(void* handle, int slotID, AccelStrategy* accelStrategy);
        ErrorCode c_SparkMax_GetSmartMotionAllowedClosedLoopError(void* handle, int slotID, float* allowedError);

        ErrorCode c_SparkMax_SetIMaxAccum(void* handle, int slotID, float iMaxAccum);
        ErrorCode c_SparkMax_GetIMaxAccum(void* handle, int slotID, float* iMaxAccum);
        ErrorCode c_SparkMax_SetIAccum(void* handle, float iAccum);
        ErrorCode c_SparkMax_GetIAccum(void* handle, float* iAccum);

        ErrorCode c_SparkMax_SetFeedbackDevice(void* handle, uint sensorID);
        ErrorCode c_SparkMax_GetFeedbackDeviceID(void* handle, uint* id);
        ErrorCode c_SparkMax_SetFeedbackDeviceRange(void* handle, float min, float max);

        // Other helpers
        APIVersion c_SparkMax_GetAPIVersion();
        void c_SparkMax_SetLastError(void* handle, ErrorCode error);
        ErrorCode c_SparkMax_GetLastError(void* handle);
        ErrorCode c_SparkMax_GenerateError(int deviceID, ErrorCode error);
    }

#pragma warning restore IDE1006 // Naming Styles
}
