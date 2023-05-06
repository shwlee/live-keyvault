// See https://aka.ms/new-console-template for more information

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

Console.WriteLine("Start!!!");

// https://net-greg-test.vault.azure.net/

var keyVaultName = "net-greg-test";
var kvUri = "https://" + keyVaultName + ".vault.azure.net";

var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

var key = "site-key";
var siteKey = await client.GetSecretAsync(key);

Console.WriteLine(siteKey);

Console.WriteLine("DONE~~~~~");