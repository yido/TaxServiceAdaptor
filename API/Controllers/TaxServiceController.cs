using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaxServiceAdaptor.DTO;

namespace API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TaxServiceController : BaseController
    {

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "This is sample test API For TalkToZRA 'SDK' Demo";
        }

        [HttpPost]
        [Route("SyncTime")]
        public async Task<TimeSyncResponse> SyncTime(Request request)
        {
            return await _taxService.SyncTime(request);
        }

        [HttpPost]
        [Route("UploadInvoice")]
        public async Task<Response> UploadInvoice(InvoiceUploadRequest request)
        {
            return await _taxService.UploadInvoice(request);
        }

        [HttpPost]
        [Route("NotifyInitializationSuccess")]
        public async Task<Response> NotifyInitializationSuccess(Request request)
        {
            return await _taxService.NotifyInitializationSuccess(request);
        }

        [HttpPost]
        [Route("ApplyReactivation")]
        public async Task<ReactivationResponse> ApplyReactivation(Request request)
        {
            return await _taxService.ApplyReactivation(request);
        }

        [HttpPost]
        [Route("ApplyPrivateKey")]
        public async Task<PrivateKeyResponse> ApplyPrivateKey(PrivateKeyRequest request)
        {
            return await _taxService.ApplyPrivateKey(request);
        }

        [HttpPost]
        [Route("SendHeartBeatSignal")]
        public async Task<HeartbeatResponse> SendHeartBeatSignal(HeartbeatRequest request)
        {
            return await _taxService.SendHeartBeatSignal(request);
        }

        [HttpPost]
        [Route("ApplyTaxInformation")]
        public async Task<TaxInfoApplicationResponse> ApplyTaxInformation(Request request)
        {
            return await _taxService.ApplyTaxInformation(request);
        }

        [HttpPost]
        [Route("ApplyInvoiceRanges")]
        public async Task<InvoiceNumberApplicationResponse> ApplyInvoiceRanges(Request request)
        {
            return await _taxService.ApplyInvoiceRanges(request);
        }

    }

}