using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaxServiceAdaptor.DTO
{
    public class Message<T> where T : ServiceType  {
        public Message(T t){
            this.ServiceType = t;
        }
        public Message(){}

        [JsonIgnore]
        public T ServiceType {get; private set;} 

       [JsonProperty("device")] 
       [Required][StringLength(12)]
        public string Device { get; set; }

       [JsonProperty("bus_id")][Required]
       public string BusId { get; set; }
        
       [JsonProperty("serial")][Required][StringLength(6)]
        public string Serial { get;  set; }
       [JsonProperty("sign")][Required]  
        public string Sign { get; set; }
       [JsonProperty("key")][Required]  
        public string Key { get; set; } 
       [JsonProperty("content")][Required]  
        public string Content { get; set; }

        public Message<T> SetType(T type){ this.ServiceType = type; return this;}
        public T1 ToResponse<T1>() where T1 : Response 
        { 
            T1 res = null;
            try
            {
                res = JsonConvert.DeserializeObject<T1>(this.Content);
            }
            catch (System.Exception e) { throw e; }
            
            return res;
        }
    }
}