using Newtonsoft.Json;

namespace WebTrace.Weather.Models
{
    public class Clouds
    {
        [JsonProperty("all")]
        public double Cloudiness { get; set; }
    }
}