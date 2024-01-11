using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaxServiceAdaptor.DTO
{
    public class PrivateKeyResponse : Response
    {
        [JsonProperty("id")]
        [Required]
        [StringLength(12)]
        public string Id { get; set; }
        [JsonProperty("secret")]
        [Required]
        [StringLength(1000)]
        public string Secret { get; set; }

    }
}
