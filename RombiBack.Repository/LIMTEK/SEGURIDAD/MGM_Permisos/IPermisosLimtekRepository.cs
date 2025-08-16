using RombiBack.Security.Model.UserAuth.Modules;
using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.LIMTEK.SEGURIDAD.Models.Permisos;
using RombiBack.Entities.LIMTEK.SEGURIDAD.Models.Perfiles;

namespace RombiBack.Repository.LIMTEK.SEGURIDAD.MGM_Permisos
{
    public interface IPermisosLimtekRepository
    {
        Task<List<CodigosResponseLimtek>> GetCodigos(CodigosRequestLimtek request);
        Task<List<AllUsersResponseLimtek>> GetAllUsers(AllUsersRequestLimtek request);

        Task<List<ModuloDTOResponse>> GetModulosPermisos(UserDTORequest request);

        Task<List<PerfilesLimtek>> GetPerfiles();

        Task<List<RespuestaLimtek>> ValidarEstructuraModulos(List<PermisosModulosRequestLimtek> requests);

    }
}
