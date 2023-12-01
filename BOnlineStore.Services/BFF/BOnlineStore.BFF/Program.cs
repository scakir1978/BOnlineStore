using BOnlineStore.BFF.Api.Injections;
using BOnlineStore.BFF.Api.Services.Production;
using BOnlineStore.Shared;
using BOnlineStore.Shared.Constansts;
using BOnlineStore.Shared.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
  .WriteTo.Seq(builder.Configuration.GetValue<string>("SeqServerUrl"))
  .ReadFrom.Configuration(builder.Configuration)
  .CreateLogger();

Log.Logger = logger;

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
    .WriteTo.Seq(builder.Configuration.GetValue<string>("SeqServerUrl"))
    .ReadFrom.Configuration(context.Configuration);
});

if (builder.Configuration[AppSettingsKeysConstants.BFFRunningMode] == "docker")
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

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());

}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;

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
    option.SwaggerDoc(GlobalConstants.swaggerVersion, new OpenApiInfo { Title = "BOnlineStore BFF APIs", Version = GlobalConstants.swaggerVersion });
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
        options.Audience = IdentityServerConstants.ApiResourcesBFF;
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddMappingInjections();
builder.Services.AddValidatorInjections();
builder.Services.AddRepositoryInjections();
builder.Services.AddServiceInjections(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExeptionHandler();

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();

app.UseAuthorization();

app.UseRequestLocalization();

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

