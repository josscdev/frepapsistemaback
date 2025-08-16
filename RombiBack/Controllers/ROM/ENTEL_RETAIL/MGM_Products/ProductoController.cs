using RombiBack.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Products;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto.Dto;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServices _productoService;
        public ProductoController(IProductoServices productoService)
        {
            _productoService = productoService;
        }

        // Define un endpoint para obtener productos
        //[HttpGet]
        //public ActionResult GetProductos()
        //{
        //    try
        //    {
        //        // Llama al método en tu servicio para obtener la lista de productos
        //        var productos = _productoService.GetProducto();
        //        return Ok(productos);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de errores, puedes personalizar esto según tus necesidades
        //        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> ObtenerTodos()
        //{
        //    var productos = await _productoService.ObtenerTodo();
        //    return Ok(productos);
        //}


       

    }
}
