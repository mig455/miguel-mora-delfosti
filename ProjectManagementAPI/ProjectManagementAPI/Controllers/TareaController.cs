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
    public class TareaController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tareas = await _tareaService.GetAllTasks();
                return Ok(tareas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTaskByProject/{id}")]
        public async Task<IActionResult> GetTaskByProject(int id)
        {
            try
            {
                var tareas = await _tareaService.GetTaskByProject(id);
                return Ok(tareas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            try
            {
                var tarea = await _tareaService.GetTaskById(id);
                if (tarea == null)
                {
                    return NotFound();
                }
                return Ok(tarea);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TareaSaveDto tareaDto)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.Claims.Where(x => x.Type == "Id").Select(s => s.Value).First();
                await _tareaService.CreateTask(tareaDto, int.Parse(userId));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TareaUpdateDto tareaDto)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.Claims.Where(x => x.Type == "Id").Select(s => s.Value).First();
                await _tareaService.UpdateTask(id, tareaDto, int.Parse(userId));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                await _tareaService.DeleteTask(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
