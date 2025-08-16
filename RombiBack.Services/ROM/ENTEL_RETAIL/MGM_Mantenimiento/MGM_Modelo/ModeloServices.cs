using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Modelo;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Modelo;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Modelo
{
    public class ModeloServices : IModeloServices
    {
        private readonly IModeloRepository _modeloRepository;


        public ModeloServices(IModeloRepository modeloRepository)
        {
            _modeloRepository = modeloRepository;
        }

        public async Task<List<Modelo>> GetModeloRomWeb(int idemppaisnegcue)
        {
            var respuesta = await _modeloRepository.GetModeloRomWeb(idemppaisnegcue);
            return respuesta;
        }

        public async Task<Respuesta> PostModeloRomWeb(Modelo modelo)
        {
            var respuesta = await _modeloRepository.PostModeloRomWeb(modelo);
            return respuesta;
        }

        public async Task<Respuesta> DeleteModeloRomWeb(Modelo modelo)
        {
            var respuesta = await _modeloRepository.DeleteModeloRomWeb(modelo);
            return respuesta;
        }

    }
}
