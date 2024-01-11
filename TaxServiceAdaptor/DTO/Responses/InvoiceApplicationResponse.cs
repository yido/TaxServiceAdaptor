using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaxServiceAdaptor.DTO
{
    public class InvoiceNumberApplicationResponse : Response
    {
        [JsonProperty("invoice")]
        [Required]
        public List<InvoiceSection> Invoice { get; set; }
    }

    public class InvoiceSection
    {
        [JsonProperty("code")]
        [Required]
        [StringLength(12)]
        public string Code { get; set; }

        [JsonProperty("number-begin")]
        [Required]
        [StringLength(12)]
        public string NumberBegin { get; set; }

        [JsonProperty("number-end")]
        [Required]
        [StringLength(12)]
        public string NumberEnd { get; set; }
    }
}