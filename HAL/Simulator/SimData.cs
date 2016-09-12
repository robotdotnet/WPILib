using System;
using System.Collections.Generic;
using HAL.Simulator.Data;
using HAL.SimulatorHAL;
using static HAL.SimulatorHAL.HALPorts;


namespace HAL.Simulator
{
    /// <summary>
    /// 
    /// </summary>
    public class SimData
    {
        /// <summary>
        /// Gets the accelerometer data.
        /// </summary>
        /// <value>
        /// The accelerometer.
        /// </value>
        public static HALSimAccelerometerData Accelerometer { get; } = new HALSimAccelerometerData(0);
        /// <summary>
        /// Gets the global data.
        /// </summary>
        /// <value>
        /// The global data.
        /// </value>
        public static GlobalData GlobalData { get; } = new GlobalData();

        public static List<HALSimAnalogGyroData> AnalogGyro { get; } = new List<HALSimAnalogGyroData>();

        public static NavXData NavXData { get; } = new NavXData();
        /// <summary>
        /// Gets the analog out data.
        /// </summary>
        /// <value>
        /// The analog out data.
        /// </value>
        public static List<HALSimAnalogOutData> AnalogOut { get; } = new List<HALSimAnalogOutData>();

        /// <summary>
        /// Gets the analog in dada.
        /// </summary>
        /// <value>
        /// The analog in data.
        /// </value>
        public static List<HALSimAnalogInData> AnalogIn { get; } = new List<HALSimAnalogInData>();
        /// <summary>
        /// Gets the analog trigger.
        /// </summary>
        /// <value>
        /// The analog trigger.
        /// </value>
        public static List<AnalogTriggerData> AnalogTrigger { get; } = new List<AnalogTriggerData>();

        /// <summary>
        /// Gets the dio.
        /// </summary>
        /// <value>
        /// The dio.
        /// </value>
        public static List<HALSimDIOData> DIO { get; } = new List<HALSimDIOData>();

       // public static List<DigitalGlitchFilterData> DigitalGlitchFilter { get; } = new List<DigitalGlitchFilterData>(); 

        /// <summary>
        /// </summary>
        /// <value>
        /// The PWM.
        /// </value>
        public static List<HALSimPWMData> PWM { get; } = new List<HALSimPWMData>();

        /// <summary>
        /// Gets the digital PWM.
        /// </summary>
        /// <value>
        /// The digital PWM.
        /// </value>
        public static List<HALSimDigitalPWMData> DigitalPWM { get; } = new List<HALSimDigitalPWMData>();

        /// <summary>
        /// Gets the relay.
        /// </summary>
        /// <value>
        /// The relay.
        /// </value>
        public static List<HALSimRelayData> Relay { get; } = new List<HALSimRelayData>();

        /// <summary>
        /// Gets the counter.
        /// </summary>
        /// <value>
        /// The counter.
        /// </value>
        public static List<CounterData> Counter { get; } = new List<CounterData>();
        /// <summary>
        /// Gets the encoder.
        /// </summary>
        /// <value>
        /// The encoder.
        /// </value>
        public static List<HALSimEncoderData> Encoder { get; } = new List<HALSimEncoderData>();

        public static List<HALSimSPIAccelerometerData> SPIAccelerometer { get; } = new List<HALSimSPIAccelerometerData>();

        public static List<SPIAccumulatorData> SPIAccumulator { get; } = new List<SPIAccumulatorData>();

        private static readonly Dictionary<int, PCMData> s_pcm = new Dictionary<int, PCMData>();

        /// <summary>
        /// Gets the robo rio data.
        /// </summary>
        /// <value>
        /// The robo rio data.
        /// </value>
        public static HALSimRoboRioData RoboRioData { get; } = new HALSimRoboRioData(0);
        private static readonly Dictionary<int, PDPData> s_pdp = new Dictionary<int, PDPData>();

        private static readonly Dictionary<int, CanTalonData> s_canTalon = new Dictionary<int, CanTalonData>();

