using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using HAL_Base;
using NetworkTables;
using NetworkTables.Tables;

namespace WPILib
{
    /// <summary>
    /// Provides a relatively simple way to save important values to the RoboRIO to access the next
    /// time the RoboRIO is booted.
    /// </summary>
    /// <remarks>This class loads and saves from a file inside the RoboRIO. The user can not access
    /// the file directly, but may modify values at specific fields whtich will then be saved
    /// to the file when <see cref="Save"/> is called.
    /// <para/>This class is thread safe.
    /// <para/>This will also interact with <see cref="NetworkTable"/> by creating a table called
    /// "Preferences" with all the key-value pairs. To save using <see cref="NetworkTable"/>, simply
    /// set the boolean at position ~S A V E~ to true. Also, if the value of any variable is " in the
    /// <see cref="NetworkTable"/>, then that represents non-existance in the <see cref="Preferences"/>table.</remarks>
    public class Preferences : ITableListener
    {
// ReSharper disable InconsistentNaming
        private const string TABLE_NAME = "Preferences";
        private const string SAVE_FIELD = "~S A V E~";
        private const string FILE_NAME = "/home/lvuser/wpilib-preferences.ini";
        private static readonly char[] VALUE_PREFIX = { '=', '\"' };
        private static readonly char[] VALUE_SUFFIX = { '\"', '\n' };
        private static readonly char[] NEW_LINE = { '\n' };
// ReSharper restore InconsistentNaming

        private static Preferences s_instance;

        private static readonly object s_lockObject = new object(); 

        /// <summary>
        /// Returns the preferences instance.
        /// </summary>
        public static Preferences Instance
        {
            get
            {
                lock (s_lockObject)
                {
                    return s_instance ?? (s_instance = new Preferences());
                }
            }
        }

        private readonly object m_fileLock = new object();

        private readonly object m_lockObject = new object();

        private Dictionary<string, string> m_values;
        private List<string> m_keys;

        private Dictionary<string, Comment> m_comments;
        private Comment m_endComment;

        private Preferences()
        {
            m_values = new Dictionary<string,string>();
            m_keys = new List<string>();

            lock(m_fileLock)
            {
                ThreadPool.QueueUserWorkItem(o => Read());
                try
                {
                    Monitor.Wait(m_fileLock);
                    
                }
                catch(ThreadInterruptedException)
                {

                }
            }

            HAL.Report(ResourceType.kResourceType_Preferences, (byte)0);
        }
        /// <summary>
        /// Gets a list of the Keys
        /// </summary>
        public List<string> Keys
        {
            get
            {
                lock (m_lockObject)
                    return m_keys;
            }
        }

        private void Put(string key, string value)
        {
            lock (m_lockObject)
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                ImproperPreferenceKeyException.ConfirmString(key);
                if (!m_values.ContainsKey(key))
                {
                    m_values.Add(key, value);
                    m_keys.Add(key);
                }
                NetworkTable.GetTable(TABLE_NAME).PutString(key, value);
            }
        }

