using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RombiBack.Security.Auth.Services;
using RombiBack.Security.Helpers;
using RombiBack.Security.JWT;
using RombiBack.Security.Model.UserAuth;
using RombiBack.Services.ROM.LOGIN.Company;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Permisos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RombiBack.Controllers.AuthLogin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthLoginController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        public IConfiguration _configuration;
        private readonly IGenerateToken _generateToken;
        public AuthLoginController(IConfiguration configuracion, IAuthServices authServices, IGenerateToken generateToken)
        {
            _authServices = authServices;
            _configuration = configuracion;
            _generateToken = generateToken;
        }


        [HttpPost("LoginMain")]
        public async Task<IActionResult> LoginMain([FromBody] UserDTORequest request)
        {

            var login = await _authServices.RombiLoginMain(request);

            if (login.Resultado == "ACCESO CONCEDIDO" && login.Accede == 1)
            {

                string token = _generateToken.GenerateToken(request.idempresa ?? 0, request.idpais ?? 0, request.user);

                // Generar token
                return Ok(new
                {
                    login.Resultado,
                    login.Accede,
                    login.Perfil,
                    login.idusuario,
                    login.idusuarioromweb,
                    message = "Usuario válido",
                    token
                });
            }
            else
            {
                string token = "";
                return Ok(new
                {
                    login.Resultado,
                    login.Accede,
                    login.Perfil,
                    login.idusuario,
                    login.idusuarioromweb,
                    message = "Usuario inválido",
                    token
                });
            }
            //return Ok(login);
        }

        [Authorize]
        [HttpPost("GetUserData")]
        public async Task<IActionResult> GetUserData([FromBody] UserDTORequest request)
        {
            var getUserData = await _authServices.GetUserData(request);
            return Ok(getUserData);
        }


        [Authorize]
        [HttpPost("GetBusinessUser")]
        public async Task<IActionResult> GetBusinessUser([FromBody] UserDTORequest request)
        {

            var getBusinessUser = await _authServices.GetBusinessUser(request);

            return Ok(getBusinessUser);
        }

        [Authorize]
        [HttpPost("GetBusinessAccountUser")]
        public async Task<IActionResult> GetBusinessAccountUser([FromBody] UserDTORequest request)
        {

            var getBusinessAccountUser = await _authServices.GetBusinessAccountUser(request);

            return Ok(getBusinessAccountUser);
        }

        [Authorize]
        [HttpPost("GetPermissions")]
        public async Task<IActionResult> GetPermissions([FromBody] UserDTORequest request)
        {

            var getPermissions = await _authServices.GetPermissions(request);

            return Ok(getPermissions);
        }

        [Authorize]
        [HttpPost("GetIdCodigo")]
        public async Task<IActionResult> GetIdCodigo([FromBody] CodigosRequest request)
        {

            var getCodigo = await _authServices.GetIdCodigo(request);

            return Ok(getCodigo);
        }

        //[Authorize]
        [HttpPost("GetIdpdv")]
        public async Task<IActionResult> GetIdpdv([FromBody] CodigosRequest request)
        {

            var getCodigo = await _authServices.GetIdpdv(request);

            return Ok(getCodigo);
        }

        [HttpPost("GetMarcacion")]
        public JsonResult GetMarcacion([FromBody] string usuario )
        {
            List<RETAIL_AsistenciaBE> results = new List<RETAIL_AsistenciaBE>();


            results = _authServices.GetMarcacionPromotor( usuario);

            return new JsonResult(new { lstData = results }); 
        }


        [HttpPost("GetRutaPrincipal")]
        public async Task<IActionResult> GetRutaPrincipal([FromBody] GetRutaRequest request)
        {
            if (request == null || request.idemppaisnegcue <= 0)
            {
                return BadRequest(new { message = "El parámetro idemppaisnegcue es inválido." });
            }

            string ruta = await _authServices.GetRutaPrincipal(request.idemppaisnegcue);

            if (string.IsNullOrEmpty(ruta))
            {
                return NotFound(new { message = "No se encontró una ruta para el parámetro proporcionado." });
            }

            return Ok(new { ruta }); // Devuelve un JSON válido { "ruta": "main" }
        }

        public class GetRutaRequest
        {
            public int idemppaisnegcue { get; set; }
        }



    }
}
