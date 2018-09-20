using Newtonsoft.Json;

namespace WebTrace.Weather.Models
{
    public class Rain
    {
        [JsonProperty("3h")]
        public double Precipitation { get; set; }
    }
}