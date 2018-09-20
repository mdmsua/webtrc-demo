using System.Threading;
using System.Threading.Tasks;
using WebTrace.Api.Models;

namespace WebTrace.Api.Services
{
    public interface IStorageService
    {
        Task<Settings> GetSettingsAsync(CancellationToken cancellationToken);
    }   
}