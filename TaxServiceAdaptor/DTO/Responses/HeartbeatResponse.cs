using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaxServiceAdaptor.DTO
{
    public class HeartbeatResponse : Response
    {
       [JsonProperty("notice")]
      public string Notice {get;set;}
      
      [JsonProperty("commands")]
      public List<HBCommand> Commands {get; set;}

      [JsonProperty("talktime")]
      public HBTalkTime TalkTime {get; set;}
    }

    public class HBCommand {
       [JsonProperty("cmd-id")]
       [Required][StringLength(10)] 
       public int Cmdid {get; set;}
       [JsonProperty("command")]
       [Required] 
       public string Command {get; set;}
    }
    public class HBTalkTime {
       [JsonProperty("batch")][StringLength(16)] 
       public string Batch {get; set;}

       [JsonProperty("content")]
       public List<HBContent> Content {get; set;}
    }
    public class HBContent {
         
           [JsonProperty("operaterID")][StringLength(10)] 
           public string OperaterID {get; set;}
           [JsonProperty("phoneScope")][StringLength(30)] 
           public string PhoneScope {get; set;}
           [JsonProperty("operater")]
           public string Operater {get; set;}
           [JsonProperty("PIN")]
           public List<int> PIN {get; set;}
           [JsonProperty("effect-time")]
           public long EffectTime {get; set;}
    } 

}
