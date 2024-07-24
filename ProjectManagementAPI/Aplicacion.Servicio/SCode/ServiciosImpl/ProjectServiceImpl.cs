using Aplicacion.DTO.SCode;
using Aplicacion.Servicio.SCode.IServicios;
using AutoMapper;
using Persistencia.Entidades;
using Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicio.SCode.ServiciosImpl
{
    public class ProjectServiceImpl:IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectServiceImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjects()
        {
            try
            {
                var projects = await _unitOfWork.Projects.GetAll();
                return _mapper.Map<IEnumerable<ProjectDto>>(projects);
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al obtener los proyectos", ex);
            }
        }

        public async Task<ProjectDto> GetProjectById(int id)
        {
            try
            {
                var project = await _unitOfWork.Projects.GetById(id);
                return _mapper.Map<ProjectDto>(project);
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al obtener el proyecto", ex);
            }
        }

        public async Task CreateProject(ProjectDto projectDto)
        {
            try
            {
                var project = _mapper.Map<Project>(projectDto);
                await _unitOfWork.Projects.Add(project);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al crear el proyecto", ex);
            }
        }

        public async Task UpdateProject(int id, ProjectDto projectDto)
        {
            try
            {

                var project = await _unitOfWork.Projects.GetById(id);
                _mapper.Map(projectDto, project);
                _unitOfWork.Projects.Update(project);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al actualziar el proyecto", ex);
            }
        }

        public async Task DeleteProject(int id)
        {
            try
            {
                var project = await _unitOfWork.Projects.GetById(id);
                _unitOfWork.Projects.Delete(project);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al eliminar el proyecto", ex);
            }
        }
    }
}
