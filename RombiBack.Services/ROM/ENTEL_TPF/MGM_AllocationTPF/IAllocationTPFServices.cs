using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Allocation;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_TPF.MGM_Allocation
{
    public interface IAllocationTPFServices
    {
        List<Roles> GetAllRolPromotorTPF(string usuario, int idemppaisnegcue, string tipoperiodo);
        List<Roles> GetRolUsuarioPDVTPF(string usuario, int idemppaisnegcue, string tipoperiodo);
        List<Respuesta> ValidarBotonRegistroVentasTPF(string usuario, int idemppaisnegcue);
        List<Roles> GetRolTipoFuncionalidadTPF(int idemppaisnegcue);
        List<Roles> GetRolTipoEstadoTPF(int idemppaisnegcue);
        List<Roles> GetRolTipoTrabajoTPF(int idemppaisnegcue);
        List<Roles> GetRolTipoLicenciaTPF(int idemppaisnegcue);
        List<Roles> GetRolPromotorDocUsuarioTPF(string usuario, int idemppaisnegcue, string tipoperiodo, string usuarioperfil);
        List<Roles> GetRolPdvTPF(int idemppaisnegcue);
        List<Respuesta> PostRolesTPF(List<Roles> roles);
        List<Respuesta> PutRolesTPF(List<Roles> roles);
    }
}
