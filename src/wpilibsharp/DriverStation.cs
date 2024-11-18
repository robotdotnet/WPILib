using CommunityToolkit.Diagnostics;
using NetworkTables;
using UnitsNet;
using UnitsNet.NumberExtensions.NumberToDuration;
using WPIHal;
using WPIHal.Natives;
using WPIUtil;
using WPIUtil.Concurrent;
using WPIUtil.Handles;
using WPIUtil.Logging;
using WPIUtil.Natives;
using MatchType = WPIHal.MatchType;

namespace WPILib;

public static class DriverStation
{
    private static readonly Duration JoystickUnpluggedMessageInterval = 1.0.Seconds();
    private static Duration m_nextMessageTime = 0.Seconds();

    public enum AllianceColor
    {
        Red,
        Blue
    }

    private sealed class MatchDataSender
    {
        NetworkTable table;
        IStringPublisher typeMetadata;
        IStringPublisher gameSpecificMessage;
        IStringPublisher eventName;
        IIntegerPublisher matchNumber;
        IIntegerPublisher replayNumber;
        IIntegerPublisher matchType;
        IBooleanPublisher alliance;
        IIntegerPublisher station;
        IIntegerPublisher controlWord;
        bool oldIsRedAlliance = true;
        int oldStationNumber = 1;
        string oldEventName = "";
        string oldGameSpecificMessage = "";
        int oldMatchNumber;
        int oldReplayNumber;
        int oldMatchType;
        uint oldControlWord;

        public MatchDataSender()
        {
            table = NetworkTableInstance.Default.GetTable("FMSInfo");
            typeMetadata = table.GetStringTopic(".type").Publish();
            typeMetadata.Set("FMSInfo");
            gameSpecificMessage = table.GetStringTopic("GameSpecificMessage").Publish();
            gameSpecificMessage.Set("");
            eventName = table.GetStringTopic("EventName").Publish();
            eventName.Set("");
            matchNumber = table.GetIntegerTopic("MatchNumber").Publish();
            matchNumber.Set(0);
            replayNumber = table.GetIntegerTopic("ReplayNumber").Publish();
            replayNumber.Set(0);
            matchType = table.GetIntegerTopic("MatchType").Publish();
            matchType.Set(0);
            alliance = table.GetBooleanTopic("IsRedAlliance").Publish();
            alliance.Set(true);
            station = table.GetIntegerTopic("StationNumber").Publish();
            station.Set(1);
            controlWord = table.GetIntegerTopic("FMSControlData").Publish();
            controlWord.Set(0);
        }

        public void SendMatchData()
        {
            var allianceID = HalDriverStation.GetAllianceStation();

            bool isRedAlliance;
            int stationNumber;
            switch (allianceID)
            {
                case AllianceStationID.Blue1:
                    isRedAlliance = false;
                    stationNumber = 1;
                    break;
                case AllianceStationID.Blue2:
                    isRedAlliance = false;
                    stationNumber = 2;
                    break;
                case AllianceStationID.Blue3:
                    isRedAlliance = false;
                    stationNumber = 3;
                    break;
                case AllianceStationID.Red1:
                    isRedAlliance = true;
                    stationNumber = 1;
                    break;
                case AllianceStationID.Red2:
                    isRedAlliance = true;
                    stationNumber = 2;
                    break;
                default:
                    isRedAlliance = true;
                    stationNumber = 3;
                    break;
            }

            string currentEventName;
            string currentGameSpecificMessage;
            int currentMatchNumber;
            int currentReplayNumber;
            int currentMatchType;
            uint currentControlWord;

            lock (m_cacheDataMutex)
            {
                currentEventName = m_matchInfo.EventName;
                currentGameSpecificMessage = m_matchInfo.GameSpecificMessage;
                currentMatchNumber = m_matchInfo.MatchNumber;
                currentReplayNumber = m_matchInfo.ReplayNumber;
                currentMatchType = (int)m_matchInfo.MatchType;
            }

            unsafe
            {
                HalDriverStation.GetControlWordNative(&currentControlWord);
            }

            if (oldIsRedAlliance != isRedAlliance)
            {
                alliance.Set(isRedAlliance);
                oldIsRedAlliance = isRedAlliance;
            }
            if (oldStationNumber != stationNumber)
            {
                station.Set(stationNumber);
                oldStationNumber = stationNumber;
            }
            if (oldEventName != currentEventName)
            {
                eventName.Set(currentEventName);
                oldEventName = currentEventName;
            }
            if (oldGameSpecificMessage != currentGameSpecificMessage)
            {
                gameSpecificMessage.Set(currentGameSpecificMessage);
                oldGameSpecificMessage = currentGameSpecificMessage;
            }
            if (currentMatchNumber != oldMatchNumber)
            {
                matchNumber.Set(currentMatchNumber);
                oldMatchNumber = currentMatchNumber;
            }
            if (currentReplayNumber != oldReplayNumber)
            {
                replayNumber.Set(currentReplayNumber);
                oldReplayNumber = currentReplayNumber;
            }
            if (currentMatchType != oldMatchType)
            {
                matchType.Set(currentMatchType);
                oldMatchType = currentMatchType;
            }
            if (currentControlWord != oldControlWord)
            {
                controlWord.Set(currentControlWord);
                oldControlWord = currentControlWord;
            }

        }
    }

