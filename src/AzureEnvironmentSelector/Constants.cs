using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michelotti.AzureEnvironmentSelector
{
    public class CloudSetting
    {
        internal CloudSetting(string name, string displayname, string jsonconfig)
        {
            Name = name; DisplayName = displayname; JsonConfig = jsonconfig;
        }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string JsonConfig { get; private set; }
        public override string ToString()
        {
            return DisplayName;
        }
    }
    internal static class Constants
    {
        internal static string PublicCloudName { get; } = "AzureCloud";
        internal static readonly Dictionary<string, CloudSetting> CloudSettings = new Dictionary<string, CloudSetting>
        {
            ["AzureCloud"] = new CloudSetting("AzureCloud", "Azure Public Cloud", null),
            // Ref: https://docs.microsoft.com/en-us/azure/azure-government/documentation-government-get-started-connect-with-vs
            ["AzureUSGovernment"] = new CloudSetting("AzureUSGovernment", "Azure Government",
                @"{
                    ""AuthenticationQueryParameters"": null,
                    ""AsmEndPoint"": ""https://management.core.usgovcloudapi.net/"",
                    ""Authority"": ""https://login.microsoftonline.us/"",
                    ""AzureResourceManagementEndpoint"": ""https://management.usgovcloudapi.net"",
                    ""AzureResourceManagementAudienceEndpoints"": [ ""https://management.core.usgovcloudapi.net"" ],
                    ""ClientIdentifier"": ""872cd9fa-d31f-45e0-9eab-6e460a02d1f1"",
                    ""EnvironmentName"": ""AzureUSGovernment"",
                    ""GraphEndpoint"": ""https://graph.windows.net"",
                    ""MsaHomeTenantId"": ""f8cdef31-a31e-4b4a-93e4-5f571e91255a"",
                    ""NativeClientRedirect"": ""urn:ietf:wg:oauth:2.0:oob"",
                    ""PortalEndpoint"": ""https://portal.azure.us/"",
                    ""ResourceEndpoint"": ""https://management.core.usgovcloudapi.net"",
                    ""ValidateAuthority"": true,
                    ""VisualStudioOnlineEndpoint"": ""https://app.vssps.visualstudio.com/"",
                    ""VisualStudioOnlineAudience"": ""499b84ac-1321-427f-aa17-267ca6975798""
                }"),

            // Ref: https://www.azure.cn/documentation/articles/developerdifferences/
            ["Mooncake"] = new CloudSetting("Mooncake", "Azure China",  // Needs review uabble to test
                @"{
									 ""AuthenticationQueryParameters"": null,
									 ""AsmEndPoint"": ""https://management.core.chinacloudapi.cn"",
									 ""Authority"": ""https://login.chinacloudapi.cn/"",
									 ""AzureResourceManagementEndpoint"": ""https://management.chinacloudapi.cn/"",
									 ""AzureResourceManagementAudienceEndpoints"": [ ""https://management.core.chinacloudapi.cn/"" ],
									 ""ClientIdentifier"": ""872cd9fa-d31f-45e0-9eab-6e460a02d1f1"",
									 ""EnvironmentName"": ""Mooncake"",
									 ""GraphEndpoint"": ""https://graph.chinacloudapi.cn"",
									 ""MsaHomeTenantId"": ""f577cd82-810c-43f9-a1f6-0cc532871050"",
									 ""NativeClientRedirect"": ""urn:ietf:wg:oauth:2.0:oob"",
									 ""PortalEndpoint"": ""http://manage.windowsazure.cn"",
									 ""ResourceEndpoint"": ""https://management.core.chinacloudapi.cn/"",
									 ""ValidateAuthority"": true,
									 ""VisualStudioOnlineEndpoint"": ""https://app.vssps.visualstudio.com/"",
									 ""VisualStudioOnlineAudience"": ""499b84ac-1321-427f-aa17-267ca6975798""
                }"),

            // Ref: https://blogs.msdn.microsoft.com/azuregermany/2017/04/06/using-visual-studio-2017-in-microsoft-azure-germany/
            ["AzureGermanCloud"] = new CloudSetting("AzureGermanCloud", "Azure Germany",
                @"{
                    ""AuthenticationQueryParameters"":null,
                    ""AsmEndPoint"":""https://management.microsoftazure.de/"",
                    ""Authority"":""https://login.microsoftonline.de/"",
                    ""AzureResourceManagementEndpoint"":""https://management.microsoftazure.de/"",
                    ""AzureResourceManagementAudienceEndpoints"":[""https://management.core.cloudapi.de/""],
                    ""ClientIdentifier"":""872cd9fa-d31f-45e0-9eab-6e460a02d1f1"",
                    ""EnvironmentName"":""AzureGermanCloud"",
                    ""GraphEndpoint"":""https://graph.cloudapi.de"",
                    ""MsaHomeTenantId"":""f577cd82-810c-43f9-a1f6-0cc532871050"",
                    ""NativeClientRedirect"":""urn:ietf:wg:oauth:2.0:oob"",
                    ""PortalEndpoint"":""https://portal.core.cloudapi.de/"",
                    ""ResourceEndpoint"":""https://management.core.cloudapi.de/"",
                    ""ValidateAuthority"":true,
                    ""VisualStudioOnlineEndpoint"":""https://app.vssps.visualstudio.com/"",
                    ""VisualStudioOnlineAudience"":""499b84ac-1321-427f-aa17-267ca6975798""
                }")
        };
    }
}
