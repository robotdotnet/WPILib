using HAL.Base;
using System;
using static HAL.Base.HALDigital;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// DigitalSource interface
    /// </summary>
    /// <remarks>The DigitalSource represents all the possible inputs
    /// for a counter or a quadrature encoder.The source may be either a digital
    /// input or an analog input.If the caller just provides a channel, then a
    /// digital input will be constructed and freed when finished for the source. The
    /// source can either be a digital input or analog trigger but not both.</remarks>
    public abstract class DigitalSource : InterruptableSensorBase
    {
        /// <summary>
        /// A collection of the Digital Sources.
        /// </summary>
        protected static readonly Resource Channels = new Resource(DigitalChannels);
        /// <summary>
        /// The Port this source is attached to.
        /// </summary>
        protected internal DigitalPortSafeHandle Port;
        /// <summary>
        /// The channel this source is connected to
        /// </summary>
        protected int Channel;

        /// <summary>
        /// Base Initialization function for all Ports.
        /// </summary>
        /// <param name="channel">The channel the port is connected too</param>
        /// <param name="input">True if port is input, false if output</param>
        protected void InitDigitalPort(int channel, bool input)
        {
            Channel = channel;

            CheckDigitalChannel(channel);

            Channels.Allocate(channel, "Digital input " + channel + " is already allocated");

            IntPtr portPointer = HAL.Base.HAL.GetPort((byte)channel);
            int status = 0;
            Port = InitializeDigitalPort(portPointer, ref status);
            AllocateDIO(Port, input, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Destructor
        /// </summary>
        public override void Dispose()
        {
            Channels.Deallocate(Channel);
            int status = 0;
            FreeDIO(Port, ref status);
            CheckStatus(status);
            Port.Dispose();
            //FreeDigitalPort(Port);
            Port = null;
            //Port = IntPtr.Zero;
            Channel = 0;
            base.Dispose();
        }

        /// <summary>
        /// Get the channel routing number.
        /// </summary>
        public override int ChannelForRouting => Channel;

        /// <summary>
        /// Get the module routing number.
        /// </summary>
        public override byte ModuleForRouting => 0;

        /// <summary>
        /// Is this an analog trigger.
        /// </summary>
        public override bool AnalogTriggerForRouting => false;
    }
}
