using Genocs.Auth.DataSqLite;
using Genocs.Auth.DataSqlServer;
using Genocs.Auth.WebApi.Authorization;
using Genocs.Auth.WebApi.Configurations;
using Genocs.Auth.WebApi.Helpers;
using Genocs.Auth.WebApi.Services;
using Genocs.Core.Builders;
using Genocs.Logging;
using Genocs.Monitoring;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

StaticLogger.EnsureInitialized();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLogging();

IGenocsBuilder gnxBuilder = builder.AddGenocs();

// add services to DI container
var services = builder.Services;

// Set Custom Open telemetry
services.AddCustomOpenTelemetry(builder.Configuration);

// services.AddDbContext<SqlServerDbContext>();

services.AddDbContext<SqLiteDbContext>();

services.AddCors();
services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var tokenValidationParameters = new TokenValidationParameters();
byte[] rawKey = Encoding.UTF8.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");
tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(rawKey);
tokenValidationParameters.ValidateAudience = false;
tokenValidationParameters.ValidateIssuer = false;

builder.Services.AddAuthentication()
    .AddJwtBearer(o =>
    {
        o.Challenge = "Bearer";
        o.TokenValidationParameters = tokenValidationParameters;
    });

builder.Services.AddAuthorization();
//// Add services to the container.
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddAuthorizationBuilder()
  .AddPolicy("admin_greetings_full", policy =>
        policy
            .RequireClaim("admin_greetings", "middle")
            .Build());

services.AddHealthChecks();

services.Configure<HealthCheckPublisherOptions>(options =>
{
    options.Delay = TimeSpan.FromSeconds(2);
    options.Predicate = check => check.Tags.Contains("ready");
});

services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.Configure<HealthCheckPublisherOptions>(options =>
{
    options.Delay = TimeSpan.FromSeconds(2);
    options.Predicate = check => check.Tags.Contains("ready");
});

services.AddOptions();

// configure strongly typed settings object
services.Configure<AppSettings>(builder.Configuration.GetSection(AppSettings.Position));
services.Configure<TwilioOptions>(builder.Configuration.GetSection(TwilioOptions.Position));
services.Configure<SmtpEmailSenderOptions>(builder.Configuration.GetSection(SmtpEmailSenderOptions.Position));

// configure DI for application services
services.AddScoped<IJwtUtils, JwtUtils>();
services.AddScoped<IAccountService, AccountService>();
services.AddScoped<IEmailVerifyService, SmtpEmailVerifyService>();
services.AddScoped<IMobileVerifyService, MobileVerifyService>();

gnxBuilder.Build();

var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
/*
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<SqlServerDbContext>();
    dataContext.Database.Migrate();
}
*/

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<SqLiteDbContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/healthz");

app.MapGet("/secret2", () => "This is a different secret!")
    .RequireAuthorization(policy => policy.RequireClaim("admin_greetings", "full").Build());

app.MapGet("/secret3", () => "This is a different secret!")
    .RequireAuthorization("admin_greetings_full");

app.Run();

Log.CloseAndFlush();