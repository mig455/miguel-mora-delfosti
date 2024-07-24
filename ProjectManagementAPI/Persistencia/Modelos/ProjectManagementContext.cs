using Microsoft.EntityFrameworkCore;
using Persistencia.Entidades;

namespace Persistencia.Modelos
{
    public class ProjectManagementContext : DbContext
    {
        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options)
           : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
    }
}
