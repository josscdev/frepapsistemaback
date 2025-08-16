using Microsoft.AspNetCore.Mvc;
using RombiBack.Services.ROM.WIN_RETAIL.MGM_ReportsWINRET;

namespace RombiBack.Controllers.ROM.WIN_RETAIL.MGM_ReportsWINRET
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsWINRETController : ControllerBase
    {
        private readonly IReportsWINRETServices _reportsWINRETServices;
        public ReportsWINRETController(IReportsWINRETServices reportsWINRETServices)
        {
            _reportsWINRETServices = reportsWINRETServices;
        }

        [HttpGet("GetReportesWINRET")]
        public async Task<IActionResult> GetReportesWINRET(string docusuario, int idemppaisnegcue)
        {
            var reportes = await _reportsWINRETServices.GetReportesWINRET(docusuario, idemppaisnegcue);
            return Ok(reportes);
        }
    }
}
