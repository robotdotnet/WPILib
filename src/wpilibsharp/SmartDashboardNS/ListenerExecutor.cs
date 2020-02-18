using System;
using System.Collections.Generic;

namespace WPILib.SmartDashboardNS
{
    internal class ListenerExecutor
    {
        private List<Action> m_tasks = new List<Action>();
        private List<Action> m_cacheTasks = new List<Action>();
        private readonly object m_lock = new object();

        public void Execute(Action task)
        {
            lock (m_lock)
            {
                m_tasks.Add(task);
            }
        }

        public void RunListenerTasks()
        {
            List<Action>? tasks = null;
            lock (m_lock)
            {
                tasks = m_tasks;
                m_tasks = m_cacheTasks;
                m_cacheTasks = tasks;
            }

            foreach (var task in tasks)
            {
                task();
            }
            tasks.Clear();
        }
    }
}
