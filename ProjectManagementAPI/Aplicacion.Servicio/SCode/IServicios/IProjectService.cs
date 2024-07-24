using Aplicacion.DTO.SCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicio.SCode.IServicios
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjects();
        Task<ProjectDto> GetProjectById(int id);
        Task CreateProject(ProjectDto projectDto);
        Task UpdateProject(int id, ProjectDto projectDto);
        Task DeleteProject(int id);
    }
}
