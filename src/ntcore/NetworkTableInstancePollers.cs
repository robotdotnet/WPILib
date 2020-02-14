using NetworkTables.Natives;
using System;
using System.Collections.Generic;
using System.Threading;

namespace NetworkTables
{
    public unsafe partial class NetworkTableInstance
    {
        private readonly object m_entryListenerLock = new object();
        private readonly Dictionary<NtEntryListener, EntryNotificationDelegate> m_entryListeners = new Dictionary<NtEntryListener, EntryNotificationDelegate>();
        private readonly Lazy<CancellationTokenSource> m_entryListenerToken;
        private Thread? m_entryListenerThread;
        private NtEntryListenerPoller m_entryListenerPoller;
        private readonly object m_entryListenerWaitQueueLock = new object();
        private bool m_entryListenerWaitQueue = false;

        private CancellationTokenSource CreateEntryListenerThread()
        {
            m_entryListenerPoller = NtCore.CreateEntryListenerPoller(Handle);
            CancellationTokenSource source = new CancellationTokenSource();
            var ret = new Thread(() =>
            {
                bool wasInterrupted = false;
                var token = source.Token;
                token.Register(() =>
                {
                    NtCore.CancelPollEntryListener(m_entryListenerPoller);
                });
                while (!token.IsCancellationRequested)
                {
                    var events = NtCore.PollEntryListener(m_entryListenerPoller);
                    if (token.IsCancellationRequested)
                    {
                        NtCore.DisposeEntryListenerSpan(events);
                        break;
                    }
                    if (events.Length == 0)
                    {
                        NtCore.DisposeEntryListenerSpan(events);
                        lock (m_entryListenerWaitQueueLock)
                        {
                            if (m_entryListenerWaitQueue)
                            {
                                m_entryListenerWaitQueue = false;
                                Monitor.PulseAll(m_entryListenerWaitQueueLock);
                                continue;
                            }
                        }
                        wasInterrupted = true;
                        break;
                    }
                    for (int i = 0; i < events.Length; i++)
                    {
                        EntryNotificationDelegate? listener;
                        lock (m_entryListenerLock)
                        {
                            m_entryListeners.TryGetValue(events.Pointer[i].listener, out listener);
                        }
                        if (listener != null)
                        {
                            listener(new RefEntryNotification(this, events.Pointer[i]));
                            if (token.IsCancellationRequested)
                            {
                                break;
                            }
                        }
                    }
                    NtCore.DisposeEntryListenerSpan(events);
                }
                lock (m_entryListenerWaitQueueLock)
                {
                    if (!wasInterrupted)
                    {
                        NtCore.DestroyEntryListenerPoller(m_entryListenerPoller);
                    }
                    m_entryListenerPoller = new NtEntryListenerPoller();
                }
            })
            {
                Name = "NTEntryListener",
                IsBackground = true
            };
            ret.Start();
            m_entryListenerThread = ret;
            return source;
        }

        public NtEntryListener AddEntryListener(string prefix, EntryNotificationDelegate listener, NotifyFlags flags)
        {
            var token = m_entryListenerToken.Value;
            lock (m_entryListenerLock)
            {
                var handle = NtCore.AddPolledEntryListener(m_entryListenerPoller, prefix, flags);
                m_entryListeners.Add(handle, listener);
                return handle;
            }
        }

        public NtEntryListener AddEntryListener(in NetworkTableEntry entry, EntryNotificationDelegate listener, NotifyFlags flags)
        {
            var token = m_entryListenerToken.Value;
            lock (m_entryListenerLock)
            {
                var handle = NtCore.AddPolledEntryListener(m_entryListenerPoller, entry, flags);
                m_entryListeners.Add(handle, listener);
                return handle;
            }
        }

        public void RemoveEntryListener(NtEntryListener listener)
        {
            NtCore.RemoveEntryListener(listener);
            lock (m_entryListenerLock)
            {
                m_entryListeners.Remove(listener);
            }
        }

