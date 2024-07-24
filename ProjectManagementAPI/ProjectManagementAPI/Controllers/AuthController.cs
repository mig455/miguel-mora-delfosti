using Aplicacion.DTO.SCode;
using Aplicacion.Servicio.SCode.IServicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                await _userService.CreateUser(userDto); 
                var user = await _userService.Authenticate(userDto.Username, userDto.Password);
                if (user == null)
                {
                    return Unauthorized();
                }

                return Ok(new { Token = user.Token });
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
            
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = await _userService.Authenticate(loginDto.Username, loginDto.Password);
                if (user == null)
                {
                    return Unauthorized();
                }

                return Ok(new { Token = user.Token });
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
            
        }
    }
}
