using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Reports;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_ReportsTPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_ReportsTPF
{
    public class ReportsTPFServices: IReportsTPFServices
    {
        private readonly IReportsTPFRepository _reportTPFRepository;


        public ReportsTPFServices( IReportsTPFRepository reportTPFRepository)
        {

            _reportTPFRepository = reportTPFRepository;

        }
        public async Task<List<Reports>> GetReportesTPF(string docusuario, int idemppaisnegcue)
        {
            var respuesta = await _reportTPFRepository.GetReportesTPF(docusuario, idemppaisnegcue);
            return respuesta;
        }
    }
}
