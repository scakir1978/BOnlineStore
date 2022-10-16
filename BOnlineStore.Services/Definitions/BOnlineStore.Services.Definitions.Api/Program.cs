
using AutoMapper;
using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Services.Definitions.Api;
using BOnlineStore.Services.Definitions.Api.Injections;
using BOnlineStore.Shared;
using BOnlineStore.Shared.Constansts;
using BOnlineStore.Shared.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()   
  .ReadFrom.Configuration(builder.Configuration)  
  .CreateLogger();

Log.Logger = logger; 

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Host.UseSerilog((context, configuration) => {

    configuration.ReadFrom.Configuration(context.Configuration);                 
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());

}).AddJsonOptions(options => {

    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;    

});

builder.Services.AddLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new(GlobalConstants.turkish);

    CultureInfo[] cultures = new CultureInfo[]
    {
        new(GlobalConstants.turkish),
        new(GlobalConstants.english)        
    };

    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;    

});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc(GlobalConstants.swaggerVersion, new OpenApiInfo { Title = "BOnlineStore Definitions APIs", Version = GlobalConstants.swaggerVersion });
    option.AddSecurityDefinition(GlobalConstants.swaggerScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = GlobalConstants.swaggerAuthorization,
        Type = SecuritySchemeType.Http,
        BearerFormat = GlobalConstants.swaggerBearerFormat,
        Scheme = GlobalConstants.swaggerScheme
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=GlobalConstants.swaggerScheme
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization();

app.ConfigureExeptionHandler();

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

app.UseSerilogRequestLogging(options =>
{
    options.IncludeQueryInRequestPath = true; 

});

app.Run();

Log.CloseAndFlush();

