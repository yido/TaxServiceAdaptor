namespace TaxServiceAdaptor.DTO
{
    public class ServiceType
    {

        public ServiceType(string ApiUrl) { this.ApiUrl = ApiUrl; }
        public ServiceType() { }
        public string Device { get { return string.IsNullOrEmpty(this.TerminalId) ? this.RegistrationCode : this.TerminalId; } }
        public string TerminalId { get; set; }
        public string RegistrationCode { get; set; }
        public string TPIN { get; set; }
        public string Serial { get; set; }
        public string Sign { get; set; }
        public string Key { get; set; }

        public string ApiUrl { get; set; }
        public string PrivateKey { get; set; }
        public string DESKey { get; set; }
    }
    public class ZRAServiceType : ServiceType
    {
        //~ ZRA Specific More Props Can Go Here ~//
    }
}