    private sealed class JoystickLogSender
    {
        readonly int m_stick;
        JoystickButtons m_prevButtons;
        JoystickAxes m_prevAxes;
        JoystickPOVs m_prevPOVs;
        readonly BooleanArrayLogEntry m_logButtons;
        readonly FloatArrayLogEntry m_logAxes;
        readonly IntegerArrayLogEntry m_logPOVs;

        public JoystickLogSender(DataLog log, int stick, long timestamp)
        {
            m_stick = stick;

            m_logButtons = new BooleanArrayLogEntry(log, $"DS:joystick{stick}/buttons", timestamp: timestamp);
            m_logAxes = new FloatArrayLogEntry(log, $"DS:joystick{stick}/axes", timestamp: timestamp);
            m_logPOVs = new IntegerArrayLogEntry(log, $"DS:joystick{stick}/povs", timestamp: timestamp);

            AppendButtons(m_joystickButtons[m_stick], timestamp);
            AppendAxes(in m_joystickAxes[m_stick], timestamp);
            AppendPOVs(in m_joystickPOVs[m_stick], timestamp);
        }

        public void Send(long timestamp)
        {
            var buttons = m_joystickButtons[m_stick];
            if (!buttons.IsEqual(m_prevButtons))
            {
                AppendButtons(buttons, timestamp);
            }

            // Ref due to copy size
            ref var axes = ref m_joystickAxes[m_stick];
            if (!axes.IsEqual(in m_prevAxes))
            {
                AppendAxes(in axes, timestamp);
            }

            // Ref due to copy size
            ref var povs = ref m_joystickPOVs[m_stick];
            if (!povs.IsEqual(in m_prevPOVs))
            {
                AppendPOVs(in povs, timestamp);
            }
        }

        void AppendButtons(JoystickButtons buttons, long timestamp)
        {
            var count = buttons.Count;
            Span<bool> buttonSpan = stackalloc bool[count];

            m_logButtons.Append(buttons.ToSpan(buttonSpan), timestamp);
            m_prevButtons = buttons;
        }

        void AppendAxes(ref readonly JoystickAxes axes, long timestamp)
        {
            ReadOnlySpan<float> axesSpan = axes.AxesSpan;
            m_logAxes.Append(axesSpan, timestamp);
            m_prevAxes = axes;
        }

        void AppendPOVs(ref readonly JoystickPOVs povs, long timestamp)
        {
            ReadOnlySpan<short> povSpan = povs.PovsSpan;
            Span<long> longSpan = stackalloc long[povSpan.Length];
            for (int i = 0; i < povSpan.Length; i++)
            {
                longSpan[i] = povSpan[i];
            }
            m_logPOVs.Append(longSpan, timestamp);
            m_prevPOVs = povs;
        }
    }

