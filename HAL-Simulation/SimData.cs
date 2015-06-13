using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HAL_FRC
{
    class NotifyDict<T, T2> : Dictionary<T, T2>
    {
        public NotifyDict() : base()
        {

        }

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
        static Dictionary<dynamic, dynamic> halData = new Dictionary<dynamic, dynamic>();
        static Dictionary<dynamic, dynamic> halInData = new Dictionary<dynamic, dynamic>();

        public static void GetData(out Dictionary<dynamic, dynamic> halDataOut, out Dictionary<dynamic, dynamic> halInDataOut)
        {
            halDataOut = halData;
            halInDataOut = halInData;
        }

        public static MULTIWAIT_ID? halNewDataSem = null;

        public static void ResetHALData()
        {
            halData.Clear();
            halInData.Clear();
            halNewDataSem = null;


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
            /*
            halData["joysticks"] = new Dictionary<dynamic, dynamic>
            {
                {
                    0, new Dictionary<dynamic, dynamic>()
                    {
                        {"has_source", false },
                        {"buttons", new bool[32] },
                        {"axes", new double[6] },
                        {"povs", new int[12] }
                    }
                },
                {
                    1, new Dictionary<dynamic, dynamic>()
                    {
                        {"has_source", false },
                        {"buttons", new bool[32] },
                        {"axes", new double[6] },
                        {"povs", new int[12] }
                    }
                },
                {
                    2, new Dictionary<dynamic, dynamic>()
                    {
                        {"has_source", false },
                        {"buttons", new bool[32] },
                        {"axes", new double[6] },
                        {"povs", new int[12] }
                    }
                },
                {
                    3, new Dictionary<dynamic, dynamic>()
                    {
                        {"has_source", false },
                        {"buttons", new bool[32] },
                        {"axes", new double[6] },
                        {"povs", new int[12] }
                    }
                },
                {
                    4, new Dictionary<dynamic, dynamic>()
                    {
                        {"has_source", false },
                        {"buttons", new bool[32] },
                        {"axes", new double[6] },
                        {"povs", new int[12] }
                    }
                },
                {
                    5, new Dictionary<dynamic, dynamic>()
                    {
                        {"has_source", false },
                        {"buttons", new bool[32] },
                        {"axes", new double[6] },
                        {"povs", new int[12] }
                    }
                }
            };
            */

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
                    { "has_source", new IN(false) },
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

            FilterHalData(halData, halInData);
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
                Dictionary<dynamic,dynamic> vIn = new Dictionary<dynamic, dynamic>();
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
    }
}
