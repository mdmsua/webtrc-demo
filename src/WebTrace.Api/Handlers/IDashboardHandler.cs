using System.Threading;
using System.Threading.Tasks;
using WebTrace.Api.Models;

namespace WebTrace.Api.Handlers
{
    public interface IDashboardHandler
    {
        Task<Dashboard> HandleGetAsync(CancellationToken cancellationToken);
    }
}