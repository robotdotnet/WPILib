using System;
using HAL.Base;
using static WPILib.Utility;
using static HAL.Base.HALAnalogTrigger;

namespace WPILib
{
    /// <summary>
    /// Class to represent a specific output from an analog trigger.
    /// </summary>
    /// <remarks>
    /// This class is used to get the current output value and also as a <see cref="DigitalSource"/> to provide
    /// routing of an output to digital subsystems on the FPGA such as <see cref="Counter"/>,
    /// <see cref="Encoder"/>, and Interrupt.
    /// <para/>
    /// The <see cref="AnalogTriggerType.State"/> output indicates the primary output value of the trigger.If
    /// the analog signal is less than the lower limit, the output is false. If the
    /// analog value is greater than the upper limit, then the output is true. If the
    /// analog value is in between, then the trigger output state maintains its most
    /// recent value.
    /// <para/>
    /// The <see cref="AnalogTriggerType.InWindow"/> output indicates whether or not the analog signal is inside the
    /// range defined by the limits.
    /// <para/>
    /// The <see cref="AnalogTriggerType.RisingPulse"/> and <see cref="AnalogTriggerType.FallingPulse"/> 
    /// outputs detect an instantaneous transition
    /// from above the upper limit to below the lower limit, and vise versa. These
    /// pulses represent a rollover condition of a sensor and can be routed to an up
    /// / down couter or to interrupts. Because the outputs generate a pulse, they
    /// cannot be read directly.To help ensure that a rollover condition is not
    /// missed, there is an average rejection filter available that operates on the
    /// upper 8 bits of a 12 bit number and selects the nearest outlyer of 3 samples.
    /// This will reject a sample that is (due to averaging or sampling) errantly
    /// between the two limits.This filter will fail if more than one sample in a
    /// row is errantly in between the two limits.You may see this problem if
    /// attempting to use this feature with a mechanical rollover sensor, such as a
    /// 360 degree no-stop potentiometer without signal conditioning, because the
    /// rollover transition is not sharp / clean enough. Using the averaging engine
    /// may help with this, but rotational speeds of the sensor will then be limited.
    /// </remarks>
    public class AnalogTriggerOutput : DigitalSource
    {
        private readonly AnalogTrigger m_trigger;
        private readonly AnalogTriggerType m_outputType;

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
                throw new ArgumentNullException(nameof(trigger), "Analog Trigger given was null");
            m_trigger = trigger;
            m_outputType = outputType;

            HAL.Base.HAL.Report(ResourceType.kResourceType_AnalogTriggerOutput, (byte)trigger.Index, (byte)outputType);
        }

        /// <summary>
        /// Get the state of the analog trigger output
        /// </summary>
        /// <returns></returns>
        public bool Get()
        {
            int status = 0;
            bool value = HAL_GetAnalogTriggerOutput(m_trigger.HALHandle, (HALAnalogTriggerType)m_outputType, ref status);
            CheckStatus(status);
            return value;
        }

        /// <inheritdoc/>
        public override int PortHandleForRouting => m_trigger.HALHandle;
        /// <inheritdoc/>
        public override AnalogTriggerType AnalogTriggerTypeForRouting => m_outputType;
        /// <inheritdoc/>
        public override bool AnalogTrigger => true;
        /// <inheritdoc/>
        public override int Channel => m_trigger.Index;
    }
}
