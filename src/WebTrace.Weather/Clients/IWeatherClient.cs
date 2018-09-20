using System.Threading;
using System.Threading.Tasks;
using WebTrace.Weather.Models;

namespace WebTrace.Weather.Clients
{
    public interface IWeatherClient
    {
        Task<Forecast> GetCurrentForecastByZipAsync(string zip, CancellationToken cancellationToken);
    }
}