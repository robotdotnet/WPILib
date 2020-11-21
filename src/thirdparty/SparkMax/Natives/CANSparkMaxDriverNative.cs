using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace REV.SparkMax.Natives
{
    public unsafe class CANSparkMaxDriverNative
    {
        private delegate* unmanaged[Cdecl]<void*, AnalogMode, ErrorCode> c_SparkMax_SetAnalogModeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAnalogMode(void* handle, AnalogMode mode)
        {
            return c_SparkMax_SetAnalogModeFunc(handle, mode);
        }


        private delegate* unmanaged[Cdecl]<void*, AnalogMode*, ErrorCode> c_SparkMax_GetAnalogModeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAnalogMode(void* handle, AnalogMode* mode)
        {
            return c_SparkMax_GetAnalogModeFunc(handle, mode);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetEncoderPositionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetEncoderPosition(void* handle, float* position)
        {
            return c_SparkMax_GetEncoderPositionFunc(handle, position);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetEncoderVelocityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetEncoderVelocity(void* handle, float* velocity)
        {
            return c_SparkMax_GetEncoderVelocityFunc(handle, velocity);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ErrorCode> c_SparkMax_SetPositionConversionFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetPositionConversionFactor(void* handle, float conversion)
        {
            return c_SparkMax_SetPositionConversionFactorFunc(handle, conversion);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ErrorCode> c_SparkMax_SetVelocityConversionFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetVelocityConversionFactor(void* handle, float conversion)
        {
            return c_SparkMax_SetVelocityConversionFactorFunc(handle, conversion);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetPositionConversionFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetPositionConversionFactor(void* handle, float* conversion)
        {
            return c_SparkMax_GetPositionConversionFactorFunc(handle, conversion);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetVelocityConversionFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetVelocityConversionFactor(void* handle, float* conversion)
        {
            return c_SparkMax_GetVelocityConversionFactorFunc(handle, conversion);
        }


        private delegate* unmanaged[Cdecl]<void*, uint, ErrorCode> c_SparkMax_SetAverageDepthFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAverageDepth(void* handle, uint depth)
        {
            return c_SparkMax_SetAverageDepthFunc(handle, depth);
        }


        private delegate* unmanaged[Cdecl]<void*, uint*, ErrorCode> c_SparkMax_GetAverageDepthFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAverageDepth(void* handle, uint* depth)
        {
            return c_SparkMax_GetAverageDepthFunc(handle, depth);
        }


        private delegate* unmanaged[Cdecl]<void*, uint, ErrorCode> c_SparkMax_SetMeasurementPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetMeasurementPeriod(void* handle, uint samples)
        {
            return c_SparkMax_SetMeasurementPeriodFunc(handle, samples);
        }


        private delegate* unmanaged[Cdecl]<void*, uint*, ErrorCode> c_SparkMax_GetMeasurementPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetMeasurementPeriod(void* handle, uint* samples)
        {
            return c_SparkMax_GetMeasurementPeriodFunc(handle, samples);
        }


        private delegate* unmanaged[Cdecl]<void*, uint, ErrorCode> c_SparkMax_SetCountsPerRevolutionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetCountsPerRevolution(void* handle, uint cpr)
        {
            return c_SparkMax_SetCountsPerRevolutionFunc(handle, cpr);
        }


        private delegate* unmanaged[Cdecl]<void*, uint*, ErrorCode> c_SparkMax_GetCountsPerRevolutionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetCountsPerRevolution(void* handle, uint* cpr)
        {
            return c_SparkMax_GetCountsPerRevolutionFunc(handle, cpr);
        }


        private delegate* unmanaged[Cdecl]<void*, byte, ErrorCode> c_SparkMax_SetEncoderInvertedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetEncoderInverted(void* handle, byte inverted)
        {
            return c_SparkMax_SetEncoderInvertedFunc(handle, inverted);
        }


        private delegate* unmanaged[Cdecl]<void*, byte*, ErrorCode> c_SparkMax_GetEncoderInvertedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetEncoderInverted(void* handle, byte* inverted)
        {
            return c_SparkMax_GetEncoderInvertedFunc(handle, inverted);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetAltEncoderPositionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAltEncoderPosition(void* handle, float* position)
        {
            return c_SparkMax_GetAltEncoderPositionFunc(handle, position);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetAltEncoderVelocityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAltEncoderVelocity(void* handle, float* velocity)
        {
            return c_SparkMax_GetAltEncoderVelocityFunc(handle, velocity);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ErrorCode> c_SparkMax_SetAltEncoderPositionFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAltEncoderPositionFactor(void* handle, float conversion)
        {
            return c_SparkMax_SetAltEncoderPositionFactorFunc(handle, conversion);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ErrorCode> c_SparkMax_SetAltEncoderVelocityFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAltEncoderVelocityFactor(void* handle, float conversion)
        {
            return c_SparkMax_SetAltEncoderVelocityFactorFunc(handle, conversion);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetAltEncoderPositionFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAltEncoderPositionFactor(void* handle, float* conversion)
        {
            return c_SparkMax_GetAltEncoderPositionFactorFunc(handle, conversion);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetAltEncoderVelocityFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAltEncoderVelocityFactor(void* handle, float* conversion)
        {
            return c_SparkMax_GetAltEncoderVelocityFactorFunc(handle, conversion);
        }


        private delegate* unmanaged[Cdecl]<void*, uint, ErrorCode> c_SparkMax_SetAltEncoderAverageDepthFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAltEncoderAverageDepth(void* handle, uint depth)
        {
            return c_SparkMax_SetAltEncoderAverageDepthFunc(handle, depth);
        }


        private delegate* unmanaged[Cdecl]<void*, uint*, ErrorCode> c_SparkMax_GetAltEncoderAverageDepthFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAltEncoderAverageDepth(void* handle, uint* depth)
        {
            return c_SparkMax_GetAltEncoderAverageDepthFunc(handle, depth);
        }


        private delegate* unmanaged[Cdecl]<void*, uint, ErrorCode> c_SparkMax_SetAltEncoderMeasurementPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAltEncoderMeasurementPeriod(void* handle, uint samples)
        {
            return c_SparkMax_SetAltEncoderMeasurementPeriodFunc(handle, samples);
        }


        private delegate* unmanaged[Cdecl]<void*, uint*, ErrorCode> c_SparkMax_GetAltEncoderMeasurementPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAltEncoderMeasurementPeriod(void* handle, uint* samples)
        {
            return c_SparkMax_GetAltEncoderMeasurementPeriodFunc(handle, samples);
        }


        private delegate* unmanaged[Cdecl]<void*, uint, ErrorCode> c_SparkMax_SetAltEncoderCountsPerRevolutionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAltEncoderCountsPerRevolution(void* handle, uint cpr)
        {
            return c_SparkMax_SetAltEncoderCountsPerRevolutionFunc(handle, cpr);
        }


        private delegate* unmanaged[Cdecl]<void*, uint*, ErrorCode> c_SparkMax_GetAltEncoderCountsPerRevolutionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAltEncoderCountsPerRevolution(void* handle, uint* cpr)
        {
            return c_SparkMax_GetAltEncoderCountsPerRevolutionFunc(handle, cpr);
        }


        private delegate* unmanaged[Cdecl]<void*, byte, ErrorCode> c_SparkMax_SetAltEncoderInvertedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAltEncoderInverted(void* handle, byte inverted)
        {
            return c_SparkMax_SetAltEncoderInvertedFunc(handle, inverted);
        }


        private delegate* unmanaged[Cdecl]<void*, byte*, ErrorCode> c_SparkMax_GetAltEncoderInvertedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAltEncoderInverted(void* handle, byte* inverted)
        {
            return c_SparkMax_GetAltEncoderInvertedFunc(handle, inverted);
        }


        private delegate* unmanaged[Cdecl]<void*, DataPortConfig, ErrorCode> c_SparkMax_SetDataPortConfigFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetDataPortConfig(void* handle, DataPortConfig config)
        {
            return c_SparkMax_SetDataPortConfigFunc(handle, config);
        }


        private delegate* unmanaged[Cdecl]<void*, DataPortConfig*, ErrorCode> c_SparkMax_GetDataPortConfigFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetDataPortConfig(void* handle, DataPortConfig* config)
        {
            return c_SparkMax_GetDataPortConfigFunc(handle, config);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float, ErrorCode> c_SparkMax_SetPFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetP(void* handle, int slotID, float gain)
        {
            return c_SparkMax_SetPFunc(handle, slotID, gain);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float, ErrorCode> c_SparkMax_SetIFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetI(void* handle, int slotID, float gain)
        {
            return c_SparkMax_SetIFunc(handle, slotID, gain);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float, ErrorCode> c_SparkMax_SetDFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetD(void* handle, int slotID, float gain)
        {
            return c_SparkMax_SetDFunc(handle, slotID, gain);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float, ErrorCode> c_SparkMax_SetDFilterFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetDFilter(void* handle, int slotID, float gain)
        {
            return c_SparkMax_SetDFilterFunc(handle, slotID, gain);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float, ErrorCode> c_SparkMax_SetFFFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetFF(void* handle, int slotID, float gain)
        {
            return c_SparkMax_SetFFFunc(handle, slotID, gain);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float, ErrorCode> c_SparkMax_SetIZoneFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetIZone(void* handle, int slotID, float IZone)
        {
            return c_SparkMax_SetIZoneFunc(handle, slotID, IZone);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float, float, ErrorCode> c_SparkMax_SetOutputRangeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetOutputRange(void* handle, int slotID, float min, float max)
        {
            return c_SparkMax_SetOutputRangeFunc(handle, slotID, min, max);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetPFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetP(void* handle, int slotID, float* gain)
        {
            return c_SparkMax_GetPFunc(handle, slotID, gain);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetIFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetI(void* handle, int slotID, float* gain)
        {
            return c_SparkMax_GetIFunc(handle, slotID, gain);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetDFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetD(void* handle, int slotID, float* gain)
        {
            return c_SparkMax_GetDFunc(handle, slotID, gain);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetDFilterFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetDFilter(void* handle, int slotID, float* gain)
        {
            return c_SparkMax_GetDFilterFunc(handle, slotID, gain);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetFFFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetFF(void* handle, int slotID, float* gain)
        {
            return c_SparkMax_GetFFFunc(handle, slotID, gain);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetIZoneFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetIZone(void* handle, int slotID, float* IZone)
        {
            return c_SparkMax_GetIZoneFunc(handle, slotID, IZone);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetOutputMinFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetOutputMin(void* handle, int slotID, float* min)
        {
            return c_SparkMax_GetOutputMinFunc(handle, slotID, min);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetOutputMaxFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetOutputMax(void* handle, int slotID, float* max)
        {
            return c_SparkMax_GetOutputMaxFunc(handle, slotID, max);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float, ErrorCode> c_SparkMax_SetSmartMotionMaxVelocityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetSmartMotionMaxVelocity(void* handle, int slotID, float maxVel)
        {
            return c_SparkMax_SetSmartMotionMaxVelocityFunc(handle, slotID, maxVel);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float, ErrorCode> c_SparkMax_SetSmartMotionMaxAccelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetSmartMotionMaxAccel(void* handle, int slotID, float maxAccel)
        {
            return c_SparkMax_SetSmartMotionMaxAccelFunc(handle, slotID, maxAccel);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float, ErrorCode> c_SparkMax_SetSmartMotionMinOutputVelocityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetSmartMotionMinOutputVelocity(void* handle, int slotID, float minVel)
        {
            return c_SparkMax_SetSmartMotionMinOutputVelocityFunc(handle, slotID, minVel);
        }


        private delegate* unmanaged[Cdecl]<void*, int, AccelStrategy, ErrorCode> c_SparkMax_SetSmartMotionAccelStrategyFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetSmartMotionAccelStrategy(void* handle, int slotID, AccelStrategy accelStrategy)
        {
            return c_SparkMax_SetSmartMotionAccelStrategyFunc(handle, slotID, accelStrategy);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float, ErrorCode> c_SparkMax_SetSmartMotionAllowedClosedLoopErrorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetSmartMotionAllowedClosedLoopError(void* handle, int slotID, float allowedError)
        {
            return c_SparkMax_SetSmartMotionAllowedClosedLoopErrorFunc(handle, slotID, allowedError);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetSmartMotionMaxVelocityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetSmartMotionMaxVelocity(void* handle, int slotID, float* maxVel)
        {
            return c_SparkMax_GetSmartMotionMaxVelocityFunc(handle, slotID, maxVel);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetSmartMotionMaxAccelFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetSmartMotionMaxAccel(void* handle, int slotID, float* maxAccel)
        {
            return c_SparkMax_GetSmartMotionMaxAccelFunc(handle, slotID, maxAccel);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetSmartMotionMinOutputVelocityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetSmartMotionMinOutputVelocity(void* handle, int slotID, float* minVel)
        {
            return c_SparkMax_GetSmartMotionMinOutputVelocityFunc(handle, slotID, minVel);
        }


        private delegate* unmanaged[Cdecl]<void*, int, AccelStrategy*, ErrorCode> c_SparkMax_GetSmartMotionAccelStrategyFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetSmartMotionAccelStrategy(void* handle, int slotID, AccelStrategy* accelStrategy)
        {
            return c_SparkMax_GetSmartMotionAccelStrategyFunc(handle, slotID, accelStrategy);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetSmartMotionAllowedClosedLoopErrorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetSmartMotionAllowedClosedLoopError(void* handle, int slotID, float* allowedError)
        {
            return c_SparkMax_GetSmartMotionAllowedClosedLoopErrorFunc(handle, slotID, allowedError);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float, ErrorCode> c_SparkMax_SetIMaxAccumFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetIMaxAccum(void* handle, int slotID, float iMaxAccum)
        {
            return c_SparkMax_SetIMaxAccumFunc(handle, slotID, iMaxAccum);
        }


        private delegate* unmanaged[Cdecl]<void*, int, float*, ErrorCode> c_SparkMax_GetIMaxAccumFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetIMaxAccum(void* handle, int slotID, float* iMaxAccum)
        {
            return c_SparkMax_GetIMaxAccumFunc(handle, slotID, iMaxAccum);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ErrorCode> c_SparkMax_SetIAccumFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetIAccum(void* handle, float iAccum)
        {
            return c_SparkMax_SetIAccumFunc(handle, iAccum);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetIAccumFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetIAccum(void* handle, float* iAccum)
        {
            return c_SparkMax_GetIAccumFunc(handle, iAccum);
        }


        private delegate* unmanaged[Cdecl]<void*, uint, ErrorCode> c_SparkMax_SetFeedbackDeviceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetFeedbackDevice(void* handle, uint sensorID)
        {
            return c_SparkMax_SetFeedbackDeviceFunc(handle, sensorID);
        }


        private delegate* unmanaged[Cdecl]<void*, uint*, ErrorCode> c_SparkMax_GetFeedbackDeviceIDFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetFeedbackDeviceID(void* handle, uint* id)
        {
            return c_SparkMax_GetFeedbackDeviceIDFunc(handle, id);
        }


        private delegate* unmanaged[Cdecl]<void*, float, float, ErrorCode> c_SparkMax_SetFeedbackDeviceRangeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetFeedbackDeviceRange(void* handle, float min, float max)
        {
            return c_SparkMax_SetFeedbackDeviceRangeFunc(handle, min, max);
        }


        private delegate* unmanaged[Cdecl]<APIVersion> c_SparkMax_GetAPIVersionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public APIVersion c_SparkMax_GetAPIVersion()
        {
            return c_SparkMax_GetAPIVersionFunc();
        }


        private delegate* unmanaged[Cdecl]<void*, ErrorCode, void> c_SparkMax_SetLastErrorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void c_SparkMax_SetLastError(void* handle, ErrorCode error)
        {
            c_SparkMax_SetLastErrorFunc(handle, error);
        }


        private delegate* unmanaged[Cdecl]<void*, ErrorCode> c_SparkMax_GetLastErrorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetLastError(void* handle)
        {
            return c_SparkMax_GetLastErrorFunc(handle);
        }


        private delegate* unmanaged[Cdecl]<int, ErrorCode, ErrorCode> c_SparkMax_GenerateErrorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GenerateError(int deviceID, ErrorCode error)
        {
            return c_SparkMax_GenerateErrorFunc(deviceID, error);
        }


        private delegate* unmanaged[Cdecl]<int, MotorType, void*> c_SparkMax_CreateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void* c_SparkMax_Create(int deviceId, MotorType type)
        {
            return c_SparkMax_CreateFunc(deviceId, type);
        }


        private delegate* unmanaged[Cdecl]<int, void*> c_SparkMax_Create_InplaceFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void* c_SparkMax_Create_Inplace(int deviceId)
        {
            return c_SparkMax_Create_InplaceFunc(deviceId);
        }


        private delegate* unmanaged[Cdecl]<void*, void> c_SparkMax_DestroyFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void c_SparkMax_Destroy(void* handle)
        {
            c_SparkMax_DestroyFunc(handle);
        }


        private delegate* unmanaged[Cdecl]<void*, FirmwareVersion*, ErrorCode> c_SparkMax_GetFirmwareVersionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetFirmwareVersion(void* handle, FirmwareVersion* fwVersion)
        {
            return c_SparkMax_GetFirmwareVersionFunc(handle, fwVersion);
        }


        private delegate* unmanaged[Cdecl]<void*, int*, ErrorCode> c_SparkMax_GetDeviceIdFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetDeviceId(void* handle, int* deviceId)
        {
            return c_SparkMax_GetDeviceIdFunc(handle, deviceId);
        }


        private delegate* unmanaged[Cdecl]<void*, MotorType, ErrorCode> c_SparkMax_SetMotorTypeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetMotorType(void* handle, MotorType type)
        {
            return c_SparkMax_SetMotorTypeFunc(handle, type);
        }


        private delegate* unmanaged[Cdecl]<void*, MotorType*, ErrorCode> c_SparkMax_GetMotorTypeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetMotorType(void* handle, MotorType* type)
        {
            return c_SparkMax_GetMotorTypeFunc(handle, type);
        }


        private delegate* unmanaged[Cdecl]<void*, PeriodicFrame, int, ErrorCode> c_SparkMax_SetPeriodicFramePeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetPeriodicFramePeriod(void* handle, PeriodicFrame frameId, int periodMs)
        {
            return c_SparkMax_SetPeriodicFramePeriodFunc(handle, frameId, periodMs);
        }


        private delegate* unmanaged[Cdecl]<void*, int, void> c_SparkMax_SetControlFramePeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void c_SparkMax_SetControlFramePeriod(void* handle, int periodMs)
        {
            c_SparkMax_SetControlFramePeriodFunc(handle, periodMs);
        }


        private delegate* unmanaged[Cdecl]<void*, int> c_SparkMax_GetControlFramePeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int c_SparkMax_GetControlFramePeriod(void* handle)
        {
            return c_SparkMax_GetControlFramePeriodFunc(handle);
        }


        private delegate* unmanaged[Cdecl]<void*, ConfigParameter, float, ErrorCode> c_SparkMax_SetParameterFloat32Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetParameterFloat32(void* handle, ConfigParameter paramId, float value)
        {
            return c_SparkMax_SetParameterFloat32Func(handle, paramId, value);
        }


        private delegate* unmanaged[Cdecl]<void*, ConfigParameter, int, ErrorCode> c_SparkMax_SetParameterInt32Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetParameterInt32(void* handle, ConfigParameter paramId, int value)
        {
            return c_SparkMax_SetParameterInt32Func(handle, paramId, value);
        }


        private delegate* unmanaged[Cdecl]<void*, ConfigParameter, uint, ErrorCode> c_SparkMax_SetParameterUint32Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetParameterUint32(void* handle, ConfigParameter paramId, uint value)
        {
            return c_SparkMax_SetParameterUint32Func(handle, paramId, value);
        }


        private delegate* unmanaged[Cdecl]<void*, ConfigParameter, byte, ErrorCode> c_SparkMax_SetParameterBoolFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetParameterBool(void* handle, ConfigParameter paramId, byte value)
        {
            return c_SparkMax_SetParameterBoolFunc(handle, paramId, value);
        }


        private delegate* unmanaged[Cdecl]<void*, ConfigParameter, float*, ErrorCode> c_SparkMax_GetParameterFloat32Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetParameterFloat32(void* handle, ConfigParameter paramId, float* value)
        {
            return c_SparkMax_GetParameterFloat32Func(handle, paramId, value);
        }


        private delegate* unmanaged[Cdecl]<void*, ConfigParameter, int*, ErrorCode> c_SparkMax_GetParameterInt32Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetParameterInt32(void* handle, ConfigParameter paramId, int* value)
        {
            return c_SparkMax_GetParameterInt32Func(handle, paramId, value);
        }


        private delegate* unmanaged[Cdecl]<void*, ConfigParameter, uint*, ErrorCode> c_SparkMax_GetParameterUint32Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetParameterUint32(void* handle, ConfigParameter paramId, uint* value)
        {
            return c_SparkMax_GetParameterUint32Func(handle, paramId, value);
        }


        private delegate* unmanaged[Cdecl]<void*, ConfigParameter, byte*, ErrorCode> c_SparkMax_GetParameterBoolFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetParameterBool(void* handle, ConfigParameter paramId, byte* value)
        {
            return c_SparkMax_GetParameterBoolFunc(handle, paramId, value);
        }


        private delegate* unmanaged[Cdecl]<void*, PeriodicStatus0*, ErrorCode> c_SparkMax_GetPeriodicStatus0Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetPeriodicStatus0(void* handle, PeriodicStatus0* rawframe)
        {
            return c_SparkMax_GetPeriodicStatus0Func(handle, rawframe);
        }


        private delegate* unmanaged[Cdecl]<void*, PeriodicStatus1*, ErrorCode> c_SparkMax_GetPeriodicStatus1Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetPeriodicStatus1(void* handle, PeriodicStatus1* rawframe)
        {
            return c_SparkMax_GetPeriodicStatus1Func(handle, rawframe);
        }


        private delegate* unmanaged[Cdecl]<void*, PeriodicStatus2*, ErrorCode> c_SparkMax_GetPeriodicStatus2Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetPeriodicStatus2(void* handle, PeriodicStatus2* rawframe)
        {
            return c_SparkMax_GetPeriodicStatus2Func(handle, rawframe);
        }


        private delegate* unmanaged[Cdecl]<void*, PeriodicStatus3*, ErrorCode> c_SparkMax_GetPeriodicStatus3Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetPeriodicStatus3(void* handle, PeriodicStatus3* rawframe)
        {
            return c_SparkMax_GetPeriodicStatus3Func(handle, rawframe);
        }


        private delegate* unmanaged[Cdecl]<void*, PeriodicStatus4*, ErrorCode> c_SparkMax_GetPeriodicStatus4Func;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetPeriodicStatus4(void* handle, PeriodicStatus4* rawframe)
        {
            return c_SparkMax_GetPeriodicStatus4Func(handle, rawframe);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ErrorCode> c_SparkMax_SetEncoderPositionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetEncoderPosition(void* handle, float position)
        {
            return c_SparkMax_SetEncoderPositionFunc(handle, position);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ErrorCode> c_SparkMax_SetAltEncoderPositionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAltEncoderPosition(void* handle, float position)
        {
            return c_SparkMax_SetAltEncoderPositionFunc(handle, position);
        }


        private delegate* unmanaged[Cdecl]<void*, byte, ErrorCode> c_SparkMax_RestoreFactoryDefaultsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_RestoreFactoryDefaults(void* handle, byte persist)
        {
            return c_SparkMax_RestoreFactoryDefaultsFunc(handle, persist);
        }


        private delegate* unmanaged[Cdecl]<void*, byte, ErrorCode> c_SparkMax_FactoryWipeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_FactoryWipe(void* handle, byte persist)
        {
            return c_SparkMax_FactoryWipeFunc(handle, persist);
        }


        private delegate* unmanaged[Cdecl]<void*, uint, uint, ErrorCode> c_SparkMax_SetFollowFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetFollow(void* handle, uint followerArbId, uint followerCfg)
        {
            return c_SparkMax_SetFollowFunc(handle, followerArbId, followerCfg);
        }


        private delegate* unmanaged[Cdecl]<float, float> c_SparkMax_SafeFloatFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float c_SparkMax_SafeFloat(float f)
        {
            return c_SparkMax_SafeFloatFunc(f);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ControlType, int, float, int, ErrorCode> c_SparkMax_SetpointCommandFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetpointCommand(void* handle, float value, ControlType ctrl, int pidSlot, float arbFeedforward, int arbFFUnits)
        {
            return c_SparkMax_SetpointCommandFunc(handle, value, ctrl, pidSlot, arbFeedforward, arbFFUnits);
        }


        private delegate* unmanaged[Cdecl]<void*, DRVStatus*, ErrorCode> c_SparkMax_GetDRVStatusFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetDRVStatus(void* handle, DRVStatus* drvStatus)
        {
            return c_SparkMax_GetDRVStatusFunc(handle, drvStatus);
        }


        private delegate* unmanaged[Cdecl]<void*, byte, ErrorCode> c_SparkMax_SetInvertedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetInverted(void* handle, byte inverted)
        {
            return c_SparkMax_SetInvertedFunc(handle, inverted);
        }


        private delegate* unmanaged[Cdecl]<void*, byte*, ErrorCode> c_SparkMax_GetInvertedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetInverted(void* handle, byte* inverted)
        {
            return c_SparkMax_GetInvertedFunc(handle, inverted);
        }


        private delegate* unmanaged[Cdecl]<void*, byte, byte, uint, ErrorCode> c_SparkMax_SetSmartCurrentLimitFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetSmartCurrentLimit(void* handle, byte stallLimit, byte freeLimit, uint limitRPM)
        {
            return c_SparkMax_SetSmartCurrentLimitFunc(handle, stallLimit, freeLimit, limitRPM);
        }


        private delegate* unmanaged[Cdecl]<void*, byte*, byte*, uint*, ErrorCode> c_SparkMax_GetSmartCurrentLimitFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetSmartCurrentLimit(void* handle, byte* stallLimit, byte* freeLimit, uint* limitRPM)
        {
            return c_SparkMax_GetSmartCurrentLimitFunc(handle, stallLimit, freeLimit, limitRPM);
        }


        private delegate* unmanaged[Cdecl]<void*, float, int, ErrorCode> c_SparkMax_SetSecondaryCurrentLimitFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetSecondaryCurrentLimit(void* handle, float limit, int chopCycles)
        {
            return c_SparkMax_SetSecondaryCurrentLimitFunc(handle, limit, chopCycles);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, int*, ErrorCode> c_SparkMax_GetSecondaryCurrentLimitFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetSecondaryCurrentLimit(void* handle, float* limit, int* chopCycles)
        {
            return c_SparkMax_GetSecondaryCurrentLimitFunc(handle, limit, chopCycles);
        }


        private delegate* unmanaged[Cdecl]<void*, IdleMode, ErrorCode> c_SparkMax_SetIdleModeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetIdleMode(void* handle, IdleMode idlemode)
        {
            return c_SparkMax_SetIdleModeFunc(handle, idlemode);
        }


        private delegate* unmanaged[Cdecl]<void*, IdleMode*, ErrorCode> c_SparkMax_GetIdleModeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetIdleMode(void* handle, IdleMode* idlemode)
        {
            return c_SparkMax_GetIdleModeFunc(handle, idlemode);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ErrorCode> c_SparkMax_EnableVoltageCompensationFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_EnableVoltageCompensation(void* handle, float nominalVoltage)
        {
            return c_SparkMax_EnableVoltageCompensationFunc(handle, nominalVoltage);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetVoltageCompensationNominalVoltageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetVoltageCompensationNominalVoltage(void* handle, float* nominalVoltage)
        {
            return c_SparkMax_GetVoltageCompensationNominalVoltageFunc(handle, nominalVoltage);
        }


        private delegate* unmanaged[Cdecl]<void*, ErrorCode> c_SparkMax_DisableVoltageCompensationFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_DisableVoltageCompensation(void* handle)
        {
            return c_SparkMax_DisableVoltageCompensationFunc(handle);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ErrorCode> c_SparkMax_SetOpenLoopRampRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetOpenLoopRampRate(void* handle, float rate)
        {
            return c_SparkMax_SetOpenLoopRampRateFunc(handle, rate);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetOpenLoopRampRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetOpenLoopRampRate(void* handle, float* rate)
        {
            return c_SparkMax_GetOpenLoopRampRateFunc(handle, rate);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ErrorCode> c_SparkMax_SetClosedLoopRampRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetClosedLoopRampRate(void* handle, float rate)
        {
            return c_SparkMax_SetClosedLoopRampRateFunc(handle, rate);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetClosedLoopRampRateFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetClosedLoopRampRate(void* handle, float* rate)
        {
            return c_SparkMax_GetClosedLoopRampRateFunc(handle, rate);
        }


        private delegate* unmanaged[Cdecl]<void*, byte*, ErrorCode> c_SparkMax_IsFollowerFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_IsFollower(void* handle, byte* isFollower)
        {
            return c_SparkMax_IsFollowerFunc(handle, isFollower);
        }


        private delegate* unmanaged[Cdecl]<void*, ushort*, ErrorCode> c_SparkMax_GetFaultsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetFaults(void* handle, ushort* faults)
        {
            return c_SparkMax_GetFaultsFunc(handle, faults);
        }


        private delegate* unmanaged[Cdecl]<void*, ushort*, ErrorCode> c_SparkMax_GetStickyFaultsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetStickyFaults(void* handle, ushort* stickyFaults)
        {
            return c_SparkMax_GetStickyFaultsFunc(handle, stickyFaults);
        }


        private delegate* unmanaged[Cdecl]<void*, FaultID, byte*, ErrorCode> c_SparkMax_GetFaultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetFault(void* handle, FaultID faultId, byte* fault)
        {
            return c_SparkMax_GetFaultFunc(handle, faultId, fault);
        }


        private delegate* unmanaged[Cdecl]<void*, FaultID, byte*, ErrorCode> c_SparkMax_GetStickyFaultFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetStickyFault(void* handle, FaultID faultId, byte* stickyfault)
        {
            return c_SparkMax_GetStickyFaultFunc(handle, faultId, stickyfault);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetBusVoltageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetBusVoltage(void* handle, float* busVoltage)
        {
            return c_SparkMax_GetBusVoltageFunc(handle, busVoltage);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetAppliedOutputFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAppliedOutput(void* handle, float* appliedOutput)
        {
            return c_SparkMax_GetAppliedOutputFunc(handle, appliedOutput);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetOutputCurrentFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetOutputCurrent(void* handle, float* outputCurrent)
        {
            return c_SparkMax_GetOutputCurrentFunc(handle, outputCurrent);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetMotorTemperatureFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetMotorTemperature(void* handle, float* motorTemperature)
        {
            return c_SparkMax_GetMotorTemperatureFunc(handle, motorTemperature);
        }


        private delegate* unmanaged[Cdecl]<void*, ErrorCode> c_SparkMax_ClearFaultsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_ClearFaults(void* handle)
        {
            return c_SparkMax_ClearFaultsFunc(handle);
        }


        private delegate* unmanaged[Cdecl]<void*, ErrorCode> c_SparkMax_BurnFlashFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_BurnFlash(void* handle)
        {
            return c_SparkMax_BurnFlashFunc(handle);
        }


        private delegate* unmanaged[Cdecl]<void*, int, ErrorCode> c_SparkMax_SetCANTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetCANTimeout(void* handle, int timeoutMs)
        {
            return c_SparkMax_SetCANTimeoutFunc(handle, timeoutMs);
        }


        private delegate* unmanaged[Cdecl]<void*, LimitDirection, byte, ErrorCode> c_SparkMax_EnableSoftLimitFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_EnableSoftLimit(void* handle, LimitDirection dir, byte enable)
        {
            return c_SparkMax_EnableSoftLimitFunc(handle, dir, enable);
        }


        private delegate* unmanaged[Cdecl]<void*, LimitDirection, byte*, ErrorCode> c_SparkMax_IsSoftLimitEnabledFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_IsSoftLimitEnabled(void* handle, LimitDirection dir, byte* enabled)
        {
            return c_SparkMax_IsSoftLimitEnabledFunc(handle, dir, enabled);
        }


        private delegate* unmanaged[Cdecl]<void*, LimitDirection, float, ErrorCode> c_SparkMax_SetSoftLimitFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetSoftLimit(void* handle, LimitDirection dir, float limit)
        {
            return c_SparkMax_SetSoftLimitFunc(handle, dir, limit);
        }


        private delegate* unmanaged[Cdecl]<void*, LimitDirection, float*, ErrorCode> c_SparkMax_GetSoftLimitFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetSoftLimit(void* handle, LimitDirection dir, float* limit)
        {
            return c_SparkMax_GetSoftLimitFunc(handle, dir, limit);
        }


        private delegate* unmanaged[Cdecl]<void*, EncoderType, ErrorCode> c_SparkMax_SetSensorTypeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetSensorType(void* handle, EncoderType sensorType)
        {
            return c_SparkMax_SetSensorTypeFunc(handle, sensorType);
        }


        private delegate* unmanaged[Cdecl]<uint*, IntPtr, IntPtr*, ErrorCode> c_SparkMax_IDQueryFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_IDQuery(uint* uniqueIdArray, IntPtr uniqueIdArraySize, IntPtr* numberOfDevices)
        {
            return c_SparkMax_IDQueryFunc(uniqueIdArray, uniqueIdArraySize, numberOfDevices);
        }


        private delegate* unmanaged[Cdecl]<uint, byte, ErrorCode> c_SparkMax_IDAssignFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_IDAssign(uint uniqueId, byte deviceId)
        {
            return c_SparkMax_IDAssignFunc(uniqueId, deviceId);
        }


        private delegate* unmanaged[Cdecl]<void*, ErrorCode> c_SparkMax_IdentifyFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_Identify(void* handle)
        {
            return c_SparkMax_IdentifyFunc(handle);
        }


        private delegate* unmanaged[Cdecl]<uint, ErrorCode> c_SparkMax_IdentifyUniqueIdFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_IdentifyUniqueId(uint uniqueId)
        {
            return c_SparkMax_IdentifyUniqueIdFunc(uniqueId);
        }


        private delegate* unmanaged[Cdecl]<void*, LimitDirection, LimitPolarity, ErrorCode> c_SparkMax_SetLimitPolarityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetLimitPolarity(void* handle, LimitDirection sw, LimitPolarity polarity)
        {
            return c_SparkMax_SetLimitPolarityFunc(handle, sw, polarity);
        }


        private delegate* unmanaged[Cdecl]<void*, LimitDirection, LimitPolarity*, ErrorCode> c_SparkMax_GetLimitPolarityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetLimitPolarity(void* handle, LimitDirection sw, LimitPolarity* polarity)
        {
            return c_SparkMax_GetLimitPolarityFunc(handle, sw, polarity);
        }


        private delegate* unmanaged[Cdecl]<void*, LimitDirection, byte*, ErrorCode> c_SparkMax_GetLimitSwitchFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetLimitSwitch(void* handle, LimitDirection sw, byte* limit)
        {
            return c_SparkMax_GetLimitSwitchFunc(handle, sw, limit);
        }


        private delegate* unmanaged[Cdecl]<void*, LimitDirection, byte, ErrorCode> c_SparkMax_EnableLimitSwitchFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_EnableLimitSwitch(void* handle, LimitDirection sw, byte enable)
        {
            return c_SparkMax_EnableLimitSwitchFunc(handle, sw, enable);
        }


        private delegate* unmanaged[Cdecl]<void*, LimitDirection, byte*, ErrorCode> c_SparkMax_IsLimitEnabledFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_IsLimitEnabled(void* handle, LimitDirection sw, byte* enabled)
        {
            return c_SparkMax_IsLimitEnabledFunc(handle, sw, enabled);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetAnalogPositionFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAnalogPosition(void* handle, float* position)
        {
            return c_SparkMax_GetAnalogPositionFunc(handle, position);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetAnalogVelocityFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAnalogVelocity(void* handle, float* velocity)
        {
            return c_SparkMax_GetAnalogVelocityFunc(handle, velocity);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetAnalogVoltageFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAnalogVoltage(void* handle, float* voltage)
        {
            return c_SparkMax_GetAnalogVoltageFunc(handle, voltage);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ErrorCode> c_SparkMax_SetAnalogPositionConversionFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAnalogPositionConversionFactor(void* handle, float conversion)
        {
            return c_SparkMax_SetAnalogPositionConversionFactorFunc(handle, conversion);
        }


        private delegate* unmanaged[Cdecl]<void*, float, ErrorCode> c_SparkMax_SetAnalogVelocityConversionFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAnalogVelocityConversionFactor(void* handle, float conversion)
        {
            return c_SparkMax_SetAnalogVelocityConversionFactorFunc(handle, conversion);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetAnalogPositionConversionFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAnalogPositionConversionFactor(void* handle, float* conversion)
        {
            return c_SparkMax_GetAnalogPositionConversionFactorFunc(handle, conversion);
        }


        private delegate* unmanaged[Cdecl]<void*, float*, ErrorCode> c_SparkMax_GetAnalogVelocityConversionFactorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAnalogVelocityConversionFactor(void* handle, float* conversion)
        {
            return c_SparkMax_GetAnalogVelocityConversionFactorFunc(handle, conversion);
        }


        private delegate* unmanaged[Cdecl]<void*, byte, ErrorCode> c_SparkMax_SetAnalogInvertedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAnalogInverted(void* handle, byte inverted)
        {
            return c_SparkMax_SetAnalogInvertedFunc(handle, inverted);
        }


        private delegate* unmanaged[Cdecl]<void*, byte*, ErrorCode> c_SparkMax_GetAnalogInvertedFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAnalogInverted(void* handle, byte* inverted)
        {
            return c_SparkMax_GetAnalogInvertedFunc(handle, inverted);
        }


        private delegate* unmanaged[Cdecl]<void*, uint, ErrorCode> c_SparkMax_SetAnalogAverageDepthFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAnalogAverageDepth(void* handle, uint depth)
        {
            return c_SparkMax_SetAnalogAverageDepthFunc(handle, depth);
        }


        private delegate* unmanaged[Cdecl]<void*, uint*, ErrorCode> c_SparkMax_GetAnalogAverageDepthFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAnalogAverageDepth(void* handle, uint* depth)
        {
            return c_SparkMax_GetAnalogAverageDepthFunc(handle, depth);
        }


        private delegate* unmanaged[Cdecl]<void*, uint, ErrorCode> c_SparkMax_SetAnalogMeasurementPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_SetAnalogMeasurementPeriod(void* handle, uint samples)
        {
            return c_SparkMax_SetAnalogMeasurementPeriodFunc(handle, samples);
        }


        private delegate* unmanaged[Cdecl]<void*, uint*, ErrorCode> c_SparkMax_GetAnalogMeasurementPeriodFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ErrorCode c_SparkMax_GetAnalogMeasurementPeriod(void* handle, uint* samples)
        {
            return c_SparkMax_GetAnalogMeasurementPeriodFunc(handle, samples);
        }



        public CANSparkMaxDriverNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            c_SparkMax_SetAnalogModeFunc = (delegate* unmanaged[Cdecl] < void *, AnalogMode, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAnalogMode");
            c_SparkMax_GetAnalogModeFunc = (delegate* unmanaged[Cdecl] < void *, AnalogMode *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAnalogMode");
            c_SparkMax_GetEncoderPositionFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetEncoderPosition");
            c_SparkMax_GetEncoderVelocityFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetEncoderVelocity");
            c_SparkMax_SetPositionConversionFactorFunc = (delegate* unmanaged[Cdecl] < void *, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetPositionConversionFactor");
            c_SparkMax_SetVelocityConversionFactorFunc = (delegate* unmanaged[Cdecl] < void *, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetVelocityConversionFactor");
            c_SparkMax_GetPositionConversionFactorFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetPositionConversionFactor");
            c_SparkMax_GetVelocityConversionFactorFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetVelocityConversionFactor");
            c_SparkMax_SetAverageDepthFunc = (delegate* unmanaged[Cdecl] < void *, uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAverageDepth");
            c_SparkMax_GetAverageDepthFunc = (delegate* unmanaged[Cdecl] < void *, uint *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAverageDepth");
            c_SparkMax_SetMeasurementPeriodFunc = (delegate* unmanaged[Cdecl] < void *, uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetMeasurementPeriod");
            c_SparkMax_GetMeasurementPeriodFunc = (delegate* unmanaged[Cdecl] < void *, uint *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetMeasurementPeriod");
            c_SparkMax_SetCountsPerRevolutionFunc = (delegate* unmanaged[Cdecl] < void *, uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetCountsPerRevolution");
            c_SparkMax_GetCountsPerRevolutionFunc = (delegate* unmanaged[Cdecl] < void *, uint *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetCountsPerRevolution");
            c_SparkMax_SetEncoderInvertedFunc = (delegate* unmanaged[Cdecl] < void *, byte, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetEncoderInverted");
            c_SparkMax_GetEncoderInvertedFunc = (delegate* unmanaged[Cdecl] < void *, byte *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetEncoderInverted");
            c_SparkMax_GetAltEncoderPositionFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAltEncoderPosition");
            c_SparkMax_GetAltEncoderVelocityFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAltEncoderVelocity");
            c_SparkMax_SetAltEncoderPositionFactorFunc = (delegate* unmanaged[Cdecl] < void *, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAltEncoderPositionFactor");
            c_SparkMax_SetAltEncoderVelocityFactorFunc = (delegate* unmanaged[Cdecl] < void *, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAltEncoderVelocityFactor");
            c_SparkMax_GetAltEncoderPositionFactorFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAltEncoderPositionFactor");
            c_SparkMax_GetAltEncoderVelocityFactorFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAltEncoderVelocityFactor");
            c_SparkMax_SetAltEncoderAverageDepthFunc = (delegate* unmanaged[Cdecl] < void *, uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAltEncoderAverageDepth");
            c_SparkMax_GetAltEncoderAverageDepthFunc = (delegate* unmanaged[Cdecl] < void *, uint *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAltEncoderAverageDepth");
            c_SparkMax_SetAltEncoderMeasurementPeriodFunc = (delegate* unmanaged[Cdecl] < void *, uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAltEncoderMeasurementPeriod");
            c_SparkMax_GetAltEncoderMeasurementPeriodFunc = (delegate* unmanaged[Cdecl] < void *, uint *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAltEncoderMeasurementPeriod");
            c_SparkMax_SetAltEncoderCountsPerRevolutionFunc = (delegate* unmanaged[Cdecl] < void *, uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAltEncoderCountsPerRevolution");
            c_SparkMax_GetAltEncoderCountsPerRevolutionFunc = (delegate* unmanaged[Cdecl] < void *, uint *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAltEncoderCountsPerRevolution");
            c_SparkMax_SetAltEncoderInvertedFunc = (delegate* unmanaged[Cdecl] < void *, byte, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAltEncoderInverted");
            c_SparkMax_GetAltEncoderInvertedFunc = (delegate* unmanaged[Cdecl] < void *, byte *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAltEncoderInverted");
            c_SparkMax_SetDataPortConfigFunc = (delegate* unmanaged[Cdecl] < void *, DataPortConfig, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetDataPortConfig");
            c_SparkMax_GetDataPortConfigFunc = (delegate* unmanaged[Cdecl] < void *, DataPortConfig *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetDataPortConfig");
            c_SparkMax_SetPFunc = (delegate* unmanaged[Cdecl] < void *, int, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetP");
            c_SparkMax_SetIFunc = (delegate* unmanaged[Cdecl] < void *, int, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetI");
            c_SparkMax_SetDFunc = (delegate* unmanaged[Cdecl] < void *, int, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetD");
            c_SparkMax_SetDFilterFunc = (delegate* unmanaged[Cdecl] < void *, int, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetDFilter");
            c_SparkMax_SetFFFunc = (delegate* unmanaged[Cdecl] < void *, int, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetFF");
            c_SparkMax_SetIZoneFunc = (delegate* unmanaged[Cdecl] < void *, int, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetIZone");
            c_SparkMax_SetOutputRangeFunc = (delegate* unmanaged[Cdecl] < void *, int, float, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetOutputRange");
            c_SparkMax_GetPFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetP");
            c_SparkMax_GetIFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetI");
            c_SparkMax_GetDFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetD");
            c_SparkMax_GetDFilterFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetDFilter");
            c_SparkMax_GetFFFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetFF");
            c_SparkMax_GetIZoneFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetIZone");
            c_SparkMax_GetOutputMinFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetOutputMin");
            c_SparkMax_GetOutputMaxFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetOutputMax");
            c_SparkMax_SetSmartMotionMaxVelocityFunc = (delegate* unmanaged[Cdecl] < void *, int, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetSmartMotionMaxVelocity");
            c_SparkMax_SetSmartMotionMaxAccelFunc = (delegate* unmanaged[Cdecl] < void *, int, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetSmartMotionMaxAccel");
            c_SparkMax_SetSmartMotionMinOutputVelocityFunc = (delegate* unmanaged[Cdecl] < void *, int, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetSmartMotionMinOutputVelocity");
            c_SparkMax_SetSmartMotionAccelStrategyFunc = (delegate* unmanaged[Cdecl] < void *, int, AccelStrategy, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetSmartMotionAccelStrategy");
            c_SparkMax_SetSmartMotionAllowedClosedLoopErrorFunc = (delegate* unmanaged[Cdecl] < void *, int, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetSmartMotionAllowedClosedLoopError");
            c_SparkMax_GetSmartMotionMaxVelocityFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetSmartMotionMaxVelocity");
            c_SparkMax_GetSmartMotionMaxAccelFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetSmartMotionMaxAccel");
            c_SparkMax_GetSmartMotionMinOutputVelocityFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetSmartMotionMinOutputVelocity");
            c_SparkMax_GetSmartMotionAccelStrategyFunc = (delegate* unmanaged[Cdecl] < void *, int, AccelStrategy *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetSmartMotionAccelStrategy");
            c_SparkMax_GetSmartMotionAllowedClosedLoopErrorFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetSmartMotionAllowedClosedLoopError");
            c_SparkMax_SetIMaxAccumFunc = (delegate* unmanaged[Cdecl] < void *, int, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetIMaxAccum");
            c_SparkMax_GetIMaxAccumFunc = (delegate* unmanaged[Cdecl] < void *, int, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetIMaxAccum");
            c_SparkMax_SetIAccumFunc = (delegate* unmanaged[Cdecl] < void *, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetIAccum");
            c_SparkMax_GetIAccumFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetIAccum");
            c_SparkMax_SetFeedbackDeviceFunc = (delegate* unmanaged[Cdecl] < void *, uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetFeedbackDevice");
            c_SparkMax_GetFeedbackDeviceIDFunc = (delegate* unmanaged[Cdecl] < void *, uint *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetFeedbackDeviceID");
            c_SparkMax_SetFeedbackDeviceRangeFunc = (delegate* unmanaged[Cdecl] < void *, float, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetFeedbackDeviceRange");
            c_SparkMax_GetAPIVersionFunc = (delegate* unmanaged[Cdecl] < APIVersion >)loader.GetProcAddress("c_SparkMax_GetAPIVersion");
            c_SparkMax_SetLastErrorFunc = (delegate* unmanaged[Cdecl] < void *, ErrorCode, void >)loader.GetProcAddress("c_SparkMax_SetLastError");
            c_SparkMax_GetLastErrorFunc = (delegate* unmanaged[Cdecl] < void *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetLastError");
            c_SparkMax_GenerateErrorFunc = (delegate* unmanaged[Cdecl] < int, ErrorCode, ErrorCode >)loader.GetProcAddress("c_SparkMax_GenerateError");
            c_SparkMax_CreateFunc = (delegate* unmanaged[Cdecl] < int, MotorType, void *>)loader.GetProcAddress("c_SparkMax_Create");
            c_SparkMax_Create_InplaceFunc = (delegate* unmanaged[Cdecl] < int, void *>)loader.GetProcAddress("c_SparkMax_Create_Inplace");
            c_SparkMax_DestroyFunc = (delegate* unmanaged[Cdecl] < void *, void >)loader.GetProcAddress("c_SparkMax_Destroy");
            c_SparkMax_GetFirmwareVersionFunc = (delegate* unmanaged[Cdecl] < void *, FirmwareVersion *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetFirmwareVersion");
            c_SparkMax_GetDeviceIdFunc = (delegate* unmanaged[Cdecl] < void *, int *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetDeviceId");
            c_SparkMax_SetMotorTypeFunc = (delegate* unmanaged[Cdecl] < void *, MotorType, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetMotorType");
            c_SparkMax_GetMotorTypeFunc = (delegate* unmanaged[Cdecl] < void *, MotorType *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetMotorType");
            c_SparkMax_SetPeriodicFramePeriodFunc = (delegate* unmanaged[Cdecl] < void *, PeriodicFrame, int, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetPeriodicFramePeriod");
            c_SparkMax_SetControlFramePeriodFunc = (delegate* unmanaged[Cdecl] < void *, int, void >)loader.GetProcAddress("c_SparkMax_SetControlFramePeriod");
            c_SparkMax_GetControlFramePeriodFunc = (delegate* unmanaged[Cdecl] < void *, int >)loader.GetProcAddress("c_SparkMax_GetControlFramePeriod");
            c_SparkMax_SetParameterFloat32Func = (delegate* unmanaged[Cdecl] < void *, ConfigParameter, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetParameterFloat32");
            c_SparkMax_SetParameterInt32Func = (delegate* unmanaged[Cdecl] < void *, ConfigParameter, int, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetParameterInt32");
            c_SparkMax_SetParameterUint32Func = (delegate* unmanaged[Cdecl] < void *, ConfigParameter, uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetParameterUint32");
            c_SparkMax_SetParameterBoolFunc = (delegate* unmanaged[Cdecl] < void *, ConfigParameter, byte, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetParameterBool");
            c_SparkMax_GetParameterFloat32Func = (delegate* unmanaged[Cdecl] < void *, ConfigParameter, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetParameterFloat32");
            c_SparkMax_GetParameterInt32Func = (delegate* unmanaged[Cdecl] < void *, ConfigParameter, int *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetParameterInt32");
            c_SparkMax_GetParameterUint32Func = (delegate* unmanaged[Cdecl] < void *, ConfigParameter, uint *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetParameterUint32");
            c_SparkMax_GetParameterBoolFunc = (delegate* unmanaged[Cdecl] < void *, ConfigParameter, byte *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetParameterBool");
            c_SparkMax_GetPeriodicStatus0Func = (delegate* unmanaged[Cdecl] < void *, PeriodicStatus0 *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetPeriodicStatus0");
            c_SparkMax_GetPeriodicStatus1Func = (delegate* unmanaged[Cdecl] < void *, PeriodicStatus1 *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetPeriodicStatus1");
            c_SparkMax_GetPeriodicStatus2Func = (delegate* unmanaged[Cdecl] < void *, PeriodicStatus2 *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetPeriodicStatus2");
            c_SparkMax_GetPeriodicStatus3Func = (delegate* unmanaged[Cdecl] < void *, PeriodicStatus3 *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetPeriodicStatus3");
            c_SparkMax_GetPeriodicStatus4Func = (delegate* unmanaged[Cdecl] < void *, PeriodicStatus4 *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetPeriodicStatus4");
            c_SparkMax_SetEncoderPositionFunc = (delegate* unmanaged[Cdecl] < void *, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetEncoderPosition");
            c_SparkMax_SetAltEncoderPositionFunc = (delegate* unmanaged[Cdecl] < void *, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAltEncoderPosition");
            c_SparkMax_RestoreFactoryDefaultsFunc = (delegate* unmanaged[Cdecl] < void *, byte, ErrorCode >)loader.GetProcAddress("c_SparkMax_RestoreFactoryDefaults");
            c_SparkMax_FactoryWipeFunc = (delegate* unmanaged[Cdecl] < void *, byte, ErrorCode >)loader.GetProcAddress("c_SparkMax_FactoryWipe");
            c_SparkMax_SetFollowFunc = (delegate* unmanaged[Cdecl] < void *, uint, uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetFollow");
            c_SparkMax_SafeFloatFunc = (delegate* unmanaged[Cdecl] < float, float >)loader.GetProcAddress("c_SparkMax_SafeFloat");
            c_SparkMax_SetpointCommandFunc = (delegate* unmanaged[Cdecl] < void *, float, ControlType, int, float, int, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetpointCommand");
            c_SparkMax_GetDRVStatusFunc = (delegate* unmanaged[Cdecl] < void *, DRVStatus *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetDRVStatus");
            c_SparkMax_SetInvertedFunc = (delegate* unmanaged[Cdecl] < void *, byte, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetInverted");
            c_SparkMax_GetInvertedFunc = (delegate* unmanaged[Cdecl] < void *, byte *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetInverted");
            c_SparkMax_SetSmartCurrentLimitFunc = (delegate* unmanaged[Cdecl] < void *, byte, byte, uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetSmartCurrentLimit");
            c_SparkMax_GetSmartCurrentLimitFunc = (delegate* unmanaged[Cdecl] < void *, byte *, byte *, uint *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetSmartCurrentLimit");
            c_SparkMax_SetSecondaryCurrentLimitFunc = (delegate* unmanaged[Cdecl] < void *, float, int, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetSecondaryCurrentLimit");
            c_SparkMax_GetSecondaryCurrentLimitFunc = (delegate* unmanaged[Cdecl] < void *, float *, int *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetSecondaryCurrentLimit");
            c_SparkMax_SetIdleModeFunc = (delegate* unmanaged[Cdecl] < void *, IdleMode, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetIdleMode");
            c_SparkMax_GetIdleModeFunc = (delegate* unmanaged[Cdecl] < void *, IdleMode *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetIdleMode");
            c_SparkMax_EnableVoltageCompensationFunc = (delegate* unmanaged[Cdecl] < void *, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_EnableVoltageCompensation");
            c_SparkMax_GetVoltageCompensationNominalVoltageFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetVoltageCompensationNominalVoltage");
            c_SparkMax_DisableVoltageCompensationFunc = (delegate* unmanaged[Cdecl] < void *, ErrorCode >)loader.GetProcAddress("c_SparkMax_DisableVoltageCompensation");
            c_SparkMax_SetOpenLoopRampRateFunc = (delegate* unmanaged[Cdecl] < void *, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetOpenLoopRampRate");
            c_SparkMax_GetOpenLoopRampRateFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetOpenLoopRampRate");
            c_SparkMax_SetClosedLoopRampRateFunc = (delegate* unmanaged[Cdecl] < void *, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetClosedLoopRampRate");
            c_SparkMax_GetClosedLoopRampRateFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetClosedLoopRampRate");
            c_SparkMax_IsFollowerFunc = (delegate* unmanaged[Cdecl] < void *, byte *, ErrorCode >)loader.GetProcAddress("c_SparkMax_IsFollower");
            c_SparkMax_GetFaultsFunc = (delegate* unmanaged[Cdecl] < void *, ushort *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetFaults");
            c_SparkMax_GetStickyFaultsFunc = (delegate* unmanaged[Cdecl] < void *, ushort *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetStickyFaults");
            c_SparkMax_GetFaultFunc = (delegate* unmanaged[Cdecl] < void *, FaultID, byte *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetFault");
            c_SparkMax_GetStickyFaultFunc = (delegate* unmanaged[Cdecl] < void *, FaultID, byte *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetStickyFault");
            c_SparkMax_GetBusVoltageFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetBusVoltage");
            c_SparkMax_GetAppliedOutputFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAppliedOutput");
            c_SparkMax_GetOutputCurrentFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetOutputCurrent");
            c_SparkMax_GetMotorTemperatureFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetMotorTemperature");
            c_SparkMax_ClearFaultsFunc = (delegate* unmanaged[Cdecl] < void *, ErrorCode >)loader.GetProcAddress("c_SparkMax_ClearFaults");
            c_SparkMax_BurnFlashFunc = (delegate* unmanaged[Cdecl] < void *, ErrorCode >)loader.GetProcAddress("c_SparkMax_BurnFlash");
            c_SparkMax_SetCANTimeoutFunc = (delegate* unmanaged[Cdecl] < void *, int, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetCANTimeout");
            c_SparkMax_EnableSoftLimitFunc = (delegate* unmanaged[Cdecl] < void *, LimitDirection, byte, ErrorCode >)loader.GetProcAddress("c_SparkMax_EnableSoftLimit");
            c_SparkMax_IsSoftLimitEnabledFunc = (delegate* unmanaged[Cdecl] < void *, LimitDirection, byte *, ErrorCode >)loader.GetProcAddress("c_SparkMax_IsSoftLimitEnabled");
            c_SparkMax_SetSoftLimitFunc = (delegate* unmanaged[Cdecl] < void *, LimitDirection, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetSoftLimit");
            c_SparkMax_GetSoftLimitFunc = (delegate* unmanaged[Cdecl] < void *, LimitDirection, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetSoftLimit");
            c_SparkMax_SetSensorTypeFunc = (delegate* unmanaged[Cdecl] < void *, EncoderType, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetSensorType");
            c_SparkMax_IDQueryFunc = (delegate* unmanaged[Cdecl] < uint *, IntPtr, IntPtr *, ErrorCode >)loader.GetProcAddress("c_SparkMax_IDQuery");
            c_SparkMax_IDAssignFunc = (delegate* unmanaged[Cdecl] < uint, byte, ErrorCode >)loader.GetProcAddress("c_SparkMax_IDAssign");
            c_SparkMax_IdentifyFunc = (delegate* unmanaged[Cdecl] < void *, ErrorCode >)loader.GetProcAddress("c_SparkMax_Identify");
            c_SparkMax_IdentifyUniqueIdFunc = (delegate* unmanaged[Cdecl] < uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_IdentifyUniqueId");
            c_SparkMax_SetLimitPolarityFunc = (delegate* unmanaged[Cdecl] < void *, LimitDirection, LimitPolarity, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetLimitPolarity");
            c_SparkMax_GetLimitPolarityFunc = (delegate* unmanaged[Cdecl] < void *, LimitDirection, LimitPolarity *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetLimitPolarity");
            c_SparkMax_GetLimitSwitchFunc = (delegate* unmanaged[Cdecl] < void *, LimitDirection, byte *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetLimitSwitch");
            c_SparkMax_EnableLimitSwitchFunc = (delegate* unmanaged[Cdecl] < void *, LimitDirection, byte, ErrorCode >)loader.GetProcAddress("c_SparkMax_EnableLimitSwitch");
            c_SparkMax_IsLimitEnabledFunc = (delegate* unmanaged[Cdecl] < void *, LimitDirection, byte *, ErrorCode >)loader.GetProcAddress("c_SparkMax_IsLimitEnabled");
            c_SparkMax_GetAnalogPositionFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAnalogPosition");
            c_SparkMax_GetAnalogVelocityFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAnalogVelocity");
            c_SparkMax_GetAnalogVoltageFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAnalogVoltage");
            c_SparkMax_SetAnalogPositionConversionFactorFunc = (delegate* unmanaged[Cdecl] < void *, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAnalogPositionConversionFactor");
            c_SparkMax_SetAnalogVelocityConversionFactorFunc = (delegate* unmanaged[Cdecl] < void *, float, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAnalogVelocityConversionFactor");
            c_SparkMax_GetAnalogPositionConversionFactorFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAnalogPositionConversionFactor");
            c_SparkMax_GetAnalogVelocityConversionFactorFunc = (delegate* unmanaged[Cdecl] < void *, float *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAnalogVelocityConversionFactor");
            c_SparkMax_SetAnalogInvertedFunc = (delegate* unmanaged[Cdecl] < void *, byte, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAnalogInverted");
            c_SparkMax_GetAnalogInvertedFunc = (delegate* unmanaged[Cdecl] < void *, byte *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAnalogInverted");
            c_SparkMax_SetAnalogAverageDepthFunc = (delegate* unmanaged[Cdecl] < void *, uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAnalogAverageDepth");
            c_SparkMax_GetAnalogAverageDepthFunc = (delegate* unmanaged[Cdecl] < void *, uint *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAnalogAverageDepth");
            c_SparkMax_SetAnalogMeasurementPeriodFunc = (delegate* unmanaged[Cdecl] < void *, uint, ErrorCode >)loader.GetProcAddress("c_SparkMax_SetAnalogMeasurementPeriod");
            c_SparkMax_GetAnalogMeasurementPeriodFunc = (delegate* unmanaged[Cdecl] < void *, uint *, ErrorCode >)loader.GetProcAddress("c_SparkMax_GetAnalogMeasurementPeriod");
        }

    }
}
