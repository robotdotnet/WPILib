using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HAL_Base;
using NetworkTablesDotNet.Tables;
using WPILib.LiveWindows;

//using System.Linq;

namespace WPILib
{
    public class Ultrasonic : SensorBase, IPIDSource, ILiveWindowSendable
    {
        public enum Unit
        {
            Inches = 0,
            Millimeters = 1
        }

        private const float PingTime = 10e-6f;
        private const double MaxUltrasonicTime = 0.1;
        private const double SpeedOfSoundInchesPerSec = 1130.0 * 12.0;
        private static readonly List<Ultrasonic> s_currentSensors = new List<Ultrasonic>();
        private static bool s_automaticRoundRobinEnabled = false;
        private static BackgroundWorker s_autoSensingWorker = null;
        private static byte s_instances;

        private DigitalInput m_echoChannel = null;
        private DigitalOutput m_pingChannel = null;
        private bool m_allocatedChannels;
        private Counter m_counter = null;

        private object m_syncRoot = new object();

        private static void GetUltrasonicChecker(object sender, EventArgs args)
        {
            while (s_automaticRoundRobinEnabled)
            {
                foreach (var sensor in s_currentSensors)
                {
                    if (sensor.Enabled)
                        sensor.m_pingChannel.Pulse(PingTime);
                }
                Timer.Delay(.1);
            }
        }

        private void Initialize()
        {
            lock (m_syncRoot)
            {
                bool originalMode = s_automaticRoundRobinEnabled;
                SetAutomaticMode(false);
                m_counter = new Counter {MaxPeriod = 1.0};
                m_counter.SetSemiPeriodMode(true);
                m_counter.Reset();
                Enabled = true;
                s_currentSensors.Add(this);
                SetAutomaticMode(originalMode);
                ++s_instances;
                HAL.Report(ResourceType.kResourceType_Ultrasonic, s_instances);
                //TODO: Add to LiveWindow
            }
        }

        public Ultrasonic(int pingChannel, int echoChannel, Unit units = Unit.Inches)
        {
            m_pingChannel = new DigitalOutput(pingChannel);
            m_echoChannel = new DigitalInput(echoChannel);
            m_allocatedChannels = true;
            DistanceUnits = units;
            Initialize();
        }

        public Ultrasonic(DigitalOutput pingChannel, DigitalInput echoChannel, Unit units = Unit.Inches)
        {
            if (pingChannel == null) throw new ArgumentNullException(nameof(pingChannel));
            if (echoChannel == null) throw new ArgumentNullException(nameof(echoChannel));
            m_pingChannel = pingChannel;
            m_echoChannel = echoChannel;
            m_allocatedChannels = false;
            DistanceUnits = units;
            Initialize();
        }

        public override void Dispose()
        {
            base.Dispose();
            lock (m_syncRoot)
            {
                bool previousAutoMode = s_automaticRoundRobinEnabled;
                SetAutomaticMode(false);
                if (m_allocatedChannels)
                {
                    m_pingChannel?.Dispose();
                    m_echoChannel?.Dispose();
                }
                m_counter?.Dispose();
                m_pingChannel = null;
                m_echoChannel = null;
                m_counter = null;
                s_currentSensors.Remove(this);
                if (!s_currentSensors.Any()) return;
                SetAutomaticMode(previousAutoMode);
            }
        }

        public static void SetAutomaticMode(bool enabling)
        {
            if (enabling == s_automaticRoundRobinEnabled) return;
            s_automaticRoundRobinEnabled = enabling;
            if (enabling)
            {
                s_currentSensors.ForEach(s => s.m_counter.Reset());
                s_autoSensingWorker = new BackgroundWorker();
                s_autoSensingWorker.DoWork += GetUltrasonicChecker;
                s_autoSensingWorker.RunWorkerAsync();
            }
            else
            {
                while (s_autoSensingWorker.IsBusy)
                {
                    Timer.Delay(MaxUltrasonicTime * 1.5);
                }
                s_currentSensors.ForEach(s => s.m_counter.Reset());
            }
        }

        public static bool AutomaticModeEnabled
        {
            get { return s_automaticRoundRobinEnabled; }
            set { SetAutomaticMode(value); }
        }

        public void Ping()
        {
            SetAutomaticMode(false);
            m_counter.Reset();
            m_pingChannel.Pulse(PingTime);
        }

        public bool IsRangeValid()
        {
            return m_counter.Get() > 1;
        }

        public double GetRangeInches()
        {
            if (IsRangeValid()) return m_counter.Period * SpeedOfSoundInchesPerSec / 2;
            return 0;
        }

        public double GetRangeMM()
        {
            return GetRangeInches() * 25.4;
        }

        public double PidGet()
        {
            switch (DistanceUnits)
            {
                case Unit.Inches:
                    return GetRangeInches();
                case Unit.Millimeters:
                    return GetRangeMM();
                default:
                    return 0.0;
            }
        }

        [Obsolete("Use DistanceUnits property instead.")]
        public void SetDistanceUnits(Unit units)
        {
            DistanceUnits = units;
        }

        [Obsolete("Use DistanceUnits property instead.")]
        public Unit GetDistanceUnits() { return DistanceUnits; }

        public Unit DistanceUnits { get; set; }

        [Obsolete("Use Enabled property instead.")]
        public bool GetEnabled() { return Enabled; }

        [Obsolete("Use Enabled property instead.")]
        public void SetEnabled(bool enabled) { Enabled = enabled; }
        
        public bool Enabled { get; set; }

        public string SmartDashboardType => "Ultrasonic";

        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        public ITable Table { get; private set; }

        public void UpdateTable()
        {
            Table?.PutNumber("Value", GetRangeInches());
        }

        public void StartLiveWindowMode() { }
        public void StopLiveWindowMode() { }
    }
}
