using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void CreateCloudEnvConfig(CloudItem cloudItem)
        {
            VerifyDirectory();
            var configFilePath = GetConfigFilePath();
            File.WriteAllText(configFilePath, cloudItem.AadConfig);

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
                var json = File.ReadAllText(configFilePath);
                dynamic config = JObject.Parse(json);
                return Constants.CloudNames[(string)config.EnvironmentName];
            }
            else
            {
                return Clouds.Azure;
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

        private static bool GovConfigFileExists() => File.Exists(GetConfigFilePath());


        #endregion
    }
}
