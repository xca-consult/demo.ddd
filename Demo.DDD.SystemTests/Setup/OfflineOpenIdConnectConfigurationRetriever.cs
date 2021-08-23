using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Demo.DDD.SystemTests.Setup
{
    public class OfflineOpenIdConnectConfigurationRetriever : IConfigurationRetriever<OpenIdConnectConfiguration>
    {
        public Task<OpenIdConnectConfiguration> GetConfigurationAsync(string address, IDocumentRetriever retriever, CancellationToken cancel)
        {
            
            var openIdConnectConfiguration = OpenIdConnectConfiguration.Create(SecurityConfigurationProvider.OpenIdConfiguration);
            openIdConnectConfiguration.JsonWebKeySet = JsonWebKeySet.Create(SecurityConfigurationProvider.JsonWebKeySet);

            return Task.FromResult(openIdConnectConfiguration);
        }
    }
}
