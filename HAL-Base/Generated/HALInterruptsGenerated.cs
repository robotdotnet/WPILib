//File automatically generated using robotdotnet-tools. Please do not modify.
using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    public partial class HALInterrupts
    {
        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            InitializeInterrupts = (InitializeInterruptsDelegate)Delegate.CreateDelegate(typeof(InitializeInterruptsDelegate), type.GetMethod("initializeInterrupts"));
            CleanInterrupts = (CleanInterruptsDelegate)Delegate.CreateDelegate(typeof(CleanInterruptsDelegate), type.GetMethod("cleanInterrupts"));
            WaitForInterrupt = (WaitForInterruptDelegate)Delegate.CreateDelegate(typeof(WaitForInterruptDelegate), type.GetMethod("waitForInterrupt"));
            EnableInterrupts = (EnableInterruptsDelegate)Delegate.CreateDelegate(typeof(EnableInterruptsDelegate), type.GetMethod("enableInterrupts"));
            DisableInterrupts = (DisableInterruptsDelegate)Delegate.CreateDelegate(typeof(DisableInterruptsDelegate), type.GetMethod("disableInterrupts"));
            ReadRisingTimestamp = (ReadRisingTimestampDelegate)Delegate.CreateDelegate(typeof(ReadRisingTimestampDelegate), type.GetMethod("readRisingTimestamp"));
            ReadFallingTimestamp = (ReadFallingTimestampDelegate)Delegate.CreateDelegate(typeof(ReadFallingTimestampDelegate), type.GetMethod("readFallingTimestamp"));
            RequestInterrupts = (RequestInterruptsDelegate)Delegate.CreateDelegate(typeof(RequestInterruptsDelegate), type.GetMethod("requestInterrupts"));
            AttachInterruptHandler = (AttachInterruptHandlerDelegate)Delegate.CreateDelegate(typeof(AttachInterruptHandlerDelegate), type.GetMethod("attachInterruptHandler"));
            SetInterruptUpSourceEdge = (SetInterruptUpSourceEdgeDelegate)Delegate.CreateDelegate(typeof(SetInterruptUpSourceEdgeDelegate), type.GetMethod("setInterruptUpSourceEdge"));
        }

        public delegate System.IntPtr InitializeInterruptsDelegate(uint interruptIndex, bool watcher, ref int status);
        public static InitializeInterruptsDelegate InitializeInterrupts;

        public delegate void CleanInterruptsDelegate(System.IntPtr interrupt_pointer, ref int status);
        public static CleanInterruptsDelegate CleanInterrupts;

        public delegate uint WaitForInterruptDelegate(System.IntPtr interrupt_pointer, double timeout, bool ignorePrevious, ref int status);
        public static WaitForInterruptDelegate WaitForInterrupt;

        public delegate void EnableInterruptsDelegate(System.IntPtr interrupt_pointer, ref int status);
        public static EnableInterruptsDelegate EnableInterrupts;

        public delegate void DisableInterruptsDelegate(System.IntPtr interrupt_pointer, ref int status);
        public static DisableInterruptsDelegate DisableInterrupts;

        public delegate double ReadRisingTimestampDelegate(System.IntPtr interrupt_pointer, ref int status);
        public static ReadRisingTimestampDelegate ReadRisingTimestamp;

        public delegate double ReadFallingTimestampDelegate(System.IntPtr interrupt_pointer, ref int status);
        public static ReadFallingTimestampDelegate ReadFallingTimestamp;

        public delegate void RequestInterruptsDelegate(System.IntPtr interrupt_pointer, byte routing_module, uint routing_pin, bool routing_analog_trigger, ref int status);
        public static RequestInterruptsDelegate RequestInterrupts;

        public delegate void AttachInterruptHandlerDelegate(System.IntPtr interrupt_pointer, InterruptHandlerFunction handler, System.IntPtr param, ref int status);
        public static AttachInterruptHandlerDelegate AttachInterruptHandler;

        public delegate void SetInterruptUpSourceEdgeDelegate(System.IntPtr interrupt_pointer, bool risingEdge, bool fallingEdge, ref int status);
        public static SetInterruptUpSourceEdgeDelegate SetInterruptUpSourceEdge;
    }
}
