using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RombiBack.AWS.ROM.ENTEL_RETAIL.Services;
using RombiBack.AWS.ROM.ENTEL_TPF.ServicesTPF;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Ventas;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Ventas;
using RombiBack.Services.ROM.ENTEL_TPF.MGM_VentasTPF;
using System.Text;

namespace RombiBack.Controllers.ROM.ENTEL_TPF.MGM_VentasTPF
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasTPFController : ControllerBase
    {
        private readonly IVentasTPFServices _ventasTPFServices;
        private readonly S3TPFServices _s3TPFService;

        public VentasTPFController(IVentasTPFServices ventasTPFServices, S3TPFServices s3TPFService)
        {
            _ventasTPFServices = ventasTPFServices;
            _s3TPFService = s3TPFService;
        }

        //[Authorize]
        //[HttpPost("GetTipoDocumentoTPF")]
        //public async Task<IActionResult> GetTipoDocumentoTPF([FromBody] int idemppaisnegcue)
        //{
        //    var tipdocs = await _ventasTPFServices.GetTipoDocumentoTPF(idemppaisnegcue);
        //    return Ok(tipdocs);
        //}

        //[Authorize]
        //[HttpPost("GetTipoBiometriaTPF")]
        //public async Task<IActionResult> GetTipoBiometriaTPF([FromBody] int idemppaisnegcue)
        //{
        //    var tipbiome = await _ventasTPFServices.GetTipoBiometriaTPF(idemppaisnegcue);
        //    return Ok(tipbiome);
        //}

        //[Authorize]
        //[HttpPost("GetSubproductoTPF")]
        //public async Task<IActionResult> GetSubproductoTPF([FromBody] int idemppaisnegcue)
        //{
        //    var subproductos = await _ventasTPFServices.GetSubproductoTPF(idemppaisnegcue);
        //    return Ok(subproductos);
        //}

        //[Authorize]
        //[HttpPost("GetOperadorTPF")]
        //public async Task<IActionResult> GetOperadorTPF([FromBody] int idemppaisnegcue)
        //{
        //    var operadores = await _ventasTPFServices.GetOperadorTPF(idemppaisnegcue);
        //    return Ok(operadores);
        //}

        //[Authorize]
        //[HttpPost("GetTipoEquipoTPF")]
        //public async Task<IActionResult> GetTipoEquipoTPF([FromBody] int idemppaisnegcue)
        //{
        //    var tipoequipo = await _ventasTPFServices.GetTipoEquipoTPF(idemppaisnegcue);
        //    return Ok(tipoequipo);
        //}


        //[Authorize]
        //[HttpPost("GetTipoSeguroTPF")]
        //public async Task<IActionResult> GetTipoSeguroTPF([FromBody] int idemppaisnegcue)
        //{
        //    var tiposeguro = await _ventasTPFServices.GetTipoSeguroTPF(idemppaisnegcue);
        //    return Ok(tiposeguro);
        //}

        //[Authorize]
        //[HttpPost("GetTipoEtiquetaTPF")]
        //public async Task<IActionResult> GetTipoEtiquetaTPF([FromBody] int idemppaisnegcue)
        //{
        //    var tipoequipo = await _ventasTPFServices.GetTipoEtiquetaTPF(idemppaisnegcue);
        //    return Ok(tipoequipo);
        //}

        //[Authorize]
        //[HttpPost("GetTipoPagoTPF")]
        //public async Task<IActionResult> GetTipoPagoTPF([FromBody] int idemppaisnegcue)
        //{
        //    var tipopago = await _ventasTPFServices.GetTipoPagoTPF(idemppaisnegcue);
        //    return Ok(tipopago);
        //}

        //[Authorize]
        //[HttpPost("GetPlanesTPF")]
        //public async Task<IActionResult> GetPlanesTPF([FromBody] int idemppaisnegcue)
        //{
        //    var planes = await _ventasTPFServices.GetPlanesTPF(idemppaisnegcue);
        //    return Ok(planes);
        //}

        //[Authorize]
        //[HttpPost("GetModeloTPF")]
        //public async Task<IActionResult> GetModeloTPF([FromBody] int idemppaisnegcue)
        //{
        //    var planes = await _ventasTPFServices.GetModeloTPF(idemppaisnegcue);
        //    return Ok(planes);
        //}

        //[Authorize]
        //[HttpPost("GetBundleTPF")]
        //public async Task<IActionResult> GetBundleTPF([FromBody] int idemppaisnegcue)
        //{
        //    var bundle = await _ventasTPFServices.GetBundleTPF(idemppaisnegcue);
        //    return Ok(bundle);
        //}

        //[Authorize]
        //[HttpPost("GetTipoAccesorioTPF")]
        //public async Task<IActionResult> GetTipoAccesorioTPF([FromBody] int idemppaisnegcue)
        //{
        //    var tipacc = await _ventasTPFServices.GetTipoAccesorioTPF(idemppaisnegcue);
        //    return Ok(tipacc);
        //}


        //[Authorize]
        //[HttpPost("GetDataReaderCodeTPF")]
        //public async Task<IActionResult> GetDataReaderCodeTPF([FromBody] string archivoBase64)
        //{

        //    // Crear un objeto HttpClient
        //    using (HttpClient client = new HttpClient())
        //    {
        //        // Especificar la URL del servicio web
        //        string url = "http://apiboletas.grupotawa.com:9097/Boleta/ObtenerDatosBoleta";

        //        // Crear un objeto JSON que contenga el archivoBase64
        //        var body = new { archivoBase64 = archivoBase64 };

        //        // Convertir el objeto JSON a una cadena
        //        var jsonBody = JsonConvert.SerializeObject(body);

        //        // Crear una instancia de HttpContent que contenga la cadena JSON
        //        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        //        // Enviar la solicitud HTTP POST a la API y recibir la respuesta
        //        HttpResponseMessage response = await client.PostAsync(url, content);

        //        // Leer el contenido de la respuesta como una cadena JSON
        //        string json = await response.Content.ReadAsStringAsync();

        //        // Deserializar la cadena JSON en un objeto ApiResponse
        //        BoletaReader.BoletaResponse responseObj = JsonConvert.DeserializeObject<BoletaReader.BoletaResponse>(json);

        //        //// Capturar los valores de numRuc y codComp
        //        //string strNumRuc = responseObj.data.numRuc;
        //        //string strCodComp = responseObj.data.codComp;
        //        //string strNumeroSerie = responseObj.data.numeroSerie;
        //        //string strNumero = responseObj.data.numero;
        //        //DateTime fechaEmision =  responseObj.data.fechaEmision);
        //        //decimal decMonto = responseObj.data.monto;
        //        //string strTramaQrCode = responseObj.data.tramaQRCode;

        //        // Crear un objeto JsonResult que contenga los valores de numRuc y codComp
        //        return new JsonResult(new { data = responseObj });


        //    }
        //}

        //[HttpPost("UploadVoucherTPF")]
        //public async Task<IActionResult> UploadVoucherTPF([FromBody] UploadImageRequest request)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(request.Base64Image) || string.IsNullOrEmpty(request.VoucherName))
        //            return BadRequest("No se ha proporcionado una imagen o nombre del voucher.");

        //        var response = await _s3TPFService.UploadImageToS3AsyncBoletasTPF(request.Base64Image, request.VoucherName);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al cargar la imagen en Amazon S3: {ex.Message}");
        //    }
        //}

        //[HttpPost("UpdateNombreVoucherTPF")]
        //public async Task<IActionResult> UpdateNombreVoucherTPF([FromBody] ActualizarNombreVoucherRequest request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest("Invalid request.");
        //    }

        //    var response = await _ventasTPFServices.UpdateNombreVoucherTPF(request);

        //    if (response == null)
        //    {
        //        return StatusCode(500, "An error occurred while updating the voucher.");
        //    }

        //    return Ok(response);
        //}

        //[Authorize]
        //[HttpPost("GetVentasAdminTPF")]
        //public async Task<IActionResult> GetVentasAdminTPF([FromBody] FiltrarVentasPerfiles filtros)
        //{
        //    var ventas = await _ventasTPFServices.GetVentasAdminTPF(filtros);
        //    return Ok(ventas);
        //}


        //[Authorize]
        //[HttpPost("GetVentasJefeTPF")]
        //public async Task<IActionResult> GetVentasJefeTPF([FromBody] FiltrarVentasPerfiles filtros)
        //{
        //    var ventas = await _ventasTPFServices.GetVentasJefeTPF(filtros);
        //    return Ok(ventas);
        //}


        //[Authorize]
        //[HttpPost("GetVentasSuperTPF")]
        //public async Task<IActionResult> GetVentasSuperTPF([FromBody] FiltrarVentasPerfiles filtros)
        //{
        //    var ventas = await _ventasTPFServices.GetVentasSuperTPF(filtros);
        //    return Ok(ventas);
        //}


        //[Authorize]
        //[HttpPost("GetVentasPromotorTPF")]
        //public async Task<IActionResult> GetVentasPromotorTPF([FromBody] FiltrarVentasPerfiles filtros)
        //{
        //    var ventas = await _ventasTPFServices.GetVentasPromotorTPF(filtros);
        //    return Ok(ventas);
        //}

        //[Authorize]
        //[HttpPost("PostVentasTPF")]
        //public async Task<IActionResult> PostVentasTPF([FromBody] Ventas ventas)
        //{
        //    if (ventas == null)
        //    {
        //        return BadRequest("El objeto de ventas no puede ser nulo.");
        //    }

        //    var result = await _ventasTPFServices.PostVentasTPF(ventas);
        //    return Ok(result);
        //}

        //[Authorize]
        //[HttpPost("DeleteVentasDetalleTPF")]
        //public async Task<IActionResult> DeleteVentasDetalle(int idventasdetalle, string usuarioanulacion)
        //{
        //    var result = await _ventasTPFServices.DeleteVentasDetalleTPF(idventasdetalle, usuarioanulacion);
        //    return Ok(result);
        //}

        //[Authorize]
        //[HttpPost("UpdateVentasDetalleTPF")]
        //public async Task<IActionResult> UpdateVentasDetalleTPF([FromBody] VentasDetalle request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest("El objeto de ventas detalle no puede ser nulo.");
        //    }

        //    var result = await _ventasTPFServices.UpdateVentasDetalleTPF(request);
        //    return Ok(result);
        //}

        //[Authorize]
        //[HttpPost("UpdateVoucherVentasTPF")]
        //public async Task<IActionResult> UpdateVoucherVentasTPF([FromBody] Ventas request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest("El objeto de ventas detalle no puede ser nulo.");
        //    }

        //    var result = await _ventasTPFServices.UpdateVoucherVentasTPF(request);
        //    return Ok(result);
        //}

        //[Authorize]
        //[HttpPost("GetUrlPrefirmadaTPF")]
        //public IActionResult GetUrlPrefirmadaTPF([FromBody] ActualizarNombreVoucherRequest request)
        //{
        //    try
        //    {
        //        string preSignedUrl = _s3TPFService.GetPreSignedUrlForVoucherTPF(request.nombrevoucher);

        //        return Ok(new
        //        {
        //            Satisfactorio = true,
        //            Titulo = "Validando",
        //            Mensaje = "Documento encontrado con éxito",
        //            UrlPrefirmada = preSignedUrl
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new
        //        {
        //            Satisfactorio = false,
        //            Titulo = "Error",
        //            Mensaje = "Error al verificar el archivo: " + ex.Message
        //        });
        //    }
        //}
    }
}
