using System;
using System.Threading.Tasks;
using OAF.TaxService.Util.FiscalCodes;
using TaxServiceAdaptor.DTO;

namespace TaxServiceAdaptor
{
    public class TalkToZRA<T> : TaxService<T>, ITalkToZRA where T : ServiceType
    {
        public TalkToZRA(T type, IHttpService httpService) : base(type, httpService) { }
        Task<InvoiceNumberApplicationResponse> ITalkToZRA.ApplyInvoiceRanges(Request request)
        {
            return HandleCommand<InvoiceNumberApplicationResponse>(CreateCommand<InvoiceNumberApplicationResponse>(CommandType.INVOICE_APPLICATION__INVOICE_APP_R, request));
        }

        Task<PrivateKeyResponse> ITalkToZRA.ApplyPrivateKey(PrivateKeyRequest request)
        {
            return HandleCommand<PrivateKeyResponse>(CreateCommand<PrivateKeyResponse>(CommandType.PRIVATE_KEY_APPLICATION__R_R_01, request));
        }

        Task<ReactivationResponse> ITalkToZRA.ApplyReactivation(Request request)
        {
            return HandleCommand<ReactivationResponse>(CreateCommand<ReactivationResponse>(CommandType.REACTIVATION__RECOVER_R, request));
        }

        Task<TaxInfoApplicationResponse> ITalkToZRA.ApplyTaxInformation(Request request)
        {
            return HandleCommand<TaxInfoApplicationResponse>(CreateCommand<TaxInfoApplicationResponse>(CommandType.TAX_INFORMATION_APPLICATION__R_R_02, request));
        }

        Task<Response> ITalkToZRA.NotifyInitializationSuccess(Request request)
        {
            return HandleCommand<Response>(CreateCommand<Response>(CommandType.INITIALIZATION_SUCCESS_NOTIFICATION__R_R_03, request));
        }

        Task<HeartbeatResponse> ITalkToZRA.SendHeartBeatSignal(HeartbeatRequest request)
        {
            return HandleCommand<HeartbeatResponse>(CreateCommand<HeartbeatResponse>(CommandType.HEART_BEAT_MONITORING__MONITOR_R, request));
        }

        Task<TimeSyncResponse> ITalkToZRA.SyncTime(Request request)
        {
            return HandleCommand<TimeSyncResponse>(CreateCommand<TimeSyncResponse>(CommandType.TIME_SYNCHRONIZATION__SYS_TIME_R, request));
        }

        Task<Response> ITalkToZRA.UploadInvoice(InvoiceUploadRequest request)
        {
            return HandleCommand<Response>(CreateCommand<Response>(CommandType.INVOICE_UPLOAD__INVOICE_REPORT_R, request));
        }
        Task<string> ITalkToZRA.GetFiscalCode(string tpin,
            string terminalId,
            string invoiceCode,
            string invoiceNumber,
            DateTime date,
            decimal amount,
            string privateKey)
        {
            return Task.FromResult(FiscalCodeHelper.GetFiscalCode(tpin, terminalId, invoiceCode, invoiceNumber, date, amount, privateKey));
        }

    }
}