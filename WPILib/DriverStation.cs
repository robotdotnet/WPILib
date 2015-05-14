

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using HAL_FRC;

namespace WPILib
{
    public class DriverStation
    {
        public const int kJoystickPorts = 6;
        public const int kMaxJoystickAxes = 12;
        public const int kMaxJoystickPOVs = 12;

        public enum Alliance
        {
            Red,
            Blue,
            Invalid
        };

        private const double JOYSTICK_UNPLUGGED_MESSAGE_INTERVAL = 1.0;
        private double _nextMessageTime = 0.0;

        //Figure out driver station task

        private static DriverStation s_instance = new DriverStation();

        private short[][] _joystickAxes = new short[kJoystickPorts][];
        private short[][] _joystickPOVs = new short[kJoystickPorts][];
        private HALJoystickButtons[] _joystickButtons = new HALJoystickButtons[kJoystickPorts];

        private Thread _thread;
        private Object _dataSem;
        private Object _mutex;
        private bool _threadKeepAlive = true;
        private bool _userInDisabled = false;
        private bool _userInAutonomous = false;
        private bool _userInTeleop = false;
        private bool _userInTest = false;
        private bool _newControlData;

        private IntPtr _packetDataAvailableMutex;
        private IntPtr _packetDataAvailableSem;

        public static DriverStation GetInstance()
        {
            return DriverStation.s_instance;
        }

        protected DriverStation()
        {
            _dataSem = new object();
            _mutex = new object();
            for (int i = 0; i < kJoystickPorts; i++)
            {
                _joystickButtons[i] = new HALJoystickButtons();
                _joystickAxes[i] = new short[12];
                _joystickPOVs[i] = new short[12];
            }

            _packetDataAvailableMutex = HALSemaphore.initializeMutexNormal();
            _packetDataAvailableSem = HALSemaphore.initializeMultiWait();
            HAL.SetNewDataSem(_packetDataAvailableSem);

            _thread = new Thread(Task) { Priority = ThreadPriority.AboveNormal };
            _thread.Start();
        }

        public void Release()
        {
            _threadKeepAlive = false;
        }

        private void Task()
        {
            int safetyCounter = 0;
            while (_threadKeepAlive)
            {
                HALSemaphore.takeMultiWait(_packetDataAvailableSem, _packetDataAvailableMutex, 0);
                GetData();
                //HALSemaphore.giveMultiWait(dataSem);

                /*
                lock (this)
                {
                    GetData();
                }
                 * */
                lock (_dataSem)
                {
                    Monitor.PulseAll(_dataSem);
                }

                if (++safetyCounter >= 4)
                {
                    MotorSafetyHelper.CheckMotors();
                    safetyCounter = 0;
                }
                if (_userInDisabled)
                    HAL.NetworkCommunicationObserveUserProgramDisabled();
                if (_userInAutonomous)
                    HAL.NetworkCommunicationObserveUserProgramAutonomous();
                if (_userInTeleop)
                    HAL.NetworkCommunicationObserveUserProgramTeleop();
                if (_userInTest)
                    HAL.NetworkCommunicationObserveUserProgramTest();
            }
        }

        public void WaitForData(long timeout = 0)
        {
            HALSemaphore.takeMultiWait(_packetDataAvailableSem, _packetDataAvailableMutex, -1);
            /*
            lock (dataSem)
            {
                try
                {
                    Monitor.Wait(dataSem, TimeSpan.FromMilliseconds(timeout));
                }
                catch (ThreadInterruptedException ex)
                {

                }
            }
             * */
        }

        protected void GetData()
        {
            lock (_mutex)
            {
                for (byte stick = 0; stick < kJoystickPorts; stick++)
                {
                    _joystickAxes[stick] = HAL.GetJoystickAxes(stick);
                    _joystickPOVs[stick] = HAL.GetJoystickPOVs(stick);
                    _joystickButtons[stick] = HAL.GetJoystickButtons(stick);
                }
                _newControlData = true;
            }
        }

        public double GetBatteryVoltage()
        {
            int status = 0;
            return HALPower.getVinVoltage(ref status);
        }

        private void ReportJoystickUnpluggedError(String message)
        {
            double currentTime = Timer.GetFPGATimestamp();
            if (currentTime > _nextMessageTime)
            {
                ReportError(message, false);
                _nextMessageTime = currentTime + JOYSTICK_UNPLUGGED_MESSAGE_INTERVAL;
            }
        }

