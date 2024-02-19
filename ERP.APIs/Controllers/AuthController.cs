using ERP.Core.Dtos;
using ERP.Core.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.APIs.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthRegisterResponseDto>> RegisterAsync([FromBody] RegisterDto model)
        {
            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(new
                {
                    Message = result.Message,
                    IsAuthenticated = result.IsAuthenticated
                });

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthLoginResponseDto>> LoginAsync([FromBody] LoginDto model)
        {
            var result = await _authService.LoginAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(new
                {
                    Message = result.Message,
                    IsAuthenticated = result.IsAuthenticated
                });

            return Ok(result);
        }
    }
}
