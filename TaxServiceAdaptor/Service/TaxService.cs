using System.Threading.Tasks;
using TaxServiceAdaptor.DTO;

namespace TaxServiceAdaptor
{
    public interface ITaxService
    {
        Task<T> HandleCommand<T>(Command<Request, T> cmd) where T : Response;
    }
    public abstract class TaxService<T> : BaseService<T>, ITaxService where T : ServiceType
    {
        public TaxService(T type, IHttpService httpService) : base(type, httpService) { }

        public async Task<Tout> HandleCommand<Tout>(Command<Request, Tout> cmd) where Tout : Response
        {
            cmd.Validate<Tout>();

            return await Post<Tout>(cmd);
        }
        public Command<Request, T1> CreateCommand<T1>(CommandType type, Request payload) where T1 : Response
        {
            return new Command<Request, T1>(type, payload);
        }
    }
}