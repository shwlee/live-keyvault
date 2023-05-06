using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using NetDevLive01.Web.configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var folder = "configs";
var config = builder.Configuration;
config.AddJsonFile(Path.Combine(folder, "appsettings.json"), true, true)
    .AddJsonFile(Path.Combine(folder, $"appsettings.{builder.Environment.EnvironmentName}.json"), true, true);
builder.Services.Configure<AppSettings>(config.GetSection(nameof(AppSettings)));

// KeyVault provider setting.
var azureKeyVaultName = config["KeyVaultName"];
var secretClient = new SecretClient(new Uri($"https://{azureKeyVaultName}.vault.azure.net/"), new DefaultAzureCredential());
var keyVaultConfigOption = new AzureKeyVaultConfigurationOptions
{
    Manager = new KeyVaultSecretManager(),
    //ReloadInterval = TimeSpan.FromSeconds(60)
};
config.AddAzureKeyVault(secretClient, keyVaultConfigOption);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();