using RombiBack.Entities.ROM.LOGIN.Business;
using RombiBack.Entities.ROM.LOGIN.Company;
using RombiBack.Entities.ROM.LOGIN.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.LOGIN.Company
{
    public interface ICompanyRepository 
    {
         Task<List<Companys>> GetCompany();
    }
}
