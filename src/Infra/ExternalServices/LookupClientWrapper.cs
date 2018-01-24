
using System.Linq;
using System.Threading.Tasks;
using Desafio.Umbler.Infra.CrossCutting;
using DnsClient;
using DnsClient.Protocol;
using Whois.NET;

namespace Desafio.Umbler.Infra.ExternalServices
{

    public class LookupClientWrapper : ILookupClientWrapper {

        
        public virtual async Task<IDnsQueryResponse> QueryAsync(string domainName, QueryType type){
            var _lookup = new LookupClient();
            var result = await _lookup.QueryAsync(domainName, type);
            return result;
            // return result.Answers.ARecords().FirstOrDefault();
        }


    }

}