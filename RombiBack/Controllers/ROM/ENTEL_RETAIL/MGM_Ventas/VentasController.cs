using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Permisos;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Ventas;
using System.Text;
using System.Net.Http;
using System.Text.Json.Nodes;
using Amazon.S3.Model;
using Amazon.S3;
using Amazon.S3.Util;
using Amazon;
using System;
using System.IO;
using System.Threading.Tasks;
using RombiBack.AWS.ROM.ENTEL_RETAIL.Services;
using Microsoft.AspNetCore.Authorization;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_Ventas
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController: ControllerBase
    {
        private readonly IVentasServices _ventasServices;
        private readonly S3Service _s3Service;

        public VentasController(IVentasServices ventasServices, S3Service s3Service)
        {
            _ventasServices = ventasServices;
            _s3Service = s3Service;
        }

        [Authorize]
        [HttpPost("GetTipoDocumento")]
        public async Task<IActionResult> GetTipoDocumento([FromBody] int idemppaisnegcue)
        {
            var tipdocs = await _ventasServices.GetTipoDocumento(idemppaisnegcue);
            return Ok(tipdocs);
        }

        [Authorize]
        [HttpPost("GetTipoBiometria")]
        public async Task<IActionResult> GetTipoBiometria([FromBody] int idemppaisnegcue)
        {
            var tipbiome = await _ventasServices.GetTipoBiometria(idemppaisnegcue);
            return Ok(tipbiome);
        }

        [Authorize]
        [HttpPost("GetSubproducto")]
        public async Task<IActionResult> GetSubproducto([FromBody] int idemppaisnegcue)
        {
            var subproductos = await _ventasServices.GetSubproducto(idemppaisnegcue);
            return Ok(subproductos);
        }

        [Authorize]
        [HttpPost("GetOperador")]
        public async Task<IActionResult> GetOperador([FromBody] int idemppaisnegcue)
        {
            var operadores = await _ventasServices.GetOperador(idemppaisnegcue);
            return Ok(operadores);
        }

        [Authorize]
        [HttpPost("GetTipoEquipo")]
        public async Task<IActionResult> GetTipoEquipo([FromBody] int idemppaisnegcue)
        {
            var tipoequipo = await _ventasServices.GetTipoEquipo(idemppaisnegcue);
            return Ok(tipoequipo);
        }

        [Authorize]
        [HttpPost("GetTipoSeguro")]
        public async Task<IActionResult> GetTipoSeguroTPF([FromBody] int idemppaisnegcue)
        {
            var tiposeguro = await _ventasServices.GetTipoSeguro(idemppaisnegcue);
            return Ok(tiposeguro);
        }

        [Authorize]
        [HttpPost("GetTipoEtiqueta")]
        public async Task<IActionResult> GetTipoEtiqueta([FromBody] int idemppaisnegcue)
        {
            var tipoequipo = await _ventasServices.GetTipoEtiqueta(idemppaisnegcue);
            return Ok(tipoequipo);
        }

        [Authorize]
        [HttpPost("GetTipoPago")]
        public async Task<IActionResult> GetTipoPago([FromBody] int idemppaisnegcue)
        {
            var tipopago = await _ventasServices.GetTipoPago(idemppaisnegcue);
            return Ok(tipopago);
        }

        [Authorize]
        [HttpPost("GetPlanes")]
        public async Task<IActionResult> GetPlanes([FromBody] int idemppaisnegcue)
        {
            var planes = await _ventasServices.GetPlanes(idemppaisnegcue);
            return Ok(planes);
        }

        [Authorize]
        [HttpPost("GetModelo")]
        public async Task<IActionResult> GetModelo([FromBody] int idemppaisnegcue)
        {
            var planes = await _ventasServices.GetModelo(idemppaisnegcue);
            return Ok(planes);
        }

        [Authorize]
        [HttpPost("GetBundle")]
        public async Task<IActionResult> GetBundle([FromBody] int idemppaisnegcue)
        {
            var bundle = await _ventasServices.GetBundle(idemppaisnegcue);
            return Ok(bundle);
        }

        [Authorize]
        [HttpPost("GetTipoAccesorio")]
        public async Task<IActionResult> GetTipoAccesorio([FromBody] int idemppaisnegcue)
        {
            var tipacc = await _ventasServices.GetTipoAccesorio(idemppaisnegcue);
            return Ok(tipacc);
        }

        [Authorize]
        [HttpPost("GetIMEISequipos")]
        public async Task<IActionResult> GetIMEISequipos([FromBody] int idusuario)
        {
            var imeiequipos = await _ventasServices.GetIMEISequipos(idusuario);
            return Ok(imeiequipos);
        }

        [Authorize]
        [HttpPost("GetDataReaderCode")]
        public async Task<IActionResult> GetDataReaderCode([FromBody] string archivoBase64)
        {

            // Crear un objeto HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Especificar la URL del servicio web
                string url = "http://apiboletas.grupotawa.com:9097/Boleta/ObtenerDatosBoleta";

                // Crear un objeto JSON que contenga el archivoBase64
                var body = new { archivoBase64 = archivoBase64 };

                // Convertir el objeto JSON a una cadena
                var jsonBody = JsonConvert.SerializeObject(body);

                // Crear una instancia de HttpContent que contenga la cadena JSON
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Enviar la solicitud HTTP POST a la API y recibir la respuesta
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Leer el contenido de la respuesta como una cadena JSON
                string json = await response.Content.ReadAsStringAsync();

                // Deserializar la cadena JSON en un objeto ApiResponse
                BoletaReader.BoletaResponse responseObj = JsonConvert.DeserializeObject<BoletaReader.BoletaResponse>(json);

                //// Capturar los valores de numRuc y codComp
                //string strNumRuc = responseObj.data.numRuc;
                //string strCodComp = responseObj.data.codComp;
                //string strNumeroSerie = responseObj.data.numeroSerie;
                //string strNumero = responseObj.data.numero;
                //DateTime fechaEmision =  responseObj.data.fechaEmision);
                //decimal decMonto = responseObj.data.monto;
                //string strTramaQrCode = responseObj.data.tramaQRCode;

                // Crear un objeto JsonResult que contenga los valores de numRuc y codComp
                return new JsonResult(new { data = responseObj });


            }
        }

        [HttpPost("UploadVoucherRetail")]
        public async Task<IActionResult> UploadVoucherRetail([FromBody] UploadImageRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Base64Image) || string.IsNullOrEmpty(request.VoucherName))
                    return BadRequest("No se ha proporcionado una imagen o nombre del voucher.");

                var response = await _s3Service.UploadImageToS3AsyncRetail(request.Base64Image, request.VoucherName);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar la imagen en Amazon S3: {ex.Message}");
            }
        }

        [HttpPost("UpdateNombreVoucherRetail")]
        public async Task<IActionResult> UpdateNombreVoucherRetail([FromBody] ActualizarNombreVoucherRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request.");
            }

            var response = await _ventasServices.UpdateNombreVoucherRetail(request);

            if (response == null)
            {
                return StatusCode(500, "An error occurred while updating the voucher.");
            }

            return Ok(response);
        }

        [Authorize]
        [HttpPost("GetVentasAdmin")]
        public async Task<IActionResult> GetVentasAdmin([FromBody] FiltrarVentasPerfiles filtros)
        {
            var ventas = await _ventasServices.GetVentasAdmin(filtros);
            return Ok(ventas);
        }


        [Authorize]
        [HttpPost("GetVentasJefe")]
        public async Task<IActionResult> GetVentasJefe([FromBody] FiltrarVentasPerfiles filtros)
        {
            var ventas = await _ventasServices.GetVentasJefe(filtros);
            return Ok(ventas);
        }


        [Authorize]
        [HttpPost("GetVentasSuper")]
        public async Task<IActionResult> GetVentasSuper([FromBody] FiltrarVentasPerfiles filtros)
        {
            var ventas = await _ventasServices.GetVentasSuper(filtros);
            return Ok(ventas);
        }


        [Authorize]
        [HttpPost("GetVentasPromotor")]
        public async Task<IActionResult> GetVentasPromotor([FromBody] FiltrarVentasPerfiles filtros)
        {
            var ventas = await _ventasServices.GetVentasPromotor(filtros);
            return Ok(ventas);
        }

        [Authorize]
        [HttpPost("PostVentas")]
        public async Task<IActionResult> PostVentas([FromBody] Ventas ventas)
        {
            if (ventas == null)
            {
                return BadRequest("El objeto de ventas no puede ser nulo.");
            }

            var result = await _ventasServices.PostVentas(ventas);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("DeleteVentasDetalle")]
        public async Task<IActionResult> DeleteVentasDetalle(int idventasdetalle, string usuarioanulacion)
        {
            var result = await _ventasServices.DeleteVentasDetalle(idventasdetalle, usuarioanulacion);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("UpdateVentasDetalle")]
        public async Task<IActionResult> UpdateVentasDetalle([FromBody] VentasDetalle request)
        {
            if (request == null)
            {
                return BadRequest("El objeto de ventas detalle no puede ser nulo.");
            }

            var result = await _ventasServices.UpdateVentasDetalle(request);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("UpdateVoucherVentas")]
        public async Task<IActionResult> UpdateVoucherVentas([FromBody] Ventas request)
        {
            if (request == null)
            {
                return BadRequest("El objeto de ventas detalle no puede ser nulo.");
            }

            var result = await _ventasServices.UpdateVoucherVentas(request);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("GetUrlPrefirmada")]
        public IActionResult GetUrlPrefirmada([FromBody] ActualizarNombreVoucherRequest request)
        {
            try
            {
                string preSignedUrl = _s3Service.GetPreSignedUrlForVoucher(request.nombrevoucher);

                return Ok(new
                {
                    Satisfactorio = true,
                    Titulo = "Validando",
                    Mensaje = "Documento encontrado con éxito",
                    UrlPrefirmada = preSignedUrl
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Satisfactorio = false,
                    Titulo = "Error",
                    Mensaje = "Error al verificar el archivo: " + ex.Message
                });
            }
        }
    }
}
