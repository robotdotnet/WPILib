using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using HAL_Base;

namespace WPILib
{
    public class Preferences
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

        private Hashtable values;
        private List<string> keys;

        Thread thread;

        private Preferences()
        {
            values = new Hashtable();
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
                    throw new NullReferenceException();
                }
                ImproperPreferenceKeyException.ConfirmString(key);
                //
            }
        }

        public void PutString(string key, string value)
        {
            if (value == null)
            {
                throw new NullReferenceException();
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

        public void PutFload(string key, float value)
        {
            Put(key, value.ToString());
        }

        public void PutBoolean(string key, bool value)
        {
            Put(key, value.ToString());
        }



        public void Read()
        {

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

    }
}