        public bool WaitForEntryListenerQueue(double timeout)
        {
            if (!NtCore.WaitForEntryListenerQueue(Handle, timeout))
            {
                return false;
            }
            lock (m_entryListenerWaitQueueLock)
            {
                if (m_entryListenerPoller.Get() == 0) return true;
                m_entryListenerWaitQueue = true;
                NtCore.CancelPollEntryListener(m_entryListenerPoller);
                while (m_entryListenerWaitQueue)
                {
                    if (timeout < 0)
                    {
                        Monitor.Wait(m_entryListenerWaitQueueLock);
                    }
                    else
                    {
                        return Monitor.Wait(m_entryListenerWaitQueueLock, TimeSpan.FromSeconds(timeout));
                    }
                }
            }
            return true;
        }



        private readonly object m_connectionListenerLock = new object();
        private readonly Dictionary<NtConnectionListener, ConnectionNotificationDelegate> m_connectionListeners = new Dictionary<NtConnectionListener, ConnectionNotificationDelegate>();
        private readonly Lazy<CancellationTokenSource> m_connectionListenerToken;
        private Thread? m_connectionListenerThread;
        private NtConnectionListenerPoller m_connectionListenerPoller;
        private readonly object m_connectionListenerWaitQueueLock = new object();
        private bool m_connectionListenerWaitQueue = false;

        private CancellationTokenSource CreateConnectionListenerThread()
        {
            m_connectionListenerPoller = NtCore.CreateConnectionListenerPoller(Handle);
            CancellationTokenSource source = new CancellationTokenSource();
            var ret = new Thread(() =>
            {
                bool wasInterrupted = false;
                var token = source.Token;
                token.Register(() =>
                {
                    NtCore.CancelPollConnectionListener(m_connectionListenerPoller);
                });
                while (!token.IsCancellationRequested)
                {
                    var events = NtCore.PollConnectionListener(m_connectionListenerPoller);
                    if (token.IsCancellationRequested)
                    {
                        NtCore.DisposeConnectionListenerSpan(events);
                        break;
                    }
                    if (events.Length == 0)
                    {
                        NtCore.DisposeConnectionListenerSpan(events);
                        lock (m_connectionListenerWaitQueueLock)
                        {
                            if (m_entryListenerWaitQueue)
                            {
                                m_entryListenerWaitQueue = false;
                                Monitor.PulseAll(m_connectionListenerWaitQueueLock);
                                continue;
                            }
                        }
                        wasInterrupted = true;
                        break;
                    }
                    for (int i = 0; i < events.Length; i++)
                    {
                        ConnectionNotificationDelegate? listener;
                        lock (m_connectionListenerLock)
                        {
                            m_connectionListeners.TryGetValue(events.Pointer[i].listener, out listener);
                        }
                        if (listener != null)
                        {
                            listener(new ConnectionNotification(this, events.Pointer[i]));
                            if (token.IsCancellationRequested)
                            {
                                break;
                            }
                        }
                    }
                    NtCore.DisposeConnectionListenerSpan(events);
                }
                lock (m_connectionListenerWaitQueueLock)
                {
                    if (!wasInterrupted)
                    {
                        NtCore.DestroyConnectionListenerPoller(m_connectionListenerPoller);
                    }
                    m_connectionListenerPoller = new NtConnectionListenerPoller();
                }
            })
            {
                Name = "NTConnectionListener",
                IsBackground = true
            };
            ret.Start();
            m_connectionListenerThread = ret;
            return source;
        }

        public NtConnectionListener AddConnectionListener(ConnectionNotificationDelegate listener, bool immediateNotify)
        {
            var token = m_connectionListenerToken.Value;
            lock (m_connectionListenerLock)
            {
                var handle = NtCore.AddPolledConnectionListener(m_connectionListenerPoller, immediateNotify);
                m_connectionListeners.Add(handle, listener);
                return handle;
            }
        }

        public void RemoveConnectionListener(NtConnectionListener listener)
        {
            NtCore.RemoveConnectionListener(listener);
            lock (m_connectionListenerLock)
            {
                m_connectionListeners.Remove(listener);
            }
        }

        public bool WaitForConnectionListenerQueue(double timeout)
        {
            if (!NtCore.WaitForConnectionListenerQueue(Handle, timeout))
            {
                return false;
            }
            lock (m_connectionListenerWaitQueueLock)
            {
                if (m_connectionListenerPoller.Get() == 0) return true;
                m_connectionListenerWaitQueue = true;
                NtCore.CancelPollConnectionListener(m_connectionListenerPoller);
                while (m_connectionListenerWaitQueue)
                {
                    if (timeout < 0)
                    {
                        Monitor.Wait(m_connectionListenerWaitQueueLock);
                    }
                    else
                    {
                        return Monitor.Wait(m_connectionListenerWaitQueueLock, TimeSpan.FromSeconds(timeout));
                    }
                }
            }
            return true;
        }

