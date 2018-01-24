using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Umbler.Infra.Data.Repositories
{
    public class DomainRepository     
    {
        DatabaseContext _context;
        public DomainRepository(DatabaseContext context){
            _context = context;
        }

        public async Task<Domain.Entities.Domain> GetByDomainNameAsync(string domain){
            return await _context.Domains.FirstOrDefaultAsync(d => d.Name == domain);
        }

        public bool Add(Domain.Entities.Domain domain){
            try
            {

                _context.Domains.Add(domain);
                _context.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        } 


        public void Update(){
            _context.SaveChanges();
        } 


    }


}