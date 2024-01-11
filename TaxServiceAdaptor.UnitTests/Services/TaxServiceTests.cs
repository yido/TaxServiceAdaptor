using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;
using TaxServiceAdaptor.DTO;

namespace TaxServiceAdaptor.UnitTests
{
    public class TaxServiceTests
    {
        private readonly string URL = @"http://";
        private readonly string PRIVATE_KEY = @"XXXX";
        private HttpService _httpService;
        private ServiceType _serviceType;

        private ITalkToZRA _taxService;

        [SetUp]
        public void Setup()
        {
            _serviceType = new ZRAServiceType {
                ApiUrl = URL,
                RegistrationCode = "546572023873",
                TerminalId = "010100000042",
                TPIN = "000000001003290331",
                Serial = "000001",
                DESKey = "72023873",
                PrivateKey = PRIVATE_KEY
            };

            _httpService = new HttpService();

            _taxService = new TalkToZRA<ServiceType>(_serviceType, _httpService);
        }

        [Test]
        public void Tax_Service_Should_Be_Defined()
        {
            _taxService.Should().NotBeNull();
        }

        [Test]
        public void Tax_Service_Model_Validations()
        {
            var privateKeyRequest = new PrivateKeyRequest() { };

            var validator = new DataAnnotationsValidator();
            var validationResults = new List<ValidationResult>();
            var isValid = validator.TryValidateObjectRecursive(privateKeyRequest, validationResults);

            isValid.Should().BeFalse();
        }

        [Test]
        //~ R-R-01 ~//
        public void Tax_Service_Should_Handle_PrivateKeyApplication_Command()
        {
            //~ Should be Singleton classl instanciated via DI ~//
            _serviceType = new ZRAServiceType {
                ApiUrl = URL,
                RegistrationCode = "546572023873",
                TerminalId = "010100000042",
                Serial = "000001",
                DESKey = "72023873",
                PrivateKey = PRIVATE_KEY
            };

            //~ Should be hooked via DI ~//
            _httpService = new HttpService();
            _taxService = new TalkToZRA<ServiceType>(_serviceType, _httpService);

            //~ Pram for Command (here for applying private key)
            var privateKeyRequest = new PrivateKeyRequest() {
                License = "546572023873",
                Sn = "ONEACRE VEFD",
                SwVersion = "1.0",
                Model = "IP-100",
                Manufacture = "Inspur Software Group",
                Imei = "",
                OS = "linux2.6.36",
                HWSN = "3458392322"
            };
            try
            {
                //~ How to use one command from ITalkToZRA ~//
                var response = _taxService.ApplyPrivateKey(privateKeyRequest).Result;

                //~ Just a smaple unit test validating a localy saved key with the response, this is temp thing ~//
                response.Secret.Should().Be(PRIVATE_KEY);
            }
            catch (Exception ex)
            {
                (ex.InnerException as TaxServiceAdaptor.DTO.Exceptions.SystemException).ReturnCode.Should().Be(ReturnCodes.THE_DEVICE_HAS_BEEN_REGISTERED);
            }

        }

        [Test]
        //~  R-R-02 ~//
        public void Tax_Service_Should_Handle_Registration_Request_Command()
        {

            var request = new Request { Id = "010100000042" };
            var response = _taxService.ApplyTaxInformation(request).Result;

            response.Code.Should().Be("200");

        }

        [Test]
        //~  R-R-03 ~//
        public void Tax_Service_Should_Handle_Notify_Initialization_Success_Command()
        {

            var request = new Request { Id = "010100000042" };
            try
            {
                var response = _taxService.NotifyInitializationSuccess(request).Result;
                response.Code.Should().Be("200");
            }
            catch (Exception ex)
            {
                (ex.InnerException as TaxServiceAdaptor.DTO.Exceptions.SystemException).ReturnCode.Should().Be(ReturnCodes.THE_DEVICE_HAS_BEEN_REGISTERED);
            }
        }

        [Test]
        //~INVOICE-APP-R ~//
        public void Tax_Service_Should_Handle_Invoice_Application_Command()
        {

            var request = new Request { Id = "010100000042" };
            var response = _taxService.ApplyInvoiceRanges(request).Result;

            response.Invoice[0].NumberBegin.Should().NotBeEmpty();
        }

