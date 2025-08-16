using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.TAWA.SEGURIDAD.Models.Permisos;
using RombiBack.Security.Model.UserAuth;
using RombiBack.Services.TAWA.SEGURIDAD.MGM_Permisos;

namespace RombiBack.Controllers.TAWA.SEGURIDAD.MGM_Permisos
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisosTawaController : ControllerBase
    {
        private readonly IPermisosTawaServices _permisosServices;
        public PermisosTawaController(IPermisosTawaServices permisosServices)
        {
            _permisosServices = permisosServices;
        }
        [HttpPost("GetCodigosTawa")]
        public async Task<IActionResult> GetCodigosTawa([FromBody] CodigosRequestTawa request)
        {
            var codigos = await _permisosServices.GetCodigos(request);
            return Ok(codigos);
        }
        [HttpPost("GetAllUsersTawa")]
        public async Task<IActionResult> GetAllUsersTawa([FromBody] AllUsersRequestTawa request)
        {
            var allusers = await _permisosServices.GetAllUsers(request);
            return Ok(allusers);
        }

        [HttpPost("GetModulosPermisosTawa")]
        public async Task<IActionResult> GetModulosPermisosTawa([FromBody] UserDTORequest request)
        {
            var allusers = await _permisosServices.GetModulosPermisos(request);
            return Ok(allusers);
        }

        [HttpGet("GetPerfilesTawa")]
        public async Task<IActionResult> GetPerfilesTawa()
        {

            var rpt = await _permisosServices.GetPerfiles();
            return Ok(rpt);
        }

        [HttpPost("ValidarEstructuraModulosTawa")]
        public async Task<IActionResult> ValidarEstructuraModulosTawa([FromBody] List<PermisosModulosRequestTawa> turnospdv)
        {
            var turnospdvres = await _permisosServices.ValidarEstructuraModulos(turnospdv);
            return Ok(turnospdvres);
        }
    }
}
