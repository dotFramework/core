using System;
using System.Collections.Generic;
using System.Configuration;

namespace DotFramework.Core
{
    public class AppSettingsManager : SingletonProvider<AppSettingsManager>
    {
        private AppSettingsManager()
        {
            _Settings = new Dictionary<String, String>();

            foreach (var settingKey in ConfigurationManager.AppSettings.AllKeys)
            {
                _Settings.Add(settingKey, ConfigurationManager.AppSettings[settingKey]);
            }
        }

        private Dictionary<String, String> _Settings;

        public string this[string key]
        {
            get
            {
                if (_Settings.ContainsKey(key))
                {
                    return _Settings[key];
                }
                else
                {
                    return null;
                }
            }
        }

        public string Get(string key)
        {
            return Get<String>(key);
        }

        public T Get<T>(string key)
        {
            string value = this[key];

            if (value != null)
            {
                try
                {
                    if (typeof(T) != typeof(TimeSpan))
                    {
                        return (T)Convert.ChangeType(value, typeof(T));
                    }
                    else
                    {
                        object retVal = TimeSpan.Parse(value);
                        return (T)retVal;
                    }
                }
                catch
                {
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }

        public T Get<T>(string key, T defaultValue) where T : IComparable
        {
            string value = this[key];

            if (value != null)
            {
                try
                {
                    if (typeof(T) != typeof(TimeSpan))
                    {
                        return (T)Convert.ChangeType(value, typeof(T));
                    }
                    else
                    {
                        object retVal = TimeSpan.Parse(value);
                        return (T)retVal;
                    }
                }
                catch
                {
                    return defaultValue;
                }
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
