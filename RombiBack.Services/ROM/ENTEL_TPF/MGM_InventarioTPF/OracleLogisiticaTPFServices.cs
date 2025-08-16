using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.OracleLogistica;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_InventarioTPF;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_InventarioTPF
{
    public class OracleLogisiticaTPFServices: IOracleLogisticaTPFServices
    {
        private readonly IOracleLogisticaTPFRepository _oraclelogisticaTPFRepository;
        private readonly INotificationService _notifier;
        public OracleLogisiticaTPFServices(IOracleLogisticaTPFRepository oraclelogisticaTPFRepository, INotificationService notifier)
        {
            _oraclelogisticaTPFRepository = oraclelogisticaTPFRepository;
            _notifier = notifier;
        }
        public async Task<ResultadoCargaMasiva> PostOracleLogisticaMasivoTPF(List<OracleLogistica> registros, string usuario)
        {
            var respuesta = await _oraclelogisticaTPFRepository.PostOracleLogisticaMasivoTPF(registros, usuario);
            //await _notifier.NotificarATodos($"Nuevo producto:");

            return respuesta;
        }

        public async Task<List<MatchResumenDto>> ObtenerConciliacionesAsyncTPF(FiltroInsertarConciliar parametros)
        {
            return await _oraclelogisticaTPFRepository.ObtenerConciliacionesAsyncTPF(parametros);
        }
    }
}
