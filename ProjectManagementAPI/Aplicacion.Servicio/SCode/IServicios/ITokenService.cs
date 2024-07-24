using Aplicacion.DTO.SCode;
using Persistencia.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicio.SCode.IServicios
{
    public interface ITokenService
    {
        string GenerateToken(UserDto user,int Id);
    }
}
