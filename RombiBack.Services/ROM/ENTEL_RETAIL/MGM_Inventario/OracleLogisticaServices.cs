using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.OracleLogistica;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Inventario
{
    public class OracleLogisticaServices:IOracleLogisticaServices
    {
        private readonly IOracleLogisticaRepository _oraclelogisticaRepository;

        private readonly INotificationService _notifier;


        public OracleLogisticaServices(IOracleLogisticaRepository oraclelogisticaRepository, INotificationService notifier)
        {
            _oraclelogisticaRepository = oraclelogisticaRepository;
            _notifier = notifier;

        }

        public async Task<ResultadoCargaMasiva> PostOracleLogisticaMasivo(List<OracleLogistica> registros, string usuario)
        {
            var respuesta = await _oraclelogisticaRepository.PostOracleLogisticaMasivo(registros, usuario);
            //await _notifier.NotificarATodos($"Nuevo producto:");

            return respuesta;
        }

        public async Task<List<MatchResumenDto>> ObtenerConciliacionesAsync(FiltroInsertarConciliar parametros)
        {
            return await _oraclelogisticaRepository.ObtenerConciliacionesAsync(parametros);
        }
    }
}
