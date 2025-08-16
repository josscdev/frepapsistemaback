using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Dto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Reports.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Reports
{
    public interface IReportsServices /*: IServices<ReportsDTO>*/
    {
        Task<List<ReportsDTO>> GetAll();
        Task<List<Reports>> GetReportes(string docusuario, int idemppaisnegcue);


    }
}
