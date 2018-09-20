using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebTrace.Api.Models;
using WebTrace.Api.Services;
using WebTrace.Domain.Services;
using WebTrace.Stock.Clients;
using WebTrace.Weather.Clients;

namespace WebTrace.Api.Handlers
{
    public class DashboardHandler : IDashboardHandler
    {
        private readonly IStorageService storageService;
        private readonly IWeatherClient weatherClient;
        private readonly IStockClient stockClient;

        public DashboardHandler(IStorageService storageService, IWeatherClient weatherClient, IStockClient stockClient)
        {
            this.storageService = storageService;
            this.weatherClient = weatherClient;
            this.stockClient = stockClient;
        }

        public async Task<Dashboard> HandleGetAsync(CancellationToken cancellationToken)
        {
            var settings = await this.storageService.GetSettingsAsync(cancellationToken);
            var weather = await this.weatherClient.GetCurrentForecastByZipAsync(settings.Zip, cancellationToken);
            var stocks = new Dictionary<string, decimal>(settings.Stocks.Length);
            foreach (var symbol in settings.Stocks)
            {
                stocks[symbol] = await this.stockClient.GetStockPriceAsync(symbol, cancellationToken);
            }

            var dashboard = new Dashboard { Weather = weather, Stocks = stocks };
            return dashboard;
        }
    }
}