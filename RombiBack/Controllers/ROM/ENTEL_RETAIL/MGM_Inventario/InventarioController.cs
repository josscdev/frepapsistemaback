using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Inventario;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Inventario;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_Inventario
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioServices _inventarioServices;

        public InventarioController(IInventarioServices inventarioServices)
        {
            _inventarioServices = inventarioServices;
        }

        //[Authorize]
        //[HttpPost("GetInventario")]
        //public async Task<IActionResult> GetInventario([FromBody] Inventario.InventarioFiltroListar request)
        //{
        //    try
        //    {
        //        var result = await _inventarioServices.GetInventario(request);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al obtener el inventario: {ex.Message}");
        //    }
        //}


        //[Authorize]
        //[HttpPost("PostInventario")]
        //public async Task<IActionResult> PostInventario([FromBody] Inventario.InsertarInventario inventario)
        //{
        //    try
        //    {
        //        if (inventario == null)
        //            return BadRequest(new { message = "No se recibieron datos." });
        //        var result = await _inventarioServices.PostInventarioMasivo(inventario);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al registrar inventario: {ex.Message}");
        //    }

        //}

        //[Authorize]
        //[HttpPost("DeleteInventario")]
        //public async Task<IActionResult> DeleteInventario(int idinventario, string usuario)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(usuario))
        //            return BadRequest(new { message = "No se recibieron datos." });

        //        var result = await _inventarioServices.DeleteInventario(idinventario, usuario);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al eliminar el inventario: {ex.Message}");
        //    }
        //}

        //[Authorize]
        //[HttpGet("GetInventarioDetalle")]
        //public async Task<IActionResult> GetInventarioDetalle(int idinventario)
        //{
        //    try
        //    {
        //        var result = await _inventarioServices.GetInventarioDetalle(idinventario);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al obtener el inventario: {ex.Message}");
        //    }
        //}

        //[Authorize]
        //[HttpPost("DeleteInventarioDetalle")]
        //public async Task<IActionResult> DeleteInventarioDetalle(int idinventariodetalle, string usuario)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(usuario))
        //            return BadRequest(new { message = "No se recibieron datos." });

        //        var result = await _inventarioServices.DeleteInventarioDetalle(idinventariodetalle, usuario);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al eliminar el inventario: {ex.Message}");
        //    }
        //}

        //[Authorize]
        //[HttpPost("PostInventarioDetalle")]
        //public async Task<IActionResult> PostInventarioDetalle([FromBody] Inventario.InsertarSoloInventarioDetalle inventario)
        //{
        //    try
        //    {
        //        if (inventario == null)
        //            return BadRequest(new { message = "No se recibieron datos." });
        //        var result = await _inventarioServices.PostInventarioDetalle(inventario);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al registrar inventario: {ex.Message}");
        //    }
        //}
    }
}

