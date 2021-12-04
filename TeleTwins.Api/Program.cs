using Microsoft.OpenApi.Models;
using Rostelecom.DigitalProfile.Api.Authentication;
using TeleTwins.DataWarehouse;
using TeleTwins.Integrations.Tvs;
using TeleTwins.Twin;
using TeleTwins.Twins;
using TeleTwins.Twins.AlienAccess;
using TeleTwins.Twins.Publications;

var builder = WebApplication.CreateBuilder(args);
var authOptions = new TvsAuthenticationOptions();

builder.Services.AddSingleton(
    new TvsClientOptions
    {
        BaseUrl = "https://tvscp.tionix.ru/",
        ServiceLogin = "tvscp",
        ServiceSecret = "f3e94369-53ac-43d5-842e-09fe6d8a71ff"
    });

builder.Services.AddSingleton(authOptions);
builder.Services.AddScoped<ITvsLoginService, TvsClient>();
builder.Services.AddScoped<ITvsUserProvider, TvsClient>();
builder.Services.AddScoped<ITwinProvider, TwinProvider>();
builder.Services.AddScoped<ITwinPublicationProvider, TwinPublicationProvider>();
builder.Services.AddScoped<IDataWarehouse, DataWarehouse>();
builder.Services.AddScoped<ITwinAlienAccessService, TwinAlienAccessService>();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        authOptions.HeaderName,
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = authOptions.HeaderName,
            Type = SecuritySchemeType.ApiKey,
            Description = "Access token"
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { 
            new OpenApiSecurityScheme 
            {
                Name = authOptions.HeaderName,
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                { 
                    Type = ReferenceType.SecurityScheme,
                    Id = authOptions.HeaderName
                },
            },
            Array.Empty<string>()
        }
    });
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = TvsAuthenticationDefaults.Scheme;
        options.DefaultAuthenticateScheme = TvsAuthenticationDefaults.Scheme;
        options.DefaultChallengeScheme = TvsAuthenticationDefaults.Scheme;
    })
    .AddScheme<TvsAuthenticationOptions, TvsAuthenticationHandler>(TvsAuthenticationDefaults.Scheme, _ => { });


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();