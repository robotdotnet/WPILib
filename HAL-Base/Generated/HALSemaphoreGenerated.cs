//File automatically generated using robotdotnet-tools. Please do not modify.
using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
    public partial class HALSemaphore
    {
        internal static void SetupDelegates()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            var types = HAL.HALAssembly.GetTypes();
            var q = from t in types where t.IsClass && t.Name == className select t;
            Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);
            InitializeMutexRecursive = (InitializeMutexRecursiveDelegate)Delegate.CreateDelegate(typeof(InitializeMutexRecursiveDelegate), type.GetMethod("initializeMutexRecursive"));
            InitializeMutexNormal = (InitializeMutexNormalDelegate)Delegate.CreateDelegate(typeof(InitializeMutexNormalDelegate), type.GetMethod("initializeMutexNormal"));
            DeleteMutex = (DeleteMutexDelegate)Delegate.CreateDelegate(typeof(DeleteMutexDelegate), type.GetMethod("deleteMutex"));
            TakeMutex = (TakeMutexDelegate)Delegate.CreateDelegate(typeof(TakeMutexDelegate), type.GetMethod("takeMutex"));
            TryTakeMutex = (TryTakeMutexDelegate)Delegate.CreateDelegate(typeof(TryTakeMutexDelegate), type.GetMethod("tryTakeMutex"));
            GiveMutex = (GiveMutexDelegate)Delegate.CreateDelegate(typeof(GiveMutexDelegate), type.GetMethod("giveMutex"));
            InitializeSemaphore = (InitializeSemaphoreDelegate)Delegate.CreateDelegate(typeof(InitializeSemaphoreDelegate), type.GetMethod("initializeSemaphore"));
            DeleteSemaphore = (DeleteSemaphoreDelegate)Delegate.CreateDelegate(typeof(DeleteSemaphoreDelegate), type.GetMethod("deleteSemaphore"));
            TakeSemaphore = (TakeSemaphoreDelegate)Delegate.CreateDelegate(typeof(TakeSemaphoreDelegate), type.GetMethod("takeSemaphore"));
            TryTakeSemaphore = (TryTakeSemaphoreDelegate)Delegate.CreateDelegate(typeof(TryTakeSemaphoreDelegate), type.GetMethod("tryTakeSemaphore"));
            GiveSemaphore = (GiveSemaphoreDelegate)Delegate.CreateDelegate(typeof(GiveSemaphoreDelegate), type.GetMethod("giveSemaphore"));
            InitializeMultiWait = (InitializeMultiWaitDelegate)Delegate.CreateDelegate(typeof(InitializeMultiWaitDelegate), type.GetMethod("initializeMultiWait"));
            DeleteMultiWait = (DeleteMultiWaitDelegate)Delegate.CreateDelegate(typeof(DeleteMultiWaitDelegate), type.GetMethod("deleteMultiWait"));
            TakeMultiWait = (TakeMultiWaitDelegate)Delegate.CreateDelegate(typeof(TakeMultiWaitDelegate), type.GetMethod("takeMultiWait"));
            GiveMultiWait = (GiveMultiWaitDelegate)Delegate.CreateDelegate(typeof(GiveMultiWaitDelegate), type.GetMethod("giveMultiWait"));
        }

        public delegate System.IntPtr InitializeMutexRecursiveDelegate();
        public static InitializeMutexRecursiveDelegate InitializeMutexRecursive;

        public delegate System.IntPtr InitializeMutexNormalDelegate();
        public static InitializeMutexNormalDelegate InitializeMutexNormal;

        public delegate void DeleteMutexDelegate(System.IntPtr sem);
        public static DeleteMutexDelegate DeleteMutex;

        public delegate sbyte TakeMutexDelegate(System.IntPtr sem);
        public static TakeMutexDelegate TakeMutex;

        public delegate sbyte TryTakeMutexDelegate(System.IntPtr sem);
        public static TryTakeMutexDelegate TryTakeMutex;

        public delegate sbyte GiveMutexDelegate(System.IntPtr sem);
        public static GiveMutexDelegate GiveMutex;

        public delegate System.IntPtr InitializeSemaphoreDelegate(uint initial_value);
        public static InitializeSemaphoreDelegate InitializeSemaphore;

        public delegate void DeleteSemaphoreDelegate(System.IntPtr sem);
        public static DeleteSemaphoreDelegate DeleteSemaphore;

        public delegate sbyte TakeSemaphoreDelegate(System.IntPtr sem);
        public static TakeSemaphoreDelegate TakeSemaphore;

        public delegate sbyte TryTakeSemaphoreDelegate(System.IntPtr sem);
        public static TryTakeSemaphoreDelegate TryTakeSemaphore;

        public delegate sbyte GiveSemaphoreDelegate(System.IntPtr sem);
        public static GiveSemaphoreDelegate GiveSemaphore;

        public delegate System.IntPtr InitializeMultiWaitDelegate();
        public static InitializeMultiWaitDelegate InitializeMultiWait;

        public delegate void DeleteMultiWaitDelegate(System.IntPtr sem);
        public static DeleteMultiWaitDelegate DeleteMultiWait;

        public delegate sbyte TakeMultiWaitDelegate(System.IntPtr sem, System.IntPtr m, int timeout);
        public static TakeMultiWaitDelegate TakeMultiWait;

        public delegate sbyte GiveMultiWaitDelegate(System.IntPtr sem);
        public static GiveMultiWaitDelegate GiveMultiWait;
    }
}