    private sealed class DataLogSender
    {
        public DataLogSender(DataLog log, bool logJoysticks, long timestamp)
        {
            m_logEnabled = new BooleanLogEntry(log, "DS:enabled", timestamp: timestamp);
            m_logAutonomous = new BooleanLogEntry(log, "DS:autonomous", timestamp: timestamp);
            m_logTest = new BooleanLogEntry(log, "DS:test", timestamp: timestamp);
            m_logEstop = new BooleanLogEntry(log, "DS:estop", timestamp: timestamp);

            m_wasEnabled = m_controlWordCache.Enabled;
            m_wasAutonomous = m_controlWordCache.Autonomous;
            m_wasTest = m_controlWordCache.Test;
            m_wasEstop = m_controlWordCache.EStop;

            m_logEnabled.Append(m_wasEnabled, timestamp);
            m_logAutonomous.Append(m_wasAutonomous, timestamp);
            m_logTest.Append(m_wasTest, timestamp);
            m_logEstop.Append(m_wasEstop, timestamp);

            if (logJoysticks)
            {
                m_joysticks = new JoystickLogSender[NumJoystickPorts];
                for (int i = 0; i < NumJoystickPorts; i++)
                {
                    m_joysticks[i] = new(log, i, timestamp);
                }
            }
            else
            {
                m_joysticks = [];
            }
        }

        public void Send(long timestamp)
        {
            // append control word value changes
            bool enabled = m_controlWordCache.Enabled;
            if (enabled != m_wasEnabled)
            {
                m_logEnabled.Append(enabled, timestamp);
            }
            m_wasEnabled = enabled;

            bool autonomous = m_controlWordCache.Autonomous;
            if (autonomous != m_wasAutonomous)
            {
                m_logAutonomous.Append(autonomous, timestamp);
            }
            m_wasAutonomous = autonomous;

            bool test = m_controlWordCache.Test;
            if (test != m_wasTest)
            {
                m_logTest.Append(test, timestamp);
            }
            m_wasTest = test;

            bool estop = m_controlWordCache.EStop;
            if (estop != m_wasEstop)
            {
                m_logEstop.Append(estop, timestamp);
            }
            m_wasEstop = estop;

            // append joystick value changes
            foreach (JoystickLogSender joystick in m_joysticks)
            {
                joystick.Send(timestamp);
            }
        }

        bool m_wasEnabled;
        bool m_wasAutonomous;
        bool m_wasTest;
        bool m_wasEstop;

        readonly BooleanLogEntry m_logEnabled;
        readonly BooleanLogEntry m_logAutonomous;
        readonly BooleanLogEntry m_logTest;
        readonly BooleanLogEntry m_logEstop;

        readonly JoystickLogSender[] m_joysticks;
    }

    private const int NumJoystickPorts = 6;

    private static JoystickAxes[] m_joystickAxes = new JoystickAxes[NumJoystickPorts];
    private static JoystickPOVs[] m_joystickPOVs = new JoystickPOVs[NumJoystickPorts];
    private static JoystickButtons[] m_joystickButtons = new JoystickButtons[NumJoystickPorts];
    private static MatchInfo m_matchInfo;
    private static ControlWord m_controlWord;

    private static JoystickAxes[] m_joystickAxesCache = new JoystickAxes[NumJoystickPorts];
    private static JoystickPOVs[] m_joystickPOVsCache = new JoystickPOVs[NumJoystickPorts];
    private static JoystickButtons[] m_joystickButtonsCache = new JoystickButtons[NumJoystickPorts];
    private static MatchInfo m_matchInfoCache;
    private static ControlWord m_controlWordCache;

