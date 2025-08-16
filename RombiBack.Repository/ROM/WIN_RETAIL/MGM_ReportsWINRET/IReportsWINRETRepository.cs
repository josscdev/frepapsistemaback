using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.ROM.WIN_RETAIL.Models.Reports;

namespace RombiBack.Repository.ROM.WIN_RETAIL.MGM_ReportsWINRET
{
    public interface IReportsWINRETRepository
    {
        Task<List<ReportsWINRET>> GetReportesWINRET(string docusuario, int idemppaisnegcue);
    }
}
