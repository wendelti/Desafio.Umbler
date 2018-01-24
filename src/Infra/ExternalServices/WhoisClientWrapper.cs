using System.Threading.Tasks;
using Desafio.Umbler.Infra.CrossCutting;
using Whois.NET;

namespace Desafio.Umbler.Infra.ExternalServices
{

    public class WhoisClientWrapper : IWhoisClient {

        
        public virtual async Task<WhoisResponse> QueryAsync(string domainName){
            var response = await WhoisClient.QueryAsync(domainName);
            return response;
        }


    }
    
}