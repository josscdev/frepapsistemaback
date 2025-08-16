using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.LIMTEK.SEGURIDAD.Models.Permisos;
using RombiBack.Security.Model.UserAuth;
using RombiBack.Services.LIMTEK.SEGURIDAD.MGM_Permisos;

namespace RombiBack.Controllers.LIMTEK.SEGURIDAD.MGM_Permisos
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisosLimtekController : ControllerBase
    {
        private readonly IPermisosLimtekServices _permisosServices;
        public PermisosLimtekController(IPermisosLimtekServices permisosServices)
        {
            _permisosServices = permisosServices;
        }
        //[HttpPost("GetCodigosLimtek")]
        //public async Task<IActionResult> GetCodigosLimtek([FromBody] CodigosRequestLimtek request)
        //{
        //    var codigos = await _permisosServices.GetCodigos(request);
        //    return Ok(codigos);
        //}
        //[HttpPost("GetAllUsersLimtek")]
        //public async Task<IActionResult> GetAllUsersLimtek([FromBody] AllUsersRequestLimtek request)
        //{
        //    var allusers = await _permisosServices.GetAllUsers(request);
        //    return Ok(allusers);
        //}

        //[HttpPost("GetModulosPermisosLimtek")]
        //public async Task<IActionResult> GetModulosPermisosLimtek([FromBody] UserDTORequest request)
        //{
        //    var allusers = await _permisosServices.GetModulosPermisos(request);
        //    return Ok(allusers);
        //}

        //[HttpGet("GetPerfilesLimtek")]
        //public async Task<IActionResult> GetPerfilesLimtek()
        //{

        //    var rpt = await _permisosServices.GetPerfiles();
        //    return Ok(rpt);
        //}

        //[HttpPost("ValidarEstructuraModulosLimtek")]
        //public async Task<IActionResult> ValidarEstructuraModulosLimtek([FromBody] List<PermisosModulosRequestLimtek> turnospdv)
        //{
        //    var turnospdvres = await _permisosServices.ValidarEstructuraModulos(turnospdv);
        //    return Ok(turnospdvres);
        //}
    }
}
