using RombiBack.Security.Helpers;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Incentivos.Dto;
using RombiBack.Services.ROM.ENTEL_RETAIL.Intranet_Incentivos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiRestNetCore.DTO.DtoIncentivo;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.Intranet_Incentivos
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncentivosController : ControllerBase
    {
        private readonly IIncentivosServices _incentivosService;
        public IConfiguration _configuration;
        private readonly JwtHelper _jwtHelper;
        private readonly TokenValidationHelper _tokenValidationHelper;
        public IncentivosController(IConfiguration configuracion , IIncentivosServices incentivosServices)
        {
            _incentivosService = incentivosServices;
            _configuration = configuracion;
            _jwtHelper = new JwtHelper(configuracion);
            _tokenValidationHelper = new TokenValidationHelper(configuracion);
        }

        [HttpPost("validateUser")]
        public IActionResult ValidateUser([FromBody] UserDTO request)
        {
            string usuario = request.USUARIO;
            string clave = request.CLAVE;

            UserDTO usuarioValidado = _incentivosService.ValidateUser(usuario, clave);

            if (usuarioValidado != null && !string.IsNullOrEmpty(usuarioValidado.USUARIO))
            {
                string token = _jwtHelper.GenerateToken(usuarioValidado.USUARIO);

                return Ok(new
                {
                    success = true,
                    message = "Usuario válido",
                    token
                });
            }
            else
            {
                return BadRequest("Usuario no encontrado o datos inválidos.");
            }
        }



        [HttpPost("GeneralWithDNIConfirmationFalse")]
        public ActionResult<IEnumerable<IncentivoVistaDTO>> GetGeneralWithDNIConfirmationFalse([FromBody] IncentivoPagoRequestDTO request)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
            {
                return BadRequest("Token inválido");
            }

            token = token.Substring("Bearer ".Length);

            bool isValidToken = _tokenValidationHelper.ValidateToken(token);

            if (!isValidToken)
            {
                return BadRequest("Token inválido");
            }

            var dniClaim = _jwtHelper.GetClaimValue(token, "dni");

            if (string.IsNullOrEmpty(dniClaim))
            {
                return BadRequest("No se encontró el claim 'dni' en el token.");
            }

            try
            {
                var incentivosVistas = _incentivosService.GetGeneralIncentivosVistasWithDNIConfirmationFalse(dniClaim);
                return Ok(incentivosVistas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }


        [HttpPost("GetIncentivosPremios")]
        public ActionResult<IEnumerable<IncentivoVistaDTO>> GetIncentivosPremios([FromBody] IncentivoPagoRequestDTO request)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
            {
                return BadRequest("Token inválido");
            }

            token = token.Substring("Bearer ".Length);

            bool isValidToken = _tokenValidationHelper.ValidateToken(token);

            if (!isValidToken)
            {
                return BadRequest("Token inválido");
            }

            var dniClaim = _jwtHelper.GetClaimValue(token, "dni");

            if (string.IsNullOrEmpty(dniClaim))
            {
                return BadRequest("No se encontró el claim 'dni' en el token.");
            }

            try
            {
                var incentivosVistas = _incentivosService.GetIncentivosPremios(dniClaim);
                return Ok(incentivosVistas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }


        [HttpPost("UpdateWithDNI")]
        public IActionResult UpdateConfirmacionEntrega([FromBody] IncentivoPagoRequestDTO request)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
            {
                return BadRequest("Token inválido");
            }

            token = token.Substring("Bearer ".Length);

            bool isValidToken = _tokenValidationHelper.ValidateToken(token);

            if (!isValidToken)
            {
                return BadRequest("Token inválido");
            }

            var dniClaim = _jwtHelper.GetClaimValue(token, "dni");

            if (string.IsNullOrEmpty(dniClaim))
            {
                return BadRequest("No se encontró el claim 'dni' en el token.");
            }

            try
            {
                int id = (int)request.Id;
                _incentivosService.UpdateConfirmacionEntrega(dniClaim, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }


    }


}
