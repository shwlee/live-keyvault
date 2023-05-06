// See https://aka.ms/new-console-template for more information

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

Console.WriteLine("Start!!!");

// https://net-greg-test.vault.azure.net/
//var keyVaultName = Environment.GetEnvironmentVariable("KEY_VAULT_NAME");
var keyVaultName = "net-greg-test";
var kvUri = "https://" + keyVaultName + ".vault.azure.net";

// var tokenCredential = new DefaultAzureCredentialOptions
// {
//
// };
var defaultAzureCredentialOptions = new DefaultAzureCredentialOptions();
defaultAzureCredentialOptions.ExcludeAzureCliCredential = false;
defaultAzureCredentialOptions.ExcludeEnvironmentCredential = true;
defaultAzureCredentialOptions.ExcludeInteractiveBrowserCredential = true;
defaultAzureCredentialOptions.ExcludeManagedIdentityCredential = true;
defaultAzureCredentialOptions.ExcludeSharedTokenCacheCredential = true;
defaultAzureCredentialOptions.ExcludeVisualStudioCodeCredential = true;
defaultAzureCredentialOptions.ExcludeVisualStudioCredential = true;

// var option = new AzureCliCredentialOptions()
// {
//     TenantId = "8d0ebc68-82db-4d1a-a2a7-b2685e6debb3",
// };
// var credential = new AzureCliCredential(option);
// var client = new SecretClient(new Uri(kvUri), credential);
var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

var key = "site-key";
var siteKey = await client.GetSecretAsync(key);

Console.WriteLine(siteKey);

Console.WriteLine("DONE~~~~~");