using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebTrace.Api.Models;
using WebTrace.Domain.Services;

namespace WebTrace.Api.Services
{
    public class StorageService : IStorageService
    {
        private readonly IMongoClient mongoClient;
        
        private readonly ITraceService traceService;

        private const string ServiceName = "Mongo";

        public StorageService(IMongoClient mongoClient, ITraceService traceService)
        {
            this.mongoClient = mongoClient;
            this.traceService = traceService;
        }

        public async Task<Settings> GetSettingsAsync(CancellationToken cancellationToken)
        {
            var span  = this.traceService.Start(ServiceName, "Dashboard:Settings");
            var collection = this.mongoClient.GetDatabase("WebTrace").GetCollection<Settings>("Settings");
            var settings = await (await collection.FindAsync(Builders<Settings>.Filter.Eq(s => s.Id, "default"))).SingleOrDefaultAsync();
            span.Log(new Dictionary<string, object> {{ "event", "find" }});
            if (settings is null)
            {
                settings = new Settings { Id = "default", Stocks = new[] { "aapl", "msft", "goog" }, Zip = "80636" };
                await collection.InsertOneAsync(settings, new InsertOneOptions(), cancellationToken);
                span.Log(new Dictionary<string, object> {{ "event", "insert" }});
            }

            span.Finish();
            return settings;
        }
    }
}