using System;
using HAL;
using HAL.Base;
using static WPILib.Utility;
using HALAnalog = HAL.Base.HALAnalog;

namespace WPILib
{
    //TODO: Add the essay that WPI has in theirs....
    /// <summary>
    /// Class to represent a specific output from an analog trigger.
    /// </summary>
    public class AnalogTriggerOutput : DigitalSource
    {
        private AnalogTrigger m_trigger;
        private AnalogTriggerType m_outputType;

        /// <summary>
        /// Create an object that represents one of the four outputs from an analog trigger.
        /// </summary>
        /// <remarks>Because this class derives from DigitalSource, it can be passed into
	    /// routing functions for Counter, Encoder, etc.</remarks>
        /// <param name="trigger">The trigger for which this is an output.</param>
        /// <param name="outputType">An enum that specifies the output on the trigger to represent.</param>
        public AnalogTriggerOutput(AnalogTrigger trigger, AnalogTriggerType outputType)
        {
            if (trigger == null)
                throw new NullReferenceException("Analog Trigger given was null");
            m_trigger = trigger;
            m_outputType = outputType;
            
            HAL.Base.HAL.Report(ResourceType.kResourceType_AnalogTriggerOutput, (byte) trigger.Index, (byte) outputType);
        }

        /// <summary>
        /// Destructor
        /// </summary>
        public override void Dispose()
        {
        }

        /// <summary>
        /// Get the state of the analog trigger output
        /// </summary>
        /// <returns></returns>
        public bool Get()
        {
            int status = 0;
            bool value = HALAnalog.GetAnalogTriggerOutput(m_trigger.Port, m_outputType, ref status);
            CheckStatus(status);
            return value;
        }

        /// <summary>
        /// Get the channel routing number.
        /// </summary>
        public override int ChannelForRouting => (m_trigger.Index << 2) + (int) m_outputType;

        /// <summary>
        /// Get the module routing number.
        /// </summary>
        public override byte ModuleForRouting => (byte) (m_trigger.Index >> 2);

        /// <summary>
        /// Is this an analog trigger?
        /// </summary>
        public override bool AnalogTriggerForRouting => true;
    }
}
