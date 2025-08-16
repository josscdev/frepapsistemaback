using AutoMapper;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Permisos;
using RombiBack.Repository.ROM.SEGURIDAD.MGM_Permisos;
using RombiBack.Security.Model.UserAuth.Modules;
using RombiBack.Security.Model.UserAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Perfiles;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;

namespace RombiBack.Services.ROM.SEGURIDAD.MGM_Permisos
{
    public class PermisosServices : IPermisosServices
    {
        private readonly IPermisosRepository _permisosRepository;

        private readonly IMapper _mapper;

        public PermisosServices(IPermisosRepository permisosRepository, IMapper mapper)
        {
            _permisosRepository = permisosRepository;
            _mapper = mapper;
        }
        public async Task<List<CodigosResponse>> GetCodigos(CodigosRequest request)
        {
            var respuesta = await _permisosRepository.GetCodigos(request);
            return respuesta;
        }
        public async Task<List<AllUsersResponse>> GetAllUsers(AllUsersRequest request)
        {
            var respuesta = await _permisosRepository.GetAllUsers(request);
            return respuesta;
        }

        public async Task<List<ModuloDTOResponse>> GetModulosPermisos(UserDTORequest request)
        {
            var getPermissionsmodule = await _permisosRepository.GetModulosPermisos(request);
            return getPermissionsmodule;
        }

        public async Task<List<Perfiles>> GetPerfiles()
        {
            var respuesta = await _permisosRepository.GetPerfiles();
            return respuesta;
        }



        public async Task<List<Respuesta>> ValidarEstructuraModulos(List<PermisosModulosRequest> requests)
        {
            var respuesta = await _permisosRepository.ValidarEstructuraModulos(requests);
            return respuesta;
        }

        public async Task<RespuestaUsuario> InsertarOActualizarUsuario(InsertarUsuario request)
        {
            var respuesta = await _permisosRepository.InsertarOActualizarUsuario(request);
            return respuesta;
        }

        public async Task<object> GetUsuarioPorDocumento(string docusuario, string nombresocio)
        {
            var respuesta = await _permisosRepository.GetUsuarioPorDocumento(docusuario, nombresocio);
            return respuesta;
        }

        public async Task<List<RespuestaValidarUsuario>> ValidarUsuariosAsync(List<ListaValidarUsuario> usuarios)
        {
            var respuesta = await _permisosRepository.ValidarUsuariosAsync(usuarios);
            return respuesta;
        }

        public async Task<List<RespuestaValidarUsuario>> InsertarUsuariosMasivoAsync(List<ListaValidarUsuario> usuarios)
        {
            var respuesta = await _permisosRepository.InsertarUsuariosMasivoAsync(usuarios);
            return respuesta;
        }
    }
}
