using System.Threading;
using System.Threading.Tasks;

namespace WebTrace.Stock.Clients
{
    public interface IStockClient
    {
        Task<decimal> GetStockPriceAsync(string symbol, CancellationToken cancellationToken);
    }
}