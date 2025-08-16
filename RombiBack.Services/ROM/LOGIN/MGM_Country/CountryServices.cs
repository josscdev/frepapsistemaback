using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Mappers;
using RombiBack.Entities.ROM.LOGIN.Country;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Products;
using RombiBack.Repository.ROM.LOGIN.MGM_Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.LOGIN.MGM_Country
{
    public class CountryServices : ICountryServices
    {
        private readonly ICountryRepository _countryRepository;

        private const string CountriesKey = "Countries";
        private readonly IDistributedCache _distributedCache;


        private readonly IMapper _mapper;

        public CountryServices(ICountryRepository countryRepository, IMapper mapper, IDistributedCache distributedCache)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _distributedCache = distributedCache;

        }
      
        public async Task<List<Country>> GetAll()
        {

            return await _countryRepository.GetAll();
        }

       
    }
}
