using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.OracleLogistica;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_InventarioTPF
{
    public interface IOracleLogisticaTPFServices
    {
        Task<ResultadoCargaMasiva> PostOracleLogisticaMasivoTPF(List<OracleLogistica> registros, string usuario);
        Task<List<MatchResumenDto>> ObtenerConciliacionesAsyncTPF(FiltroInsertarConciliar parametros);

    }
}
