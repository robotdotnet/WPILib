using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using HAL_Base;
using System.IO;
using NetworkTablesDotNet.NetworkTables;
using NetworkTablesDotNet.Tables;

namespace WPILib
{
    public class Preferences : ITableListener
    {
        private static string TABLE_NAME = "Preferences";
        private static string SAVE_FIELD = "~S A V E~";
        private static string FILE_NAME = "/home/lvuser/wpilib-preferences.ini";
        private static char[] VALUE_PREFIX = { '=', '\"' };
        private static char[] VALUE_SUFFIX = { '\"', '\n' };
        private static char[] NEW_LINE = { '\n' };

        private static Preferences s_instance;

        private static object s_lockObject = new object(); 

        public static Preferences GetInstance()
        {
            lock(s_lockObject)
            {
                if (s_instance == null)
                    s_instance = new Preferences();
                return s_instance;
            }
        }

        private object fileLock = new object();

        private object m_lockObject = new object();

        private Dictionary<string, string> values;
        private List<string> keys;

        private Dictionary<string, Comment> m_comments;
        private Comment m_endComment;

        Thread thread;

        private Preferences()
        {
            values = new Dictionary<string,string>();
            keys = new List<string>();

            lock(fileLock)
            {
                thread = new Thread(Read);
                thread.Start();

                try
                {
                    Monitor.Wait(fileLock);
                    
                }
                catch(ThreadInterruptedException ex)
                {

                }
            }

            HAL.Report(ResourceType.kResourceType_Preferences, (byte)0);
        }

        public List<string> GetKeys()
        {
            lock (m_lockObject)
                return keys;
        }

        private void Put(string key, string value)
        {
            lock (m_lockObject)
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }
                ImproperPreferenceKeyException.ConfirmString(key);
                if (!values.ContainsKey(key))
                {
                    values.Add(key, value);
                    keys.Add(key);
                }
                //NetworkTable.GetTable(TABLE_NAME).PutString(key, value);
            }
        }

        public void PutString(string key, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (value.IndexOf('"') != -1)
            {
                throw new ArgumentException("Can not put string:" + value + " because it contains quotation marks");
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
                    throw new ArgumentNullException("key");
                }
                if (!values.ContainsKey(key))
                    return null;
                return values[key];
            }
        }

        public bool ContainsKey(string key)
        {
            return values.ContainsKey(key);
        }

        public void Remove(string key)
        {
            lock (m_lockObject)
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }
                values.Remove(key);
                keys.Remove(key);
            }
        }

        public string GetString(string key, string backup)
        {
            string value = Get(key);
            return value == null ? backup : value;
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

        Thread saveThread;
        public void Save()
        {
            lock (fileLock)
            {
                saveThread = new Thread(Write);
                saveThread.Start();
                try
                {
                    Monitor.Wait(fileLock);
                }
                catch(ThreadInterruptedException ex)
                {

                }
            }
        }

        private void Write()
        {
            lock(m_lockObject)
            {
                lock(fileLock)
                {
                    Monitor.PulseAll(fileLock);
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
                    for (int i = 0; i < keys.Count; i++)
                    {
                        string key = keys[i];
                        string value = values[key];
                        if (m_comments != null)
                        {
                            Comment comment = m_comments[key];
                            if (comment != null)
                            {
                                comment.Write(output);
                            }
                        }

                        output.Write(key);
                        output.Write(VALUE_PREFIX);
                        output.Write(value);
                        output.Write(VALUE_SUFFIX);
                    }

                    if (m_endComment != null)
                    {
                        m_endComment.Write(output);
                    }
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
                        catch (IOException ex)
                        {

                        }
                    }
                    //NetworkTable.GetTable(TABLE_NAME).PutBoolean(SAVE_FIELD, false);
                }
            }
        }

        public void Read()
        {
            lock (m_lockObject)
            {
                lock (fileLock)
                {
                    Monitor.PulseAll(fileLock);
                }

                Comment comment = null;

                StreamReader input = null;
                try
                {
                    if (File.Exists(FILE_NAME))
                    {
                        input = new StreamReader(FILE_NAME);
                        EndOfStreamException.Reader reader = new EndOfStreamException.Reader(input);
                        StringBuilder buffer;

                        while (true)
                        {
                            char value = reader.ReadWithoutWhitespace();
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
                                while (reader.Read() != ']') ;
                                while (reader.Read() != '\n');
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
                                    while (reader.Read() != '\n') ;
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
                                    catch (EndOfStreamException e)
                                    {
                                        shouldBreak = true;
                                    }
                                }
                                string result = buffer.ToString();

                                keys.Add(name);
                                values.Add(name, result);

                                //NetworkTable.GetTable(TABLE_NAME).PutString(name, result);
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
                catch (EndOfStreamException ex)
                {
                    Console.WriteLine("Done Reading");
                }

                if (input != null)
                {
                    try
                    {
                        input.Close();
                    }
                    catch(IOException ex)
                    {

                    }
                }

                if (comment != null)
                {
                    m_endComment = comment;
                }
            }

            //NetworkTable.GetTable(TABLE_NAME).PutBoolean(SAVE_FIELD, false);
            //TableListener listener = new TableListener(this, ref m_lockObject, ref values, ref keys);
            //NetworkTable.GetTable(TABLE_NAME).AddTableListener(listener);//Figure this out
        }

        public void ValueChanged(ITable source, string key, object value, bool isNew)
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
                        if (values.ContainsKey(key) || keys.Contains(key))
                        {
                            values.Remove(key);
                            keys.Remove(key);
                            //NetworkTable.GetTable(TABLE_NAME).PutString(key, "\"");
                        }
                    }
                    else
                    {
                        if (!values.ContainsKey(key))
                        {
                            values.Add(key, value.ToString());
                            keys.Add(key);
                        }
                    }
                }
            }
        }

        internal class EndOfStreamException : Exception
        {
            internal class Reader
            {
                StreamReader stream;

                internal Reader(StreamReader stream)
                {
                    this.stream = stream;
                }

                internal char Read()
                {
                    int input = stream.Read();
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

        public class ImproperPreferenceKeyException : SystemException
        {
            public ImproperPreferenceKeyException(string value, char letter)
                : base("Preference key \""
                    + value + "\" is not allowed to contain letter with ASCII code:" + (byte)letter)
            {

            }

            public static void ConfirmString(string value)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    char letter = value[i];
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
                for (int i = 0; i < value.Length; i++)
                {
                    char letter = value[i];
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
            private List<char> bytes = new List<char>();

            internal void AddBytes(char[] bytes)
            {
                this.bytes.AddRange(bytes);
            }

            internal void Write(StreamWriter writer)
            {
                for (int i = 0; i < bytes.Count; i++)
                {
                    writer.Write(bytes[i]);
                }
            }
        }
    }
}
