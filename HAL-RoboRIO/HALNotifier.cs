//File automatically generated using robotdotnet-tools. Please do not modify.
using HAL_Base;
namespace HAL_RoboRIO
{
    public class HALNotifier
    {

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "initializeNotifier")]
        public static extern System.IntPtr initializeNotifier(NotifierDelegate ProcessQueue, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "cleanNotifier")]
        public static extern void cleanNotifier(System.IntPtr notifier_pointer, ref int status);

        [System.Runtime.InteropServices.DllImport("libHALAthena_shared.so", EntryPoint = "updateNotifierAlarm")]
        public static extern void updateNotifierAlarm(System.IntPtr notifier_pointer, uint triggerTime, ref int status);
    }
}
