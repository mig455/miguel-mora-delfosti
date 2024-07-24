using Aplicacion.Base.SCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Entidades
{
    public class User : BaseEntidad
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; } 
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
