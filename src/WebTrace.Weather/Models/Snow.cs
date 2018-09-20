using Newtonsoft.Json;

namespace WebTrace.Weather.Models
{
    public class Snow
    {
        [JsonProperty("3h")]
        public double Volume { get; set; }
    }
}