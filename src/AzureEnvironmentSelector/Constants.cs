using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michelotti.AzureEnvironmentSelector
{
    internal static class Constants
    {
        public const string AadConfigFile =
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
}";
    }

    internal static class Clouds
    {
        public const string Azure = "Azure Commercial";
        public const string AzureGovernment = "Azure Government";
        public const string AzureGermany = "Azure Germany";
        public const string AzureChina = "Azure China";
    }
}
