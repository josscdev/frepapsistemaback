using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Allocation;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Allocation;
using RombiBack.Services.ROM.ENTEL_TPF.MGM_Allocation;

namespace RombiBack.Controllers.ROM.ENTEL_TPF.MGM_AllocationTPF
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocationTPFController : ControllerBase
    {
        private readonly IAllocationTPFServices _allocationTPFServices;

        public AllocationTPFController(IAllocationTPFServices allocationTPFServices)
        {
            _allocationTPFServices = allocationTPFServices;
        }



        //[HttpGet("GetAllRolPromotorTPF")]
        //public IActionResult GetAllRolPromotorTPF(string usuario, int idemppaisnegcue, string tipoperiodo)
        //{
        //    try
        //    {
        //        var respuesta = _allocationTPFServices.GetAllRolPromotorTPF(usuario, idemppaisnegcue, tipoperiodo); // Asume que GetOneRol ahora recibe un int
        //        return Ok(respuesta);  // Retorna un 200 OK con la respuesta
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones, devuelve un error 500 con el mensaje de error
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

        //[HttpGet("GetRolUsuarioPDVTPF")]
        //public IActionResult GetRolUsuarioPDVTPF(string usuario, int idemppaisnegcue, string tipoperiodo)
        //{
        //    try
        //    {
        //        var respuesta = _allocationTPFServices.GetRolUsuarioPDVTPF(usuario, idemppaisnegcue, tipoperiodo); // Asume que GetOneRol ahora recibe un int
        //        return Ok(respuesta);  // Retorna un 200 OK con la respuesta
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones, devuelve un error 500 con el mensaje de error
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}


        //[HttpGet("ValidarBotonRegistroVentasTPF")]
        //public IActionResult ValidarBotonRegistroVentasTPF(string usuario, int idemppaisnegcue)
        //{
        //    try
        //    {
        //        var respuesta = _allocationTPFServices.ValidarBotonRegistroVentasTPF(usuario, idemppaisnegcue); // Asume que GetOneRol ahora recibe un int
        //        return Ok(respuesta);  // Retorna un 200 OK con la respuesta
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones, devuelve un error 500 con el mensaje de error
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}


        //[HttpGet("GetRolTipoFuncionalidadTPF")]
        //public IActionResult GetRolTipoFuncionalidadTPF(int idemppaisnegcue)
        //{
        //    try
        //    {
        //        var respuesta = _allocationTPFServices.GetRolTipoFuncionalidadTPF(idemppaisnegcue); // Asume que GetOneRol ahora recibe un int
        //        return Ok(respuesta);  // Retorna un 200 OK con la respuesta
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones, devuelve un error 500 con el mensaje de error
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

        //[HttpGet("GetRolTipoTrabajoTPF")]
        //public IActionResult GetRolTipoTrabajo(int idemppaisnegcue)
        //{
        //    try
        //    {
        //        var respuesta = _allocationTPFServices.GetRolTipoTrabajoTPF(idemppaisnegcue); // Asume que GetOneRol ahora recibe un int
        //        return Ok(respuesta);  // Retorna un 200 OK con la respuesta
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones, devuelve un error 500 con el mensaje de error
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

        //[HttpGet("GetRolTipoLicenciaTPF")]
        //public IActionResult GetRolTipoLicencia(int idemppaisnegcue)
        //{
        //    try
        //    {
        //        var respuesta = _allocationTPFServices.GetRolTipoLicenciaTPF(idemppaisnegcue); // Asume que GetOneRol ahora recibe un int
        //        return Ok(respuesta);  // Retorna un 200 OK con la respuesta
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones, devuelve un error 500 con el mensaje de error
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

        //[HttpGet("GetRolTipoEstadoTPF")]
        //public IActionResult GetRolTipoEstado(int idemppaisnegcue)
        //{
        //    try
        //    {
        //        var respuesta = _allocationTPFServices.GetRolTipoEstadoTPF(idemppaisnegcue); // Asume que GetOneRol ahora recibe un int
        //        return Ok(respuesta);  // Retorna un 200 OK con la respuesta
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones, devuelve un error 500 con el mensaje de error
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

        //[HttpGet("GetRolPromotorDocUsuarioTPF")]
        //public IActionResult GetRolPromotorDocUsuario(string usuario, int idemppaisnegcue, string tipoperiodo, string usuarioperfil)
        //{
        //    try
        //    {
        //        var respuesta = _allocationTPFServices.GetRolPromotorDocUsuarioTPF(usuario, idemppaisnegcue, tipoperiodo, usuarioperfil); // Asume que GetOneRol ahora recibe un int
        //        return Ok(respuesta);  // Retorna un 200 OK con la respuesta
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones, devuelve un error 500 con el mensaje de error
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

        //[HttpGet("GetRolPdvTPF")]
        //public IActionResult GetRolPdv(int idemppaisnegcue)
        //{
        //    try
        //    {
        //        var respuesta = _allocationTPFServices.GetRolPdvTPF(idemppaisnegcue); // Asume que GetOneRol ahora recibe un int
        //        return Ok(respuesta);  // Retorna un 200 OK con la respuesta
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones, devuelve un error 500 con el mensaje de error
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

        //[HttpPost("PostRolesTPF")]
        //public IActionResult PostRoles([FromBody] List<Roles> roles)
        //{
        //    try
        //    {
        //        var respuesta = _allocationTPFServices.PostRolesTPF(roles); // Asume que GetOneRol ahora recibe un int
        //        return Ok(respuesta);  // Retorna un 200 OK con la respuesta
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones, devuelve un error 500 con el mensaje de error
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

        //[HttpPost("PutRolesTPF")]
        //public IActionResult PutRoles([FromBody] List<Roles> roles)
        //{
        //    try
        //    {
        //        var respuesta = _allocationTPFServices.PutRolesTPF(roles); // Asume que GetOneRol ahora recibe un int
        //        return Ok(respuesta);  // Retorna un 200 OK con la respuesta
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones, devuelve un error 500 con el mensaje de error
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

    }
}