        private readonly object m_rpcCallLock = new object();
        private readonly Dictionary<NtEntry, RpcAnswerDelegate> m_rpcCalls = new Dictionary<NtEntry, RpcAnswerDelegate>();
        private readonly Lazy<CancellationTokenSource> m_rpcListenerToken;
        private Thread? m_rpcListenerThread;
        private NtRpcCallPoller m_rpcListenerPoller;
        private readonly object m_rpcListenerWaitQueueLock = new object();
        private bool m_rpcListenerWaitQueue = false;

        private CancellationTokenSource CreateRpcListenerThread()
        {
            m_rpcListenerPoller = NtCore.CreateRpcCallPoller(Handle);
            CancellationTokenSource source = new CancellationTokenSource();
            var ret = new Thread(() =>
            {
                bool wasInterrupted = false;
                var token = source.Token;
                token.Register(() =>
                {
                    NtCore.CancelPollRpc(m_rpcListenerPoller);
                });
                Span<bool> respondedStore = stackalloc bool[1] { false };
                while (!token.IsCancellationRequested)
                {
                    var events = NtCore.PollRpc(m_rpcListenerPoller);
                    if (token.IsCancellationRequested)
                    {
                        NtCore.DisposeRpcAnswerSpan(events);
                        break;
                    }
                    if (events.Length == 0)
                    {
                        NtCore.DisposeRpcAnswerSpan(events);
                        lock (m_rpcListenerWaitQueueLock)
                        {
                            if (m_entryListenerWaitQueue)
                            {
                                m_entryListenerWaitQueue = false;
                                Monitor.PulseAll(m_rpcListenerWaitQueueLock);
                                continue;
                            }
                        }
                        wasInterrupted = true;
                        break;
                    }
                    for (int i = 0; i < events.Length; i++)
                    {
                        RpcAnswerDelegate? listener;
                        lock (m_rpcCallLock)
                        {
                            m_rpcCalls.TryGetValue(events.Pointer[i].entry, out listener);
                        }
                        if (listener != null)
                        {
                            respondedStore[0] = false;
                            var evnt = new RpcAnswer(this, events.Pointer[i], respondedStore);
                            listener(evnt);
                            if (!respondedStore[0])
                            {
                                evnt.PostResponse(Span<byte>.Empty);
                            }

                            if (token.IsCancellationRequested)
                            {
                                break;
                            }
                        }
                    }
                    NtCore.DisposeRpcAnswerSpan(events);
                }
                lock (m_rpcListenerWaitQueueLock)
                {
                    if (!wasInterrupted)
                    {
                        NtCore.DestroyRpcCallPoller(m_rpcListenerPoller);
                    }
                    m_rpcListenerPoller = new NtRpcCallPoller();
                }
            })
            {
                Name = "NTRpcCall",
                IsBackground = true
            };
            ret.Start();
            m_rpcListenerThread = ret;
            return source;
        }

        public void CreateRpc(in NetworkTableEntry entry, RpcAnswerDelegate callback)
        {
            var token = m_rpcListenerToken.Value;
            Span<byte> def = stackalloc byte[1] { 0 };
            lock (m_rpcCallLock)
            {
                NtCore.CreatePolledRpc(entry.Handle, def, m_rpcListenerPoller);
                m_rpcCalls.Add(entry.Handle, callback);
            }
        }

        public bool WaitForRpcCallQueue(double timeout)
        {
            if (!NtCore.WaitForRpcCallQueue(Handle, timeout))
            {
                return false;
            }
            lock (m_rpcListenerWaitQueueLock)
            {
                if (m_rpcListenerPoller.Get() == 0) return true;
                m_rpcListenerWaitQueue = true;
                NtCore.CancelPollRpc(m_rpcListenerPoller);
                while (m_rpcListenerWaitQueue)
                {
                    if (timeout < 0)
                    {
                        Monitor.Wait(m_rpcListenerWaitQueueLock);
                    }
                    else
                    {
                        return Monitor.Wait(m_rpcListenerWaitQueueLock, TimeSpan.FromSeconds(timeout));
                    }
                }
            }
            return true;
        }






