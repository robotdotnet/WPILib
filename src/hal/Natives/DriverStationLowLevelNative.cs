using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
    public unsafe class DriverStationLowLevelNative
    {
        
        private readonly delegate* unmanaged[Cdecl]<int, int, int, byte*, byte*, byte*, int, int> HAL_SendErrorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_SendError(int isError, int errorCode, int isLVCode, byte* details, byte* location, byte* callStack, int printMsg)
        {
            return HAL_SendErrorFunc(isError, errorCode, isLVCode, details, location, callStack, printMsg);
        }


        
        private readonly delegate* unmanaged[Cdecl]<byte*, int> HAL_SendConsoleLineFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_SendConsoleLine(byte* line)
        {
            return HAL_SendConsoleLineFunc(line);
        }


        
        private readonly delegate* unmanaged[Cdecl]<Hal.ControlWord*, void> HAL_GetControlWordFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_GetControlWord(Hal.ControlWord* controlWord)
        {
            HAL_GetControlWordFunc(controlWord);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int*, Hal.AllianceStationID> HAL_GetAllianceStationFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Hal.AllianceStationID HAL_GetAllianceStation(int* status)
        {
            return HAL_GetAllianceStationFunc(status);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, Hal.JoystickAxes*, int> HAL_GetJoystickAxesFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetJoystickAxes(int joystickNum, Hal.JoystickAxes* axes)
        {
            return HAL_GetJoystickAxesFunc(joystickNum, axes);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, Hal.JoystickPOVs*, int> HAL_GetJoystickPOVsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetJoystickPOVs(int joystickNum, Hal.JoystickPOVs* povs)
        {
            return HAL_GetJoystickPOVsFunc(joystickNum, povs);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, Hal.JoystickButtons*, int> HAL_GetJoystickButtonsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetJoystickButtons(int joystickNum, Hal.JoystickButtons* buttons)
        {
            return HAL_GetJoystickButtonsFunc(joystickNum, buttons);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, Hal.JoystickDescriptor*, int> HAL_GetJoystickDescriptorFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetJoystickDescriptor(int joystickNum, Hal.JoystickDescriptor* desc)
        {
            return HAL_GetJoystickDescriptorFunc(joystickNum, desc);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_GetJoystickIsXboxFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetJoystickIsXbox(int joystickNum)
        {
            return HAL_GetJoystickIsXboxFunc(joystickNum);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int> HAL_GetJoystickTypeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetJoystickType(int joystickNum)
        {
            return HAL_GetJoystickTypeFunc(joystickNum);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, byte*> HAL_GetJoystickNameFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte* HAL_GetJoystickName(int joystickNum)
        {
            return HAL_GetJoystickNameFunc(joystickNum);
        }


        
        private readonly delegate* unmanaged[Cdecl]<byte*, void> HAL_FreeJoystickNameFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_FreeJoystickName(byte* name)
        {
            HAL_FreeJoystickNameFunc(name);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, int, int> HAL_GetJoystickAxisTypeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetJoystickAxisType(int joystickNum, int axis)
        {
            return HAL_GetJoystickAxisTypeFunc(joystickNum, axis);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int, long, int, int, int> HAL_SetJoystickOutputsFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_SetJoystickOutputs(int joystickNum, long outputs, int leftRumble, int rightRumble)
        {
            return HAL_SetJoystickOutputsFunc(joystickNum, outputs, leftRumble, rightRumble);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int*, double> HAL_GetMatchTimeFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double HAL_GetMatchTime(int* status)
        {
            return HAL_GetMatchTimeFunc(status);
        }


        
        private readonly delegate* unmanaged[Cdecl]<Hal.MatchInfo*, int> HAL_GetMatchInfoFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_GetMatchInfo(Hal.MatchInfo* info)
        {
            return HAL_GetMatchInfoFunc(info);
        }


        
        private readonly delegate* unmanaged[Cdecl]<void> HAL_ReleaseDSMutexFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ReleaseDSMutex()
        {
            HAL_ReleaseDSMutexFunc();
        }


        
        private readonly delegate* unmanaged[Cdecl]<void> HAL_WaitForCachedControlDataFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_WaitForCachedControlData()
        {
            HAL_WaitForCachedControlDataFunc();
        }


        
        private readonly delegate* unmanaged[Cdecl]<double, int> HAL_WaitForCachedControlDataTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_WaitForCachedControlDataTimeout(double timeout)
        {
            return HAL_WaitForCachedControlDataTimeoutFunc(timeout);
        }


        
        private readonly delegate* unmanaged[Cdecl]<int> HAL_IsNewControlDataFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_IsNewControlData()
        {
            return HAL_IsNewControlDataFunc();
        }


        
        private readonly delegate* unmanaged[Cdecl]<void> HAL_WaitForDSDataFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_WaitForDSData()
        {
            HAL_WaitForDSDataFunc();
        }


        
        private readonly delegate* unmanaged[Cdecl]<double, int> HAL_WaitForDSDataTimeoutFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_WaitForDSDataTimeout(double timeout)
        {
            return HAL_WaitForDSDataTimeoutFunc(timeout);
        }


        
        private readonly delegate* unmanaged[Cdecl]<void> HAL_InitializeDriverStationFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_InitializeDriverStation()
        {
            HAL_InitializeDriverStationFunc();
        }


        
        private readonly delegate* unmanaged[Cdecl]<void> HAL_ObserveUserProgramStartingFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ObserveUserProgramStarting()
        {
            HAL_ObserveUserProgramStartingFunc();
        }


        
        private readonly delegate* unmanaged[Cdecl]<void> HAL_ObserveUserProgramDisabledFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ObserveUserProgramDisabled()
        {
            HAL_ObserveUserProgramDisabledFunc();
        }


        
        private readonly delegate* unmanaged[Cdecl]<void> HAL_ObserveUserProgramAutonomousFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ObserveUserProgramAutonomous()
        {
            HAL_ObserveUserProgramAutonomousFunc();
        }


        
        private readonly delegate* unmanaged[Cdecl]<void> HAL_ObserveUserProgramTeleopFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ObserveUserProgramTeleop()
        {
            HAL_ObserveUserProgramTeleopFunc();
        }


        
        private readonly delegate* unmanaged[Cdecl]<void> HAL_ObserveUserProgramTestFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_ObserveUserProgramTest()
        {
            HAL_ObserveUserProgramTestFunc();
        }



    }
}
