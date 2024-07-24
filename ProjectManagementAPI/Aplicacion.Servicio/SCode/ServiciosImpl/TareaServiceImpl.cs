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
    public class TareaServiceImpl : ITareaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TareaServiceImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TareaDto>> GetAllTasks()
        {
            try
            {
                var tasks = await _unitOfWork.Tareas.GetAll();
                return _mapper.Map<IEnumerable<TareaDto>>(tasks);
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al obtener las tareas", ex);
            }
        }

        public async Task<TareaDto> GetTaskById(int id)
        {
            try
            {
                var task = await _unitOfWork.Tareas.GetById(id);
                return _mapper.Map<TareaDto>(task);
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al obtener la tarea", ex);
            }
        }

        public async Task<IEnumerable<TareaDto>> GetTaskByProject(int id)
        {
            try
            {
                var tasks = await _unitOfWork.Tareas.GetBy(x=>x.ProjectId==id);
                return _mapper.Map<IEnumerable<TareaDto>>(tasks);
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al obtener la tarea", ex);
            }
        }
        public async Task CreateTask(TareaSaveDto taskDto, int userId)
        {
            try
            {
                var proyect = await _unitOfWork.Projects.GetBy(x => x.Name == taskDto.Proyecto);
                if(proyect==null || proyect.Count() == 0)
                {
                    var newProject = new Project();
                    newProject.CreatedBy = userId;
                    newProject.ModifiedBy = userId;
                    newProject.ModifiedDate = DateTime.Now;
                    newProject.CreatedDate = DateTime.Now;
                    newProject.Estado = true;
                    newProject.Name = taskDto.Proyecto;
                    await _unitOfWork.Projects.Add(newProject);
                    await _unitOfWork.Complete();
                    proyect = await _unitOfWork.Projects.GetBy(x => x.Name == taskDto.Proyecto);
                }
                var proyectId= proyect.FirstOrDefault()?.Id;

                var task = new Tarea();
                task.CreatedBy = userId;
                task.ModifiedBy = userId;
                task.ModifiedDate = DateTime.Now;
                task.CreatedDate = DateTime.Now;
                task.Estado = true;
                task.ProjectId = (int)proyectId;
                task.Title = taskDto.Title;
                task.Description = taskDto.Description;
                task.Status = "Pendiente";
                await _unitOfWork.Tareas.Add(task);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al ocrear la tarea", ex);
            }
        }

        public async Task UpdateTask(int id, TareaUpdateDto taskDto, int userId)
        {
            try
            {
                var task = await _unitOfWork.Tareas.GetById(id);
                task.Status = taskDto.Status;
                task.ModifiedDate = DateTime.Now;
                task.ModifiedBy = userId;
                _unitOfWork.Tareas.Update(task);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al actualizar la tarea", ex);
            }
        }

        public async Task DeleteTask(int id)
        {
            try
            {
                var task = await _unitOfWork.Tareas.GetById(id);
                _unitOfWork.Tareas.Delete(task);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al eliminar la tarea", ex);
            }
        }
    }
}
