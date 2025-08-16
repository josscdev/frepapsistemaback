using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.ROM.WIN_RETAIL.Models.Reports;
using RombiBack.Repository.ROM.WIN_RETAIL.MGM_ReportsWINRET;

namespace RombiBack.Services.ROM.WIN_RETAIL.MGM_ReportsWINRET
{
    public class ReportsWINRETServices: IReportsWINRETServices
    {
        private readonly IReportsWINRETRepository _reportWINRETRepository;

        public ReportsWINRETServices(IReportsWINRETRepository reportWINRETRepository)
        {
            _reportWINRETRepository = reportWINRETRepository;
        }

        public async Task<List<ReportsWINRET>> GetReportesWINRET(string docusuario, int idemppaisnegcue)
        {
            var respuesta = await _reportWINRETRepository.GetReportesWINRET(docusuario, idemppaisnegcue);
            return respuesta;
        }
    }
}
