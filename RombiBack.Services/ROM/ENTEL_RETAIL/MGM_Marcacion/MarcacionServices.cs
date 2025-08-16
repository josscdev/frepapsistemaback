using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Allocation;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Marcacion;
using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Marcacion
{
    public class MarcacionServices: IMarcacionServices
    {
        private readonly IMarcacionRepository _marcacionRepository;


        public MarcacionServices(IMarcacionRepository marcacionRepository, IMapper mapper)
        {
            _marcacionRepository = marcacionRepository;
        }

        public Respuesta PostEmpleadoAsistenciaMarcacion(string docusuario, DateTime fechamarcacion)
        {
            var respuesta = _marcacionRepository.PostEmpleadoAsistenciaMarcacion(docusuario, fechamarcacion);
            return respuesta;
        }

        public UserDataDTOResponse GetUsuarioMarcacion(string docusuario)
        {
            var respuesta = _marcacionRepository.GetUsuarioMarcacion( docusuario);
            return respuesta;
        }
    }
}
