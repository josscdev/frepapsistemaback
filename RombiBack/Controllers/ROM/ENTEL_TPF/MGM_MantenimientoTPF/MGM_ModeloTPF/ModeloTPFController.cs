using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Modelo;
using RombiBack.Services.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_ModeloTPF;

namespace RombiBack.Controllers.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_ModeloTPF
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeloTPFController : ControllerBase
    {
        private readonly IModeloTPFServices _modeloTPFServices;

        public ModeloTPFController(IModeloTPFServices modeloTPFServices)
        {
            _modeloTPFServices = modeloTPFServices;
        }

        //[HttpPost("GetModeloRomWebTPF")]
        //public async Task<IActionResult> GetModeloRomWebTPF([FromBody] int idemppaisnegcue)
        //{

        //    var modelorespuesta = await _modeloTPFServices.GetModeloRomWebTPF(idemppaisnegcue);
        //    return Ok(modelorespuesta);
        //}

        //[HttpPost("PostModeloRomWebTPF")]
        //public async Task<IActionResult> PostModeloRomWebTPF([FromBody] Modelo modelo)
        //{

        //    var modelorespuesta = await _modeloTPFServices.PostModeloRomWebTPF(modelo);
        //    return Ok(modelorespuesta);
        //}

        //[HttpPost("DeleteModeloRomWebTPF")]
        //public async Task<IActionResult> DeleteModeloRomWebTPF([FromBody] Modelo modelo)
        //{

        //    var modelorespuesta = await _modeloTPFServices.DeleteModeloRomWebTPF(modelo);
        //    return Ok(modelorespuesta);
        //}
    }
}
