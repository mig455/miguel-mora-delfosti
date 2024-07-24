using Persistencia.Entidades;
using Repositorio.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Repositorio.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //void Commit();
        //Task CommitAsync();
        //void Rollback();
        //void Dispose();
        //void DetachAll();
        IRepository<Role> Roles { get; }
        IRepository<User> Users { get; }
        IRepository<Project> Projects { get; }
        IRepository<Tarea> Tareas { get; }
        Task<int> Complete();
    }
}
