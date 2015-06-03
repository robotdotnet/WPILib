using System.Linq;
using NetworkTablesDotNet.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Linq;

namespace WPILib
{
    public class Ultrasonic : SensorBase, PIDSource, livewindow.LiveWindowSendable
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
                        sensor.m_pingChannel.Pulse(sensor.m_pingChannel.GetChannel(), PingTime);
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
                m_counter = new Counter();
                m_counter.SetMaxPeriod(1.0);
                m_counter.SetSemiPeriodMode(true);
                m_counter.Reset();
                Enabled = true;
                s_currentSensors.Add(this);
                SetAutomaticMode(originalMode);
                ++s_instances;
                HAL_Base.HAL.Report(HAL_Base.ResourceType.kResourceType_Ultrasonic, s_instances);
                //TODO: Add to LiveWindow
            }
        }

        public Ultrasonic(int pingChannel, int echoChannel, Unit units = Unit.Inches)
        {
            this.m_pingChannel = new DigitalOutput(pingChannel);
            this.m_echoChannel = new DigitalInput(echoChannel);
            m_allocatedChannels = true;
            DistanceUnits = units;
            Initialize();
        }

        public Ultrasonic(DigitalOutput pingChannel, DigitalInput echoChannel, Unit units = Unit.Inches)
        {
            if (pingChannel == null) throw new ArgumentNullException("pingChannel");
            if (echoChannel == null) throw new ArgumentNullException("echoChannel");
            this.m_pingChannel = pingChannel;
            this.m_echoChannel = echoChannel;
            m_allocatedChannels = false;
            DistanceUnits = units;
            Initialize();
        }

        public override void Free()
        {
            base.Free();
            lock (m_syncRoot)
            {
                bool previousAutoMode = s_automaticRoundRobinEnabled;
                SetAutomaticMode(false);
                if (m_allocatedChannels)
                {
                    if (m_pingChannel != null) m_pingChannel.Free();
                    if (m_echoChannel != null) m_echoChannel.Free();
                }
                if (m_counter != null) m_counter.Free();
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
            m_pingChannel.Pulse(m_pingChannel.GetChannel(), PingTime);
        }

        public bool IsRangeValid()
        {
            return m_counter.Get() > 1;
        }

        public double GetRangeInches()
        {
            if (IsRangeValid()) return m_counter.GetPeriod() * SpeedOfSoundInchesPerSec / 2;
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

        public string GetSmartDashboardType()
        {
            return "Ultrasonic";
        }

        private ITable m_table;

        public void InitTable(ITable subtable)
        {
            m_table = subtable;
            UpdateTable();
        }

        public ITable GetTable()
        {
            return m_table;
        }

        public void UpdateTable()
        {
            if (m_table != null) m_table.PutNumber("Value", GetRangeInches());
        }

        public void StartLiveWindowMode() { }
        public void StopLiveWindowMode() { }
    }
}
