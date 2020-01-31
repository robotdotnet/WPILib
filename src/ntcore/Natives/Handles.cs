using System;
using System.Runtime.CompilerServices;

namespace NetworkTables.Natives
{
    /// <summary>
    /// Low level NT Core Handle
    /// </summary>
    public readonly struct NtHandle : IEquatable<NtHandle>
    {
        private readonly int m_value;

        /// <summary>
        /// Create a handle from an int.
        /// </summary>
        /// <param name="value">handle value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtHandle(int value)
        {
            m_value = value;
        }

        /// <summary>
        /// Gets the raw handle value
        /// </summary>
        /// <returns>The raw handle value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get()
        {
            return m_value;
        }

        /// <summary>
        /// Checks equality between another object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is NtHandle v)
            {
                return Equals(v);
            }
            return false;
        }

        /// <summary>
        /// Checks equality between another Handle
        /// </summary>
        /// <param name="other">Handle to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NtHandle other)
        {
            return m_value == other.m_value;
        }

        /// <summary>
        /// Gets Hash Code of Handle
        /// </summary>
        /// <returns>Handle Value as Hash Code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        /// <summary>
        /// Gets if 2 handles are equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NtHandle lhs, NtHandle rhs)
        {
            return lhs.m_value == rhs.m_value;
        }

