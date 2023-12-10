using OpenTelemetry.Trace;
using Serilog.Core;
using Serilog.Events;

namespace API.CustomEnrichers
{
    public class TraceIdEnricher : ILogEventEnricher
    {
        private readonly string _traceId;

        public TraceIdEnricher(string traceId)
        {
            _traceId = traceId;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            string traceId = Tracer.CurrentSpan.Context.TraceId.ToString();
            var traceIdProperty = propertyFactory.CreateProperty("TraceId", traceId);
            logEvent.AddOrUpdateProperty(traceIdProperty);
        }
    }
}
