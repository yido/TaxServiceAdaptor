using Microsoft.AspNetCore.Mvc;
using TaxServiceAdaptor;
using TaxServiceAdaptor.DTO;
namespace API.Controllers 
{

 public class BaseController : ControllerBase 
   {
       public BaseController() {

              _serviceType = new ZRAServiceType {
                ApiUrl = URL,
                RegistrationCode = "461905221063",
                TerminalId = "010100001183",
                Serial = "000001",
                DESKey = "95221063",
                PrivateKey = PRIVATE_KEY
            };
            _httpService = new HttpService (); 
            _taxService = new TalkToZRA<ServiceType> (_serviceType, _httpService); 
       }
        protected readonly ITalkToZRA _taxService;
        protected readonly ZRAServiceType _serviceType;
        private HttpService _httpService;
        private readonly string URL = @"http://196.223.29.24:8097/iface/index";
        private readonly string PRIVATE_KEY = @"MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBANM58KMsym1+Xb9BwnrdWW4CWhKd5U+qAVnSiATsN8k8mbiLNYS91gkxfD/6PExUaZNjtLdWhKvWtzpA0A4DQ9voAWaQUybJjDkRNUSBeClcub1Bpj5DjhxaLMq6y1pGDDCzU4+qWiUh3ErwBO93EXQ+qB9WbfvG6/Xwg9GaKNMZAgMBAAECgYEAxndoXUmWoi7b0vrsyxj0EGNxUpC9h47LWiRw3X8+I30nSriyfpkIZPb0MgDjayzdTjme8az/V56V5sxDMQdsV5xbESIZ4wEQf4R1z8XFLhJ0GuansHbBruuLMJYyDDtZoV6/+YHOzXVyzFIwoPiUY7+JDwUSrbX8Fch/oTm5oAkCQQDyM4SXVJy1kLF0gDKfV998GV9iVz0kwB0g+UVMk+zAcXHCdaHM5KyIRJ4ZjgSIERKETGFalY00IXCbpcsHGu1nAkEA30Kn6BHZh2rAo22nf74+J8Hxp2Pcd6vO9cb5CkNskXY1UKmtxK4OLfutLs/nmCp1+5eBbXVLAkQ9dQoFCfVrfwJAdJj0ld2363iS1WD5/dfR0O5uCuuwlcaev0cBY5I7AZIbj+ANWpQjsx5Fdkv2RoLhhSs4GuGwLo6CpYu1J+CAtQJACSZ0N0C0B6bKamhOGyAy7/I080VWfdkezFfxUQheZL/Rg5LQumTf0+aivG5s8YGcAEm/VjsebeNaGXflqPjUBQJAKS+iIscGz+ZapXh01WDrVEVT9Wk3Yh1klCIHdl+szzEG+VtmYBnMW+EHdkzKCaIyf5Nypf5q/uzM7gEuMg3ZQg==";
         

    }
}