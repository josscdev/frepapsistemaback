using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_ValidacionBundles;
using RombiBack.AWS.ROM.ENTEL_RETAIL.Services;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles;


namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_ValidacionBundles
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidacionBundlesController : ControllerBase
    {
        private readonly IValidacionBundlesServices _validacionBundlesServices;
        private readonly S3Service _s3Service;


        public ValidacionBundlesController(IValidacionBundlesServices validacionBundlesServices, S3Service s3Service)
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
        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadFile(IFormFile pdf)
        //{
        //    try
        //    {
        //        if (pdf == null || pdf.Length == 0)
        //            return BadRequest("No se ha proporcionado un archivo PDF.");

        //        var response = await _s3Service.UploadFileToS3Async(pdf);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al cargar el archivo PDF en Amazon S3: {ex.Message}");
        //    }
        //}




        ////[HttpPost("upload")]
        ////public async Task<IActionResult> UploadFile([FromBody] UploadFileRequest request)
        ////{
        ////    if (request.FileContent == null || request.FileContent.Length == 0)
        ////        return BadRequest("No file content provided.");

        ////    string keyName = request.FileName;

        ////    byte[] fileBytes = Convert.FromBase64String(request.FileContent);

        ////    try
        ////    {
        ////        string result = await _s3Service.UploadFileToS3Async(fileBytes, keyName);
        ////        return Ok(result);  // Only returns "ok"
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        // Log the exception details for debugging
        ////        Console.Error.WriteLine(ex);
        ////        return StatusCode(500, "Internal server error");
        ////    }
        ////}
        ////public class UploadFileRequest
        ////{
        ////    public string FileName { get; set; }
        ////    public string FileContent { get; set; } // Base64 encoded file content
        ////}

        //[HttpGet("GetBundlesVentas/{id}")]
        //public async Task<IActionResult> GetBundlesVentas(int id)
        //{
        //    var rptabundle = await _validacionBundlesServices.GetBundlesVentas(id);
        //    return Ok(rptabundle);
        //}

        //[HttpGet("ValidarCodigoAuthBundle")]
        //public async Task<IActionResult> ValidarCodigoAuthBundle(int idventasdetalle, string codigoauthbundle)
        //{
        //    var rptabundle = await _validacionBundlesServices.ValidarCodigoAuthBundle(idventasdetalle, codigoauthbundle);
        //    return Ok(rptabundle);
        //}

        //[HttpPost("PostBundlesFirma")]
        //public async Task<IActionResult> PostBundlesFirma([FromBody] ValidacionBundle validacionbundle)
        //{
        //    var rptabundle = await _validacionBundlesServices.PostBundlesFirma(validacionbundle);
        //    return Ok(rptabundle);
        //}

        //[HttpPost("ValidarSubidaS3")]
        //public async Task<IActionResult> ValidarSubidaS3([FromBody] int idventasdetalle)
        //{
        //    var rptabundle = await _validacionBundlesServices.ValidarSubidaS3(idventasdetalle);
        //    return Ok(rptabundle);
        //}
    }
}
