using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Services.ROM.LOGIN.MGM_Country;
using RombiBack.Services.ROM.LOGIN.MGM_UserType;

namespace RombiBack.Controllers.ROM.LOGIN.MGM_UserType
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUsertypeServices _usertypeservices;
        public UserTypeController(IUsertypeServices usertypeservices)
        {
            _usertypeservices = usertypeservices;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserType()
        {
            var usertype = await _usertypeservices.GetAll();
            return Ok(usertype);
        }
    }
}
