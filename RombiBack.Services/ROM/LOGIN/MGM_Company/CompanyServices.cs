using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RombiBack.Entities.ROM.LOGIN.Company;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Products;
using RombiBack.Repository.ROM.LOGIN.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.LOGIN.Company
{
    public class CompanyServices : ICompanyServices
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IDistributedCache _cache;
        private readonly ILogger<CompanyServices> _logger;

        private IMapper _mapper;

        public CompanyServices(ILogger<CompanyServices> logger, IDistributedCache cache, ICompanyRepository companytRepository, IMapper mapper)
        {
            _logger = logger;

            _cache = cache;
            _companyRepository = companytRepository;
            _mapper = mapper;
        }
        public async Task<List<CompanyDTO>> GetCompany()
        {
            var companiesFromDatabase = await _companyRepository.GetCompany();
            return _mapper.Map<List<CompanyDTO>>(companiesFromDatabase);
        }



        //public async Task<List<CompanyDTO>> GetCompany()
        //{
        //    try
        //    {
        //        var cachedCompanies = await _cache.GetStringAsync("AllCompanies");
        //        if (cachedCompanies != null)
        //        {
        //            var companiesDTO = JsonSerializer.Deserialize<List<CompanyDTO>>(cachedCompanies);
        //            return companiesDTO;
        //        }
        //        else
        //        {
        //            var companiesFromDatabase = await _companyRepository.GetCompany();
        //            var companiesDTO = _mapper.Map<List<CompanyDTO>>(companiesFromDatabase);
        //            var serializedCompanies = JsonSerializer.Serialize(companiesDTO);
        //            await _cache.SetStringAsync("AllCompanies", serializedCompanies);

        //            return companiesDTO;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar cualquier excepción
        //        _logger.LogError(ex, "Error al obtener datos de la empresa. Se recurrirá a la base de datos.");

        //        // En caso de error, recuperar los datos directamente de la base de datos
        //        var companiesFromDatabase = await _companyRepository.GetCompany();
        //        return _mapper.Map<List<CompanyDTO>>(companiesFromDatabase);
        //    }
        //}


    }
}
