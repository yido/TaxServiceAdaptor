using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaxServiceAdaptor.DTO
{
    public class HeartbeatRequest : Request
    {
        [JsonProperty("lon")]
        public double Lon { get; set; }
        [JsonProperty("lat")]
        public double Lat { get; set; }
        [JsonProperty("sw_version")]
        [Required]
        [StringLength(10)]
        public string SwVersion { get; set; }
        [JsonProperty("batch")]
        [Required]
        [StringLength(16)]
        public string Batch { get; set; }
    }
}