        private readonly object m_loggerListenerLock = new object();
        private readonly Dictionary<NtLogger, LogMessageDelegate> m_loggerListeners = new Dictionary<NtLogger, LogMessageDelegate>();
        private readonly Lazy<CancellationTokenSource> m_loggerListenerToken;
        private Thread? m_loggerListenerThread;
        private NtLoggerPoller m_loggerListenerPoller;
        private readonly object m_loggerListenerWaitQueueLock = new object();
        private bool m_loggerListenerWaitQueue = false;

        private CancellationTokenSource CreateLoggerThread()
        {
            m_loggerListenerPoller = NtCore.CreateLoggerPoller(Handle);
            CancellationTokenSource source = new CancellationTokenSource();
            var ret = new Thread(() =>
            {
                bool wasInterrupted = false;
                var token = source.Token;
                token.Register(() =>
                {
                    NtCore.CancelPollLogger(m_loggerListenerPoller);
                });
                while (!token.IsCancellationRequested)
                {
                    var events = NtCore.PollLogger(m_loggerListenerPoller);
                    if (token.IsCancellationRequested)
                    {
                        NtCore.DisposeLoggerSpan(events);
                        break;
                    }
                    if (events.Length == 0)
                    {
                        NtCore.DisposeLoggerSpan(events);
                        lock (m_loggerListenerWaitQueueLock)
                        {
                            if (m_entryListenerWaitQueue)
                            {
                                m_entryListenerWaitQueue = false;
                                Monitor.PulseAll(m_loggerListenerWaitQueueLock);
                                continue;
                            }
                        }
                        wasInterrupted = true;
                        break;
                    }
                    for (int i = 0; i < events.Length; i++)
                    {
                        LogMessageDelegate? listener;
                        lock (m_loggerListenerLock)
                        {
                            m_loggerListeners.TryGetValue(events.Pointer[i].logger, out listener);
                        }
                        if (listener != null)
                        {
                            listener(new LogMessage(this, events.Pointer[i]));
                            if (token.IsCancellationRequested)
                            {
                                break;
                            }
                        }
                    }
                    NtCore.DisposeLoggerSpan(events);
                }
                lock (m_loggerListenerWaitQueueLock)
                {
                    if (!wasInterrupted)
                    {
                        NtCore.DestroyLoggerPoller(m_loggerListenerPoller);
                    }
                    m_loggerListenerPoller = new NtLoggerPoller();
                }
            })
            {
                Name = "NTLogger",
                IsBackground = true
            };
            ret.Start();
            m_loggerListenerThread = ret;
            return source;
        }

        public NtLogger AddLogger(LogMessageDelegate listener, int minLevel, int maxLevel)
        {
            var token = m_loggerListenerToken.Value;
            lock (m_loggerListenerLock)
            {
                var handle = NtCore.AddPolledLogger(m_loggerListenerPoller, minLevel, maxLevel);
                m_loggerListeners.Add(handle, listener);
                return handle;
            }
        }

        public void RemoveLogger(NtLogger listener)
        {
            NtCore.RemoveLogger(listener);
            lock (m_loggerListenerLock)
            {
                m_loggerListeners.Remove(listener);
            }

        }

        public bool WaitForLoggerQueue(double timeout)
        {
            if (!NtCore.WaitForLoggerQueue(Handle, timeout))
            {
                return false;
            }
            lock (m_loggerListenerWaitQueueLock)
            {
                if (m_loggerListenerPoller.Get() == 0) return true;
                m_loggerListenerWaitQueue = true;
                NtCore.CancelPollLogger(m_loggerListenerPoller);
                while (m_loggerListenerWaitQueue)
                {
                    if (timeout < 0)
                    {
                        Monitor.Wait(m_loggerListenerWaitQueueLock);
                    }
                    else
                    {
                        return Monitor.Wait(m_loggerListenerWaitQueueLock, TimeSpan.FromSeconds(timeout));
                    }
                }
            }
            return true;
        }
    }
}
