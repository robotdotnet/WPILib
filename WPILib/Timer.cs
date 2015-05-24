

using System;
using System.Threading;
using HAL_Base;
using WPILib.Util;

namespace WPILib
{
    public class Timer
    {
        private static StaticInterface impl;

        public static void SetImplementation(StaticInterface ti)
        {
            impl = ti;
        }

        public static double GetFPGATimestamp()
        {
            if (impl != null)
            {
                return impl.GetFPGATimestamp();
            }
            else
            {
                throw new BaseSystemNotInitializedException("");
            }
        }

        public static double GetMatchTime()
        {
            if (impl != null)
            {
                return impl.GetMatchTime();
            }
            else
            {
                throw new BaseSystemNotInitializedException("");
            }
        }

        public static void Delay(double seconds)
        {
            if (impl != null) {
			impl.Delay(seconds);
		} else {
			throw new BaseSystemNotInitializedException("");
		}
        }

        public interface StaticInterface
        {
            double GetFPGATimestamp();
            double GetMatchTime();
            void Delay(double seconds);
            Interface NewTimer();
        }

        private Interface timer;

        public Timer()
        {
            if (impl != null)
                timer = impl.NewTimer();
            else
            {
                throw new BaseSystemNotInitializedException("");//Need to figure out 
            }
        }

        public double Get()
        {
            return timer.Get();
        }

        public void Reset()
        {
            timer.Reset();
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public bool HasPeriodPassed(double period)
        {
            return timer.HasPeriodPassed(period);
        }
        public interface Interface
        {
            double Get();
            void Reset();
            void Start();
            void Stop();
            bool HasPeriodPassed(double period);
        }
    }
}
