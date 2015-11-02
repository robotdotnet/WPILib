using System;
using System.Collections.Generic;
using System.Linq;
using HAL_Simulator.Data;

namespace HAL_Simulator
{
    /// <summary>
    /// This class allows us to listen for changes in a specific part of the dictionary. 
    /// </summary>
    /// <remarks>Not everything is wrapped, because we don't care about changes in certain things.
    /// <para/>Note that since the indexer is not marked virtual, if you set a value in the
    /// dictionary manually, the notify callback will not be called unless you first
    /// box the dictionary as a NotifyDict. The recommended way to update the data however
    /// is to update the HalInData dictionary, and then call <see cref="SimData.UpdateHalData"/> 
    /// with that dictionary.</remarks>
    /// <typeparam name="TKey">Please use dynamic</typeparam>
    /// <typeparam name="TValue">Please use dynamic</typeparam>
    public class NotifyDict<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, Action<dynamic, dynamic>> m_callbacks = new Dictionary<TKey, Action<dynamic, dynamic>>();


        /// <summary>
        /// Register a notify function to get called when the field updates.
        /// </summary>
        /// <param name="key">The key to watch</param>
        /// <param name="action">The callback function</param>
        /// <param name="notify">Whether to notify immediately</param>
        public void Register(TKey key, Action<dynamic, dynamic> action, bool notify = false)
        {
            if (!ContainsKey(key))
            {
                throw new ArgumentOutOfRangeException(nameof(key), "Cannot register for non existent key");
            }
            if (!m_callbacks.ContainsKey(key))
            {
                m_callbacks.Add(key, action);
            }
            else
            {
                m_callbacks[key] += action;
            }
            if (notify)
            {
                m_callbacks[key]?.Invoke(key, this[key]);
            }
        }

        /// <summary>
        /// Cancel a notify function
        /// </summary>
        /// <param name="key">The key the function is waiting for</param>
        /// <param name="action">The callback function to cancel.</param>
        public void Cancel(TKey key, Action<dynamic, dynamic> action)
        {
            if (action != null && m_callbacks.ContainsKey(key)) m_callbacks[key] -= action;
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key. If a callback exists for the key, call it.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        public new TValue this[TKey key]
        {
            get { return base[key]; }
            set
            {
                base[key] = value;
                if (m_callbacks.ContainsKey(key))
                {
                    m_callbacks[key]?.Invoke(key, value);
                }
            }
        }

    }

    /// <summary>
    /// Marks a variable in the dict as something the simulator can set.
    /// </summary>
    internal class IN
    {
        public dynamic value { get; set; }
        public IN(dynamic d)
        {
            value = d;
        }
    }

    /// <summary>
    /// Marks a variable in the dict as something the robot will set.
    /// </summary>
    internal class OUT
    {
        public dynamic value { get; set; }
        public OUT(dynamic d)
        {
            value = d;
        }
    }

