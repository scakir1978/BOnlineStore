
using AutoMapper;
using BOnlineStore.Services.Definitions.Api;
using BOnlineStore.Services.Definitions.Api.Injections;
using BOnlineStore.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()   
  .ReadFrom.Configuration(builder.Configuration)
  /*.Enrich.FromLogContext()
  .Enrich.WithMachineName()
  .Enrich.WithEnvironmentName()
  .Enrich.WithThreadName()
  .Enrich.WithThreadId()*/
  .CreateLogger();

Log.Logger = logger; 

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Host.UseSerilog((context, configuration) => {

    configuration.ReadFrom.Configuration(context.Configuration);
                 /*.Enrich.FromLogContext()
                 .Enrich.WithMachineName()
                 .Enrich.WithEnvironmentName()
                 .Enrich.WithThreadName()
                 .Enrich.WithThreadId();*/
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());

}).AddJsonOptions(options => {

    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "BOnlineStore Definitions APIs", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration[AppSettingsKeysConstants.IdentityServerUrl];
        options.Audience = IdentityServerConstants.ApiResourcesDefinitions;
        options.RequireHttpsMetadata = false;
    });

IMapper mapper = MappingConfigrations.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddMongoDbConfigurationAndInjections(builder.Configuration);
builder.Services.AddRepositoryInjections();
builder.Services.AddServiceInjections();

builder.Services.AddLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new("tr-TR");

    CultureInfo[] cultures = new CultureInfo[]
    {
        new("tr-TR"),
        new("en-US"),
        new("fr-FR")
    };

    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

var app = builder.Build();

Log.Information("app build complete");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization();

/*app.UseExceptionHandler(options =>
{
    
});*/

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseEndpoints(endpoints =>
{    
    endpoints.MapControllers();
});

app.UseSerilogRequestLogging();

app.Run();

Log.CloseAndFlush();

