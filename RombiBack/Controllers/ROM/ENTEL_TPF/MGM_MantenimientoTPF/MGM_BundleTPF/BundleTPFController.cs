using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Bundles;
using RombiBack.Services.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_BundleTPF;

namespace RombiBack.Controllers.ROM.ENTEL_TPF.MGM_MantenimientoTPF.MGM_BundleTPF
{
    [Route("api/[controller]")]
    [ApiController]
    public class BundleTPFController : ControllerBase
    {
        private readonly IBundleTPFServices _bundleTPFServices;

        public BundleTPFController(IBundleTPFServices bundleTPFServices)
        {
            _bundleTPFServices = bundleTPFServices;
        }

        [HttpGet("GetBundlesTPF")]
        public async Task<IActionResult> GetBundlesTPF(int idemppaisnegcue)
        {

            var accrespuesta = await _bundleTPFServices.GetBundlesTPF(idemppaisnegcue);
            return Ok(accrespuesta);
        }

        [HttpPost("PutBundleTPF")]
        public async Task<IActionResult> PutBundleTPF([FromBody] ActualizarBundle actualizarbundle)
        {
            var accrespuesta = await _bundleTPFServices.UpdateBundleTPF(actualizarbundle);
            return Ok(accrespuesta);
        }
    }
}
