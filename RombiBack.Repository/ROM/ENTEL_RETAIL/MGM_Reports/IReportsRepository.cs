using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Reports
{
    public interface IReportsRepository /*: IRepository<Reports>*/
    {
        Task<List<Reports>> GetAll();

        Task<List<Reports>> GetReportes(string docusuario, int idemppaisnegcue);
    }
}
