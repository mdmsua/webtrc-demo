using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebTrace.Api.Handlers;
using WebTrace.Api.Services;
using WebTrace.Domain.Options;
using WebTrace.Domain.Services;
using WebTrace.Stock.Clients;
using WebTrace.Weather.Clients;
using WebTrace.Weather.Options;

namespace WebTrace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<WeatherOptions>(this.Configuration.GetSection("Weather"));
            services.Configure<JaegerOptions>(this.Configuration.GetSection("Jaeger"));
            services.AddSingleton<IDashboardHandler, DashboardHandler>();
            services.AddSingleton<IStorageService, StorageService>();
            services.AddSingleton<ITraceService, TraceService>();
            services.AddSingleton<IMongoClient>(new MongoClient(this.Configuration.GetConnectionString("Atlas")));
            services.AddHttpClient<IWeatherClient, WeatherClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.openweathermap.org", UriKind.Absolute);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddHttpClient<IStockClient, StockClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.iextrading.com", UriKind.Absolute);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseMvc();
        }
    }
}
