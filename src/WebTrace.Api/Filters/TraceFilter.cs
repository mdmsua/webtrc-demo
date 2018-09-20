using Microsoft.AspNetCore.Mvc.Filters;
using WebTrace.Domain.Services;

namespace WebTrace.Api.Filters
{
    public class TraceFilter : ActionFilterAttribute
    {
        private readonly string endpoint;
        private readonly ITraceService service;

        private const string ServiceName = "Api";

        public TraceFilter(string endpoint, ITraceService service)
        {
            this.endpoint = endpoint;
            this.service = service;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.service.Start(ServiceName, this.endpoint);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            this.service.Finish();
        }
    }
}