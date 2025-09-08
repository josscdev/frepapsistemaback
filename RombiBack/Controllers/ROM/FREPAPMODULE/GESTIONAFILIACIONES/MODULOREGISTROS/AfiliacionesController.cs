using System.Text.Json;
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

        [HttpPost("GetListarAfiliaciones")]
        public async Task<ActionResult<IEnumerable<ListarAfiliacion>>> ListarAfiliaciones([FromBody] FiltroAfiliacion filtro)
        {
            var result = await _afiliacionesServices.ListarAfiliaciones(filtro);
            return Ok(result);
        }


       
        [HttpPost("GetListarUbigeos")]
        public async Task<ActionResult<IEnumerable<ListarOpcionUbigeo>>> Listar([FromBody] FiltroUbigeo f)
        => Ok(await _afiliacionesServices.ListarUbigeos(f.idemppaisnegcue, f.pais));

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
                    observacion = form.observacion
                },
                form.foto, form.fichaafiliacionfile, form.hojadevida, form.copiadocumento);

            return Ok(res);
        }

    }

}