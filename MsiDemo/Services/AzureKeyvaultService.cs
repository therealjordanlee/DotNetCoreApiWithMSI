using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Options;
using MsiDemo.Settings;

// https://github.com/Azure-Samples/app-service-msi-keyvault-dotnet


namespace MsiDemo.Services
{
    public class AzureKeyvaultService : IAzureKeyvaultService
    {
        private AzureServiceTokenProvider _azureServiceTokenProvider;
        private KeyvaultSettings _keyvaultSettings;

        public AzureKeyvaultService(IOptions<KeyvaultSettings> keyvaultOptions, AzureServiceTokenProvider azureServiceTokenProvider)
        {
            /*
             * The AzureServiceTokenProvider class (which is part of Microsoft.Azure.Services.AppAuthentication) tries the following methods to get an access token:-
             *  1. Managed Service Identity (MSI) - for scenarios where the code is deployed to Azure, and the Azure resource supports MSI.
             *  2. Azure CLI (for local development) - Azure CLI version 2.0.12 and above supports the get-access-token option. AzureServiceTokenProvider uses this option to get an access token for local development.
             *  3. Active Directory Integrated Authentication (for local development). To use integrated Windows authentication, your domain’s Active Directory must be federated with Azure Active Directory. Your application must be running on a domain-joined machine under a user’s domain credentials.
             */
            _azureServiceTokenProvider = azureServiceTokenProvider;
            _keyvaultSettings = keyvaultOptions.Value;
        }

        public async Task<string> GetSecret()
        {
            var keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(_azureServiceTokenProvider.KeyVaultTokenCallback));

            // Secrets are references through URI, e.g.
            // https://keyvaultname.vault.azure.net/secrets/secretName
            var secret = await keyVaultClient.GetSecretAsync($"{_keyvaultSettings.KeyvaultUri}secrets/DemoSecret")
                .ConfigureAwait(false);

            return secret.Value;
        }
            
    }
}
