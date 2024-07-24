using Persistencia.Entidades;
using Persistencia.Modelos;
using Repositorio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectManagementContext _context;

        public UnitOfWork(ProjectManagementContext context)
        {
            _context = context;
            Roles = new Repository<Role>(_context);
            Users = new Repository<User>(_context);
            Projects = new Repository<Project>(_context);
            Tareas = new Repository<Tarea>(_context);
        }

        public IRepository<Role> Roles { get; private set; }
        public IRepository<User> Users { get; private set; }
        public IRepository<Project> Projects { get; private set; }
        public IRepository<Tarea> Tareas { get; private set; }


        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
