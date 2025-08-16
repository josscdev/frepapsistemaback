using Microsoft.AspNetCore.Mvc;
using RombiBack.AWS.ROM.ENTEL_RETAIL.Services;
using RombiBack.Entities.ROM.BIWEB.ValidacionBundles;
using RombiBack.Services.ROM.BIWEB.ValidacionBundles;

namespace RombiBack.Controllers.ROM.BIWEB.ValidacionBundles
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidacionBundlesRWController : ControllerBase
    {
        private readonly IValidacionBundlesRWServices _validacionBundlesServices;
        private readonly S3Service _s3Service;


        public ValidacionBundlesRWController(IValidacionBundlesRWServices validacionBundlesServices, S3Service s3Service)
        {
            _validacionBundlesServices = validacionBundlesServices;
            _s3Service = s3Service;

        }

        //[HttpPost("GetBundlesVentas")]
        //public async Task<IActionResult> GetBundlesVentas([FromBody] int intIdVentasPrincipal)
        //{
        //    var rptabundle = await _validacionBundlesServices.GetBundlesVentas(intIdVentasPrincipal);
        //    return Ok(rptabundle);
        //}
        [HttpPost("uploadRW")]
        public async Task<IActionResult> UploadFile(IFormFile pdf)
        {
            try
            {
                if (pdf == null || pdf.Length == 0)
                    return BadRequest("No se ha proporcionado un archivo PDF.");

                var response = await _s3Service.UploadFileToS3AsyncRW(pdf);

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

        [HttpGet("GetBundlesVentasRW/{id}")]
        public async Task<IActionResult> GetBundlesVentas(int id)
        {
            var rptabundle = await _validacionBundlesServices.GetBundlesVentas(id);
            return Ok(rptabundle);
        }

        [HttpGet("ValidarCodigoAuthBundleRW")]
        public async Task<IActionResult> ValidarCodigoAuthBundle(int intventasromid, string strcodigoauthbundle)
        {
            var rptabundle = await _validacionBundlesServices.ValidarCodigoAuthBundle(intventasromid, strcodigoauthbundle);
            return Ok(rptabundle);
        }

        [HttpPost("PostBundlesFirmaRW")]
        public async Task<IActionResult> PostBundlesFirma([FromBody] ValidacionBundlesRW validacionbundle)
        {
            var rptabundle = await _validacionBundlesServices.PostBundlesFirma(validacionbundle);
            return Ok(rptabundle);
        }

        [HttpPost("ValidarSubidaS3RW")]
        public async Task<IActionResult> ValidarSubidaS3([FromBody] int intventasromid)
        {
            var rptabundle = await _validacionBundlesServices.ValidarSubidaS3(intventasromid);
            return Ok(rptabundle);
        }
    }
}
