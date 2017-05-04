using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michelotti.AzureEnvironmentSelector
{
    internal static class Constants
    {
        internal static readonly List<CloudItem> CloudList = new List<CloudItem>
        {
            new CloudItem { Name = "Azure Commercial", AadConfig = null },
            new CloudItem { Name = "Azure Government", AadConfig =
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
                }"},
            new CloudItem { Name = "Azure Germany", AadConfig =
                @"{
                        ""AuthenticationQueryParameters"": null,
                        ""AsmEndPoint"": ""https://management.microsoftazure.de/"",
                        ""Authority"": ""https://login.microsoftonline.de/"",
                        ""AzureResourceManagementEndpoint"": ""https://management.microsoftazure.de/"",
                        ""AzureResourceManagementAudienceEndpoints"": [ ""https://management.core.cloudapi.de/"" ],
                        ""ClientIdentifier"": ""872cd9fa-d31f-45e0-9eab-6e460a02d1f1"",
                        ""EnvironmentName"": ""AzureGermanCloud"",
                        ""GraphEndpoint"": ""https://graph.cloudapi.de"",
                        ""MsaHomeTenantId"": ""f577cd82-810c-43f9-a1f6-0cc532871050"",
                        ""NativeClientRedirect"": ""urn:ietf:wg:oauth:2.0:oob"",
                        ""PortalEndpoint"": ""https://portal.core.cloudapi.de/"",
                        ""ResourceEndpoint"": ""https://management.core.cloudapi.de/"",
                        ""ValidateAuthority"": true,
                        ""VisualStudioOnlineEndpoint"": ""https://app.vssps.visualstudio.com/"",
                        ""VisualStudioOnlineAudience"": ""499b84ac-1321-427f-aa17-267ca6975798""
                }"},
            new CloudItem { Name = "Azure China", AadConfig =
                @"{
                        ""AuthenticationQueryParameters"": null,
                        ""AsmEndPoint"": ""https://management.core.chinacloudapi.cn"",
                        ""Authority"": ""https://login.chinacloudapi.cn/"",
                        ""AzureResourceManagementEndpoint"": ""https://management.chinacloudapi.cn/"",
                        ""AzureResourceManagementAudienceEndpoints"": [ ""https://management.core.chinacloudapi.cn/"" ],
                        ""ClientIdentifier"": ""872cd9fa-d31f-45e0-9eab-6e460a02d1f1"",
                        ""EnvironmentName"": ""Mooncake"",
                        ""GraphEndpoint"": ""https://graph.chinacloudapi.cn"",
                        ""MsaHomeTenantId"": ""f577cd82-810c-43f9-a1f6-0cc532871050"",
                        ""NativeClientRedirect"": ""urn:ietf:wg:oauth:2.0:oob"",
                        ""PortalEndpoint"": ""http://manage.windowsazure.cn"",
                        ""ResourceEndpoint"": ""https://management.core.chinacloudapi.cn/"",
                        ""ValidateAuthority"": true,
                        ""VisualStudioOnlineEndpoint"": ""https://app.vssps.visualstudio.com/"",
                        ""VisualStudioOnlineAudience"": ""499b84ac-1321-427f-aa17-267ca6975798""
                }"}
        };


        internal static Dictionary<string, string> CloudNames = new Dictionary<string, string>
        {
            ["AzureCloud"] = Clouds.Azure,
            ["AzureUSGovernment"] = Clouds.AzureGovernment,
            ["AzureGermanCloud"] = Clouds.AzureGermany,
            ["Mooncake"] = Clouds.AzureChina
        };
    }

    internal static class Clouds
    {
        public const string Azure = "Azure Commercial";
        public const string AzureGovernment = "Azure Government";
        public const string AzureGermany = "Azure Germany";
        public const string AzureChina = "Azure China";
    }
}
