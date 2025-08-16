using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Inventario;
using RombiBack.Services.ROM.ENTEL_TPF.MGM_InventarioTPF;

namespace RombiBack.Controllers.ROM.ENTEL_TPF.MGM_InventarioTPF
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioTPFController : ControllerBase
    {
        private readonly IInventarioTPFServices _inventarioServicesTPF;

        public InventarioTPFController(IInventarioTPFServices inventarioServicesTPF)
        {
            _inventarioServicesTPF = inventarioServicesTPF;
        }

        //[Authorize]
        //[HttpPost("GetInventarioTPF")]
        //public async Task<IActionResult> GetInventarioTPF([FromBody] Inventario.InventarioFiltroListar request)
        //{
        //    try
        //    {
        //        var result = await _inventarioServicesTPF.GetInventarioTPF(request);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al obtener el inventario: {ex.Message}");
        //    }
        //}

        //[Authorize]
        //[HttpPost("PostInventarioTPF")]
        //public async Task<IActionResult> PostInventarioTPF([FromBody] Inventario.InsertarInventario inventario)
        //{
        //    try
        //    {
        //        if (inventario == null)
        //            return BadRequest(new { message = "No se recibieron datos." });
        //        var result = await _inventarioServicesTPF.PostInventarioMasivoTPF(inventario);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al registrar inventario: {ex.Message}");
        //    }
        //}

        //[Authorize]
        //[HttpPost("DeleteInventarioTPF")]
        //public async Task<IActionResult> DeleteInventarioTPF(int idinventario, string usuario)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(usuario))
        //            return BadRequest(new { message = "No se recibieron datos." });
        //        var result = await _inventarioServicesTPF.DeleteInventarioTPF(idinventario, usuario);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al eliminar el inventario: {ex.Message}");
        //    }
        //}

        //[Authorize]
        //[HttpGet("GetInventarioDetalleTPF")]
        //public async Task<IActionResult> GetInventarioDetalleTPF(int idinventario)
        //{
        //    try
        //    {
        //        var result = await _inventarioServicesTPF.GetInventarioDetalleTPF(idinventario);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al obtener el detalle del inventario: {ex.Message}");
        //    }
        //}

        //[Authorize]
        //[HttpPost("DeleteInventarioDetalleTPF")]
        //public async Task<IActionResult> DeleteInventarioDetalleTPF(int idinventariodetalle, string usuario)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(usuario))
        //            return BadRequest(new { message = "No se recibieron datos." });
        //        var result = await _inventarioServicesTPF.DeleteInventarioDetalleTPF(idinventariodetalle, usuario);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al eliminar el detalle del inventario: {ex.Message}");
        //    }
        //}

        //[Authorize]
        //[HttpPost("PostInventarioDetalleTPF")]
        //public async Task<IActionResult> PostInventarioDetalleTPF([FromBody] Inventario.InsertarSoloInventarioDetalle inventario)
        //{
        //    try
        //    {
        //        if (inventario == null)
        //            return BadRequest(new { message = "No se recibieron datos." });
        //        var result = await _inventarioServicesTPF.PostInventarioDetalleTPF(inventario);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al registrar el detalle del inventario: {ex.Message}");
        //    }
        //}
    }
}
