using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Accesorios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Modelo;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Modelo;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Modelo
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeloController : ControllerBase
    {
        private readonly IModeloServices _modeloServices;

        public ModeloController(IModeloServices modeloServices)
        {
            _modeloServices = modeloServices;
        }

        //[HttpPost("GetModeloRomWeb")]
        //public async Task<IActionResult> GetModeloRomWeb([FromBody] int idemppaisnegcue)
        //{

        //    var modelorespuesta = await _modeloServices.GetModeloRomWeb(idemppaisnegcue);
        //    return Ok(modelorespuesta);
        //}

        //[HttpPost("PostModeloRomWeb")]
        //public async Task<IActionResult> PostModeloRomWeb([FromBody] Modelo modelo)
        //{

        //    var modelorespuesta = await _modeloServices.PostModeloRomWeb(modelo);
        //    return Ok(modelorespuesta);
        //}

        //[HttpPost("DeleteModeloRomWeb")]
        //public async Task<IActionResult> DeleteModeloRomWeb([FromBody] Modelo modelo)
        //{

        //    var modelorespuesta = await _modeloServices.DeleteModeloRomWeb(modelo);
        //    return Ok(modelorespuesta);
        //}
    }
}
