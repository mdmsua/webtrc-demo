using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebTrace.Api.Filters;
using WebTrace.Api.Handlers;
using WebTrace.Api.Models;

namespace WebTrace.Api.Controllers
{
    [Route("[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardHandler handler;

        public DashboardController(IDashboardHandler handler)
        {
            this.handler = handler;
        }


        [HttpGet]
        [ProducesResponseType(typeof(Dashboard), (int)HttpStatusCode.OK)]
        [TypeFilter(typeof(TraceFilter), Arguments = new[] { "GET /dashboard" })]
        public async Task<IActionResult> Get() => this.Ok(await handler.HandleGetAsync(Request.HttpContext.RequestAborted));
    }
}