        /// <summary>
        /// Gets the driver station.
        /// </summary>
        /// <value>
        /// The driver station.
        /// </value>
        public static DriverStationData DriverStation { get; } = new DriverStationData();

        /// <summary>
        /// Gets the reports.
        /// </summary>
        /// <value>
        /// The reports.
        /// </value>
        public static NotifyDict<byte, dynamic> Reports { get; } = new NotifyDict<byte, dynamic>();

        public static List<ErrorData> ErrorList { get; } = new List<ErrorData>();

        /// <summary>
        /// Gets the can talon.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static CanTalonData GetCanTalon(int id)
        {
            CanTalonData data;
            bool retVal = s_canTalon.TryGetValue(id, out data);
            if (retVal)
            {
                //Contains key. Just return it
                return data;
            }
            else
            {
                //CAN Talon does not exist yet. Return null.
                return null;
            }
        }

        /// <summary>
        /// Initializes the can talon.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static bool InitializeCanTalon(int id)
        {
            CanTalonData data;
            bool retVal = s_canTalon.TryGetValue(id, out data);
            if (retVal)
            {
                //Contains key. return false saying we did not initialize a new one.
                return false;
            }
            else
            {
                //Create a new Can Talon data and return true.
                data = new CanTalonData();
                s_canTalon.Add(id, data);
                OnTalonSRXAddedOrRemoved?.Invoke(id, new TalonSRXEventArgs(true));
                return true;
            }
        }

        /// <summary>
        /// Removes the can talon.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public static void RemoveCanTalon(int id)
        {
            s_canTalon.Remove(id);
            OnTalonSRXAddedOrRemoved?.Invoke(id, new TalonSRXEventArgs(false));
        }

        /// <summary>
        /// Gets the PCM.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static PCMData GetPCM(int id)
        {
            PCMData data;
            bool retVal = s_pcm.TryGetValue(id, out data);
            if (retVal)
            {
                //Contains key. Just return it
                return data;
            }
            else
            {
                data = new PCMData();
                s_pcm.Add(id, data);
                OnPCMAdded?.Invoke(data, null);
                return data;
            }
        }

        /// <summary>
        /// Initializes the PCM.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static bool InitializePCM(int id)
        {
            PCMData data;
            bool retVal = s_pcm.TryGetValue(id, out data);
            if (retVal)
            {
                //Contains key. return false saying we did not initialize a new one.
                return false;
            }
            else
            {
                //Create a new PCM data and return true.
                data = new PCMData();
                s_pcm.Add(id, data);
                OnPCMAdded?.Invoke(data, null);
                return true;
            }
        }

        /// <summary>
        /// Gets the PDP.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static PDPData GetPDP(int id)
        {
            PDPData data;
            bool retVal = s_pdp.TryGetValue(id, out data);
            if (retVal)
            {
                //Contains key. Just return it
                return data;
            }
            else
            {
                data = new PDPData();
                s_pdp.Add(id, data);
                OnPDPAdded?.Invoke(data, null);
                return data;
            }
        }

        /// <summary>
        /// Initializes a new PDP.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True if new PDP was created, otherwise false</returns>
        public static bool InitializePDP(int id)
        {
            PDPData data;
            bool retVal = s_pdp.TryGetValue(id, out data);
            if (retVal)
            {
                //Contains key. return false saying we did not initialize a new one.
                return false;
            }
            else
            {
                //Create a new PCM data and return true.
                data = new PDPData();
                s_pdp.Add(id, data);
                OnPDPAdded?.Invoke(data, null);
                return true;
            }
        }

        /// <summary>
        /// Occurs on talon SRX added or removed.
        /// </summary>
        public static event EventHandler<TalonSRXEventArgs> OnTalonSRXAddedOrRemoved;
        /// <summary>
        /// Occurs when a PCM is added.
        /// </summary>
        public static event EventHandler OnPCMAdded;
        /// <summary>
        /// Occurs when a PDP is added.
        /// </summary>
        public static event EventHandler OnPDPAdded;

