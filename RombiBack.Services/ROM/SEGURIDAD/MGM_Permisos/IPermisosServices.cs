using RombiBack.Entities.ROM.SEGURIDAD.Models.Permisos;
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
    public interface IPermisosServices
    {
        Task<List<CodigosResponse>> GetCodigos(CodigosRequest request);
        Task<List<AllUsersResponse>> GetAllUsers(AllUsersRequest request);
        Task<List<ModuloDTOResponse>> GetModulosPermisos(UserDTORequest request);
        Task<List<Perfiles>> GetPerfiles();
        Task<List<Respuesta>> ValidarEstructuraModulos(List<PermisosModulosRequest> requests);
        Task<RespuestaUsuario> InsertarOActualizarUsuario(InsertarUsuario request);
        Task<object> GetUsuarioPorDocumento(string docusuario, string nombresocio);
        Task<List<RespuestaValidarUsuario>> ValidarUsuariosAsync(List<ListaValidarUsuario> usuarios);
        Task<List<RespuestaValidarUsuario>> InsertarUsuariosMasivoAsync(List<ListaValidarUsuario> usuarios);
    }
}
