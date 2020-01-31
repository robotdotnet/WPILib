using Hal;
using NetworkTables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnitsNet;
using WPIUtil;

namespace WPILib
{
    public enum Alliance { kRed, kBlue, kInvalid }
    public enum MatchType { kNone, kPractice, kQualification, kElimination }

    public class DriverStation : IDisposable
    {
        

        private static readonly Lazy<DriverStation> lazyInstance = new Lazy<DriverStation>(() =>
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

        private readonly struct MatchDataSender
        {
            public readonly NetworkTable table;
            public readonly NetworkTableEntry typeMetadata;
            public readonly NetworkTableEntry gameSpecificMessage;
            public readonly NetworkTableEntry eventName;
            public readonly NetworkTableEntry matchNumber;
            public readonly NetworkTableEntry replayNumber;
            public readonly NetworkTableEntry matchType;
            public readonly NetworkTableEntry alliance;
            public readonly NetworkTableEntry station;
            public readonly NetworkTableEntry controlWord;

            public MatchDataSender(NetworkTableInstance inst)
            {
                table = inst.GetTable("FMSInfo");
                typeMetadata = table.GetEntry(".type");
                typeMetadata.ForceSetString("FMSInfo");
                gameSpecificMessage = table.GetEntry("GameSpecificMessage");
                gameSpecificMessage.ForceSetString("");
                eventName = table.GetEntry("EventName");
                eventName.ForceSetString("");
                matchNumber = table.GetEntry("MatchNumber");
                matchNumber.ForceSetDouble(0);
                replayNumber = table.GetEntry("ReplayNumber");
                replayNumber.ForceSetDouble(0);
                matchType = table.GetEntry("MatchType");
                matchType.ForceSetDouble(0);
                alliance = table.GetEntry("IsRedAlliance");
                alliance.ForceSetBoolean(true);
                station = table.GetEntry("StationNumber");
                station.ForceSetDouble(1);
                controlWord = table.GetEntry("FMSControlData");
                controlWord.ForceSetDouble(0);
            }
        }

        private static readonly TimeSpan JoystickUnpluggedMessageInterval = TimeSpan.FromSeconds(1);
        public static readonly int NumJoystickPorts = 6;
        public static readonly int MaxJoystickAxes = 12;
        public static readonly int MaxJoystickPOVs = 12;


        private bool m_userInDisabled = false;
        private bool m_userInAutonomous = false;
        private bool m_userInTeleop = false;
        private bool m_userInTest = false;
        private readonly MatchDataSender m_matchDataSender;

        private DriverStation()
        {
            m_waitForDataCounter = 0;
            m_matchDataSender = new MatchDataSender(NetworkTableInstance.Default);

            m_dsThread = new Thread(Run)
            {
                Name = "Driver Station",
                IsBackground = true
            };
            m_dsThread.Start();
        }

        private void ValidateJoystickPort(int stick)
        {
            if (stick < 0 || stick > NumJoystickPorts)
            {
                throw new ArgumentOutOfRangeException(nameof(stick), $"Joystick index is out of range, should be 0-5");
            }
        }

        private bool ValidateButtonIndexValid(int button)
        {
            if (button <= 0)
            {
                ReportJoystickUnpluggedError("Button indexes begin at 1 in WPILib for C#");
                return false;
            }
            return true;
        }

        private bool ValidateAxisIndexValid(int axis)
        {
            if (axis < 0 || axis > MaxJoystickAxes)
            {
                ReportJoystickUnpluggedError("Joystick axis is out of range");
                return false;
            }
            return true;
        }

        private bool ValidatePOVIndexValid(int axis)
        {
            if (axis < 0 || axis > MaxJoystickPOVs)
            {
                ReportJoystickUnpluggedError("Joystick pov is out of range");
                return false;
            }
            return true;
        }

        private bool ValidateButtonIsFound(int button, int count, int stick)
        {
            if (button >= count)
            {
                ReportJoystickUnpluggedWarning("Joystick Button " + button + " on port " + stick
            + " not available, check if controller is plugged in");
                return false;
            }
            return true;
        }

        private bool ValidateAxisIsFound(int axis, int count, int stick)
        {
            if (axis > count)
            {
                ReportJoystickUnpluggedWarning("Joystick axis " + axis + " on port " + stick
            + " not available, check if controller is plugged in");
                return false;
            }
            return true;
        }

        private bool ValidatePOVIsFound(int pov, int count, int stick)
        {
            if (pov > count)
            {
                ReportJoystickUnpluggedWarning("Joystick axis " + pov + " on port " + stick
            + " not available, check if controller is plugged in");
                return false;
            }
            return true;
        }

        public bool GetStickButton(int stick, int button)
        {
            ValidateJoystickPort(stick);
            if (!ValidateButtonIndexValid(button))
            {
                return false;
            }

            var buttons = Hal.DriverStation.GetJoystickButtons(stick);

            if (!ValidateButtonIsFound(button, buttons.Count, stick))
            {
                return false;
            }

            return buttons.GetButton(button);
        }

        public bool GetStickButtonPressed(int stick, int button)
        {
            ValidateJoystickPort(stick);
            if (!ValidateButtonIndexValid(button))
            {
                return false;
            }

            var buttons = Hal.DriverStation.GetJoystickButtons(stick);

            if (!ValidateButtonIsFound(button, buttons.Count, stick))
            {
                return false;
            }

            lock (m_buttonEdgeMutex)
            {
                if ((m_joystickButtonsPressed[stick] & 1u << (button - 1)) != 0)
                {
                    m_joystickButtonsPressed[stick] &= ~(1u << (button - 1));
                    return true;
                }
                return false;
            }
        }

        public bool GetStickButtonReleased(int stick, int button)
        {
            ValidateJoystickPort(stick);
            if (!ValidateButtonIndexValid(button))
            {
                return false;
            }

            var buttons = Hal.DriverStation.GetJoystickButtons(stick);

            if (!ValidateButtonIsFound(button, buttons.Count, stick))
            {
                return false;
            }

            lock (m_buttonEdgeMutex)
            {
                if ((m_joystickButtonsReleased[stick] & 1u << (button - 1)) != 0)
                {
                    m_joystickButtonsReleased[stick] &= ~(1u << (button - 1));
                    return true;
                }
                return false;
            }
        }

        public double GetStickAxis(int stick, int axis)
        {
            ValidateJoystickPort(stick);
            if (!ValidateAxisIndexValid(axis))
            {
                return 0.0;
            }

            var axes = Hal.DriverStation.GetJoystickAxes(stick);

            if (!ValidateAxisIsFound(axis, axes.Count, stick))
            {
                return 0.0;
            }

            unsafe
            {
                return axes.Axes[axis];
            }
        }

        public int GetStickPOV(int stick, int pov)
        {
            ValidateJoystickPort(stick);
            if (!ValidatePOVIndexValid(pov))
            {
                return -1;
            }

            var povs = Hal.DriverStation.GetJoystickPOVs(stick);

            if (!ValidateAxisIsFound(pov, povs.Count, stick))
            {
                return -1;
            }

            unsafe
            {
                return povs.POVs[pov];
            }
        }

        public uint GetStickButtons(int stick)
        {
            ValidateJoystickPort(stick);

            return Hal.DriverStation.GetJoystickButtons(stick).Buttons;
        }

        public int GetStickAxisCount(int stick)
        {
            ValidateJoystickPort(stick);

            return Hal.DriverStation.GetJoystickAxes(stick).Count;
        }

        public int GetStickPOVCount(int stick)
        {
            ValidateJoystickPort(stick);

            return Hal.DriverStation.GetJoystickPOVs(stick).Count;
        }

        public int GetStickButtonCount(int stick)
        {
            ValidateJoystickPort(stick);

            return Hal.DriverStation.GetJoystickButtons(stick).Count;
        }

        public bool GetJoystickIsXbox(int stick)
        {
            ValidateJoystickPort(stick);

            return Hal.DriverStation.GetJoystickIsXbox(stick);
        }

        public int GetJoystickType(int stick)
        {
            ValidateJoystickPort(stick);

            return Hal.DriverStation.GetJoystickType(stick);
        }

        public string GetJoystickName(int stick)
        {
            ValidateJoystickPort(stick);

            return Hal.DriverStation.GetJoystickName(stick);
        }

        public int GetJoystickAxisType(int stick, int axis)
        {
            ValidateJoystickPort(stick);

            if (!ValidateAxisIndexValid(axis))
            {
                return 0;
            }

            return Hal.DriverStation.GetJoystickAxisType(stick, axis);
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

        public string GameSpecificMessage
        {
            get
            {
                unsafe
                {
                    var matchInfo = Hal.DriverStation.GetMatchInfo();
                    return UTF8String.ReadUTF8String(matchInfo.GameSpecificMessage, matchInfo.GameSpecificMessageSize);
                }
            }
        }

        public string EventName
        {
            get
            {
                unsafe
                {
                    var matchInfo = Hal.DriverStation.GetMatchInfo();
                    return UTF8String.ReadUTF8String(matchInfo.EventName);
                }
            }
        }

        public MatchType MatchType => (MatchType)Hal.DriverStation.GetMatchInfo().MatchType;

        public int MatchNumber => Hal.DriverStation.GetMatchInfo().MatchNumber;
        public int ReplayNumber => Hal.DriverStation.GetMatchInfo().ReplayNumber;
        public Alliance Alliance
        {
            get
            {
                var allianceId = Hal.DriverStation.GetAllianceStation();
                switch (allianceId)
                {
                    case AllianceStationID.kRed1:
                    case AllianceStationID.kRed2:
                    case AllianceStationID.kRed3:
                        return Alliance.kRed;
                    case AllianceStationID.kBlue1:
                    case AllianceStationID.kBlue2:
                    case AllianceStationID.kBlue3:
                        return Alliance.kBlue;
                    default:
                        return Alliance.kInvalid;
                }
            }
        }
        public int Location
        {
            get
            {
                var allianceId = Hal.DriverStation.GetAllianceStation();
                switch (allianceId)
                {
                    case AllianceStationID.kRed1:
                    case AllianceStationID.kBlue1:
                        return 1;
                    case AllianceStationID.kRed2:
                    case AllianceStationID.kBlue2:
                        return 2;
                    case AllianceStationID.kRed3:
                    case AllianceStationID.kBlue3:
                        return 3;
                    default:
                        return 0;
                }
            }
        }

        public void WaitForData()
        {
            WaitForData(TimeSpan.Zero);
        }


        public bool WaitForData(TimeSpan timeout)
        {
            var timeoutTime = Timer.FPGATimestamp + timeout;

            lock (m_waitForDataMutex)
            {
                int currentCount = m_waitForDataCounter;
                while (m_waitForDataCounter == currentCount)
                {
                    if (timeout != TimeSpan.Zero)
                    {
                        var currentTime = Timer.FPGATimestamp;
                        if (currentTime >= timeoutTime)
                        {
                            return false;
                        }

                        var timeToWait = timeoutTime - currentTime;
                        if (!Monitor.Wait(m_waitForDataMutex, timeToWait))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        Monitor.Wait(m_waitForDataMutex);
                    }
                }
                return true;
            }
        }

        public TimeSpan MatchTime => TimeSpan.FromSeconds(Hal.DriverStation.GetMatchTime());
        public ElectricPotential BatteryVoltage => ElectricPotential.FromVolts(Hal.Power.GetVinVoltage());

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
            lock (m_waitForDataMutex)
            {
                m_waitForDataCounter++;
                Monitor.PulseAll(m_waitForDataMutex);
            }
        }

        protected void GetData()
        {
            lock (m_buttonEdgeMutex)
            {
                for (int i = 0; i < NumJoystickPorts; i++)
                {
                    var currentButtons = Hal.DriverStation.GetJoystickButtons(i);

                    m_joystickButtonsPressed[i] |= ~m_previousButtonStates[i].Buttons & currentButtons.Buttons;

                    m_joystickButtonsReleased[i] |= m_previousButtonStates[i].Buttons & ~currentButtons.Buttons;

                    m_previousButtonStates[i] = currentButtons;
                }
            }

            WakeupWaitForData();
            SendMatchData();
        }


        private void ReportJoystickUnpluggedError(string message)
        {
            var currentTime = Timer.FPGATimestamp;
            if (currentTime > m_nextMessageTime)
            {
                ReportError(message, false);
                m_nextMessageTime = currentTime + JoystickUnpluggedMessageInterval;
            }
        }

        private void ReportJoystickUnpluggedWarning(string message)
        {
            var currentTime = Timer.FPGATimestamp;
            if (currentTime > m_nextMessageTime)
            {
                ReportWarning(message, false);
                m_nextMessageTime = currentTime + JoystickUnpluggedMessageInterval;
            }
        }

        private void Run()
        {
            m_isRunning = true;
            int safetyCounter = 0;
            while (m_isRunning)
            {
                Hal.DriverStation.WaitForDSData();
                GetData();

                if (IsDisabled)
                {
                    safetyCounter = 0;
                }

                if (++safetyCounter >= 4)
                {
                    MotorSafety.CheckMotors();
                    safetyCounter = 0;
                }

                if (m_userInDisabled) Hal.DriverStation.ObserveUserProgramDisabled();
                if (m_userInAutonomous) Hal.DriverStation.ObserveUserProgramAutonomous();
                if (m_userInTeleop) Hal.DriverStation.ObserveUserProgramTeleop();
                if (m_userInTest) Hal.DriverStation.ObserveUserProgramTest();
            }
        }

        private unsafe void SendMatchData()
        {
            var alliance = Hal.DriverStation.GetAllianceStation();
            bool isRedAlliance;
            int stationNumber;
            switch (alliance)
            {
                case AllianceStationID.kBlue1:
                    isRedAlliance = false;
                    stationNumber = 1;
                    break;
                case AllianceStationID.kBlue2:
                    isRedAlliance = false;
                    stationNumber = 2;
                    break;
                case AllianceStationID.kBlue3:
                    isRedAlliance = false;
                    stationNumber = 3;
                    break;
                case AllianceStationID.kRed1:
                    isRedAlliance = true;
                    stationNumber = 1;
                    break;
                case AllianceStationID.kRed2:
                    isRedAlliance = true;
                    stationNumber = 2;
                    break;
                default:
                    isRedAlliance = true;
                    stationNumber = 3;
                    break;
            }

            var tmpDataStore = Hal.DriverStation.GetMatchInfo();

            m_matchDataSender.alliance.SetBoolean(isRedAlliance);
            m_matchDataSender.station.SetDouble(stationNumber);
            int count = 0;
            var lenToCheck = tmpDataStore.EventName;
            while (count < 64)
            {
                if (lenToCheck[count] == 0)
                {
                    break;
                }
                count++;
            }
            m_matchDataSender.eventName.SetStringDirect(tmpDataStore.EventName, count);
            m_matchDataSender.gameSpecificMessage.SetStringDirect(tmpDataStore.GameSpecificMessage, tmpDataStore.GameSpecificMessageSize);

            m_matchDataSender.matchNumber.SetDouble(tmpDataStore.MatchNumber);
            m_matchDataSender.replayNumber.SetDouble(tmpDataStore.ReplayNumber);
            m_matchDataSender.matchType.SetDouble((int)tmpDataStore.MatchType);

            var ctlWord = Hal.DriverStation.GetControlWord();
            m_matchDataSender.controlWord.SetDouble(ctlWord.Word);
        }

        public void Dispose()
        {
            m_isRunning = false;
            Hal.DriverStation.ReleaseDSMutex();
            m_dsThread.Join();
        }

        private readonly object m_buttonEdgeMutex = new object();
        private JoystickButtons[] m_previousButtonStates = new JoystickButtons[6];
        private uint[] m_joystickButtonsPressed = new uint[6];
        private uint[] m_joystickButtonsReleased = new uint[6];

        private readonly Thread m_dsThread;
        private bool m_isRunning = false;

        private readonly object m_waitForDataMutex = new object();
        private int m_waitForDataCounter = 0;
        private TimeSpan m_nextMessageTime = TimeSpan.Zero;



    }
}
