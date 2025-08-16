using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Allocation;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Allocation;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Allocation
{
    public class AllocationServices : IAllocationServices
    {
        private readonly IAllocationRepository _allocationRepository;


        public AllocationServices(IAllocationRepository allocationRepository, IMapper mapper)
        {
            _allocationRepository = allocationRepository;
        }


        public List<Roles> GetAllRolPromotor(string usuario, int idemppaisnegcue, string tipoperiodo)
        {
            var respuesta = _allocationRepository.GetAllRolPromotor(usuario, idemppaisnegcue, tipoperiodo);
            return respuesta;
        }

        public List<Roles> GetRolPromotorDocUsuario(string usuario, int idemppaisnegcue, string tipoperiodo, string usuarioperfil)
        {
            var respuesta = _allocationRepository.GetRolPromotorDocUsuario(usuario, idemppaisnegcue, tipoperiodo, usuarioperfil);
            return respuesta;
        }

        public List<Roles> GetRolTipoEstado(int idemppaisnegcue)
        {
            var respuesta = _allocationRepository.GetRolTipoEstado(idemppaisnegcue);
            return respuesta;
        }

        public List<Roles> GetRolTipoFuncionalidad(int idemppaisnegcue)
        {
            var respuesta = _allocationRepository.GetRolTipoFuncionalidad(idemppaisnegcue);
            return respuesta;
        }

        public List<Roles> GetRolTipoLicencia(int idemppaisnegcue)
        {
            var respuesta = _allocationRepository.GetRolTipoLicencia(idemppaisnegcue);
            return respuesta;
        }

        public List<Roles> GetRolTipoTrabajo(int idemppaisnegcue)
        {
            var respuesta = _allocationRepository.GetRolTipoTrabajo(idemppaisnegcue);
            return respuesta;
        }


        public List<Roles> GetRolPdv(int idemppaisnegcue)
        {
            var respuesta = _allocationRepository.GetRolPdv(idemppaisnegcue);
            return respuesta;
        }

        public List<Respuesta> PostRoles(List<Roles> roles)
        {
            var respuesta = _allocationRepository.PostRoles(roles);
            return respuesta;
        }

        public List<Respuesta> PutRoles(List<Roles> roles)
        {
            var respuesta = _allocationRepository.PutRoles(roles);
            return respuesta;
        }

        public List<Roles> GetRolUsuarioPDV(string usuario, int idemppaisnegcue, string tipoperiodo)
        {
            var respuesta = _allocationRepository.GetRolUsuarioPDV(usuario, idemppaisnegcue, tipoperiodo);
            return respuesta;
        }

        public List<Respuesta> ValidarBotonRegistroVentas(string usuario, int idemppaisnegcue)
        {
            var respuesta = _allocationRepository.ValidarBotonRegistroVentas(usuario, idemppaisnegcue);
            return respuesta;
        }


        public List<UsuariosEntel> GetUsuariosEntel(int idemppaisnegcue)
        {
            var respuesta = _allocationRepository.GetUsuariosEntel( idemppaisnegcue);
            return respuesta;
        }

        public List<Respuesta> PostUsuarioEntel(List<UsuariosEntel> usuariosentel)
        {
            var respuesta = _allocationRepository.PostUsuarioEntel(usuariosentel);
            return respuesta;
        }

        public Respuesta PutUsuarioEntel(UsuariosEntel usuarioEntel)
        {
            var respuesta = _allocationRepository.PutUsuarioEntel(usuarioEntel);
            return respuesta;
        }

        public Respuesta DeleteUsuarioEntel(int idUsuarioEntel, string usuarioEliminacion)
        {
            var respuesta = _allocationRepository.DeleteUsuarioEntel(idUsuarioEntel, usuarioEliminacion);
            return respuesta;
        }

        public NombreExcel GetNombreExcel(int idemppaisnegcue, string vista)
        {
            var respuesta = _allocationRepository.GetNombreExcel(idemppaisnegcue, vista);
            return respuesta;
        }

    }
}
