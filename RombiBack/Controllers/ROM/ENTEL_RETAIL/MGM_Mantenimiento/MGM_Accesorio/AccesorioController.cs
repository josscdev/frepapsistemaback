using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Accesorios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Modelo;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Accesorio;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Modelo;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Accesorio
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesorioController : ControllerBase
    {
        private readonly IAccesorioServices _accesorioServices;

        public AccesorioController(IAccesorioServices accesorioServices)
        {
            _accesorioServices = accesorioServices;
        }

        [HttpPost("GetAccesorioRomWeb")]
        public async Task<IActionResult> GetAccesorioRomWeb([FromBody] int idemppaisnegcue)
        {

            var accrespuesta = await _accesorioServices.GetAccesorioRomWeb(idemppaisnegcue);
            return Ok(accrespuesta);
        }

        //[HttpPost("GetAccesorioRomBI")]
        //public async Task<IActionResult> GetAccesorioRomBI()
        //{

        //    var accrespuesta = await _accesorioServices.GetAccesorioRomBI();
        //    return Ok(accrespuesta);
        //}

        [HttpPost("PostAccesesorioRomWeb")]
        public async Task<IActionResult> PostAccesesorioRomWeb([FromBody] Accesorio accesorio)
        {

            var accesoriorespuesta = await _accesorioServices.PostAccesesorioRomWeb(accesorio);
            return Ok(accesoriorespuesta);
        }


        [HttpPost("DeleteAccesesorioRomWeb")]
        public async Task<IActionResult> DeleteAccesesorioRomWeb([FromBody] Accesorio accesorio)
        {

            var accesoriorespuesta = await _accesorioServices.DeleteAccesesorioRomWeb(accesorio);
            return Ok(accesoriorespuesta);
        }
    }
}