        public double GetStickAxis(int stick, int axis)
        {
            if (stick < 0 || stick >= kJoystickPorts)
            {
                throw new SystemException("Joystick index is out of range, should be 0-5");
            }

            if (axis < 0 || axis >= kMaxJoystickAxes)
            {
                throw new SystemException("Joystick axis is out of range");
            }
            lock (_mutex)
            {
                if (axis >= _joystickAxes[stick].Length)
                {
                    ReportJoystickUnpluggedError("WARNING: Joystick axis " + axis + " on port " + stick +
                                                 " not available, check if controller is plugged in\n");
                    return 0.0;
                }

                sbyte value = (sbyte)_joystickAxes[stick][axis];

                if (value < 0)
                {
                    return value / 128.0;
                }
                else
                {
                    return value / 127.0;
                }
            }
        }

        public int GetStickAxisCount(int stick)
        {
            if (stick < 0 || stick >= kJoystickPorts)
            {
                throw new SystemException("Joystick index is out of range, should be 0-5");
            }
            lock (_mutex)
            {
                return _joystickAxes[stick].Length;
            }
        }

        public int GetStickPOV(int stick, int pov)
        {
            if (stick < 0 || stick >= kJoystickPorts)
            {
                throw new SystemException("Joystick index is out of range, should be 0-5");
            }

            if (pov < 0 || pov >= kMaxJoystickPOVs)
            {
                throw new SystemException("Joystick POV is out of range");
            }

            lock (_mutex)
            {
                if (pov >= _joystickPOVs[stick].Length)
                {
                    ReportJoystickUnpluggedError("WARNING: Joystick POV " + pov + " on port " + stick +
                                                 " not available, check if controller is plugged in\n");
                    return 0;
                }

                return _joystickPOVs[stick][pov];
            }
        }

        public int GetStickPOVCount(int stick)
        {
            if (stick < 0 || stick >= kJoystickPorts)
            {
                throw new SystemException("Joystick index is out of range, should be 0-5");
            }
            lock (_mutex)
            {
                return _joystickPOVs[stick].Length;
            }
        }

        public int GetStickButtons(int stick)
        {
            if (stick < 0 || stick >= kJoystickPorts)
            {
                throw new SystemException("Joystick index is out of range, should be 0-5");
            }

            lock (_mutex)
            {
                return (int)_joystickButtons[stick].buttons;
            }
        }

        public bool GetStickButton(int stick, byte button)
        {
            if (stick < 0 || stick >= kJoystickPorts)
            {
                throw new SystemException("Joystick index is out of range, should be 0-5");
            }

            lock (_mutex)
            {
                if (button > _joystickButtons[stick].count)
                {
                    ReportJoystickUnpluggedError("WARNING: Joystick Button " + button + " on port " + stick +
                                                 " not available, check if controller is plugged in\n");
                    return false;
                }

                if (button <= 0)
                {
                    ReportJoystickUnpluggedError("ERROR: Button indexes begin at 1 in WPILib for C#\n");
                    return false;
                }
                return ((0x1 << (button - 1)) & _joystickButtons[stick].buttons) != 0;
            }
        }

        public int GetStickButtonCount(int stick)
        {
            if (stick < 0 || stick >= kJoystickPorts)
            {
                throw new SystemException("Joystick index is out of range, should be 0-5");
            }

            lock (_mutex)
            {
                return _joystickButtons[stick].count;
            }
        }

        /**
     * Gets a value indicating whether the Driver Station requires the
     * robot to be enabled.
     *
     * @return True if the robot is enabled, false otherwise.
     */

        public bool IsEnabled()
        {
            HALControlWord controlWord = HAL.GetControlWord();
            return controlWord.GetEnabled() && controlWord.GetDSAttached();
        }

        /**
         * Gets a value indicating whether the Driver Station requires the
         * robot to be disabled.
         *
         * @return True if the robot should be disabled, false otherwise.
         */

        public bool IsDisabled()
        {
            return !IsEnabled();
        }

        /**
         * Gets a value indicating whether the Driver Station requires the
         * robot to be running in autonomous mode.
         *
         * @return True if autonomous mode should be enabled, false otherwise.
         */

        public bool IsAutonomous()
        {
            HALControlWord controlWord = HAL.GetControlWord();
            return controlWord.GetAutonomous();
        }

