using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaxServiceAdaptor.DTO
{
    public class Response
    {  
       [JsonProperty("code")]
       [Required][StringLength(3)] 
        public string Code {get;set;}

       [JsonProperty("desc")]
       [Required][StringLength(100)] 
        public string Desc {get;set;}


       [JsonIgnore] 
       public ReturnCodes ReturnCode {
           get
           {
              try
              {
                return (ReturnCodes) int.Parse(this.Code);
              }
              catch (System.Exception)
              {
                 return ReturnCodes.UNKNOWN_ERROR;
              }
           }
        }

    }
}
