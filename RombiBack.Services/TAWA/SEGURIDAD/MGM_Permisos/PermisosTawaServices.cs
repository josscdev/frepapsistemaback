using AutoMapper;
using RombiBack.Repository.TAWA.SEGURIDAD.MGM_Permisos;
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
    public class PermisosTawaServices : IPermisosTawaServices
    {
        private readonly IPermisosTawaRepository _permisosRepository;

        private readonly IMapper _mapper;

        public PermisosTawaServices(IPermisosTawaRepository permisosRepository, IMapper mapper)
        {
            _permisosRepository = permisosRepository;
            _mapper = mapper;
        }

        public async Task<List<CodigosResponseTawa>> GetCodigos(CodigosRequestTawa request)
        {
            var respuesta = await _permisosRepository.GetCodigos(request);
            return respuesta;
        }
        public async Task<List<AllUsersResponseTawa>> GetAllUsers(AllUsersRequestTawa request)
        {
            var respuesta = await _permisosRepository.GetAllUsers(request);
            return respuesta;
        }

        public async Task<List<ModuloDTOResponse>> GetModulosPermisos(UserDTORequest request)
        {
            var getPermissionsmodule = await _permisosRepository.GetModulosPermisos(request);
            return getPermissionsmodule;
        }

        public async Task<List<PerfilesTawa>> GetPerfiles()
        {
            var respuesta = await _permisosRepository.GetPerfiles();
            return respuesta;
        }



        public async Task<List<RespuestaTawa>> ValidarEstructuraModulos(List<PermisosModulosRequestTawa> requests)
        {
            var respuesta = await _permisosRepository.ValidarEstructuraModulos(requests);
            return respuesta;
        }
    }
}
