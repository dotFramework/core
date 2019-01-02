using System.Configuration;

namespace DotFramework.Core.TestSupport
{
    public class TestAppSettingsManager
    {
        public readonly static TestAppSettingsManager Default = new TestAppSettingsManager();

        private static Configuration _Config;
        private static Configuration Config
        {
            get
            {
                if (_Config == null)
                {
                    ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                    map.ExeConfigFilename = "testhost.dll.config";

                    _Config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                }

                return _Config;
            }
        }

        public string this[string key]
        {
            get
            {
#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
                var setting = Config.AppSettings.Settings[key];

                if (setting != null)
                {
                    return setting.Value;
                }
                else
                {
                    return null;
                }
#else
                return ConfigurationManager.AppSettings[key];
#endif
            }
        }
    }
}
