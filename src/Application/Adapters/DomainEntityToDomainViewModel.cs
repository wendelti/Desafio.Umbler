using System;

namespace Desafio.Umbler.Application.Adapters
{
    public class DomainEntityToDomainViewModel
    {

        public static ViewModels.DomainViewModel GetViewModel(Domain.Entities.Domain domain){

            var domainViewModel = new ViewModels.DomainViewModel();
            domainViewModel.HostedAt = domain.HostedAt;
            domainViewModel.Name = domain.Name;
            domainViewModel.Ip = domain.Ip;
            domainViewModel.WhoIs = domain.WhoIs;
            
            return domainViewModel;
        }
        
    }
}