    /// <summary>
    /// Marks a variable in the dict as something controlled by the driver station.
    /// </summary>
    internal class DS
    {
        public dynamic value { get; set; }
        public DS(dynamic d)
        {
            value = d;
        }
    }

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
                data = new CanTalonData();
                s_canTalon.Add(id, data);
                return data;
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
                return true;
            }
        }

        public static void RemoveCanTalon(int id)
        {
            s_canTalon.Remove(id);
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
                return true;
            }
        }

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



        /*
        internal static Dictionary<dynamic, dynamic> halData = new Dictionary<dynamic, dynamic>();

        /// <summary>
        /// Gets a reference to the HalData dictionary.
        /// </summary>
        public static Dictionary<dynamic, dynamic> HalData => halData;

        internal static Dictionary<dynamic, dynamic> halInData = new Dictionary<dynamic, dynamic>();

        /// <summary>
        /// Gets a reference to the HalInData dictionary.
        /// </summary>
        public static Dictionary<dynamic, dynamic> HalInData => halInData;

        internal static Dictionary<dynamic, dynamic> halDSData = new Dictionary<dynamic, dynamic>();

        /// <summary>
        /// Gets a reference to the HalDSData dictionary.
        /// </summary>
        public static Dictionary<dynamic, dynamic> HalDSData => halDSData;

        /// <summary>
        /// This method gets a copy of both data dictionaries.
        /// It is here for use with reflection. Please use the Properties instead
        /// </summary>
        /// <param name="halDataOut"></param>
        /// <param name="halInDataOut"></param>
        /// <param name="halDSDataOut"></param>
        public static void GetData(out Dictionary<dynamic, dynamic> halDataOut,
            out Dictionary<dynamic, dynamic> halInDataOut, out Dictionary<dynamic, dynamic> halDSDataOut)
        {
            halDataOut = halData;
            halInDataOut = halInData;
            halDSDataOut = halDSData;
        }
        */

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

            /*
            halData.Clear();
            halInData.Clear();
            halDSData.Clear();
            */


            if (resetDS)
            {
                HALNewDataSem = IntPtr.Zero;
            }

            /*
            halData["time"] = new Dictionary<dynamic, dynamic>
            {
                {"has_source", new IN(false) },
                {"program_start", new OUT(SimHooks.GetTime())},
                {"match_start", new OUT(0.0)}
            };

            halData["control"] = new Dictionary<dynamic, dynamic>
            {
                {"has_source", new IN(false)},
                {"enabled", new DS(false)},
                {"autonomous", new DS(false)},
                {"test", new DS(false)},
                {"eStop", new DS(false)},
                {"fms_attached", new DS(false)},
                {"ds_attached", new DS(false)},
            };

            halData["reports"] = new NotifyDict<dynamic, dynamic>();

            halData["joysticks"] = new List<dynamic>();
            for (int i = 0; i < HAL_Base.HAL.DriverStationConstants.JoystickPorts; i++)
            {
                halData["joysticks"].Add(new NotifyDict<dynamic, dynamic>
                {
                    {"has_source", new IN(false) },
                    {"buttons", new DS(new bool[13]) },
                    {"axes", new DS(new double[6]) },
                    {"povs", new DS(new []{-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1})},
                    {"rightRumble", new OUT(0) },
                    {"leftRumble", new OUT(0) },
                    {"isXbox", new DS(0)},
                    {"type",  new DS(0)},
                    {"name", new DS("") },
                    {"axisCount", new DS(6) },
                    {"buttonCount", new DS(12) }
                });
            }
            halData["error_data"] = new OUT(null);

            halData["user_program_state"] = new OUT(null);


            //manually filling out DS data. Later this will be automated.
            halDSData["alliance_station"] = 0;
            halDSData["control"] = new Dictionary<dynamic, dynamic>
            {
                {"enabled", false},
                {"autonomous", false},
                {"test", false},
                {"eStop", false},
                {"fms_attached", false},
                {"ds_attached", false},
            };

            halDSData["joysticks"] = new List<dynamic>();
            for (int i = 0; i < 6; i++)
            {
                halDSData["joysticks"].Add(new NotifyDict<dynamic, dynamic>
                {
                    {"buttons", new bool[13] },
                    {"axes", new double[6] },
                    {"povs", new int[12] },
                    {"isXbox", 0},
                    {"type",  0},
                    {"name", "" },
                    {"axisCount", 6 },
                    {"buttonCount", 12 }
                });
            }


            FilterHalData(halData, halInData);
            */

        }


        /*
        private static void FilterHalData(Dictionary<dynamic, dynamic> both, Dictionary<dynamic, dynamic> inData)
        {
            List<dynamic> myKeys = both.Keys.ToList();

            foreach (var s in myKeys)
            {
                dynamic v = both[s];
                dynamic k = s;
                if (v is IN)
                {
                    both[k] = v.value;
                    inData[k] = v.value;
                }
                else if (v is OUT)
                {
                    both[k] = v.value;
                }
                else if (v is DS)
                {
                    both[k] = v.value;
                }
                else if (v is Dictionary<dynamic, dynamic>)
                {
                    Dictionary<dynamic, dynamic> vIn = new Dictionary<dynamic, dynamic>();
                    FilterHalData(v, vIn);
                    if (vIn.Count > 0)
                    {
                        inData[k] = vIn;
                    }
                }
                else if (v is List<dynamic>)
                {
                    List<dynamic> vIn = FilterHalList(v);
                    if (vIn.Count > 0)
                    {
                        inData[k] = vIn;
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(both), " Must be dictionary, list, In, or Out.");
                }
            }
        }

        private static List<dynamic> FilterHalList(List<dynamic> both)
        {
            List<dynamic> inList = new List<dynamic>();

            foreach (var v in both)
            {
                if (!(v is Dictionary<dynamic, dynamic>))
                {
                    throw new ArgumentOutOfRangeException(nameof(both), "Lists can only contain dictionaries, otherwise must only be contained in IN or OUT.");
                }
                Dictionary<dynamic, dynamic> vIn = new Dictionary<dynamic, dynamic>();
                FilterHalData(v, vIn);
                if (vIn.Count != 0)
                {
                    inList.Add(vIn);
                }
            }
            return inList;
        }

        /// <summary>
        /// This function takes a dictionary, and updates it into the main HAL dictionary.
        /// </summary>
        /// <param name="inDict"></param>
        /// <param name="outData"></param>
        public static void UpdateHalData(Dictionary<dynamic, dynamic> inDict, Dictionary<dynamic, dynamic> outData = null)
        {
            if (outData == null)
                outData = halData;

            foreach (var o in inDict)
            {
                if (o.Value is Dictionary<dynamic, dynamic>)
                {
                    UpdateHalData(o.Value, outData[o.Key]);
                }
                else if (o.Value is List<dynamic> || o.Value is Array)
                {
                    var vOut = outData[o.Key];
                    int count = 0;
                    foreach (var vv in o.Value)
                    {
                        if (vv is Dictionary<dynamic, dynamic>)
                        {
                            UpdateHalData(vv, vOut[count]);
                        }
                        else
                        {
                            vOut[count] = vv;
                        }
                        count++;
                    }
                }
                else
                {
                    //Since the Dictionary Indexer is not marked virtual, we have to explcitly
                    //see if the dictionary is a NotifyDict, and if so, box it and then set the value.
                    NotifyDict<dynamic, dynamic> tmp = outData as NotifyDict<dynamic, dynamic>;
                    if (tmp != null)
                    {
                        tmp[o.Key] = o.Value;
                    }
                    else
                    {
                        outData[o.Key] = o.Value;
                    }
                }
            }
        }
        */
    }
}
