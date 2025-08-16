using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Accesorios;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Allocation;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Allocation;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Mantenimiento.MGM_Accesorio;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_Allocation
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocationController : ControllerBase
    {
        private readonly IAllocationServices _allocationServices;

        public AllocationController(IAllocationServices allocationServices)
        {
            _allocationServices = allocationServices;
        }



        [HttpGet("GetAllRolPromotor")]
        public IActionResult GetAllRolPromotor(string usuario, int idemppaisnegcue, string tipoperiodo)
        {
            try
            {
                var respuesta = _allocationServices.GetAllRolPromotor(usuario, idemppaisnegcue,tipoperiodo); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpGet("GetRolUsuarioPDV")]
        public IActionResult GetRolUsuarioPDV(string usuario, int idemppaisnegcue, string tipoperiodo)
        {
            try
            {
                var respuesta = _allocationServices.GetRolUsuarioPDV(usuario, idemppaisnegcue, tipoperiodo); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpGet("ValidarBotonRegistroVentas")]
        public IActionResult ValidarBotonRegistroVentas(string usuario, int idemppaisnegcue)
        {
            try
            {
                var respuesta = _allocationServices.ValidarBotonRegistroVentas(usuario, idemppaisnegcue ); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetRolTipoFuncionalidad")]
        public IActionResult GetRolTipoFuncionalidad(int idemppaisnegcue)
        {
            try
            {
                var respuesta = _allocationServices.GetRolTipoFuncionalidad(idemppaisnegcue); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetRolTipoTrabajo")]
        public IActionResult GetRolTipoTrabajo(int idemppaisnegcue)
        {
            try
            {
                var respuesta = _allocationServices.GetRolTipoTrabajo(idemppaisnegcue); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetRolTipoLicencia")]
        public IActionResult GetRolTipoLicencia(int idemppaisnegcue)
        {
            try
            {
                var respuesta = _allocationServices.GetRolTipoLicencia(idemppaisnegcue); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetRolTipoEstado")]
        public IActionResult GetRolTipoEstado(int idemppaisnegcue)
        {
            try
            {
                var respuesta = _allocationServices.GetRolTipoEstado(idemppaisnegcue); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetRolPromotorDocUsuario")]
        public IActionResult GetRolPromotorDocUsuario(string usuario,int idemppaisnegcue, string tipoperiodo, string usuarioperfil)
        {
            try
            {
                var respuesta = _allocationServices.GetRolPromotorDocUsuario(usuario, idemppaisnegcue,tipoperiodo, usuarioperfil); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetRolPdv")]
        public IActionResult GetRolPdv(int idemppaisnegcue)
        {
            try
            {
                var respuesta = _allocationServices.GetRolPdv(idemppaisnegcue); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("PostRoles")]
        public IActionResult PostRoles([FromBody] List<Roles> roles)
        {
            try
            {
                var respuesta = _allocationServices.PostRoles(roles); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("PutRoles")]
        public IActionResult PutRoles([FromBody] List<Roles> roles)
        {
            try
            {
                var respuesta = _allocationServices.PutRoles(roles); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("GetUsuariosEntel")]
        public IActionResult GetUsuariosEntel(int idemppaisnegcue)
        {
            try
            {
                var respuesta = _allocationServices.GetUsuariosEntel(idemppaisnegcue); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("PostUsuarioEntel")]
        public IActionResult PostUsuarioEntel([FromBody] List<UsuariosEntel> usuariosentel)
        {
            try
            {
                var respuesta = _allocationServices.PostUsuarioEntel(usuariosentel); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("PutUsuarioEntel")]
        public IActionResult PutUsuarioEntel([FromBody] UsuariosEntel usuariosentel)
        {
            try
            {
                var respuesta = _allocationServices.PutUsuarioEntel(usuariosentel); // Asume que GetOneRol ahora recibe un int
                    return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("DeleteUsuarioEntel")]
        public IActionResult DeleteUsuarioEntel(int idUsuarioEntel, string usuarioEliminacion)
        {
            try
            {
                var respuesta = _allocationServices.DeleteUsuarioEntel(idUsuarioEntel, usuarioEliminacion); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpPost("GetNombreExcel")]
        public IActionResult GetNombreExcel(int idemppaisnegcue, string vista)
        {
            try
            {
                var respuesta = _allocationServices.GetNombreExcel(idemppaisnegcue, vista); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
