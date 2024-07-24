using Aplicacion.DTO.SCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicio.SCode.IServicios
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(int id);
        Task CreateUser(UserDto userDto);
        Task UpdateUser(int id, UserDto userDto);
        Task<UserDto> Authenticate(string username, string password);
        Task CreateUserAdministrador();
    }
}
