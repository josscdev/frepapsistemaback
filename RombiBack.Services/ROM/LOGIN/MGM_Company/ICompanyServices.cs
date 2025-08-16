using RombiBack.Entities.ROM.LOGIN.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.LOGIN.Company
{
    public interface ICompanyServices
    {
        Task<List<CompanyDTO>> GetCompany();
    }
}
