using RombiBack.Entities.LIMTEK.SEGURIDAD.Models.Perfiles;
using RombiBack.Entities.LIMTEK.SEGURIDAD.Models.Permisos;
using RombiBack.Security.Model.UserAuth.Modules;
using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.LIMTEK.SEGURIDAD.MGM_Permisos
{
    public interface IPermisosLimtekServices
    {
        Task<List<CodigosResponseLimtek>> GetCodigos(CodigosRequestLimtek request);
        Task<List<AllUsersResponseLimtek>> GetAllUsers(AllUsersRequestLimtek request);
        Task<List<ModuloDTOResponse>> GetModulosPermisos(UserDTORequest request);
        Task<List<PerfilesLimtek>> GetPerfiles();
        Task<List<RespuestaLimtek>> ValidarEstructuraModulos(List<PermisosModulosRequestLimtek> requests);
    }
}
