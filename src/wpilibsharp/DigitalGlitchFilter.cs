using System;
using Hal;
using WPILib.SmartDashboardNS;

namespace WPILib
{
    public sealed class DigitalGlitchFilter : ISendable, IDisposable
    {
        private static readonly object m_mutex = new object();
        private static readonly bool[] m_filterAllocated = new bool[3];

        private int m_channelIndex = -1;

        public DigitalGlitchFilter()
        {
            lock (m_mutex)
            {
                int index = 0;
                while (m_filterAllocated[index] && index < m_filterAllocated.Length)
                {
                    index++;
                }
                if (index != m_filterAllocated.Length)
                {
                    m_channelIndex = index;
                    m_filterAllocated[index] = true;
                    UsageReporting.Report(ResourceType.DigitalGlitchFilter, m_channelIndex + 1);
                    SendableRegistry.Instance.AddLW(this, "DigitalGlitchFilter", index);
                }
                else
                {
                    throw new InvalidOperationException("Out of filters");
                }
            }
        }

        private static void SetFilter(IDigitalSource input, int channelIndex)
        {
            if (input != null)
            {
                if (input.IsAnalogTrigger)
                {
                    throw new InvalidOperationException("Analog Triggers not supported for DigitalGlitchFilters");
                }
                Hal.DIOLowLevel.SetFilterSelect(input.PortHandleForRouting, channelIndex);

                int selected = DIOLowLevel.GetFilterSelect(input.PortHandleForRouting);
                if (selected != channelIndex)
                {
                    throw new InvalidOperationException($"SetFilterSelect {channelIndex} failed -> {selected}");
                }
            }
        }

        public void Add(IDigitalSource input)
        {
            SetFilter(input, m_channelIndex + 1);
        }

#pragma warning disable CA1822 // Mark members as static
        public void Remove(IDigitalSource input)
#pragma warning restore CA1822 // Mark members as static
        {
            SetFilter(input, 0);
        }

        public void SetPeriodCycles(long fpgaCycles)
        {
            Console.WriteLine(fpgaCycles);
            DIOLowLevel.SetFilterPeriod(m_channelIndex, fpgaCycles);
        }

        public void SetPeriod(TimeSpan timeSpan)
        {
            // Ticks are in 100ns periods.
            // 4 is because an fpga cycle is 25ns
            long fpgaCycles = timeSpan.Ticks * 4;
            SetPeriodCycles(fpgaCycles);
        }

        public void SetPeriodNanoseconds(long nanoseconds)
        {
            // x 25 to get to 40MHz
            long fpgaCycles = nanoseconds * 25;
            SetPeriodCycles(fpgaCycles);
        }


        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            if (m_channelIndex > 0)
            {
                lock (m_mutex)
                {
                    m_filterAllocated[m_channelIndex] = false;
                }
                m_channelIndex = -1;
            }

        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
        }
    }
}
