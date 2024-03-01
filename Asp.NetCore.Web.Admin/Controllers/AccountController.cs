using Asp.NetCore.Services.Identity;
using Asp.NetCore.Services.Models.Identity;
using Asp.NetCore.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Asp.NetCore.Services.Helpers;

namespace Asp.NetCore.Web.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;
        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/login")]
        public IActionResult Login()
        {            
            return View();
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultModel { Success = false, Code = StatusCodes.Status400BadRequest, Data = ModelState.GetErrorMessages() });
            }
            var loginRes = await _identityService.LoginAsync(ModelState, model);
            if (loginRes == false)
            {
                return BadRequest(new ResultModel { Success = loginRes, Code = StatusCodes.Status400BadRequest, Data = ModelState.GetErrorMessages() });
            }
            return Ok(new ResultModel { Success = loginRes, Code = StatusCodes.Status200OK });
        }
    }
}
