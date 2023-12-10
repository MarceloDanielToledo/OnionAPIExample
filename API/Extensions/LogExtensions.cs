using API.CustomEnrichers;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace API.Extensions
{
    public static class LogExtensions
    {
        public static LoggerConfiguration WithGuidTraceId(this LoggerEnrichmentConfiguration enrich)
        {
            if (enrich == null)
                throw new ArgumentNullException(nameof(enrich));
            return enrich.With(new TraceIdEnricher(Tracer.CurrentSpan.Context.TraceId.ToString()));
        }

        public static void AddCustomLogConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(loggingBulder =>
            {
                var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithClientIp()
                .Enrich.WithCorrelationId()
                .Enrich.WithAssemblyName()
                .Enrich.WithGuidTraceId()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] - {Message} {Properties} {Exception} {NewLine} {ClientIP} {LogEventGuid}"
                    )
                .WriteTo.MSSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    new MSSqlServerSinkOptions() { TableName = "Logs", SchemaName = "dbo", AutoCreateSqlTable = true },
                    columnOptions: new ColumnOptions
                    {
                        AdditionalColumns = new Collection<SqlColumn>
                        {
                            new SqlColumn{ColumnName="ClientIP",PropertyName="ClientIp",DataType= SqlDbType.NVarChar},
                            new SqlColumn{ColumnName="TraceId",DataType= SqlDbType.NVarChar, DataLength= 50}

                        },
                    });
                var logger = loggerConfiguration.CreateLogger();
                loggingBulder.Services.AddSingleton<ILoggerFactory>(
                    provider => new SerilogLoggerFactory(logger, dispose: false));

            });

        }

        public static void HttpRequestPipeline(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(
                options =>
                {
                    options.MessageTemplate = "{RequestScheme} {RequestHost} {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
                    options.EnrichDiagnosticContext = (
                        diagnosticContext,
                        httpContext) =>
                    {
                        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                        diagnosticContext.Set("ClientIp", httpContext.Connection.RemoteIpAddress);
                    };
                });
        }
    }
}
