using System.Collections.Generic;
using WebTrace.Weather.Models;

namespace WebTrace.Api.Models
{
    public class Dashboard
    {
        public Forecast Weather { get; set; }

        public IDictionary<string, decimal> Stocks { get; set; }
    }
}