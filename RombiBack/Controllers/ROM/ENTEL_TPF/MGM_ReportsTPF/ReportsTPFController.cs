using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Reports;
using RombiBack.Services.ROM.ENTEL_TPF.MGM_ReportsTPF;

namespace RombiBack.Controllers.ROM.ENTEL_TPF.MGM_ReportsTPF
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsTPFController : ControllerBase
    {
        private readonly IReportsTPFServices _reportsTPFServices;

        public ReportsTPFController(IReportsTPFServices reportsTPFServices)
        {
            _reportsTPFServices = reportsTPFServices;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetReports()
        //{
        //    var reporte = await _reportsServices.GetAll();
        //    return Ok(reporte);
        //}

        //[HttpGet("GetReportesTPF")]
        //public async Task<IActionResult> GetReportesTPF(string docusuario, int idemppaisnegcue)
        //{
        //    var tipdocs = await _reportsTPFServices.GetReportesTPF(docusuario, idemppaisnegcue);
        //    return Ok(tipdocs);
        //}
    }
}
