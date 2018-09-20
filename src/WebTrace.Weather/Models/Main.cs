using Newtonsoft.Json;

namespace WebTrace.Weather.Models
{
    public class Main
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("temp_min")]
        public double MinimumTemperature { get; set; }
        
        [JsonProperty("temp_max")]
        public double MaximumTemperature { get; set; }
        
        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("sea_level")]
        public double SeaLevelPressure { get; set; }
        
        [JsonProperty("grnd_level")]
        public double GroundLevelPressure { get; set; }
    }
}