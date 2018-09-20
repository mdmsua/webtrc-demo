using Newtonsoft.Json;

namespace WebTrace.Weather.Models
{
    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public double Direction { get; set; }
        
        [JsonProperty("gust")]
        public double Gust { get; set; }
    }
}