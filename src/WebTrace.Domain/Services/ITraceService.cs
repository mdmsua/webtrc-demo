using System;
using OpenTracing;

namespace WebTrace.Domain.Services
{
    public interface ITraceService : IDisposable
    {
        ISpan Start(string serviceName, string operationName);

        void Finish();
    }
}