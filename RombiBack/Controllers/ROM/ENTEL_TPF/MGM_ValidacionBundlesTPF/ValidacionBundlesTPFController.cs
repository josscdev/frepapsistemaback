using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.AWS.ROM.ENTEL_RETAIL.Services;
using RombiBack.AWS.ROM.ENTEL_TPF.ServicesTPF;
using RombiBack.Entities.ROM.BIWEB.ValidacionBundles;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_ValidacionBundles;
using RombiBack.Services.ROM.ENTEL_TPF.MGM_ValidacionBundlesTPF;

namespace RombiBack.Controllers.ROM.ENTEL_TPF.MGM_ValidacionBundlesTPF
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidacionBundlesTPFController : ControllerBase
    {
        private readonly IValidacionBundlesTPFServices _validacionBundlesTPFServices;
        private readonly S3TPFServices _s3ServiceTPF;


        public ValidacionBundlesTPFController(IValidacionBundlesTPFServices validacionBundlesTPFServices, S3TPFServices s3ServiceTPF)
        {
            _validacionBundlesTPFServices = validacionBundlesTPFServices;
            _s3ServiceTPF = s3ServiceTPF;

        }

        //[HttpPost("GetBundlesVentas")]
        //public async Task<IActionResult> GetBundlesVentas([FromBody] int intIdVentasPrincipal)
        //{
        //    var rptabundle = await _validacionBundlesServices.GetBundlesVentas(intIdVentasPrincipal);
        //    return Ok(rptabundle);
        //}
        [HttpPost("uploadTPF")]
        public async Task<IActionResult> UploadFileTPF(IFormFile pdf)
        {
            try
            {
                if (pdf == null || pdf.Length == 0)
                    return BadRequest("No se ha proporcionado un archivo PDF.");

                var response = await _s3ServiceTPF.UploadFileToS3AsyncBundleTPF(pdf);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al cargar el archivo PDF en Amazon S3: {ex.Message}");
            }
        }




        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadFile([FromBody] UploadFileRequest request)
        //{
        //    if (request.FileContent == null || request.FileContent.Length == 0)
        //        return BadRequest("No file content provided.");

        //    string keyName = request.FileName;

        //    byte[] fileBytes = Convert.FromBase64String(request.FileContent);

        //    try
        //    {
        //        string result = await _s3Service.UploadFileToS3Async(fileBytes, keyName);
        //        return Ok(result);  // Only returns "ok"
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception details for debugging
        //        Console.Error.WriteLine(ex);
        //        return StatusCode(500, "Internal server error");
        //    }
        //}
        //public class UploadFileRequest
        //{
        //    public string FileName { get; set; }
        //    public string FileContent { get; set; } // Base64 encoded file content
        //}

        [HttpGet("GetBundlesVentasTPF/{id}")]
        public async Task<IActionResult> GetBundlesVentasTPF(int id)
        {
            var rptabundle = await _validacionBundlesTPFServices.GetBundlesVentasTPF(id);
            return Ok(rptabundle);
        }

        [HttpGet("ValidarCodigoAuthBundleTPF")]
        public async Task<IActionResult> ValidarCodigoAuthBundleTPF(int idventasdetalle, string codigoauthbundle)
        {
            var rptabundle = await _validacionBundlesTPFServices.ValidarCodigoAuthBundleTPF(idventasdetalle, codigoauthbundle);
            return Ok(rptabundle);
        }

        [HttpPost("PostBundlesFirmaTPF")]
        public async Task<IActionResult> PostBundlesFirmaTPF([FromBody] ValidacionBundle validacionbundle)
        {
            var rptabundle = await _validacionBundlesTPFServices.PostBundlesFirmaTPF(validacionbundle);
            return Ok(rptabundle);
        }

        [HttpPost("ValidarSubidaS3TPF")]
        public async Task<IActionResult> ValidarSubidaS3TPF([FromBody] int idventasdetalle)
        {
            var rptabundle = await _validacionBundlesTPFServices.ValidarSubidaS3TPF(idventasdetalle);
            return Ok(rptabundle);
        }
    }
}
