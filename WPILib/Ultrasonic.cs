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
        private static readonly List<Ultrasonic> currentSensors = new List<Ultrasonic>();
        private static bool automaticRoundRobinEnabled = false;
        private static BackgroundWorker autoSensingWorker = null;
        private static byte instances;

        private DigitalInput echoChannel = null;
        private DigitalOutput pingChannel = null;
        private bool allocatedChannels;
        private Counter counter = null;

        private object syncRoot = new object();

        private static void GetUltrasonicChecker(object sender, EventArgs args)
        {
            while (automaticRoundRobinEnabled)
            {
                foreach (var sensor in currentSensors)
                {
                    if (sensor.Enabled)
                        sensor.pingChannel.Pulse(sensor.pingChannel.GetChannel(), PingTime);
                }
                Timer.Delay(.1);
            }
        }

        private void Initialize()
        {
            lock (syncRoot)
            {
                bool originalMode = automaticRoundRobinEnabled;
                SetAutomaticMode(false);
                counter = new Counter();
                counter.SetMaxPeriod(1.0);
                counter.SetSemiPeriodMode(true);
                counter.Reset();
                Enabled = true;
                currentSensors.Add(this);
                SetAutomaticMode(originalMode);
                ++instances;
                HAL_Base.HAL.Report(HAL_Base.ResourceType.kResourceType_Ultrasonic, instances);
                //TODO: Add to LiveWindow
            }
        }

        public Ultrasonic(int pingChannel, int echoChannel, Unit units = Unit.Inches)
        {
            this.pingChannel = new DigitalOutput(pingChannel);
            this.echoChannel = new DigitalInput(echoChannel);
            allocatedChannels = true;
            DistanceUnits = units;
            Initialize();
        }

        public Ultrasonic(DigitalOutput pingChannel, DigitalInput echoChannel, Unit units = Unit.Inches)
        {
            if (pingChannel == null) throw new ArgumentNullException("pingChannel");
            if (echoChannel == null) throw new ArgumentNullException("echoChannel");
            this.pingChannel = pingChannel;
            this.echoChannel = echoChannel;
            allocatedChannels = false;
            DistanceUnits = units;
            Initialize();
        }

        public override void Free()
        {
            base.Free();
            lock (syncRoot)
            {
                bool previousAutoMode = automaticRoundRobinEnabled;
                SetAutomaticMode(false);
                if (allocatedChannels)
                {
                    if (pingChannel != null) pingChannel.Free();
                    if (echoChannel != null) echoChannel.Free();
                }
                if (counter != null) counter.Free();
                pingChannel = null;
                echoChannel = null;
                counter = null;
                currentSensors.Remove(this);
                if (!currentSensors.Any()) return;
                SetAutomaticMode(previousAutoMode);
            }
        }

        public static void SetAutomaticMode(bool enabling)
        {
            if (enabling == automaticRoundRobinEnabled) return;
            automaticRoundRobinEnabled = enabling;
            if (enabling)
            {
                currentSensors.ForEach(s => s.counter.Reset());
                autoSensingWorker = new BackgroundWorker();
                autoSensingWorker.DoWork += GetUltrasonicChecker;
                autoSensingWorker.RunWorkerAsync();
            }
            else
            {
                while (autoSensingWorker.IsBusy)
                {
                    Timer.Delay(MaxUltrasonicTime * 1.5);
                }
                currentSensors.ForEach(s => s.counter.Reset());
            }
        }

        public static bool AutomaticModeEnabled
        {
            get { return automaticRoundRobinEnabled; }
            set { SetAutomaticMode(value); }
        }

        public void Ping()
        {
            SetAutomaticMode(false);
            counter.Reset();
            pingChannel.Pulse(pingChannel.GetChannel(), PingTime);
        }

        public bool IsRangeValid()
        {
            return counter.Get() > 1;
        }

        public double GetRangeInches()
        {
            if (IsRangeValid()) return counter.GetPeriod() * SpeedOfSoundInchesPerSec / 2;
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

        private ITable table;

        public void InitTable(ITable subtable)
        {
            table = subtable;
            UpdateTable();
        }

        public ITable GetTable()
        {
            return table;
        }

        public void UpdateTable()
        {
            if (table != null) table.PutNumber("Value", GetRangeInches());
        }

        public void StartLiveWindowMode() { }
        public void StopLiveWindowMode() { }
    }
}