    private static readonly EventVector m_refreshEvents = new();

#pragma warning disable IDE0052 // Remove unread private members
    private static readonly int[] m_joystickButtonsPressed = new int[NumJoystickPorts];
#pragma warning restore IDE0052 // Remove unread private members
#pragma warning disable IDE0052 // Remove unread private members
    private static readonly int[] m_joystickButtonsReleased = new int[NumJoystickPorts];
#pragma warning restore IDE0052 // Remove unread private members

    private static readonly MatchDataSender m_matchDataSender;
    private static DataLogSender? m_dataLogSender;
    private static bool m_silenceJoystickConnectionWarning;
    private static readonly object m_cacheDataMutex = new();

    static DriverStation()
    {
        HalBase.Initialize();

        m_matchDataSender = new();
    }

    private static void ReportJoystickUnpluggedError(string message)
    {
        var currentTime = Timer.FPGATimestamp;
        if (currentTime > m_nextMessageTime)
        {
            ReportError(message, false);
            m_nextMessageTime = currentTime + JoystickUnpluggedMessageInterval;
        }
    }

    private static void ReportJoystickUnpluggedWarning(string message)
    {
        if (SilenceJoystickConnectionWarning)
        {
            return;
        }
        var currentTime = Timer.FPGATimestamp;
        if (currentTime > m_nextMessageTime)
        {
            ReportWarning(message, false);
            m_nextMessageTime = currentTime + JoystickUnpluggedMessageInterval;
        }
    }