        /// <summary>
        /// Gets if 2 handles are not equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if not equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NtHandle lhs, NtHandle rhs)
        {
            return lhs.m_value != rhs.m_value;
        }
    }

    /// <summary>
    /// Low Level NT Core Instance Handle
    /// </summary>
    public readonly struct NtInst
    {
        private readonly NtHandle m_value;

        /// <summary>
        /// Creates a new handle
        /// </summary>
        /// <param name="value">handle value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtInst(int value)
        {
            m_value = new NtHandle(value);
        }

        /// <summary>
        /// Gets the raw handle value
        /// </summary>
        /// <returns>The raw handle value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get()
        {
            return m_value.Get();
        }

        /// <summary>
        /// Converts a handle to a base handle
        /// </summary>
        /// <param name="value">The current handle</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NtHandle(NtInst value)
        {
            return value.m_value;
        }

        /// <summary>
        /// Checks equality between another object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is NtInst v)
            {
                return Equals(v);
            }
            return false;
        }

        /// <summary>
        /// Checks equality between another Handle
        /// </summary>
        /// <param name="other">Handle to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NtInst other)
        {
            return m_value == other.m_value;
        }

        /// <summary>
        /// Gets Hash Code of Handle
        /// </summary>
        /// <returns>Handle Value as Hash Code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        /// <summary>
        /// Gets if 2 handles are equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NtInst lhs, NtInst rhs)
        {
            return lhs.m_value == rhs.m_value;
        }

        /// <summary>
        /// Gets if 2 handles are not equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if not equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NtInst lhs, NtInst rhs)
        {
            return lhs.m_value != rhs.m_value;
        }
    }

    /// <summary>
    /// Low Level NT Core Entry Handle
    /// </summary>
    public readonly struct NtEntry : IEquatable<NtEntry>
    {
        private readonly NtHandle m_value;

        /// <summary>
        /// Creates a new handle
        /// </summary>
        /// <param name="value">handle value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntry(int value)
        {
            m_value = new NtHandle(value);
        }

        /// <summary>
        /// Gets the raw handle value
        /// </summary>
        /// <returns>The raw handle value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get()
        {
            return m_value.Get();
        }

        /// <summary>
        /// Converts a handle to a base handle
        /// </summary>
        /// <param name="value">The current handle</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NtHandle(NtEntry value)
        {
            return value.m_value;
        }

        /// <summary>
        /// Checks equality between another object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is NtEntry v)
            {
                return Equals(v);
            }
            return false;
        }

        /// <summary>
        /// Checks equality between another Handle
        /// </summary>
        /// <param name="other">Handle to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NtEntry other)
        {
            return m_value == other.m_value;
        }

        /// <summary>
        /// Gets Hash Code of Handle
        /// </summary>
        /// <returns>Handle Value as Hash Code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        /// <summary>
        /// Gets if 2 handles are equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NtEntry lhs, NtEntry rhs)
        {
            return lhs.m_value == rhs.m_value;
        }

        /// <summary>
        /// Gets if 2 handles are not equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if not equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NtEntry lhs, NtEntry rhs)
        {
            return lhs.m_value != rhs.m_value;
        }
    }

    /// <summary>
    /// Low Level NT Core Entry Listener Handle
    /// </summary>
    public readonly struct NtEntryListener : IEquatable<NtEntryListener>
    {
        private readonly NtHandle m_value;

        /// <summary>
        /// Creates a new handle
        /// </summary>
        /// <param name="value">handle value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryListener(int value)
        {
            m_value = new NtHandle(value);
        }

        /// <summary>
        /// Gets the raw handle value
        /// </summary>
        /// <returns>The raw handle value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get()
        {
            return m_value.Get();
        }

        /// <summary>
        /// Converts a handle to a base handle
        /// </summary>
        /// <param name="value">The current handle</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NtHandle(NtEntryListener value)
        {
            return value.m_value;
        }

        /// <summary>
        /// Checks equality between another object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is NtEntryListener v)
            {
                return Equals(v);
            }
            return false;
        }

        /// <summary>
        /// Checks equality between another Handle
        /// </summary>
        /// <param name="other">Handle to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NtEntryListener other)
        {
            return m_value == other.m_value;
        }

        /// <summary>
        /// Gets Hash Code of Handle
        /// </summary>
        /// <returns>Handle Value as Hash Code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        /// <summary>
        /// Gets if 2 handles are equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NtEntryListener lhs, NtEntryListener rhs)
        {
            return lhs.m_value == rhs.m_value;
        }

        /// <summary>
        /// Gets if 2 handles are not equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if not equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NtEntryListener lhs, NtEntryListener rhs)
        {
            return lhs.m_value != rhs.m_value;
        }
    }

    /// <summary>
    /// Low Level NT Core Entry Poller Handle
    /// </summary>
    public readonly struct NtEntryListenerPoller : IEquatable<NtEntryListenerPoller>
    {
        private readonly NtHandle m_value;

        /// <summary>
        /// Creates a new handle
        /// </summary>
        /// <param name="value">handle value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtEntryListenerPoller(int value)
        {
            m_value = new NtHandle(value);
        }

        /// <summary>
        /// Gets the raw handle value
        /// </summary>
        /// <returns>The raw handle value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get()
        {
            return m_value.Get();
        }

        /// <summary>
        /// Converts a handle to a base handle
        /// </summary>
        /// <param name="value">The current handle</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NtHandle(NtEntryListenerPoller value)
        {
            return value.m_value;
        }

        /// <summary>
        /// Checks equality between another object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is NtEntryListenerPoller v)
            {
                return Equals(v);
            }
            return false;
        }

        /// <summary>
        /// Checks equality between another Handle
        /// </summary>
        /// <param name="other">Handle to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NtEntryListenerPoller other)
        {
            return m_value == other.m_value;
        }

        /// <summary>
        /// Gets Hash Code of Handle
        /// </summary>
        /// <returns>Handle Value as Hash Code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        /// <summary>
        /// Gets if 2 handles are equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NtEntryListenerPoller lhs, NtEntryListenerPoller rhs)
        {
            return lhs.m_value == rhs.m_value;
        }

        /// <summary>
        /// Gets if 2 handles are not equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if not equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NtEntryListenerPoller lhs, NtEntryListenerPoller rhs)
        {
            return lhs.m_value != rhs.m_value;
        }
    }

    /// <summary>
    /// Low Level NT Core Connection Listener Handle
    /// </summary>
    public readonly struct NtConnectionListener : IEquatable<NtConnectionListener>
    {
        private readonly NtHandle m_value;

        /// <summary>
        /// Creates a new handle
        /// </summary>
        /// <param name="value">handle value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtConnectionListener(int value)
        {
            m_value = new NtHandle(value);
        }

        /// <summary>
        /// Gets the raw handle value
        /// </summary>
        /// <returns>The raw handle value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get()
        {
            return m_value.Get();
        }

        /// <summary>
        /// Converts a handle to a base handle
        /// </summary>
        /// <param name="value">The current handle</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NtHandle(NtConnectionListener value)
        {
            return value.m_value;
        }

        /// <summary>
        /// Checks equality between another object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is NtConnectionListener v)
            {
                return Equals(v);
            }
            return false;
        }

        /// <summary>
        /// Checks equality between another Handle
        /// </summary>
        /// <param name="other">Handle to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NtConnectionListener other)
        {
            return m_value == other.m_value;
        }

        /// <summary>
        /// Gets Hash Code of Handle
        /// </summary>
        /// <returns>Handle Value as Hash Code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        /// <summary>
        /// Gets if 2 handles are equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NtConnectionListener lhs, NtConnectionListener rhs)
        {
            return lhs.m_value == rhs.m_value;
        }

        /// <summary>
        /// Gets if 2 handles are not equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if not equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NtConnectionListener lhs, NtConnectionListener rhs)
        {
            return lhs.m_value != rhs.m_value;
        }
    }

    /// <summary>
    /// Low Level NT Core Connection Listener Poller Handle
    /// </summary>
    public readonly struct NtConnectionListenerPoller : IEquatable<NtConnectionListenerPoller>
    {
        private readonly NtHandle m_value;

        /// <summary>
        /// Creates a new handle
        /// </summary>
        /// <param name="value">handle value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtConnectionListenerPoller(int value)
        {
            m_value = new NtHandle(value);
        }

        /// <summary>
        /// Gets the raw handle value
        /// </summary>
        /// <returns>The raw handle value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get()
        {
            return m_value.Get();
        }

        /// <summary>
        /// Converts a handle to a base handle
        /// </summary>
        /// <param name="value">The current handle</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NtHandle(NtConnectionListenerPoller value)
        {
            return value.m_value;
        }

        /// <summary>
        /// Checks equality between another object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is NtConnectionListenerPoller v)
            {
                return Equals(v);
            }
            return false;
        }

        /// <summary>
        /// Checks equality between another Handle
        /// </summary>
        /// <param name="other">Handle to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NtConnectionListenerPoller other)
        {
            return m_value == other.m_value;
        }

        /// <summary>
        /// Gets Hash Code of Handle
        /// </summary>
        /// <returns>Handle Value as Hash Code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        /// <summary>
        /// Gets if 2 handles are equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NtConnectionListenerPoller lhs, NtConnectionListenerPoller rhs)
        {
            return lhs.m_value == rhs.m_value;
        }

        /// <summary>
        /// Gets if 2 handles are not equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if not equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NtConnectionListenerPoller lhs, NtConnectionListenerPoller rhs)
        {
            return lhs.m_value != rhs.m_value;
        }
    }

    /// <summary>
    /// Low Level NT Core Logger Handle
    /// </summary>
    public readonly struct NtLogger : IEquatable<NtLogger>
    {
        private readonly NtHandle m_value;

        /// <summary>
        /// Creates a new handle
        /// </summary>
        /// <param name="value">handle value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtLogger(int value)
        {
            m_value = new NtHandle(value);
        }

        /// <summary>
        /// Gets the raw handle value
        /// </summary>
        /// <returns>The raw handle value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get()
        {
            return m_value.Get();
        }

        /// <summary>
        /// Converts a handle to a base handle
        /// </summary>
        /// <param name="value">The current handle</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NtHandle(NtLogger value)
        {
            return value.m_value;
        }

        /// <summary>
        /// Checks equality between another object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is NtLogger v)
            {
                return Equals(v);
            }
            return false;
        }

        /// <summary>
        /// Checks equality between another Handle
        /// </summary>
        /// <param name="other">Handle to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NtLogger other)
        {
            return m_value == other.m_value;
        }

        /// <summary>
        /// Gets Hash Code of Handle
        /// </summary>
        /// <returns>Handle Value as Hash Code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        /// <summary>
        /// Gets if 2 handles are equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NtLogger lhs, NtLogger rhs)
        {
            return lhs.m_value == rhs.m_value;
        }

        /// <summary>
        /// Gets if 2 handles are not equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if not equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NtLogger lhs, NtLogger rhs)
        {
            return lhs.m_value != rhs.m_value;
        }
    }

    /// <summary>
    /// Low Level NT Core Logger Poller Handle
    /// </summary>
    public readonly struct NtLoggerPoller : IEquatable<NtLoggerPoller>
    {
        private readonly NtHandle m_value;

        /// <summary>
        /// Creates a new handle
        /// </summary>
        /// <param name="value">handle value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtLoggerPoller(int value)
        {
            m_value = new NtHandle(value);
        }

        /// <summary>
        /// Gets the raw handle value
        /// </summary>
        /// <returns>The raw handle value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get()
        {
            return m_value.Get();
        }

        /// <summary>
        /// Converts a handle to a base handle
        /// </summary>
        /// <param name="value">The current handle</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NtHandle(NtLoggerPoller value)
        {
            return value.m_value;
        }

        /// <summary>
        /// Checks equality between another object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is NtLoggerPoller v)
            {
                return Equals(v);
            }
            return false;
        }

        /// <summary>
        /// Checks equality between another Handle
        /// </summary>
        /// <param name="other">Handle to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NtLoggerPoller other)
        {
            return m_value == other.m_value;
        }

        /// <summary>
        /// Gets Hash Code of Handle
        /// </summary>
        /// <returns>Handle Value as Hash Code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        /// <summary>
        /// Gets if 2 handles are equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NtLoggerPoller lhs, NtLoggerPoller rhs)
        {
            return lhs.m_value == rhs.m_value;
        }

        /// <summary>
        /// Gets if 2 handles are not equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if not equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NtLoggerPoller lhs, NtLoggerPoller rhs)
        {
            return lhs.m_value != rhs.m_value;
        }
    }

    /// <summary>
    /// Low Level NT Core Rpc Call Handle
    /// </summary>
    public readonly struct NtRpcCall : IEquatable<NtRpcCall>
    {
        private readonly NtHandle m_value;

        /// <summary>
        /// Creates a new handle
        /// </summary>
        /// <param name="value">handle value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtRpcCall(int value)
        {
            m_value = new NtHandle(value);
        }

        /// <summary>
        /// Gets the raw handle value
        /// </summary>
        /// <returns>The raw handle value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get()
        {
            return m_value.Get();
        }

        /// <summary>
        /// Converts a handle to a base handle
        /// </summary>
        /// <param name="value">The current handle</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NtHandle(NtRpcCall value)
        {
            return value.m_value;
        }

        /// <summary>
        /// Checks equality between another object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is NtRpcCall v)
            {
                return Equals(v);
            }
            return false;
        }

        /// <summary>
        /// Checks equality between another Handle
        /// </summary>
        /// <param name="other">Handle to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NtRpcCall other)
        {
            return m_value == other.m_value;
        }

        /// <summary>
        /// Gets Hash Code of Handle
        /// </summary>
        /// <returns>Handle Value as Hash Code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        /// <summary>
        /// Gets if 2 handles are equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NtRpcCall lhs, NtRpcCall rhs)
        {
            return lhs.m_value == rhs.m_value;
        }

        /// <summary>
        /// Gets if 2 handles are not equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if not equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NtRpcCall lhs, NtRpcCall rhs)
        {
            return lhs.m_value != rhs.m_value;
        }
    }

    /// <summary>
    /// Low Level NT Core Rpc Call Poller Handle
    /// </summary>
    public readonly struct NtRpcCallPoller : IEquatable<NtRpcCallPoller>
    {
        private readonly NtHandle m_value;

        /// <summary>
        /// Creates a new handle
        /// </summary>
        /// <param name="value">handle value</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public NtRpcCallPoller(int value)
        {
            m_value = new NtHandle(value);
        }

        /// <summary>
        /// Gets the raw handle value
        /// </summary>
        /// <returns>The raw handle value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Get()
        {
            return m_value.Get();
        }

        /// <summary>
        /// Converts a handle to a base handle
        /// </summary>
        /// <param name="value">The current handle</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator NtHandle(NtRpcCallPoller value)
        {
            return value.m_value;
        }

        /// <summary>
        /// Checks equality between another object
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is NtRpcCallPoller v)
            {
                return Equals(v);
            }
            return false;
        }

        /// <summary>
        /// Checks equality between another Handle
        /// </summary>
        /// <param name="other">Handle to check</param>
        /// <returns>True if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(NtRpcCallPoller other)
        {
            return m_value == other.m_value;
        }

        /// <summary>
        /// Gets Hash Code of Handle
        /// </summary>
        /// <returns>Handle Value as Hash Code</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }

        /// <summary>
        /// Gets if 2 handles are equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(NtRpcCallPoller lhs, NtRpcCallPoller rhs)
        {
            return lhs.m_value == rhs.m_value;
        }

        /// <summary>
        /// Gets if 2 handles are not equal
        /// </summary>
        /// <param name="lhs">Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns>true if not equal</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(NtRpcCallPoller lhs, NtRpcCallPoller rhs)
        {
            return lhs.m_value != rhs.m_value;
        }
    }
}
