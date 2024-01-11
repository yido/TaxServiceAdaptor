using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaxServiceAdaptor.DTO
{
    public class Request
    {  
       [JsonProperty("id")]
       [Required][StringLength(12)] 
       [Display(Name = "TerminalID")]
        public string Id {get; set;} //~ TerminalID ~// 
    }
}
