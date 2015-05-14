

using System;
using System.Collections.Generic;
using System.Text;
using WPILib.Interfaces;
using WPILib.Util;
using HAL_FRC;

namespace WPILib
{
    public class Counter : SensorBase, CounterBase, PIDSource
    {
        private DigitalSource _upSource;
        private DigitalSource _downSource;
        private bool _allocatedUpSource;
        private bool _allocatedDownSource;
        private IntPtr _counterPtr;
        private uint _index;
        private PIDSourceParameter _pidSource;
        private double _distancePerPulse;

        private void InitCounter(Mode mode)
        {
            int status = 0;
            _counterPtr = HALDigital.initializeCounter(mode, ref _index, ref status);

            _allocatedUpSource = false;
            _allocatedDownSource = false;
            _upSource = null;
            _downSource = null;

            SetMaxPeriod(0.5);

            HAL.Report(ResourceType.kResourceType_Counter, (byte)_index, (byte)mode);
        }

        public Counter()
        {
            InitCounter(Mode.kTwoPulse);
        }

        public Counter(DigitalSource source)
        {
            if (source == null)
                throw new NullReferenceException("Digital Source given was null");
            InitCounter(Mode.kTwoPulse);
            SetUpSource(source);
        }

        public Counter(int channel)
        {
            InitCounter(Mode.kTwoPulse);
            //SetUpSource(channel);
        }

        public Counter(EncodingType encodingType, DigitalSource upSource, DigitalSource downSource, bool inverted)
        {
            InitCounter(Mode.kExternalDirection);
            if (encodingType != EncodingType.k2X_val && encodingType != EncodingType.k1X_val)
            {
                throw new SystemException("Counters only support 1X and 2X decoding!");
            }
            if (upSource == null)
                throw new NullReferenceException("Up Source given was null");
            //SetUpSource(upSource)
            if (downSource == null)
                throw new NullReferenceException("Down Source given was null");
            //SetDownSource(downSource);
            int status = 0;
            if (encodingType == EncodingType.k1X_val)
            {
                //SetUpSourceEdge(true, false);
                HALDigital.setCounterAverageSize(_counterPtr, 1, ref status);
            }
            else
            {
                //SetDownSourceEdge(true, true);
                HALDigital.setCounterAverageSize(_counterPtr, 2, ref status);
            }

            //SetDownSourceEdge(inverted, true);
        }

        //AnalogTriggerCojnter

        public override void Free()
        {
            //SetUpdateWhenEmpty

            int status = 0;
            HALDigital.freeCounter(_counterPtr, ref status);

            _upSource = null;
            _downSource = null;
            _counterPtr = IntPtr.Zero;
        }

        public int GetFPGAIndex()
        {
            return (int)_index;
        }

        public void SetUpSource(int channel)
        {
            SetUpSource(new DigitalInput(channel));
            _allocatedUpSource = true;
        }
        public void SetUpSource(DigitalSource source)
        {
            if (_upSource != null && _allocatedUpSource)
            {
                _upSource.Free();
                _allocatedUpSource = false;
            }
            _upSource = source;
            int status = 0;
            HALDigital.setCounterUpSource(_counterPtr, (uint)source.GetChannelForRouting(), source.GetAnalogTriggerForRouting(), ref status);
        }

        public void SetUpSource(AnalogTrigger analogTrigger, AnalogTriggerType triggerType)
        {
            if (analogTrigger == null)
                throw new NullReferenceException("Analog Trigger given was null");
            if (triggerType == null)
                throw new NullReferenceException("Analog Trigger Type given was null");
            //SetUpSource() //Waiting on AnalogTriggerOutput
            _allocatedUpSource = true;
        }

        public void SetUpSource(bool risingEdge, bool fallingEdge)
        {
            if (_upSource == null)
                throw new SystemException("Up Source must be set before setting the edge!");
            int status = 0;
            HALDigital.setCounterUpSourceEdge(_counterPtr, risingEdge, fallingEdge, ref status);
        }

        public void ClearUpSource()
        {
            if (_upSource != null && _allocatedUpSource)
            {
                _upSource.Free();
                _allocatedUpSource = false;
            }
            _upSource = null;

            int status = 0;
            HALDigital.clearCounterUpSource(_counterPtr, ref status);
        }

        public int Get()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public double GetPeriod()
        {
            throw new NotImplementedException();
        }

        public void SetMaxPeriod(double maxPeriod)
        {
            throw new NotImplementedException();
        }

        public bool GetStopped()
        {
            throw new NotImplementedException();
        }

        public bool GetDirection()
        {
            throw new NotImplementedException();
        }

        public double PidGet()
        {
            throw new NotImplementedException();
        }
    }
}
