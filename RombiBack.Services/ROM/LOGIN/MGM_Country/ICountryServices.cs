using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using RombiBack.Entities.ROM.LOGIN.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.LOGIN.MGM_Country
{
    public interface ICountryServices/* : IServices<Country>*/
    {
        Task<List<Country>> GetAll();

    }
}
