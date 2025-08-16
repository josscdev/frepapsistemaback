using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_TPF.MGM_ReportsTPF
{
    public interface IReportsTPFRepository
    {
        Task<List<Reports>> GetReportesTPF(string docusuario, int idemppaisnegcue);
    }
}
