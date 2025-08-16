using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Dto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports.DTO;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Reports
{
    public class ReportsServices : IReportsServices
    {
        private readonly IReportsRepository _reportRepository;
        private readonly IDistributedCache _cache;

        private readonly IMapper _mapper;

        public ReportsServices(IDistributedCache cache,IReportsRepository reportRepository, IMapper mapper)
        {
            _cache = cache;

            _reportRepository = reportRepository;
            _mapper = mapper;

        }
        #region SERVICES-PADRE

        public Task<ReportsDTO> Add(ReportsDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<ReportsDTO> Get(int id)
        {
            throw new NotImplementedException();
        }

     
        public Task<bool> Remove(ReportsDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<ReportsDTO> Update(ReportsDTO entity)
        {
            throw new NotImplementedException();
        }

        #endregion Atributos

        //public async Task<List<ReportsDTO>> GetAll()
        //{
        //    var reportes = await _reportRepository.GetAll();
        //    return _mapper.Map<List<ReportsDTO>>(reportes);
        //}
        public async Task<List<ReportsDTO>> GetAll()
        {
            // Verificar si los datos están en caché
            var cachedReports = await _cache.GetStringAsync("AllReports");

            if (cachedReports != null)
            {
                // Los datos están en caché, los deserializamos y los devolvemos
                var reportsDTO = JsonSerializer.Deserialize<List<ReportsDTO>>(cachedReports);
                return reportsDTO;
            }
            else
            {
                // Los datos no están en caché, los obtenemos de la base de datos
                var reportsFromDatabase = await _reportRepository.GetAll();

                // Mapeamos los datos al DTO
                var reportsDTO = _mapper.Map<List<ReportsDTO>>(reportsFromDatabase);

                // Serializamos los datos antes de guardarlos en caché
                var serializedReports = JsonSerializer.Serialize(reportsDTO);

                // Guardamos los datos en caché
                await _cache.SetStringAsync("AllReports", serializedReports);

                // Devolvemos los datos obtenidos
                return reportsDTO;
            }
        }

        public async Task<List<Reports>> GetReportes(string docusuario, int idemppaisnegcue)
        {
            var respuesta = await _reportRepository.GetReportes(docusuario,idemppaisnegcue);
            return respuesta;
        }
    }
}
