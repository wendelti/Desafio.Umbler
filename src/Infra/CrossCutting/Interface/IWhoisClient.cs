using System.Threading.Tasks;
using Whois.NET;

namespace Desafio.Umbler.Infra.CrossCutting
{

    public interface IWhoisClient
    {
        Task<WhoisResponse> QueryAsync(string domainName);  
    }

}