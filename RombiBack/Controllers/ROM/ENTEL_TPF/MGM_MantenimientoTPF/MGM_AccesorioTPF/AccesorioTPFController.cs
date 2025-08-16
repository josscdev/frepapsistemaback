using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Accesorios;
using RombiBack.Services.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_AccesorioTPF;

namespace RombiBack.Controllers.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_AccesorioTPF
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesorioTPFController : ControllerBase
    {
        private readonly IAccesorioTPFServices _accesorioTPFServices;

        public AccesorioTPFController(IAccesorioTPFServices accesorioTPFServices)
        {
            _accesorioTPFServices = accesorioTPFServices;
        }

        [HttpPost("GetAccesorioRomWebTPF")]
        public async Task<IActionResult> GetAccesorioRomWebTPF([FromBody] int idemppaisnegcue)
        {

            var accrespuesta = await _accesorioTPFServices.GetAccesorioRomWebTPF(idemppaisnegcue);
            return Ok(accrespuesta);
        }

        //[HttpPost("GetAccesorioRomBI")]
        //public async Task<IActionResult> GetAccesorioRomBI()
        //{

        //    var accrespuesta = await _accesorioServices.GetAccesorioRomBI();
        //    return Ok(accrespuesta);
        //}

        [HttpPost("PostAccesesorioRomWebTPF")]
        public async Task<IActionResult> PostAccesesorioRomWebTPF([FromBody] Accesorio accesorio)
        {

            var accesoriorespuesta = await _accesorioTPFServices.PostAccesesorioRomWebTPF(accesorio);
            return Ok(accesoriorespuesta);
        }


        [HttpPost("DeleteAccesesorioRomWebTPF")]
        public async Task<IActionResult> DeleteAccesesorioRomWebTPF([FromBody] Accesorio accesorio)
        {

            var accesoriorespuesta = await _accesorioTPFServices.DeleteAccesesorioRomWebTPF(accesorio);
            return Ok(accesoriorespuesta);
        }
    }
}
