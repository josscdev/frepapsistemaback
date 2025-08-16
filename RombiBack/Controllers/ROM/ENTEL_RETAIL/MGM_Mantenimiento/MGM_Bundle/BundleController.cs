using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Bundles;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Bundle;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Bundle
{
    [Route("api/[controller]")]
    [ApiController]
    public class BundleController : ControllerBase
    {
        private readonly IBundleServices _bundleServices;

        public BundleController(IBundleServices bundleServices)
        {
            _bundleServices = bundleServices;
        }


        //[HttpGet("GetBundles")]
        //public async Task<IActionResult> GetBundles(int idemppaisnegcue)
        //{

        //    var accrespuesta = await _bundleServices.GetBundles(idemppaisnegcue);
        //    return Ok(accrespuesta);
        //}

        //[HttpPost("PutBundle")]
        //public async Task<IActionResult> PutBundle([FromBody] ActualizarBundle actualizarbundle)
        //{
        //    var accrespuesta = await _bundleServices.UpdateBundle(actualizarbundle);
        //    return Ok(accrespuesta);
        //}
    }
}
