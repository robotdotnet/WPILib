using System;
using System.Collections.Generic;
using HAL_Simulator.Data;

namespace HAL_Simulator
{
    /// <summary>
    /// 
    /// </summary>
    public class SimData
    {
        public static AccelerometerData Accelerometer { get; } = new AccelerometerData();
        public static GlobalData GlobalData { get; } = new GlobalData();
        public static List<AnalogOutData> AnalogOut { get; } = new List<AnalogOutData>();

        public static List<AnalogInData> AnalogIn { get; } = new List<AnalogInData>();
        public static List<AnalogTriggerData> AnalogTrigger { get; } = new List<AnalogTriggerData>();

        public static List<DIOData> DIO { get; } = new List<DIOData>();

        public static List<PWMData> PWM { get; } = new List<PWMData>();

        public static List<MXPData> MXP { get; } = new List<MXPData>();

        public static List<DigitalPWMData> DigitalPWM { get; } = new List<DigitalPWMData>();

        public static List<RelayData> Relay { get; } = new List<RelayData>();

        public static List<CounterData> Counter { get; } = new List<CounterData>();
        public static List<EncoderData> Encoder { get; } = new List<EncoderData>();

        private static readonly Dictionary<int, PCMData> s_pcm = new Dictionary<int, PCMData>();

        public static RoboRioData RoboRioData { get; } = new RoboRioData();
        private static readonly Dictionary<int, PDPData> s_pdp = new Dictionary<int, PDPData>();

        private static readonly Dictionary<int, CanTalonData> s_canTalon = new Dictionary<int, CanTalonData>();

        public static DriverStationData DriverStation { get; } = new DriverStationData();

        public static NotifyDict<byte, dynamic> Reports { get; } = new NotifyDict<byte, dynamic>();

        public static string ErrorData { get; internal set; } = "";

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

        public static void RemoveCanTalon(int id)
        {
            s_canTalon.Remove(id);
            OnTalonSRXAddedOrRemoved?.Invoke(id, new TalonSRXEventArgs(false));
        }

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

        public static event EventHandler<TalonSRXEventArgs> OnTalonSRXAddedOrRemoved;
        public static event EventHandler OnPCMAdded;
        public static event EventHandler OnPDPAdded;

        static SimData()
        {
            for (int i = 0; i < 2; i++)
            {
                AnalogOut.Add(new AnalogOutData());
            }

            for (int i = 0; i < 8; i++)
            {
                AnalogIn.Add(new AnalogInData());
            }

            for (int i = 0; i < 4; i++)
            {
                AnalogTrigger.Add(new AnalogTriggerData());
            }

            for (int i = 0; i < 26; i++)
            {
                DIO.Add(new DIOData());
            }

            for (int i = 0; i < 20; i++)
            {
                PWM.Add(new PWMData());
            }

            for (int i = 0; i < 16; i++)
            {
                MXP.Add(new MXPData());
            }

            for (int i = 0; i < 4; i++)
            {
                Relay.Add(new RelayData());
            }

            for (int i = 0; i < 8; i++)
            {
                Counter.Add(new CounterData());
            }

            for (int i = 0; i < 4; i++)
            {
                Encoder.Add(new EncoderData());
            }

            InitializePDP(0);
        }

        internal static IntPtr HALNewDataSem = IntPtr.Zero;

        /// <summary>
        /// Clears all HAL Sim Data and resets it.
        /// </summary>
        /// <param name="resetDS">If true, resets the DS data sempahore.</param>
        public static void ResetHALData(bool resetDS)
        {
            Accelerometer.ResetData();
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
            foreach (var digitalPWMData in DigitalPWM)
            {
                digitalPWMData.ResetData();
            }
            foreach (var mxpData in MXP)
            {
                mxpData.ResetData();
            }
            foreach (var relayData in Relay)
            {
                relayData.ResetData();
            }
            DigitalPWM.Clear();
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
            DriverStation.ResetData();
            GlobalData.ProgramStartTime = SimHooks.GetTime();

            if (resetDS)
            {
                HALNewDataSem = IntPtr.Zero;
            }
        }
    }
}
