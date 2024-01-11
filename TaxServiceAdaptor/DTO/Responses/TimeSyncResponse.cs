using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaxServiceAdaptor.DTO
{
    public class TimeSyncResponse : Response
    {

       [JsonProperty("time")]
       [Required]
       public long Time {get;set;}
    } 
}
