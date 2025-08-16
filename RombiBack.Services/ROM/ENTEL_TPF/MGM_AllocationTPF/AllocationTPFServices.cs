using AutoMapper;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Allocation;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Allocation;
using RombiBack.Repository.ROM.ENTEL_TPF.MGM_Allocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_Allocation
{
    public class AllocationTPFServices:IAllocationTPFServices
    {
        private readonly IAllocationTPFRepository _allocationTPFRepository;


        public AllocationTPFServices(IAllocationTPFRepository allocationTPFRepository, IMapper mapper)
        {
            _allocationTPFRepository = allocationTPFRepository;
        }

        public List<Roles> GetAllRolPromotorTPF(string usuario, int idemppaisnegcue, string tipoperiodo)
        {
            var respuesta = _allocationTPFRepository.GetAllRolPromotorTPF(usuario, idemppaisnegcue, tipoperiodo);
            return respuesta;
        }

        public List<Roles> GetRolUsuarioPDVTPF(string usuario, int idemppaisnegcue, string tipoperiodo)
        {
            var respuesta = _allocationTPFRepository.GetRolUsuarioPDVTPF(usuario, idemppaisnegcue, tipoperiodo);
            return respuesta;
        }

        public List<Respuesta> ValidarBotonRegistroVentasTPF(string usuario, int idemppaisnegcue)
        {
            var respuesta = _allocationTPFRepository.ValidarBotonRegistroVentasTPF(usuario, idemppaisnegcue);
            return respuesta;
        }

        public List<Roles> GetRolPromotorDocUsuarioTPF(string usuario, int idemppaisnegcue, string tipoperiodo, string usuarioperfil)
        {
            var respuesta = _allocationTPFRepository.GetRolPromotorDocUsuarioTPF(usuario, idemppaisnegcue, tipoperiodo, usuarioperfil);
            return respuesta;
        }

        public List<Roles> GetRolTipoEstadoTPF(int idemppaisnegcue)
        {
            var respuesta = _allocationTPFRepository.GetRolTipoEstadoTPF(idemppaisnegcue);
            return respuesta;
        }

        public List<Roles> GetRolTipoFuncionalidadTPF(int idemppaisnegcue)
        {
            var respuesta = _allocationTPFRepository.GetRolTipoFuncionalidadTPF(idemppaisnegcue);
            return respuesta;
        }

        public List<Roles> GetRolTipoLicenciaTPF(int idemppaisnegcue)
        {
            var respuesta = _allocationTPFRepository.GetRolTipoLicenciaTPF(idemppaisnegcue);
            return respuesta;
        }

        public List<Roles> GetRolTipoTrabajoTPF(int idemppaisnegcue)
        {
            var respuesta = _allocationTPFRepository.GetRolTipoTrabajoTPF(idemppaisnegcue);
            return respuesta;
        }


        public List<Roles> GetRolPdvTPF(int idemppaisnegcue)
        {
            var respuesta = _allocationTPFRepository.GetRolPdvTPF(idemppaisnegcue);
            return respuesta;
        }

        public List<Respuesta> PostRolesTPF(List<Roles> roles)
        {
            var respuesta = _allocationTPFRepository.PostRolesTPF(roles);
            return respuesta;
        }

        public List<Respuesta> PutRolesTPF(List<Roles> roles)
        {
            var respuesta = _allocationTPFRepository.PutRolesTPF(roles);
            return respuesta;
        }

    }
}