    public static bool GetStickButton(int stick, int button)
    {
        if (stick is < 0 or >= NumJoystickPorts)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(stick), $"Joystick index {stick} is out of range, should be 0-5");
        }

        if (button <= 0)
        {
            ReportJoystickUnpluggedError("Button indexes begin at 1 in WPILib for C#");
            return false;
        }

        lock (m_cacheDataMutex)
        {
            var result = m_joystickButtons[stick][button];
            if (result is { } r)
            {
                return r;
            }
        }

        ReportJoystickUnpluggedWarning($"Joystick Button {button} on port {stick} not available, check if controller is plugged in");
        return false;
    }

    public static double GetStickAxis(int stick, int axis)
    {
        if (stick is < 0 or >= NumJoystickPorts)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(stick), $"Joystick index {stick} is out of range, should be 0-5");
        }

        if (axis is < 0 or >= JoystickAxes.NumJoystickAxes)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(axis), $"Joystick axis {axis} is out of range");
        }

        lock (m_cacheDataMutex)
        {
            var result = m_joystickAxes[stick][axis];
            if (result is { } r)
            {
                return r;
            }
        }

        ReportJoystickUnpluggedWarning($"Joystick Axis {axis} on port {stick} not available, check if controller is plugged in");
        return 0.0;
    }

    public static int GetStickPOV(int stick, int pov)
    {
        if (stick is < 0 or >= NumJoystickPorts)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(stick), $"Joystick index {stick} is out of range, should be 0-5");
        }

        if (pov is < 0 or >= JoystickPOVs.NumJoystickPOVs)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(pov), $"Joystick pov {pov} is out of range");
        }

        lock (m_cacheDataMutex)
        {
            var result = m_joystickPOVs[stick][pov];
            if (result is { } r)
            {
                return r;
            }
        }

        ReportJoystickUnpluggedWarning($"Joystick POV {pov} on port {stick} not available, check if controller is plugged in");
        return -1;
    }

    public static uint GetStickButtons(int stick)
    {
        if (stick is < 0 or >= NumJoystickPorts)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(stick), $"Joystick index {stick} is out of range, should be 0-5");
        }

        lock (m_cacheDataMutex)
        {
            return m_joystickButtons[stick].Buttons;
        }
    }

    public static int GetStickAxisCount(int stick)
    {
        if (stick is < 0 or >= NumJoystickPorts)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(stick), $"Joystick index {stick} is out of range, should be 0-5");
        }

        lock (m_cacheDataMutex)
        {
            return m_joystickAxes[stick].Count;
        }
    }

    public static int GetStickPOVCount(int stick)
    {
        if (stick is < 0 or >= NumJoystickPorts)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(stick), $"Joystick index {stick} is out of range, should be 0-5");
        }

        lock (m_cacheDataMutex)
        {
            return m_joystickPOVs[stick].Count;
        }
    }

    public static int GetStickButtonCount(int stick)
    {
        if (stick is < 0 or >= NumJoystickPorts)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(stick), $"Joystick index {stick} is out of range, should be 0-5");
        }

        lock (m_cacheDataMutex)
        {
            return m_joystickButtons[stick].Count;
        }
    }

    public static bool GetJoystickIsXbox(int stick)
    {
        if (stick is < 0 or >= NumJoystickPorts)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(stick), $"Joystick index {stick} is out of range, should be 0-5");
        }

        return HalDriverStation.GetJoystickIsXbox(stick);
    }

    public static int GetJoystickType(int stick)
    {
        if (stick is < 0 or >= NumJoystickPorts)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(stick), $"Joystick index {stick} is out of range, should be 0-5");
        }

        return HalDriverStation.GetJoystickType(stick);
    }

    public static string GetJoystickName(int stick)
    {
        if (stick is < 0 or >= NumJoystickPorts)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(stick), $"Joystick index {stick} is out of range, should be 0-5");
        }

        HalDriverStation.GetJoystickName(out var name, stick);
        return name;
    }

    public static int GetJoystickAxisType(int stick, int axis)
    {
        if (stick is < 0 or >= NumJoystickPorts)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException(nameof(stick), $"Joystick index {stick} is out of range, should be 0-5");
        }

        return HalDriverStation.GetJoystickAxisType(stick, axis);
    }

    public static bool IsJoystickConnected(int stick)
    {
        return GetStickAxisCount(stick) > 0 || GetStickButtonCount(stick) > 0 || GetStickPOVCount(stick) > 0;
    }

    public static bool IsEnabled
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_controlWord.Enabled && m_controlWord.DsAttached;
            }
        }
    }

    public static bool IsDisabled => !IsEnabled;

    public static bool IsEStopped
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_controlWord.EStop;
            }
        }
    }

    public static bool IsAutonomous
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_controlWord.Autonomous;
            }
        }
    }

    public static bool IsAutonomousEnabled
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_controlWord.Autonomous && m_controlWord.Enabled;
            }
        }
    }

    public static bool IsTeleop
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return !(m_controlWord.Autonomous || m_controlWord.Test);
            }
        }
    }

    public static bool IsTeleopEnabled
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return !m_controlWord.Autonomous && !m_controlWord.Test && m_controlWord.Enabled;
            }
        }
    }

    public static bool IsTest
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_controlWord.Test;
            }
        }
    }

    public static bool IsTestEnabled
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_controlWord.Test && m_controlWord.Enabled;
            }
        }
    }

    public static bool IsDSAttached
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_controlWord.DsAttached;
            }
        }
    }

    public static bool IsFMSAttached
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_controlWord.FmsAttached;
            }
        }
    }

    public static string GameSpecificMessage
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_matchInfo.GameSpecificMessage;
            }
        }
    }

    public static string EventName
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_matchInfo.EventName;
            }
        }
    }

    public static MatchType MatchType
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_matchInfo.MatchType;
            }
        }
    }

    public static int MatchNumber
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_matchInfo.MatchNumber;
            }
        }
    }

    public static int ReplayNumber
    {
        get
        {
            lock (m_cacheDataMutex)
            {
                return m_matchInfo.ReplayNumber;
            }
        }
    }

    public static AllianceColor? Alliance
    {
        get
        {
            var alliance = HalDriverStation.GetAllianceStation(out var _);
            return alliance switch
            {
                AllianceStationID.Red1 => AllianceColor.Red,
                AllianceStationID.Red2 => AllianceColor.Red,
                AllianceStationID.Red3 => AllianceColor.Red,
                AllianceStationID.Blue1 => AllianceColor.Blue,
                AllianceStationID.Blue2 => AllianceColor.Blue,
                AllianceStationID.Blue3 => AllianceColor.Blue,
                _ => null,
            };
        }
    }

    public static int? Location
    {
        get
        {
            var alliance = HalDriverStation.GetAllianceStation(out var _);
            return alliance switch
            {
                AllianceStationID.Red1 => 1,
                AllianceStationID.Red2 => 2,
                AllianceStationID.Red3 => 3,
                AllianceStationID.Blue1 => 1,
                AllianceStationID.Blue2 => 2,
                AllianceStationID.Blue3 => 3,
                _ => null,
            };
        }
    }

    public static AllianceStationID RawAllianceStation => HalDriverStation.GetAllianceStation(out var _);

    public static bool WaitForDsConnection(Duration timeout)
    {
        using WpiEvent wpiEvent = new();
        try
        {
            HalDriverStation.ProvideNewDataEventHandle(wpiEvent.Handle);
            SynchronizationResult result;
            if (timeout.Seconds <= 0)
            {
                result = Synchronization.WaitForObject(wpiEvent);
            }
            else
            {
                result = Synchronization.WaitForObject(wpiEvent, timeout);
            }
            return result == SynchronizationResult.Signaled;
        }
        finally
        {
            HalDriverStation.RemoveNewDataEventHandle(wpiEvent.Handle);
        }
    }

    public static double MatchTime => HalDriverStation.GetMatchTime(out var _);

    public static bool SilenceJoystickConnectionWarning
    {
        get => !IsFMSAttached && m_silenceJoystickConnectionWarning;
        set => m_silenceJoystickConnectionWarning = value;
    }

    public static ControlWord GetControlWordFromCache()
    {
        lock (m_cacheDataMutex)
        {
            return m_controlWord;
        }
    }

    public static void RefreshData()
    {
        HalDriverStation.RefrehshDSData();

        // Get the status of all the joysticks
        for (int stick = 0; stick < NumJoystickPorts; stick++)
        {
            HalDriverStation.GetJoystickAxes(stick, out m_joystickAxesCache[stick]);
            HalDriverStation.GetJoystickPOVs(stick, out m_joystickPOVsCache[stick]);
            HalDriverStation.GetJoystickButtons(stick, out m_joystickButtonsCache[stick]);
        }

        HalDriverStation.GetMatchInfo(out m_matchInfoCache);
        HalDriverStation.GetControlWord(out m_controlWordCache);

        DataLogSender? dataLogSender;

        // lock joystick mutex to swap cache data
        lock (m_cacheDataMutex)
        {
            // move cache to actual data
            (m_joystickAxesCache, m_joystickAxes) = (m_joystickAxes, m_joystickAxesCache);
            (m_joystickButtonsCache, m_joystickButtons) = (m_joystickButtons, m_joystickButtonsCache);
            (m_joystickPOVsCache, m_joystickPOVs) = (m_joystickPOVs, m_joystickPOVsCache);
            (m_matchInfoCache, m_matchInfo) = (m_matchInfo, m_matchInfoCache);
            (m_controlWordCache, m_controlWord) = (m_controlWord, m_controlWordCache);
            dataLogSender = m_dataLogSender;
        }

        m_refreshEvents.Wakeup();

        m_matchDataSender.SendMatchData();
        dataLogSender?.Send((long)TimestampNative.Now());
    }

    public static void ReportWarning(string _, bool __)
    {

    }

    public static void ReportError(string _, bool __)
    {

    }

    public static void ProvideRefreshedDataEventHandle(WpiEventHandle handle)
    {
        m_refreshEvents.Add(handle.Handle);
    }

    public static void RemoveRefreshedDataEventHandle(WpiEventHandle handle)
    {
        m_refreshEvents.Remove(handle.Handle);
    }

    public static void StartDataLog(DataLog log, bool logJoysticks)
    {
        lock (m_cacheDataMutex)
        {
            m_dataLogSender ??= new(log, logJoysticks, (long)TimestampNative.Now());
        }
    }

    public static void StartDataLog(DataLog log)
    {
        StartDataLog(log, true);
    }
}
