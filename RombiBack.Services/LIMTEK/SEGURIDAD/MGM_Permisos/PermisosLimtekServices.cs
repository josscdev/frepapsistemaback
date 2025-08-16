using AutoMapper;
using RombiBack.Entities.LIMTEK.SEGURIDAD.Models.Perfiles;
using RombiBack.Entities.LIMTEK.SEGURIDAD.Models.Permisos;
using RombiBack.Repository.LIMTEK.SEGURIDAD.MGM_Permisos;
using RombiBack.Security.Model.UserAuth.Modules;
using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.LIMTEK.SEGURIDAD.MGM_Permisos
{
    public class PermisosLimtekServices : IPermisosLimtekServices
    {
        private readonly IPermisosLimtekRepository _permisosRepository;

        private readonly IMapper _mapper;

        public PermisosLimtekServices(IPermisosLimtekRepository permisosRepository, IMapper mapper)
        {
            _permisosRepository = permisosRepository;
            _mapper = mapper;
        }

        public async Task<List<CodigosResponseLimtek>> GetCodigos(CodigosRequestLimtek request)
        {
            var respuesta = await _permisosRepository.GetCodigos(request);
            return respuesta;
        }
        public async Task<List<AllUsersResponseLimtek>> GetAllUsers(AllUsersRequestLimtek request)
        {
            var respuesta = await _permisosRepository.GetAllUsers(request);
            return respuesta;
        }

        public async Task<List<ModuloDTOResponse>> GetModulosPermisos(UserDTORequest request)
        {
            var getPermissionsmodule = await _permisosRepository.GetModulosPermisos(request);
            return getPermissionsmodule;
        }

        public async Task<List<PerfilesLimtek>> GetPerfiles()
        {
            var respuesta = await _permisosRepository.GetPerfiles();
            return respuesta;
        }



        public async Task<List<RespuestaLimtek>> ValidarEstructuraModulos(List<PermisosModulosRequestLimtek> requests)
        {
            var respuesta = await _permisosRepository.ValidarEstructuraModulos(requests);
            return respuesta;
        }
    }
}
