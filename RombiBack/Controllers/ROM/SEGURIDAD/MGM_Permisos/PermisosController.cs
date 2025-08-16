using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Permisos;
using RombiBack.Security.Model.UserAuth;
using RombiBack.Services.ROM.SEGURIDAD.MGM_Permisos;

namespace RombiBack.Controllers.ROM.SEGURIDAD.MGM_Permisos
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisosController : ControllerBase
    {
        private readonly IPermisosServices _permisosServices;

        public PermisosController(IPermisosServices permisosServices)
        {
            _permisosServices = permisosServices;
        }
        [HttpPost("GetCodigos")]
        public async Task<IActionResult> GetCodigos([FromBody] CodigosRequest request)
        {
            var codigos = await _permisosServices.GetCodigos(request);
            return Ok(codigos);
        }
        [HttpPost("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromBody] AllUsersRequest request)
        {
            var allusers = await _permisosServices.GetAllUsers(request);
            return Ok(allusers);
        }

        [HttpPost("GetModulosPermisos")]
        public async Task<IActionResult> GetModulosPermisos([FromBody] UserDTORequest request)
        {
            var allusers = await _permisosServices.GetModulosPermisos(request);
            return Ok(allusers);
        }

        [HttpGet("GetPerfiles")]
        public async Task<IActionResult> GetPerfiles()
        {

            var rpt = await _permisosServices.GetPerfiles();
            return Ok(rpt);
        }

        [HttpPost("ValidarEstructuraModulos")]
        public async Task<IActionResult> ValidarEstructuraModulos([FromBody] List<PermisosModulosRequest> turnospdv)
        {
            var turnospdvres = await _permisosServices.ValidarEstructuraModulos(turnospdv);
            return Ok(turnospdvres);
        }

        [HttpPost("InsertarOActualizarUsuario")]
        public async Task<IActionResult> InsertarOActualizarUsuario([FromBody] InsertarUsuario request)
        {
            var respuesta = await _permisosServices.InsertarOActualizarUsuario(request);
            return Ok(respuesta);
        }

        [HttpGet("GetUsuarioPorDocumento")]
        public async Task<IActionResult> GetUsuarioPorDocumento([FromQuery] string docusuario, [FromQuery] string nombresocio)
        {
            var result = await _permisosServices.GetUsuarioPorDocumento(docusuario, nombresocio);

            if (result == null)
            {
                return Ok(new RespuestaUsuario
                {
                    Estado = "ERROR",
                    Mensaje = "No se encontró el usuario con los datos proporcionados."
                });
            }

            return Ok(result);
        }

        [HttpPost("ValidarUsuariosAsync")]
        public async Task<IActionResult> ValidarUsuariosAsync([FromBody] List<ListaValidarUsuario> usuarios)
        {
            if (usuarios == null || !usuarios.Any())
                return BadRequest("Debe enviar al menos un usuario para validar.");

            var resultado = await _permisosServices.ValidarUsuariosAsync(usuarios);
            return Ok(resultado);
        }

        [HttpPost("InsertarUsuariosMasivo")]
        public async Task<IActionResult> InsertarUsuariosMasivoAsync([FromBody] List<ListaValidarUsuario> usuarios)
        {
            if (usuarios == null || !usuarios.Any())
                return BadRequest("Debe enviar al menos un usuario.");

            var resultado = await _permisosServices.InsertarUsuariosMasivoAsync(usuarios);
            return Ok(resultado);
        }
    }
}
