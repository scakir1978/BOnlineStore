using BOnlineStore.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


Console.WriteLine(builder.Environment.EnvironmentName.ToLower());
Console.WriteLine(builder.Configuration[AppSettingsKeysConstants.IdentityServerUrl]);

builder.Configuration
    .AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json")
    .AddEnvironmentVariables();

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
