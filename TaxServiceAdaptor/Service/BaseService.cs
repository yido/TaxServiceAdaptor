using System.Threading.Tasks;
using TaxServiceAdaptor.DTO;
using SystemException = TaxServiceAdaptor.DTO.Exceptions.SystemException;

namespace TaxServiceAdaptor
{
    public abstract class BaseService<T> where T : ServiceType {
        private IHttpService _httpService;
        protected T Type { get; set; }
        public BaseService (T _serviceType, IHttpService httpService) {
            this.Type = _serviceType;
            _httpService = httpService;
        }
        public async Task<T1> Post<T1> (Command<Request, T1> cmd) where T1 : Response 
        {
            var msg = await _httpService
                .PostAsync<Message<ServiceType>, Message<ServiceType>> (cmd.ToMessage (this.Type).Encrypt(), this.Type.ApiUrl);
            
            if(msg.BusId == "unknown")
                throw new SystemException(ReturnCodes.UNKNOWN_ERROR);
            
            var is_private_key_application = cmd.CommandType == CommandType.PRIVATE_KEY_APPLICATION__R_R_01;

            T1 response = msg.SetType(this.Type)
                             .Dycrypt(is_private_key_application)
                             .ToResponse<T1> ();
             
            if(response.Code != ((int) ReturnCodes.SUCCESS).ToString())
                throw new SystemException(response.ReturnCode);

            return response;
        }
    }
}