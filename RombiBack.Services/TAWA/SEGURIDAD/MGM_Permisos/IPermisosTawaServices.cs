using RombiBack.Security.Model.UserAuth.Modules;
using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.TAWA.SEGURIDAD.Models.Permisos;
using RombiBack.Entities.TAWA.SEGURIDAD.Models.Perfiles;

namespace RombiBack.Services.TAWA.SEGURIDAD.MGM_Permisos
{
    public interface IPermisosTawaServices
    {
        Task<List<CodigosResponseTawa>> GetCodigos(CodigosRequestTawa request);
        Task<List<AllUsersResponseTawa>> GetAllUsers(AllUsersRequestTawa request);
        Task<List<ModuloDTOResponse>> GetModulosPermisos(UserDTORequest request);
        Task<List<PerfilesTawa>> GetPerfiles();
        Task<List<RespuestaTawa>> ValidarEstructuraModulos(List<PermisosModulosRequestTawa> requests);
    }
}
