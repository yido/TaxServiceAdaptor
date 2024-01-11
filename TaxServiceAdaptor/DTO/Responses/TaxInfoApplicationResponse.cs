using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaxServiceAdaptor.DTO {
    public class TaxInfoApplicationResponse : Response {

        [JsonProperty("taxpayer")]
        [Required]
        public Taxpayer Taxpayer { get; set; }

        [JsonProperty("tax-control")]
        [Required]
        public TaxControl TaxControl { get; set; }

        [JsonProperty("tax-types")]
        [Required]
        public List<TaxTypes> TaxTypes { get; set; }

        [JsonProperty("backup-ip")]
        [Required]
        public List<BackupIp> BackupIp { get; set; }
    }
    public class Taxpayer {

        [JsonProperty("tpin")]
        [Required][StringLength(10)] 
        public long TPin { get; set; }

        [JsonProperty("vat")]
        [Required][StringLength(100)] 
        public string Vat { get; set; }

        [JsonProperty("name")]
        [Required][StringLength(200)] 
        public string Name { get; set; }
        
        [JsonProperty("address")]
        [Required][StringLength(200)] 
        public string Address { get; set; }
    }
    public class TaxControl {
        [JsonProperty("offline-num")]
        [Required]
        public int OfflineNum { get; set; }

        [JsonProperty("single-amount")]
        [Required][RegularExpression(@"^\d+\.\d{0,2}$")] 
        public decimal SingleAmount { get; set; }

        [JsonProperty("monthly-invoice-quantity")]
        [Required]
        public int MonthlyInvoicequantity { get; set; }

        [JsonProperty("monthly-credit-note-amount")]
        [Required][RegularExpression(@"^\d+\.\d{0,2}$")] 
        public decimal MonthlyCreditNoteAmount { get; set; }

        [JsonProperty("approved-date")]
        [Required]
        public long ApprovedDate { get; set; }

        [JsonProperty("approved-mode")]
        [Required] 
        public string ApprovedMode { get; set; }

        [JsonProperty("invoice-holding-quantity")]
        [Required] 
        public int InvoiceHoldingQuantity { get; set; }

        [JsonProperty("remain-threshold")]
        [Required]
        public int RemainThreshold { get; set; }

        [JsonProperty("monthly-credit-note-num")]
        [Required]
        public int MonthlyCreditNoteNum { get; set; }
    }
    public class TaxTypes {

        [JsonProperty("tax-type")]
        [Required]
        public string TaxType { get; set; }

        [JsonProperty("category")]
        [Required]
        public List<Category> Category { get; set; }
    }
    public class Category {

        [JsonProperty("no")]
        [Required]
        public int No { get; set; }

        [JsonProperty("tax-code")]
        [Required]
        public string TaxCode { get; set; }

        [JsonProperty("tax-name")]
        [Required]
        public string TaxName { get; set; }

        [JsonProperty("tax-rate")]
        [Required][RegularExpression(@"^\d+\.\d{0,2}$")] 
        public decimal TaxRate { get; set; }

        [JsonProperty("effective-date")]
        [Required]
        public long EffectiveDate { get; set; }

        [JsonProperty("expire-date")]
        [Required]
        public long ExpireDate { get; set; } 
    }
    public class BackupIp {

        [JsonProperty("server-ip")]
        [Required]
        public string ServerIp { get; set; }

        [JsonProperty("server-port")]
        [Required]
        public string ServerPort { get; set; }
    }
}