        static SimData()
        {
            for (int i = 0; i < kNumAccumulators; i++)
            {
                AnalogGyro.Add(new HALSimAnalogGyroData(i));
            }

            for (int i = 0; i < kNumAnalogOutputs; i++)
            {
                AnalogOut.Add(new HALSimAnalogOutData(i));
            }

            for (int i = 0; i < kNumAnalogInputs; i++)
            {
                AnalogIn.Add(new HALSimAnalogInData(i));
            }

            for (int i = 0; i < kNumAnalogTriggers; i++)
            {
                AnalogTrigger.Add(new AnalogTriggerData());
            }

            for (int i = 0; i < kNumDigitalPins; i++)
            {
                DIO.Add(new HALSimDIOData(i));
            }

            for (int i = 0; i < kNumDigitalPWMOutputs; i++)
            {
                DigitalPWM.Add(new HALSimDigitalPWMData(i));
            }

            for (int i = 0; i < 4; i++)
            {
                //DigitalGlitchFilter.Add(new DigitalGlitchFilterData());
            }

            for (int i = 0; i < kNumPWMPins; i++)
            {
                PWM.Add(new HALSimPWMData(i));
            }

            for (int i = 0; i < 4; i++)
            {
                Relay.Add(new HALSimRelayData(i));
            }

            for (int i = 0; i < 8; i++)
            {
                Counter.Add(new CounterData());
            }

            for (int i = 0; i < 8; i++)
            {
                Encoder.Add(new HALSimEncoderData(i));
            }

            for (int i = 0; i < 5; i++)
            {
                SPIAccelerometer.Add(new HALSimSPIAccelerometerData(i));
            }

            for (int i = 0; i < 5; i++)
            {
                SPIAccumulator.Add(new SPIAccumulatorData());
            }

            InitializePDP(0);
            ErrorList.Clear();
        }

        internal static IntPtr s_halNewDataSem = IntPtr.Zero;

        /// <summary>
        /// Clears all HAL Sim Data and resets it.
        /// </summary>
        /// <param name="resetDS">If true, resets the DS data sempahore.</param>
        public static void ResetHALData(bool resetDS)
        {
            Accelerometer.ResetData();
            foreach (var analogGyroData in AnalogGyro)
            {
                analogGyroData.ResetData();
            }
            foreach (var analogInData in AnalogIn)
            {
                analogInData.ResetData();
            }
            foreach (var analogOutData in AnalogOut)
            {
                analogOutData.ResetData();
            }
            foreach (var analogTriggerData in AnalogTrigger)
            {
                analogTriggerData.ResetData();
            }
            foreach (var dioData in DIO)
            {
                dioData.ResetData();
            }
            foreach (var pwmData in PWM)
            {
                pwmData.ResetData();
            }
            foreach (var relayData in Relay)
            {
                relayData.ResetData();
            }
            foreach (var digitalPWMData in DigitalPWM)
            {
                digitalPWMData.ResetData();
            }
            /*
            foreach (var digitalGlitchFilterData in DigitalGlitchFilter)
            {
                digitalGlitchFilterData.ResetData();
            }
            */
            foreach (var counterData in Counter)
            {
                counterData.ResetData();
            }
            foreach (var encoderData in Encoder)
            {
                encoderData.ResetData();
            }
            RoboRioData.ResetData();

            foreach (var pdpData in s_pdp.Values)
            {
                pdpData.ResetData();
            }

            foreach (var pcmData in s_pcm.Values)
            {
                pcmData.ResetData();
            }

            foreach (var spiAccelerometerData in SPIAccelerometer)
            {
                spiAccelerometerData.ResetData();
            }
            ErrorList.Clear();
            DriverStation.ResetData();
            SimHooks.RestartTiming();

            if (resetDS)
            {
                s_halNewDataSem = IntPtr.Zero;
            }
        }
    }
}
