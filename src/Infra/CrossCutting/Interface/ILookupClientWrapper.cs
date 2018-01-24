using System.Threading.Tasks;
using DnsClient;

namespace Desafio.Umbler.Infra.CrossCutting
{

    public interface ILookupClientWrapper
    {
        Task<IDnsQueryResponse> QueryAsync(string domainName, QueryType type);  
    }

}