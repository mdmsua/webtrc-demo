using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebTrace.Domain.Services;
using WebTrace.Weather.Models;
using WebTrace.Weather.Options;

namespace WebTrace.Weather.Clients
{
    public class WeatherClient : IWeatherClient
    {
        private readonly HttpClient client;
        private readonly ITraceService trace;
        private readonly WeatherOptions options;

        private const string ServiceName = "Weather";

        public WeatherClient(HttpClient client, IOptions<WeatherOptions> options, ITraceService trace)
        {
            this.client = client;
            this.trace = trace;
            this.options = options.Value;
        }

        public async Task<Forecast> GetCurrentForecastByZipAsync(string zip, CancellationToken cancellationToken)
        {
            var uri = $"/data/2.5/weather?zip={zip},de&units=metric&appid={this.options.ApiKey}";
            var span = this.trace.Start(ServiceName, "GET /weather/zip");
            var data = await this.client.GetStringAsync(uri);
            span.Log(new Dictionary<string, object> { { "event", "weather@zip:res" }, { "zip", zip } });
            var forecast = JsonConvert.DeserializeObject<Forecast>(data);
            span.Finish();
            return forecast;
        }
    }   
}