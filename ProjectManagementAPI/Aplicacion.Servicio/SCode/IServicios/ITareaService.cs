using Aplicacion.DTO.SCode;
using Persistencia.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicio.SCode.IServicios
{
    public interface ITareaService
    {
        Task<IEnumerable<TareaDto>> GetAllTasks();
        Task<TareaDto> GetTaskById(int id);
        Task<IEnumerable<TareaDto>> GetTaskByProject(int id);
        Task CreateTask(TareaSaveDto taskDto,int userId);
        Task UpdateTask(int id, TareaUpdateDto taskDto, int userId);
        Task DeleteTask(int id);
    }
}