        /**
         * Gets a value indicating whether the Driver Station requires the
         * robot to be running in test mode.
         * @return True if test mode should be enabled, false otherwise.
         */

        public bool IsTest()
        {
            HALControlWord controlWord = HAL.GetControlWord();
            return controlWord.GetTest();
        }

        /**
         * Gets a value indicating whether the Driver Station requires the
         * robot to be running in operator-controlled mode.
         *
         * @return True if operator-controlled mode should be enabled, false otherwise.
         */

        public bool IsOperatorControl()
        {
            return !(IsAutonomous() || IsTest());
        }

        public bool IsSysActive()
        {
            int status = 0;
            bool retVal = HAL.GetSystemActive(ref status);
            return retVal;
        }

        public bool IsBrownedOut()
        {
            int status = 0;
            bool retval = HAL.GetBrownedOut(ref status);
            return retval;
        }

        public bool IsNewControlData()
        {
            lock (_mutex)
            {
                bool result = _newControlData;
                _newControlData = false;
                return result;
            }
        }

        public Alliance GetAlliance()
        {
            HALAllianceStationID allianceStationID = new HALAllianceStationID();
            HAL.GetAllianceStation(ref allianceStationID);

            switch (allianceStationID)
            {
                case HALAllianceStationID.kHALAllianceStationID_red1:
                case HALAllianceStationID.kHALAllianceStationID_red2:
                case HALAllianceStationID.kHALAllianceStationID_red3:
                    return Alliance.Red;

                case HALAllianceStationID.kHALAllianceStationID_blue1:
                case HALAllianceStationID.kHALAllianceStationID_blue2:
                case HALAllianceStationID.kHALAllianceStationID_blue3:
                    return Alliance.Blue;

                default:
                    return Alliance.Invalid;
            }
        }

        public int GetLocation()
        {
            HALAllianceStationID allianceStationID = new HALAllianceStationID();
            HAL.GetAllianceStation(ref allianceStationID);

            switch (allianceStationID)
            {
                case HALAllianceStationID.kHALAllianceStationID_red1:
                case HALAllianceStationID.kHALAllianceStationID_blue1:
                    return 1;

                case HALAllianceStationID.kHALAllianceStationID_red2:
                case HALAllianceStationID.kHALAllianceStationID_blue2:
                    return 2;

                case HALAllianceStationID.kHALAllianceStationID_red3:
                case HALAllianceStationID.kHALAllianceStationID_blue3:
                    return 3;

                default:
                    return 0;
            }
        }

        public bool IsFMSAttached()
        {
            HALControlWord controlWord = HAL.GetControlWord();
            return controlWord.GetFMSAttached();
        }

        public bool IsDSAttached()
        {
            HALControlWord controlWord = HAL.GetControlWord();
            return controlWord.GetDSAttached();
        }

        public double GetMatchTime()
        {
            float temp = 0;
            HAL.GetMatchTime(ref temp);
            return temp;
        }



        public static void ReportError(String error, bool printTrace)
        {
            String errorString = error;
            if (printTrace)
            {
                //errorString += " at ";
                //Add stack trace code
            }
            TextWriter errorWriter = Console.Error;
            errorWriter.WriteLine(errorString);
            errorWriter.Close();


            HALControlWord controlWord = HAL.GetControlWord();
            if (controlWord.GetDSAttached())
            {
                HAL.SetErrorData(errorString, 0);
            }
        }

        /** Only to be used to tell the Driver Station what code you claim to be executing
     *   for diagnostic purposes only
     * @param entering If true, starting disabled code; if false, leaving disabled code */

        public void InDisabled(bool entering)
        {
            _userInDisabled = entering;
        }

        /** Only to be used to tell the Driver Station what code you claim to be executing
        *   for diagnostic purposes only
         * @param entering If true, starting autonomous code; if false, leaving autonomous code */

        public void InAutonomous(bool entering)
        {
            _userInAutonomous = entering;
        }

        /** Only to be used to tell the Driver Station what code you claim to be executing
        *   for diagnostic purposes only
         * @param entering If true, starting teleop code; if false, leaving teleop code */

        public void InOperatorControl(bool entering)
        {
            _userInTeleop = entering;
        }

        /** Only to be used to tell the Driver Station what code you claim to be executing
         *   for diagnostic purposes only
         * @param entering If true, starting test code; if false, leaving test code */

        public void InTest(bool entering)
        {
            _userInTest = entering;
        }
    }
}
