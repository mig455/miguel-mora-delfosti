using Aplicacion.DTO.SCode;
using Aplicacion.Servicio.SCode.IServicios;

namespace ProjectManagementAPI.Helpers
{
    public class SeedData
    {
        public static async Task InitializeAsync(IUserService userService)
        {
            await userService.CreateUserAdministrador();
        }
    }
}
