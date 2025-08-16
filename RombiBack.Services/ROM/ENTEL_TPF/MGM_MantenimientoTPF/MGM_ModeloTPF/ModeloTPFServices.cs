using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Modelo;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_ModeloTPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_ModeloTPF
{
    public class ModeloTPFServices : IModeloTPFServices
    {
        private readonly IModeloTPFRepository _modeloTPFRepository;

        public ModeloTPFServices(IModeloTPFRepository modeloTPFRepository)
        {
            _modeloTPFRepository = modeloTPFRepository;
        }

        public async Task<List<Modelo>> GetModeloRomWebTPF(int idemppaisnegcue)
        {
            var respuesta = await _modeloTPFRepository.GetModeloRomWebTPF(idemppaisnegcue);
            return respuesta;
        }

        public async Task<Respuesta> PostModeloRomWebTPF(Modelo modelo)
        {
            var respuesta = await _modeloTPFRepository.PostModeloRomWebTPF(modelo);
            return respuesta;
        }

        public async Task<Respuesta> DeleteModeloRomWebTPF(Modelo modelo)
        {
            var respuesta = await _modeloTPFRepository.DeleteModeloRomWebTPF(modelo);
            return respuesta;
        }
    }
}
