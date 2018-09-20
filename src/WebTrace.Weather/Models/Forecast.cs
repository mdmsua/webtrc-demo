using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebTrace.Weather.Models
{
    public class Forecast
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("dt")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("name")]
        public string City { get; set; }

        [JsonProperty("coord")]
        public Location Location { get; set; }

        [JsonProperty("sys")]
        public Info Info { get; set; }

        [JsonProperty("main")]
        public Main Main { get; set; }
        
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        
        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }
        
        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }
     
        [JsonProperty("rain")]
        public Rain Rain { get; set; }
        
        [JsonProperty("snow")]
        public Snow Snow { get; set; }
    }
}