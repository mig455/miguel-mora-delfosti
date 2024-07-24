using Aplicacion.DTO.SCode;
using Aplicacion.Servicio.SCode.IServicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistencia.Entidades;
using Repositorio.UnitOfWork;
using System.Security.Claims;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById()
        {
            try
            {

                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.Claims.Where(x => x.Type == "Id").Select(s => s.Value).First();
                var user = await _userService.GetUserById(int.Parse(userId));
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                await _userService.CreateUser(userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            try
            {
                await _userService.UpdateUser(id, userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