        /// <summary>
        /// Puts the given string into the preferences table.
        /// </summary>
        /// <remarks>The value may not have quotation marks, nor may the key have any
        /// whitespace nor an equals sign
        /// <para/>This will <b>NOT</b> save the value to memory between power cycles </remarks>
        /// <param name="key">The key to write to</param>
        /// <param name="value">The value to set</param>
        /// <exception cref="ArgumentNullException">If the value is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the value contains a quotation mark</exception>
        /// <exception cref="ImproperPreferenceKeyException">If the key contains any white space of an equals sign</exception>
        public void PutString(string key, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value.IndexOf('"') != -1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Can not put string:{value} because it contains quotation marks");
            }
            Put(key, value);
        }

        public void PutInt(string key, int value)
        {
            Put(key, value.ToString());
        }

        public void PutDouble(string key, double value)
        {
            Put(key, value.ToString());
        }

        public void PutFloat(string key, float value)
        {
            Put(key, value.ToString());
        }

        public void PutBoolean(string key, bool value)
        {
            Put(key, value.ToString());
        }

        public void PutLong(string key, long value)
        {
            Put(key, value.ToString());
        }

        private string Get(string key)
        {
            lock (m_lockObject)
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                return !m_values.ContainsKey(key) ? null : m_values[key];
            }
        }

        public bool ContainsKey(string key)
        {
            return m_values.ContainsKey(key);
        }

        public void Remove(string key)
        {
            lock (m_lockObject)
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                m_values.Remove(key);
                m_keys.Remove(key);
            }
        }

        public string GetString(string key, string backup)
        {
            string value = Get(key);
            return value ?? backup;
        }

        public int GetInt(string key, int backup)
        {
            string value = Get(key);
            if (value == null)
            {
                return backup;
            }
            else
            {
                int retVal = 0;

                if (int.TryParse(value, out retVal))
                {
                    return retVal;
                }
                else
                {
                    throw new FormatException(value + " was not in the right format (int)");
                }
            }
        }

        public double GetDouble(string key, double backup)
        {
            string value = Get(key);
            if (value == null)
            {
                return backup;
            }
            else
            {
                double retVal = 0;
                if (double.TryParse(value, out retVal))
                {
                    return retVal;
                }
                else
                {
                    throw new FormatException(value + " was not in the right format (double)");
                }
            }
        }

        public bool GetBoolean(string key, bool backup)
        {
            string value = Get(key);
            if (value == null)
                return backup;
            else
            {
                bool retVal;
                if (bool.TryParse(value, out retVal))
                {
                    return retVal;
                }
                else
                {
                    throw new FormatException(value + " was not in the right format (bool)");
                }
            }
        }

        public float GetFloat(string key, float backup)
        {
            string value = Get(key);
            if (value == null)
            {
                return backup;
            }
            else
            {
                float retVal = 0;
                if (float.TryParse(value, out retVal))
                {
                    return retVal;
                }
                else
                {
                    throw new FormatException(value + " was not in the right format (float)");
                }
            }
        }

        public long GetLong(string key, long backup)
        {
            string value = Get(key);
            if (value == null)
            {
                return backup;
            }
            else
            {
                long retVal = 0;

                if (long.TryParse(value, out retVal))
                {
                    return retVal;
                }
                else
                {
                    throw new FormatException(value + " was not in the right format (long)");
                }
            }
        }

        public void Save()
        {
            lock (m_fileLock)
            {
                ThreadPool.QueueUserWorkItem(o => Write());
                try
                {
                    Monitor.Wait(m_fileLock);
                }
                catch(ThreadInterruptedException)
                {

                }
            }
        }

        private void Write()
        {
            lock(m_lockObject)
            {
                lock(m_fileLock)
                {
                    Monitor.PulseAll(m_fileLock);
                }

                StreamWriter output = null;
                try
                {
                    if (File.Exists(FILE_NAME))
                    {
                        File.Delete(FILE_NAME);
                    }
                    output = new StreamWriter(FILE_NAME);

                    output.Write("[Preferences]\n");
                    foreach (string key in m_keys)
                    {
                        string value = m_values[key];
                        if (m_comments != null)
                        {
                            Comment comment = m_comments[key];
                            comment?.Write(output);
                        }

                        output.Write(key);
                        output.Write(VALUE_PREFIX);
                        output.Write(value);
                        output.Write(VALUE_SUFFIX);
                    }

                    m_endComment?.Write(output);
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    if (output != null)
                    {
                        try
                        {
                            output.Close();
                        }
                        catch (IOException)
                        {

                        }
                    }
                    NetworkTable.GetTable(TABLE_NAME).PutBoolean(SAVE_FIELD, false);
                }
            }
        }

        public void Read()
        {
            lock (m_lockObject)
            {
                lock (m_fileLock)
                {
                    Monitor.PulseAll(m_fileLock);
                }

                Comment comment = null;

                StreamReader input = null;
                try
                {
                    if (File.Exists(FILE_NAME))
                    {
                        input = new StreamReader(FILE_NAME);
                        EndOfStreamException.Reader reader = new EndOfStreamException.Reader(input);

                        while (true)
                        {
                            char value = reader.ReadWithoutWhitespace();
                            StringBuilder buffer;
                            if (value == '\n' || value == ';')
                            {
                                if (comment == null)
                                {
                                    comment = new Comment();
                                }

                                if (value == '\n')
                                {
                                    comment.AddBytes(NEW_LINE);
                                }
                                else
                                {
                                    buffer = new StringBuilder(30);
                                    for (; value != '\n'; value = reader.Read())
                                    {
                                        buffer.Append(value);
                                    }
                                    buffer.Append('\n');
                                    char[] data = new char[buffer.Length];
                                    buffer.CopyTo(0, data, 0, buffer.Length);
                                    comment.AddBytes(data);
                                }
                            }
                            else if (value == '[')
                            {
                                while (reader.Read() != ']')
                                {
                                }
                                while (reader.Read() != '\n')
                                {
                                }
                            }
                            else
                            {
                                buffer = new StringBuilder(30);
                                for (; value != '='; value = reader.ReadWithoutWhitespace())
                                {
                                    buffer.Append(value);
                                }
                                string name = buffer.ToString();
                                buffer = new StringBuilder(30);
                                bool shouldBreak = false;
                                value = reader.ReadWithoutWhitespace();

                                if (value == '"')
                                {
                                    for (value = reader.Read(); value != '"'; value = reader.Read())
                                    {
                                        buffer.Append(value);
                                    }
                                    while (reader.Read() != '\n')
                                    {
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        for (; value != '\n'; value = reader.ReadWithoutWhitespace())
                                        {
                                            buffer.Append(value);
                                        }
                                    }
                                    catch (EndOfStreamException)
                                    {
                                        shouldBreak = true;
                                    }
                                }
                                string result = buffer.ToString();

                                m_keys.Add(name);
                                m_values.Add(name, result);

                                NetworkTable.GetTable(TABLE_NAME).PutString(name, result);
                                if (comment != null)
                                {
                                    if (m_comments == null)
                                    {
                                        m_comments = new Dictionary<string, Comment>();
                                    }
                                    m_comments.Add(name, comment);
                                    comment = null;
                                }

                                if (shouldBreak)
                                {
                                    break;
                                }

                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex);
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine("Done Reading");
                }

                if (input != null)
                {
                    try
                    {
                        input.Close();
                    }
                    catch(IOException)
                    {

                    }
                }

                if (comment != null)
                {
                    m_endComment = comment;
                }
            }

            NetworkTable.GetTable(TABLE_NAME).PutBoolean(SAVE_FIELD, false);
            NetworkTable.GetTable(TABLE_NAME).AddTableListener(this);//Figure this out
        }

        public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
        {
            if (key.Equals(SAVE_FIELD))
            {
                if ((bool)value)
                {
                    Save();
                }
            }
            else
            {
                lock (m_lockObject)
                {
                    if (!ImproperPreferenceKeyException.IsAcceptable(key) || value.ToString().IndexOf('"') != -1)
                    {
                        if (m_values.ContainsKey(key) || m_keys.Contains(key))
                        {
                            m_values.Remove(key);
                            m_keys.Remove(key);
                            NetworkTable.GetTable(TABLE_NAME).PutString(key, "\"");
                        }
                    }
                    else
                    {
                        if (m_values.ContainsKey(key))
                        {
                            m_values[key] = value.ToString();
                        }
                        else
                        {
                            m_values.Add(key, value.ToString());
                            m_keys.Add(key);
                        }
                    }
                }
            }
        }

        internal class EndOfStreamException : Exception
        {
            internal class Reader
            {
                StreamReader m_stream;

                internal Reader(StreamReader stream)
                {
                    m_stream = stream;
                }

                internal char Read()
                {
                    int input = m_stream.Read();
                    if (input == -1)
                    {
                        throw new EndOfStreamException();
                    }
                    else
                    {
                        return input == '\r' ? '\n' : (char)input;
                    }
                }

                internal char ReadWithoutWhitespace()
                {
                    while (true)
                    {
                        char value = Read();
                        switch (value)
                        {
                            case ' ':
                            case '\t':
                                continue;
                            default:
                                return value;
                        }
                    }

                }
            }
        }

        internal class ImproperPreferenceKeyException : SystemException
        {
            public ImproperPreferenceKeyException(string value, char letter)
                : base("Preference key \""
                    + value + "\" is not allowed to contain letter with ASCII code:" + (byte)letter)
            {

            }

            public static void ConfirmString(string value)
            {
                foreach (char letter in value)
                {
                    switch(letter)
                    {
                        case '=':
                        case '\n':
                        case '\r':
                        case ' ':
                        case '\t':
                        case '[':
                        case ']':
                            throw new ImproperPreferenceKeyException(value, letter);
                    }
                }
            }

            public static bool IsAcceptable(string value)
            {
                foreach (char letter in value)
                {
                    switch (letter)
                    {
                        case '=':
                        case '\n':
                        case '\r':
                        case ' ':
                        case '\t':
                        case '[':
                        case ']':
                            return false;
                    }
                }
                return true;
            }
        }

        private class Comment
        {
            private List<char> m_bytes = new List<char>();

            internal void AddBytes(char[] bytes)
            {
                m_bytes.AddRange(bytes);
            }

            internal void Write(StreamWriter writer)
            {
                foreach (char t in m_bytes)
                {
                    writer.Write(t);
                }
            }
        }
    }
}
