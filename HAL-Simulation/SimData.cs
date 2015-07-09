using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HAL_Simulator
{
    class NotifyDict<T, T2> : Dictionary<T, T2>
    {
        private Action<dynamic, dynamic> callback;

        public void Register(T key, Action<dynamic, dynamic> action, bool notify = false)
        {
            if (!this.ContainsKey(key))
            {
                throw new ArgumentOutOfRangeException(nameof(key), "Cannot register for non existent key");
            }
            callback += action;
            if (notify)
            {
                callback?.Invoke(key, this[key]);
            }
        }

        public new T2 this[T key]
        {
            get { return base[key]; }
            set
            {
                base[key] = value;
                Action<dynamic, dynamic> handler = callback;
                handler?.Invoke(key, value);
            }
        }

    }

    public class IN
    {
        public dynamic value { get; set; }
        public IN(dynamic d)
        {
            value = d;
        }
    }

    public class OUT
    {
        public dynamic value { get; set; }
        public OUT(dynamic d)
        {
            value = d;
        }
    }

    public class SimData
    {
        internal static Dictionary<dynamic, dynamic> halData = new Dictionary<dynamic, dynamic>();
        internal static Dictionary<dynamic, dynamic> halInData = new Dictionary<dynamic, dynamic>();

        public static void GetData(out Dictionary<dynamic, dynamic> halDataOut, out Dictionary<dynamic, dynamic> halInDataOut)
        {
            halDataOut = halData;
            halInDataOut = halInData;
        }

        public static IntPtr halNewDataSem = IntPtr.Zero;

        public static void ResetHALData()
        {
            halData.Clear();
            halInData.Clear();
            halNewDataSem = IntPtr.Zero;


            halData["alliance_station"] = new IN(0);
            halData["time"] = new Dictionary<dynamic, dynamic>
            {
                {"has_source", new IN(false) },
                {"program_start", new OUT(0)},
                {"match_start", new OUT(null)}
            };

            halData["control"] = new Dictionary<dynamic, dynamic>
            {
                {"has_source", new IN(false)},
                {"enabled", new OUT(false)},
                {"autonomous", new OUT(false)},
                {"test", new OUT(false)},
                {"eStop", new OUT(false)},
                {"fms_attached", new IN(false)},
                {"ds_attached", new OUT(false)},
            };
            halData["reports"] = new NotifyDict<dynamic, dynamic>();

            halData["joysticks"] = new List<dynamic>();
            for (int i = 0; i < 6; i++)
            {
                halData["joysticks"].Add(new NotifyDict<dynamic, dynamic>
                {
                    {"has_source", new IN(false) },
                    {"buttons", new IN(new bool[13]) },
                    {"axes", new IN(new int[6]) },
                    {"povs", new IN(new int[12]) }

                });
            }

            halData["fpga_button"] = new IN(false);
            halData["error_data"] = new OUT(null);

            halData["accelerometer"] = new Dictionary<dynamic, dynamic>()
            {
                {"has_source", new IN(false) },
                {"active", new OUT(false) },
                {"range", new OUT(0) },
                {"x", new IN(0) },
                {"y", new IN(0) },
                {"z", new IN(0) },
            };

            halData["analog_sample_rate"] = new OUT(1024.0);

            halData["analog_out"] = new List<dynamic>();
            for (int i = 0; i < 8; i++)
            {
                halData["analog_out"].Add(new NotifyDict<dynamic, dynamic>
                {
                    {"initialized", new OUT(false) },
                    {"voltage", new OUT(0.0) },

                });
            }

            halData["analog_in"] = new List<dynamic>();
            for (int i = 0; i < 8; i++)
            {
                halData["analog_in"].Add(new NotifyDict<dynamic, dynamic>
                {
                    {"has_source", new IN(false) },
                    {"initialized", new OUT(false) },
                    {"avg_bits", new OUT(0) },
                    {"oversample_bits", new OUT(0) },
                    { "value", new IN(0) },
                    { "avg_value", new IN(0) },
                    { "voltage", new IN(0) },
                    { "avg_voltage", new IN(0) },
                    { "lsb_weight", new IN(1) },
                    { "offset", new IN(65535) },

                    {"accumulator_initialized", new OUT(0) },
                    {"accumulator_center", new OUT(0) },
                    { "accumulator_value", new IN(1) },
                    { "accumulator_count", new IN(1) },
                    {"accumulator_deadband", new OUT(0) },

                });
            }

            halData["analog_trigger"] = new Dictionary<dynamic, dynamic>()
            {
                {"has_source", new IN(false) },
                {"initialized", new OUT(false) },
                {"port", new OUT(null) },
                {"trig_lower",  new OUT(null)},
                {"trig_upper",  new OUT(null)},
                {"trig_type",   new OUT(null)},
                {"trig_state", new  OUT(false)},
            };

            halData["compressor"] = new NotifyDict<dynamic, dynamic>()
            {
                { "has_source", new IN(false) },
                {"initialized", new OUT(false) },
                {"on", new IN(false) },
                {"closed_loop_enabled", new OUT(false) },
                {"pressure_switch", new IN(false) },
                {"current", new IN(0.0) },
            };

            halData["pwm"] = new List<dynamic>();
            for (int i = 0; i < 20; i++)
            {
                halData["pwm"].Add(new NotifyDict<dynamic, dynamic>
                {
                    {"initialized", new OUT(false) },
                    {"type", new OUT(null) },
                    {"raw_value", new OUT(0.0) },
                    {"value", new OUT(0.0) },
                    {"period_scale", new OUT(null) },
                    {"zero_latch", new OUT(false) },

                });
            }

            halData["pwm_loop_timing"] = new IN(40);

            halData["d0_pwm"] = new OUT(new Dictionary<dynamic, dynamic>[6]); //# dict with keys: duty_cycle, pin
            halData["d0_pwm_rate"] = new OUT(null);


            halData["relay"] = new List<dynamic>();
            for (int i = 0; i < 8; i++)
            {
                halData["relay"].Add(new NotifyDict<dynamic, dynamic>
                {
                    {"initialized", new OUT(false) },
                    {"fwd", new OUT(false) },
                    {"rev", new OUT(false) },
                });
            }

            halData["mxp"] = new List<dynamic>();
            for (int i = 0; i < 16; i++)
            {
                halData["mxp"].Add(new Dictionary<dynamic, dynamic>
                    {
                        {"initialized", new OUT(false)},
                    }
                );
            }
            halData["dio"] = new List<dynamic>();
            for (int i = 0; i < 26; i++)
            {
                halData["dio"].Add(new NotifyDict<dynamic, dynamic>
                {
                    {"has_source", new IN(false) },
                    {"initialized", new OUT(false) },
                    {"value", new IN(false) },
                    {"pulse_length", new OUT(null) },
                    {"is_input", new OUT(false) },

                });
            }

            halData["power"] = new Dictionary<dynamic, dynamic>()
            {
                {"has_source", new IN(false) },
                {"vin_voltage", new IN(0) },
                {"vin_current", new IN(0) },
                {"user_voltage_3v3", new IN(0) },
            };

            halData["solenoid"] = new List<dynamic>();
            for (int i = 0; i < 8; i++)
            {
                halData["solenoid"].Add(new NotifyDict<dynamic, dynamic>
                {
                    {"initialized", new OUT(false)},
                    {"value", new OUT(false)}
                });
            };


            halData["CAN"] = new Dictionary<dynamic,dynamic>();

            

            FilterHalData(halData, halInData);

            halData["time"]["program_start"] = Hooks.GetTime();
        }

        public static void FilterHalData(Dictionary<dynamic, dynamic> both, Dictionary<dynamic, dynamic> inData)
        {
            List<dynamic> myKeys = new List<dynamic>();
            foreach (var key in both.Keys)
            {
                myKeys.Add(key);
            }

            foreach (var s in myKeys)
            {
                dynamic v = both[s];
                dynamic k = s;
                //dynamic v = o.Value;
                if (v is IN)
                {
                    both[k] = v.value;
                    inData[k] = new IN(v.value);
                }
                else if (v is OUT)
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

        public static List<dynamic> FilterHalList(List<dynamic> both)
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
            /*
            if (inList.Count == 0 || inList.Count == both.Count)
            {
                throw new Exception("This is being thrown in Filter HAL Data.");
            }*/
            return inList;
        }

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
                    outData[o.Key] = o.Value;
                }
            }
        }
    }
}
