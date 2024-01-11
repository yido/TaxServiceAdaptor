using System;
using System.Threading.Tasks;
using TaxServiceAdaptor.DTO;

namespace TaxServiceAdaptor
{
    public interface ITalkToZRA
    {
        /// <summary>
        /// This method will handle SYS-TIME-R command, so that V-EFD should be able to realize time synchronization with the EFD system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>TimeSyncResponse</returns>
        Task<TimeSyncResponse> SyncTime(Request request);

        /// <summary>
        /// This method will handle INVOICE-REPORT-R command and used for V-EFD to upload the invoice details to the EFD system. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Response</returns>
        Task<Response> UploadInvoice(InvoiceUploadRequest request);

        /// <summary>
        ///  This method will handle R-R-03 command, After V-EFD has been initialized successfully, this will send a success information to the EFD system.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Response> NotifyInitializationSuccess(Request request);

        /// <summary>
        /// This method will handle RECOVER-R command and used for V-EFD to send reactivation requests to the EFD system when it is locked by the EFD system due to errors.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ReactivationResponse> ApplyReactivation(Request request);

        /// <summary>
        /// This method will handle R-R-01 command and used to apply for the private key and terminal ID from the EFD system using registration code.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PrivateKeyResponse> ApplyPrivateKey(PrivateKeyRequest request);

        /// <summary>
        /// This method will handle MONITOR-R command, which is used for the EFD system to monitor the online status of V-EFD. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<HeartbeatResponse> SendHeartBeatSignal(HeartbeatRequest request);

        /// <summary>
        /// This method will handle R-R-02 command, which can be used to apply tax information from the EFD system by terminal ID. 
        /// The tax information includes TPIN, tax type, tax category, tax rate, monitoring information, and so on.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<TaxInfoApplicationResponse> ApplyTaxInformation(Request request);

        /// <summary>
        /// This method will handle INVOICE-APP-R command and used for V-EFD to apply for new invoices to be issued from the EFD system. 
        /// The information of the InvoiceApplicationResponse will involve invoice code, Start No., End No. and the numbers of invoices in this section. 
        /// Once the remaining number of invoices is less than the threshold of Invoice Holding Quantity, V-EFDs should apply a new invoice section automatically from EFD System
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<InvoiceNumberApplicationResponse> ApplyInvoiceRanges(Request request);

        /// <summary>
        /// Computes Fiscal Code of an inovice
        /// </summary>
        /// <param name="tpin">TPIN: Length-18</param>
        /// <param name="terminalId">Terminal Id. Length-12</param>
        /// <param name="invoiceCode">Invoice Code. Length-12</param>
        /// <param name="invoiceNumber">Invoice Number. Length-8</param>
        /// <param name="date">Date. Invoicing Time. Human readable time. Format - YYYYMMDDHHMMSS. Length-14</param>
        /// <param name="amount">Amount. Length-20</param>
        /// <param name="privateKey">Private Key.</param>
        /// <returns>Computed Fiscal Code string.</returns>
        Task<string> GetFiscalCode(string tpin,
            string terminalId,
            string invoiceCode,
            string invoiceNumber,
            DateTime date,
            decimal amount,
            string privateKey);

    }
}