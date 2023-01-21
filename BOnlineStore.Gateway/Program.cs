using BOnlineStore.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Path: " + Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));

Console.WriteLine($"Ocelot File: configuration.{builder.Configuration[AppSettingsKeysConstants.GatewayRunningMode]}.json");

builder.Configuration
    .AddJsonFile($"configuration.{builder.Configuration[AppSettingsKeysConstants.GatewayRunningMode]}.json")
    .AddEnvironmentVariables();

if (builder.Configuration[AppSettingsKeysConstants.GatewayRunningMode] == "docker")
{
    var certName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/bonlinestore.pfx";
    Console.WriteLine(File.Exists(certName) == true ? $"Certificate status: {certName} found." : $"Certificate status: {certName} NOT FOUND!!!");

    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(80);
        options.ListenAnyIP(443, listenOptions =>
        {
            listenOptions.UseConnectionLogging();
            listenOptions.UseHttps(certName, "Scag185489");
        });
    });
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(AppSettingsKeysConstants.GatewayAuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration[AppSettingsKeysConstants.IdentityServerUrl];
        options.Audience = IdentityServerConstants.ApiResourcesGateway;
        options.RequireHttpsMetadata = false;
    });


builder.Services.AddOcelot();



var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseOcelot().Wait();

app.Run();
