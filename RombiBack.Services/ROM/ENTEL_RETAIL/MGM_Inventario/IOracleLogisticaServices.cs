using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.OracleLogistica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Inventario
{
    public interface IOracleLogisticaServices
    {
        Task<ResultadoCargaMasiva> PostOracleLogisticaMasivo(List<OracleLogistica> registros, string usuario);
        Task<List<MatchResumenDto>> ObtenerConciliacionesAsync(FiltroInsertarConciliar parametros);

    }
}
