using System;
using System.Linq;
using System.Threading.Tasks;
using Desafio.Umbler.Infra.CrossCutting;
using Desafio.Umbler.Infra.Data;
using Desafio.Umbler.Infra.Data.Repositories;
using DnsClient;

namespace Desafio.Umbler.Application
{
    public class DomainAppService
    {

        DatabaseContext _db;
        DomainRepository _domainRepository;
        private ILookupClientWrapper _lookup;
        private IWhoisClient _whoisClient;

        public DomainAppService(DatabaseContext db,  ILookupClientWrapper lookup, IWhoisClient whoisClient)
        {            
            _lookup = lookup;
            _whoisClient = whoisClient;
            _db = db;
            _domainRepository = new DomainRepository(_db);
        }

        public async Task<ViewModels.DomainViewModel> QueryForDomainDataAsync(string domainName){
            
            if(!Umbler.Infra.CrossCutting.StringHelper.IsValidDomain(domainName)){
                return new ViewModels.DomainViewModel(){ Name = "Dominío Invalído"};                
            }

            var domain = await _domainRepository.GetByDomainNameAsync(domainName);

            if (domain == null)
            {
                domain = await FillDomainDataAsync(domainName);
                _domainRepository.Add(domain);
            }

            if (DateTime.Now.Subtract(domain.UpdatedAt).TotalMinutes > domain.Ttl)
            {
                var refreshedDomain = await FillDomainDataAsync(domainName);
                    
                domain.HostedAt = refreshedDomain.HostedAt;
                domain.Name = refreshedDomain.Name;
                domain.Ip = refreshedDomain.Ip;
                domain.WhoIs = refreshedDomain.WhoIs;
                domain.UpdatedAt = refreshedDomain.UpdatedAt;
                domain.Ttl = refreshedDomain.Ttl;
            
                _domainRepository.Update();
            }


            return Adapters.DomainEntityToDomainViewModel.GetViewModel(domain);
        }

        public async Task<Domain.Entities.Domain> FillDomainDataAsync(string domainName){

                Domain.Entities.Domain domain = new Domain.Entities.Domain();

                var result = await _lookup.QueryAsync(domainName, QueryType.ANY);
                
                var response = await _whoisClient.QueryAsync(domainName);

                var record = result.Answers.ARecords().FirstOrDefault();
                var address = record?.Address;
                var ip = address?.ToString();

                var hostResponse = await _whoisClient.QueryAsync(ip);

                domain.Name = domainName;
                domain.Ip = ip;
                domain.UpdatedAt = DateTime.Now;
                domain.WhoIs = response.Raw;
                domain.Ttl = record?.TimeToLive ?? 0;
                domain.HostedAt = hostResponse.OrganizationName;

                return domain;

        }

    }
}
