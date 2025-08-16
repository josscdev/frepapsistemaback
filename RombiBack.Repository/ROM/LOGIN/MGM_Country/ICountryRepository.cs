using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using RombiBack.Entities.ROM.LOGIN.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.LOGIN.MGM_Country
{
    public interface ICountryRepository /*: IRepository<Country>*/
    {
        Task<List<Country>> GetAll();
    }
}
