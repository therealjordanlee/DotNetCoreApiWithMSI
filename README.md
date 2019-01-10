# MSIDemo

An ASP.Net Core web application demostrating the use of Azure Managed Service Identity (MSI) for accessing Keyvault to read secrets.

For an overview of what MSI is, and how it works, please see the following links:

- https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/overview
- https://blogs.msdn.microsoft.com/benjaminperkins/2018/06/13/how-to-connect-to-a-database-from-an-azure-function-using-azure-key-vault/

Microsoft also has an example (which this solution is based on):

- https://github.com/Azure-Samples/app-service-msi-keyvault-dotnet


# Required Packages

- [Microsoft.Azure.Services.AppAuthentication][1] (for using MSI)
- [Microsoft.Azure.KeyVault][2] (for accessing Keyvault)


# Running this application

1. Create a keyvault and add a new secret called __DemoSecret__
2. Create a web app, enable MSI for it, and give it Get/List permissions on the Keyvault
3. Update `appsettings.json` with the URL of your keyvault:
```
{
  "KeyvaultSettings": {
    "KeyvaultUri": "https://yourkeyvault.vault.azure.net/"
  }
}
```

If running locally, make sure you install Azure CLI:

- https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest


Then use the cli to log in and select the correct subscription.

```
az login
az account set --subscription subscription-name
```

[1]: https://www.nuget.org/packages/Microsoft.Azure.Services.AppAuthentication/
[2]: https://www.nuget.org/packages/Microsoft.Azure.KeyVault/