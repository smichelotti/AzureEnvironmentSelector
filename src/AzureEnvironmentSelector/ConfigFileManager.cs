using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Michelotti.AzureEnvironmentSelector
{
    public class ConfigFileManager
    {
        public void DeleteAadConfigFile()
        {
            var configFilePath = GetConfigFilePath();
            if (File.Exists(configFilePath))
            {
                File.Delete(configFilePath);
            }
        }

        public void CreateSovereignCloudConfig(CloudSetting setting)
        {
            string jsonconfig = null;
            jsonconfig = setting.JsonConfig;
            VerifyDirectory();
            var configFilePath = GetConfigFilePath();
            File.WriteAllText(configFilePath, jsonconfig);

            void VerifyDirectory()
            {
                var aadConfigDir = GetAadConfigDirectory();
                if (!Directory.Exists(aadConfigDir))
                {
                    Directory.CreateDirectory(aadConfigDir);
                }
            }
        }
        public string GetCurrentConnection()
        {
            var configFilePath = GetConfigFilePath();
            if (File.Exists(configFilePath))
            {
                var jsonstr = File.ReadAllText(configFilePath);
                var dict = (new JavaScriptSerializer()).Deserialize<Dictionary<string, dynamic>>(jsonstr);
                var cloudname = dict["EnvironmentName"];
                return Constants.CloudSettings[cloudname].Name;
            }
            else
            {
                return Constants.PublicCloudName;
            }
        }

        #region Private static methods

        private static string GetAadConfigDirectory()
        {
            var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = System.IO.Path.Combine(localAppDataPath, ".IdentityService", "AadConfigurations");
            return path;
        }

        internal static string GetConfigFilePath()
        {
            var aadConfigDirectory = GetAadConfigDirectory();
            var path = System.IO.Path.Combine(aadConfigDirectory, "AadProvider.Configuration.json");
            return path;
        }

        private static bool SovereignConfigFileExists() => File.Exists(GetConfigFilePath());


        #endregion
    }
}
