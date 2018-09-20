using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebTrace.Domain.Services;

namespace WebTrace.Stock.Clients
{
    public class StockClient : IStockClient
    {
        private readonly HttpClient client;

        private readonly ITraceService trace;

        private const string ServiceName = "Stock";

        public StockClient(HttpClient client, ITraceService trace)
        {
            this.client = client;
            this.trace = trace;
        }

        public async Task<decimal> GetStockPriceAsync(string symbol, CancellationToken cancellationToken)
        {
            var uri = $"/1.0/stock/{symbol}/price";
            var span = this.trace.Start(ServiceName, "GET /stock/price");
            var data = await this.client.GetStringAsync(uri);
            span.Log(new Dictionary<string, object> { { "symbol", symbol } });
            span.Finish();
            return decimal.TryParse(data, out var price) ? price : default;
        }
    }   
}