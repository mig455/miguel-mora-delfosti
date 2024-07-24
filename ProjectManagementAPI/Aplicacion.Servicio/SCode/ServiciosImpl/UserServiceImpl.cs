using Aplicacion.DTO.SCode;
using Aplicacion.Servicio.SCode.IServicios;
using Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Persistencia.Entidades;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Servicio.SCode.ServiciosImpl
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserServiceImpl(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            try
            {
                var user = await _unitOfWork.Users.GetById(id);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al obtener el usuario", ex);
            }
        }
        
        public async Task CreateUserAdministrador()
        {
            try
            {
                var UserAdministrador= await _unitOfWork.Users.GetBy(x => x.Username == "Admin" && x.Email== "Admin@admin.com");
                if(UserAdministrador != null && UserAdministrador.Count()>0)
                {
                    return ;
                }
                User user =new User();
                user.Username = "Admin";
                user.Email = "Admin@admin.com";
                // Cifrar la contraseña
                using (var hmac = new HMACSHA512())
                {
                    user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Admin"));
                    user.PasswordSalt = hmac.Key;
                }
                user.CreatedBy = null;
                user.ModifiedBy = null;
                user.ModifiedDate = DateTime.Now;
                user.CreatedDate = DateTime.Now;
                user.RoleId = 1;
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al crear el usuario Administrador", ex);
            }
        }

        public async Task CreateUser(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);

                // Cifrar la contraseña
                using (var hmac = new HMACSHA512())
                {
                    user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userDto.Password));
                    user.PasswordSalt = hmac.Key;
                }
                user.CreatedBy = null;
                user.ModifiedBy = null;
                user.ModifiedDate = DateTime.Now;
                user.CreatedDate= DateTime.Now;
                user.RoleId = 2;
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al crear el usuario", ex);
            }
        }

        public async Task UpdateUser(int id, UserDto userDto)
        {
            try
            {
                var user = await _unitOfWork.Users.GetById(id);
                _mapper.Map(userDto, user);
                _unitOfWork.Users.Update(user);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // Manejar error o lanzar excepción
                throw new ApplicationException("Error al actualizar el usuario", ex);
            }
        }
        public async Task<UserDto> Authenticate(string username, string password)
        {
            var users = await _unitOfWork.Users.GetBy(x=> x.Username == username);
            if (users == null)
            {
                return null;
            }
            var user = users.FirstOrDefault();
            if (user == null)
            {
                return null;
            }

            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if (!computedHash.SequenceEqual(user.PasswordHash))
                {
                    return null; // Contraseña incorrecta
                }
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.GenerateToken(userDto, user.Id);

            return userDto;
        }
    }
}
