using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaxServiceAdaptor.DTO
{
    public class InvoiceUploadRequest : Request
    {   
        [JsonProperty("POS-SN")]
        [Required][StringLength(30)] 
        public string POSSN  {get; set;}

        [JsonProperty("declaration-info")]
        [Required]
        public DeclarationInfo DeclarationInfo  {get; set;} 
    }


    public class DeclarationInfo {

        [JsonProperty("invoice-code")]
        [Required][StringLength(12)] 
        public string InvoiceCode  {get; set;}

        [JsonProperty("invoice-number")]
        [Required] 
        public string InvoiceNumber  {get; set;}

        [JsonProperty("buyer-tpin")]
        [StringLength(10)] 
        public string BuyerTpin  {get; set;}

        [JsonProperty("buyer-vat-acc-name")]
        [StringLength(100)] 
        public string BuyerVatAccName  {get; set;}

        [JsonProperty("buyer-name")]
        [StringLength(200)] 
        public string BuyerName  {get; set;}

        [JsonProperty("buyer-address")]
        [StringLength(200)] 
        public string BuyerAddress  {get; set;}

        [JsonProperty("buyer-tel")]
        [StringLength(20)] 
        public string BuyerTel  {get; set;}

        [JsonProperty("tax-amount")]
        [Required]//[RegularExpression(@"^\d+\.\d{0,2}$")] 
        public double TaxAmount  {get; set;}

        [JsonProperty("total-amount")]
        [Required]//[RegularExpression(@"^\d+\.\d{0,2}$")] 
        public double TotalAmount  {get; set;}

        [JsonProperty("total-discount")]
        //[RegularExpression(@"^\d+\.\d{0,2}$")] 
        public double TotalDiscount  {get; set;}

        [JsonProperty("invoice-status")]
        [Required][StringLength(2)]  
        public string InvoiceStatus  {get; set;}

        [JsonProperty("invoice-issuer")]
        [Required][StringLength(50)] 
        public string InvoiceIssuer  {get; set;} 

        [JsonProperty("invoicing-time")]
        [Required] 
        public long InvoicingTime  {get; set;} 

        [JsonProperty("old-invoice-code")]
        public string OldInvoiceCode  {get; set;} 

        [JsonProperty("old-invoice-number")]
        public string OldInvoiceNumber  {get; set;} 

        [JsonProperty("fiscal-code")]
        [Required][StringLength(20)] 
        public string FiscalCode  {get; set;} 

        [JsonProperty("memo")]
        [StringLength(100)] 
        public string Memo  {get; set;} 

        [JsonProperty("sale-type")]
        [Required]
        public int SaleType  {get; set;} 

        [JsonProperty("currency-type")]
        [Required][StringLength(3)] 
        public string CurrencyType  {get; set;} 

        [JsonProperty("conversion-rate")]
        [Required]//[RegularExpression(@"^\d+\.\d{0,4}$")]  
        public double ConversionRate  {get; set;} 

        [JsonProperty("local-purchase-order")]
        [StringLength(10)] 
        public string LocalPurchaseOrder  {get; set;} 

        [JsonProperty("voucher-PIN")]
        [StringLength(10)] 
        public string VoucherPIN  {get; set;} 

        [JsonProperty("items-info")]
        [Required]
        public List<ItemsInfo> ItemsInfo {get; set;}

        [JsonProperty("tax-info")]
        [Required]
        public List<TaxInfo> TaxInfo {get; set;}
    }
    public class ItemsInfo { 

        [JsonProperty("no")]
        [Required]
        public int No  {get; set;} 

        [JsonProperty("tax-category-code")]
        [Required]
        public string TaxCategoryCode  {get; set;} 

        [JsonProperty("tax-category-name")]
        [Required][StringLength(100)] 
        public string TaxCategoryName  {get; set;} 

        [JsonProperty("name")]
        [Required][StringLength(80)] 
        public string Name  {get; set;} 

        [JsonProperty("barcode")]
        [StringLength(100)] 
        public string BarCode  {get; set;}
        
        [JsonProperty("count")]
        [Required]//[RegularExpression(@"^\d+\.\d{0,4}$")]  
        public double Count  {get; set;}
        
        [JsonProperty("amount")]
        [Required]//[RegularExpression(@"^\d+\.\d{0,2}$")]  
        public double Amount  {get; set;} 

        [JsonProperty("tax-amount")]
        [Required]//[RegularExpression(@"^\d+\.\d{0,2}$")]  
        public double TaxAmount  {get; set;} 

        [JsonProperty("discount")]
        //[RegularExpression(@"^\d+\.\d{0,2}$")] 
        public double Discount  {get; set;} 

        [JsonProperty("unit-price")]
        [Required]//[RegularExpression(@"^\d+\.\d{0,2}$")]   
        public double UnitPrice  {get; set;} 

        [JsonProperty("tax-rate")]
        [Required]//[RegularExpression(@"^\d+\.\d{0,2}$")]  
        public double TaxRate  {get; set;} 

        [JsonProperty("rrp")]
        //[RegularExpression(@"^\d+\.\d{0,2}$")]  
        public double RRP  {get; set;}  
    }
    public class TaxInfo { 

        [JsonProperty("tax-code")]
        [Required][StringLength(20)] 
        public string TaxCode  {get; set;} 

        [JsonProperty("tax-name")]
        [Required][StringLength(50)] 
        public string TaxName  {get; set;} 

        [JsonProperty("tax-rate")]
        [Required]//[RegularExpression(@"^\d+\.\d{0,2}$")]
        public double TaxRate  {get; set;} 

        [JsonProperty("tax-value")]
        [Required]//[RegularExpression(@"^\d+\.\d{0,2}$")]
        public double TaxValue  {get; set;}  
    } 
}