        [Test]
        //~ ===> NOT WORKING <==  INVOICE-REPORT-R ~//
        public void Tax_Service_Should_Handle_Invoice_upload_Command()
        {

            var request = new InvoiceUploadRequest {
                Id = "010100000042",
                POSSN = "092344823532",
                DeclarationInfo = new DeclarationInfo {
                    InvoiceCode = "000210110000",
                    InvoiceNumber = "00036438",
                    BuyerTpin = "0100022473",
                    BuyerVatAccName = "40168862",
                    BuyerName = "Yididiya",
                    BuyerAddress = "Kigali, Rwanda",
                    BuyerTel = "0784955919",
                    TaxAmount = 1.3,
                    TotalAmount = 10,
                    TotalDiscount = 0,
                    InvoiceStatus = "01",
                    InvoiceIssuer = "Cashier01",
                    InvoicingTime = 1617971848,
                    OldInvoiceCode = "",
                    OldInvoiceNumber = "",
                    FiscalCode = "11298303812903938000", // Change this,   
                    Memo = "I don't have any memo for today.",
                    CurrencyType = "USD",
                    ConversionRate = 6.5434,
                    SaleType = 0,
                    LocalPurchaseOrder = "3452345342",
                    VoucherPIN = "0377823442",
                    ItemsInfo = new List<ItemsInfo>
                    {
                        new ItemsInfo {
                            No = 1,
                            TaxCategoryCode ="A",
                            TaxCategoryName="Standard Rate",
                            Name ="Maize",
                            BarCode="6009706160821",
                            Count=1.00,
                            Amount=10.00,
                            TaxAmount=1.30,
                            Discount=0,
                            UnitPrice=10.00,
                            TaxRate=0.15,
                            RRP=12.00
                        },
                         new ItemsInfo {
                            No=2,
                            TaxCategoryCode="B",
                            TaxCategoryName="MTV",
                            Name="Banana",
                            BarCode="6009706160821",
                            Count=1.00,
                            Amount=10.00,
                            TaxAmount=1.30,
                            Discount=0,
                            UnitPrice=10.00,
                            TaxRate=0.15,
                            RRP=12.00
                        },
                            new ItemsInfo {
                            No=3,
                            TaxCategoryCode="A",
                            TaxCategoryName="Standard Rate",
                            Name="Tomato Tree",
                            BarCode="6009706160821",
                            Count=1.00,
                            Amount=10.00,
                            TaxAmount=1.30,
                            Discount=0,
                            UnitPrice=10.00,
                            TaxRate=0.15,
                            RRP=12.00
                        }
                    },
                    TaxInfo = new List<TaxInfo> {
                        new TaxInfo {
                            TaxCode="A",
                            TaxName="standard rate",
                            TaxRate=0.16,
                            TaxValue=100
                        },
                        new TaxInfo {
                            TaxCode="C1",
                            TaxName="export",
                            TaxRate=0,
                            TaxValue=0
                        },
                        new TaxInfo {
                            TaxCode="T",
                            TaxName="Tourism Levy",
                            TaxRate=0.015,
                            TaxValue=30
                        }
                    }

                },
            };
            var fiscalCode = _taxService.GetFiscalCode(_serviceType.TPIN, _serviceType.TerminalId, request.DeclarationInfo.InvoiceCode, request.DeclarationInfo.InvoiceNumber, DateTime.UtcNow, Convert.ToDecimal(request.DeclarationInfo.TotalAmount), PRIVATE_KEY).Result;
            request.DeclarationInfo.FiscalCode = fiscalCode;
            var response = _taxService.UploadInvoice(request).Result;

            response.Code.Should().Be("200");
        }

        [Test]
        //~SYS-TIME-R ~//
        public void Tax_Service_Should_Handle_Time_Synchronization_Command()
        {

            var request = new Request { Id = "010100000042" };
            var response = _taxService.SyncTime(request).Result;

            var server_time = DateTimeOffset.FromUnixTimeSeconds(response.Time).DateTime;
            server_time.Hour.Should().BeLessOrEqualTo(DateTime.UtcNow.Hour);
        }

        [Test]
        //~MONITOR-R ~//
        public void Tax_Service_Should_Handle_Heartbeat_Monitoring_Command()
        {

            var request = new HeartbeatRequest {
                Id = "010100000042",
                Lon = 100.832004,
                Lat = 45.832004,
                SwVersion = "1.0",
                Batch = "0000000000000001"
            };
            var response = _taxService.SendHeartBeatSignal(request).Result;

            response.TalkTime.Batch.Should().NotBeNull();
        }
        [Test]
        //~RECOVER-R ~//
        public void Tax_Service_Should_Handle_Reactivation_Command()
        {

            var request = new Request { Id = "010100000042" };
            var response = _taxService.ApplyReactivation(request).Result; 
            response.Code.Should().Be("200");
            
        }
        [Test]
        //~ Fiscal Code .dll ~//
        public void Tax_Service_Should_Generate_Fiscal_Code_When_Using_The_Dll_Library()
        {
            var _amount = (decimal.Round(10, 2)).ToString("#.00").PadLeft(20, '0'); 
            var date = DateTimeOffset.Parse("2021-07-20T08:15:21.337Z").UtcDateTime;
            var fiscalCode = _taxService.GetFiscalCode("000000001003290331", "010100000042", "000210110000", "00013387", date, Convert.ToDecimal(10), PRIVATE_KEY).Result;

            fiscalCode.Should().NotBeNull();
        }
        /// <summary>
        /// Convert Unix time value to a DateTime object.
        /// </summary>
        /// <param name="unixtime">The Unix time stamp you want to convert to DateTime.</param>
        /// <returns>Returns a DateTime object that represents value of the Unix time.</returns>
        public  DateTime FromUnixTimeStampToDateTime( long unixTimeStamp)
        {
          return DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).UtcDateTime;
        }
    }
}