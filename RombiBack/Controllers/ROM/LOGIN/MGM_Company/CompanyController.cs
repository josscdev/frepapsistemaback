using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Products;
using RombiBack.Services.ROM.LOGIN.Company;

namespace RombiBack.Controllers.ROM.LOGIN.MGM_Company
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyServices _companyService;
        public CompanyController(ICompanyServices companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanys()
        {
            var company = await _companyService.GetCompany();
            return Ok(company);
        }
    }
}
