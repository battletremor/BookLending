using DTO.DBContexts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Exceptions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using BookLending.Interfaces;
using BookLending.Repositories;
using BookLending.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddControllers();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Allow any origin
              .AllowAnyMethod()  // Allow any HTTP method
              .AllowAnyHeader(); // Allow any headers
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DTOContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("BLDBConnection")));
builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    loggerConfiguration
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        //.Enrich.WithExceptionDetails(new DestructuringOptionsBuilder().WithDefaultDestructurers()).WriteTo.File("logs/ExceptionErrors.txt", rollingInterval: RollingInterval.Day)
        .Enrich.WithExceptionDetails()
        .WriteTo.File(new CompactJsonFormatter(), "logs/JsonLog.txt", rollingInterval: RollingInterval.Day)
        .WriteTo.File("logs/Logs.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
            .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(evt => evt.Level == Serilog.Events.LogEventLevel.Error)
                .WriteTo.File("logs/Errors.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}"))
                .ReadFrom.Configuration(context.Configuration);
});
builder.Services
        .AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddTransient<IAuthenticationService, AuthService>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookService, BookService>();
var app = builder.Build();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.Run();
