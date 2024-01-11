using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaxServiceAdaptor.DTO
{
    public class PrivateKeyRequest : Request
    {
        public PrivateKeyRequest() { this.Id = "_"; }

        [JsonProperty("license")]
        [Required]
        [StringLength(12)]
        public string License { get; set; }

        [JsonProperty("sn")]
        [Required]
        [StringLength(50)]
        public string Sn { get; set; }

        [JsonProperty("sw_version")]
        [Required]
        [StringLength(50)]
        public string SwVersion { get; set; }

        [JsonProperty("model")]
        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [JsonProperty("manufacture")]
        [Required]
        [StringLength(50)]
        public string Manufacture { get; set; }

        [JsonProperty("imei")]
        public string Imei { get; set; }

        [JsonProperty("os")]
        [Required]
        [StringLength(30)]
        public string OS { get; set; }

        [JsonProperty("hw_sn")]
        [Required]
        [StringLength(30)]
        public string HWSN { get; set; }
    }
}
