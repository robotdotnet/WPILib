﻿using System;
using System.Collections.Generic;
using NetworkTablesDotNet.NetworkTables;
using NetworkTablesDotNet.Tables;
using WPILib.Commands;

namespace WPILib.livewindow
{
    /// <summary>
    /// A LiveWindow component is a device (sensor or actuator) that should be added to the
    /// <para /> SmartDashboard in test mode. The components are cached until the first time the robot
    /// <para /> enters Test mode. This allows the components to be inserted, then renamed.
    /// </summary>
    internal class LiveWindowComponent
    {
        private string m_subsystem;
        private string m_name;
        private bool m_isSensor;

        public LiveWindowComponent(string subsystem, string name, bool isSensor)
        {
            m_isSensor = isSensor;
            m_subsystem = subsystem;
            m_name = name;
        }

        public string GetName()
        {
            return m_name;
        }

        public string GetSubsystem()
        {
            return m_subsystem;
        }

        public bool IsSensor()
        {
            return m_isSensor;
        }
    }

    /// <summary>
    /// The LiveWindow class is trhe public interface for putting sensor and actuators on the LiveWindow.
    /// </summary>
    public class LiveWindow
    {
        private static List<LiveWindowSendable> s_sensors = new List<LiveWindowSendable>();
        private static Dictionary<LiveWindowSendable, LiveWindowComponent> s_components = new Dictionary<LiveWindowSendable, LiveWindowComponent>();
        private static ITable s_liveWindowTable;
        private static ITable s_statusTable;
        private static bool s_liveWindowEnabled = false;
        private static bool s_firstTime = true;

        /// <summary>
        /// Initialize all the LiveWindow elements the first time we enter LiveWindow
        /// <para /> mode. By holding off creating the NetworkTable entries, it allows them to
        /// <para /> be redefined before the first time in LiveWindow mode. This allows
        /// <para /> default sensor and actuator values to be created that are replaced with
        /// <para /> the custom names from users calling addActuator and addSensor.
        /// </summary>
        private static void InitializeLiveWindowComponents()
        {
            Console.WriteLine("Initializing the components  first time");
            s_liveWindowTable = NetworkTable.GetTable("LiveWindow");
            s_statusTable = s_liveWindowTable.GetSubTable("~STATUS~");
            foreach (var component in s_components.Keys)
            {
                LiveWindowComponent c = s_components[component];
                string subsystem = c.GetSubsystem();
                string name = c.GetName();
                Console.WriteLine("Initializing table for '" + subsystem + "' '" + name + "'");
                s_liveWindowTable.GetSubTable(subsystem).PutString("~TYPE~", "LW Subsystem");
                ITable table = s_liveWindowTable.GetSubTable(subsystem).GetSubTable(name);
                table.PutString("~TYPE~", component.SmartDashboardType);
                table.PutString("Name", name);
                table.PutString("Subsystem", subsystem);
                component.InitTable(table);
                if (c.IsSensor())
                {
                    s_sensors.Add(component);
                }
            }
        }

        /// <summary>
        /// Set the enabled state of LiveWindow. If it's being enabled, turn off the
        /// <para /> scheduler and remove all the commands from the queue and enable all the
        /// <para /> components registered for LiveWindow. If it's being disabled, stop all
        /// <para /> the registered components and reenable the scheduler. TODO: add code to
        /// <para /> disable PID loops when enabling LiveWindow. The commands should reenable
        /// <para /> the PID loops themselves when they get rescheduled. This prevents arms
        /// <para /> from starting to move around, etc. after a period of adjusting them in
        /// <para /> LiveWindow mode.
        /// </summary>
        /// <param name="enabled"></param>
        public static void SetEnabled(bool enabled)
        {
            if (s_liveWindowEnabled != enabled)
            {
                if (enabled)
                {
                    Console.WriteLine("Starting live window mode.");
                    if (s_firstTime)
                    {
                        InitializeLiveWindowComponents();
                        s_firstTime = false;
                    }
                    Scheduler.GetInstance().Disable();
                    Scheduler.GetInstance().RemoveAll();
                    foreach (var component in s_components.Keys)
                    {
                        component.StartLiveWindowMode();
                    }
                }
                else
                {
                    Console.WriteLine("Stopping live window mode.");
                    foreach (var component in s_components.Keys)
                    {
                        component.StopLiveWindowMode();
                    }
                    Scheduler.GetInstance().Enable();
                }
                s_liveWindowEnabled = enabled;
                s_statusTable.PutBoolean("LW Enabled", enabled);
            }
        }

