using API.Extensions;
using API.Filters;
using API.Middleware;
using Application;
using EntityGuardian.Extensions;
using ExternalServiceCommunication;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.HttpLogging;
using Persistence;
using Serilog;
using Shared;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API
{
    public class Startup
    {
        public static WebApplication Initialize(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;
        }

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddSerilog();
            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });
            builder.Services.AddCustomLogConfiguration(builder.Configuration);
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
            builder.Services.AddApiVersioningExtension();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddMemoryCache();
            builder.Services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = $"Onion Example - {builder.Environment.EnvironmentName}",
                    Description = "Clean Architecture Example"
                });
            });
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceService(builder.Configuration);
            builder.Services.AddExternalServices(builder.Configuration);
            builder.Services.AddSharedServices();
            builder.Services.ConfigureHangfire(builder.Configuration);
            builder.Services.AddHangfireServer();
            builder.Services.AddHealthChecks();
            builder.Services.AddHttpContextAccessor();
        }

        public static void Configure(WebApplication app)
        {

            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                DeveloperExceptionPageOptions d = new() { SourceCodeLineCount = 2 };
                app.UseDeveloperExceptionPage(d);
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseHangfireDashboard("/hangfire", new DashboardOptions
                {
                    DashboardTitle = $"API Example - {app.Environment.EnvironmentName}",
                    IsReadOnlyFunc = (DashboardContext context) => false,
                    Authorization = new[] { new HangfireAuthorizationFilter() }
                });
            }
            else
            {
                app.UseHangfireDashboard("/hangfire", new DashboardOptions
                {
                    DashboardTitle = $"API Example - {app.Environment.EnvironmentName}",
                    IsReadOnlyFunc = (DashboardContext context) => true,
                    Authorization = new[] { new HangfireAuthorizationFilter() }
                });

            }
            app.UseEntityGuardian();
            app.HttpRequestPipeline();
            app.UseHttpLogging();
            app.UseMiddleware<LogMiddleware>();
            app.UseErrorHandlingMiddleware();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("health");
            app.Run();
        }
    }
}
