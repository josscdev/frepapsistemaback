using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.FREPAPMODULE.MODULOREGISTROS;
using RombiBack.Services.ROM.FREPAPMODULE.MODULOREGISTROS;

namespace RombiBack.Controllers.ROM.FREPAPMODULE.GESTIONAFILIACIONES.MODULOREGISTROS
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfiliacionesController : ControllerBase
    {
        private readonly IAfiliacionesServices _afiliacionesServices;
        private const string Root = @"C:\Archivos";
        private const int MaxImageKB = 200;
        private const int MaxPdfMB = 2;

        private static readonly string[] ImageTypes = { "image/png", "image/jpeg", "image/jpg", "image/webp" };
        private static readonly string[] PdfTypes = { "application/pdf" };

        public AfiliacionesController(IAfiliacionesServices afiliacionesServices)
        {
            _afiliacionesServices = afiliacionesServices;
        }

        [Authorize]
        [HttpPost("GetListarAfiliaciones")]
        public async Task<ActionResult<IEnumerable<ListarAfiliacion>>> ListarAfiliaciones([FromBody] FiltroAfiliacion filtro)
        {
            var result = await _afiliacionesServices.ListarAfiliaciones(filtro);
            return Ok(result);
        }


        [Authorize]
        [HttpPost("GetListarUbigeos")]
        public async Task<ActionResult<IEnumerable<ListarOpcionUbigeo>>> Listar([FromBody] FiltroUbigeo f)
        => Ok(await _afiliacionesServices.ListarUbigeos(f.idemppaisnegcue, f.pais));

        [Authorize]
        [HttpPost("registrarafiliacion")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Registrar([FromForm] RegistrarAfiliacionForm form)
        {
            var res = await _afiliacionesServices.RegistrarAfiliacion(
                // mapea campos del form a tu DTO si lo necesitas
                new AfiliacionCreateDto
                {
                    numficha = form.numficha,
                    fechaafiliacion = form.fechaafiliacion,
                    nombres = form.nombres,
                    apellidopaterno = form.apellidopaterno,
                    apellidomaterno = form.apellidomaterno,
                    idtipodocumento = form.idtipodocumento,
                    docafiliado = form.docafiliado,
                    fechanacimiento = form.fechanacimiento,
                    edadafiliado = form.edadafiliado,
                    sexo = form.sexo,
                    idestadocivil = form.idestadocivil,
                    lugarnacimiento = form.lugarnacimiento,
                    rr = form.rr,
                    pp = form.pp,
                    dd = form.dd,
                    ubigeo = form.ubigeo,
                    avenida = form.avenida,
                    numero = form.numero,
                    urbanizacion = form.urbanizacion,
                    telefono = form.telefono,
                    correo = form.correo,
                    estado_text = form.estado_text,
                    estado = form.estado,
                    observacion = form.observacion,
                    usuario_creacion = form.usuario_creacion
                },
                form.foto, form.fichaafiliacionfile, form.hojadevida, form.copiadocumento);

            return Ok(res);
        }

        // === GET BY ID ===
        [Authorize]
        [HttpPost("getById")]
        public async Task<IActionResult> GetById([FromBody] int id)
        {
            var dto = await _afiliacionesServices.GetAfiliacionById(id);
            if (dto == null) return NotFound(new { message = "No existe la afiliación." });
            return Ok(dto);
        }

        // === UPDATE TOTAL (DATOS + ARCHIVOS) vía POST, id separado en la ruta ===
        [HttpPost("update/{id:int}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromForm] AfiliacionUpdateForm form
        )
        {
            var res = await _afiliacionesServices.ActualizarAfiliacion(
                id,
                new AfiliacionUpdateDto
                {
                    numficha = form.numficha,
                    idtipodocumento = form.idtipodocumento,
                    docafiliado = form.docafiliado,
                    nombres = form.nombres,
                    apellidopaterno = form.apellidopaterno,
                    apellidomaterno = form.apellidomaterno,
                    fechanacimiento = form.fechanacimiento,
                    edadafiliado = form.edadafiliado,
                    sexo = form.sexo,
                    idestadocivil = form.idestadocivil,
                    lugarnacimiento = form.lugarnacimiento,
                    rr = form.rr,
                    pp = form.pp,
                    dd = form.dd,
                    avenida = form.avenida,
                    numero = form.numero,
                    urbanizacion = form.urbanizacion,
                    telefono = form.telefono,
                    correo = form.correo,
                    observacion = form.observacion,
                    fechaafiliacion = form.fechaafiliacion,
                    estado = form.estado
                },
                form.foto,
                form.fichaafiliacionfile,
                form.hojadevida,
                form.copiadocumento
            );

            if (!res.Ok) return BadRequest(res);
            return Ok(res);
        }

        [Authorize]
        [HttpGet("GetListarEstadosCiviles")]
        public async Task<IActionResult> GetListarEstadosCiviles([FromQuery] int idemppaisnegcue)
        {
            try
            {
                var result = await _afiliacionesServices.ListarEstadosCiviles(idemppaisnegcue);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = ex.Message, detail = ex.InnerException?.Message });
            }
        }

        [Authorize]
        [HttpGet("GetListarTiposDocumento")]
        public async Task<IActionResult> GetListarTiposDocumento([FromQuery] int idemppaisnegcue)
        {
            try
            {
                var result = await _afiliacionesServices.ListarTiposDocumento(idemppaisnegcue);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = ex.Message, detail = ex.InnerException?.Message });
            }
        }

        [Authorize]
        [HttpPost("PostDesactivarAfiliacion")]
        public async Task<IActionResult> PostDesactivarAfiliacion([FromBody] FiltroAfiliacionDesactivar request)
        {
            if (request == null)
                return BadRequest(new { success = false, message = "Datos inválidos" });

            var result = await _afiliacionesServices.PostDesactivarAfiliacion(request);

            if (!result.success)
                return BadRequest(result); // 👈 devuelve 400 si hubo error

            return Ok(result);
        }

    }

}