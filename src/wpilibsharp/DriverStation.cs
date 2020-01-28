using Hal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WPILib
{
    public enum Alliance { kRed, kBlue, kInvalid }
    public enum MatchType { kNone, kPractice, kQualification, kElimination }

    public class DriverStation
    {
        

        private static Lazy<DriverStation> lazyInstance = new Lazy<DriverStation>(() =>
        {
            return new DriverStation();
        },LazyThreadSafetyMode.ExecutionAndPublication);

        public static DriverStation Instance => lazyInstance.Value;

        public static void ReportError(string error, bool printTrace)
        {
            ReportErrorImpl(true, 1, error, printTrace);
        }

        public static void ReportError(string error, string stackTrace)
        {
            ReportErrorImpl(true, 1, error, stackTrace);
        }

        public static void ReportWarning(string error, bool printTrace)
        {
            ReportErrorImpl(false, 1, error, printTrace);
        }

        public static void ReportWarning(string error, string stackTrace)
        {
            ReportErrorImpl(false, 1, error, stackTrace);
        }

        private static void ReportErrorImpl(bool isError, int code, string error, bool printTrace)
        {
            ReportErrorImpl(isError, code, error, printTrace, System.Environment.StackTrace, 3);
        }

        private static void ReportErrorImpl(bool isError, int code, string error, string stackTrace)
        {
            ReportErrorImpl(isError, code, error, true, stackTrace, 0);
        }

        private static void ReportErrorImpl(bool isError, int code, string error, bool printTrace, string stackTrace, int stackTraceFirst)
        {
            ReadOnlySpan<char> printedTrace = stackTrace.AsSpan();
            if (!printTrace)
            {
                printedTrace = "".AsSpan();
            }
            Hal.DriverStation.SendError(isError, code, false, error.AsSpan(), "".AsSpan(), printedTrace, true);
        }

        private bool m_userInDisabled = false;
        private bool m_userInAutonomous = false;
        private bool m_userInTeleop = false;
        private bool m_userInTest = false;

        private DriverStation()
        {

        }

        public bool GetStickButton(int stick, int button)
        {
            return false;
        }

        public bool GetStickButtonPressed(int stick, int button)
        {
            return false;
        }

        public bool GetStickButtonReleased(int stick, int button)
        {
            return false;
        }

        public double GetStickAxis(int stick, int axis)
        {
            return 0;
        }

        public int GetStickPOV(int stick, int pov)
        {
            return -1;
        }

        public uint GetStickButtons(int stick)
        {
            return 0;
        }

        public int GetStickAxisCount(int stick)
        {
            return 0;
        }

        public int GetStickPOVCount(int stick)
        {
            return 0;
        }

        public int GetStickButtonCount(int stick)
        {
            return 0;
        }

        public bool GetJoystickIsXbox(int stick)
        {
            return false;
        }

        public int GetJoystickType(int stick)
        {
            return 0;
        }

        public string GetJoystickName(int stick)
        {
            return "";
        }

        public int GetJoystickAxisType(int stick, int axis)
        {
            return 0;
        }

        public bool IsEnabled => Hal.DriverStation.GetControlWord().Enabled;
        public bool IsDisabled => !IsEnabled;
        public bool IsEStopped => Hal.DriverStation.GetControlWord().EStop;
        public bool IsAutonomous => Hal.DriverStation.GetControlWord().Autonomous;
        public bool IsOperatorControl => Hal.DriverStation.GetControlWord().FmsAttached;
        public bool IsTest => Hal.DriverStation.GetControlWord().Test;
        public bool IsDSAttached => Hal.DriverStation.GetControlWord().DsAttached;
        public bool IsNewControlData => Hal.DriverStation.IsNewControlData();
        public bool IsFMSAttached => Hal.DriverStation.GetControlWord().FmsAttached;

        public string GameSpecificMessage => "";

        public string EventName => "";

        public MatchType MatchType => MatchType.kNone;

        public int MatchNumber => 0;
        public int ReplayNumber => 0;
        public Alliance Alliance => Alliance.kInvalid;
        public int Location => 0;

        public void WaitForData()
        {

        }

        public bool WaitForData(double timeout)
        {
            return false;
        }

        public double MatchTime => 0;
        public double BatteryVoltage => 0;

        public void InDisabled(bool entering)
        {
            m_userInDisabled = entering;
        }

        public void InAutonomous(bool entering)
        {
            m_userInAutonomous = entering;
        }

        public void InOperatorControl(bool entering)
        {
            m_userInTeleop = entering;
        }

        public void InTest(bool entering)
        {
            m_userInTest = entering;
        }

        public void WakeupWaitForData()
        {
            Hal.DriverStation.ReleaseDSMutex();
        }

        protected void GetData()
        {

        }


        private void ReportJoystickUnpluggedError(ReadOnlySpan<char> message)
        {

        }

        private void ReportJoystickUnpluggedWarning(ReadOnlySpan<char> message)
        {

        }

        private void Run()
        {

        }

        private void SendMatchData()
        {

        }

        private readonly object m_buttonEdgeMutex = new object();
        private JoystickButtons[] m_previousButtonStates = new JoystickButtons[6];
        private uint[] m_joystickButtonsPressed = new uint[6];
        private uint[] m_joystickButtonsReleased = new uint[6];

        private readonly Thread m_dsThread;
        private bool m_isRunning = false;

        private readonly object m_waitForDataMutex = new object();
        private int m_waitForDataCounter = 0;
        private double m_nextMessageTime = 0;



    }
}
