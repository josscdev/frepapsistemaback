using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.FREPAPMODULE.MODULOREGISTROS;
using RombiBack.Services.ROM.FREPAPMODULE.MODULOREGISTROS;

namespace RombiBack.Controllers.ROM.FREPAPMODULE.GESTIONAFILIACIONES.MODULOREGISTROS
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfiliacionesController : ControllerBase
    {
        private readonly IAfiliacionesServices _afiliacionesServices;
        public AfiliacionesController(IAfiliacionesServices afiliacionesServices)
        {
            _afiliacionesServices = afiliacionesServices;
        }

        [HttpPost("GetListarAfiliaciones")]
        public async Task<ActionResult<IEnumerable<ListarAfiliacion>>> ListarAfiliaciones([FromBody] FiltroAfiliacion filtro)
        {
            var result = await _afiliacionesServices.ListarAfiliaciones(filtro);
            return Ok(result);
        }

        [HttpPost("GetListarUbigeos")]
        public async Task<ActionResult<IEnumerable<ListarOpcionUbigeo>>> Listar([FromBody] FiltroUbigeo f)
        => Ok(await _afiliacionesServices.ListarUbigeos(f.idemppaisnegcue, f.pais));
    }
}
