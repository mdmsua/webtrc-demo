using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenTracing;
using WebTrace.Domain.Options;
using static Jaeger.Configuration;

namespace WebTrace.Domain.Services
{
    public class TraceService : ITraceService
    {
        private readonly ConcurrentDictionary<string, ITracer> tracers;

        private readonly ReporterConfiguration reporterConfiguration;

        private readonly SamplerConfiguration samplerConfiguration;

        private readonly ILoggerFactory loggerFactory;

        private ISpan rootSpan;

        public TraceService(ILoggerFactory loggerFactory, IOptions<JaegerOptions> options)
        {
            this.loggerFactory = loggerFactory;
            var senderConfiguration = new Configuration.SenderConfiguration(loggerFactory).WithAgentHost(options.Value.Host);
            this.samplerConfiguration = new Configuration.SamplerConfiguration(loggerFactory).WithType(ConstSampler.Type).WithParam(1);
            this.reporterConfiguration = new Configuration.ReporterConfiguration(loggerFactory).WithLogSpans(true).WithSender(senderConfiguration);
            this.tracers = new ConcurrentDictionary<string, ITracer>();
        }

        public ISpan Start(string serviceName, string operationName)
        {
            if (!this.tracers.TryGetValue(serviceName, out var tracer))
            {
                tracer = new Configuration(serviceName, loggerFactory).WithReporter(this.reporterConfiguration).WithSampler(this.samplerConfiguration).GetTracer();
                this.tracers.TryAdd(serviceName, tracer);
            }

            var span = tracer.BuildSpan(operationName);
           
            if (this.rootSpan is null)
            {
                rootSpan = span.Start();
                return rootSpan;
            }

            return span.AsChildOf(this.rootSpan).Start();
        }

        public void Finish()
        {
            this.rootSpan?.Finish();
            this.rootSpan = null;
        }

        public void Dispose()
        {
            this.Finish();
        }
    }
}