using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebTrace.Weather.Models
{
    public class Info
    {
        [JsonIgnore]
        public string Message { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("sunrise")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Sunrise { get; set; }

        [JsonProperty("sunset")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Sunset { get; set; }
    }
}