        /// <summary>
        /// The run method is called repeatedly to keep the values refreshed on the screen in test mode.
        /// </summary>
        public static void Run()
        {
            UpdateValues();
        }

        /// <summary>
        /// Add a Sensor associated with the subsystem and with call it by the given name.
        /// </summary>
        /// <param name="subsystem">The subsystem this component is part of.</param>
        /// <param name="name">The name of this component.</param>
        /// <param name="component">A LiveWindowSendable component that represents a sensor.</param>
        public static void AddSensor(string subsystem, string name, LiveWindowSendable component)
        {
            s_components.Add(component, new LiveWindowComponent(subsystem, name, true));
        }

        /// <summary>
        /// Add an Actuator associated with the subsystem and with call it by the given name.
        /// </summary>
        /// <param name="subsystem">The subsystem this component is part of.</param>
        /// <param name="name">The name of this component.</param>
        /// <param name="component">A LiveWindowSendable component that represents an actuator.</param>
        public static void AddActuator(string subsystem, string name, LiveWindowSendable component)
        {
            s_components.Add(component, new LiveWindowComponent(subsystem, name, false));
        }

        /// <summary>
        /// Puts all sensor values on the live window
        /// </summary>
        private static void UpdateValues()
        {
            //TODO: Gross - needs to be sped up
            foreach (var lws in s_sensors)
            {
                lws.UpdateTable();
            }
        }

        /// <summary>
        /// Add Sensor to LiveWindow. The components are shown with the type and
        /// <para /> channel like this: Gyro[1] for a gyro object connected to the first
        /// <para /> analog channel.
        /// </summary>
        /// <param name="moduleType">A string indicating the type if the module used in the naming (above)</param>
        /// <param name="channel">The channel number the device is connected to</param>
        /// <param name="component">A reference to the object being added</param>
        public static void AddSensor(string moduleType, int channel, LiveWindowSendable component)
        {
            AddSensor("Ungrouped", moduleType + "[" + channel + "]", component);
            if (s_sensors.Contains(component))
            {
                s_sensors.Remove(component);
            }
            s_sensors.Add(component);
        }

        /// <summary>
        /// Add Actuator to LiveWindow. The components are shown with the module
        /// <para /> type, slot and channel like this: Servo[1,2] for a servo object connected
        /// <para /> to the first digital module and PWM port 2.
        /// </summary>
        /// <param name="moduleType">A string that defines the module name in the label for the value</param>
        /// <param name="channel">The channel number the device is connected to (usually PWM)</param>
        /// <param name="component">A reference to the object being added</param>
        public static void AddActuator(string moduleType, int channel, LiveWindowSendable component)
        {
            AddSensor("Ungrouped", moduleType + "[" + channel + "]", component);

        }

        /// <summary>
        /// Add Actuator to LiveWindow. The components are shown with the module
        /// <para /> type, slot and channel like this: Servo[1,2] for a servo object connected
        /// <para /> to the first digital module and PWM port 2.
        /// </summary>
        /// <param name="moduleType">A string that defines the module name in the label for the value</param>
        /// <param name="moduleNumber">The number of the particular module type</param>
        /// <param name="channel">The channel number the device is connected to (usually PWM)</param>
        /// <param name="component">A reference to the object being added</param>
        public static void AddActuator(string moduleType, int moduleNumber, int channel, LiveWindowSendable component)
        {
            AddSensor("Ungrouped", moduleType + "[" + moduleNumber + "," + channel + "]", component);
        }
    }
}
